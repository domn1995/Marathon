using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class Runner : BaseRunner, IRun
    {
        public Runner()
        {
            CombinationMode = TaskCollectionCombinationMode.Sequential;
        }

        public Runner(TaskCollectionCombinationMode mode) : base(mode)
        {
            
        }

        public BaseRunner Run(params Action[] tasks) => Run((IEnumerable<Action>)tasks);

        public BaseRunner Run(IEnumerable<Action> tasks)
        {
            List<Action> taskList = tasks.ToList();
            switch (CombinationMode)
            {
                case TaskCollectionCombinationMode.Sequential:
                    CombinedTasks = new Task(taskList[0]);
                    foreach (Action action in taskList.Skip(1))
                    {
                        Task newTask = new Task(action);
                        CombinedTasks.ContinueWith(t => newTask.Start()).ConfigureAwait(false);
                    }
                    break;
                case TaskCollectionCombinationMode.Parallel:
                    CombinedTasks = Task.WhenAll(taskList.Select(a => new Task(a)));
                    break;
                default:
                    throw new InvalidOperationException("Invalid task collection combination mode. Must either be sequential or parallel.");
            }
            return this;
        }
    }
}