using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyQuangCoBa.Constants;
using MyQuangCoBa.Dtos.Auth;
using MyQuangCoBa.Entities.User;

namespace MyQuangCoBa.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _config;
    
    public AuthController(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        SignInManager<AppUser> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _config = config;
    }

    [HttpPost("/auth/register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegisterInput input)
    {
        var user = new AppUser
        {
            UserName = input.PhoneNumber,
            FullName = input.FullName,
            PhoneNumber = input.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            return BadRequest();
        }
        var userRole = await _roleManager.FindByNameAsync(RoleTypes.User.ToString());  
        if (userRole?.Name != null)  
        {  
            await _userManager.AddToRoleAsync(user, userRole.Name);  
        }
        return Ok();
    }

    [HttpPost("/auth/login")]
    public async Task<IActionResult> AuthenticateAsync(LoginInput input)
    {
        var user = await _userManager.FindByNameAsync(input.UserName);
        if (user?.UserName is null)
        {
            return BadRequest();
        }

        var loginResult = await _signInManager.PasswordSignInAsync(user.UserName, input.Password, false, false);
        if (!loginResult.Succeeded)
        {
            return BadRequest();
        }

        var (accessToken, expiredTime) = GenerateAccessToken(user.UserName);

        return Ok(new LoginOutput()
        {
            AccessToken = accessToken,
            AccessTokenExpiredTime = expiredTime,
            RefreshToken = Guid.NewGuid().ToString()
        });
    }
    
    private (string token, DateTime validTo) GenerateAccessToken(string userName)
    {
        var jwtConfig = _config.GetSection("JwtConfig").Get<JwtConfig>();
        var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, userName)
            }),
            Expires = DateTime.UtcNow.AddMinutes(jwtConfig.DurationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), token.ValidTo);
    }
}