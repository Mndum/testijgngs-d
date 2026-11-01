using System;
using UnityEngine;

[Serializable]
public struct ButtonColorPalette(Color backgroundColor, Color backgroundColorActive)
{
	public Color BackgroundColor = backgroundColor;

	public Color BackgroundColorActive = backgroundColorActive;

	public Color BackgroundColorDisabled = backgroundColor * 0.5f;

	public Color TextColor = Color.white;
}
