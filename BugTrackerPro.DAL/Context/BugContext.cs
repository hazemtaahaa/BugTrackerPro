using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerPro.DAL;


public class BugContext : IdentityDbContext<User, Role, Guid>
{
    public BugContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Bug> Bugs { get; set; }
    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BugContext).Assembly);
    }
}
