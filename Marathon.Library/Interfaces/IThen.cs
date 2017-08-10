using System;

namespace Marathon.Library.Interfaces
{
    public interface IThen
    {
        BaseRunner Then(Action action);
    }
}