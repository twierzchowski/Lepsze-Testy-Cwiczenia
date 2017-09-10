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
            var bug = _bugRepository.GetById(command.BugId);
            var severity = _triageBugService.GetSeverity(bug.Title);
            var priority = _triageBugService.GetPriority(bug.Title);
            bug.Triage(new Severity(severity), new Priority(priority));
            _unitOfWork.Save();
        }
    }
}