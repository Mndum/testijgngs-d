using System;
using System.Text;
using TMPro;

public static class GenericCompoundItemListAdapterExtensions
{
	public static CompoundItemList.CompoundItemListAdapter<TItem, StringAdvancedItemListItemPrefab>.IBuilderBeforeBuild WithDefaultListItemPool<TItem>(this CompoundItemList.CompoundItemListAdapter<TItem, StringAdvancedItemListItemPrefab>.IBuilderListItemPool poolStage)
	{
		return poolStage.WithListPool(UiOverlay.Instance.stringAdvancedItemListPrefab.GetPool());
	}

	public static CompoundItemList.CompoundItemListAdapter<TItem, TListItemPrefab>.IBuilderListItemPool WithDefaultSetItem<TItem, TListItemPrefab>(this CompoundItemList.CompoundItemListAdapter<TItem, TListItemPrefab>.IBuilderSetItem setItemStage, Func<TListItemPrefab, TextMeshProUGUI> tmpGetter) where TListItemPrefab : AdvancedItemListPrefab
	{
		return setItemStage.WithSetItem((TItem item, TListItemPrefab display, CompoundItemList.CompoundItemListAdapter<TItem, TListItemPrefab> adapter) =>
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				adapter.AppendNameOfItem(item, stringBuilder);
				if (adapter.ReferenceSelection != null)
				{
					adapter.ReferenceSelection.DescribeItemSelection(stringBuilder, item);
				}
				tmpGetter(display).SetText(stringBuilder);
			}
		});
	}

	public static CompoundItemList.CompoundItemListAdapter<TItem, StringAdvancedItemListItemPrefab>.IBuilderListItemPool WithDefaultStringAdvancedPrefabSetItem<TItem>(this CompoundItemList.CompoundItemListAdapter<TItem, StringAdvancedItemListItemPrefab>.IBuilderSetItem setItemStage)
	{
		return setItemStage.WithDefaultSetItem((StringAdvancedItemListItemPrefab x) => x.VisibleText);
	}

	public static CompoundItemList.CompoundItemListAdapter<TItem, TListItemPrefab>.IBuilderFolders WithAlphabeticalSortingOnly<TItem, TListItemPrefab>(this CompoundItemList.CompoundItemListAdapter<TItem, TListItemPrefab>.IBuilderSortingStage sortingStage) where TListItemPrefab : AdvancedItemListPrefab
	{
		StringRepresentationHelper<TItem> stringRepresentationHelper = sortingStage.StringRepresentationHelper;
		return sortingStage.WithSortTypes(new SortType<TItem>[2]
		{
			new SortType<TItem>(() => LOC.UI.Equipment_SortAlphabetically, stringRepresentationHelper),
			new SortType<TItem>(() => LOC.UI.Equipment_SortNoSort, null)
		});
	}

	public static CompoundItemList.CompoundItemListAdapter<TItem, StringAdvancedItemListItemPrefab>.IBuilderBeforeBuild WithBasicToStringEverything<TItem>(this CompoundItemList.CompoundItemListAdapter<TItem, StringAdvancedItemListItemPrefab>.IBuilderSortingStage sortingStage)
	{
		return sortingStage.WithAlphabeticalSortingOnly().WithAllFolder().WithDefaultStringAdvancedPrefabSetItem()
			.WithDefaultListItemPool();
	}
}
