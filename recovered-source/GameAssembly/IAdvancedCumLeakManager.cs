public interface IAdvancedCumLeakManager : Live2DController.ILive2DControllerManager
{
	void StartLeak(InsertionHandlerOrificeTypeEnum orfice);

	void EndLeak();
}
