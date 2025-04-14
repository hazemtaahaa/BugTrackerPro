using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public interface IBugRepository
{
    Task<List<Bug>> GetAllAsync();
    Task<List<Bug>> GetAllWithProjectAsync();
    Task<Bug> GetByIdAsync(Guid id);
    Task<Bug> GetByIdWithDetailsAsync(Guid id);
    Task<Bug> GetByIdWithAttachmentsAsync(Guid id);
    Task AddAsync(Bug bug);
    Task<BugAssignee> GetAssignment(Guid bugId, Guid userId);
    Task AddAssignmentAsync(BugAssignee assignment);
    void RemoveAssignment(BugAssignee assignment);
    Task AddAttachmentAsync(Attachment attachment);
    Task<Attachment> GetAttachmentByIdAsync(Guid id);
    void RemoveAttachment(Attachment attachment);
}
