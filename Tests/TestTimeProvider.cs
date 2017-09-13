using System;
using Infrastructure;

namespace Tests
{
    public class TestTimeProvider : TimeProvider
    {
        private DateTime _testDateTime;
        public TestTimeProvider(DateTime dateTime)
        {
            _testDateTime = dateTime;
        }

        public override DateTime Now => _testDateTime;
    }
}