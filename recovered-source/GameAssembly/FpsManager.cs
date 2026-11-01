using UnityEngine;

public sealed class FpsManager : SingletonMonoBehaviour<FpsManager>
{
	private int defaultFPS = 144;

	private int defaultVSync;

	private bool _isLoading;

	private bool _hasFocus = true;

	public override void Awake()
	{
		base.Awake();
		defaultFPS = 120;
		defaultVSync = 1;
		Application.targetFrameRate = defaultFPS;
		QualitySettings.vSyncCount = defaultVSync;
	}

	public void StartLoading()
	{
		_isLoading = true;
		UpdateSettings();
	}

	public void EndLoading()
	{
		_isLoading = false;
		UpdateSettings();
	}

	private void UpdateSettings()
	{
		if (_isLoading)
		{
			Application.targetFrameRate = 1000;
			QualitySettings.vSyncCount = 0;
		}
		else if (_hasFocus)
		{
			Application.targetFrameRate = defaultFPS;
			QualitySettings.vSyncCount = defaultVSync;
		}
		else
		{
			Application.targetFrameRate = 30;
			QualitySettings.vSyncCount = 0;
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		_hasFocus = hasFocus;
		UpdateSettings();
	}
}
