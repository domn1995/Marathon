using System;

namespace Marathon.Test
{
    public static class TimeAssert
    {
        public static void DeltaEquals(TimeSpan expected, TimeSpan actual, TimeSpan epsilon)
        {
            double expectedMs = expected.TotalMilliseconds;
            double actualMs = actual.TotalMilliseconds;
            double epsilonMs = epsilon.TotalMilliseconds / 2;
            if (actualMs < expectedMs - epsilonMs || actualMs > expectedMs + epsilonMs)
            {
                throw new TimeAssertException($"Actual time of {actualMs}ms is outside of the expected time of {expectedMs}ms±{epsilonMs}ms.");
            }
            Console.WriteLine($"Actual time {actualMs}ms. Expected time: {expectedMs}ms. Epsilon ±{epsilonMs}ms.");
        }

        public static void DeltaEquals(TimeSpan expected, TimeSpan actual, double epsilonPercent = 0.0)
        {
            double epsilonMs = expected.TotalMilliseconds * epsilonPercent;
            DeltaEquals(expected, actual, TimeSpan.FromMilliseconds(epsilonMs));
        }
    }
}