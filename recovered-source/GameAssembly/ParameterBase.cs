using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public abstract class ParameterBase
{
	public enum TweenType
	{
		Linear,
		EaseOut
	}

	public abstract float DefaultValue { get; }

	public abstract float MinimumValue { get; }

	public abstract float MaximumValue { get; }

	public abstract float UnclampedValue { get; set; }

	public float Value
	{
		get
		{
			return UnclampedValue;
		}
		set
		{
			UnclampedValue = value;
			Clamp();
		}
	}

	public float Length => MaximumValue - MinimumValue;

	public Action<float> GetTweenAction(float targetValue, TweenType tweenType = TweenType.Linear)
	{
		float unclampedValue = UnclampedValue;
		targetValue = Mathf.Clamp(targetValue, MinimumValue, MaximumValue);
		return GetTweenAction(unclampedValue, targetValue, tweenType);
	}

	public Action<float> GetTweenAction(float startingValue, float targetValue, TweenType tweenType = TweenType.Linear)
	{
		if (tweenType == TweenType.Linear || tweenType != TweenType.EaseOut)
		{
			return (float t) =>
			{
				UnclampedValue = Mathf.Lerp(startingValue, targetValue, t);
			};
		}
		return (float t) =>
		{
			UnclampedValue = EasingFunction.EaseOutCubic(startingValue, targetValue, t);
		};
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToEaseOut(float targetValue, float time)
	{
		return CoroutineUtilities.GenericTween(GetTweenAction(targetValue), time);
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToLinear(float targetValue, float time)
	{
		return CoroutineUtilities.GenericTween(GetTweenAction(targetValue, TweenType.EaseOut), time);
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToMaxLinear(float time)
	{
		return EnumeratorMoveToLinear(MaximumValue, time);
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToDefaultLinear(float time)
	{
		return EnumeratorMoveToLinear(DefaultValue, time);
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToMinLinear(float time)
	{
		return EnumeratorMoveToLinear(MinimumValue, time);
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToMaxEaseOut(float time)
	{
		return EnumeratorMoveToEaseOut(MaximumValue, time);
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToDefaultEaseOut(float time)
	{
		return EnumeratorMoveToEaseOut(DefaultValue, time);
	}

	[MustUseReturnValue]
	public IEnumerator EnumeratorMoveToMinEaseOut(float time)
	{
		return EnumeratorMoveToEaseOut(MinimumValue, time);
	}

	public void ResetToDefault()
	{
		Value = DefaultValue;
		Clamp();
	}

	protected virtual void Clamp()
	{
		UnclampedValue = Mathf.Clamp(UnclampedValue, MinimumValue, MaximumValue);
	}

	public void SetRandom()
	{
		Value = UnityEngine.Random.Range(MinimumValue, MaximumValue);
	}

	public float GetPercent()
	{
		return (Value - MinimumValue) / Length;
	}

	public void SetPercent(float percent)
	{
		Value = MinimumValue + percent * Length;
	}

	public void SetPercentDirectional(float percent)
	{
		if (percent <= 0f)
		{
			Value = Mathf.LerpUnclamped(MinimumValue, DefaultValue, percent + 1f);
		}
		else
		{
			Value = Mathf.LerpUnclamped(DefaultValue, MaximumValue, percent);
		}
	}

	public void SetMaxValue()
	{
		Value = MaximumValue;
	}

	public void SetMinValue()
	{
		Value = MinimumValue;
	}

	public void AddPercent(float percent)
	{
		Value += percent * MaximumValue;
	}

	public void Add(float toAdd)
	{
		Value += toAdd;
	}

	public void MoveValueTowardsInUpdate(float target, float deltaTime, float unitPerSecond)
	{
		Value = Mathf.MoveTowards(Value, target, deltaTime * unitPerSecond);
	}

	public void MoveValueTowardsDefaultInUpdate(float deltaTime, float unitPerSecond)
	{
		MoveValueTowardsInUpdate(DefaultValue, deltaTime, unitPerSecond);
	}

	public void ClampOffsetFromDefaultValue(float offsetLeftPercent, float offsetRightPercent)
	{
		float num = (MinimumValue - DefaultValue) * offsetLeftPercent;
		float num2 = (MaximumValue - DefaultValue) * offsetRightPercent;
		Value = Mathf.Clamp(Value, num + DefaultValue, num2 + DefaultValue);
	}

	public void ClampValue(float min, float max)
	{
		Value = Mathf.Clamp(Value, min, max);
	}

	public void ClampValueSoftRepulsionFromMax(float min, float max, float softRange01, float sharpness = 0.25f)
	{
		if (Value < min)
		{
			Value = min;
		}
		else if (Value > max)
		{
			Value = max;
		}
		else if (Value > max - softRange01)
		{
			Value = GlobalFunctions.SmoothLerpEaseOutDeltaT(Value, max - softRange01, sharpness);
		}
	}
}
