using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marathon.Library.Interfaces
{
    public interface IRun
    {
        BaseRunner Run(params Action[] tasks);
        BaseRunner Run(IEnumerable<Action> tasks);
    }
}