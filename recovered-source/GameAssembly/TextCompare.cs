using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;
using UnityEngine.Localization;

[CommandInfo("My", "Text Compare", "Checks if text is equal", 0)]
internal class TextCompare : Command
{
	public enum CheckType
	{
		Full,
		ToLower,
		LevenshteinDistance
	}

	[Tooltip("Text to check against")]
	[SerializeField]
	public StringData inputText;

	[Tooltip("Where to store value")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	public Variable target;

	[Tooltip("Type of check")]
	[SerializeField]
	protected CheckType checkType;

	public List<StringData> toCompare;

	public List<LocalizedString> toCompareLocalizable;

	public IEnumerable<string> ToCompare => toCompare.Select((StringData t) => t.Value).Concat(toCompareLocalizable.Select((LocalizedString t) => t.GetLocalizedStringAsync().Result));

	private string CheckTypeString(CheckType checkType)
	{
		return checkType switch
		{
			CheckType.ToLower => "to lower of", 
			CheckType.LevenshteinDistance => "is similar to", 
			_ => "exactly", 
		};
	}

	public override void OnEnter()
	{
		if (target != null && target is BooleanVariable booleanVariable)
		{
			string lower = inputText.Value.ToLowerInvariant().Trim();
			switch (checkType)
			{
			default:
				booleanVariable.Value = ToCompare.Any((string t) => t == inputText.Value);
				break;
			case CheckType.ToLower:
				booleanVariable.Value = ToCompare.Any((string t) => t.ToLowerInvariant() == lower);
				break;
			case CheckType.LevenshteinDistance:
				booleanVariable.Value = ToCompare.Any((string t) =>
				{
					string text = t.ToLowerInvariant().Trim();
					int num = Mathf.Max(text.Length / 4, 1);
					return GlobalFunctions.LevenshteinDistance(text, lower) <= (double)num;
				});
				break;
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (target == null || target.GetType() != typeof(BooleanVariable))
		{
			return "Error: No gamevariable selected";
		}
		if (toCompare.Count == 0 && toCompareLocalizable.Count == 0)
		{
			return "Error: No strings to compare against";
		}
		string text = null;
		if (toCompare.Count != 0)
		{
			text = text + " any of [" + string.Join(",", toCompare.Select((StringData a) => a.GetDescription())) + "]";
		}
		if (toCompareLocalizable.Count != 0)
		{
			text = text + " any of localizable [" + string.Join(",", toCompareLocalizable.Select((LocalizedString a) => a.TableEntryReference)) + "]";
		}
		return "Check if " + inputText.GetDescription() + " equals " + CheckTypeString(checkType) + " " + text + " and puts it in " + target.Key;
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
