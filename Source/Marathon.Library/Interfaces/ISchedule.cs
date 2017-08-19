using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to schedule and start execution of typed tasks.
    /// </summary>
    public interface IScheduler
    {
        Task ScheduleAsync(IEnumerable<ITypedTask> tasks);
    }
}