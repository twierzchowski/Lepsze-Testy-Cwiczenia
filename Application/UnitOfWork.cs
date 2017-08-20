using DataAccess;
using EnsureThat;

namespace Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BugManagementContext _context;

        public UnitOfWork(BugManagementContext context)
        {
            Ensure.That(context).IsNotNull();

            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}