using System;
using Application.Commands;
using Domain;

namespace Application.UseCases
{
    public class AutoTriageUseCase : ICommandHandler<AutoTriageBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly ITriageBugService _triageBugService;
        private readonly IUnitOfWork _unitOfWork;

        public AutoTriageUseCase(IBugRepository bugRepository, ITriageBugService triageBugService, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _triageBugService = triageBugService;
            _unitOfWork = unitOfWork;
        }
        public void Handle(AutoTriageBugCommand command)
        {
            var bug = _bugRepository.GetById(command.Id);
            if (bug == null)
                throw new Exception($"bug with Id ='{command.Id}' not found");

            var severityFromExternalService = _triageBugService.GetSeverity(bug.Title, bug.Description);
            var priorityFromExternalService = _triageBugService.GetPriority(bug.Title, bug.Description);

            Severity severity = MapSeverityFromExternalService(severityFromExternalService);
            Priority priority = MapPriorityFromExternalService(priorityFromExternalService);
            
            bug.Triage(severity, priority);
            _unitOfWork.Save();
        }

        private Priority MapPriorityFromExternalService(int priorityFromExternalService)
        {
            switch (priorityFromExternalService)
            {
                case 1:
                    return Priority.High;
                case 2:
                    return Priority.Medium;
                case 3:
                    return Priority.Low;
                default:
                    throw new Exception("Invalid priority value");
            }
        }

        private Severity MapSeverityFromExternalService(int severityFromExternalService)
        {
            if (severityFromExternalService < 0)
                throw new Exception("Invalid severity value");
            if (severityFromExternalService < 100)
                return Severity.High;
            if (severityFromExternalService <= 250)
                return Severity.Medium;

            return Severity.Low;
        }
    }
}