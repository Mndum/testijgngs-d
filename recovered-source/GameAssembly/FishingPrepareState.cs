public class FishingPrepareState : ResultGameState<FishingStartInfo>
{
	private FishingPrepareStaticGui _gui;

	public override void EnterState()
	{
		GetAndEnableStaticGui<FishingPrepareStaticGui>(out _gui);
		GetAndEnableStaticGui<GameStaticGui>(out var _);
		_gui.Init(ReportResult);
	}

	protected override void DefaultOnResult(FishingStartInfo result)
	{
		if (result == null)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Outside_OutsideStart);
		}
		else
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.StartFishingGameplay(result);
		}
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
