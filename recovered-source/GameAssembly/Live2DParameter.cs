using System;
using Live2D.Cubism.Core;
using UnityEngine;

public sealed class Live2DParameter : ParameterBase
{
	private float _lastFrameRealValue;

	private float _realvalue;

	private float _weight;

	private float _defaultValue;

	private float _minimumValue;

	private float _maximumValue;

	public bool Exists { get; private set; }

	public CubismParameter Parameter { get; private set; }

	public bool IsApproximatelyDefaultValue => Mathf.Approximately(DefaultValue, UnclampedValue);

	public bool IsApproximatelyMinimalValue => Mathf.Approximately(MinimumValue, UnclampedValue);

	public bool IsApproximatelyMaximumValue => Mathf.Approximately(MaximumValue, UnclampedValue);

	public bool IsRealValueApproximatelyDefaultValue => Mathf.Approximately(DefaultValue, _realvalue);

	public bool IsRealValueApproximatelyMinimalValue => Mathf.Approximately(MinimumValue, _realvalue);

	public bool IsRealValueApproximatelyMaximumValue => Mathf.Approximately(MaximumValue, _realvalue);

	public override float DefaultValue => _defaultValue;

	public override float MinimumValue => _minimumValue;

	public override float MaximumValue => _maximumValue;

	public override float UnclampedValue { get; set; }

	public bool DirectlyControlledByModel { get; set; }

	public bool Repeat { get; set; }

	public Live2DParameterUpdateTypeEnum UpdateType { get; set; }

	private Live2DParameter(CubismParameter parameter, bool exits, bool directlyControlledByModel, float weight = 1f, bool repeat = false)
	{
		Parameter = parameter;
		Exists = exits;
		DirectlyControlledByModel = directlyControlledByModel;
		Repeat = repeat;
		_weight = weight;
		UpdateType = Live2DParameterUpdateTypeEnum.Smooth;
		if (exits)
		{
			_defaultValue = parameter.DefaultValue;
			_minimumValue = parameter.MinimumValue;
			_maximumValue = parameter.MaximumValue;
		}
		else
		{
			_defaultValue = 0f;
			_minimumValue = 0f;
			_maximumValue = 1f;
		}
		ResetToDefault();
	}

	public static Live2DParameter CreateReal(CubismParameter parameter, bool directlyControlledByModel, float weight = 1f, bool repeat = false)
	{
		return new Live2DParameter(parameter, exits: true, directlyControlledByModel, weight, repeat);
	}

	public static Live2DParameter CreateFake(bool directlyControlledByModel, float weight = 1f, bool repeat = false)
	{
		return new Live2DParameter(null, exits: false, directlyControlledByModel, weight, repeat);
	}

	public void SetRealValue(float value)
	{
		float realvalue = (UnclampedValue = value);
		_realvalue = realvalue;
	}

	public void SetOnlyRealValue(float value)
	{
		_realvalue = value;
	}

	public void AddOnlyRealValue(float to_add)
	{
		_realvalue += to_add;
	}

	public void AddRealAndInterpolatedValue(float toAdd)
	{
		Value += toAdd;
		_realvalue += toAdd;
	}

	public float GetRealValue()
	{
		return _realvalue;
	}

	public float GetRealValueLastFrame()
	{
		return _lastFrameRealValue;
	}

	public float GetTargetChange()
	{
		return UnclampedValue - _realvalue;
	}

	public float GetChangeFromLastFrame()
	{
		return _realvalue - _lastFrameRealValue;
	}

	public float GetAbsChangeFromLastFrame()
	{
		return Mathf.Abs(_lastFrameRealValue - _realvalue);
	}

	public float GetLastFrameRealSpeed()
	{
		return GetChangeFromLastFrame() / Time.deltaTime;
	}

	public float GetRealPercent()
	{
		return (_realvalue - MinimumValue) / Length;
	}

	public bool IsNearEdge(float edge = 0.1f)
	{
		float realPercent = GetRealPercent();
		if (realPercent < edge)
		{
			return true;
		}
		if (realPercent > 1f - edge)
		{
			return true;
		}
		return false;
	}

	public void SetWeight(float value)
	{
		_weight = value;
	}

	public float GetWeight()
	{
		return _weight;
	}

	public void FinishMove()
	{
		_realvalue = UnclampedValue;
		LateUpdate();
	}

	public void LateUpdate()
	{
		_lastFrameRealValue = _realvalue;
		if (!DirectlyControlledByModel || !Exists)
		{
			return;
		}
		Live2DParameterUpdateTypeEnum updateType = UpdateType;
		if (updateType == Live2DParameterUpdateTypeEnum.Smooth || updateType != Live2DParameterUpdateTypeEnum.Linear)
		{
			float sharpness = 1f / (2f + _weight);
			_realvalue = GlobalFunctions.SmoothLerpEaseOutDeltaT(_realvalue, UnclampedValue, sharpness);
			if (Mathf.Abs(UnclampedValue - _realvalue) < 0.0001f)
			{
				_realvalue = UnclampedValue;
			}
		}
		else
		{
			float num = Length * Time.deltaTime / _weight;
			float num2 = UnclampedValue - _realvalue;
			if (num2 > num)
			{
				_realvalue += num;
			}
			else if (num2 < 0f - num)
			{
				_realvalue -= num;
			}
			else
			{
				_realvalue = UnclampedValue;
			}
		}
		Parameter.Value = _realvalue;
	}

	public bool HasFinishedMove()
	{
		return Math.Abs(_realvalue - UnclampedValue) < 0.02f;
	}

	protected override void Clamp()
	{
		if (!Repeat)
		{
			UnclampedValue = Mathf.Clamp(UnclampedValue, _minimumValue, _maximumValue);
		}
	}

	public void ClampRealValueOnly(float min, float max)
	{
		_realvalue = Mathf.Clamp(_realvalue, min, max);
	}

	public bool IsApproximately(float value)
	{
		return Mathf.Approximately(value, UnclampedValue);
	}

	public bool IsApproximately(float value, float tolerance)
	{
		return Math.Abs(UnclampedValue - value) < tolerance;
	}

	public bool IsRealValueApproximately(float value, float tolerance)
	{
		return Math.Abs(_realvalue - value) < tolerance;
	}

	public bool Get01Bool()
	{
		return Value > 0.5f;
	}

	public void Set01Bool(bool value)
	{
		Value = (value ? 1f : 0f);
	}

	public void SetMinMaxBool(bool value)
	{
		Value = (value ? MaximumValue : MinimumValue);
	}
}
