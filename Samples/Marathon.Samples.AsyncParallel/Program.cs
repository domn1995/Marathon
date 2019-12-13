using System;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.Samples.SyncSeq
{
    public static class Program
    {
        public static void WriteLineWithTimeStamp(string s) => Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] {s}");
        public static Action action = () =>
        {
            WriteLineWithTimeStamp("action(): Starting work.");
            Task.Delay(2500).GetAwaiter().GetResult();
            WriteLineWithTimeStamp("action(): Finished work.");
        };
        public static async Task Main()
        {
            await Task.Delay(1000);
            WriteLineWithTimeStamp("Main(): Before work.");
            var task = action.Run().And(action).And(action).Async();
            WriteLineWithTimeStamp("Main(): Starting some other work...");
            await Task.Delay(2000);
            WriteLineWithTimeStamp("Main(): Finished some other work.");
            await task;
            // Wait to exit.
            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }
    }
}