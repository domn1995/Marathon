using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Marathon.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marathon.Tests
{
    [TestClass]
    public class RunnerTests
    {
        private object locker = new object();
        private Stopwatch stopwatch;
        private readonly List<double> list = new List<double>();
        private Action start;
        private Action time;
        private Runner runner;

        [TestInitialize]
        public void TestInitialize()
        {
            stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            start = () =>
            {
                stopwatch.Start();
                list.Add(stopwatch.Elapsed.TotalMilliseconds);
            };
            time = async () =>
            {
                await Task.Delay(500);
                list.Add(stopwatch.Elapsed.TotalMilliseconds);
            };
            runner = new Runner();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            stopwatch.Stop();
        }

        [TestMethod]
        public void TestMethod1()
        {
            runner.Run(start).Then(time).Then(time).Sync();
        }
    }
}
