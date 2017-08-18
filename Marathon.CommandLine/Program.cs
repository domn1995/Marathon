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

        private static readonly Action longWait = delegate
        {
            Console.WriteLine("Starting long wait...");
            Task.Delay(5000).Wait();
            Console.WriteLine($"Long wait time: {sw.Elapsed.TotalMilliseconds}ms");
        };

        private static readonly Action shortWait = delegate
        {
            Stopwatch sw = Stopwatch.StartNew();
            Task.Delay(1000).Wait();
            Console.WriteLine($"Short wait time: {sw.Elapsed.TotalMilliseconds}ms");
        };

        public static void Main()
        {
            sw = Stopwatch.StartNew();
            AsyncWork();
            UiWork();
        }

        private static void SyncWork()
        {
            runner.Run(longWait).Then(longWait).Then(longWait).Sync();
            Console.WriteLine($"{nameof(SyncWork)} done.");
        }

        private static async Task AsyncWork()
        {
            await runner.Run(longWait).Async();
            Console.WriteLine($"{nameof(AsyncWork)} done.");
        }

        private static void UiWork()
        {
            while (true)
            {
                Console.Write("\b|");
                Task.Delay(500).GetAwaiter().GetResult();
                Console.Write("\b/");
                Task.Delay(500).GetAwaiter().GetResult();
                Console.Write("\b-");
                Task.Delay(500).GetAwaiter().GetResult();
                Console.Write("\b\\");
                Task.Delay(500).GetAwaiter().GetResult();
            }
        }
    }
}