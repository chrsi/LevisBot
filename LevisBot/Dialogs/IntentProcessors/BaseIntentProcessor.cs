using LevisBot.Attributes;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LevisBot.Dialogs.IntentProcessors
{
  [Serializable]
  public abstract class BaseIntentProcessor
  {
    #region member

    /// <summary>
    /// A List of all required Properties
    /// </summary>
    private readonly IList<PropertyInfo> _requiredPropertys;

    /// <summary>
    /// The PropertyInfo that is currently prompted.
    /// This is needed to assign the received value when it is available.
    /// </summary>
    private PropertyInfo currentInfo { get; set; }

    /// <summary>
    /// Action for suspending the context
    /// </summary>
    private readonly Action<IDialogContext> _defaultSuspendAction;

    #endregion

    #region helper methods

    /// <summary>
    /// A Method to find all Required Properties
    /// </summary>
    /// <returns>A Enumerable of required Properties</returns>
    private IEnumerable<PropertyInfo> GetAllRequiredProperties()
    {
      return GetType()
              .GetProperties()
              .Where(propertyInfo => propertyInfo
                  .CustomAttributes
                  .Any(attr => attr.AttributeType == typeof(RequiredEntityAttribute)));

    }

    /// <summary>
    /// Gets the RequiredEntityAttribute instance of a given PropertyInfo
    /// </summary>
    /// <param name="propertyInfo">PropertyInfo attributing a required property</param>
    /// <returns>The corresponding RequiredEntityAttribute instance</returns>
    private RequiredEntityAttribute GetRequiredEntityAttribute(PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttribute(typeof(RequiredEntityAttribute), false) as RequiredEntityAttribute;
    }

    /// <summary>
    /// Assigns the Property to the Entity when received.
    /// </summary>
    /// <param name="context">Current DialogContext</param>
    /// <param name="result">Received entity value</param>
    /// <returns></returns>
    private async Task PromptForPropertyFinished(IDialogContext context, IAwaitable<string> result)
    {
      if (currentInfo == null) throw new Exception("There is no Property to set");
      currentInfo.SetValue(this, await result);
      currentInfo = null;
      await Run(context);
    }

    /// <summary>
    /// Prompts the user for the missing Property
    /// </summary>
    /// <param name="context">Current DialogContext</param>
    /// <param name="promptText">Question that is used to prompt the user for the missing information.</param>
    private void TryGetProperty(IDialogContext context, string promptText)
    {
      PromptDialog.PromptString prompt = new PromptDialog.PromptString(promptText, null, 1);
      context.Call(prompt, PromptForPropertyFinished);
    }

    /// <summary>
    /// Checks if all required properties are present.
    /// </summary>
    /// <param name="context">Current DialogContext</param>
    /// <returns></returns>
    private bool CheckAllRequiredProperties(IDialogContext context)
    {
      //try to get a missing required property
      var missingRequiredProperty = _requiredPropertys
        .FirstOrDefault(info => info.GetValue(this) == null);

      //if such a property is available prompt the user about it.
      if (missingRequiredProperty != null)
      {
        RequiredEntityAttribute requiredEntityAttribute = GetRequiredEntityAttribute(missingRequiredProperty);

        currentInfo = missingRequiredProperty;
        TryGetProperty(context, requiredEntityAttribute.PromptMessage);

        return false;
      }

      //otherwise wait for another action and continue processing
      _defaultSuspendAction(context);
      return true;
    }
    #endregion

    #region c'tor
    protected BaseIntentProcessor(LuisResult luisResult, Action<IDialogContext> defaultSuspendAction)
    {
      //assign parameters
      _defaultSuspendAction = defaultSuspendAction;

      //get all requiredproperties attributes
      _requiredPropertys = GetAllRequiredProperties().ToList();

      //assign each entity values to it's property if present
      foreach (var property in _requiredPropertys)
      {
        RequiredEntityAttribute entity = GetRequiredEntityAttribute(property);

        EntityRecommendation entityValue;
        if (luisResult.TryFindEntity(entity.LuisEntityName, out entityValue))
        {
          property.SetValue(this, entityValue.Entity);
        }
      }
    }
    #endregion

    /// <summary>
    /// Implements the Processor body.
    /// All Entities are present at this point.
    /// </summary>
    /// <param name="context">Current DialogContext</param>
    /// <returns></returns>
    protected abstract Task Process(IDialogContext context);

    /// <summary>
    /// Starts the processor.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Run(IDialogContext context)
    {
      bool allRequiredPropertiesPresent = CheckAllRequiredProperties(context);

      if (allRequiredPropertiesPresent) await Process(context);
    }
  }
}