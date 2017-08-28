namespace Domain
{
    public class Status : ValueObject<Status>
    {
        public string Value {get; set; }
        
        private Status()
        { }

        public Status(string name)
        {
            Value = name;
        }

        public static Status New = new Status("New");
        public static Status Todo = new Status("Todo");
        public static Status Resolved = new Status("Resolved");
        public static Status Closed  = new Status("Closed");

        protected override bool EqualsCore(Status other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}