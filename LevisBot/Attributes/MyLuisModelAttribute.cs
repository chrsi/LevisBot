using Microsoft.Bot.Builder.Luis;
using System;
using System.Configuration;

namespace LevisBot.Attributes
{
	[Serializable]
	public class MyLuisModelAttribute : LuisModelAttribute
	{
		/// <summary>
		/// Custom LuisModel that inherits from the Microsoft LuisModel.
		/// This was implemented to extract the AppId and SubscriptionKey from the Codebase.
		/// </summary>
		public MyLuisModelAttribute() : base(
			ConfigurationManager.AppSettings["LuisModelId"],
			ConfigurationManager.AppSettings["LuisSubscriptionKey"])
		{
		}
	}
}