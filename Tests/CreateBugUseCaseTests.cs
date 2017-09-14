using System;
using System.Collections.Generic;
using System.Configuration;
using Application;
using Application.Commands;
using Application.UseCases;
using Autofac;
using DataAccess;
using Domain;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class CreateBugUseCaseTest
    {
        [Test]
        public void Moq_CreateBug_WhenNewBugCreated_ThenBugIsSaved()
        {
            //Given
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBugRepository = new Mock<IBugRepository>();

            var createBugUseCase = new CreateBugUseCase(mockBugRepository.Object, mockUnitOfWork.Object);
            var guid = Guid.NewGuid();
            var createBugCommand = new CreateBugCommand { Description = "Description", Title = "Title", Id = guid };
            
            //When
            createBugUseCase.Handle(createBugCommand);

            //Then
            mockUnitOfWork.Verify(work => work.Save());
        }

        [Test]
        public void SimpleMockImplementation_CreateBug_WhenNewBugCreated_ThenRepositoryContainsCreatedBug()
        {
            //Given
            IBugRepository bugRepository = new MockBugRepository();
            IUnitOfWork unitOfWork = new MockUnitOfWork();

            var createBugUseCase = new CreateBugUseCase(bugRepository, unitOfWork);
            var guid = Guid.NewGuid();
            var createBugCommand = new CreateBugCommand { Description = "Description", Title = "Title", Id = guid };

            //When
            createBugUseCase.Handle(createBugCommand);

            //Then
            bugRepository.GetById(guid).Description.ShouldBe("Description");
            bugRepository.GetById(guid).Title.ShouldBe("Title");
            bugRepository.GetById(guid).Status.ShouldBe(Status.New);
        }

        [Test]
        public void RealImplementation_CreateBug_WhenNewBugCreated_ThenRepositoryContainsCreatedBug()
        {
            //DI setup
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            containerBuilder.RegisterAssemblyTypes(typeof(DbBugRepository).Assembly)
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder.Register(c => new BugManagementContext(ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString)).InstancePerLifetimeScope();
            var container = containerBuilder.Build();

            //Given
            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();
            IBugRepository bugRepository = container.Resolve<IBugRepository>();

            var createBugUseCase = new CreateBugUseCase(bugRepository, unitOfWork);
            var guid = Guid.NewGuid();
            var createBugCommand = new CreateBugCommand { Description = "Description", Title = "Title", Id = guid };

            //When
            createBugUseCase.Handle(createBugCommand);

            //Then
            bugRepository.GetById(guid).Description.ShouldBe("Description");
            bugRepository.GetById(guid).Title.ShouldBe("Title");
            bugRepository.GetById(guid).Status.ShouldBe(Status.New);
        }
    }

    public class MockUnitOfWork : IUnitOfWork
    {
        public void Save()
        { }
    }

    public class MockBugRepository : IBugRepository
    {
        private List<Bug> _bugs = new List<Bug>();
        public Bug GetById(Guid bugId)
        {
            return _bugs.Find(b => b.Id == bugId);
        }

        public void Store(Bug bug)
        {
            _bugs.Add(bug);
        }
    }
}