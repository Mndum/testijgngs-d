public class ItemSortItemPairings : ItemComparer
{
	public static readonly ItemSortItemPairings Comparer = new ItemSortItemPairings();

	private ItemSortItemPairings()
	{
	}

	public override int CompareNotNull(Item x, Item y)
	{
		int num = x.GameId.CompareTo(y.GameId);
		if (num != 0)
		{
			return num;
		}
		return ItemSortColor.CompareByColor(x, y);
	}
}
