using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
	public TextMeshProUGUI TextMeshProUGUI;

	public string lastText;

	public TMP_FontAsset lastFont;

	private void Update()
	{
		string text = TextMeshProUGUI.text;
		if (lastText != text)
		{
			Debug.LogError("Changed to \"" + text + "\"");
			lastText = text;
		}
		TMP_FontAsset font = TextMeshProUGUI.font;
		if (lastFont != font)
		{
			Debug.LogError($"Font changed from {lastFont} to {font}!");
			Debug.LogError($"New font is null: {font == null}!");
			lastFont = font;
		}
	}
}
