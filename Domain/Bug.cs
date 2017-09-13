using System;

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

        public Bug()
        {
            Severity = Severity.Medium;
            Priority = Priority.Medium;
            Status = Status.New;
        }

        public void Triage(Severity severity, Priority priority)
        {
            if (Status != Status.New)
                throw new DomainException("Cannot triage not new bug");

            Severity = severity;
            Priority = priority;
            Status = Status.Todo;
        }

        public void TriageExpired()
        {
            if (Status != Status.Todo)
                throw  new DomainException($"Traige cannot be expired in status {Status}");

            Status = Status.New;
        }

        public void Resolve()
        {
            if (Status != Status.Todo)
                throw new DomainException($"Cannot resolved bug with status {Status}");

            Status = Status.Done;
        }

        public void Renew()
        {
            if (Status != Status.Done)
                throw new DomainException($"Cannot renew bug with status {Status}");

            Status = Status.New;
        }

        public BugHistory Close(string commandReason)
        {
            if (Status != Status.Done)
                throw new DomainException("Cannot close not resolved bug");

            if (string.IsNullOrWhiteSpace(commandReason))
                throw new DomainException("Cannot close bug without reason");

            Status = Status.Closed;

            return new BugHistory(this);
        }

        public bool IsActive()
        {
            return Status != Status.Closed;
        }
    }
}
