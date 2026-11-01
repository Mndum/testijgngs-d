public class ParameterMappingDirect : ParameterMapping
{
	public static ParameterMappingDirect Direct = new ParameterMappingDirect();

	private ParameterMappingDirect()
	{
	}

	public override float MapTo(float outputValue)
	{
		return outputValue;
	}

	public override float MapFrom(float inputValue)
	{
		return inputValue;
	}
}
