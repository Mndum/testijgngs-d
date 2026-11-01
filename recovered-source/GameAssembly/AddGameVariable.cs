using Fungus;
using UnityEngine;

[CommandInfo("My", "Add To Game Variable", "Adds value to an integer variable", 0)]
internal class AddGameVariable : Command
{
	public bool isFloat;

	[Tooltip("Value to add")]
	[SerializeField]
	public IntegerData integervalue;

	[Tooltip("Value to add")]
	[SerializeField]
	public FloatData floatvalue;

	[Tooltip("Target variable from GameVariables")]
	[SerializeField]
	public string gamevariable;

	public override void OnEnter()
	{
		if (!string.IsNullOrEmpty(gamevariable))
		{
			if (isFloat)
			{
				float gameVariableValue = SingletonLoadableMonoBehaviour<GameScript>.Instance.GetGameVariableValue<float>(gamevariable);
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SetGameVariableValue(gamevariable, gameVariableValue + floatvalue.Value);
			}
			else
			{
				int gameVariableValue2 = SingletonLoadableMonoBehaviour<GameScript>.Instance.GetGameVariableValue<int>(gamevariable);
				SingletonLoadableMonoBehaviour<GameScript>.Instance.SetGameVariableValue(gamevariable, gameVariableValue2 + integervalue.Value);
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (string.IsNullOrEmpty(gamevariable))
		{
			return "Error: No gamevariable selected";
		}
		if (isFloat)
		{
			if (!GlobalFunctions.HasPropertyOrField<GameVariables>(gamevariable, typeof(float)))
			{
				return "Error: No source variable found in gamevariables";
			}
			return gamevariable + " from game variables += " + floatvalue.GetDescription();
		}
		if (!GlobalFunctions.HasPropertyOrField<GameVariables>(gamevariable, typeof(int)))
		{
			return "Error: No source variable found in gamevariables";
		}
		return gamevariable + " from game variables += " + integervalue.GetDescription();
	}

	public override bool HasReference(Variable var)
	{
		return var == integervalue.integerRef;
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
