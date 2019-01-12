using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Marathon.CommandLine.FileManipulation
{
    public abstract class BaseFileManipulationExample : BaseExample
    {
        protected static string DirectoryPath => @"../../../../Marathon.CommandLine/Files";
        protected static Action StartAction => () => { Console.WriteLine($"[{Now}] Beginning file manipulation."); };
        protected static Action EndAction => () => { Console.WriteLine($"[{Now}] Done manipulating all files."); };
        protected static List<FileInfo> Files => new DirectoryInfo(DirectoryPath).EnumerateFiles().ToList();
        protected static IList<Action> FileActions => GetFileManipulations(Files).ToList();
        /// <summary>
        /// Gets all the file manipulations.
        /// </summary>
        /// <param name="fileInfos">The files to manipulate.</param>
        /// <returns>A collection of manipulation functions.</returns>
        protected static IEnumerable<Action> GetFileManipulations(IList<FileInfo> fileInfos)
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

        public abstract Task Go();
    }
}