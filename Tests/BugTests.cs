using System;
using Domain;
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
        public void Bug_WhenResolve_ThenStausIsDone()
        {
            //Given
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
            //When
            bug.Resolve();
            //Then
            bug.Status.ShouldBe(Status.Done);
        }

        [Test]
        public void Bug_WhenRenew_ThenStatusIsNew()
        {
            //Given
            Bug bug = new Bug();
            bug.Triage(Severity.High, Priority.High);
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
            bug.Resolve();
            //When
            bug.Close("reason");
            //Then
            bug.IsActive().ShouldBe(false);
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
            bug.Resolve();
            bug.Close("reason");
            return bug;
        }
    }
}
