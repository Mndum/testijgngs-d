using System;
using UnityEngine.Events;

public class CasinoWebsiteCore : Website
{
	[Serializable]
	public class TokensChangeEvent : UnityEvent<int>
	{
	}

	public UnityEvent<int> OnTokensChange = new TokensChangeEvent();

	public int AvailableTokens
	{
		get
		{
			return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.casinoTokens;
		}
		set
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.casinoTokens = value;
			OnTokensChange.Invoke(value);
		}
	}

	public void BuyTokensPopup(int tokens, int price)
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.casinodotic_BuyTokensPrompt(tokens, price), () =>
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.TryTakeMoney(price))
			{
				AvailableTokens += tokens;
			}
			else
			{
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Failed, LOC.UI.Shop_NotEnoughMoney);
			}
		});
	}
}
