using System;

namespace DataAccess.DTOs
{
    public class BugSearchCriteria
    {
        public Guid? Id { get; set; }
        public int? Severity { get; set; }
    }
}