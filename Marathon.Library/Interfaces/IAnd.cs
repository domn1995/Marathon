using System;
using System.Collections.Generic;

namespace Marathon.Library.Interfaces
{
    public interface IAnd
    {
        BaseRunner And(params Action[] actions);
        BaseRunner And(IEnumerable<Action> actions);
    }
}