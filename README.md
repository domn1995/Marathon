# Marathon
The Marathon library provides a .NET, cross-platform, lightweight task running library. It facilitates the composition of tasks for sequential and parallel execution, or both. Further, it provides synchronous and asynchronous implementations of the composed tasks in a transparent manner to the user.

Leveraging the Task Parallel Library is even easier, clearer, and more expressive when using Marathon.

Compatibility
---
  - .NET Standard 2.0
  
Installation
---
   - Nuget: coming soon!
   
Hello World
---
    // Create some Actions to execute.
    Action hello = delegate { Console.Write("Hello, "); }
    Action world = delegate { Console.Write("world"); }
    Action emphasis = delegate { Console.Write("!"); }
    Action nl = delegate { Console.WriteLine(); }
    // The 'Runner' class builds and runs tasks from the given Actions.
    Runner runner = new Runner();
    // `Run()` starts building and running the given delegates.
    // `Then()` waits for the previous Task to finish before starting.
    // `And()` starts running the given delegate at the same time as the previous one.
    // `Sync()` waits for all the tasks to finish, blocking the current thread.
    runner.Run(h)
          .Then(world)
          .Then(emphasis)
          .And(emphasis)
          .And(emphasis)
          .Then(nl)
          .Sync();

Output:

    > Hello, world!!!
    >
    
In-Depth
---
Marathon tasks are built starting with the `Runner` class's `Run()` method.

There are two run types in Marathon: `And` and `Then`.
 
Changes
---
  - Updated README file.
  - Tenatively finished initial pre-release.
