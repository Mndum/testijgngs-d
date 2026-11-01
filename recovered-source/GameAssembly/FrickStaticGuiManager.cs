using UnityEngine;
using UnityEngine.UI;

public class FrickStaticGuiManager : Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_OnDisable, Live2DController.ILive2DControllerManager_Callback, Live2DController.ILive2DControllerManager_Update
{
	private Live2DController _controller;

	private FrickStaticGui _frickStaticGui;

	private Image _cumImage;

	private Image _flashbangImage;

	private Image _cumBarFill;

	private bool _flashBangOn;

	private const float _alphaTarget = 0.6f;

	public FrickStaticGuiManager(Live2DController controller)
	{
		_controller = controller;
		UiOverlay.Instance.GetStaticGUI<FrickStaticGui>(out _frickStaticGui);
		_cumImage = _frickStaticGui.CumImage;
		_flashbangImage = _frickStaticGui.FlashBangImage;
		_cumBarFill = _frickStaticGui.CumBar.fillRect.GetComponent<Image>();
		HideCumImage();
		ToggleCumFlashBang(active: false);
	}

	public void ShowCumImage()
	{
		if ((bool)OptionsStatic.CumFlashBangEnabledPref)
		{
			_cumImage.gameObject.SetActive(value: true);
		}
		SetCumImageAlpha(0f);
	}

	public void SetCumImageAlpha(float alpha)
	{
		_cumImage.color = _cumImage.color.SetAlpha(alpha);
	}

	public void HideCumImage()
	{
		_cumImage.gameObject.SetActive(value: false);
	}

	public void FlashRedCumBar(float alpha)
	{
		float num = Random.Range(0.7f, 1f);
		_cumBarFill.color = new Color(num, num * alpha, num * alpha);
	}

	public void ClearCumBar()
	{
		_cumBarFill.color = Color.white;
	}

	public void ToggleCumFlashBang(bool active)
	{
		_flashBangOn = active;
	}

	public void Update()
	{
		if (_flashBangOn)
		{
			if ((bool)OptionsStatic.CumFlashBangEnabledPref)
			{
				_flashbangImage.gameObject.SetActive(value: true);
			}
			Color color = _flashbangImage.color;
			color.a = GlobalFunctions.SmoothLerpEaseOutDeltaT(color.a, 0.6f, 0.2f);
			_flashbangImage.color = color;
		}
		else
		{
			Color color2 = _flashbangImage.color;
			color2.a = GlobalFunctions.SmoothLerpEaseOutDeltaT(color2.a, 0f, 0.2f);
			_flashbangImage.color = color2;
			if (color2.a < 0.001f)
			{
				_flashbangImage.gameObject.SetActive(value: false);
			}
		}
	}

	public void OnDisable()
	{
		HideCumImage();
		ToggleCumFlashBang(active: false);
		ClearCumBar();
	}

	public void UpdateUI()
	{
		if (_controller is ILive2DController_Manager_XRay live2DController_Manager_XRay)
		{
			live2DController_Manager_XRay.XRayManager.UpdateUI(_frickStaticGui);
		}
		else
		{
			_frickStaticGui.SetXrayButtonActive(active: false);
		}
		UpdateLeftButtonList();
	}

	public void UpdateLeftButtonList()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (_frickStaticGui.LeftButtonList.DoInitialization(out modificationPeriod))
		{
			if (_controller is ILive2DController_Manager_CockManager live2DController_Manager_CockManager)
			{
				live2DController_Manager_CockManager.CockManager.FillLeftButtonList(modificationPeriod);
			}
			if (_controller is ILive2DController_Manager_Camera live2DController_Manager_Camera)
			{
				live2DController_Manager_Camera.CameraManager.FillLeftButtonList(modificationPeriod);
			}
			foreach (ILimbUsage distinctLimbUsage in _controller.GetDistinctLimbUsages())
			{
				if (distinctLimbUsage is ILimbUsageGUI limbUsageGUI)
				{
					limbUsageGUI.LeftButtonGuiFill(modificationPeriod);
				}
			}
		}
	}
}
