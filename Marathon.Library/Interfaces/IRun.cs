using System;

namespace Marathon.Library.Interfaces
{
    public interface IRun
    {
        BaseRunner Run(Action action);
    }
}