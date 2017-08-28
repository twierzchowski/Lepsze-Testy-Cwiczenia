﻿using Domain;

namespace Application.UseCases
{
    public class CloseBugUseCase : ICommandHandler<CloseBugCommand>
    {
        private readonly IBugRepository _bugBugRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBugHistoryRepository _bugHistoryRepository;

        public CloseBugUseCase(IBugRepository bugRepository, IBugHistoryRepository bugHistoryRepository, IUnitOfWork unitOfWork)
        {
            _bugBugRepository = bugRepository;
            _bugHistoryRepository = bugHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void Handle(CloseBugCommand command)
        {
            var bug = _bugBugRepository.GetById(command.Id);
            var history = bug.Close(command.Reason);
            _bugHistoryRepository.Store(history);
            _unitOfWork.Save();
        }
    }
}