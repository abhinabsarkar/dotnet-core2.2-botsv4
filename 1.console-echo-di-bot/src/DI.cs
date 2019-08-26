using Console_EchoBot.Implementations;
using Console_EchoBot.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Console_EchoBot
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

            // Create the bot as a transient.
            collection.AddTransient<IEchoBot, EchoBot>();

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
