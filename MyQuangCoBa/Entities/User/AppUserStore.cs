using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyQuangCoBa.Entities.User;

public class AppUserStore : UserStore<AppUser, AppRole, AppDbContext, Guid>
{
    public AppUserStore(AppDbContext context, IdentityErrorDescriber? describer = null)
        : base(context, describer)
    {
    }
}