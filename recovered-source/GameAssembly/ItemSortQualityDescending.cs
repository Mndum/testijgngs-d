public class ItemSortQualityDescending : ItemComparer
{
	public static readonly ItemSortQualityDescending Comparer = new ItemSortQualityDescending();

	private ItemSortQualityDescending()
	{
	}

	public override int CompareNotNull(Item x, Item y)
	{
		ItemPrefab itemPrefab = x.GetItemPrefab();
		int num = y.GetItemPrefab().HasQuality.CompareTo(itemPrefab.HasQuality);
		if (num != 0)
		{
			return num;
		}
		return y.Quality.CompareTo(x.Quality);
	}
}
