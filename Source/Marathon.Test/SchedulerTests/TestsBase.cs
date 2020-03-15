using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.Test.SchedulerTests
{
    /// <summary>
    /// Provides the base functionality of the scheduler tests.
    /// </summary>
    public abstract class TestsBase
    {
        private readonly object locker = new object();
        protected List<TimeSpan> Laps { get; } = new List<TimeSpan>();
        protected Lapper Lapper { get; private set; }
        protected Runner Runner { get; private set; }

        /// <summary>
        /// Initializes a new test.
        /// </summary>
        protected TestsBase()
        {
            Lapper = Lapper.StartNew();
            Runner = new Runner();
        }

        /// <summary>
        /// Synchronously waits 500ms then laps the timer. S stands for 'short'.
        /// </summary>
        protected void S()
        {
            using Lapper l = Lapper.StartNew();
            Task.Delay(500).GetAwaiter().GetResult();
            lock (locker)
            {
                Laps.Add(l.Lap());
            }
        }

        /// <summary>
        /// Synchronously waits 1000ms then laps the timer. L stands for 'long'.
        /// </summary>
        protected void L()
        {
            using Lapper l = Lapper.StartNew();
            Task.Delay(1000).GetAwaiter().GetResult();
            lock (locker)
            {
                Laps.Add(l.Lap());
            }
        }
    }
}