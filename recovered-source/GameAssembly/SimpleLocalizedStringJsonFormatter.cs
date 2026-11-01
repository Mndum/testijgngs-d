using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Core.Extensions;

[Serializable]
[DisplayName("Simple Localized String Json Formatter")]
public class SimpleLocalizedStringJsonFormatter : FormatterBase
{
	public override string[] DefaultNames => new string[2] { "SimpleLocalizedString", "sls" };

	public override bool TryEvaluateFormat(IFormattingInfo formattingInfo)
	{
		if (formattingInfo.CurrentValue is string json)
		{
			SimpleLocalizedStringWithBakedArguments simpleLocalizedStringWithBakedArguments = JsonUtility.FromJson<SimpleLocalizedStringWithBakedArguments>(json);
			if (simpleLocalizedStringWithBakedArguments != null)
			{
				formattingInfo.Write(simpleLocalizedStringWithBakedArguments.Value);
				return true;
			}
			SimpleLocalizedString simpleLocalizedString = JsonUtility.FromJson<SimpleLocalizedString>(json);
			if (simpleLocalizedString != null)
			{
				formattingInfo.Write(simpleLocalizedString.GetValue());
				return true;
			}
		}
		return false;
	}
}
