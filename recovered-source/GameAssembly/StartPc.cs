using Fungus;
using UnityEngine;

[CommandInfo("My", "Start PC", "Starts pc", 0)]
public class StartPc : Command
{
	public override void OnEnter()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToPcState();
		Continue();
	}

	public override string GetSummary()
	{
		return "Starts pc.";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
