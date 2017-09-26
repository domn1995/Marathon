using System;
using System.Linq;
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

        [TestMethod]
        public async Task TestShortThensAsync_AverageApprox500Ms()
        {
            Runner runner = new Runner();
            lapper.Restart();
            // Start the runner asynchronously.
            Task t = runner.Run(s).Then(s, s, s).Async();
            // Take a lap right away. This should be only a few milliseconds in
            // if the runner tasks really started asynchronously.
            lapper.Lap();
            // Wait for all of the runner tasks to finish.
            await t;
            lapper.Stop();
            // There should be 5 laps; one that we took right away and one each per runner task.
            Assert.IsTrue(lapper.Laps.Count == 5);
            // The first task should have been lapped right away, a few milliseconds after starting.
            // Let's give a 500% epsilon since the expected time is so short.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(10), lapper.Laps[0], 5);
            // The rest should each take about 500ms.
            foreach (TimeSpan lap in lapper.Laps.Skip(1))
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
        }

        [TestMethod]
        public async Task TestLongThensAsync_AverageApprox2500Ms()
        {
            Runner runner = new Runner();
            lapper.Restart();
            // Start the runner asynchronously.
            Task t = runner.Run(l).Then(l, l, l).Async();
            // Take a lap right away. This should be only a few milliseconds in
            // if the runner tasks really started asynchronously.
            lapper.Lap();
            // Wait for all of the runner tasks to finish.
            await t;
            lapper.Stop();
            // There should be 5 laps; one that we took right away and one each per runner task.
            Assert.IsTrue(lapper.Laps.Count == 5);
            // The first task should have been lapped right away, a few milliseconds after starting.
            // Let's give a 500% epsilon since the expected time is so short.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(10), lapper.Laps[0], 5);
            // The rest should each take about 500ms.
            foreach (TimeSpan lap in lapper.Laps.Skip(1))
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2500), lap, 0.025);
            }
        }
    }
}
