using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public abstract class BaseRunner : IAsync, ISync, IThen, IAnd
    {
        protected Task CombinedTasks { get; set; }

        public Task Async() => CombinedTasks;

        public void Sync() => CombinedTasks.GetAwaiter().GetResult();

        public BaseRunner Then(params Action[] actions) => Then((IEnumerable<Action>)actions);

        public BaseRunner Then(IEnumerable<Action> actions)
        {
            Task thenTasks = actions.ParallelCombine();
            ThenRunner runner = new ThenRunner(CombinedTasks, thenTasks);
            return runner;
        }

        public BaseRunner And(params Action[] actions) => And((IEnumerable<Action>)actions);

        public BaseRunner And(IEnumerable<Action> actions)
        {
            Task andTasks = actions.ParallelCombine();
            AndRunner runner = new AndRunner(CombinedTasks, andTasks);
            return runner;
        }
    }
}