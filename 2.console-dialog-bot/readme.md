# Console Bot Showing Dialogs

This is a bot to demo dialogs using console channel.

This bot is an extension of the [echo bot sample](https://github.com/abhinabsarkar/botsv4-dotnet-core2.2/tree/master/1.console-echo-di-bot)

The bot is built using [Microsoft Bot framework v4](https://dev.botframework.com/) on [Dotnet Core v2.2](https://docs.microsoft.com/en-us/dotnet/core/about), which is an open source, cross platform successor to .NET Framework. It has baked in [dependency injection (DI)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.

# Demo
```cmd
D:\microsoft\abhinab\console-dialog-bot>dotnet run
Welcome to the demo of console dialog bot. Type something to get started.
hi
Please enter your name.
Abhinab
Please enter your age.
23
I have your name as Abhinab and age as 23. Thank you!
^C
D:\microsoft\abhinab\console-dialog-bot>
```