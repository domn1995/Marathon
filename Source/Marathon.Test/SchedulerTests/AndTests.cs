using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Marathon.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marathon.Test.SchedulerTests
{
    [TestClass]
    public class AndTests
    {
        [TestMethod]
        public void TestAndsSync_Average500MsTotal500Ms()
        {
            List<TimeSpan> laps = new List<TimeSpan>();
            Action a = delegate
            {
                using (Lapper l = Lapper.StartNew())
                {
                    Task.Delay(500).Wait();
                    laps.Add(l.Lap());
                }
            };
            Runner runner = new Runner();
            // Start the lapping stopwatch.
            Lapper lapper = Lapper.StartNew();
            // Execute the runner synchronously.
            runner.Run(a).And(a, a, a).Sync();
            // Take a lap once the runner is finished.
            lapper.Lap();
            // We should have one lap per task run.
            Assert.IsTrue(laps.Count == 4);
            // Each lap should be about 500ms...
            foreach (TimeSpan lap in laps)
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
            // ... And the total time should also be about 500ms since we ran all four tasks in parallel.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lapper.Elapsed, 0.1);
        }

        [TestMethod]
        public async Task TestAndsAsync_Average500MsTotal500Ms()
        {
            List<TimeSpan> laps = new List<TimeSpan>();
            Action a = delegate
            {
                using (Lapper l = Lapper.StartNew())
                {
                    Task.Delay(500).Wait();
                    laps.Add(l.Lap());
                }
            };
            Runner runner = new Runner();
            // Start the lapping stopwatch.
            Lapper lapper = Lapper.StartNew();
            // Start the runner asynchronously.
            Task t = runner.Run(a).And(a, a, a).Async();
            // Take a lap right away. This should be only a few milliseconds in
            // if the runner tasks really started asynchronously.
            lapper.Lap();
            await t;
            lapper.Stop();
            // Wait for the runner to finish.
            // The async lap (first lap) should have been only a few ms after its stopwatch started.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(25), lapper.Laps[0], TimeSpan.FromMilliseconds(50));
            // We should have one lap per task run.
            Assert.IsTrue(laps.Count == 4);
            // Each lap should be about 500ms...
            foreach (TimeSpan lap in laps)
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
            // And the total time should be 4 | 500ms = 500ms, since the tasks were all run in parallel.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lapper.Elapsed, 0.1);
        }
    }
}