using System;
using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class ThenRunner : BaseRunner
    {
        public ThenRunner(Task first, Action then)
        {
            Task thenTask = first.ContinueWith(t => new Task(then));
            CombinedTasks = thenTask;
        }
    }
}