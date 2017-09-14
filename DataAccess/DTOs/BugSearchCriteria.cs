using System;

namespace DataAccess.DTOs
{
    public class BugSearchCriteria
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public int? Severity { get; set; }
        public int? Priority { get; set; }
        public string Status { get; set; }
        public Guid? User { get; set; }
    }
}