using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class BugConfigration : IEntityTypeConfiguration<Bug>
{
    public void Configure(EntityTypeBuilder<Bug> builder)
    {
        builder.HasKey(b => b.Id);


        builder.Property(b => b.Description)
               .HasMaxLength(1000);

        // Required foreign key to Project
        builder.HasOne(b => b.Project)
               .WithMany(p => p.Bugs)
               .HasForeignKey(b => b.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);



        // One-to-many: Bug -> Attachments
        builder.HasMany(b => b.Attachments)
               .WithOne(a => a.Bug)
               .HasForeignKey(a => a.BugId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
