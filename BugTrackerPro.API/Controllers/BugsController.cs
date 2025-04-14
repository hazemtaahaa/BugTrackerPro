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
public class BugsController : ControllerBase
{
    private readonly IBugManager _bugManager;

    public BugsController(IBugManager bugManager)
    {
        _bugManager = bugManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBug(CreateBugDto dto)
    {
        try
        {
            var bug = await _bugManager.CreateBugAsync(dto);
            return CreatedAtAction(nameof(GetBugById), new { id = bug.Id }, bug);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBugs()
    {
        try
        {
            var bugs = await _bugManager.GetAllBugsAsync();
            return Ok(bugs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBugById(Guid id)
    {
        try
        {
            var bug = await _bugManager.GetBugByIdAsync(id);
            return Ok(bug);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/assignees")]
    public async Task<IActionResult> AssignUserToBug(Guid id, AssignBugDto dto)
    {
        try
        {
            await _bugManager.AssignUserToBugAsync(id, dto.UserId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}/assignees/{userId}")]
    public async Task<IActionResult> RemoveUserFromBug(Guid id, Guid userId)
    {
        try
        {
            await _bugManager.RemoveUserFromBugAsync(id, userId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 