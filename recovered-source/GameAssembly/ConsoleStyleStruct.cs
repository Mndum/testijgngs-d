using System;
using UnityEngine;

[Serializable]
public struct ConsoleStyleStruct(Color32 textColor, Color32 backgroundColor)
{
	public Color32 TextColor = textColor;

	public Color32 BackgroundColor = backgroundColor;
}
