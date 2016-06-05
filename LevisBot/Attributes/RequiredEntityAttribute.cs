using System;

namespace LevisBot.Attributes
{
  /// <summary>
  /// Marks an Entity that is required for processing a Luis Dialog
  /// </summary>
  public class RequiredEntityAttribute : Attribute
  {
    public RequiredEntityAttribute(string luisEntityName)
    {
      LuisEntityName = luisEntityName;
    }

    /// <summary>
    /// The Entity Name used by LUIS
    /// </summary>
    public string LuisEntityName { get; set; }

    /// <summary>
    /// The question that is used to inquire the user about the missing information.
    /// </summary>
    public string PromptMessage { get; set; }
  }
}