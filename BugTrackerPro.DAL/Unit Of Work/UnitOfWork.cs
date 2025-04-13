using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL.Unit_Of_Work
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly BugContext _context;
        public IBugRepository BugRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public UnitOfWork(BugContext context, IBugRepository bugRepository, IProjectRepository projectRepository)
        {
            _context = context;
            BugRepository = bugRepository;
            ProjectRepository = projectRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
    
}
