# Console Bot Showing Dialogs

This is a bot to demo [LUIS](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/what-is-luis) capabilities using console channel.

The bot is built using [Microsoft Bot framework v4](https://dev.botframework.com/) on [Dotnet Core v2.2](https://docs.microsoft.com/en-us/dotnet/core/about), which is an open source, cross platform successor to .NET Framework. It has baked in [dependency injection (DI)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.

The bot returns the intent 'Greeting' & 'Expression'. In case the intent identified is none, it pushes a new dialog into the dialog stack, in this case 'MyFirtsDialog'

# Demo
```cmd
D:\microsoft\abhinab\console-luis-bot>dotnet run
Welcome to the demo of console LUIS bot. Type something to get started.
hello
Top scoring intent: Greeting
cool
Top scoring intent: Acknowledge
jibrish
Please enter your name.
Abhinab
Please enter your age.
33
I have your name as Abhinab and age as 33. Thank you!
^C
D:\microsoft\abhinab\console-dialog-bot>
```