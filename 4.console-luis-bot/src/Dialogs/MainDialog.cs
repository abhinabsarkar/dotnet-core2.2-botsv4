using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly LuisIntentRecognizer _luisRecognizer;

        // Dependency injection uses this constructor to instantiate MainDialog
        public MainDialog(LuisIntentRecognizer luisRecognizer, MyFirstDialog myFirstDialog)
            : base(nameof(MainDialog))
        {
            _luisRecognizer = luisRecognizer;

            var waterfallSteps = new WaterfallStep[]
            {
                // Add the different steps in the Main Dialog
                LuisStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            // While subclassing a ComponentDialog, call AddDialog() within your constructor
            // The dialog must be included in the current or parent DialogSet
            AddDialog(myFirstDialog);

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> LuisStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            try
            {
                if (!_luisRecognizer.IsConfigured)
                {
                    var msg = $"LUIS is not configured";
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);

                    // LUIS is not configured
                    // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
                    return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
                }

                // Call LUIS and get the intent. (Note the TurnContext has the response to the prompt.)
                var luisResult = await _luisRecognizer.RecognizeAsync(stepContext.Context, cancellationToken);
                var intent = luisResult.GetTopScoringIntent().intent;

                switch(intent)
                {
                    case "Greeting":
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text("Top scoring intent: " + intent), cancellationToken);
                        // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
                        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);                        
                    case "Acknowledge":
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text("Top scoring intent: " + intent), cancellationToken);
                        // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
                        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
                    case "None":
                        // All the corresponding dialogs will happen in the context of APIC
                        var entity = "APIC";
                        // Push a new dialog (APIC dialog) into the dialog stack
                        // Run the APICDialog giving it whatever details we have, it will fill out the remainder.
                        return await stepContext.BeginDialogAsync(nameof(MyFirstDialog), entity, cancellationToken);
                    default:
                        break;
                }
                await stepContext.Context.SendActivityAsync(MessageFactory.Text(intent), cancellationToken);

                // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // LUIS is not responding correctly
                // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
