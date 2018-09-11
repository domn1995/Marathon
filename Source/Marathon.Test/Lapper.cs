using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Marathon.Test
{
    public class Lapper : Stopwatch, IDisposable
    {
        private readonly object locker = new object();
        private readonly Stopwatch lapper = new Stopwatch();
        public List<TimeSpan> Laps { get; private set; } = new List<TimeSpan>();

        public Lapper() { }

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

        public TimeSpan Mean() => TimeSpan.FromMilliseconds(Laps.Average(t => t.TotalMilliseconds));

        public TimeSpan Max() => TimeSpan.FromMilliseconds(Laps.Max(t => t.TotalMilliseconds));

        public TimeSpan Min() => TimeSpan.FromMilliseconds(Laps.Min(t => t.TotalMilliseconds));

        public new void Restart()
        {
            lock (locker)
            {
                Laps = new List<TimeSpan>();
                base.Restart();
                lapper.Restart();
            }
        }

        public new static Lapper StartNew()
        {
            Lapper lapper = new Lapper();
            lapper.Start();
            return lapper;
            
        }

        public new void Start()
        {
            base.Start();
            lapper.Start();
        }

        public new void Stop()
        {
            base.Stop();
            lapper.Stop();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}