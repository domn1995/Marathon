using System;
using System.Collections.Generic;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class Runner : BaseRunner, IRun
    {
        public BaseRunner Run(params Action[] actions) => Run((IEnumerable<Action>)actions);

        public BaseRunner Run(IEnumerable<Action> actions)
        {
            CombinedTasks = actions.ParallelCombine();
            return this;
        }
    }
}