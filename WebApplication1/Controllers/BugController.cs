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
        private readonly ICommandHandler<TriageBugCommand> _triageCommandHandler;
        private readonly ICommandHandler<AutoTriageBugCommand> _autoTriageCommandHandler;
        private readonly ICommandHandler<ResolveBugCommand> _resoleBugCommandHandler;
        private readonly ICommandHandler<AssignUserToBugCommand> _assignUserCommandHandler;
        private readonly TestWorkshopEntities _readModel;

        public BugController(ICommandHandler<CreateBugCommand> createBugCommandHandler,
            ICommandHandler<CloseBugCommand> closeBugCommandHandler,
            ICommandHandler<TriageBugCommand> triageCommandHandler,
            ICommandHandler<AutoTriageBugCommand> autoTriageCommandHandler,
            ICommandHandler<ResolveBugCommand> resoleBugCommandHandler,
            ICommandHandler<AssignUserToBugCommand> assignUserCommandHandler,
            TestWorkshopEntities readModel)
        {
            _createBugCommandHandler = createBugCommandHandler;
            _closeBugCommandHandler = closeBugCommandHandler;
            _triageCommandHandler = triageCommandHandler;
            _autoTriageCommandHandler = autoTriageCommandHandler;
            _resoleBugCommandHandler = resoleBugCommandHandler;
            _assignUserCommandHandler = assignUserCommandHandler;
            _readModel = readModel;
        }

        [HttpGet]
        [Route("bugs")]
        public IQueryable<BugDto> Search([FromUri]BugSearchCriteria bugSearchCriteria)
        {
            return _readModel.Bugs
                .WhereIf(bugSearchCriteria?.Id != null, b => b.Id == bugSearchCriteria.Id.Value)
                .WhereIf(bugSearchCriteria?.Title != null, b => b.Title.Contains(bugSearchCriteria.Title))
                .WhereIf(bugSearchCriteria?.Severity!= null, b => b.Severity_Value == bugSearchCriteria.Severity.Value)
                .WhereIf(bugSearchCriteria?.Priority != null, b => b.Severity_Value == bugSearchCriteria.Priority.Value)
                .WhereIf(bugSearchCriteria?.Status != null, b => b.Status_Value == bugSearchCriteria.Status)
                .WhereIf(bugSearchCriteria?.User != null, b => b.Users.Id == bugSearchCriteria.User)
                .Select(bug => new BugDto
                {
                        Id = bug.Id,
                        Title = bug.Title,
                        Description = bug.Description,
                        Priority = bug.Priority_Value,
                        Severity = bug.Severity_Value,
                        Status = bug.Status_Value,
                        AssignedUser = bug.AssignedUser_Id
                    });
                }

        [HttpPost]
        [Route("bugs")]
        public void AddBug(string title, string description)
        {
            var command = new CreateBugCommand {Id = Guid.NewGuid(), Title = title, Description = description};
            _createBugCommandHandler.Handle(command);
        }

        [HttpPost]
        [Route("bugs/{bugId}/triage")]
        public void Triage(Guid bugId, int priority, int severity)
        {
            var command = new TriageBugCommand
            {
                Id = bugId,
                Priority = new Priority(priority),
                Severity = new Severity(severity)
            };
            _triageCommandHandler.Handle(command);
        }

        [HttpPost]
        [Route("bugs/{bugId}/autotriage")]
        public void AutoTraige(Guid bugId)
        {
            var command = new AutoTriageBugCommand{Id = bugId};
            _autoTriageCommandHandler.Handle(command);
        }

        [HttpPost]
        [Route("bugs/{bugId}/resolve")]
        public void Resolve(Guid bugId)
        {
            var command = new ResolveBugCommand{Id = bugId};
            _resoleBugCommandHandler.Handle(command);
        }

        [HttpPut]
        [Route("bugs/{bugId}/close")]
        public void CloseBug(Guid bugId, string reason)
        {
            var command = new CloseBugCommand{Id = bugId, Reason = reason};
            _closeBugCommandHandler.Handle(command);
        }

        [HttpPost]
        [Route("bugs/{bugId}/AssignUser")]
        public void AssignUserToBug(Guid bugId, Guid userId)
        {
            var command = new AssignUserToBugCommand{BugId = bugId, UserId = userId};
            _assignUserCommandHandler.Handle(command);
        }
    }
}
