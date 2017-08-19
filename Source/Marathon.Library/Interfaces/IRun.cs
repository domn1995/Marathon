using System;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to start task scheduling.
    /// </summary>
    public interface IRun
    {
        BaseRunner Run(Action action);
    }
}