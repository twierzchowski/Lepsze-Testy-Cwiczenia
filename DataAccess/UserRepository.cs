using System;
using Domain;

namespace DataAccess
{
    class UserRepository : IUserRepository
    {
        private readonly BugManagementContext _context;

        public UserRepository(BugManagementContext context)
        {
            _context = context;
        }
        public User GetUser(Guid id)
        {
            return _context.Users.Find(id);
        }
    }
}