public class ItemSortAlphabetically : ItemComparer
{
	public static readonly ItemSortAlphabetically Comparer = new ItemSortAlphabetically();

	private ItemSortAlphabetically()
	{
	}

	public override int CompareNotNull(Item x, Item y)
	{
		return string.Compare(x.GetItemPrefab().Name, y.GetItemPrefab().Name);
	}
}
