using System;
using System.Text;
using Cysharp.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStaticGui : StaticGuiBase
{
	private int _lastDayShown = -1;

	private static int _moneyMemory;

	[Header("Auto")]
	[SerializeField]
	private TextMeshProUGUI A_MoneyTextTextMeshProUGUI;

	[SerializeField]
	private TooltipDisplayArea A_MoneyTextTooltipDisplayArea;

	[SerializeField]
	private TextMeshProUGUI A_DayText;

	[SerializeField]
	private TextMeshProUGUI A_HourText;

	[SerializeField]
	private Slider A_Stamina_Bar;

	[SerializeField]
	private Slider A_Mental_Bar;

	[SerializeField]
	private Slider A_Hunger_Bar;

	public event Action<int> OnMoneyChanged;

	private void Awake()
	{
		TooltipDisplayArea staminaBarTooltip = A_Stamina_Bar.GetComponent<TooltipDisplayArea>();
		staminaBarTooltip.TextFunc = () => LOC.UI.UI_StaminaBarTooltipDynamic(Mathf.RoundToInt(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Stamina * 16f)).TMPColorSurround("green") + "\n" + staminaBarTooltip.tooltipText;
		TooltipDisplayArea hungerBarTooltip = A_Hunger_Bar.GetComponent<TooltipDisplayArea>();
		hungerBarTooltip.TextFunc = () => (GameVariables.Current.SatiationStatus() + "\n").TMPColorSurround("green") + hungerBarTooltip.tooltipText;
		A_MoneyTextTooltipDisplayArea.TextFunc = () => LOC.UI.UI_RentAmountInfo(GameVariables.Current.WeeklyRent);
	}

	private void OnDisable()
	{
		_lastDayShown = -1;
	}

	public void SyncGui(GameVariables gv)
	{
		int day = gv.Day;
		if (day != _lastDayShown)
		{
			A_DayText.text = LOC.UI.UI_Day(day);
			_lastDayShown = day;
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendFormat("{0:00}:{1:00}", gv.Hour, gv.Minute);
			A_HourText.SetText(stringBuilder);
		}
		A_Stamina_Bar.value = gv.Stamina;
		A_Mental_Bar.value = gv.MentalHealth;
		A_Hunger_Bar.value = gv.Satiation;
	}

	public void UpdateRentStatusText(GameVariables gv)
	{
		SetMoneyAndRentStatusText(gv.statusText);
	}

	public void SyncMoney(GameVariables gv)
	{
		int money = gv.money;
		if (money != _moneyMemory)
		{
			_moneyMemory = GlobalFunctions.MoveTowards(_moneyMemory, money, Mathf.Max(money / 10, _moneyMemory / 10), 10);
			SetMoneyAndRentStatusText(gv.statusText);
			OnMoneyChanged?.Invoke(_moneyMemory);
		}
	}

	private void SetMoneyAndRentStatusText(string status)
	{
		using Utf16ValueStringBuilder stringBuilder = ZString.CreateStringBuilder(notNested: true);
		stringBuilder.Append(_moneyMemory);
		stringBuilder.Append("$ ");
		stringBuilder.Append(status);
		A_MoneyTextTextMeshProUGUI.SetText(stringBuilder);
	}
}
