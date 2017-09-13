using Application.Commands;
using Domain;

namespace Application.UseCases
{
    public class AutoTriageUseCase : ICommandHandler<AutoTriageBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly ITiageBugService _triageBugService;
        private readonly IUnitOfWork _unitOfWork;

        public AutoTriageUseCase(IBugRepository bugRepository, ITiageBugService triageBugService, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _triageBugService = triageBugService;
            _unitOfWork = unitOfWork;
        }
        public void Handle(AutoTriageBugCommand command)
        {
            var bug = _bugRepository.GetById(command.Id);
            var severity = _triageBugService.GetSeverity(bug.Title);
            var priority = _triageBugService.GetPriority(bug.Title);
            Severity S;
            Priority P;
            switch (severity)
            {
                case 1:
                    S = Severity.High;
                    break;
                case 2:
                    S = Severity.Medium;
                    break;
                case 3:
                    S = Severity.Low;
                    break;
                default:
                    S = Severity.Medium;
                    break;
            }

            switch (priority)
            {
                case 1:
                    P = Priority.High;
                    break;
                case 2:
                    P = Priority.Medium;
                    break;
                case 3:
                    P = Priority.Low;
                    break;
                default:
                    P = Priority.Medium;
                    break;
            }
            bug.Triage(S, P);
            _unitOfWork.Save();
        }
    }
}