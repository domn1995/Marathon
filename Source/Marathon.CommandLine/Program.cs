using Marathon.Library;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Marathon.CommandLine
{
    public static class Program
    {
        private static readonly Runner runner = new Runner();
        private static Stopwatch sw;

        private static readonly Action l = delegate
        {
            Console.CursorLeft = 0;
            Console.WriteLine("Starting long wait...");
            Task.Delay(5000).Wait();
            Console.CursorLeft = 0;
            Console.WriteLine($"Long wait time: {sw.Elapsed.TotalMilliseconds}ms");
        };

        private static readonly Action s = delegate
        {
            Stopwatch sw = Stopwatch.StartNew();
            Task.Delay(1000).Wait();
            Console.CursorLeft = 0;
            Console.WriteLine($"Short wait time: {sw.Elapsed.TotalMilliseconds}ms");
        };

        public static void Main()
        {
            sw = Stopwatch.StartNew();
            AsyncWork();
            SimulateUiWork();
        }

        private static void SyncWork()
        {
            runner.Run(l).Then(l).Then(l).Sync();
            Console.CursorLeft = 0;
            Console.WriteLine($"{nameof(SyncWork)} done.");
        }

        private static async Task AsyncWork()
        {
            await runner.Run(l).Then(l).Then(l).Async();
            Console.CursorLeft = 0;
            Console.WriteLine($"{nameof(AsyncWork)} done.");
        }

        private static void SimulateUiWork()
        {
            while (true)
            {
                Console.Write("\b\b\\ ");
                Task.Delay(250).Wait();
                Console.Write("\b|");
                Task.Delay(250).Wait();
                Console.Write("\b/");
                Task.Delay(250).Wait();
                Console.Write("\b--");
                Task.Delay(250).Wait();
            }
        }
    }
}