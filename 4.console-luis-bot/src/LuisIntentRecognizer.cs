using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBot
{
    public class LuisIntentRecognizer : IRecognizer
    {
        private readonly LuisRecognizer _recognizer;

        public LuisIntentRecognizer()
        {
            var luisIsConfigured = !string.IsNullOrEmpty(DI._config["LuisAppId"]) && 
                !string.IsNullOrEmpty(DI._config["LuisAPIKey"]) && !string.IsNullOrEmpty(DI._config["LuisAPIHostName"]);

            if (luisIsConfigured)
            {
                var luisApplication = new LuisApplication(
                    DI._config["LuisAppId"],
                    DI._config["LuisAPIKey"], //TODO: Currently using authoring key. Need to replace it with API key
                    "https://" + DI._config["LuisAPIHostName"]);
                _recognizer = new LuisRecognizer(luisApplication);

            }
        }

        // Returns true if luis is configured in the appsettings.json and initialized.
        public virtual bool IsConfigured => _recognizer != null;

        public virtual async Task<RecognizerResult> RecognizeAsync(ITurnContext turnContext, CancellationToken cancellationToken)
            => await _recognizer.RecognizeAsync(turnContext, cancellationToken);

        public virtual async Task<T> RecognizeAsync<T>(ITurnContext turnContext, CancellationToken cancellationToken)
            where T : IRecognizerConvert, new()
            => await _recognizer.RecognizeAsync<T>(turnContext, cancellationToken);
    }
}
