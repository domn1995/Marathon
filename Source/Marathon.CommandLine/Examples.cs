using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.CommandLine
{
    public static class SynchronousSequentialFileManipulation
    {
        /// <summary>
        /// Gets the current time.
        /// </summary>
        public static string Now => DateTime.Now.ToString("HH:mm:ss.fff");

        /// <summary>
        /// Performs a sequential file read and parse synchronously.
        /// </summary>
        public static void Go()
        {
            const string dirPath = @"../../../../Marathon.CommandLine/Files";
            var dirInfo = new DirectoryInfo(dirPath);
            List<FileInfo> files = dirInfo.EnumerateFiles().ToList();
            Action start = () => { Console.WriteLine($"[{Now}] Beginning file manipulation."); };
            IEnumerable<Action> fileActions = GetFileManipulations(files);
            Action end = () => { Console.WriteLine($"[{Now}] Done manipulating all files."); };

            Runner runner = new Runner();
            runner.Run(start)
                  .Then(fileActions)
                  .Then(end)
                  .Sync();
        }

        /// <summary>
        /// Gets all the file manipulations.
        /// </summary>
        /// <param name="fileInfos">The files to manipulate.</param>
        /// <returns>A collection of manipulation functions.</returns>
        private static IEnumerable<Action> GetFileManipulations(IList<FileInfo> fileInfos)
        {
            for (int i = 0; i < fileInfos.Count; ++i)
            {
                int index = i;
                yield return () =>
                {
                    var file = fileInfos[index];
                    Console.WriteLine($"[{Now}] Reading file #{index} \"{file.Name}\".");
                    // Simulating work.
                    Task.Delay(2500).GetAwaiter().GetResult();
                    string contents = File.ReadAllText(file.FullName);
                    Console.WriteLine($"[{Now}] Parsing file #{index} \"{file.Name}\".");
                    // Simulating work.
                    Task.Delay(2500).GetAwaiter().GetResult();
                    Console.WriteLine($"[{Now}] Done parsing file #{index} \"{file.Name}\".");
                };
            }
        }
    }
}