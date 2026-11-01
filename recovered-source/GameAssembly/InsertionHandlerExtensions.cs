public static class InsertionHandlerExtensions
{
	public static bool ShouldCumFarWhenInside(InsertionHandlerOrificeTypeEnum orificeTypeEnum)
	{
		if (orificeTypeEnum == InsertionHandlerOrificeTypeEnum.Thighjob)
		{
			return true;
		}
		return false;
	}

	public static bool ShouldHideCumAnimationIfInside(InsertionHandlerOrificeTypeEnum orificeTypeEnum)
	{
		if (orificeTypeEnum == InsertionHandlerOrificeTypeEnum.None || orificeTypeEnum == InsertionHandlerOrificeTypeEnum.Thighjob)
		{
			return false;
		}
		return true;
	}

	public static CumTargetTypeEnum ToCumTarget(this InsertionHandlerOrificeTypeEnum orificeTypeEnum)
	{
		return orificeTypeEnum switch
		{
			InsertionHandlerOrificeTypeEnum.Vagina => CumTargetTypeEnum.Vagina, 
			InsertionHandlerOrificeTypeEnum.Anal => CumTargetTypeEnum.Anal, 
			InsertionHandlerOrificeTypeEnum.Mouth => CumTargetTypeEnum.Mouth, 
			InsertionHandlerOrificeTypeEnum.Thighjob => CumTargetTypeEnum.Thighs, 
			_ => CumTargetTypeEnum.Outside, 
		};
	}
}
