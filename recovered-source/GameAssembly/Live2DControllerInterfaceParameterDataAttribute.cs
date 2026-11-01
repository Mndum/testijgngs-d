using System;

[AttributeUsage(AttributeTargets.Property)]
public class Live2DControllerInterfaceParameterDataAttribute : Attribute
{
	public float Weight { get; private set; }

	public Live2DParameterUpdateTypeEnum UpdateType { get; private set; }

	public Live2DControllerInterfaceParameterDataAttribute(float weight, Live2DParameterUpdateTypeEnum updateType = Live2DParameterUpdateTypeEnum.Smooth)
	{
		Weight = weight;
		UpdateType = updateType;
	}
}
