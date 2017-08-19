using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to schedule tasks for asynchronous execution.
    /// </summary>
    public interface IAsync
    {
        Task Async();
    }
}