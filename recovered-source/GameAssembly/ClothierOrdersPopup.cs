using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothierOrdersPopup : PrefabPopup<ClothierOrdersPopup>
{
	[SerializeField]
	private CompoundItemList _gameItemList;

	private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

	private AdvancedItemList.AdvancedItemListAdapterConvert<ItemOrder, Item, GameItemContainer> _orderListAdapter;

	[SerializeField]
	private Button _newOrderButton;

	[SerializeField]
	private ItemStats itemStats;

	[SerializeField]
	private ButtonList itemStatsButtons;

	[SerializeField]
	private TextMeshProUGUI _deliveryInfoText;

	private static int PriceMulti = 5;

	private Action _afterClose;

	private void SwitchDisplayedOrder((ItemOrder, Item) data, int index)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (itemStatsButtons.DoInitialization(out modificationPeriod))
		{
			var (order, _) = data;
			if (order != null)
			{
				itemStats.priceMulti = PriceMulti;
				switch (order.GetStatusConsideringTime())
				{
				case ItemOrder.OrderStatus.InCreation:
				{
					itemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.CustomItemShop;
					ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton();
					buttonListWrapper2.OnClick = () =>
					{
						int hours = 18;
						int num = order.Item.GetPrice() * PriceMulti;
						UiOverlay.Instance.BuyPopup(LOC.UI.Shop_OrderFinish, num, () =>
						{
							if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.ShopManager.TryGetShop("clothier", out var shop))
							{
								shop.MoneySpent += order.Item.GetPrice() * PriceMulti;
							}
							order.CreateOrder(hours * 60);
							SyncAll();
						}, LOC.UI.Shop_OrderFinishPrompt(num, hours));
					};
					buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.UI_Finish);
					ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton(20, CommonButtonColorType.Red);
					buttonListWrapper3.OnClick = () =>
					{
						GameVariables.Current.itemManager.RemoveOrder(order);
						SyncAll();
					};
					buttonListWrapper3.SetTextFuncLocalizedString(LOC.UI.TupleReferences.UI_Cancel);
					_deliveryInfoText.text = LOC.UI.Shop_NotOrdered.TMPColorSurround(Color.red);
					break;
				}
				case ItemOrder.OrderStatus.Created:
					itemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.ShopItem;
					_deliveryInfoText.text = LOC.UI.Shop_Delivery(GlobalFunctions.TimePeriod(order.OrderFinishedTime - SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time)).TMPColorSurround(Color.yellow);
					break;
				case ItemOrder.OrderStatus.Done:
				{
					itemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.ShopItem;
					ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
					buttonListWrapper.OnClick = () =>
					{
						UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Shop_OrderCollectPrompt, () =>
						{
							if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.ShopManager.TryGetShop("clothier", out var shop))
							{
								shop.ItemsBought++;
							}
							GameVariables.Current.itemManager.RemoveOrder(order);
							SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddItem(order.Item, Item.ItemLocationEnum.Carried);
							SyncAll();
						});
					};
					buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Shop_Collect);
					_deliveryInfoText.text = LOC.UI.Shop_CanBeCollected.TMPColorSurround(Color.green);
					break;
				}
				default:
					itemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.ShopItem;
					break;
				}
				itemStats.SetItem(order.Item);
			}
			else
			{
				itemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.ShopItem;
				_deliveryInfoText.text = "";
				itemStats.SetItem(null);
			}
		}
	}

	private void NewOrderButtonClicked()
	{
		if (!GameVariables.Current.itemManager.ShopManager.TryGetShop("clothier", out var shop))
		{
			return;
		}
		CompoundItemList.StartBuilder<ItemPrefab, StringAdvancedItemListItemPrefab>().WithDefaultItemPrefabName().WithDefaultItemPrefabSorting()
			.WithDefaultItemPrefabFolders()
			.WithDefaultStringAdvancedPrefabSetItem()
			.WithDefaultListItemPool()
			.Build(shop.Prefab.SingleContainer.GetGameIdsWithCheck().Distinct().Select(ItemPrefab.ItemPrefabManager.GetItemById)
				.ToFrozenDataSource())
			.ToChooseItemsComplexPopupBuilderItemPrefab()
			.WithSingularStartingSelectionSingleItemAllowed()
			.WithSuccessfulOnlyOnFinished((Selection<ItemPrefab> selection) =>
			{
				if (selection.TryGetFirstItem(out var item) && item.TryGetItemGameId(out var gameId))
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddClothierOrder(new ItemOrder(Item.CreateRandomItem(gameId)));
					SyncAll();
				}
			})
			.OpenPopup(LOC.UI.UI_ChooseItems, LOC.UI.SelectItemsForANewOrderPrompt);
	}

	private void SyncAll()
	{
		_compoundGameItemListAdapter.UpdateAll();
	}

	public void Open(Action afterClose)
	{
		_afterClose = afterClose;
		_newOrderButton.onClick.SetListener(NewOrderButtonClicked);
		using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
			.WithAllFolder()
			.WithDefaultGameItemSetItem()
			.WithDefaultGameItemListItemPool()
			.BuildAndPrepareConvert(_gameItemList, out _compoundGameItemListAdapter, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.ClothierOrders), (ItemOrder order) => order.Item, out _orderListAdapter))
		{
			_orderListAdapter.UpdateItemAction = ((ItemOrder, Item) itemObject, GameItemContainer display) =>
			{
				var (itemOrder, _) = itemObject;
				if (itemOrder != null)
				{
					display.SetItem(itemOrder.Item);
					display.SetFocusOnClick();
				}
			};
			_orderListAdapter.OnFocusedItemChanged = SwitchDisplayedOrder;
			_compoundGameItemListAdapter.ItemRightClickedAction = (GameItemContainer item) =>
			{
				_orderListAdapter.Focus(item.Index);
				itemStatsButtons.ShowAsContextMenu();
			};
		}
		itemStats.CurrentItemInternallyChanged = GameVariables.Current.itemManager.ReportChangeInItemData;
		base.Open();
		SyncAll();
	}

	public override bool CloseInner()
	{
		itemStats.CurrentItemInternallyChanged = null;
		base.CloseInner();
		_afterClose?.Invoke();
		return true;
	}
}
