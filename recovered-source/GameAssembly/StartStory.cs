using Fungus;
using UnityEngine;

[CommandInfo("My", "Start Story", "Starts story mode", 0)]
public class StartStory : Command
{
	public override void OnEnter()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
		Continue();
	}

	public override string GetSummary()
	{
		return "Starts story mode.";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
