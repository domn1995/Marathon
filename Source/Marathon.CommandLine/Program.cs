using System;
using System.Threading.Tasks;
using Marathon.CommandLine.FileManipulation;

namespace Marathon.CommandLine
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length > 0 && int.TryParse(args[0], out int result))
            {
                switch (result)
                {
                    case 1:
                        await new SynchronousSequentialFileManipulation().Go();
                        break;
                    case 2:
                        await new SynchronousParallelFileManipulation().Go();
                        break;
                    case 3:
                        Task task = new AsynchronousSequentialFileManipulation().Go();
                        Console.WriteLine("File manipulation tasks started asynchronously. Doing other stuff...");
                        await task;
                        Console.WriteLine("Back in Main().");
                        break;
                    case 4:
                        Task task2 = new AsynchronousParallelFileManipulation().Go();
                        Console.WriteLine("File manipulation tasks started asynchronously. Doing other stuff...");
                        await task2;
                        Console.WriteLine("Back in Main().");
                        break;
                    default:
                        PrintHelp();
                        break;
                }
            }
            else
            {
                PrintHelp();
            }
            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1 - Synchronous sequential file manipulation.");
            Console.WriteLine("2 - Synchronous parallel file manipulation.");
            Console.WriteLine("3 - Asynchronous sequential file manipulation.");
            Console.WriteLine("4 - Asynchronous parallel file manipulation.");
        }
    }
}