using ConsoleBot.Dialogs;
using ConsoleBot.Implementations;
using ConsoleBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ConsoleBot
{
    static class DI
    {
        public static ServiceProvider _serviceProvider;
        internal static IConfiguration _config;

        // Build the configuration with JSON file.         
        public static void LoadConfiguration()
        {
            _config = new ConfigurationBuilder()                
                .SetBasePath(Directory.GetCurrentDirectory()) // Requires Install-Package Microsoft.Extensions.Configuration.FileExtensions -Version 2.2.0
                // Other configuration files like xml, environment variables can also be added
                // Set the property "Copy to output directory" for the json file as "Copy always"
                .AddJsonFile("configsettings.json", optional: true, reloadOnChange: true) // Requires Microsoft.Extensions.Configuration.Json
                .Build();
        }

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

            // Register LUIS recognizer
            collection.AddSingleton<LuisIntentRecognizer>();

            // The Dialog that will be run by the bot.
            collection.AddSingleton<MainDialog>();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            collection.AddTransient<IDialogBot, DialogBot<MainDialog>>();

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
