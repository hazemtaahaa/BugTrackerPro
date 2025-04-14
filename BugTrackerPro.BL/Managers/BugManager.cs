using BugTrackerPro.BL.DTOs;
using BugTrackerPro.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerPro.BL.Managers;

public class BugManager : IBugManager
{
    private readonly IUnitOfWork _unitOfWork;

    public BugManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BugDto> CreateBugAsync(CreateBugDto dto)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(dto.ProjectId);
        if (project == null)
        {
            throw new Exception("Project not found");
        }

        var bug = new Bug
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            ProjectId = dto.ProjectId
        };

        await _unitOfWork.BugRepository.AddAsync(bug);
        await _unitOfWork.SaveChangesAsync();

        return new BugDto
        {
            Id = bug.Id,
            Title = bug.Title,
            Description = bug.Description,
            ProjectId = bug.ProjectId,
            ProjectName = project.Name
        };
    }

    public async Task<List<BugDto>> GetAllBugsAsync()
    {
        var bugs = await _unitOfWork.BugRepository.GetAllWithProjectAsync();
        
        return bugs.Select(b => new BugDto
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            ProjectId = b.ProjectId,
            ProjectName = b.Project?.Name
        }).ToList();
    }

    public async Task<BugDetailsDto> GetBugByIdAsync(Guid id)
    {
        var bug = await _unitOfWork.BugRepository.GetByIdWithDetailsAsync(id);
        if (bug == null)
        {
            throw new Exception("Bug not found");
        }

        var bugDetailsDto = new BugDetailsDto
        {
            Id = bug.Id,
            Title = bug.Title,
            Description = bug.Description,
            ProjectId = bug.ProjectId,
            ProjectName = bug.Project?.Name,
            Assignees = bug.Assignees?.Select(a => new UserDto
            {
                Id = a.User.Id,
                UserName = a.User.UserName,
                Email = a.User.Email
            }).ToList() ?? new List<UserDto>(),
            Attachments = bug.Attachments?.Select(a => new AttachmentDto
            {
                Id = a.Id,
                FileName = a.FileName,
                FilePath = a.FilePath,
                BugId = a.BugId
            }).ToList() ?? new List<AttachmentDto>()
        };

        return bugDetailsDto;
    }

    public async Task AssignUserToBugAsync(Guid bugId, Guid userId)
    {
        var bug = await _unitOfWork.BugRepository.GetByIdAsync(bugId);
        if (bug == null)
        {
            throw new Exception("Bug not found");
        }

        var existingAssignment = await _unitOfWork.BugRepository.GetAssignment(bugId, userId);
        if (existingAssignment != null)
        {
            throw new Exception("User is already assigned to this bug");
        }

        var assignment = new BugAssignee
        {
            BugId = bugId,
            UserId = userId
        };

        await _unitOfWork.BugRepository.AddAssignmentAsync(assignment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveUserFromBugAsync(Guid bugId, Guid userId)
    {
        var assignment = await _unitOfWork.BugRepository.GetAssignment(bugId, userId);
        if (assignment == null)
        {
            throw new Exception("User is not assigned to this bug");
        }

        _unitOfWork.BugRepository.RemoveAssignment(assignment);
        await _unitOfWork.SaveChangesAsync();
    }
} 