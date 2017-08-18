using System;
using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class TypedTask : ITypedTask
    {
        public TaskType RunType { get; }
        public Task Task { get; }

        public TypedTask(TaskType runType, Action action)
        {
            RunType = runType;
            Task = new Task(action);
        }
    }
}