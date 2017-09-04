using System;
using System.Collections.Generic;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to schedule tasks for parallel execution.
    /// </summary>
    public interface IAnd
    {
        BaseRunner And(params Action[] actions);
        BaseRunner And(IEnumerable<Action> actions);
    }
}