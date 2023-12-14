using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Entities;

namespace ProjectManager.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Todo> Todos { get; set; } = null!;

    public DbSet<Project> Projects { get; set; } = null!;

    public DbSet<Email> Emails { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
        modelBuilder.Ignore<IdentityRole<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        //modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
    }
}
