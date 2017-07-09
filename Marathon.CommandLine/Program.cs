using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.CommandLine
{
    public static class Program
    {
        public static void Main()
        {
            Runner runner = new Runner();
            Action hello = () => Console.Write("Hello ");
            Action world = () => Console.Write("world");
            Action emphasis = () => Console.Write("!");
            Action newLine = Console.WriteLine;
            runner.Run(hello, world, emphasis, newLine).Sync();

            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
    }
}