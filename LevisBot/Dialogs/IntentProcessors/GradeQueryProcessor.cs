using LevisBot.Attributes;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace LevisBot.Dialogs.IntentProcessors
{
  internal class GradeQueryProcessor : BaseIntentProcessor
  {
    public GradeQueryProcessor(LuisResult luisResult, Action<IDialogContext> defaultSuspendAction) : base(luisResult, defaultSuspendAction)
    {
    }

    [RequiredEntity("Course", PromptMessage = "For what course do you want to know this Information?")]
    public string Course { get; set; }

    [RequiredEntity("Type", PromptMessage = "What kind of Grade do you want to know?")]
    public string Type { get; set; }

    [RequiredEntity("builtin.ordinal", PromptMessage = "Which Information do you want to know?")]
    public string Ordinal { get; set; }

    protected override async Task Process(IDialogContext context)
    {
      await context.PostAsync($"{Course}/{Type}/{Ordinal}");
    }
  }
}