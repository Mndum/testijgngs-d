using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public static class StockCompanyPrefabManager
{
	public sealed class StockCompanyPrefabManagerData : PrefabManagerDataOneToOne<string, StockCompanyPrefab, StockCompany>
	{
		protected override StockCompanyPrefab GenerateEmpty(string key)
		{
			return new StockCompanyPrefab(key, 10f, 10f, 100f, 2147483647);
		}

		protected override StockCompany CreateSerializedPart(string key, StockCompanyPrefab prefab)
		{
			return new StockCompany(key, prefab);
		}
	}

	private static StockCompanyPrefabManagerData data = new StockCompanyPrefabManagerData();

	public static string oldIdCompany1 = "Incontinent Cell";

	public static string oldIdCompany2 = "FeetCoin";

	public static string oldIdCompany3 = "Cock Twitch";

	public static string oldIdCompany4 = "bang.ic";

	public static StockCompanyPrefab GetStockPrefab(string name)
	{
		return data.Get(name);
	}

	public static void EnsureCompaniesData(List<StockCompany> stockCompanies)
	{
		data.EnsureSerializedDataExistsAndHasPrefabs(stockCompanies);
		stockCompanies.Sort((StockCompany a, StockCompany b) => a.Prefab.Order.CompareTo(b.Prefab.Order));
	}

	[LoadableStaticMethod(null)]
	private static UniTask Load()
	{
		data.Init(new StockCompanyPrefab(oldIdCompany1, 5f, 7f, 2f, 0), new StockCompanyPrefab(oldIdCompany2, 169f, 20f, 5f, 10), new StockCompanyPrefab(oldIdCompany3, 50f, 100f, 1f, 20), new StockCompanyPrefab(oldIdCompany4, 5f, 5f, 15f, 30));
		return UniTask.CompletedTask;
	}
}
