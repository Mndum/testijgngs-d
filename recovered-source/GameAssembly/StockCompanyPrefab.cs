public class StockCompanyPrefab : PrefabPart<string>
{
	public string Name => Key;

	public float StartingPrice { get; private set; }

	public float Instability { get; private set; }

	public float MinPrice { get; private set; }

	public int Order { get; private set; }

	public StockCompanyPrefab(string name, float startingPrice, float instability, float minPrice, int order)
		: base(name)
	{
		StartingPrice = startingPrice;
		Instability = instability;
		MinPrice = minPrice;
		Order = order;
	}
}
