using System.Threading.Tasks;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    public class ThenRunner : BaseRunner
    {
        public ThenRunner(Task first, Task then)
        {
            Task thenTask = first.ContinueWith(t => then);
            CombinedTasks = thenTask;
        }
    }
}