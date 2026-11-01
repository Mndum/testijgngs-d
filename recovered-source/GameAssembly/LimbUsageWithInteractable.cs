using System;
using UnityEngine;

public class LimbUsageWithInteractable<T> : ILimbUsageWithInteractable, ILimbUsage where T : Live2DController
{
	private T _targetController;

	public Action<T> StartLimbAction;

	public Action<T> UpdateLimbAction;

	public Action<T> FixedUpdateLimbAction;

	public Action<T, LetGoLimbsCaller> EndLimbAction;

	private readonly LimbUsageChoiceEnum LimbUsageChoiceEnum;

	public Action<T, Vector2> HoldUpdateAction;

	public Action<T> HoldFixedUpdateAction;

	public LimbUsageChoiceEnum TargetLimbUsage => LimbUsageChoiceEnum;

	public LimbUsageWithInteractable(T targetController, LimbUsageChoiceEnum limbUsageChoiceEnum)
	{
		_targetController = targetController;
		LimbUsageChoiceEnum = limbUsageChoiceEnum;
	}

	public void Start()
	{
		StartLimbAction?.Invoke(_targetController);
	}

	public void Update()
	{
		UpdateLimbAction?.Invoke(_targetController);
	}

	public void FixedUpdate()
	{
		FixedUpdateLimbAction?.Invoke(_targetController);
	}

	public void End(LetGoLimbsCaller caller)
	{
		EndLimbAction?.Invoke(_targetController, caller);
	}

	public void ContinueHold(Vector2 input)
	{
		HoldUpdateAction?.Invoke(_targetController, input);
	}

	public void ContinueHoldFixed()
	{
		HoldFixedUpdateAction?.Invoke(_targetController);
	}
}
