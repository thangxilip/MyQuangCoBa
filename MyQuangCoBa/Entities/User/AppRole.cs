using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyQuangCoBa.Entities.User;

[Table("AppRoles")]
public class AppRole : IdentityRole<Guid>
{
    
}