using System;
using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class AndRunner : BaseRunner
    {
        public AndRunner(Task combine, Action with)
        {
            Task andTask = Task.WhenAll(combine, new Task(with));
            CombinedTasks = andTask;
        }
    }
}
