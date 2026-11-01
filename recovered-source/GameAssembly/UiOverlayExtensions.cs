using System;
using UnityEngine;

public static class UiOverlayExtensions
{
	public static void ShowResultIfFail(this Result result, string title = "Fail!", Action OnClose = null)
	{
		if (!result.IsSuccess)
		{
			Debug.LogError($"SRIF: {result.GetDescription()} ({result.GetType()})");
			UiOverlay.Instance.OkPopup(title, result.GetDescription(), OnClose);
		}
	}
}
