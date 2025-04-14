using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class BugRepository : IBugRepository
{
    private readonly BugContext _context;

    public BugRepository(BugContext context)
    {
        _context = context;
    }

    public async Task<List<Bug>> GetAllAsync()
    {
        return await _context.Bugs.ToListAsync();
    }

    public async Task<List<Bug>> GetAllWithProjectAsync()
    {
        return await _context.Bugs
            .Include(b => b.Project)
            .ToListAsync();
    }

    public async Task<Bug> GetByIdAsync(Guid id)
    {
        return await _context.Bugs.FindAsync(id);
    }

    public async Task<Bug> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Bugs
            .Include(b => b.Project)
            .Include(b => b.Assignees)
                .ThenInclude(a => a.User)
            .Include(b => b.Attachments)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Bug> GetByIdWithAttachmentsAsync(Guid id)
    {
        return await _context.Bugs
            .Include(b => b.Attachments)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(Bug bug)
    {
        await _context.Bugs.AddAsync(bug);
    }

    public async Task<BugAssignee> GetAssignment(Guid bugId, Guid userId)
    {
        return await _context.Set<BugAssignee>()
            .FirstOrDefaultAsync(a => a.BugId == bugId && a.UserId == userId);
    }

    public async Task AddAssignmentAsync(BugAssignee assignment)
    {
        await _context.Set<BugAssignee>().AddAsync(assignment);
    }

    public void RemoveAssignment(BugAssignee assignment)
    {
        _context.Set<BugAssignee>().Remove(assignment);
    }

    public async Task AddAttachmentAsync(Attachment attachment)
    {
        await _context.Attachments.AddAsync(attachment);
    }

    public async Task<Attachment> GetAttachmentByIdAsync(Guid id)
    {
        return await _context.Attachments.FindAsync(id);
    }

    public void RemoveAttachment(Attachment attachment)
    {
        _context.Attachments.Remove(attachment);
    }

    public async Task<bool> AssignUserAsync(Guid bugId, Guid userId)
    {
        var bug = await GetById(bugId);
        var user = await GetUserByIdAsync(userId);
        if (bug == null || user == null || bug.Assignees.Any(u => u.UserId == userId)) return false;

        bug.Assignees.Add(new BugAssignee
        {
            BugId = bugId,
            UserId = userId,
            Bug = bug,
            User = user
        });
        return true;
    }

    public async Task<List<Bug>> GetAll()
    {
       return await _context.Set<Bug>()
            .Include(b => b.Project)
            .Include(b => b.Assignees)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Bug> GetById(Guid id)
    {
      return await _context.Set<Bug>()
            .Include(b => b.Project)
            .Include(b => b.Assignees)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<bool> UnassignUserAsync(Guid bugId, Guid userId)
    {
        var bug = await GetById(bugId);
        if (bug == null) return false;

        var user = bug.Assignees.FirstOrDefault(u => u.UserId == userId);
        if (user == null) return false;

        bug.Assignees.Remove(user);
        return true;
    }
}
