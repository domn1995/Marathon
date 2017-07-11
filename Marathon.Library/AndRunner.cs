using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class AndRunner : BaseRunner
    {
        public AndRunner(Task combine, Task with)
        {
            Task andTask = Task.WhenAll(combine, with);
            CombinedTasks = andTask;
        }
    }
}
