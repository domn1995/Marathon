﻿using System;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    /// <summary>
    /// Provides runner operations that support scheduling typed tasks for execution.
    /// </summary>
    public class Runner : BaseRunner, IRun
    {
        public BaseRunner Run(Action action)
        {
            TypedTask run = new TypedTask(action, RunType.And);
            Tasks.Add(run);
            return this;
        }
    }
}