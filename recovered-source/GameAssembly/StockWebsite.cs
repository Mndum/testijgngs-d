using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StockWebsite : Website
{
	private TMP_Dropdown companyDropdown;

	private MyGraph graph;

	private TextMeshProUGUI companyInfo;

	private TextMeshProUGUI portfolioInfo;

	public Scrollbar buyScrollbar;

	public Button buyButton;

	private TextMeshProUGUI buyButtonTMP;

	public Scrollbar sellScrollbar;

	public Button sellButton;

	private TextMeshProUGUI sellButtonTMP;

	public Button sellAllButton;

	private TextMeshProUGUI sellAllButtonTMP;

	private bool _subscribedToOnChange;

	private int CurrentCompanyId => companyDropdown.value;

	private StockCompany CurrentCompany => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stockManager.StockCompanies[CurrentCompanyId];

	private int BuyScrollBarValue
	{
		get
		{
			int num = Mathf.CeilToInt(buyScrollbar.value * (float)buyScrollbar.numberOfSteps);
			if (num == 0)
			{
				return 1;
			}
			return num;
		}
	}

	private int SellScrollBarValue
	{
		get
		{
			int num = Mathf.CeilToInt(sellScrollbar.value * (float)sellScrollbar.numberOfSteps);
			if (num == 0)
			{
				return 1;
			}
			return num;
		}
	}

	private void SyncCompany()
	{
		List<StockCompany> stockCompanies = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stockManager.StockCompanies;
		StockCompany stockCompany = stockCompanies[CurrentCompanyId];
		LinkedList<float> priceMemory = stockCompany.PriceMemory;
		int num = GameVariables.LastMultiple(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time, 15);
		int num2 = 15 * priceMemory.Count;
		int firstTime = num - num2;
		graph.SetData(priceMemory, new MyGraph.GraphStyleContainer((float t) => GameVariables.TimeToHourString(Mathf.RoundToInt((float)firstTime + 15f * t)), (float t) => t.ToString("0.##") + "$"));
		int num3 = stockCompany.CurrentPriceRounded();
		OwnedStockInfo[] array = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stockManager.StockCompanies.Select((StockCompany t) => t.GetOwnedStockInfo()).ToArray();
		OwnedStockInfo ownedStockInfo = array[CurrentCompanyId];
		companyInfo.text = stockCompany.Name + "\n" + LOC.UI.stonksdotic_CurrentPrice(num3) + "\n" + ownedStockInfo.GetInfo() + "\n" + ownedStockInfo.GetIncomeInfo(num3);
		int num4 = 0;
		int num5 = 0;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(LOC.UI.stocksdotic_Portfolio);
			for (int num6 = 0; num6 < array.Length; num6++)
			{
				if (array[num6].count > 0)
				{
					num4 += array[num6].sum;
					int num7 = array[num6].count * stockCompanies[num6].CurrentPriceRounded();
					num5 += num7;
					stringBuilder.AppendLine(LOC.UI.stocksdotic_ShortInfo(array[num6].count, stockCompanies[num6].Name, num7));
				}
			}
			int number = num5 - num4;
			stringBuilder.AppendLine(string.Format("{0} {1}$-{2}$={3}\n", LOC.UI.stonksdotic_Income, num5, num4, StringUtils.TMPColorNumber(number, negative: false, "0$")));
			stringBuilder.TMPAppendSubHeaderSizeTag();
			stringBuilder.AppendLine(LOC.UI.stocksdotic_OwnershipLimitInfo(25000));
			stringBuilder.TMPAppendCloseSizeTag();
			if (num5 != 0)
			{
				portfolioInfo.SetText(stringBuilder);
			}
			else
			{
				portfolioInfo.text = LOC.UI.stocksdotic_PortfolioEmpty;
			}
		}
		stockCompany.CalculatePossibleToBuyCount(GameVariables.Current.money, out var _, out var possibleToBuyGeneral);
		int count = ownedStockInfo.count;
		int num8 = 2500;
		sellScrollbar.numberOfSteps = ((count < num8) ? count : num8);
		if (count <= 1)
		{
			sellScrollbar.gameObject.SetActive(value: false);
			Scrollbar scrollbar = sellScrollbar;
			float size = (sellScrollbar.value = 1f);
			scrollbar.size = size;
		}
		else
		{
			sellScrollbar.gameObject.SetActive(value: true);
			sellScrollbar.size = 1f / (float)sellScrollbar.numberOfSteps;
		}
		buyScrollbar.numberOfSteps = Mathf.Min(possibleToBuyGeneral, num8);
		if (possibleToBuyGeneral <= 1)
		{
			buyScrollbar.gameObject.SetActive(value: false);
			Scrollbar scrollbar2 = buyScrollbar;
			float size = (buyScrollbar.value = 1f);
			scrollbar2.size = size;
		}
		else
		{
			buyScrollbar.gameObject.SetActive(value: true);
			buyScrollbar.size = 1f / (float)buyScrollbar.numberOfSteps;
		}
		SyncSellButtons(ownedStockInfo, stockCompany, num3);
		SyncBuyButton(stockCompany, num3);
	}

	private void BuyScrollBarValueChanged(float value)
	{
		SyncBuyButton();
	}

	private void SellScrollBarValueChanged(float value)
	{
		SyncSellButtons();
	}

	private void SyncSellButtons()
	{
		StockCompany currentCompany = CurrentCompany;
		SyncSellButtons(currentCompany.GetOwnedStockInfo(), currentCompany, currentCompany.CurrentPriceRounded());
	}

	private void SyncSellButtons(OwnedStockInfo currentCompanyStockInfo, StockCompany currentCompany, int stockPrice)
	{
		int sellScrollBarValue = SellScrollBarValue;
		sellButtonTMP.text = LOC.UI.stocksdotic_SellButton(sellScrollBarValue, stockPrice * sellScrollBarValue);
		sellAllButtonTMP.text = LOC.UI.stocksdotic_SellAllButton(currentCompanyStockInfo.count * currentCompany.CurrentPriceRounded());
		sellButton.interactable = currentCompanyStockInfo.count > 0;
		sellAllButton.interactable = currentCompanyStockInfo.count > 0;
	}

	private void SyncBuyButton()
	{
		StockCompany currentCompany = CurrentCompany;
		SyncBuyButton(currentCompany, currentCompany.CurrentPriceRounded());
	}

	private void SyncBuyButton(StockCompany currentCompany, int stockPrice)
	{
		currentCompany.CalculatePossibleToBuyCount(GameVariables.Current.money, out var possibleToBuyMoney, out var possibleToBuyGeneral);
		if (possibleToBuyGeneral > 0)
		{
			int buyScrollBarValue = BuyScrollBarValue;
			buyButtonTMP.text = LOC.UI.stocksdotic_BuyButton(buyScrollBarValue, stockPrice * buyScrollBarValue);
			buyButton.interactable = true;
		}
		else if (possibleToBuyMoney > 0)
		{
			buyButtonTMP.text = LOC.UI.stocksdotic_OwnershipLimitReached;
			buyButton.interactable = false;
		}
		else
		{
			int buyScrollBarValue2 = BuyScrollBarValue;
			buyButtonTMP.text = LOC.UI.stocksdotic_BuyButton(buyScrollBarValue2, stockPrice * buyScrollBarValue2);
			buyButton.interactable = false;
		}
	}

	private void BuyButtonClicked()
	{
		int buyScrollBarValue = BuyScrollBarValue;
		CurrentCompany.BuyStock(buyScrollBarValue);
		SyncAll();
	}

	private void SellButtonClicked()
	{
		int sellScrollBarValue = SellScrollBarValue;
		CurrentCompany.SellStock(sellScrollBarValue);
		SyncAll();
	}

	private void SellAllButtonClicked()
	{
		CurrentCompany.SellAllStock();
		SyncAll();
	}

	private void CreateAll()
	{
		graph = GetComponentInChildren<MyGraph>();
		companyDropdown = GetComponentInChildren<TMP_Dropdown>();
		companyInfo = transform.Find("Company Info").GetComponent<TextMeshProUGUI>();
		portfolioInfo = transform.Find("Portfolio Info").GetComponent<TextMeshProUGUI>();
		buyButtonTMP = buyButton.GetComponentInChildren<TextMeshProUGUI>();
		sellButtonTMP = sellButton.GetComponentInChildren<TextMeshProUGUI>();
		sellAllButtonTMP = sellAllButton.GetComponentInChildren<TextMeshProUGUI>();
		companyDropdown.options = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stockManager.StockCompanies.Select((StockCompany t) => new TMP_Dropdown.OptionData(t.Name)).ToList();
		companyDropdown.value = 0;
		companyDropdown.onValueChanged.RemoveAllListeners();
		companyDropdown.onValueChanged.AddListener((int t) =>
		{
			SyncCompany();
		});
		SyncCompany();
		if (!_subscribedToOnChange)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stockManager.OnSync += SyncAll;
			_subscribedToOnChange = true;
		}
		buyButton.onClick.RemoveAllListeners();
		buyButton.onClick.AddListener(BuyButtonClicked);
		buyScrollbar.onValueChanged.RemoveAllListeners();
		buyScrollbar.onValueChanged.AddListener(BuyScrollBarValueChanged);
		sellButton.onClick.RemoveAllListeners();
		sellButton.onClick.AddListener(SellButtonClicked);
		sellScrollbar.onValueChanged.RemoveAllListeners();
		sellScrollbar.onValueChanged.AddListener(SellScrollBarValueChanged);
		sellAllButton.onClick.RemoveAllListeners();
		sellAllButton.onClick.AddListener(SellAllButtonClicked);
		SyncMoney();
	}

	private void SyncMoney()
	{
		transform.Find("Money").GetComponent<TextMeshProUGUI>().text = LOC.UI.Shop_Money(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.money);
	}

	private void SyncAll()
	{
		SyncCompany();
		SyncMoney();
	}

	public void OnDestroy()
	{
		if (_subscribedToOnChange)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.stockManager.OnSync -= SyncAll;
		}
	}

	public override IEnumerator Open()
	{
		CreateAll();
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
