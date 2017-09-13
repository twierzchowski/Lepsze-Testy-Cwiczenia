using System;
using Domain;

namespace Application.Commands
{
    public class TriageBugCommand : ICommand
    {
        public Guid Id { get; set; }
        public Priority Priority { get; set; }
        public Severity Severity { get; set; }
    }
}