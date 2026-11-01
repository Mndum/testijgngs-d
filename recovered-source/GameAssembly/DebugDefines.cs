using Fungus;
using UnityEngine;

public static class DebugDefines
{
	public static bool LogMusicDebugInfo;

	public static bool LogLoadingInfo;

	public static bool ShowModLogs;

	public static bool DebugPrefEnabled
	{
		get
		{
			return OptionsStatic.DebugEnabledPref.Value;
		}
		set
		{
			OptionsStatic.DebugEnabledPref.Value = value;
		}
	}

	public static bool FastViewSwitching
	{
		get
		{
			return FungusManager.Instance.CameraManager.fastViewChanges;
		}
		set
		{
			FungusManager.Instance.CameraManager.fastViewChanges = value;
		}
	}

	public static void InitDefault()
	{
		if (Application.isEditor)
		{
			FastViewSwitching = true;
			LogMusicDebugInfo = false;
			ShowModLogs = false;
			LogLoadingInfo = true;
		}
		else
		{
			ShowModLogs = true;
			LogLoadingInfo = false;
		}
	}
}
