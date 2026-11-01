using Fungus;
using UnityEngine;

[CommandInfo("My", "BetterStopMusic", "BetterStopsMusic", 0)]
public class BetterStopMusic : Command
{
	public override void OnEnter()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(BetterPlayMusic.CommandMusicId);
		Continue();
	}

	public override string GetSummary()
	{
		return "Stops command music!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
