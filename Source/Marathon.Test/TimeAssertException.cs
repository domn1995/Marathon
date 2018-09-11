using System;

namespace Marathon.Test
{
    /// <summary>
    /// Represents an error during a time assertion.
    /// </summary>
    public class TimeAssertException : Exception
    {
        public TimeAssertException(string message = "") : base(message)
        {
            
        }
    }
}