using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Library
{
    public static class TaskExtensions
    {
        public static async Task WrapAndStartAsync(this Action action)
        {
            await new ActionWrapper(action).StartAsync();
        }

        public static Task ParallelCombine(this IEnumerable<Action> actions)
        {
            return Task.WhenAll(actions.Select(a => a.WrapAndStartAsync()));
        }
    }
}
