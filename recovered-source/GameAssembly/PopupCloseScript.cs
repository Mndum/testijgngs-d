using UnityEngine;

public class PopupCloseScript : MonoBehaviour
{
	private UIPopup _targetPopup;

	private bool _initialized;

	public void Start()
	{
		Init();
	}

	private void Init()
	{
		if (!_initialized)
		{
			_targetPopup = GetComponent<UIPopup>();
			if (_targetPopup == null)
			{
				Debug.LogError("Popup not found in " + name + "!!!");
			}
			else
			{
				_initialized = true;
			}
		}
	}

	public void Close()
	{
		Init();
		if (_initialized)
		{
			if (_targetPopup is IPopupCloseScriptHandler popupCloseScriptHandler)
			{
				popupCloseScriptHandler.CloseFromPopupCloseScript();
			}
			else if (_targetPopup != null)
			{
				_targetPopup.CloseFromUIOverlay();
			}
		}
	}
}
