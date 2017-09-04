using System;
using System.Collections;
using System.Collections.Generic;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to start task scheduling.
    /// </summary>
    public interface IRun
    {
        BaseRunner Run(params Action[] actions);
        BaseRunner Run(IEnumerable<Action> actions);
    }
}