using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.Test.SchedulerTests
{
    public abstract class TestsBase
    {
        private readonly object locker = new object();
        protected List<TimeSpan> Laps;
        protected Lapper Lapper;
        protected Runner Runner;

        public virtual void Initialize()
        {
            Laps = new List<TimeSpan>();
            Lapper = Lapper.StartNew();
            Runner = new Runner();
        }

        protected void S()
        {
            using (Lapper l = Lapper.StartNew())
            {
                Task.Delay(500).GetAwaiter().GetResult();
                lock (locker)
                {
                    Laps.Add(l.Lap());
                }
            }
        }

        protected void L()
        {
            using (Lapper l = Lapper.StartNew())
            {
                Task.Delay(1000).GetAwaiter().GetResult();
                lock (locker)
                {
                    Laps.Add(l.Lap());
                }
            }
        }
    }
}