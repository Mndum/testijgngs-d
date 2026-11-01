using System.Text;
using TMPro;
using UnityEngine;

public class CasinoBalance : MonoBehaviour
{
	[SerializeField]
	private bool showMoney;

	private CasinoWebsiteCore casinoWebsiteCore;

	private TextMeshProUGUI balanceText;

	private int currentVisibleBalance;

	public void BuyMoreClicked()
	{
		casinoWebsiteCore.ChangeWebsite("casino.ic/tokens");
	}

	private void Start()
	{
		casinoWebsiteCore = gameObject.GetAncestor<CasinoWebsiteCore>();
		balanceText = transform.Find("Casino Balance Text").GetComponent<TextMeshProUGUI>();
		currentVisibleBalance = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.casinoTokens;
	}

	private void FixedUpdate()
	{
		int casinoTokens = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.casinoTokens;
		int money = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.money;
		currentVisibleBalance = GlobalFunctions.MoveTowards(currentVisibleBalance, casinoTokens, Mathf.Max(currentVisibleBalance / 10, casinoTokens / 10), 10);
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			if (currentVisibleBalance > casinoTokens)
			{
				currentVisibleBalance--;
				stringBuilder.AppendColorTagStart("red");
			}
			else if (currentVisibleBalance < casinoTokens)
			{
				currentVisibleBalance++;
				stringBuilder.AppendColorTagStart("green");
			}
			stringBuilder.Append(LOC.UI.casinodotic_Tokens(currentVisibleBalance));
			if (currentVisibleBalance != casinoTokens)
			{
				stringBuilder.AppendColorTagEnd();
			}
			if (showMoney)
			{
				stringBuilder.Append($"\n{money}$");
			}
			balanceText.SetText(stringBuilder);
		}
	}
}
