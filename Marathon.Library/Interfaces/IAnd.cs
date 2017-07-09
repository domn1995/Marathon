using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public interface IAnd
    {
        BaseRunner And(params Task[] tasks);
        BaseRunner And(IEnumerable<Task> tasks);
    }
}