public class ItemSortPriceDescending : ItemComparer
{
	public static readonly ItemSortPriceDescending Comparer = new ItemSortPriceDescending();

	private ItemSortPriceDescending()
	{
	}

	public override int CompareNotNull(Item x, Item y)
	{
		return y.GetPrice().CompareTo(x.GetPrice());
	}
}
