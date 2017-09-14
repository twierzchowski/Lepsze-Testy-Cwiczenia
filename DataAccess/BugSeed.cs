using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DataAccess
{
    internal class BugSeed
    {
        public static void Seed(BugManagementContext context)
        {
            var bug1 = new Bug
            {
                Id = Guid.NewGuid(),
                Title = "Cannot create ticket",
                Description = "When clicking create button exception is thrown"
            };
            bug1.AssignUser(context.Users.First(u => u.Role == UserRole.Dev));

            var bug2 = new Bug
            {
                Id = Guid.NewGuid(),
                Title = "Create button has typo 'crete'",
                Description = "Create button has text 'crete' instead of 'create'"
            };
            bug2.Triage(Severity.Low, Priority.Low);

            var bug3 = new Bug
            {
                Id = Guid.NewGuid(),
                Title = "Cannot assign bug to developer",
                Description = "There should be possiblity to assign bug to developer so that everyone knows who is fixing the bug"
            };
            bug3.Triage(Severity.Medium, Priority.High);
            bug3.AssignUser(context.Users.First(u => u.Role == UserRole.Qa));
            bug3.Resolve();

            var bug4 = new Bug
            {
                Id = Guid.NewGuid(),
                Title = "Poor performance of application",
                Description = "Application is slow, almost every action requires >5sec to complete."
            };
            context.Bugs.AddRange(new List<Bug>
            {
                bug1,
                bug2,
                bug3,
                bug4
            });
        }
    }
}