using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class AttachmentConfigrations : IEntityTypeConfiguration<Attachment>
{
    

    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        // Configure the primary key
        builder.HasKey(a => a.Id);
        // Configure the properties
        builder.Property(a => a.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.FilePath)
            .IsRequired();

        // One-to-many: Bug -> Attachments
        builder.HasOne(a => a.Bug)
            .WithMany(b => b.Attachments)
            .HasForeignKey(a => a.BugId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

