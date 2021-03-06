﻿using System;
using Domain;
using Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class BugTests
    {
        [Test]
        public void Bug_WhenCreated_ThenHaveMediumPriorityAndSeverity()
        {
            //Given
            var bug = new Bug();
            //Then
            bug.Priority.ShouldBe(Priority.Medium);
            bug.Severity.ShouldBe(Severity.Medium);
        }

        [Test]
        public void Bug_WhenTriaged_ThenStatusIsTodo()
        {
            //Given
            Bug bug = new Bug();
            //When
            bug.Triage(Severity.High, Priority.High);
            //Then
            bug.Status.ShouldBe(Status.Todo);
        }

        [Test]
        public void Bug_WhenTriageInWrongStatus_ThenExceptionIsThrown()
        {
            //Given
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            //When
            Action action = () => bug.Triage(Severity.High, Priority.High);
            //Then
            Should.Throw<DomainException>(action);
        }

        [Test]
        public void Bug_WhenAutoTriage_ThenSeverityAndPriorityIsSetFromService()
        {
            //Given
            Bug bug = new Bug();
            //When
            bug.AutoTriage(new TraigeServiceMock());
            //Then
            bug.Severity.ShouldBe(Severity.Low);
            bug.Priority.ShouldBe(Priority.Low);
        }

        [Test]
        public void Bug_WhenResolve_ThenStausIsDone()
        {
            //Given
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            var mondayDate = new DateTime(2017, 10, 2);
            TimeProvider.Current = new TestTimeProvider(mondayDate);
            //When
            bug.Resolve();
            //Then
            bug.Status.ShouldBe(Status.Done);
        }

        [Test]
        public void Bug_WhenResolveWithoutAssignedUser_ThenExceptionIsThrown()
        {
            //Given
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            //When
            Action action = () => bug.Resolve();
            //Then
            Should.Throw<DomainException>(action);
        }

        [Test]
        public void Bug_WhenResolveOnWeekend_ThenExceptionIsThrown()
        {
            //Given
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            var sundayDate = new DateTime(2017, 10, 1);
            TimeProvider.Current = new TestTimeProvider(sundayDate);
            //When
            Action action = () => bug.Resolve();
            //Then
            Should.Throw<DomainException>(action);
        }

        [Test]
        public void Bug_WhenRenew_ThenStatusIsNew()
        {
            //Given
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            var mondayDate = new DateTime(2017, 10, 2);
            TimeProvider.Current = new TestTimeProvider(mondayDate);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            bug.Resolve();
            //When
            bug.Renew();
            //Then
            bug.Status.ShouldBe(Status.New);
        }
        [Test]
        public void Bug_WhenClosingWithReason_ThenBugIsClosed()
        {
            //Given
            var bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            var mondayDate = new DateTime(2017, 10, 2);
            TimeProvider.Current = new TestTimeProvider(mondayDate);
            bug.Resolve();
            //When
            bug.Close("reason");
            //Then
            bug.IsActive().ShouldBe(false);
        }

        [Test]
        public void Bug_WhenClosing_ThenNoUserIsAssigned()
        {
            //Given
            var bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            var mondayDate = new DateTime(2017, 10, 2);
            TimeProvider.Current = new TestTimeProvider(mondayDate);
            bug.Resolve();
            //When
            bug.Close("reason");
            //Then
            bug.AssignedUser.ShouldBeNull();
        }

        [Test]
        public void Bug_WhenClosingAlreadyClosed_ThenExceptionIsThrown()
        {
            //Given
            Bug bug = CreateClosedBug();
            //When
            Action action = () => bug.Close("reason");
            //Then
            Should.Throw<DomainException>(action);
        }

        [Test]
        public void Bug_WhenCheckingIsActive_ThenCorrectValueIsReturned()
        {
            //Given
            Bug bug = new Bug();
            //Then
            bug.IsActive().ShouldBe(true);
        }

        [Test]
        public void Bug_WhenClosingNewBug_ThenExceptionIsThrown()
        {
            //Given
            Bug bug = new Bug();
            //When
            Action action = () => bug.Close("reason");
            //Then
            Should.Throw<DomainException>(action);
        }

        private static Bug CreateClosedBug()
        {
            var bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            bug.AssignUser(new User("testuser", UserRole.Dev));
            bug.Resolve();
            bug.Close("reason");
            return bug;
        }
    }

    public class TraigeServiceMock : ITriageBugService
    {
        public int GetSeverity(string title, string description)
        {
            return 500;
        }

        public int GetPriority(string title, string description)
        {
            return 3;
        }
    }
}
