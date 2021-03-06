﻿using System;
using Infrastructure;

namespace Domain
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; private set; }
        public Priority Priority { get; private set; }
        public Status Status { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime LastEditionDateTime { get; private set; }
        public virtual User AssignedUser { get; private set; }

        public Bug()
        {
            Severity = Severity.Medium;
            Priority = Priority.Medium;
            Status = Status.New;
            CreatedDateTime = TimeProvider.Current.Now;
            UpdateEditionDateTime();
        }

        private void UpdateEditionDateTime()
        {
            LastEditionDateTime = TimeProvider.Current.Now;
        }

        public void AssignUser(User user)
        {
            if (user == null)
                throw new DomainException("Cannot assign null user");
            AssignedUser = user;
        }

        public void UnassignUser()
        {
            AssignedUser = null;
        }

        #region bug status methods
        public void Triage(Severity severity, Priority priority)
        {
            if (Status != Status.New)
                throw new DomainException("Cannot triage not new bug");

            Severity = severity;
            Priority = priority;
            Status = Status.Todo;
            UpdateEditionDateTime();
        }

        public void AutoTriage(ITriageBugService triageBugService)
        {
            if (Status != Status.New)
                throw new DomainException("Cannot triage not new bug");

            var severityFromExternalService = triageBugService.GetSeverity(Title, Description);
            var priorityFromExternalService = triageBugService.GetPriority(Title, Description);

            Severity = MapSeverityFromExternalService(severityFromExternalService);
            Priority = MapPriorityFromExternalService(priorityFromExternalService);
            Status = Status.Todo;
            UpdateEditionDateTime();
        }

        private Priority MapPriorityFromExternalService(int priorityFromExternalService)
        {
            switch (priorityFromExternalService)
            {
                case 1:
                    return Priority.High;
                case 2:
                    return Priority.Medium;
                case 3:
                    return Priority.Low;
                default:
                    throw new Exception("Invalid priority value");
            }
        }

        private Severity MapSeverityFromExternalService(int severityFromExternalService)
        {
            if (severityFromExternalService < 0)
                throw new Exception("Invalid severity value");
            if (severityFromExternalService < 100)
                return Severity.High;
            if (severityFromExternalService <= 250)
                return Severity.Medium;

            return Severity.Low;
        }

        public void TriageExpired()
        {
            if (Status != Status.Todo)
                throw  new DomainException($"Traige cannot be expired in status {Status.Value}");

            Status = Status.New;
            UpdateEditionDateTime();
        }

        public void Resolve()
        {
            if (Status != Status.Todo)
                throw new DomainException($"Cannot resolved bug with status {Status.Value}");

            if (AssignedUser == null)
                throw new DomainException("Cannot resolve bug without assigned user");

            //cannot edit bugs on weekends ;)
            var dayOfWeek = TimeProvider.Current.Now.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Saturday || 
                dayOfWeek == DayOfWeek.Sunday)
                throw new DomainException($"Cannot resovle bug on {dayOfWeek}");

            Status = Status.Done;
            UpdateEditionDateTime();
        }

        public void Renew()
        {
            if (Status != Status.Done)
                throw new DomainException($"Cannot renew bug with status {Status.Value}");

            Status = Status.New;
            UpdateEditionDateTime();
        }

        public BugHistory Close(string commandReason)
        {
            if (Status != Status.Done)
                throw new DomainException("Cannot close not resolved bug");

            if (AssignedUser == null)
                throw new DomainException("Cannot close bug without assigned user");

            Status = Status.Closed;
            UnassignUser();
            UpdateEditionDateTime();

            return new BugHistory(this);
        }
        #endregion

        public bool IsActive()
        {
            return Status != Status.Closed;
        }
    }
}
