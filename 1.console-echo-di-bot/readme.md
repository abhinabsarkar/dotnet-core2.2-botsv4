# Console Echo Bot 

This is an echo bot running on console channel.

This bot is an extension of the [echo bot sample](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/01.console-echo) provided by microsoft using Dependency Injection. [Beginners tutorial on dependency injection](https://github.com/abhinabsarkar/dependencyinjection) is a good start for people to understand DI before getting into Dotnet Core.

The bot is built using [Microsoft Bot framework v4](https://dev.botframework.com/) on [Dotnet Core v2.2](https://docs.microsoft.com/en-us/dotnet/core/about), which is an open source, cross platform successor to .NET Framework. It has baked in [dependency injection (DI)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.

# Demo
```cmd
D:\microsoft\abhinab\console-echo-di-bot>dotnet run
Welcome to the EchoBot demoing dependency injection. Type something to get started.
hi
You sent 'hi'
hello
You sent 'hello'
^C
D:\microsoft\abhinab\console-echo-di-bot>
```