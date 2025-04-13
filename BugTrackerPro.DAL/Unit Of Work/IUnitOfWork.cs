using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPro.DAL.Unit_Of_Work
{
    internal interface IUnitOfWork
    {
        public IBugRepository BugRepository { get; }

        public IProjectRepository ProjectRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
