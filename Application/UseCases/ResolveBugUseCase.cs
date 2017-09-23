using System;
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
            if (bug == null)
                throw new Exception($"bug with Id ='{command.Id}' not found");
            bug.Resolve();
            _unitOfWork.Save();
        }
    }
}