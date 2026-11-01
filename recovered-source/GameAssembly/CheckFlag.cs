using System;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Check Flag", "Checks if flag exists", 0)]
public class CheckFlag : Command
{
	[Tooltip("Flag to modify")]
	[SerializeField]
	public string flag;

	[Tooltip("Where to store value")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	public Variable target;

	[Tooltip("Type of check")]
	[SerializeField]
	protected GameVariables.CheckType checkType;

	[Tooltip("Where to get time from")]
	[SerializeField]
	public IntegerData time;

	[Tooltip("Type of time check")]
	[SerializeField]
	protected GameVariables.TimeCheckType timeCheckType;

	private string CheckTypeString()
	{
		return checkType switch
		{
			GameVariables.CheckType.Start => "starting with", 
			GameVariables.CheckType.End => "ending with", 
			_ => "", 
		};
	}

	public override void OnEnter()
	{
		if (target != null && target is BooleanVariable booleanVariable)
		{
			booleanVariable.Value = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.CheckFlagSpecialWithTime(flag, checkType, time.Value, timeCheckType);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (target == null || target.GetType() != typeof(BooleanVariable))
		{
			return "Error: No gamevariable selected";
		}
		string value = ((time.integerRef != null) ? time.integerRef.Key : GameVariables.TimeToDateString(time.Value));
		return "Check if flag " + CheckTypeString() + " \"" + flag + "\" " + GameVariables.TimeCheckTypeString(timeCheckType, value) + " exists and puts it in " + target.Key;
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
