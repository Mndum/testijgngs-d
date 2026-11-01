using System;
using System.Text;
using UnityEngine;

public static class ChooseItemsComplexPopupExtensions
{
	public static ChooseItemsComplexPopup.ChooseItemsAdapter<TData, TPrefab, TDisplay>.IBuilderOutsideSelection ToChooseItemsComplexPopupBuilder<TData, TPrefab, TDisplay>(this CompoundItemList.CompoundItemListAdapter<TData, TPrefab>.IBuilderBeforePrepare builder, PrefabPool<TDisplay> itemDataDisplayPool, Action<ChooseItemsComplexPopup.ChooseItemsAdapter<TData, TPrefab, TDisplay>, TData, TDisplay> showDataOnDisplay) where TPrefab : AdvancedItemListPrefab where TDisplay : MonoBehaviour, IPooledPrefab
	{
		return ChooseItemsComplexPopup.ChooseItemsAdapter<TData, TPrefab, TDisplay>.Builder.Start(builder, itemDataDisplayPool, showDataOnDisplay);
	}

	public static ChooseItemsComplexPopup.ChooseItemsAdapter<TData, TPrefab, StringGenericDataDisplay>.IBuilderOutsideSelection ToChooseItemsComplexPopupBuilderGenericString<TData, TPrefab>(this CompoundItemList.CompoundItemListAdapter<TData, TPrefab>.IBuilderBeforePrepare builder) where TPrefab : AdvancedItemListPrefab
	{
		return ChooseItemsComplexPopup.ChooseItemsAdapter<TData, TPrefab, StringGenericDataDisplay>.Builder.Start(builder, UiOverlay.Instance.stringGenericDataDisplay.GetPool(), (ChooseItemsComplexPopup.ChooseItemsAdapter<TData, TPrefab, StringGenericDataDisplay> adapter, TData data, StringGenericDataDisplay display) =>
		{
			StringBuilder stringBuilder;
			if (data != null)
			{
				using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
				{
					adapter.CompoundItemListAdapter.AppendNameOfItem(data, stringBuilder, StringRepresentationHelperEnum.Description);
					display.MainText.SetText(stringBuilder);
					return;
				}
			}
			display.MainText.SetText(LOC.UI.Equipment_NoItemSelected);
		});
	}

	public static ChooseItemsComplexPopup.ChooseItemsAdapter<Item, TPrefab, ItemStats>.IBuilderOutsideSelection ToChooseItemsComplexPopupBuilderItem<TPrefab>(this CompoundItemList.CompoundItemListAdapter<Item, TPrefab>.IBuilderBeforePrepare builder) where TPrefab : AdvancedItemListPrefab
	{
		return ChooseItemsComplexPopup.ChooseItemsAdapter<Item, TPrefab, ItemStats>.Builder.Start(builder, UiOverlay.Instance.itemStats.GetPool(), (ChooseItemsComplexPopup.ChooseItemsAdapter<Item, TPrefab, ItemStats> adapter, Item item, ItemStats display) =>
		{
			display.SetItem(item);
		});
	}

	public static ChooseItemsComplexPopup.ChooseItemsAdapter<ItemPrefab, TPrefab, ItemStats>.IBuilderOutsideSelection ToChooseItemsComplexPopupBuilderItemPrefab<TPrefab>(this CompoundItemList.CompoundItemListAdapter<ItemPrefab, TPrefab>.IBuilderBeforePrepare builder) where TPrefab : AdvancedItemListPrefab
	{
		return ChooseItemsComplexPopup.ChooseItemsAdapter<ItemPrefab, TPrefab, ItemStats>.Builder.Start(builder, UiOverlay.Instance.itemStats.GetPool(), (ChooseItemsComplexPopup.ChooseItemsAdapter<ItemPrefab, TPrefab, ItemStats> adapter, ItemPrefab item, ItemStats display) =>
		{
			display.SetPrefab(item);
		});
	}

	public static ChooseItemsComplexPopup.ChooseItemsAdapter<TDataSelectable, TPrefab, ItemStats>.IBuilderOutsideOnFinished WithItemCountsSelection<TDataSelectable, TPrefab, TDisplay>(this ChooseItemsComplexPopup.ChooseItemsAdapter<TDataSelectable, TPrefab, ItemStats>.IBuilderOutsideSelection builderOutsideSelection, Selection<TDataSelectable> alreadyClaimedSelection = null) where TDataSelectable : ISelectable where TPrefab : AdvancedItemListPrefab where TDisplay : MonoBehaviour, IPooledPrefab
	{
		return builderOutsideSelection.WithStartingSelection(SelectionFactory.CreateInstance<TDataSelectable>(), alreadyClaimedSelection);
	}

	public static ChooseItemsComplexPopup OpenPopup(this ChooseItemsComplexPopup.IBuilderAdapterReady adapter, string title, string text, bool allowClosing = true, bool showCancel = false)
	{
		return UiOverlay.Instance.ChooseItemsComplexPopup(title, text, adapter, allowClosing, showCancel);
	}
}
