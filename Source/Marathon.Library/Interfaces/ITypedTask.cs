using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to model a typed task
    /// that can be scheduled for execution.
    /// </summary>
    public interface ITypedTask
    {
        TaskType RunType { get; }
        Task Task { get; }
    }
}