using System;
using Application;
using Application.UseCases;
using Domain;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CreateBugUseCaseTest
    {
        [Test]
        public void CreateBug_InitialCondition_IsSaved()
        {
            //Given
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBugRepository = Mock.Of<IBugRepository>();

            var createBugUseCase = new CreateBugUseCase(mockBugRepository, mockUnitOfWork.Object);
            var createBugCommand = new CreateBugCommand { Description = "Description", Title = "Title", Id = Guid.NewGuid() };
            
            //When
            createBugUseCase.Handle(createBugCommand);

            //Then
            mockUnitOfWork.Verify(work => work.Save());
        }
    }
}