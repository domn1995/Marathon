using System;
using System.Threading.Tasks;

namespace Marathon.Library
{
    public class ActionWrapper
    {
        private readonly Task task;

        public ActionWrapper(Action action)
        {
            task = new Task(action);
        }

        public async Task StartAsync()
        {
            task.Start();
            await task;
        }
    }
}