using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class BugAssigneeConfigrations : IEntityTypeConfiguration<BugAssignee>
{
    public void Configure(EntityTypeBuilder<BugAssignee> builder)
    {

        builder.HasKey(ba => new { ba.BugId, ba.UserId });
        // Configure the relationships
        builder.HasOne(ba => ba.Bug)
            .WithMany(b => b.Assignees)
            .HasForeignKey(ba => ba.BugId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(ba => ba.User)
            .WithMany(u => u.Assignees)
            .HasForeignKey(ba => ba.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
