using System;
using Fungus;
using UnityEngine;

[CommandInfo("My", "PassTime", "Passes time", 0)]
public class PassTime : Command
{
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	public Variable variable;

	public int minutes = 10;

	public GameVariables.StaminaSyncType syncStamina;

	public override void OnEnter()
	{
		int num = minutes;
		if (variable != null && variable is IntegerVariable integerVariable)
		{
			num += integerVariable.Value;
		}
		SingletonLoadableMonoBehaviour<GameScript>.Instance.StartPassTime(num, Continue, suppressEvents: true, syncStamina);
	}

	public override string GetSummary()
	{
		if (variable == null)
		{
			return "Passes " + minutes + " minutes.";
		}
		return "Passes " + variable.Key + " + " + minutes + " minutes.";
	}

	public override Color GetButtonColor()
	{
		return new Color32(216, 228, 170, 255);
	}
}
