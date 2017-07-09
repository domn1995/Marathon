namespace Marathon.Library
{
    public enum TaskCollectionCombinationMode
    {
        /// <summary>
        /// The collection of Tasks passed to a single runner will be combined into a parallel task.
        /// </summary>
        Parallel,
        /// <summary>
        /// The collection of Tasks passed to a single runner will be combined into a sequential task.
        /// </summary>
        Sequential,
    }
}