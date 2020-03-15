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

        public static Action<int> action2 = i =>
        {
            WriteLineWithTimeStamp($"action({i}): Starting work.");
            Task.Delay(2500).GetAwaiter().GetResult();
            WriteLineWithTimeStamp($"action({i}): Finished work.");
        };

        public static Action<int, double> action3 = (i, d) =>
        {
            WriteLineWithTimeStamp($"action({i}, {d}): Starting work.");
            Task.Delay(2500).GetAwaiter().GetResult();
            WriteLineWithTimeStamp($"action({i}, {d}): Finished work.");
        };

        public static async Task Main()
        {
            await Task.Delay(1000);
            WriteLineWithTimeStamp("Main(): Before work.");
            action.Run().Then(action2, 1).Then(action3, 1, 2.0).Sync();
            WriteLineWithTimeStamp("Main(): After work.");
            // Wait to exit.
            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }
    }
}