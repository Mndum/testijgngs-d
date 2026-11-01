public class ItemSortSlot : ItemComparer
{
	public static readonly ItemSortSlot Comparer = new ItemSortSlot();

	private ItemSortSlot()
	{
	}

	public override int CompareNotNull(Item x, Item y)
	{
		ItemPrefab itemPrefab = x.GetItemPrefab();
		ItemPrefab itemPrefab2 = y.GetItemPrefab();
		bool flag = itemPrefab.IsEquipable(out var slotEquipData);
		bool value = itemPrefab2.IsEquipable(out var slotEquipData2);
		int num = flag.CompareTo(value);
		if (num != 0)
		{
			return num;
		}
		if (!flag)
		{
			return 0;
		}
		return ItemSlot.IdComparer.Compare(slotEquipData.TargetSlot, slotEquipData2.TargetSlot);
	}
}
