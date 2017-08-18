using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public interface ITypedTask
    {
        TaskType RunType { get; }
        Task Task { get; }
    }
}