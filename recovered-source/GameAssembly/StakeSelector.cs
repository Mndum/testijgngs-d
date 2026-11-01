using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StakeSelector : MonoBehaviour
{
	public int minStake = 5;

	public int maxStake = 1000;

	private bool _lockedIn;

	private CasinoWebsiteCore _casinoWebsiteCore;

	private Slider _slider;

	private TextMeshProUGUI _stakeInfo;

	public bool LockedIn
	{
		get
		{
			return _lockedIn;
		}
		set
		{
			_lockedIn = value;
			_slider.interactable = !value;
		}
	}

	private void OnStakeChange(float value)
	{
		_stakeInfo.text = LOC.UI.casinodotic_Stake(Mathf.RoundToInt(_slider.value));
	}

	public void UpdateValues(int currentTokens)
	{
		int num = Mathf.Min(maxStake, currentTokens);
		if (minStake > num && num <= 0)
		{
			Slider slider = _slider;
			float minValue = (_slider.maxValue = 0f);
			slider.minValue = minValue;
			_stakeInfo.text = LOC.UI.casinodotic_NotEnoughTokens + "\n" + LOC.UI.casinodotic_MinimalStake(minStake);
			_slider.interactable = false;
		}
		else
		{
			_slider.minValue = minStake;
			_slider.maxValue = num;
			_slider.interactable = !_lockedIn;
		}
	}

	public int GetStake()
	{
		return Mathf.RoundToInt(_slider.value);
	}

	private void OnEnable()
	{
		_casinoWebsiteCore.OnTokensChange.AddListener(UpdateValues);
	}

	private void OnDisable()
	{
		_casinoWebsiteCore.OnTokensChange.RemoveListener(UpdateValues);
	}

	private void Awake()
	{
		_casinoWebsiteCore = gameObject.GetAncestor<CasinoWebsiteCore>();
		_slider = GetComponentInChildren<Slider>();
		_stakeInfo = GetComponentInChildren<TextMeshProUGUI>();
		_slider.onValueChanged.AddListener(OnStakeChange);
		UpdateValues(_casinoWebsiteCore.AvailableTokens);
	}
}
