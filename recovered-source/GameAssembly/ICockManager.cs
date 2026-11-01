public interface ICockManager : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_SerializedState, Live2DController.ILive2DControllerManager_Callback
{
	bool IsCockCummingState { get; }

	bool IsCockNormalStateOrHidden { get; }

	bool AutoFuckImplemented { get; }

	bool IsFlacid { get; set; }

	float TargetCumBarValue { get; set; }

	float CumBarValue { get; }

	bool ShouldGetFlacid { get; }

	bool AllowFlaccidCumming { get; }

	void FillLeftButtonList(IModificationPeriodButtonList modificationPeriod);

	bool IsCockLimbInside();

	bool TryGetCockLimbUsage(out ICockLimbUsage o);

	void WhipOutCock();

	void AddCumBarValue(float amount);

	bool IsCockVisible();

	bool FixedUpdateCumBarValue();

	void PrintDebugInfo();
}
