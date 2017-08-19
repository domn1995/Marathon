using System;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to schedule tasks for parallel execution.
    /// </summary>
    public interface IAnd
    {
        BaseRunner And(Action action);
    }
}