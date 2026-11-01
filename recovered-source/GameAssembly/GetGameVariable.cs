using System;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Get Game Variable", "Gets variable", 0)]
internal class GetGameVariable : Command
{
	[Tooltip("Target variable to store the value in.")]
	[VariableProperty(new Type[] { })]
	[SerializeField]
	public Variable variable;

	[Tooltip("Source variable from GameVariables")]
	[SerializeField]
	public string gamevariable;

	public override void OnEnter()
	{
		if (variable != null && !string.IsNullOrEmpty(gamevariable))
		{
			if (variable is IntegerVariable integerVariable)
			{
				integerVariable.Value = SingletonLoadableMonoBehaviour<GameScript>.Instance.GetGameVariableValue<int>(gamevariable);
			}
			else if (variable is FloatVariable floatVariable)
			{
				floatVariable.Value = SingletonLoadableMonoBehaviour<GameScript>.Instance.GetGameVariableValue<float>(gamevariable);
			}
			else if (variable is BooleanVariable booleanVariable)
			{
				booleanVariable.Value = SingletonLoadableMonoBehaviour<GameScript>.Instance.GetGameVariableValue<bool>(gamevariable);
			}
			else if (variable is StringVariable stringVariable)
			{
				stringVariable.Value = SingletonLoadableMonoBehaviour<GameScript>.Instance.GetGameVariableValue<string>(gamevariable);
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
		if (variable == null)
		{
			return "Error: Variable not selected";
		}
		if (gamevariable == null)
		{
			return "Error: No gamevariable selected";
		}
		if (!GlobalFunctions.HasPropertyOrField<GameVariables>(gamevariable, variable.GetValue().GetType(), GlobalFunctions.VariableType.HasGetter))
		{
			return "Error: No source variable found in gamevariables";
		}
		return variable.Key + " = " + gamevariable + " from game variables";
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
