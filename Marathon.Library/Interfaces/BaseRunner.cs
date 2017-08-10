using System;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public abstract class BaseRunner : IAsync, ISync, IThen, IAnd
    {
        protected Task CombinedTasks { get; set; }

        public Task Async()
        {
            CombinedTasks.Start();
            return CombinedTasks;
        }

        public void Sync()
        {
            CombinedTasks.Start();
            CombinedTasks.GetAwaiter().GetResult();
        }

        public BaseRunner Then(Action action) => new ThenRunner(CombinedTasks, action);

        public BaseRunner And(Action action) => new AndRunner(CombinedTasks, action);
    }
}