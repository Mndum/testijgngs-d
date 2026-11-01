using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericShop : MonoBehaviour
{
	[SerializeField]
	private CompoundItemList gameItemList;

	private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

	[SerializeField]
	private ItemStats _itemStats;

	[SerializeField]
	private ButtonList itemStatsButtons;

	[SerializeField]
	private TextMeshProUGUI _shopNameText;

	[SerializeField]
	private PooledPrefabReference<GameItemContainer> overrideGameItemContainer;

	private string _shopId;

	private Shop shop;

	private AdvancedItemList.AdvancedItemListAdapterConvert<ShopItem, Item, GameItemContainer> _adapter;

	[NonSerialized]
	public Action OnDisableAction;

	[NonSerialized]
	public Action<ShopItem, int> OnNotEnoughMoney;

	[NonSerialized]
	public Action<ShopItem, int> BuyConfirmation;

	public ButtonList ItemRelatedButtons => itemStatsButtons;

	public string ShopId => _shopId;

	private static ShopManager ShopManager => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.ShopManager;

	public void SetShopName(string shopName)
	{
		if (_shopNameText != null)
		{
			_shopNameText.GetComponent<TextMeshProUGUI>().text = shopName;
		}
	}

	private void BuyConfirmationPopup(ShopItem shopItem, int count)
	{
		if (BuyConfirmation != null)
		{
			BuyConfirmation(shopItem, count);
			return;
		}
		shop.ShowBuyPopup(shopItem, count, (int pricePaid) =>
		{
			Item item = shop.BuyItem(shopItem, count, pricePaid);
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddItem(item);
			UpdateAll();
		});
	}

	private void NotEnoughMoneyPopup(ShopItem item, int count)
	{
		if (OnNotEnoughMoney != null)
		{
			OnNotEnoughMoney(item, count);
		}
		else
		{
			UiOverlay.Instance.OkPopup(LOC.UI.UI_Failed, LOC.UI.Shop_NotEnoughMoney, () =>
			{
			});
		}
	}

	private void PrepareButton(IModificationPeriodButtonList buttonList, ShopItem item, string text, int count)
	{
		ButtonListWrapper buttonListWrapper = buttonList.AddButton();
		buttonListWrapper.SetText(text);
		buttonListWrapper.OnClick = () =>
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.money >= item.Item.GetPrice() * count)
			{
				BuyConfirmationPopup(item, count);
			}
			else
			{
				NotEnoughMoneyPopup(item, count);
			}
		};
	}

	private void SwitchDisplayedItem((ShopItem, Item) data, int i)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (itemStatsButtons.DoInitialization(out modificationPeriod))
		{
			var (shopItem, _) = data;
			if (shopItem != null)
			{
				_itemStats.SetItem(shopItem.Item);
				PrepareButton(modificationPeriod, shopItem, shopItem.Item.IsStackable ? LOC.UI.Shop_BuyCount(1) : LOC.UI.Shop_Buy, 1);
				if (shopItem.Item.IsStackable)
				{
					PrepareButton(modificationPeriod, shopItem, LOC.UI.Shop_BuyCount(5), 5);
					PrepareButton(modificationPeriod, shopItem, LOC.UI.Shop_BuyCount(15), 15);
				}
			}
			else
			{
				_itemStats.SetItem(null);
			}
		}
	}

	public void OnDisable()
	{
		OnDisableAction?.Invoke();
		OnDisableAction = null;
	}

	public void UpdateAll()
	{
		_compoundGameItemListAdapter.UpdateAll();
	}

	public void Open(string shopId, List<Folder<Item>> folders = null)
	{
		_shopId = shopId;
		SetShopName(GameVariables.Current.ShopManager.TryGetShop(shopId, out shop) ? shop.Name : LOC.UI.Shop);
		if (folders == null)
		{
			folders = Folder<Item>.GetPooledWithAllFolder();
		}
		using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
			.WithFolders(folders)
			.WithDefaultGameItemSetItem()
			.WithDefaultGameItemListItemPool()
			.BuildAndPrepareConvert(gameItemList, out _compoundGameItemListAdapter, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.ShopManager.GetItems(_shopId)), (ShopItem item) => item.Item, out _adapter))
		{
			_adapter.OnFocusedItemChanged = SwitchDisplayedItem;
			_compoundGameItemListAdapter.ItemRightClickedAction = (GameItemContainer item) =>
			{
				_adapter.Focus(item.Index);
				itemStatsButtons.ShowAsContextMenu();
			};
		}
		OnDisableAction = null;
		BuyConfirmation = null;
		OnNotEnoughMoney = null;
	}
}
