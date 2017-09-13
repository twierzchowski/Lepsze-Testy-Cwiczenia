using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Application;
using Application.Commands;
using DataAccess.DTOs;
using DataAccess.ReadModel;
using Domain;
using Infrastructure;

namespace WebApplication.Controllers
{
    public class BugController : ApiController
    {
        private readonly ICommandHandler<CreateBugCommand> _createBugCommandHandler;
        private readonly ICommandHandler<CloseBugCommand> _closeBugCommandHandler;
        private readonly ICommandHandler<AutoTriageBugCommand> _autoTriageCommandHandler;
        private readonly ICommandHandler<ResolveBugCommand> _resoleBugCommandHandler;

        public BugController(ICommandHandler<CreateBugCommand> createBugCommandHandler,
            ICommandHandler<CloseBugCommand> closeBugCommandHandler,
            ICommandHandler<AutoTriageBugCommand> autoTriageCommandHandler,
            ICommandHandler<ResolveBugCommand> resoleBugCommandHandler,
            IBugRepository bugRepository)
        {
            _createBugCommandHandler = createBugCommandHandler;
            _closeBugCommandHandler = closeBugCommandHandler;
            _autoTriageCommandHandler = autoTriageCommandHandler;
            _resoleBugCommandHandler = resoleBugCommandHandler;
        }

        [HttpGet]
        [Route("bugs")]
        public List<BugDTO> Search([FromUri]BugSearchCriteria bugSearchCriteria)
        {
            return new TestWorkshopEntities().Bugs
                .WhereIf(bugSearchCriteria?.Id != null, b => b.Id == bugSearchCriteria.Id.Value)
                .WhereIf(bugSearchCriteria?.Severity!= null, b => b.Severity_Value == bugSearchCriteria.Severity.Value)
                .Select(bug => new BugDTO
                {
                        Id = bug.Id,
                        Title = bug.Title,
                        Description = bug.Description,
                        Priority = bug.Priority_Value,
                        Severity = bug.Severity_Value,
                        Status = bug.Status_Value
                    }).ToList();
                }

        [HttpPost]
        [Route("bugs")]
        public void AddBug(CreateBugCommand command)
        {
            command.Id = Guid.NewGuid();
            _createBugCommandHandler.Handle(command);
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
            command.Id = bugId;
            _autoTriageCommandHandler.Handle(command);
        }

        [HttpPost]
        [Route("Bugs/{bugId}/resolve")]
        public void Resolve(Guid bugId, ResolveBugCommand command)
        {
            command.Id = bugId;
            _resoleBugCommandHandler.Handle(command);
        }


        [HttpPut]
        [Route("bugs/{bugId}/close")]
        public void CloseBug(Guid bugId, CloseBugCommand command)
        {
            command.Id = bugId;
            _closeBugCommandHandler.Handle(command);
        }
    }
}
