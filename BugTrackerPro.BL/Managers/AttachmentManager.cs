using BugTrackerPro.BL.DTOs;
using BugTrackerPro.DAL;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerPro.BL.Managers;

public class AttachmentManager : IAttachmentManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AttachmentManager(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<AttachmentDto> UploadAttachmentAsync(Guid bugId, CreateAttachmentDto dto)
    {
        var bug = await _unitOfWork.BugRepository.GetByIdAsync(bugId);
        if (bug == null)
        {
            throw new Exception("Bug not found");
        }

        // Create directory for attachments if it doesn't exist
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "attachments", bugId.ToString());
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // Create unique filename
        var uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Save file
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(fileStream);
        }

        // Save attachment info to database
        var attachment = new Attachment
        {
            Id = Guid.NewGuid(),
            FileName = dto.File.FileName,
            FilePath = $"/attachments/{bugId}/{uniqueFileName}",
            BugId = bugId
        };

        await _unitOfWork.BugRepository.AddAttachmentAsync(attachment);
        await _unitOfWork.SaveChangesAsync();

        return new AttachmentDto
        {
            Id = attachment.Id,
            FileName = attachment.FileName,
            FilePath = attachment.FilePath,
            BugId = attachment.BugId
        };
    }

    public async Task<List<AttachmentDto>> GetAttachmentsForBugAsync(Guid bugId)
    {
        var bug = await _unitOfWork.BugRepository.GetByIdWithAttachmentsAsync(bugId);
        if (bug == null)
        {
            throw new Exception("Bug not found");
        }

        return bug.Attachments?.Select(a => new AttachmentDto
        {
            Id = a.Id,
            FileName = a.FileName,
            FilePath = a.FilePath,
            BugId = a.BugId
        }).ToList() ?? new List<AttachmentDto>();
    }

    public async Task DeleteAttachmentAsync(Guid id)
    {
        var attachment = await _unitOfWork.BugRepository.GetAttachmentByIdAsync(id);
        if (attachment == null)
        {
            throw new Exception("Attachment not found");
        }

        // Delete file from disk
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, attachment.FilePath.TrimStart('/'));
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // Delete from database
        _unitOfWork.BugRepository.RemoveAttachment(attachment);
        await _unitOfWork.SaveChangesAsync();
    }
} 