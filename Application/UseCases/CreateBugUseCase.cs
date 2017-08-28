using Domain;

namespace Application.UseCases
{
    public class CreateBugUseCase : ICommandHandler<CreateBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBugUseCase(IBugRepository bugRepository, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _unitOfWork = unitOfWork;
        }

        public void Handle(CreateBugCommand command)
        {
            Bug bug = new Bug
            {
                Id = command.Id,
                Title = command.Title,
                Description = command.Description
            };

            _bugRepository.Store(bug);
            _unitOfWork.Save();
        }
    }
}
