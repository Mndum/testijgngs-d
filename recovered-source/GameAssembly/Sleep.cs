using Fungus;
using UnityEngine;

[CommandInfo("My", "Sleep", "Sleeps", 0)]
public class Sleep : Command
{
	public int minutes = 10;

	public override void OnEnter()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.Sleep(minutes, Continue, suppressEvents: true, GameVariables.StaminaSyncType.Recovery);
	}

	public override string GetSummary()
	{
		return "Tries to sleep for " + minutes + " minutes.";
	}

	public override Color GetButtonColor()
	{
		return new Color32(216, 228, 170, 255);
	}
}
