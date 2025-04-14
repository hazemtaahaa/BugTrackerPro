using BugTrackerPro.BL.DTOs;
using BugTrackerPro.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerPro.BL.Managers;

public class ProjectManager : IProjectManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _unitOfWork.ProjectRepository.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name
        };
    }

    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
        
        return projects.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name
        }).ToList();
    }

    public async Task<ProjectDetailsDto> GetProjectByIdAsync(Guid id)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdWithBugsAsync(id);
        if (project == null)
        {
            throw new Exception("Project not found");
        }

        var projectDetailsDto = new ProjectDetailsDto
        {
            Id = project.Id,
            Name = project.Name,
            Bugs = project.Bugs?.Select(b => new BugDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ProjectId = b.ProjectId,
                ProjectName = project.Name
            }).ToList() ?? new List<BugDto>()
        };

        return projectDetailsDto;
    }
} 