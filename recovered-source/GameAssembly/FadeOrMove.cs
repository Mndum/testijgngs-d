using System;
using Fungus;
using UnityEngine;

[CommandInfo("Camera", "Fade or move", "Fades the camera out and in again at a position specified by a View object or moves if they are in the same scene", 0)]
[AddComponentMenu("")]
public class FadeOrMove : Command
{
	[Tooltip("Time for fade effect to complete")]
	[SerializeField]
	protected float duration = 1f;

	[Tooltip("View to transition to when Fade is complete")]
	[SerializeField]
	protected View targetView;

	[Tooltip("Wait until the fade has finished before executing next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	public virtual View TargetView => targetView;

	public override void OnEnter()
	{
		if (targetView == null)
		{
			Continue();
			return;
		}
		Action afterFinished = null;
		if (waitUntilFinished)
		{
			afterFinished = Continue;
		}
		SingletonMonoBehaviour<GameUtilities>.Instance.FadeOrMoveToView(targetView, afterFinished, duration);
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
		if (targetView == null)
		{
			return "Error: No view selected";
		}
		return targetView.name;
	}

	public override Color GetButtonColor()
	{
		return new Color32(216, 228, 170, 255);
	}
}
