public class CameraManager<T> : ICameraManager, Live2DController.ILive2DControllerManager where T : Live2DController, ILive2DController_Manager_Camera
{
	private readonly T _controller;

	private ButtonListWrapper _cameraButtonWrapper;

	public CameraManager(T controller)
	{
		_controller = controller;
	}

	private void CameraButtonClicked()
	{
		_controller.ToggleCamera();
	}

	public void FillLeftButtonList(IModificationPeriodButtonList buttonList)
	{
		_cameraButtonWrapper = buttonList.AddButton(50);
		_cameraButtonWrapper.SetText(LOC.UI.Frick_ChangeCamera);
		_cameraButtonWrapper.OnClick = CameraButtonClicked;
	}
}
