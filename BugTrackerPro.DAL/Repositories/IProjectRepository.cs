using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project> GetByIdAsync(Guid id);
    Task<Project> GetByIdWithBugsAsync(Guid id);
    Task AddAsync(Project project);
}
