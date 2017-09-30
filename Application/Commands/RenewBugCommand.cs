using System;

namespace Application.Commands
{
    public class RenewBugCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}