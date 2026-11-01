using System;

[AttributeUsage(AttributeTargets.Property)]
public class CubismParameterName : Attribute
{
	public string Name { get; private set; }

	public float Weight { get; private set; }

	public Live2DParameterUpdateTypeEnum Type { get; private set; }

	public CubismParameterName(string name, float weight = 1f, Live2DParameterUpdateTypeEnum type = Live2DParameterUpdateTypeEnum.Smooth)
	{
		Name = name;
		Weight = weight;
		Type = type;
	}
}
