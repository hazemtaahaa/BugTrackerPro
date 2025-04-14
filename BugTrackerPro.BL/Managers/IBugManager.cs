using BugTrackerPro.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackerPro.BL.Managers;

public interface IBugManager
{
    Task<BugDto> CreateBugAsync(CreateBugDto dto);
    Task<List<BugDto>> GetAllBugsAsync();
    Task<BugDetailsDto> GetBugByIdAsync(Guid id);
    Task AssignUserToBugAsync(Guid bugId, Guid userId);
    Task RemoveUserFromBugAsync(Guid bugId, Guid userId);
} 