using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;

[CommandInfo("My", "BetterPlayMusic", "Starts music", 0)]
public class BetterPlayMusic : Command
{
	public static string CommandMusicId = "CommandMusic";

	public bool clearOther = true;

	public MusicWithInfo music;

	public MusicGroup musicGroup;

	public override void OnEnter()
	{
		if (clearOther)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(CommandMusicId, stopIfPlaying: false);
		}
		if (music != null)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(CommandMusicId, SoundSingleton.MusicOption.MusicOptionLayer.Forced, music));
		}
		if (musicGroup != null)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(CommandMusicId, musicGroup.music, SoundSingleton.MusicOption.MusicOptionLayer.Forced);
		}
		if (music == null && musicGroup == null)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(CommandMusicId, SoundSingleton.MusicOption.MusicOptionLayer.Forced, null));
		}
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
		Continue();
	}

	public override string GetSummary()
	{
		List<MusicWithInfo> list = new List<MusicWithInfo>();
		if (music != null)
		{
			list.Add(music);
		}
		if (musicGroup != null)
		{
			list.AddRange(musicGroup.music.Where((MusicWithInfo t) => t != null));
		}
		if (list.Count == 0)
		{
			return "Stops all music(Empty song)!";
		}
		return "Plays " + string.Join(" or ", list.Select((MusicWithInfo t) => "\"" + t.Info + "\"")) + "!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(50, 50, 250, 255);
	}
}
