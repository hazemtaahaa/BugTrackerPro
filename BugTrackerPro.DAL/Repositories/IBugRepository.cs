using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public interface IBugRepository

{
    public Task<List<Bug>> GetAll();

    public Task<Bug> GetById(Guid id);

    public void Add(Bug bug);

    Task<User?> GetUserByIdAsync(Guid userId);
    Task<bool> AssignUserAsync(Guid bugId, Guid userId);
    Task<bool> UnassignUserAsync(Guid bugId, Guid userId);
}
