using UnityEngine;

public static class ClipboardHelper
{
	public static void CopyToClipboard(string text)
	{
		GUIUtility.systemCopyBuffer = text;
	}
}
