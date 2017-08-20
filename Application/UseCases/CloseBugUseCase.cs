using Domain;

namespace Application
{
    public class CloseBugUseCase : ICommandHandler<CloseBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CloseBugUseCase(IBugRepository repository, IUnitOfWork unitOfWork)
        {
            _bugRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Handle(CloseBugCommand command)
        {
            var bug = _bugRepository.GetById(command.Id);
            bug.Close(command.Reason);
            _unitOfWork.Save();
        }
    }
}