using System;
using UnityEngine;

public static class XRayManagerCommon
{
	public static IXRayManager CreateInstance<T>(T controller) where T : Live2DController, ILive2DController_Manager_XRay, ILive2DController_Manager_InsertionManager
	{
		return new XRayManagerCommon<T>(controller);
	}
}
public class XRayManagerCommon<T> : IXRayManager, Live2DController.ILive2DControllerManager, Live2DController.ILive2DControllerManager_StartUsageGameplay, Live2DController.ILive2DControllerManager_Callback where T : Live2DController, ILive2DController_Manager_XRay, ILive2DController_Manager_InsertionManager
{
	private readonly T _controller;

	protected bool _XrayOn;

	protected int _XrayInsertionHandlerIndex = -1;

	public bool ShouldXrayBeActive => _XrayOn;

	public bool IsXrayEnabled => _XrayOn;

	public int CurrentXrayInsertionHandlerIndex => _XrayInsertionHandlerIndex;

	public XRayManagerCommon(T controller)
	{
		_controller = controller ?? throw new ArgumentNullException("controller");
	}

	public bool IsXrayActiveFor(InsertionHandler insertionHandler)
	{
		if (!_XrayOn)
		{
			return false;
		}
		int num = _controller.InsertionManager.InsertionHandlers.IndexOf(insertionHandler);
		if (num < 0)
		{
			return false;
		}
		return num == _XrayInsertionHandlerIndex;
	}

	public void XrayToggle()
	{
		_XrayOn = !_XrayOn;
		SyncXRay();
	}

	public void SyncXRay()
	{
		if (_XrayOn)
		{
			_XrayInsertionHandlerIndex = FindActiveInsertionHandlerIndex(_XrayInsertionHandlerIndex);
			if (_XrayInsertionHandlerIndex >= 0 && _controller.InsertionManager.InsertionHandlers.TryGetValue(_XrayInsertionHandlerIndex, out var value))
			{
				SetXrayInner(value.OrificeType);
				return;
			}
		}
		SetXrayInner(InsertionHandlerOrificeTypeEnum.None);
	}

	private int FindActiveInsertionHandlerIndex(int start)
	{
		if (start < 0)
		{
			start = 0;
		}
		for (int i = start; i < _controller.InsertionManager.InsertionHandlers.Count; i++)
		{
			if (_controller.InsertionManager.InsertionHandlers[i].IsActive())
			{
				return i;
			}
		}
		int num = Mathf.Min(_XrayInsertionHandlerIndex, _controller.InsertionManager.InsertionHandlers.Count - 1);
		for (int j = 0; j <= num; j++)
		{
			if (_controller.InsertionManager.InsertionHandlers[j].IsActive())
			{
				return j;
			}
		}
		return -1;
	}

	private void SetXrayInner(InsertionHandlerOrificeTypeEnum xrayType)
	{
		switch (xrayType)
		{
		default:
			_controller.ParamXray.SetRealValue(0f);
			break;
		case InsertionHandlerOrificeTypeEnum.Vagina:
			_controller.ParamXray.SetRealValue(1f);
			break;
		case InsertionHandlerOrificeTypeEnum.Anal:
			_controller.ParamXray.SetRealValue(2f);
			break;
		}
	}

	void Live2DController.ILive2DControllerManager_StartUsageGameplay.StartUsageGameplay()
	{
		_XrayInsertionHandlerIndex = -1;
		SyncXRay();
	}

	public void UpdateUI(FrickStaticGui _frickStaticGui)
	{
		_frickStaticGui.SetXrayButtonActive(active: true);
		_frickStaticGui.XrayToggle.onClick.SetListener(XRayButtonClicked);
		UpdateXRayButton();
	}

	private void XRayButtonClicked()
	{
		XrayToggle();
		UpdateXRayButton();
	}

	private void UpdateXRayButton()
	{
		bool shouldXrayBeActive = ShouldXrayBeActive;
		UiOverlay.Instance.GetStaticGUI<FrickStaticGui>(out var staticGui);
		staticGui.XrayOnImage.gameObject.SetActive(shouldXrayBeActive);
		staticGui.XrayOffImage.gameObject.SetActive(!shouldXrayBeActive);
	}
}
