using BugTrackerPro.BL.DTOs;
using BugTrackerPro.BL.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BugTrackerPro.API.Controllers;

[Route("api/bugs/{bugId}/[controller]")]
[ApiController]
[Authorize]
public class AttachmentsController : ControllerBase
{
    private readonly IAttachmentManager _attachmentManager;

    public AttachmentsController(IAttachmentManager attachmentManager)
    {
        _attachmentManager = attachmentManager;
    }

    [HttpPost]
    public async Task<IActionResult> UploadAttachment(Guid bugId, [FromForm] CreateAttachmentDto dto)
    {
        try
        {
            var attachment = await _attachmentManager.UploadAttachmentAsync(bugId, dto);
            return CreatedAtAction(nameof(GetAttachments), new { bugId = bugId }, attachment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAttachments(Guid bugId)
    {
        try
        {
            var attachments = await _attachmentManager.GetAttachmentsForBugAsync(bugId);
            return Ok(attachments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttachment(Guid id)
    {
        try
        {
            await _attachmentManager.DeleteAttachmentAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 