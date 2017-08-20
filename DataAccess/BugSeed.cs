using System;
using System.Data.Entity;
using Domain;

namespace DataAccess
{
    internal class BugSeed
    {
        public static void Seed(BugManagementContext bugManagementContext)
        {
            bugManagementContext.Bugs.Add(new Bug
            {
                Id = Guid.NewGuid(),
                Title = "Cannot create ticker",
                Description = "when clicking create button exception is thrown"
            }
            );
        }
    }
}