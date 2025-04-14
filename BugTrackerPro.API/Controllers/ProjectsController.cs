using BugTrackerPro.BL.DTOs;
using BugTrackerPro.BL.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BugTrackerPro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectManager _projectManager;

    public ProjectsController(IProjectManager projectManager)
    {
        _projectManager = projectManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectDto dto)
    {
        try
        {
            var project = await _projectManager.CreateProjectAsync(dto);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        try
        {
            var projects = await _projectManager.GetAllProjectsAsync();
            return Ok(projects);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        try
        {
            var project = await _projectManager.GetProjectByIdAsync(id);
            return Ok(project);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
} 