using System;
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

        public void Triage(Severity severity, Priority priority)
        {
            if (Status != Status.New)
                throw new DomainException("Cannot triage not new bug");

            Severity = severity;
            Priority = priority;
            Status = Status.Todo;
            UpdateEditionDateTime();
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

            if (string.IsNullOrWhiteSpace(commandReason))
                throw new DomainException("Cannot close bug without reason");

            Status = Status.Closed;
            UpdateEditionDateTime();

            return new BugHistory(this);
        }

        public bool IsActive()
        {
            return Status != Status.Closed;
        }
    }
}
