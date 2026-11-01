using System;
using UnityEngine;

public class LimbUsageWithInteractableSymmetry<T> : ILimbUsageWithInteractable, ILimbUsage where T : Live2DController
{
	private T _targetController;

	private int _symmetryIndex;

	public Action<T, int> StartLimbAction;

	public Action<T, int> UpdateLimbAction;

	public Action<T, int> FixedUpdateLimbAction;

	public Action<T, int, LetGoLimbsCaller> EndLimbAction;

	private readonly LimbUsageChoiceEnum LimbUsageChoiceEnum;

	public Action<T, int, Vector2> HoldUpdateAction;

	public Action<T, int> HoldFixedUpdateAction;

	public LimbUsageChoiceEnum TargetLimbUsage => LimbUsageChoiceEnum;

	public LimbUsageWithInteractableSymmetry(T targetController, int symmetryIndex, LimbUsageChoiceEnum limbUsageChoiceEnum)
	{
		_targetController = targetController;
		_symmetryIndex = symmetryIndex;
		LimbUsageChoiceEnum = limbUsageChoiceEnum;
	}

	public void Start()
	{
		StartLimbAction?.Invoke(_targetController, _symmetryIndex);
	}

	public void Update()
	{
		UpdateLimbAction?.Invoke(_targetController, _symmetryIndex);
	}

	public void FixedUpdate()
	{
		FixedUpdateLimbAction?.Invoke(_targetController, _symmetryIndex);
	}

	public void End(LetGoLimbsCaller caller)
	{
		EndLimbAction?.Invoke(_targetController, _symmetryIndex, caller);
	}

	public void ContinueHold(Vector2 input)
	{
		HoldUpdateAction?.Invoke(_targetController, _symmetryIndex, input);
	}

	public void ContinueHoldFixed()
	{
		HoldFixedUpdateAction?.Invoke(_targetController, _symmetryIndex);
	}
}
