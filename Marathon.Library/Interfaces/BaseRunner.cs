using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public abstract class BaseRunner : IAsync, ISync, IThen, IAnd
    {
        protected readonly List<ITypedTask> Tasks = new List<ITypedTask>();
        private readonly IScheduler scheduler;

        protected BaseRunner(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        protected BaseRunner()
        {
            scheduler = new Scheduler(Tasks);
        }

        public Task Async() => scheduler.ScheduleAsync();

        public void Sync() => scheduler.ScheduleAsync().GetAwaiter().GetResult();

        public BaseRunner Then(Action action)
        {
            TypedTask then = new TypedTask(TaskType.Then, action);
            Tasks.Add(then);
            return this;
        }

        public BaseRunner And(Action action)
        {
            TypedTask and = new TypedTask(TaskType.And, action);
            Tasks.Add(and);
            return this;
        }
    }
}