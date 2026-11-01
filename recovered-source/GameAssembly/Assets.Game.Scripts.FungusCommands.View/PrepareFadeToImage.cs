using Fungus;
using UnityEngine;

namespace Assets.Game.Scripts.FungusCommands.View;

[CommandInfo("Camera", "Prepare Fade to Image", "Prepares fade to image by getting a view", 0)]
[AddComponentMenu("")]
public class PrepareFadeToImage : Command
{
	public override void OnEnter()
	{
		SingletonMonoBehaviour<ViewSingleton>.Instance.SetNextView();
		Continue();
	}

	public override string GetSummary()
	{
		return "Prepares the transition";
	}

	public override Color GetButtonColor()
	{
		return new Color32(216, 228, 170, 255);
	}
}
