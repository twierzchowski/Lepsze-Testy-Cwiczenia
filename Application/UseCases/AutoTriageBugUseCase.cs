using System;
using Application.Commands;
using Domain;

namespace Application.UseCases
{
    public class AutoTriageUseCase : ICommandHandler<AutoTriageBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AutoTriageUseCase(IBugRepository bugRepository, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _unitOfWork = unitOfWork;
        }
        public void Handle(AutoTriageBugCommand command)
        {
            var bug = _bugRepository.GetById(command.Id);
            if (bug == null)
                throw new Exception($"bug with Id ='{command.Id}' not found");
            
            bug.AutoTriage(new TriageBugService());
            _unitOfWork.Save();
        }
    }
}