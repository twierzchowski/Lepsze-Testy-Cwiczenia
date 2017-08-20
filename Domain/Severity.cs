using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Severity : ValueObject<Severity>
    {
        public int Value { get; set; }

        private Severity()
        {
            
        }
        public Severity(int severity)
        {
            //ensure 0<s<4
            Value = severity;
        }
        public static Severity Low = new Severity(3);
        public static Severity Medium = new Severity(2);
        public static Severity High = new Severity(1);
        protected override bool EqualsCore(Severity other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}