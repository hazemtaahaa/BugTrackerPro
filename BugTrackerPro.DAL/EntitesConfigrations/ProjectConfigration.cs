using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class ProjectConfigration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        // Configure the primary key
        builder.HasKey(p => p.Id);

        // Configure the properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);
        // One-to-many: Project -> Bugs
        builder.HasMany(p => p.Bugs)
            .WithOne(b => b.Project)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

      

    }
}
