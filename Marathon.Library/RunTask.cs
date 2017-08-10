using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marathon.Library
{
    public class RunTask : Task
    {
        public RunTask(Action action) : base(action) { }
        public RunTask(Action action, CancellationToken cancellationToken) : base(action, cancellationToken) { }
        public RunTask(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(action, cancellationToken, creationOptions) { }
        public RunTask(Action action, TaskCreationOptions creationOptions) : base(action, creationOptions) { }
        public RunTask(Action<object> action, object state) : base(action, state) { }
        public RunTask(Action<object> action, object state, CancellationToken cancellationToken) : base(action, state, cancellationToken) { }
        public RunTask(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(action, state, cancellationToken, creationOptions) { }
        public RunTask(Action<object> action, object state, TaskCreationOptions creationOptions) : base(action, state, creationOptions) { }

        public void TryStart()
        {
            switch (this.Status)
            {
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                case TaskStatus.RanToCompletion:
                case TaskStatus.Running:
                case TaskStatus.WaitingForChildrenToComplete:
                    return;
                default:
                    this.Start();
                    break;
            }
        }
    }
}