using System;
using System.Linq;
using System.Threading.Tasks;
using Marathon.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marathon.Test.SchedulerTests
{
    [TestClass]
    public class ThenTests
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
        public void TestThensSync_Average500MsTotal2000Ms()
        {
            Runner runner = new Runner();
            // Start the lapping stopwatch.
            lapper.Restart();
            // Run the four runner tasks synchronously.
            runner.Run(s).Then(s, s, s).Sync();
            // Stop the timer after the four tasks have finished.
            lapper.Stop();
            // We should have four laps since we ran four tasks.
            Assert.IsTrue(lapper.Laps.Count == 4);
            // Each lap should have been about 500ms...
            foreach (TimeSpan lap in lapper.Laps)
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
            // ... And the total time should have been 4 x 500ms = 2000ms.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2000), lapper.Elapsed, 0.1);
        }

        [TestMethod]
        public async Task TestThensAsync_Average500MsTotal2000Ms()
        {
            Runner runner = new Runner();
            // Starting the lappting stopwatch.
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
            // ... And the total time should have been about 4 x 500ms = 2000ms.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2000), lapper.Elapsed, 0.1);
        }
    }
}
