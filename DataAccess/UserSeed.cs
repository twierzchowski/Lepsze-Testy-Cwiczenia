using System.Collections.Generic;
using Domain;

namespace DataAccess
{
    internal class UserSeed
    {
        public static void Seed(BugManagementContext context)
        {
            context.Users.AddRange(
                new List<User> {
                    new User("Tomek", UserRole.Qa),
                    new User("Agata", UserRole.Dev),
                    new User("Adam", UserRole.Dev),
                    new User("Marta", UserRole.Qa)
                });
        }
    }
}