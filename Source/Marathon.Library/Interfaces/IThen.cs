using System;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to schedule tasks for sequential execution.
    /// </summary>
    public interface IThen
    {
        BaseRunner Then(Action action);
    }
}