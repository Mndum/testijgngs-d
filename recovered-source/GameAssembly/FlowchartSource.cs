using System;
using Fungus;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Core.Extensions;

[Serializable]
public class FlowchartSource : ISource
{
	public static readonly string Prefix = "f_";

	public bool TryEvaluateSelector(ISelectorInfo selectorInfo)
	{
		Flowchart flowchart = (Application.isPlaying ? SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart : UnityEngine.Object.FindFirstObjectByType<Flowchart>(FindObjectsInactive.Include));
		if (flowchart == null)
		{
			Debug.LogWarning("Couldn't find the flowchart!");
			return false;
		}
		if (!selectorInfo.SelectorText.StartsWith(Prefix))
		{
			return false;
		}
		string text = selectorInfo.SelectorText.Substring(Prefix.Length);
		Variable variable = flowchart.GetVariable(text);
		if (variable == null)
		{
			Debug.LogWarning("Variable \"" + text + "\" not found in the flowchart!");
			selectorInfo.Result = text;
			return true;
		}
		selectorInfo.Result = variable.GetValue();
		return true;
	}
}
