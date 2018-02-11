using System;
using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    /// <summary>
    /// Represents a task that has a run type and can be custom scheduled.
    /// </summary>
    public class TypedTask : ITypedTask
    {
        /// <summary>
        /// Gets this task's type.
        /// </summary>
        public RunType RunType { get; }

        /// <summary>
        /// Gets this task.
        /// </summary>
        public Task Task { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedTask"/> class with the given parameters.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="runType"></param>
        public TypedTask(Action action, RunType runType)
        {
            RunType = runType;
            Task = new Task(action);
        }

        public TypedTask(int delay)
        {
            RunType = RunType.Then;
            Action action = delegate { Task.Delay(delay).Wait(); };
            Task = new Task(action);
        }
    }
}