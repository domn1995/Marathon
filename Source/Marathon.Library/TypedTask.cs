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
        public TaskType RunType { get; }
        /// <summary>
        /// Gets this task.
        /// </summary>
        public Task Task { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedTask"/> class with the given parameters.
        /// </summary>
        /// <param name="runType"></param>
        /// <param name="action"></param>
        public TypedTask(TaskType runType, Action action)
        {
            RunType = runType;
            Task = new Task(action);
        }
    }
}