using System;
using Microsoft.AspNetCore.Http;

namespace BugTrackerPro.BL.DTOs;

public class AttachmentDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public Guid BugId { get; set; }
}

public class CreateAttachmentDto
{
    public IFormFile File { get; set; }
} 