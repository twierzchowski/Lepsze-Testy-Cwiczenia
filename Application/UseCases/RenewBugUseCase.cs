using System;
using Application.Commands;
using Domain;

namespace Application.UseCases
{
    public class RenewBugUseCase : ICommandHandler<RenewBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RenewBugUseCase(IBugRepository bugRepository, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _unitOfWork = unitOfWork;
        }

        public void Handle(RenewBugCommand command)
        {
            var bug = _bugRepository.GetById(command.Id);
            if (bug == null)
                throw new Exception($"bug with Id ='{command.Id}' not found");
            bug.Renew();
            _unitOfWork.Save();
        }
    }
}