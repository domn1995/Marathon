using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marathon.Library
{
    public static class CombinationHelper
    {
        public static Task ParallelCombine(this IEnumerable<Action> actions)
        {
            return Task.WhenAll(actions.Select(a => new Task(a)));
        }

        public static List<Task> SequentialCombine(this IEnumerable<Action> actions)
        {
            List<Task> tasks = actions.Select(a => new Task(a)).ToList();

            for (int i = 0; i < tasks.Count - 1; ++i)
            {
                int index = i;
                tasks[index].ContinueWith(t => tasks[index + 1].Start());
            }

            return tasks;
        }
    }
}
