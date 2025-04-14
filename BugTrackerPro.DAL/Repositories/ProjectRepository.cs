using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public class ProjectRepository : IProjectRepository
{
    private readonly BugContext _context;

    public ProjectRepository(BugContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project> GetByIdWithBugsAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Bugs)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
    }
}
