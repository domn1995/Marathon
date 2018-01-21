using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marathon.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marathon.Test.SchedulerTests
{
    [TestClass]
    public abstract class TestsBase
    {
        protected List<TimeSpan> Laps;
        protected Lapper Lapper;
        protected Runner Runner;

        [TestInitialize]
        public virtual void TestInitialize()
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
                Laps.Add(l.Lap());
            }
        }

        protected void L()
        {
            using (Lapper l = Lapper.StartNew())
            {
                Task.Delay(1000).GetAwaiter().GetResult();
                Laps.Add(l.Lap());
            }
        }
    }
}