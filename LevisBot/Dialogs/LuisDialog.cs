using LevisBot.Attributes;
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
		public const string COURSE_NAME = "CourseName";

		public LevisDialog(ILuisService service = null) : base(service)
		{
		}

		[LuisIntent("None")]
		public async Task NothingUnderstood(IDialogContext context, LuisResult result)
		{
			await context.PostAsync($"Sorry, I couldn't understand your Request :(");
			context.Wait(MessageReceived);
		}

		[LuisIntent("GradeQuery")]
		public async Task GradeQuery(IDialogContext context, LuisResult result)
		{
			string course;

			EntityRecommendation courseEntity;
			if (result.TryFindEntity(COURSE_NAME, out courseEntity))
			{
				course = courseEntity.Entity;
			}
			else
			{
				await context.PostAsync($"Sorry, I didn't get the Course.");
				context.Wait(MessageReceived);
				return;
			}

			//retrieve course grade

			await context.PostAsync($"Your grade in {course} is: ...");
			context.Wait(MessageReceived);
		}
	}
}