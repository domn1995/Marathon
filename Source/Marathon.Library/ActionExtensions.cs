using System;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    /// <summary>
    /// Provides extension methods for interfacing <see cref="Action"/>s with the Marathon framework.
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// Runs the given <see cref="Action"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>(s).</returns>
        public static BaseRunner Run(this Action action) => new Runner().Run(action);
    }
}