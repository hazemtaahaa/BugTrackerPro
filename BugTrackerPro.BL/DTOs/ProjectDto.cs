using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerPro.BL.DTOs;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class ProjectDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<BugDto> Bugs { get; set; } = new List<BugDto>();
}

public class CreateProjectDto
{
    [Required]
    public string Name { get; set; }
} 