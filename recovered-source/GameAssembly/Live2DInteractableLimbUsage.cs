using System;
using System.Collections.Generic;
using System.Linq;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;

public class Live2DInteractableLimbUsage<T> : ILive2DInteractableHoverable, ILive2DInteractable where T : Live2DController
{
	private T _targetController;

	private ILimbUsageWithInteractable[] _limbUsages;

	public LimbUsageLive2DInteractableTypeEnum LimbUsageType;

	public Action<Live2DInteractableLimbUsage<T>, CubismDrawable> StartHoldOverride;

	public Func<T, bool> ActiveFunc;

	public Action<T> OnHoverStart;

	public Action<T> OnHoverEnd;

	public CursorSingleton.CursorType HoverCursor;

	public SimpleLocalizedString InfoSls;

	public T Controller => _targetController;

	public bool Active
	{
		get
		{
			if (ActiveFunc != null)
			{
				return ActiveFunc(_targetController);
			}
			return true;
		}
	}

	CursorSingleton.CursorType ILive2DInteractableHoverable.HoverCursor => HoverCursor;

	public string HoverText => InfoSls?.Value;

	public List<CubismRaycastable> ConnectedRaycastables { get; private set; }

	public Live2DInteractableLimbUsage(T targetController, SimpleLocalizedString InfoSls, params ILimbUsageWithInteractable[] limbUsages)
	{
		_targetController = targetController;
		_limbUsages = limbUsages;
		this.InfoSls = InfoSls;
	}

	public ILimbUsageWithInteractable[] GetLimbUsages()
	{
		return _limbUsages;
	}

	public bool ReportLimbUsageFinished(ILimbUsage limbUsage)
	{
		if (!_limbUsages.Contains(limbUsage))
		{
			return false;
		}
		for (int i = 0; i < _limbUsages.Length; i++)
		{
			if (_targetController.IsLimbUsageExactlyCorrect(limbUsage))
			{
				return false;
			}
		}
		return true;
	}

	public void EnsureAllLimbs()
	{
		for (int i = 0; i < _limbUsages.Length; i++)
		{
			ILimbUsageWithInteractable limbUsage = _limbUsages[i];
			_targetController.EnsureLimbs(limbUsage);
		}
	}

	public void StartHold(CubismDrawable clickedDrawable)
	{
		if (StartHoldOverride != null)
		{
			StartHoldOverride(this, clickedDrawable);
		}
		else
		{
			EnsureAllLimbs();
		}
	}

	public void ContinueHold(Vector2 mouseMove)
	{
		for (int i = 0; i < _limbUsages.Length; i++)
		{
			_limbUsages[i].ContinueHold(mouseMove);
		}
	}

	public void ContinueHoldFixed()
	{
		for (int i = 0; i < _limbUsages.Length; i++)
		{
			_limbUsages[i].ContinueHoldFixed();
		}
	}

	public void EndHold()
	{
		if (LimbUsageType != LimbUsageLive2DInteractableTypeEnum.StopAfterLetGo)
		{
			_ = 1;
			return;
		}
		for (int i = 0; i < _limbUsages.Length; i++)
		{
			ILimbUsageWithInteractable limbUsage = _limbUsages[i];
			_targetController.LetGoLimbs(limbUsage, LetGoLimbsCaller.FromInteractable);
		}
	}

	public void StartHover()
	{
		OnHoverStart?.Invoke(_targetController);
	}

	public void EndHover()
	{
		OnHoverEnd?.Invoke(_targetController);
	}

	public void SetConnectedRaycastables(List<CubismRaycastable> cubismRaycastables)
	{
		ConnectedRaycastables = cubismRaycastables;
	}
}
