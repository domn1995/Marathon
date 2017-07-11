using System;
using System.Collections.Generic;

namespace Marathon.Library.Interfaces
{
    public interface IThen
    {
        BaseRunner Then(params Action[] actions);
        BaseRunner Then(IEnumerable<Action> actions);
    }
}