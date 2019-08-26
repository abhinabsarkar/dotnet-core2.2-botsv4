using ConsoleBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleBot
{
    internal class ConsoleBot
    {
        private IBot _bot;
        public ConsoleBot(IBot bot)
        {
            _bot = bot;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the demo of console LUIS bot. Type something to get started.");

            try
            {
                // Register the dependencies
                DI.RegisterServices();

                // Load the configuration settings file
                DI.LoadConfiguration();

                // Create the Console Adapter, and add Conversation State
                // to the Bot. The Conversation State will be stored in memory.
                var consoleAdapterService = DI._serviceProvider.GetService<IConsoleAdapter>();

                // Create the instance of our Bot.
                var dialogBotService = DI._serviceProvider.GetService<IDialogBot>();

                // Connect the Console Adapter to the Bot.
                consoleAdapterService.ProcessActivityAsync( 
                    async (turnContext, cancellationToken) => await dialogBotService.OnTurnAsync(turnContext)).Wait();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: " + ex.Message + ex.StackTrace);
                Console.ReadLine();
            }
            finally
            {
                // Calling Dispose() on service provider is mandatory as otherwise registered instances will not get disposed. 
                DI.DisposeServices();
                Console.WriteLine("Exitng the program");
                Console.ReadLine();
            }

        }
    }
}
