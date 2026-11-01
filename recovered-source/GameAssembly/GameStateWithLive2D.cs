using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateWithLive2D<TController> : GameState, IGameStateWithLive2D where TController : Live2DController
{
	protected virtual Live2DToolSelector TargetToolSelector => null;

	protected virtual IEnumerable<Live2DToolSelector.Live2DTool> TargetTools => null;

	public ModelBrain Brain { get; protected set; }

	public TController CurrentController { get; protected set; }

	public Live2DToolSelector Live2DToolSelector { get; protected set; }

	protected GameStateWithLive2D(TController live2DController)
	{
		CurrentController = live2DController;
	}

	protected abstract ModelBrainState GetTargetBrainState();

	public virtual void ChangeLive2DController(TController controller)
	{
		CurrentController = controller;
		PrepareReferences();
	}

	protected void PrepareReferences()
	{
		Brain = CurrentController.CurrentBrain;
		if (Brain == null)
		{
			Debug.LogError("Controller has no brain!");
		}
		Live2DToolSelector = TargetToolSelector;
		if (Live2DToolSelector != null)
		{
			Live2DToolSelector.gameObject.SetActive(value: true);
			Live2DToolSelector.ChangeController(CurrentController, TargetTools);
		}
		else
		{
			CurrentController.CurrentToolSelector = null;
		}
		Brain.SwitchToController(CurrentController);
		Brain.MakeSureInitialized();
		Brain.ChangeState(GetTargetBrainState(), force: true);
		CurrentController.SetEnabled(v: true);
	}

	public override void EnterState()
	{
		PrepareReferences();
	}

	public void DisableBrain()
	{
		Brain.DisableBrain();
	}

	public override void ExitState()
	{
		Brain.DisableBrain();
		CurrentController.SetEnabled(v: false);
	}
}
