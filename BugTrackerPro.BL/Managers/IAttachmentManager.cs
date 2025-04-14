using BugTrackerPro.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackerPro.BL.Managers;

public interface IAttachmentManager
{
    Task<AttachmentDto> UploadAttachmentAsync(Guid bugId, CreateAttachmentDto dto);
    Task<List<AttachmentDto>> GetAttachmentsForBugAsync(Guid bugId);
    Task DeleteAttachmentAsync(Guid id);
} 