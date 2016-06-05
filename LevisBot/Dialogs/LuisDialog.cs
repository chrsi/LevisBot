using LevisBot.Attributes;
using LevisBot.Dialogs.IntentProcessors;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace LevisBot.Dialogs
{
  [Serializable]
  [MyLuisModel]
  public class LevisDialog : LuisDialog<object>
  {
    public LevisDialog(ILuisService service = null) : base(service)
    {
    }

    /// <summary>
    /// Suspends the context until another LuisMessage arrives. This Action is used by the IntentProcessors.
    /// </summary>
    /// <param name="context"></param>
    public void WaitForMessage(IDialogContext context)
    {
      context.Wait(MessageReceived);
    }

    #region LuisIntents

    [LuisIntent("None")]
    public async Task NothingUnderstood(IDialogContext context, LuisResult result)
    {
      await context.PostAsync($"Sorry, I couldn't understand your Request :(");
      context.Wait(MessageReceived);
    }

    [LuisIntent("CourseGradeQuery")]
    public async Task CourseGradeQuery(IDialogContext context, LuisResult result)
    {
      var gradeQuery = new CourseGradeQueryProcessor(result, WaitForMessage);

      await gradeQuery.Run(context);
    }

    public async Task GradeQuery(IDialogContext context, LuisResult result)
    {
      var gradeQuery = new GradeQueryProcessor(result, WaitForMessage);

      await gradeQuery.Run(context);
    }
    #endregion
  }
}