namespace Domain
{
    public class Priority : ValueObject<Priority>
    {
        public  int Value { get; set; }
        private Priority()
        { }
        public Priority(int priority)
        {
            if (priority < 1 || priority > 3)
                throw new DomainException($"{priority} is invalid value of priority");
            Value = priority;
        }
        public static Priority Low = new Priority(3);
        public static Priority Medium = new Priority(2);
        public static Priority High = new Priority(1);
        protected override bool EqualsCore(Priority other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}