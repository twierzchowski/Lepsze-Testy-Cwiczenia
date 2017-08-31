using System;
using System.Collections.Generic;
using Domain;

namespace DataAccess
{
    internal class BugSeed
    {
        public static void Seed(BugManagementContext bugManagementContext)
        {
            bugManagementContext.Bugs.AddRange(new List<Bug>
            {
                new Bug
                {
                    Id = Guid.NewGuid(),
                    Title = "Cannot create ticket",
                    Description = "when clicking create button exception is thrown"
                },
                new Bug
                {
                    Id = Guid.NewGuid(),
                    Title = "Create button has typo 'crete'",
                    Description = "create button has text 'crete' instead of 'create'"
                },
                new Bug
                {
                    Id = Guid.NewGuid(),
                    Title = "Cannot assign bug to developer",
                    Description = "There should be possiblity to assign bug to developer so that everyone knows who is fixing the bug"
                }
            });
        }
    }
}