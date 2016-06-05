using LevisBot.Attributes;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace LevisBot.Dialogs.IntentProcessors
{
  [Serializable]
  public class CourseGradeQueryProcessor : BaseIntentProcessor
  {
    [RequiredEntity("Course",
                    PromptMessage = "For what course do you want to know the grade ?")]
    public string Course { get; set; }

    public CourseGradeQueryProcessor(LuisResult luisResult, Action<IDialogContext> defaultSuspendAction) : base(luisResult, defaultSuspendAction)
    {
    }

    protected override async Task Process(IDialogContext context)
    {
      await context.PostAsync($"Your grade in {Course} is: ...");
    }
  }
}