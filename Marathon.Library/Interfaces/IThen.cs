using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public interface IThen
    {
        BaseRunner Then(params Task[] tasks);
        BaseRunner Then(IEnumerable<Task> tasks);
    }
}