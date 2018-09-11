using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Marathon.Test.SchedulerTests
{
    /// <summary>
    /// Tests that the scheduler correctly schedules Then tasks.
    /// </summary>
    public class ThenTests : TestsBase
    {
        [Fact]
        public void Test4ThensSync500MsEach2000MsTotal()
        {
            Initialize();
            // Run the four runner tasks synchronously.
            Runner.Run(S).Then(S, S, S).Sync();
            // Stop the timer after the four tasks have finished.
            Lapper.Stop();
            // We should have four laps since we ran four tasks.
            Assert.True(Laps.Count == 4);
            // Each lap should have been about 500ms...
            foreach (TimeSpan lap in Laps)
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
            // ... And the total time should have been 4 x 500ms = 2000ms.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2000), Lapper.Elapsed, 0.1);
        }

        [Fact]
        public async Task Test4ThensAsync500MsEach2000MsTotal()
        {
            Initialize();
            // Start the runner asynchronously.
            Task t = Runner.Run(S).Then(S, S, S).Async();
            // Take a lap right away. This should be only a few milliseconds in
            // if the runner tasks really started asynchronously.
            Laps.Add(Lapper.Lap());
            // Wait for all of the runner tasks to finish.
            await t.ConfigureAwait(false);
            Lapper.Stop();
            // There should be 5 laps; one that we took right away and one each per runner task.
            Assert.True(Laps.Count == 5);
            // The first task should have been lapped right away, a few milliseconds after starting.
            // Let's give a 500% epsilon since the expected time is so short.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(10), Lapper.Laps[0], 5);
            // The rest should each take about 500ms.
            foreach (TimeSpan lap in Laps.Skip(1))
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
            // ... And the total time should have been about 4 x 500ms = 2000ms.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(2000), Lapper.Elapsed, 0.1);
        }

        [Fact]
        public void TestThenDelaySync100MsEach500MsTotal()
        {
            Initialize();
            Runner.Run(100).Then(100).Then(100).Then(100).Then(100).Sync();
            Lapper.Stop();
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), Lapper.Elapsed, 0.25);
        }
    }
}