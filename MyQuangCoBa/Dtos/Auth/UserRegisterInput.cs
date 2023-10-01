using System.ComponentModel.DataAnnotations;

namespace MyQuangCoBa.Dtos.Auth;

public class UserRegisterInput
{
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; }

    [Required]
    [MaxLength(30)]
    public string Password { get; set; }

    [Required]
    [MaxLength(30)]
    public string PasswordConfirm { get; set; }
}