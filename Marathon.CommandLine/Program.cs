using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.CommandLine
{
    public static class Program
    {
        private static readonly Runner runner = new Runner();
        private static Stopwatch sw;
        private static readonly Action start = () => sw = Stopwatch.StartNew();
        private static readonly Action stop = () => sw.Stop();
        private static readonly Action reset = () => sw.Reset();

        private static readonly Action longWait = delegate
        {
            Task.Delay(500).GetAwaiter().GetResult();
            Console.WriteLine($"Long: {sw.Elapsed.Milliseconds}");
        };

        private static readonly Action shortWait = () => Console.WriteLine($"Short: {sw.Elapsed.Milliseconds}");

        public static void Main()
        {
            AsyncWork();
            UiWork();
        }

        private static void SyncWork()
        {
            runner.Run(start).Then(longWait).Then(longWait).Then(longWait).Then(stop).Sync();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
        }

        private static async Task AsyncWork()
        {
            await runner.Run(start).Then(longWait).Then(longWait).Then(longWait).Async();
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