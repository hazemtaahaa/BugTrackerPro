using System.Threading.Tasks;

namespace BugTrackerPro.DAL
{
    public class UnitOfWork : IUnitOfWork
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
