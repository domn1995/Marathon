using System;
using System.Collections.Generic;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    /// <summary>
    /// Provides runner operations that support scheduling typed tasks for execution.
    /// </summary>
    public class Runner : BaseRunner, IRun
    {
        /// <summary>
        /// Adds the given actions as tasks to be scheduling and run.
        /// </summary>
        /// <param name="actions"></param>
        /// <returns></returns>
        public BaseRunner Run(params Action[] actions) => Run((IEnumerable<Action>)actions);

        /// <summary>
        /// Adds the given actions as tasks to be scheduling and run.
        /// </summary>
        /// <param name="actions"></param>
        /// <returns></returns>
        public BaseRunner Run(IEnumerable<Action> actions)
        {
            foreach (Action action in actions)
            {
                TypedTask task = new TypedTask(action, RunType.And);
                Tasks.Add(task);
            }
            return this;
        }
    }
}