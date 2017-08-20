using System;

namespace Application
{
    public class CloseBugCommand : ICommand
    {
        public Guid Id { get; set; }
        
        public string Reason { get; set; }
        
    }
}