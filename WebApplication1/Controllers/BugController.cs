using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Application;
using DataAccess.DTOs;
using DataAccess.ReadModel;
using Domain;
using Infrastructure;

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
