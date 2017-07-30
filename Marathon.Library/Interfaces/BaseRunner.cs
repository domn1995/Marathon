using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public abstract class BaseRunner : IAsync, ISync, IThen, IAnd
    {
        protected Task CombinedTasks { get; set; }

        public Task Async() => CombinedTasks;

        public void Sync() => CombinedTasks.GetAwaiter().GetResult();

        public BaseRunner Then(params Action[] tasks) => Then((IEnumerable<Action>)tasks);

        public BaseRunner Then(IEnumerable<Action> tasks)
        {
            Task thenTasks = tasks.ParallelCombine();
            ThenRunner runner = new ThenRunner(CombinedTasks, thenTasks);
            return runner;
        }

        public BaseRunner And(params Action[] tasks) => And((IEnumerable<Action>)tasks);

        public BaseRunner And(IEnumerable<Action> tasks)
        {
            Task andTasks = tasks.ParallelCombine();
            AndRunner runner = new AndRunner(CombinedTasks, andTasks);
            return runner;
        }
    }
}