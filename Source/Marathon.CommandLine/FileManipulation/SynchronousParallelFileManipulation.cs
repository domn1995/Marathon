using System;
using System.Threading.Tasks;
using Marathon.Library;

namespace Marathon.CommandLine.FileManipulation
{
    public class SynchronousParallelFileManipulation : BaseFileManipulationExample
    {
        public override async Task Go()
        {
            Action fileAction1 = FileActions[0];
            Action fileAction2 = FileActions[1];
            Action fileAction3 = FileActions[2];
            Runner runner = new Runner();
            runner.Run(StartAction)
                  .Then(fileAction1)
                  .And(fileAction2)
                  .And(fileAction3)
                  .Then(EndAction)
                  .Sync();
        }
    }
}