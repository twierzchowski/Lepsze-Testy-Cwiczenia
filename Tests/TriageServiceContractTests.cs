using System;
using Domain;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class TriageServiceContractTests
    {
        [Test]
        public void TriageService_WhenSeverityCalled_ThenValidValueIsReturned()
        {
            //Given
            var bugTitle = "test";
            var bugDescirption = "test";
            ITriageBugService triageBugService = new TriageBugService();
            //When
            int actualSeverity = triageBugService.GetSeverity(bugTitle, bugDescirption);
            //Then
            actualSeverity.ShouldBeInRange(100, 250);
        }

        [Test]
        public void TriageService_WhenCalledWithMissingTitle_ThenStatusCodeIsNotSucess()
        {
            //Given
            var bugTitle = string.Empty;
            var bugDescirption = "some description";
            ITriageBugService triageBugService = new TriageBugService();
            //When
            Action action = () => triageBugService.GetSeverity(bugTitle, bugDescirption);
            //Then
            Should.Throw<Exception>(action);
        }

        [Test]
        public void TriageService_WhenPriorityCalled_ThenValidValueIsReturned()
        {
            //Given
            var bugTitle = "test";
            var bugDescirption = "test";
            ITriageBugService triageBugService = new TriageBugService();
            //When
            int actualPriority = triageBugService.GetPriority(bugTitle, bugDescirption);
            //Then
            actualPriority.ShouldBe(1);
        }
    }
}