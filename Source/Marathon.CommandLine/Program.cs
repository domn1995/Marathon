using Marathon.Library;
using System;
using System.Threading.Tasks;

namespace Marathon.CommandLine
{
    public static class Program
    {
        private static readonly Runner runner = new Runner();

        public static void Main()
        {
            Task.Delay(10000).Wait();
            SynchronousSequentialFileManipulation.Go();
            Console.WriteLine("Press enter to quit.");
            Console.ReadLine(); 
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