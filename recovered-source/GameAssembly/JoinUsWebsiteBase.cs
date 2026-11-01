public class JoinUsWebsiteBase : Website
{
	private string MusicId => GetType().Name;

	private void OnEnable()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(MusicId, SoundSingleton.MusicOption.MusicOptionLayer.Base, R.Music.Story_Hypersexual.Get()));
	}

	private void OnDisable()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(MusicId, stopIfPlaying: false);
	}

	public override bool IsWebsiteUnlocked()
	{
		return GameVariables.Current.joinUsBlogManager.CanEnterWebsite();
	}
}
