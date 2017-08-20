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
        public void Bug_WhenClosingWithReason_ThenBugIsClosed()
        {
            //Given
            var bug = new Bug();
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

        private static Bug CreateClosedBug()
        {
            var bug = new Bug();
            bug.Close("reason");
            return bug;
        }
    }
}
