using System;
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
            if (user == null)
                throw new Exception($"User with Id ='{command.UserId}' not found");
            var bug = _bugRepository.GetById(command.BugId);
            if (bug == null)
                throw new Exception($"bug with Id ='{command.BugId}' not found");
            bug.AssignUser(user);
            _unitOfWork.Save();
        }
    }
}