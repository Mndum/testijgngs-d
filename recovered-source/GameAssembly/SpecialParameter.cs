using System;
using UnityEngine;

[Serializable]
public sealed class SpecialParameter : ParameterBase
{
	public override float DefaultValue { get; }

	public override float MinimumValue { get; }

	public override float MaximumValue { get; }

	[field: SerializeField]
	public override float UnclampedValue { get; set; }

	public SpecialParameter(float minimumValue, float maximumValue, float defaultValue)
	{
		UnclampedValue = (DefaultValue = defaultValue);
		MinimumValue = minimumValue;
		MaximumValue = maximumValue;
	}
}
