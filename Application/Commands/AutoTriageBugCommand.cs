using System;

namespace Application
{
    public class AutoTriageBugCommand : ICommand
    {
        public Guid BugId { get; set; }
    }
}