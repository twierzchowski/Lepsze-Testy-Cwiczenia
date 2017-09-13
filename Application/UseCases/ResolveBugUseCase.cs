using Application.Commands;
using Domain;

namespace Application.UseCases
{
    public class ResolveBugUseCase : ICommandHandler<ResolveBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResolveBugUseCase(IBugRepository bugRepository, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _unitOfWork = unitOfWork;
        }

        public void Handle(ResolveBugCommand command)
        {
            var bug = _bugRepository.GetById(command.Id);
            bug.Resolve();
            _unitOfWork.Save();
        }
    }
}