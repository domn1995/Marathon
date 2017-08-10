using System;

namespace Marathon.Library.Interfaces
{
    public interface IAnd
    {
        BaseRunner And(Action action);
    }
}