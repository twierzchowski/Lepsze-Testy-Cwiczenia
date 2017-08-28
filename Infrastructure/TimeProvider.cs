using System;

namespace Infrastructure
{
    public abstract class TimeProvider
    {
        private static TimeProvider _current = new DefaultTimeProvider();

        public static TimeProvider Current
        {
            get { return _current; }
            set { _current = value ?? new DefaultTimeProvider(); }
        }

        public abstract DateTime Now { get; }
    }

    public class DefaultTimeProvider : TimeProvider
    {
        public override DateTime Now => DateTime.Now; 
    }
}