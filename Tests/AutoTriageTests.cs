using Application;
using Application.Commands;
using Application.UseCases;
using Domain;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class AutoTriageTests
    {
        [Test]
        public void Moq_AutoTriage_WhenCalled_ThenSeverityAndPriorityAreAssign()
        {
            //Given
            Bug bug = new Bug();
            var bugRepositoryMock = new Mock<IBugRepository>();
            bugRepositoryMock.Setup(mock => mock.GetById(bug.Id)).Returns(bug);
            var bugRepository = bugRepositoryMock.Object;

            var triageBugServiceMock = new Mock<ITriageBugService>();
            triageBugServiceMock.Setup(mock => mock.GetSeverity(bug.Title, bug.Description)).Returns(149);
            triageBugServiceMock.Setup(mock => mock.GetPriority(bug.Title, bug.Description)).Returns(1);
            var triageBugService = triageBugServiceMock.Object;

            IUnitOfWork unitOfWork = new Mock<IUnitOfWork>().Object;
            
            AutoTriageUseCase useCase = new AutoTriageUseCase(bugRepository, triageBugService, unitOfWork);
            //When
            useCase.Handle(new AutoTriageBugCommand{Id = bug.Id});
            //Then
            bug.Status.ShouldBe(Status.Todo);
            bug.Priority.ShouldBe(Priority.High);
            bug.Severity.ShouldBe(Severity.Medium);
        }

        [Test]
        public void AutoTriage_WhenCalled_ThenSeverityAndPriorityAreAssigned()
        {
            //Given
            Bug bug = new Bug();
            var bugRepository = new MockBugRepository();
            bugRepository.Store(bug);

            var triageBugService = new MockTriageBugService();

            IUnitOfWork unitOfWork = new Mock<IUnitOfWork>().Object;

            AutoTriageUseCase useCase = new AutoTriageUseCase(bugRepository, triageBugService, unitOfWork);
            //When
            useCase.Handle(new AutoTriageBugCommand { Id = bug.Id });
            //Then
            bug.Status.ShouldBe(Status.Todo);
            bug.Priority.ShouldBe(Priority.High);
            bug.Severity.ShouldBe(Severity.Medium);
        }
    }

    public class MockTriageBugService : ITriageBugService
    {
        public int GetSeverity(string title, string description)
        {
            return 149;
        }

        public int GetPriority(string title, string description)
        {
            return 1;
        }
    }
}