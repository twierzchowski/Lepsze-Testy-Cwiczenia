using System;
using System.Xml.Schema;

namespace Domain
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; }
        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public Bug()
        {
            Severity = Severity.Medium;
            Priority = Priority.Medium;
            Status = Status.New;
        }
        //public Bug(Guid id, string title, string description)
        //{
        //    Id = id;
        //    Title = title;
        //    Description = description;
        //}

        void AssignToUser()
        {
            
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
    }
}
