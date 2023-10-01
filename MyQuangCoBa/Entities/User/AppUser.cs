using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyQuangCoBa.Entities.User;

[Table("AppUsers")]
public class AppUser : IdentityUser<Guid>
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
}