public class OwnedStockInfo
{
	public int count;

	public int sum;

	public float AveragePrice => (float)sum * 1f / (float)count;

	public OwnedStockInfo()
	{
		count = 0;
		sum = 0;
	}

	public string GetInfo()
	{
		if (count == 0)
		{
			return LOC.UI.stonksdotic_NoStocks;
		}
		return LOC.UI.stonksdotic_StockSummary(count, sum, AveragePrice);
	}

	public string GetIncomeInfo(int currentPrice)
	{
		if (count != 0)
		{
			int num = currentPrice * count;
			int num2 = num - sum;
			return LOC.UI.stonksdotic_IncomeInfo(num, (num2 > 0) ? LOC.UI.stonksdotic_Income : LOC.UI.stonksdotic_Loss, StringUtils.TMPColorNumber(num2, negative: false, "0$"), StringUtils.TMPColorNumber((float)currentPrice - AveragePrice, negative: false, "0.##$"));
		}
		return "";
	}
}
