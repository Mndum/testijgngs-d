using Fungus;
using UnityEngine;

[CommandInfo("My", "Set/Remove Flag", "Modifies a flag", 0)]
public class ModifyFlag : Command
{
	[SerializeField]
	protected bool substitite;

	[Tooltip("Flag to modify")]
	[SerializeField]
	public string flag;

	[Tooltip("Should it set or remove flag")]
	[SerializeField]
	protected BooleanData set = new BooleanData(v: true);

	public override void OnEnter()
	{
		string text = ((!substitite) ? flag : GetFlowchart().SubstituteVariables(flag));
		if (set.Value)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddFlag(text);
		}
		else
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.RemoveFlag(text);
		}
		Continue();
	}

	public override string GetSummary()
	{
		return (set.Value ? "Sets" : "Removes") + " flag \"" + flag + "\"";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
