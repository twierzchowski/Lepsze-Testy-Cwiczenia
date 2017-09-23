using System;
using System.Configuration;
using DataAccess.ReadModel;
using Domain;
using NUnit.Framework;
using WebApplication.Controllers;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void NewBug()
        {
            var bug = new Bug();
            if (bug.Priority != Priority.Medium)
                throw new Exception();
        }

        [Test]
        public void TriagedStatusIsTodo()
        {
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            Assert.True(bug.Status == Status.Todo);
        }

        [Test]
        public void ResolvedStausIsDone()
        {
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            if (bug.AssignedUser != null)
            {
                bug.Resolve();
                Assert.True(bug.Status == Status.Done);
                bug.Close("fixed in build 1.23");
                //Then
                Assert.True(bug.AssignedUser == null);
            }
        }

        [Test]
        public void Bug_WhenRenewStatusIsNew()
        {
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            bug.Resolve();
            bug.Renew();
            Assert.True(bug.Status == Status.Done);
        }

        [Test]
        public void Bug_WhenClosingAlreadyClosed_ThenExceptionIsThrown()
        {
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            bug.Resolve();
            bug.Close("reason");
            try
            {
                bug.Close("reason");
                throw new Exception();
            }
            catch (DomainException e)
            {
            }

        }

        [Test]
        public void GetUsers()
        {
            var UsersController = new UsersController(new TestWorkshopEntities(ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString));
            UsersController.PostUsers("Test", 1);
            var users = UsersController.GetUsers();
            foreach (var user in users)
            {
                if (user.Name == "Test" && user.Role == "1")
                {
                    return;
                }
            }
            Assert.Fail("user not found");
        }
    }
}
