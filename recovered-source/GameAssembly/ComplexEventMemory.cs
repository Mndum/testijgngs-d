using System;
using UnityEngine;

public class ComplexEventMemory : EventMemory
{
	public InteractionMemoryEventEnum LastRememberedActivateEvent;

	public object AdditionalData;

	public InteractionMemoryEventEnum LastRememberedDeactivateEvent;

	public bool Active { get; private set; }

	public float Length { get; protected set; }

	public float LastLength { get; protected set; }

	public float ActiveLength
	{
		get
		{
			if (!Active)
			{
				return 0f;
			}
			return Length;
		}
	}

	public event Action OnActivate;

	public event Action OnDeactivate;

	public override void FireSingle()
	{
		Active = false;
		Length = 0f;
		LastLength = Length;
		OnActivate?.Invoke();
		OnDeactivate?.Invoke();
	}

	public void Activate(InteractionMemoryEventEnum sourceEvent)
	{
		LastRememberedActivateEvent = sourceEvent;
		if (!Active)
		{
			Active = true;
			LastLength = Length;
			Length = 0f;
			OnActivate?.Invoke();
		}
	}

	public void Deactivate(InteractionMemoryEventEnum sourceEvent)
	{
		LastRememberedDeactivateEvent = sourceEvent;
		if (Active)
		{
			Active = false;
			LastLength = Length;
			Length = 0f;
			OnDeactivate?.Invoke();
		}
	}

	private bool IsRecent(float recentTime = 3f)
	{
		if (Length < recentTime)
		{
			return LastLength >= 0f;
		}
		return false;
	}

	public bool IsActiveOrRecent(float recentTime = 3f)
	{
		if (!Active)
		{
			return IsRecent(recentTime);
		}
		return true;
	}

	public bool BeenActiveFor(float activeForTime)
	{
		if (Active)
		{
			return Length >= activeForTime;
		}
		return false;
	}

	public bool TryGetAdditionalDataAs<T>(out T activateData)
	{
		if (AdditionalData is T val)
		{
			activateData = val;
			return true;
		}
		activateData = default;
		return false;
	}

	public override void Reset()
	{
		Active = false;
		Length = 0f;
		LastLength = -3.4028235E+38f;
		OnActivate = (OnDeactivate = null);
	}

	public override void Update()
	{
		Length += Time.fixedDeltaTime;
	}
}
