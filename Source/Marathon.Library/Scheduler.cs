using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    /// <summary>
    /// Provides default scheduling functionality based on a list of typed tasks.
    /// </summary>
    public class Scheduler : IScheduler
    {
        /// <summary>
        /// Schedules the given tasks for execution.
        /// </summary>
        /// <returns></returns>
        public async Task ScheduleAsync(IEnumerable<ITypedTask> tasks)
        {
            List<ITypedTask> typedTasks = tasks.ToList();
            Task combinedTask = typedTasks[0].Task;
            combinedTask.Start();

            foreach (ITypedTask typedTask in typedTasks.Skip(1))
            {
                Task nextTask;
                switch (typedTask.RunType)
                {
                    case RunType.And:
                        nextTask = typedTask.Task;
                        nextTask.Start();
                        combinedTask = Task.WhenAll(combinedTask, nextTask);
                        break;
                    case RunType.Then:
                        nextTask = typedTask.Task;
                        await combinedTask.ConfigureAwait(false);
                        nextTask.Start();
                        combinedTask = nextTask;
                        break;
                    default:
                        throw new ArgumentException(nameof(typedTask));
                }
            }
            await combinedTask.ConfigureAwait(false);
        }
    }
}
