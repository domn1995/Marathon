using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public interface IScheduler
    {
        Task ScheduleAsync();
    }
}