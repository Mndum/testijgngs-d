using IC.AutomaticReference;

public class CookingGameplayState : GameState
{
	private CookingGameplayStartInfo _startInfo;

	private CookingGameplayStaticGui _gui;

	public CookingGameplayState(CookingGameplayStartInfo startInfo)
	{
		_startInfo = startInfo;
	}

	public override void EnterState()
	{
		CookingStateUtilities.RegisterSilenceMusic();
		SingletonMonoBehaviour<AutomaticReferenceSingleton>.Instance.AvoidMemoryCleaning = true;
		GetAndEnableStaticGui<CookingGameplayStaticGui>(out _gui);
		_gui.StartGameplay(_startInfo, (CookingResults results) =>
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new CookingResultsState(results));
		});
	}

	public override void ExitState()
	{
		CookingStateUtilities.UnregisterSilenceMusic();
		DisableAllClaimedStaticGui();
		SingletonMonoBehaviour<AutomaticReferenceSingleton>.Instance.AvoidMemoryCleaning = false;
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
	}
}
