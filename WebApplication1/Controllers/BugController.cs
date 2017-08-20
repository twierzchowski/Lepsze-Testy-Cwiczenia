using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;
using Application;
using DataAccess.DTOs;
using Domain;

namespace WebApplication1.Controllers
{
    public class BugController : ApiController
    {
        private readonly ICommandHandler<CreateBugCommand> _createBugCommandHandler;
        private readonly ICommandHandler<CloseBugCommand> _closeBugCommandHandler;
        private readonly ICommandHandler<AutoTriageBugCommand> _autoTriageCommandHandler;
        private readonly IBugRepository _bugRepository;

        public BugController(ICommandHandler<CreateBugCommand> createBugCommandHandler,
            ICommandHandler<CloseBugCommand> closeBugCommandHandler,
            ICommandHandler<AutoTriageBugCommand> autoTriageCommandHandler,
            IBugRepository bugRepository)
        {
            _createBugCommandHandler = createBugCommandHandler;
            _closeBugCommandHandler = closeBugCommandHandler;
            _autoTriageCommandHandler = autoTriageCommandHandler;
            _bugRepository = bugRepository;
        }

        [HttpGet]
        [Route("bugs")]
        public List<BugDTO> Search()
        {
            return _bugRepository.GetActiveBugs().Select(bug => new BugDTO{Id = bug.Id, Title = bug.Title}).ToList();
        }

        [HttpPost]
        [Route("bugs")]
        public void AddBug(CreateBugCommand command)
        {
            command.Id = Guid.NewGuid();
            _createBugCommandHandler.Handle(command);
        }

        [HttpPut]
        [Route("bugs/{bugId}/close")]
        public void CloseBug(Guid bugId, CloseBugCommand command)
        {
            command.Id = bugId;

            _closeBugCommandHandler.Handle(command);
        }

        [HttpPost]
        [Route("Bugs/{bugId}/triage")]
        public void Triage(Guid bugId, TriageBugCommand command)
        {
            
        }

        [HttpPost]
        [Route("bugs/{bugId}/autotriage")]
        public void AutoTraige(Guid bugId, AutoTriageBugCommand command)
        {
            _autoTriageCommandHandler.Handle(command);
        }


    }
}
