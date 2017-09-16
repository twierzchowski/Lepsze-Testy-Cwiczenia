using DataAccess;

namespace Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BugManagementContext _context;

        public UnitOfWork(BugManagementContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}