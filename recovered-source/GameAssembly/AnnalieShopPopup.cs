using System;
using UnityEngine;

public class AnnalieShopPopup : PrefabPopup<AnnalieShopPopup>
{
	[SerializeField]
	private CompoundItemList _gameItemList;

	private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

	private AdvancedItemList.AdvancedItemListAdapterConvert<ShopItem, Item, GameItemContainer> _shopItemConvertAdapter;

	[SerializeField]
	private ItemStats itemStats;

	[SerializeField]
	private ButtonList itemStatsButtons;

	private Action _onFinished;

	private bool _isWithBot;

	private string ShopId => "clothier";

	private void SwitchDisplayedItem((ShopItem, Item) data, int i)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (itemStatsButtons.DoInitialization(out modificationPeriod))
		{
			var (shopItem, _) = data;
			if (shopItem != null)
			{
				Item item = shopItem.Item;
				itemStats.SetItem(item);
				ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
				buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Shop_Buy);
				buttonListWrapper.OnClick = BuyButtonClicked;
				if ((item.GetItemPrefab().EquipableByBot || item.CanBeUnpacked()) && _isWithBot && item.ItemLocation != Item.ItemLocationEnum.BorrowedFromShop)
				{
					ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(10, CommonButtonColorType.Green);
					buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Shop_TryOn);
					buttonListWrapper2.OnClick = TryButtonClicked;
				}
			}
			else
			{
				itemStats.SetItem(null);
			}
		}
	}

	private void BuyButtonClicked()
	{
		if (_shopItemConvertAdapter.TryGetCurrentlyFocusedItem(out ShopItem shopItem))
		{
			Shop shop = GameVariables.Current.ShopManager.GetShop(ShopId);
			shop.ShowBuyPopup(shopItem, 1, (int pricePaid) =>
			{
				Item item = shop.BuyItem(shopItem, 1, pricePaid);
				GameVariables.Current.itemManager.AddItem(item, Item.ItemLocationEnum.Carried);
				SyncAll();
			});
		}
	}

	private void TryButtonClicked()
	{
		if (_shopItemConvertAdapter.TryGetCurrentlyFocusedItem(out ShopItem shopItem))
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.TryItemPrompt(shopItem.Item.GetName()), () =>
			{
				GameVariables.Current.itemManager.BorrowItemFromShop(shopItem);
				Action onFinished = _onFinished;
				_onFinished = null;
				CloseFromUIOverlay();
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Outside_Clothier_ChangingRoom, onFinished);
			});
		}
	}

	private void SyncAll()
	{
		_compoundGameItemListAdapter.UpdateAll();
	}

	public void Open(Action onFinished, bool isWithBot)
	{
		_onFinished = onFinished;
		_isWithBot = isWithBot;
		using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
			.WithAllFolder()
			.WithDefaultGameItemSetItem()
			.WithDefaultGameItemListItemPool()
			.BuildAndPrepareConvert(_gameItemList, out _compoundGameItemListAdapter, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.ShopManager.GetItems("clothier")), (ShopItem si) => si.Item, out _shopItemConvertAdapter))
		{
			_shopItemConvertAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
			_compoundGameItemListAdapter.ItemRightClickedAction = (GameItemContainer item) =>
			{
				_shopItemConvertAdapter.Focus(item.Index);
				itemStatsButtons.ShowAsContextMenu();
			};
		}
		base.Open();
	}

	public override bool CloseInner()
	{
		base.CloseInner();
		_onFinished?.Invoke();
		return true;
	}
}
