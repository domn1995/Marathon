using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// A base class that defines runner operations.
    /// </summary>
    public abstract class BaseRunner : IAsync, ISync, IThen, IAnd
    {
        private readonly IScheduler scheduler;
        /// <summary>
        /// Gets the list of tasks to be run.
        /// </summary>
        protected List<ITypedTask> Tasks { get; } = new List<ITypedTask>();

        /// <summary>
        /// Initializes a new instance of <see cref="BaseRunner"/> subclasses with the given <see cref="IScheduler"/>.
        /// </summary>
        /// <param name="scheduler"></param>
        protected BaseRunner(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseRunner"/> subclasses with the default scheduler.
        /// </summary>
        protected BaseRunner()
        {
            scheduler = new Scheduler();
        }

        /// <summary>
        /// Schedules all internal tasks for asynchronous execution and starts them.
        /// </summary>
        /// <returns>A single <see cref="Task"/> that completes when all schedule tasks complete.</returns>
        public Task Async() => scheduler.ScheduleAsync(Tasks);

        /// <summary>
        /// Schedules all internal tasks for execution and synchronously waits for all of them to finish.
        /// </summary>
        /// <remarks>This method will block the calling thread.</remarks>
        public void Sync() => scheduler.ScheduleAsync(Tasks).GetAwaiter().GetResult();

        /// <summary>
        /// Schedules a new task that starts executing the given <see cref="Action"/> 
        /// after the previously scheduled task(s) have finished.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public BaseRunner Then(Action action)
        {
            TypedTask then = new TypedTask(TaskType.Then, action);
            Tasks.Add(then);
            return this;
        }

        /// <summary>
        /// Schedules a new task that starts executing the given <see cref="Action"/>
        /// at the same time as as the previously scheduled task(s).
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public BaseRunner And(Action action)
        {
            TypedTask and = new TypedTask(TaskType.And, action);
            Tasks.Add(and);
            return this;
        }
    }
}