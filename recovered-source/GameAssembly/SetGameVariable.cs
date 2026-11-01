using System;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Set Game Variable", "Sets variable", 0)]
internal class SetGameVariable : Command
{
	[Tooltip("Source variable to get value from")]
	[VariableProperty(new Type[] { })]
	[SerializeField]
	protected Variable variable;

	[Tooltip("Target variable in GameVariables to store the value in.")]
	[SerializeField]
	public string gamevariable;

	public override void OnEnter()
	{
		if (variable != null && !string.IsNullOrEmpty(gamevariable))
		{
			if (variable is IntegerVariable integerVariable)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SetGameVariableValue(gamevariable, integerVariable.Value);
			}
			else if (variable is FloatVariable floatVariable)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SetGameVariableValue(gamevariable, floatVariable.Value);
			}
			else if (variable is BooleanVariable booleanVariable)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SetGameVariableValue(gamevariable, booleanVariable.Value);
			}
			else if (variable is StringVariable stringVariable)
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SetGameVariableValue(gamevariable, stringVariable.Value);
			}
			else
			{
				Debug.LogWarning("Wrong wariable type selected!");
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (gamevariable == null)
		{
			return "Error: No gamevariable selected";
		}
		if (variable == null)
		{
			return "Error: Variable not selected";
		}
		if (!GlobalFunctions.HasPropertyOrField<GameVariables>(gamevariable, variable.GetValue().GetType(), GlobalFunctions.VariableType.HasSetter))
		{
			return "Error: No target variable found in gamevariables";
		}
		return gamevariable + " from game variables = " + variable.Key;
	}

	public override bool HasReference(Variable var)
	{
		return var == variable;
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
