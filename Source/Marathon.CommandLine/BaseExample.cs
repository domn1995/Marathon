using System;

namespace Marathon.CommandLine
{
    public abstract class BaseExample
    {
        public static string Now => DateTime.Now.ToString("HH:mm:ss.fff");
    }
}
