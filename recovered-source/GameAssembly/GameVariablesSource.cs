using System;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Core.Extensions;

[Serializable]
public class GameVariablesSource : ISource
{
	public static readonly string Prefix = "gv_";

	public bool TryEvaluateSelector(ISelectorInfo selectorInfo)
	{
		if (!selectorInfo.SelectorText.StartsWith(Prefix))
		{
			return false;
		}
		string text = selectorInfo.SelectorText.Substring(Prefix.Length);
		if (GameScript.HasGameStarted)
		{
			if (GameVariablesClassDescription.Instance.TryFindAny(GameVariables.Current, text, out var value))
			{
				selectorInfo.Result = value;
				return true;
			}
			Debug.LogWarning("Variable \"" + text + "\" not found in gamevariables");
			selectorInfo.Result = text;
			return true;
		}
		if (GlobalFunctions.HasPropertyOrFieldAnyType(text, typeof(GameVariables), GlobalFunctions.VariableType.HasGetter))
		{
			Debug.LogWarning("Variable \"" + text + "\" found, but game hasn't been started yet!");
			selectorInfo.Result = text;
			return true;
		}
		Debug.LogWarning("Variable \"" + text + "\" not found!");
		selectorInfo.Result = text;
		return true;
	}
}
