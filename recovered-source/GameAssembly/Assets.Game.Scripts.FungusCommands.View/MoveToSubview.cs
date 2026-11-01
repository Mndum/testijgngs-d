using Fungus;
using UnityEngine;

namespace Assets.Game.Scripts.FungusCommands.View;

[CommandInfo("Camera", "Move To Subview", "Moves to subview of the current view!", 0)]
[AddComponentMenu("")]
public class MoveToSubview : Command
{
	[Tooltip("Time for fade effect to complete")]
	[SerializeField]
	public float duration = 0.5f;

	[Tooltip("Wait until the fade has finished before executing next command")]
	[SerializeField]
	public bool waitUntilFinished = true;

	[SerializeField]
	public Fungus.View.SubViews subView = Fungus.View.SubViews.ZoomLeft;

	public override void OnEnter()
	{
		Fungus.View currentView = SingletonMonoBehaviour<ViewSingleton>.Instance.CurrentView;
		if (currentView == null)
		{
			Continue();
			return;
		}
		currentView = currentView.GetSubView(subView);
		SingletonMonoBehaviour<GameUtilities>.Instance.MoveToView(currentView, () =>
		{
			if (waitUntilFinished)
			{
				Continue();
			}
		}, duration);
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override void OnStopExecuting()
	{
		FungusManager.Instance.CameraManager.Stop();
	}

	public override string GetSummary()
	{
		return $"Moves to subview({subView}) of the current view!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(216, 228, 170, 255);
	}
}
