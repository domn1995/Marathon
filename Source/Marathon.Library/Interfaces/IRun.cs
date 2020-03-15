using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to start task scheduling.
    /// </summary>
    public interface IRun
    {
        public BaseRunner Run(Action action) => Run(new[] { action });
        public BaseRunner Run<T>(Action<T> action, T arg);
        public BaseRunner Run(params Action[] actions) => Run(actions.AsEnumerable());
        public BaseRunner Run(IEnumerable<Action> actions);
        public BaseRunner Run(int delay);
    }
}