using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Marathon.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marathon.Test
{
    [TestClass]
    public class SchedulerTests
    {
        private static readonly MyStopwatch lapper = new MyStopwatch();

        private static readonly Action s = delegate
        {
            Task.Delay(500).Wait();
            lapper.Lap();
        };

        private static readonly Action l = delegate
        {
            Task.Delay(2500).Wait();
            lapper.Lap();
        };

        private static Action lap = delegate
        {
            lapper.Lap();
        };

        [TestMethod]
        public void TestShortThensSync_AverageApprox500Ms()
        {
            Runner runner = new Runner();
            lapper.Restart();
            runner.Run(s).Then(s, s, s).Sync();
            Assert.IsTrue(lapper.Laps.Count == 4);
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lapper.LapMean(), 0.025);
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lapper.LapMin(), 0.025);
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lapper.LapMax(), 0.025);
        }

        [TestMethod]
        public void TestLongThensSync_AverageApprox2500Ms()
        {
            Runner runner = new Runner();
            lapper.Restart();
            runner.Run(l).Then(l).Sync();
            Assert.IsTrue(lapper.Laps.Count == 2);
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2500), lapper.LapMean(), 0.025);
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2500), lapper.LapMin(), 0.025);
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2500), lapper.LapMax(), 0.025);
        }
    }
}
