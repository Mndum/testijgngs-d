using System;
using UnityEngine;

public static class QuitHandler
{
	private static bool PopupOpen;

	public static bool CanQuit { get; set; }

	private static bool WantsToQuit()
	{
		try
		{
			if (CanQuit)
			{
				return true;
			}
			if (PopupOpen)
			{
				return false;
			}
			if (GameScript.HasGameStarted || UiOverlay.Instance.IsOpen)
			{
				PopupOpen = true;
				SimplePopup simplePopup = ((!LoadingManager.LocalizationLoadingSuccessful) ? UiOverlay.Instance.SimplePopup("Are you sure?", "Are you sure you want to quit?", ForceQuitGame, () =>
				{
					PopupOpen = false;
				}) : UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.UI_AreYouSureYouWantToQuit, ForceQuitGame, () =>
				{
					PopupOpen = false;
				}));
				simplePopup.sortOrder = 100;
				UiOverlay.Instance.SyncAllPopups();
				return false;
			}
			return true;
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
			Console.WriteLine("Exception during quitting! Quitting anyway!");
			return true;
		}
	}

	public static void TryQuitGame()
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
		Application.Quit();
	}

	public static void ForceQuitGame()
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
		ForceQuitGameWithoutSave();
	}

	public static void ForceQuitGameWithoutSave()
	{
		CanQuit = true;
		Application.Quit();
	}

	[RuntimeInitializeOnLoadMethod]
	private static void RunOnStart()
	{
		Application.wantsToQuit += WantsToQuit;
	}
}
