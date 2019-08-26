using ConsoleBot.Dialogs;
using ConsoleBot.Implementations;
using ConsoleBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleBot
{
    static class DI
    {
        public static ServiceProvider _serviceProvider;

        // Register the collection of services along with their implementations to the service container
        // These services will be injected to the Main method using dependency injection via constructor, property or method
        internal static void RegisterServices()
        {
            var collection = new ServiceCollection();

            // Create the Bot Framework Adapter for console
            collection.AddSingleton<IConsoleAdapter, ConsoleAdapter>();

            // Create the storage we'll be using for User and Conversation state. (Memory is great for testing purposes.)
            collection.AddSingleton<IStorage, MemoryStorage>();

            // Create the User state. (Used in this bot's Dialog implementation.)
            collection.AddSingleton<UserState>();

            // Create the Conversation state. (Used by the Dialog system itself.)
            collection.AddSingleton<ConversationState>();

            // The Dialog that will be run by the bot.
            collection.AddSingleton<MyFirstDialog>();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            collection.AddTransient<IDialogBot, DialogBot<MyFirstDialog>>();

            //// Create the bot as a transient.
            //collection.AddTransient<IEchoBot, EchoBot>();

            // ...
            // Keep adding other services
            // ...
            _serviceProvider = collection.BuildServiceProvider();
        }

        // Dispose the service collection
        internal static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
