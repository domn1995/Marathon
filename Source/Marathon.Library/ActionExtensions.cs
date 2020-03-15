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
        public static BaseRunner Run(this Action action) 
            => new Runner().Run(action);

        /// <summary>
        /// Runs the given <see cref="Action"/> with one parameter.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>.</returns>
        public static BaseRunner Run<T>(this Action<T> action, T arg) 
            => new Runner().Run(() => action(arg));

        /// <summary>
        /// Runs the given <see cref="Action"/> with two parameters.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>.</returns>
        public static BaseRunner Run<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2) 
            => new Runner().Run(() => action(arg1, arg2));

        /// <summary>
        /// Runs the given <see cref="Action"/> with three parameters.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>.</returns>
        public static BaseRunner Run<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
            => new Runner().Run(() => action(arg1, arg2, arg3));

        /// <summary>
        /// Runs the given <see cref="Action"/> with four parameters.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>.</returns>
        public static BaseRunner Run<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            => new Runner().Run(() => action(arg1, arg2, arg3, arg4));

        /// <summary>
        /// Runs the given <see cref="Action"/> with five parameters.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>.</returns>
        public static BaseRunner Run<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            => new Runner().Run(() => action(arg1, arg2, arg3, arg4, arg5));

        /// <summary>
        /// Runs the given <see cref="Action"/> with six parameters.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to be run.</param>
        /// <returns>A <see cref="BaseRunner"/> scheduled to run the given <see cref="Action"/>.</returns>
        public static BaseRunner Run<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            => new Runner().Run(() => action(arg1, arg2, arg3, arg4, arg5, arg6));
    }
}