using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyQuangCoBa.Constants;
using MyQuangCoBa.Entities.User;

namespace MyQuangCoBa.Entities;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IPersistedGrantDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<PersistedGrant> PersistedGrants { get; set; }
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    public DbSet<Key> Keys { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppRole>()
            .HasData(
                new AppRole
                {
                    Id = Guid.NewGuid(),
                    Name = RoleTypes.Admin.ToString()
                },
                new AppRole
                {
                    Id = Guid.NewGuid(),
                    Name = RoleTypes.User.ToString()
                });
        
        modelBuilder.Entity<DeviceFlowCodes>()
            .HasNoKey();
        modelBuilder.Entity<PersistedGrant>()
            .HasKey(x => x.Key);
        modelBuilder.Entity<Key>()
            .HasKey(x => x.Id);
    }
}