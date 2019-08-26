using System.Threading;
using System.Threading.Tasks;
using Console_EchoBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Console_EchoBot.Implementations
{
    public class EchoBot : IBot, IEchoBot
    {
        // Added this property to resolve the named instance 
        // Since Microsoft.Extensions.DependencyInjection doesn't support registering multiple implementations 
        // of the same interface, this name is used to resolve the instance using LINQ
        public string Name { get { return this.GetType().Name; } }

        // Constructor dependency injection
        public EchoBot()
        {

        }

        // Every Conversation turn for our EchoBot will call this method. In here
        // the bot checks the <see cref="Activity"/> type to verify it's a <see cref="ActivityTypes.Message"/>
        // message, and then echoes the user's typing back to them.
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Echo back to the user whatever they typed.
                await turnContext.SendActivityAsync($"You sent '{turnContext.Activity.Text}'");
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}