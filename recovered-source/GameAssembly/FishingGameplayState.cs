public class FishingGameplayState : ResultGameState<FishingResults>
{
	private FishingStartInfo _fishingStartInfo;

	private FishingGameplayStaticGui _gui;

	private const string MusicId = "FishingGameplayState";

	public FishingGameplayState(FishingStartInfo fishingStartInfo)
	{
		_fishingStartInfo = fishingStartInfo;
	}

	public override void EnterState()
	{
		if (!_fishingStartInfo.IsFishingJointDuel())
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(BetterPlayMusic.CommandMusicId, stopIfPlaying: false);
			SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions("FishingGameplayState", R.Music.Fishing_All, SoundSingleton.MusicOption.MusicOptionLayer.AboveBase);
			SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
		}
		GetAndEnableStaticGui<FishingGameplayStaticGui>(out _gui);
		GetAndEnableStaticGui<GameStaticGui>(out var _);
		_gui.StartFishingMinigame(_fishingStartInfo, ReportResult);
	}

	protected override void DefaultOnResult(FishingResults result)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new FishingResultsState(result));
	}

	public override void ExitState()
	{
		_gui.KillMinigameTask();
		SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById("FishingGameplayState", stopIfPlaying: false);
		DisableAllClaimedStaticGui();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
	}
}
