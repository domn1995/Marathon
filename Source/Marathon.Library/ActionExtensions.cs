using System;
using System.Collections.Generic;
using Marathon.Library.Interfaces;

namespace Marathon.Library
{
    /// <summary>
    /// Provides extension methods for interfacing <see cref="Action"/>s with the Marathon framework.
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// Runs the given <see cref="Action"/>s.
        /// </summary>
        /// <param name="actions">The <see cref="Action"/>s to be run in parallel.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>(s).</returns>
        /// <remarks>By default, the this method will run the given <see cref="Action"/> collection in parallel.</remarks>
        public static BaseRunner Run(this IEnumerable<Action> actions) => new Runner().Run(actions);

        /// <summary>
        /// Runs the given <see cref="Action"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>.</returns>
        public static BaseRunner Run(this Action action) => new Runner().Run(action);
    }
}