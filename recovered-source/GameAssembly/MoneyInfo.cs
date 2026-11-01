using TMPro;
using UnityEngine;

public class MoneyInfo : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _moneyText;

	private GameStaticGui _gameStaticGui;

	public void UpdateText()
	{
		UpdateText(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.money);
	}

	private void UpdateText(int money)
	{
		string arg = ((money < 0) ? ("$<color=\"red\">" + money + "</color>") : (money.ToString() ?? ""));
		_moneyText.text = LOC.UI.Shop_Money(arg);
	}

	public void OnEnable()
	{
		UpdateText();
		UiOverlay.Instance.GetStaticGUI<GameStaticGui>(out _gameStaticGui);
		_gameStaticGui.OnMoneyChanged += UpdateText;
	}

	public void OnDisable()
	{
		_gameStaticGui.OnMoneyChanged -= UpdateText;
	}
}
