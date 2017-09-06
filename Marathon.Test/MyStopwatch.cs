using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Marathon.Test
{
    public class MyStopwatch : Stopwatch
    {
        private readonly Stopwatch lapper = new Stopwatch();
        public List<TimeSpan> Laps { get; private set; } = new List<TimeSpan>();

        public MyStopwatch() { }

        public void Lap()
        {
            Laps.Add(lapper.Elapsed);
            lapper.Restart();
        }

        public TimeSpan LapMean() => TimeSpan.FromMilliseconds(Laps.Average(t => t.TotalMilliseconds));

        public TimeSpan LapMax() => TimeSpan.FromMilliseconds(Laps.Max(t => t.TotalMilliseconds));

        public TimeSpan LapMin() => TimeSpan.FromMilliseconds(Laps.Min(t => t.TotalMilliseconds));

        public new void Restart()
        {
            Laps = new List<TimeSpan>();
            base.Restart();
            lapper.Restart();
        }

        public new void Start()
        {
            base.Start();
            lapper.Start();
        }
    }
}