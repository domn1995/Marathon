using System;

namespace Marathon.Test
{
    /// <summary>
    /// Provides assertion methods related to time.
    /// </summary>
    public static class TimeAssert
    {
        /// <summary>
        /// Asserts that the expected and actual times are equal, with an allowed time epsilon still passing the test.
        /// <para>If epsilon is n, then the actual time may be n / 2 less than the expected
        /// or n / 2 greater than the expected for the test to pass. You can think of the epsilon as a value centered
        /// on the actual value on the number line with half of it extending left and half of it right. Examples: </para>
        /// <para>expected = 10ms, actual = 9ms, epsilon = 2ms => PASS</para>
        /// <para>expected = 100ms, actual = 90ms, epsilon = 33ms => PASS</para>
        /// <para>expected = 500ms, actual = 510ms, epsilon = 100ms => PASS</para>
        /// <para>expected = 10ms, actual = 9ms, epsilon = 1ms => FAIL</para>
        /// <para>expected = 100ms, actual = 90ms, epsilon = 10ms => FAIL</para>
        /// <para>expected = 50ms, actual = 51ms, epsilon = 0ms => FAIL</para>
        /// </summary>
        /// <param name="expected">The expected time.</param>
        /// <param name="actual">The actual time.</param>
        /// <param name="epsilon">The allowed time difference between the expected and actual values.</param>
        /// <exception cref="TimeAssertException">The expected time is more than epsilon time different from the actual time.</exception>
        public static void EpsilonEquals(TimeSpan expected, TimeSpan actual, TimeSpan epsilon)
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

        /// <summary>
        /// Asserts that the expected and actual times are equal, with an allowed time epsilon still passing the test.
        /// <para>If epsilon is n, then the actual time may be n / 2 less than the expected
        /// or n / 2 greater than the expected for the test to pass. You can think of the epsilon as a value centered
        /// on the actual value on the number line with half of it extending left and half of it right. Examples: </para>
        /// <para>expected = 10ms, actual = 9ms, epsilon = 2ms => PASS</para>
        /// <para>expected = 100ms, actual = 90ms, epsilon = 33ms => PASS</para>
        /// <para>expected = 500ms, actual = 510ms, epsilon = 100ms => PASS</para>
        /// <para>expected = 10ms, actual = 9ms, epsilon = 1ms => FAIL</para>
        /// <para>expected = 100ms, actual = 90ms, epsilon = 10ms => FAIL</para>
        /// <para>expected = 50ms, actual = 51ms, epsilon = 0ms => FAIL</para>
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="epsilonPercent"></param>
        public static void DeltaEquals(TimeSpan expected, TimeSpan actual, double epsilonPercent = 0.0)
        {
            double epsilonMs = expected.TotalMilliseconds * epsilonPercent;
            EpsilonEquals(expected, actual, TimeSpan.FromMilliseconds(epsilonMs));
        }
    }
}