using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerPro.BL.DTOs;

public class BugDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
}

public class BugDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    public List<UserDto> Assignees { get; set; } = new List<UserDto>();
    public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
}

public class CreateBugDto
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public Guid ProjectId { get; set; }
}

public class AssignBugDto
{
    [Required]
    public Guid UserId { get; set; }
} 