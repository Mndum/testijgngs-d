public interface ILive2DController_Headpat
{
	[Live2DControllerInterfaceParameterData(1f, Live2DParameterUpdateTypeEnum.Linear)]
	Live2DParameter ParamHeadpatX { get; }

	[Live2DControllerInterfaceParameterData(1f, Live2DParameterUpdateTypeEnum.Linear)]
	Live2DParameter ParamHeadpatY { get; }

	Live2DParameter ParamHeadpat { get; }
}
