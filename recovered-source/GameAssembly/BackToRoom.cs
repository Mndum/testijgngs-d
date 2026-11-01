using Fungus;
using UnityEngine;

[CommandInfo("My", "Back To Room", "Changes the game state to room state", 0)]
public class BackToRoom : Command
{
	public override void OnEnter()
	{
		base.GetFlowchart().SetBooleanVariable("isHome", value: true);
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToRoomState();
		Continue();
	}

	public override string GetSummary()
	{
		return "Changes the game state to room state.";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
