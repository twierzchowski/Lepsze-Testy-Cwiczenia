using System;

namespace Application.Commands
{
    public class CreateBugCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
