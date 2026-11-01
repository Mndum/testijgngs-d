using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CasinoPayout : MonoBehaviour
{
	public int minPayout = 10;

	public int maxPayout = 1000;

	private CasinoWebsiteCore casinoWebsiteCore;

	private Slider slider;

	private Button payoutButton;

	private TextMeshProUGUI payoutInfo;

	private TextMeshProUGUI minPayoutInfo;

	private void OnStakeChange(float value)
	{
		int payoutTokens = GetPayoutTokens();
		int num = TokensToDollars(payoutTokens);
		payoutInfo.text = LOC.UI.casinodotic_Payout(payoutTokens, num);
	}

	public void UpdateValues(int currentTokens)
	{
		slider.minValue = minPayout;
		int num = Mathf.Min(maxPayout, currentTokens);
		slider.maxValue = num;
		if (minPayout > num)
		{
			payoutInfo.text = LOC.UI.casinodotic_NotEnoughTokens;
			slider.interactable = false;
			payoutButton.interactable = false;
		}
		else
		{
			slider.interactable = true;
			payoutButton.interactable = true;
		}
	}

	public int GetPayoutTokens()
	{
		return Mathf.RoundToInt(slider.value);
	}

	public int TokensToDollars(int tokens)
	{
		return tokens * 7 / 10;
	}

	private void OnEnable()
	{
		casinoWebsiteCore.OnTokensChange.AddListener(UpdateValues);
	}

	private void OnDisable()
	{
		casinoWebsiteCore.OnTokensChange.RemoveListener(UpdateValues);
	}

	private void PayoutButtonClicked()
	{
		int payoutTokens = GetPayoutTokens();
		if (payoutTokens > 0)
		{
			casinoWebsiteCore.AvailableTokens -= payoutTokens;
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddMoney(TokensToDollars(payoutTokens));
		}
	}

	private void Awake()
	{
		casinoWebsiteCore = gameObject.GetAncestor<CasinoWebsiteCore>();
		slider = GetComponentInChildren<Slider>();
		payoutButton = GetComponentInChildren<Button>();
		payoutButton.onClick.AddListener(PayoutButtonClicked);
		minPayoutInfo = GetComponentInChildren<TextMeshProUGUI>();
		minPayoutInfo.text = LOC.UI.casinodotic_MinimalPayout(minPayout);
		payoutInfo = payoutButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
		slider.onValueChanged.AddListener(OnStakeChange);
		UpdateValues(casinoWebsiteCore.AvailableTokens);
	}
}
