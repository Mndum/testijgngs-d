public static class CookingStateUtilities
{
	public static string MusicId => "CookingState";

	public static void RegisterSilenceMusic()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(MusicId, SoundSingleton.MusicOption.MusicOptionLayer.AboveBase, null));
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
	}

	public static void UnregisterSilenceMusic()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(MusicId);
	}
}
