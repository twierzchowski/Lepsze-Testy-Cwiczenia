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

        public bool IsActive()
        {
            return Status != Status.Closed;
        }

        public void Close(string commandReason)
        {
            if (!IsActive())
            {
                throw new DomainException("cannot close not active bug");
            }

            if (string.IsNullOrWhiteSpace(commandReason))
            {
                throw new DomainException("cannot close bug without reason");
            }

            Status = Status.Closed;
        }

        public void SetSeverity(Severity severity)
        {
            if (!IsActive())
            {
                throw  new DomainException("cannot edit closed bug");
            }

            Severity = severity;
        }

        public void Triage(Severity severity, Priority priority)
        {
            if (!IsActive())
            {
                throw new DomainException("cannot edit closed bug");
            }

            Severity = severity;
            Priority = priority;
            Status = Status.Todo;
        }

        public void Resolve()
        {
            if (Status != Status.Todo)
            {
                throw new DomainException($"Cannot resolved bug with status {Status}");
            }

            Status = Status.Resolved;
        }

        public void Renew()
        {
            if (Status != Status.Resolved)
            {
                throw new DomainException($"Cannot renew bug with status {Status}");
            }

            Status = Status.New;
        }
    }
}
