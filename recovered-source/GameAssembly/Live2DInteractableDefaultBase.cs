using System;
using Live2D.Cubism.Core;
using UnityEngine;

public abstract class Live2DInteractableDefaultBase
{
	public delegate void HoldStartDelegate(CubismDrawable clickedDrawable);

	public delegate void HeldDelegate(Vector2 mouseMove);

	public delegate void HeldFixedDelegate();

	public delegate void HoldEndDelegate();

	private bool _innerActive = true;

	public Func<bool> ActiveFunc { get; set; }

	public bool Active
	{
		get
		{
			if (ActiveFunc == null)
			{
				return _innerActive;
			}
			return ActiveFunc();
		}
		set
		{
			ActiveFunc = null;
			_innerActive = value;
		}
	}

	public HoldStartDelegate OnHoldStart { get; set; }

	public HeldDelegate OnHeld { get; set; }

	public HeldFixedDelegate OnHeldFixed { get; set; }

	public HoldEndDelegate OnHoldEnd { get; set; }

	public void StartHold(CubismDrawable clickedDrawable)
	{
		OnHoldStart?.Invoke(clickedDrawable);
	}

	public void ContinueHold(Vector2 mouseMove)
	{
		OnHeld?.Invoke(mouseMove);
	}

	public void ContinueHoldFixed()
	{
		OnHeldFixed?.Invoke();
	}

	public void EndHold()
	{
		OnHoldEnd?.Invoke();
	}
}
