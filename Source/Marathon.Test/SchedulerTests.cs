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
        private static readonly Lapper lapper = new Lapper();

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

        [TestMethod]
        public void TestShortThensSync_AverageApprox500Ms()
        {
            Runner runner = new Runner();
            lapper.Restart();
            runner.Run(s).Then(s, s, s).Sync();
            lapper.Stop();
            Assert.IsTrue(lapper.Laps.Count == 4);
            foreach (TimeSpan lap in lapper.Laps)
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
        }

        [TestMethod]
        public void TestLongThensSync_AverageApprox2500Ms()
        {
            Runner runner = new Runner();
            lapper.Restart();
            runner.Run(l).Then(l).Sync();
            lapper.Stop();
            Assert.IsTrue(lapper.Laps.Count == 2);
            foreach (TimeSpan lap in lapper.Laps)
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2500), lap, 0.025);
            }
        }
    }
}
