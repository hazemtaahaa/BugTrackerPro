using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class ProjectRepository : IProjectRepository
{
    private readonly BugContext _context;

    public ProjectRepository(BugContext context)
    {
        _context = context;
    }
    public async void Add(Project project)
    {
        await _context.Set<Project>().AddAsync(project);
    }

    public async Task<List<Project>> GetAll()
    {
       return await _context.Set<Project>()
              .Include(p => p.Bugs)
                .ThenInclude(b => b.Attachments)
                .Include(p => p.Bugs)
                .ThenInclude(b => b.Assignees)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Project> GetById(Guid id)
    {
        return await _context.Set<Project>()
               .Include(p => p.Bugs)
               .ThenInclude(b => b.Attachments)
               .Include(p => p.Bugs)
               .ThenInclude(b => b.Assignees)
               .AsSplitQuery()
               .FirstOrDefaultAsync(p => p.Id == id);
    }
}
