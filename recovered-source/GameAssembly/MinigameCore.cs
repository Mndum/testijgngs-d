using System;
using System.Collections;
using TMPro;
using UnityEngine;

public abstract class MinigameCore : MonoBehaviour
{
	protected Action<float> _onComplete;

	public GameObject InstantiateAndInit(Action<float> onComplete, Transform parent)
	{
		MinigameCore minigameCore = UnityEngine.Object.Instantiate(this, parent);
		minigameCore.Init(onComplete);
		return minigameCore.gameObject;
	}

	protected virtual void Init(Action<float> onComplete)
	{
		_onComplete = onComplete;
	}

	private static TextMeshProUGUI CreateText(Transform parent, string text)
	{
		GameObject gameObject = new GameObject("Text");
		RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
		rectTransform.SetParent(parent, worldPositionStays: false);
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.sizeDelta = new Vector2(0f, 0f);
		TextMeshProUGUI textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
		textMeshProUGUI.fontSize = 42f;
		textMeshProUGUI.text = text;
		textMeshProUGUI.font = LocaleFontMetadata.GetFontCurrentLocale(LocaleFontMetadata.FontType.MainFont);
		textMeshProUGUI.outlineWidth = 0.1f;
		textMeshProUGUI.alignment = TextAlignmentOptions.Center;
		textMeshProUGUI.margin = new Vector4(20f, 20f, 20f, 20f);
		return textMeshProUGUI;
	}

	public static IEnumerator ShowText(Transform parent, string text, float show = 2f, float fadeIn = 0.5f, float fadeout = 0.1f, bool canBeSkipped = true)
	{
		TextMeshProUGUI tmp = CreateText(parent, text);
		yield return CoroutineUtilities.FadeIn(tmp, withShake: false, fadeIn);
		if (canBeSkipped)
		{
			float time = 0f;
			while (true)
			{
				time += Time.deltaTime;
				if (Input.anyKey || time >= show)
				{
					break;
				}
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(show);
		}
		yield return CoroutineUtilities.FadeOut(tmp, fadeout);
		UnityEngine.Object.Destroy(tmp.gameObject);
	}

	protected static IEnumerator ShowTextTillClick(Transform parent, string text, float fadeIn = 0.5f, float fadeout = 0.3f)
	{
		TextMeshProUGUI tmp = CreateText(parent, text);
		yield return CoroutineUtilities.FadeIn(tmp, withShake: false, fadeIn);
		yield return CoroutineUtilities.WaitForAnyKeyDown(tmp).Coroutine;
		yield return CoroutineUtilities.FadeOut(tmp, fadeout);
		UnityEngine.Object.Destroy(tmp.gameObject);
	}
}
