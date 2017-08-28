using System;
using Infrastructure;

namespace Domain
{
    public class BugHistory
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime ChangeTime { get; set; }
        public string Changes { get; set; }

        public BugHistory(Bug bug)
        {
            ChangeTime = TimeProvider.Current.Now;
            Changes = $"Status: {bug.Status}, Description: {bug.Description}";
        }
    }
}