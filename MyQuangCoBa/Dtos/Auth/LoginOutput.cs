namespace MyQuangCoBa.Dtos.Auth;

public class LoginOutput
{
    public string AccessToken { get; set; }

    public DateTime AccessTokenExpiredTime { get; set; }

    public string RefreshToken { get; set; }
}