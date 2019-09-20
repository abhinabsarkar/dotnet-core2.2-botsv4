# Console Core Bot Showing LUIS and Dialogs

This is a bot to demo its core capabilities (LUIS, Dialogs along with prompts, retry logic, etc)  using console channel.

The bot is built using [Microsoft Bot framework v4](https://dev.botframework.com/) on [Dotnet Core v2.2](https://docs.microsoft.com/en-us/dotnet/core/about), which is an open source, cross platform successor to .NET Framework. It has baked in [dependency injection (DI)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.

# Demo
```cmd
D:\microsoft\abhinab\console-luis-bot>dotnet run
Welcome to the demo of console LUIS bot. Type something to get started.
hi
Top scoring intent: Greeting
Please enter your name.
ab
Please enter your age.
12
Age should be a number and, it must be greater than 18 and less than 45. Please enter your age.
33
I have your name as ab and age as 33. Thank you!
^C
D:\microsoft\abhinab\console-dialog-bot>
```