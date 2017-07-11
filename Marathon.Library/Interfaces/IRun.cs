using System;
using System.Collections.Generic;

namespace Marathon.Library.Interfaces
{
    public interface IRun
    {
        BaseRunner Run(params Action[] actions);
        BaseRunner Run(IEnumerable<Action> actions);
    }
}