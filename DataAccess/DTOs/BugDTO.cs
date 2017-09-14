using System;

namespace DataAccess.DTOs
{
    public class BugDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Severity { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public Guid? AssignedUser { get; set; }
    }
}
