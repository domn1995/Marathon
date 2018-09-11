using System;

namespace Marathon.Test
{
    public class TimeAssertException : Exception
    {
        public TimeAssertException(string message = "") : base(message)
        {
            
        }
    }
}