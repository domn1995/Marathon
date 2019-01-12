using Marathon.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.CommandLine.FileManipulation
{
    public class AsynchronousParallelFileManipulation : BaseFileManipulationExample 
    { 
        public override async Task Go()
        {
            Action fileAction1 = FileActions[0];
            Action fileAction2 = FileActions[1];
            Action fileAction3 = FileActions[2];
            Runner runner = new Runner();
            Task task = runner.Run(StartAction)
                              .Then(fileAction1)
                              .And(fileAction2)
                              .And(fileAction3)
                              .Async();
            await task;
            Console.WriteLine("File manipulation tasks finished. Jumped back into Go() method.");
        }
    }
}
