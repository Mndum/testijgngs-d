using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class BuyTokensButton : MonoBehaviour
{
	public int price;

	public int tokens;

	private void Start()
	{
		string text = LOC.UI.casinodotic_BuyTokens(tokens, price);
		int num = Mathf.CeilToInt((float)(tokens - price) / (float)tokens * 100f);
		if (num > 0)
		{
			text += LOC.UI.casinodotic_Off(num);
		}
		GetComponentInChildren<TextMeshProUGUI>().text = text;
		GetComponent<Button>().onClick.RemoveAllListeners();
		GetComponent<Button>().onClick.AddListener(() =>
		{
			gameObject.GetAncestor<CasinoWebsiteCore>().BuyTokensPopup(tokens, price);
		});
	}
}
