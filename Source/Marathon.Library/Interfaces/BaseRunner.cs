using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Schedules new tasks that start executing the given <see cref="Action"/>s 
        /// in parallel after the previously scheduled task(s) have finished.
        /// </summary>
        /// <param name="actions">A collection of one or more actions to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> ready to schedule tasks for execution.</returns>
        public BaseRunner Then(params Action[] actions) => Then(actions.AsEnumerable());

        /// <summary>
        /// Schedules new tasks that start executing the given <see cref="Action"/>s 
        /// in parallel after the previously scheduled task(s) have finished.
        /// </summary>
        /// <param name="actions">A collection of actions to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> ready to schedule tasks for execution.</returns>
        public BaseRunner Then(IEnumerable<Action> actions)
        {
            foreach (Action action in actions)
            {
                TypedTask task = new TypedTask(action, RunType.Then);
                Tasks.Add(task);
            }
            return this;
        }

        public BaseRunner Then<T>(Action<T> action, T arg) => Then(() => action(arg));

        /// <summary>
        /// Schedules a delay to start executing after the previously scheduled task(s) have finished.
        /// </summary>
        /// <param name="delay">The delay in milliseconds.</param>
        /// <returns>A <see cref="BaseRunner"/> ready to schedule tasks for execution.</returns>
        public BaseRunner Then(int delay)
        {
            TypedTask task = new TypedTask(delay);
            Tasks.Add(task);
            return this;
        }

        /// <summary>
        /// Schedules the given actions to start executing 
        /// at the same time as as the previously scheduled action(s).
        /// </summary>
        /// <param name="actions"></param>
        /// <returns></returns>
        public BaseRunner And(params Action[] actions) => And(actions.AsEnumerable());

        /// <summary>
        /// Schedules the given actions to start executing 
        /// at the same time as as the previously scheduled action(s).
        /// </summary>
        /// <param name="actions"></param>
        /// <returns></returns>
        public BaseRunner And(IEnumerable<Action> actions)
        {
            foreach (Action action in actions)
            {
                TypedTask task = new TypedTask(action, RunType.And);
                Tasks.Add(task);
            }
            return this;
        }

        public BaseRunner And<T>(Action<T> action, T arg) => And(() => action(arg));

        /// <summary>
        /// Schedules and starts all internal tasks for asynchronous execution.
        /// </summary>
        /// <returns>A <see cref="Task"/> that completes when all scheduled tasks complete.</returns>
        public Task Async() => scheduler.ScheduleAsync(Tasks);

        /// <summary>
        /// Schedules and starts all internal tasks and synchronously waits for all of them to finish.
        /// </summary>
        /// <remarks>This method will block the calling thread.</remarks>
        public void Sync() => scheduler.ScheduleAsync(Tasks).GetAwaiter().GetResult();
    }
}