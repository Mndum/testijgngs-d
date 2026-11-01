public class FishingResultsState : ResultGameState
{
	private FishingResults _results;

	private FishingResultsStaticGui _gui;

	public FishingResultsState(FishingResults fishingResults)
	{
		_results = fishingResults;
	}

	protected override void DefaultOnResult()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Outside_OutsideStart);
	}

	public override void EnterState()
	{
		GetAndEnableStaticGui<FishingResultsStaticGui>(out _gui);
		GetAndEnableStaticGui<GameStaticGui>(out var _);
		_gui.DisplayResults(_results, DefaultOnResult);
	}

	public override void ExitState()
	{
		DisableAllClaimedStaticGui();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
	}
}
