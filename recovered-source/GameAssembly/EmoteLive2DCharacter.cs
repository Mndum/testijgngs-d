using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("My", "Live2D emote", "Sets a Live2D emote", 0)]
public class EmoteLive2DCharacter : Command
{
	public string targetCharacter = "bot";

	public List<EmoteData> emoteData;

	public override void OnEnter()
	{
		Live2DController readyController = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetReadyController(targetCharacter);
		if (readyController != null)
		{
			readyController.SetEmote(emoteData);
		}
		Continue();
	}

	public override string GetSummary()
	{
		return targetCharacter + "." + emoteData.ToStringBetter();
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 150, 50, 255);
	}
}
