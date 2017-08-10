using System;
using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class Runner : BaseRunner, IRun
    {
        public BaseRunner Run(Action action)
        {
            CombinedTasks = new Task(action);
            return this;
        }
    }
}