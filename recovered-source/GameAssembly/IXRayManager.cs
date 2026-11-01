public interface IXRayManager : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_StartUsageGameplay, Live2DController.ILive2DControllerManager_Callback
{
	bool ShouldXrayBeActive { get; }

	bool IsXrayActiveFor(InsertionHandler insertionHandler);

	void XrayToggle();

	void SyncXRay();

	void UpdateUI(FrickStaticGui _frickStaticGui);
}
