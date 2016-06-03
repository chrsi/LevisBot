using Microsoft.Bot.Builder.Luis;
using System;
using System.Configuration;

namespace LevisBot.Attributes
{
	[Serializable]
	public class MyLuisModel : LuisModelAttribute
	{
		public MyLuisModel() : base(
			ConfigurationManager.AppSettings["LuisModelId"],
			ConfigurationManager.AppSettings["LuisSubscriptionKey"])
		{
		}
	}
}