using BugTrackerPro.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackerPro.BL.Managers;

public interface IProjectManager
{
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto);
    Task<List<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDetailsDto> GetProjectByIdAsync(Guid id);
} 