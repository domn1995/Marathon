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
        /// Schedules the give tasks for execution.
        /// </summary>
        /// <returns></returns>
        public async Task ScheduleAsync(IEnumerable<ITypedTask> tasks)
        {
            List<ITypedTask> taskList = tasks.ToList();
            Task combinedTask = taskList[0].Task;
            combinedTask.Start();

            foreach (ITypedTask action in taskList.Skip(1))
            {
                Task nextTask;
                switch (action.RunType)
                {
                    case TaskType.And:
                        nextTask = action.Task;
                        nextTask.Start();
                        combinedTask = Task.WhenAll(combinedTask, nextTask);
                        break;
                    case TaskType.Then:
                        nextTask = action.Task;
                        await combinedTask.ConfigureAwait(false);
                        nextTask.Start();
                        combinedTask = nextTask;
                        break;
                    default:
                        throw new ArgumentException(nameof(action));
                }
            }
            await combinedTask.ConfigureAwait(false);
        }
    }
}