using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL;

public interface IProjectRepository
{
    public Task<List<Project>> GetAll();

    public Task<Project> GetById(Guid id);

    public void Add(Project project);
}
