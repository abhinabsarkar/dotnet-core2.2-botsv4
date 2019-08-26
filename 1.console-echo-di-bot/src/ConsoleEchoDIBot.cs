//Console echo bot with dependency injection 
using Console_EchoBot.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Console_EchoBot
{
    internal class ConsoleEchoDIBot
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the EchoBot demoing dependency injection. Type something to get started.");

            // Register the dependencies
            DI.RegisterServices();

            // Create the Console Adapter, and add Conversation State
            // to the Bot. The Conversation State will be stored in memory.
            var consoleAdapterService = DI._serviceProvider.GetService<IConsoleAdapter>();

            // Create the instance of our Bot.
            var echoBotService = DI._serviceProvider.GetService<IEchoBot>();

            // Connect the Console Adapter to the Bot.
            consoleAdapterService.ProcessActivityAsync(
               async (turnContext, cancellationToken) => await echoBotService.OnTurnAsync(turnContext)).Wait();
        }
    }
}
