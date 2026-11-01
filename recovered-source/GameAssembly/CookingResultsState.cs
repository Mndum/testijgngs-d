public class CookingResultsState : GameState
{
	private CookingResults _results;

	private CookingResultsStaticGui _gui;

	public CookingResultsState(CookingResults results)
	{
		_results = results;
	}

	public override void EnterState()
	{
		CookingStateUtilities.RegisterSilenceMusic();
		GetAndEnableStaticGui<CookingResultsStaticGui>(out _gui);
		_gui.DisplayResults(_results);
	}

	public override void ExitState()
	{
		CookingStateUtilities.UnregisterSilenceMusic();
		DisableAllClaimedStaticGui();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
	}
}
