using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FisherSellFishPopup : PrefabPopup<FisherSellFishPopup>
{
	[SerializeField]
	private CompoundItemList _gameItemList;

	private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

	private AdvancedItemList.AdvancedItemListAdapter<Item, GameItemContainer> _gameItemListAdapter;

	[SerializeField]
	private ItemStats itemStats;

	[SerializeField]
	private ButtonList itemStatsButtonList;

	private Item.ItemManager _itemManager;

	private Action _afterClose;

	private void SwitchDisplayedItem(Item item, int i)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (itemStatsButtonList.DoInitialization(out modificationPeriod))
		{
			itemStats.SetItem(item);
			if (item != null)
			{
				ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
				buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Fisher_SellFish);
				buttonListWrapper.OnClick = SellButtonClicked;
				ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton();
				buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Fisher_SellAllFish);
				buttonListWrapper2.OnClick = SellAllButtonClicked;
			}
		}
	}

	private void SellAllButtonClicked()
	{
		List<Item> list = MyCollectionPool<Item>.GetList();
		list.AddRange(from x in GameVariables.Current.itemManager.GetAllFishes()
			where !x.IsFavourite
			select x);
		if (list.Count > 0)
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Fisher_SellAllFishPrompt(list.Count, list.Sum((Item x) => x.GetPrice())), () =>
			{
				Item.ItemManager itemManager = GameVariables.Current.itemManager;
				using (itemManager.DelayCallbacks("Selling fish"))
				{
					foreach (Item item in list)
					{
						GameVariables.Current.AddMoney(item.GetPrice());
						itemManager.RemoveItem(item);
					}
					R.Sound.Money_All.PlayRandomSlightlyRandomVolumeAndPitch();
					MyCollectionPool<Item>.Release(list);
				}
			});
		}
		else
		{
			MyCollectionPool<Item>.Release(list);
		}
	}

	private void SellButtonClicked()
	{
		Item currentItem = itemStats.CurrentItem;
		if (currentItem != null)
		{
			R.Sound.Money_All.PlayRandomSlightlyRandomVolumeAndPitch();
			GameVariables.Current.AddMoney(currentItem.GetPrice());
			GameVariables.Current.itemManager.RemoveItem(currentItem);
		}
	}

	public void Open(Action onFinished)
	{
		_afterClose = onFinished;
		List<Folder<Item>> list = MyCollectionPool<Folder<Item>>.GetList();
		list.Add(Folder<Item>.AllFolder);
		list.Add(new Folder<Item>(() => LOC.UI.Item_Favourite, (Item item) => item.IsFavourite));
		using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
			.WithFolders(list)
			.WithDefaultGameItemSetItem()
			.WithDefaultGameItemListItemPool()
			.BuildAndPrepare(_gameItemList, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.GetAllFishes()), out _compoundGameItemListAdapter, out _gameItemListAdapter))
		{
			_gameItemListAdapter.UpdateItemAction = (Item item, GameItemContainer display) =>
			{
				display.SetItem(item);
				display.SetFocusOnClick();
			};
			_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
		}
		_itemManager = GameVariables.Current.itemManager;
		ItemStats itemStats = this.itemStats;
		itemStats.CurrentItemInternallyChanged = (Action<Item>)Delegate.Combine(itemStats.CurrentItemInternallyChanged, new Action<Item>(_itemManager.ReportChangeInItemData));
		_itemManager.OnItemsModified += _compoundGameItemListAdapter.UpdateAll;
		base.Open();
	}

	public override bool CloseInner()
	{
		ItemStats itemStats = this.itemStats;
		itemStats.CurrentItemInternallyChanged = (Action<Item>)Delegate.Remove(itemStats.CurrentItemInternallyChanged, new Action<Item>(_itemManager.ReportChangeInItemData));
		_itemManager.OnItemsModified -= _compoundGameItemListAdapter.UpdateAll;
		base.CloseInner();
		_afterClose?.Invoke();
		return true;
	}
}
