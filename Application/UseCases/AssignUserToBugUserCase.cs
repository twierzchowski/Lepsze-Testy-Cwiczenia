using Application.Commands;
using Domain;

namespace Application.UseCases
{
    public class AssignUserToBugUserCase : ICommandHandler<AssignUserToBugCommand>
    {
        private readonly IBugRepository _bugRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignUserToBugUserCase(IBugRepository bugRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public void Handle(AssignUserToBugCommand command)
        {
            var user = _userRepository.GetUser(command.UserId);
            var bug = _bugRepository.GetById(command.BugId);
            bug.AssignUser(user);
            _unitOfWork.Save();
        }
    }
}