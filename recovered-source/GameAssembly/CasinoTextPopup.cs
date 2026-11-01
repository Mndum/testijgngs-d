using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CasinoTextPopup : MonoBehaviour
{
	private CasinoWebsiteCore _casinoWebsiteCore;

	public TextMeshProUGUI WinningsText { get; private set; }

	public bool IsRunning { get; private set; }

	public bool Skip { get; set; }

	private void SetText(string text)
	{
		WinningsText.gameObject.SetActive(value: true);
		WinningsText.text = text;
	}

	private IEnumerator FadeIn(bool withShake, float fadeInLength)
	{
		WinningsText.CrossFadeAlpha(0f, 0f, ignoreTimeScale: true);
		WinningsText.CrossFadeAlpha(1f, fadeInLength, ignoreTimeScale: true);
		if (withShake)
		{
			ShakingScript shakingScript = WinningsText.gameObject.AddComponent<ShakingScript>();
			yield return new WaitForSeconds(fadeInLength);
			Object.Destroy(shakingScript);
		}
		else
		{
			yield return new WaitForSeconds(fadeInLength);
		}
	}

	private IEnumerator FadeOut(float fadeOutLength)
	{
		WinningsText.CrossFadeAlpha(0f, fadeOutLength, ignoreTimeScale: true);
		yield return new WaitForSeconds(fadeOutLength);
		WinningsText.gameObject.SetActive(value: false);
	}

	private IEnumerator ShowSingleWin(string text, int win, int sumOfWinnings)
	{
		SetText(LOC.UI.casinodotic_Win(text, win, sumOfWinnings));
		yield return WaitForSecondCache.WaitForEndOfFrame;
		yield return FadeIn(withShake: true, 0.5f);
		if (!Skip)
		{
			yield return WaitForSecondCache.Seconds0_5;
		}
		if (!Skip)
		{
			yield return WaitForSecondCache.Seconds0_5;
		}
		if (!Skip)
		{
			yield return WaitForSecondCache.Seconds0_5;
		}
		if (!Skip)
		{
			yield return WaitForSecondCache.Seconds0_5;
		}
		yield return FadeOut(0.3f);
	}

	private IEnumerator ShowAndCashinWinnings(List<(string, int)> winningsList)
	{
		IsRunning = true;
		WinningsText.GetComponent<RectTransform>();
		int sumOfWinnings = 0;
		while (winningsList.Any())
		{
			(string, int) tuple = winningsList[winningsList.Count - 1];
			winningsList.RemoveAt(winningsList.Count - 1);
			int win = tuple.Item2;
			sumOfWinnings += win;
			if (!Skip)
			{
				yield return ShowSingleWin(tuple.Item1, win, sumOfWinnings);
			}
			_casinoWebsiteCore.AvailableTokens += win;
		}
		IsRunning = false;
		Skip = false;
	}

	public void StartShowing(List<(string, int)> winningsList)
	{
		StartCoroutine(ShowAndCashinWinnings(winningsList));
	}

	public IEnumerator ShowText(string text)
	{
		SetText(text);
		yield return FadeIn(withShake: false, 0.5f);
		yield return new WaitForSeconds(2f);
		yield return FadeOut(0.3f);
	}

	public void Awake()
	{
		WinningsText = transform.Find("Winnings Text").GetComponent<TextMeshProUGUI>();
		WinningsText.gameObject.SetActive(value: false);
		_casinoWebsiteCore = gameObject.GetComponent<CasinoWebsiteCore>();
	}
}
