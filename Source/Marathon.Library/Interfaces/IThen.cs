﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to schedule tasks for sequential execution.
    /// </summary>
    public interface IThen
    {
        BaseRunner Then(params Action[] actions);
        BaseRunner Then(IEnumerable<Action> actions);
    }
}