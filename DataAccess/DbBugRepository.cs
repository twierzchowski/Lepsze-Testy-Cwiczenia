using System;
using Domain;

namespace DataAccess
{
    public class DbBugRepository : IBugRepository
    {
        private readonly BugManagementContext _context;

        public DbBugRepository(BugManagementContext context)
        {
            _context = context;
        }
        public Bug GetById(Guid id)
        {
            return _context.Bugs.Find(id);
        }

        public void Store(Bug bug)
        {
            _context.Bugs.Add(bug);
        }
    }
}