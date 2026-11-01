public class ParameterMappingMinMax : ParameterMapping
{
	public readonly float InputMin;

	public readonly float InputRange;

	public readonly float Min;

	public readonly float Range;

	public ParameterMappingMinMax(float inputMin, float inputMax, float min, float max)
	{
		InputMin = inputMin;
		Min = min;
		InputRange = inputMax - inputMin;
		Range = max - Min;
	}

	public override float MapTo(float outputValue)
	{
		return (outputValue - Min) / Range * InputRange + InputMin;
	}

	public override float MapFrom(float inputValue)
	{
		return (inputValue - InputMin) / InputRange * Range + Min;
	}
}
