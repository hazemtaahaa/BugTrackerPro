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
    public async void Add(Bug bug)
    {
      await _context.Set<Bug>().AddAsync(bug);
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
