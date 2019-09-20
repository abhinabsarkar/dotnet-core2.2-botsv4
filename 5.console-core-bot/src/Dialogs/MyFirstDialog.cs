using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBot.Dialogs
{
    public class MyFirstDialog : CancelAndHelpDialog
    {
        bool tooManyAttempts;

        public MyFirstDialog(UserState userState)
            : base(nameof(MyFirstDialog))
        {

            // This array defines how the Waterfall will execute.
            var waterfallSteps = new WaterfallStep[]
            {
                GetNameStepAsync,
                GetAgeStepAsync,
                UserSummaryStepAsync,
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>), AgePromptValidatorAsync));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);

        }

        private async Task<DialogTurnResult> GetNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter your name.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> GetAgeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Get the result from previous prompt and add it to the step context
            stepContext.Values["name"] = (string)stepContext.Result;
            return await stepContext.PromptAsync(nameof(NumberPrompt<int>), 
                new PromptOptions 
                    { 
                        Prompt = MessageFactory.Text("Please enter your age."), 
                        RetryPrompt = MessageFactory.Text("Age should be a number and, it must be greater than 18 and less than 45. Please enter your age.")                         
                    }, 
                cancellationToken);
        }

        private async Task<DialogTurnResult> UserSummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (tooManyAttempts)
            {
                // We can send messages to the user at any point in the WaterfallStep.
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Too many attempts. Ending this dialog."), cancellationToken);

                tooManyAttempts = false;
                // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }

            // Fetch the value added to the step context
            var name = (string)stepContext.Values["name"];
            // Get the result from previous prompt
            var age = (int)stepContext.Result;
            var msg = $"I have your name as {name} and age as {age}. Thank you!";

            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);

            // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }    

        private async Task<bool> AgePromptValidatorAsync(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            // This condition is our validation rule. You can also change the value at this point.
            bool result = promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 18 && promptContext.Recognized.Value < 45;
            // If validation fails more than 3 times, return true and exit the dialog in the next step
            if (result == false && promptContext.AttemptCount > 2 )
            {
                // Set the flag as true
                tooManyAttempts = true;

                // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
                return await Task.FromResult(true);
            }
            else
            {                
                return await Task.FromResult(result);            
            }
        }
    }
}
