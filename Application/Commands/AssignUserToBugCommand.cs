using System;

namespace Application.Commands
{
    public class AssignUserToBugCommand : ICommand
    {
        public Guid UserId;
        public Guid BugId;
    }
}