using System.Text;

public static class GameItemCompoundListAdapterExtensions
{
	public static StringRepresentationHelper<Item> _itemStringRepresentationHelper = StringRepresentationHelper<Item>.Builder().WithGetNameFunc((Item item, StringRepresentationHelperEnum type) => type switch
	{
		StringRepresentationHelperEnum.Full => item.GetName(), 
		StringRepresentationHelperEnum.Description => item.GetDescription(), 
		_ => item.GetName(ItemNameIncludeEnum.DefaultNoCount), 
	}).WithAppendNameAction((Item item, StringBuilder sb, StringRepresentationHelperEnum type) =>
	{
		switch (type)
		{
		default:
			item.GetNameStringBuilder(sb, ItemNameIncludeEnum.DefaultNoCount);
			break;
		case StringRepresentationHelperEnum.Full:
			item.GetNameStringBuilder(sb);
			break;
		case StringRepresentationHelperEnum.Description:
			item.GetDescriptionStringBuilder(sb);
			break;
		}
	})
		.WithDefaultSearch()
		.Build();

	public static CompoundItemList.CompoundItemListAdapter<TItem, GameItemContainer>.IBuilderBeforeBuild WithDefaultGameItemListItemPool<TItem>(this CompoundItemList.CompoundItemListAdapter<TItem, GameItemContainer>.IBuilderListItemPool poolStage)
	{
		return poolStage.WithListPool(UiOverlay.Instance.gameItemContainerPrefab.GetPool());
	}

	public static CompoundItemList.CompoundItemListAdapter<Item, TListItemPrefab>.IBuilderFolders WithDefaultGameItemSorting<TListItemPrefab>(this CompoundItemList.CompoundItemListAdapter<Item, TListItemPrefab>.IBuilderSortingStage sortingStage) where TListItemPrefab : AdvancedItemListPrefab
	{
		return sortingStage.WithSortTypes(new SortType<Item>[6]
		{
			new SortType<Item>(() => LOC.UI.Equipment_SortNoSort, null),
			new SortType<Item>(() => LOC.UI.Equipment_SortQuality + LOC.UI.Equipment_SortDescending, ItemSortQualityDescending.Comparer),
			new SortType<Item>(() => LOC.UI.Equipment_SortPrice + LOC.UI.Equipment_SortDescending, ItemSortPriceDescending.Comparer),
			new SortType<Item>(() => LOC.UI.Equipment_SortSlot, ItemSortSlot.Comparer),
			new SortType<Item>(() => LOC.UI.Equipment_SortColor, ItemSortColor.Comparer),
			new SortType<Item>(() => LOC.UI.Equipment_SortAlphabetically, ItemSortAlphabetically.Comparer)
		});
	}

	public static CompoundItemList.CompoundItemListAdapter<Item, TListItemPrefab>.IBuilderSortingStage WithDefaultGameItemName<TListItemPrefab>(this CompoundItemList.CompoundItemListAdapter<Item, TListItemPrefab>.IBuilderNames nameStage) where TListItemPrefab : AdvancedItemListPrefab
	{
		return nameStage.WithStringRepresentationHelper(_itemStringRepresentationHelper);
	}

	public static CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer>.IBuilderListItemPool WithDefaultGameItemSetItem(this CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer>.IBuilderSetItem setItemStage)
	{
		return setItemStage.WithSetItem((Item item, GameItemContainer display, CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> adapter) =>
		{
			display.SetItem(item, adapter.ReferenceSelection);
		});
	}
}
