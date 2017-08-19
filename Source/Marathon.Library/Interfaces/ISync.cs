namespace Marathon.Library.Interfaces
{
    /// <summary>
    /// Enables implementing classes to schedule tasks for synchronous/blocking execution.
    /// </summary>
    public interface ISync
    {
        void Sync();
    }
}