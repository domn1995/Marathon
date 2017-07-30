using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.CommandLine
{
    public static class Program
    {
        private static readonly Action hello = delegate { Console.Write("Hello "); };
        private static readonly Action world = () => Console.Write("world");
        private static readonly Action emphasis = () => Console.Write("!");
        private static readonly Action newLine = Console.WriteLine;
        private static readonly Runner runner = new Runner();

        public static void Main()
        {
            while (true)
            {
                char c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case '1':
                        Go1();
                        break;
                    case '2':
                        Go2();
                        break;
                    case '3':
                        Go3();
                        break;
                    case '4':
                        Go4();
                        break;
                }
                Console.WriteLine("Done.");
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press enter to quit.");
                Console.ReadLine();
            }
        }
        
        private static async Task Go1()
        {
            await runner.Run(hello)
                        .Then(world)
                        .Then(emphasis)
                        .And(emphasis)
                        .And(emphasis)
                        .Then(newLine)
                        .Async();
        }

        private static void Go2()
        {
            runner.Run(hello)
                  .Then(world)
                  .And(emphasis)
                  .And(emphasis)
                  .And(emphasis)
                  .Then(newLine)
                  .Sync();
        }

        private static async Task Go3()
        {
            await runner.Run(hello)
                        .Then(world)
                        .And(emphasis, emphasis, emphasis)
                        .Then(newLine)
                        .Async();
        }

        private static void Go4()
        {
            runner.Run(hello)
                  .Then(world)
                  .Then(emphasis, emphasis, emphasis)
                  .Then(newLine)
                  .Sync();            
        }
    }
}