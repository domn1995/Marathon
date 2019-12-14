# Marathon [![Build Status](https://github.com/domn1995/Marathon/workflows/build/badge.svg)](https://github.com/domn1995/Marathon/actions/build)

The Marathon library provides a .NET, cross-platform, lightweight task running library. It facilitates the composition of tasks for sequential and parallel execution, or both. Further, it provides synchronous and asynchronous implementations of the composed tasks in a transparent manner to the user.

Leveraging the Task Parallel Library is even easier, clearer, and more expressive when using Marathon.

Compatibility
---
  - .NET Standard 2.1
  
Installation
---
   - Package Manage Console: `Install-Package Marathon`
   - .NET CLI: `dotnet add package Marathon`
   - .csproj reference: Add `<PackageReference Include="Marathon"/>` to the `<PropertyGroup>` node of your project.
   
Hello World
---
```csharp
// Create some Actions to execute.
Action hello = delegate { Console.Write("Hello, "); }
Action world = delegate { Console.Write("world"); }
Action emphasis = delegate { Console.Write("!"); }
Action nl = delegate { Console.WriteLine(); }
// The 'Runner' class builds and runs tasks from the given Actions.
Runner runner = new Runner();  
runner.Run(hello)     // `Run()` starts building and running the given delegates.
      .Then(world)    // `Then()` waits for the previous Task to finish before starting.
      .Then(emphasis)
      .And(emphasis)  // `And()` starts running the given delegate at the same time as the previous one.
      .And(emphasis)
      .Then(nl)
      .Sync();        // `Sync()` waits for all the tasks to finish, blocking the current thread.
```
Output:

    > Hello, world!!!
    >
    
In-Depth
---
Marathon tasks are built starting with the `Runner` class. As the only class that implements the `IRun` interface, it exposes the `Run()` method which starts the scheduling of `Action`s as tasks to be run. The `Run()` method returns a `BaseRunner` which exposes the two run types in Marathon, `And` and `Then`.

`And`s are the implementation of the `IAnd` interface and tell the scheduler that the given `Action` should be run in parallel as the previous `Action`. These can be chained as many times as desired. For example, `BaseRunner tasks = runner.Run(action1).And(action2).And(action3)` would build `tasks` to run `action1`, `action2`, and `action3` at the same time once either the `Sync()` or `Async()` method is invoked.

`Then`s are the implementation of the `IThen` interface and tell the scheduler that the given `Action` should only be run after the previous `Action` has finished. These can be chained as many times as desired. For example, `BaseRunner tasks = runner.Run(action1).Then(action2).Then(action3)` would build `tasks` to run `action1`, then `action2`, and finally `action3` once either the `Sync()` or `Async()` method is invoked.

Lastly, the crème de la crème that makes Marathon special: the ability to transparently execute the scheduled tasks synchronously or asynchronously. This is possible because the `BaseRunner` class implements the `ISync` and `IAsync` interfaces. These two interfaces expose the `Sync()` and `Async()` methods, respectively, and end the chain of a `Runner`'s tasks. The `Sync()` method schedules the tasks to be executed synchronously, blocking the current thread. Alternatively, the `Async()` method schedules the tasks to be executed asynchronously, just how you'd expect normal async methods in .NET to work. 

Examples
---
See [here](https://github.com/domn1995/Marathon/wiki/Examples) in the wiki.

Changes
---
  - Added examples to wiki.
  - Updated README file.
  - Tentatively finished initial pre-release.
