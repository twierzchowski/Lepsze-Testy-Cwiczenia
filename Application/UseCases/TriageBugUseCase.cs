using System;
using Application.Commands;
using Domain;

namespace Application.UseCases
{
    public class TriageBugUseCase : ICommandHandler<TriageBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TriageBugUseCase(IBugRepository bugRepository, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _unitOfWork = unitOfWork;
        }
        public void Handle(TriageBugCommand command)
        {
            var bug = _bugRepository.GetById(command.Id);
            if (bug == null)
                throw new Exception($"bug with Id ='{command.Id}' not found");

            bug.Triage(command.Severity, command.Priority);
            _unitOfWork.Save();
        }
    }
}