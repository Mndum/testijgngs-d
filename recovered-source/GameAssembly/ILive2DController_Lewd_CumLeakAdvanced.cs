public interface ILive2DController_Lewd_CumLeakAdvanced : ILive2DController_Lewd_CumLeakBasic
{
	Live2DParameter ParamCumLeakSource { get; }

	Live2DParameter ParamCumLeakSize { get; }

	void OnLeaked(InsertionHandlerOrificeTypeEnum orifice, float ml);
}
