using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public abstract class BaseRunner : IAsync, ISync, IThen, IAnd
    {
        protected TaskCollectionCombinationMode CombinationMode { get; set; }
        protected Task CombinedTasks { get; set; }

        protected BaseRunner()
        {
            
        }

        protected BaseRunner(TaskCollectionCombinationMode mode)
        {
            CombinationMode = mode;
        }

        public Task Async() => CombinedTasks;

        public void Sync()
        {
            CombinedTasks.Start();
            CombinedTasks.GetAwaiter().GetResult();
        }

        public BaseRunner Then(params Task[] tasks) => Then((IEnumerable<Task>)tasks);

        public BaseRunner Then(IEnumerable<Task> tasks)
        {
            throw new System.NotImplementedException();
        }

        public BaseRunner And(params Task[] tasks) => And((IEnumerable<Task>)tasks);

        public BaseRunner And(IEnumerable<Task> tasks)
        {
            throw new System.NotImplementedException();
        }
    }
}