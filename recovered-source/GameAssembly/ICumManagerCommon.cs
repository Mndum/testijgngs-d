public interface ICumManagerCommon : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_SerializedState, Live2DController.ILive2DControllerManager_Callback
{
	float CurrentCumMouth { get; }

	float CurrentCumInside { get; }

	float CurrentCumInsideAnal { get; }

	void CumInside(float amount, float depth01, InsertionHandlerOrificeTypeEnum anal);

	void ClearCumInside(InsertionHandlerOrificeTypeEnum orfice);

	void SetFastWombLeakActive(bool value);

	bool TryExtractCum(InsertionHandlerOrificeTypeEnum vagina, float f);
}
