using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Marathon.Test
{
    /// <summary>
    /// Augments a stopwatch by providing lapping functionality.
    /// </summary>
    public class Lapper : Stopwatch, IDisposable
    {
        private readonly object locker = new object();
        private readonly Stopwatch lapper = new Stopwatch();

        /// <summary>
        /// Gets the current laps.
        /// </summary>
        public List<TimeSpan> Laps { get; private set; } = new List<TimeSpan>();

        private Lapper() { }

        /// <summary>
        /// Takes a lap.
        /// </summary>
        /// <returns>The lap time.</returns>
        public TimeSpan Lap()
        {
            lock (locker)
            {
                TimeSpan elapsed = lapper.Elapsed;
                Laps.Add(elapsed);
                lapper.Restart();
                return elapsed;
            }
        }

        /// <summary>
        /// Gets the average lap time of all the laps.
        /// </summary>
        /// <returns>The average lap time.</returns>
        public TimeSpan Mean() => TimeSpan.FromMilliseconds(Laps.Average(t => t.TotalMilliseconds));

        /// <summary>
        /// Gest the maximum lap time between all the laps.
        /// </summary>
        /// <returns>The maximum lap time.</returns>
        public TimeSpan Max() => TimeSpan.FromMilliseconds(Laps.Max(t => t.TotalMilliseconds));

        /// <summary>
        /// Gets the minimum lap time between all the laps.
        /// </summary>
        /// <returns>The mimimum lap time.</returns>
        public TimeSpan Min() => TimeSpan.FromMilliseconds(Laps.Min(t => t.TotalMilliseconds));

        /// <summary>
        /// Erases all of the laps and restarts the stopwatch.
        /// </summary>
        public new void Restart()
        {
            lock (locker)
            {
                Laps = new List<TimeSpan>();
                base.Restart();
                lapper.Restart();
            }
        }

        /// <summary>
        /// Starts a new lapping stopwatch.
        /// </summary>
        /// <returns>The new lapping stopwatch.</returns>
        public new static Lapper StartNew()
        {
            Lapper lapper = new Lapper();
            lapper.Start();
            return lapper;
            
        }

        /// <summary>
        /// Starts the lapping stopwatch.
        /// </summary>
        public new void Start()
        {
            base.Start();
            lapper.Start();
        }

        /// <summary>
        /// Stops the lapping stopwatch.
        /// </summary>
        public new void Stop()
        {
            base.Stop();
            lapper.Stop();
        }

        /// <summary>
        /// Stops and disposes of the lapping stopwatch.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}