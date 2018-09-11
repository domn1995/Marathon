using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Marathon.Test.SchedulerTests
{
    /// <summary>
    /// Tests that the scheduler correctly schedules And tasks.
    /// </summary>
    public class AndTests : TestsBase
    {
        [Fact]
        public void Test4AndsSync500MsEach500MsTotal()
        {
            Initialize();
            // Execute the runner synchronously.
            Runner.Run(S).And(S, S, S).Sync();
            // Take a lap once the runner is finished.
            Lapper.Stop();
            // We should have one lap per task run.
            Assert.True(Laps.Count == 4);
            // Each lap should be about 500ms...
            foreach (TimeSpan lap in Laps)
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
            // ... And the total time should also be about 500ms since we ran all four tasks in parallel.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), Lapper.Elapsed, 0.1);
        }

        [Fact]
        public async Task Test4AndsAsync500MsEach500MsTotal()
        {
            Initialize();
            // Start the runner asynchronously.
            Task t = Runner.Run(S).And(S, S, S).Async();
            // Take a lap right away. This should be only a few milliseconds in
            // if the runner tasks really started asynchronously.
            Laps.Add(Lapper.Lap());
            // Wait for the runner to finish.
            await t.ConfigureAwait(false);
            Lapper.Stop();
            // The async lap (first lap) should have been only a few ms after its stopwatch started.
            TimeAssert.EpsilonEquals(TimeSpan.FromMilliseconds(25), Laps[0], TimeSpan.FromMilliseconds(50));
            // We should have one lap per task run.
            Assert.True(Laps.Count == 5);
            // Each lap should be about 500ms...
            // Skip the first lap because it was the one we took right away.
            foreach (TimeSpan lap in Laps.Skip(1))
            {
                TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), lap, 0.1);
            }
            // And the elapsed time should be 500ms since the tasks were all run in parallel.
            TimeAssert.DeltaEquals(TimeSpan.FromMilliseconds(500), Lapper.Elapsed, 0.1);
        }
    }
}