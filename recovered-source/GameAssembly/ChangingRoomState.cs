using System;
using System.Collections.Generic;
using System.Linq;

public class ChangingRoomState : GameStateWithLive2D<Live2DInteractionController>
{
	public enum ChangingRoomSection
	{
		NoSection,
		Equipment,
		Shop
	}

	private Action _onFinished;

	private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

	private ChangingRoomGui _changingRoomGui;

	private EquipmentSet _equipmentSetBefore;

	private ChangingRoomSection _section;

	private int _zoomLevel;

	private AdvancedItemList.IAdvancedItemListAdapter<Item> _itemListAdapter;

	public ChangingRoomState(Live2DInteractionController live2DController, Action onFinished)
		: base(live2DController)
	{
		_onFinished = onFinished;
	}

	protected override ModelBrainState GetTargetBrainState()
	{
		return new EquipmentBrainState();
	}

	private void ZoomButtonClicked()
	{
		_zoomLevel = (_zoomLevel + 1) % 4;
		_changingRoomGui.FreeCamera.SetZoomLevel(_zoomLevel);
	}

	private void Exit()
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
		_onFinished();
	}

	private void SwitchSection(ChangingRoomSection section)
	{
		if (section == _section || section == ChangingRoomSection.NoSection)
		{
			return;
		}
		_section = section;
		List<Folder<Item>> list = MyCollectionPool<Folder<Item>>.GetList();
		ChangingRoomSection section2 = _section;
		IDisposable disposable;
		if (section2 == ChangingRoomSection.Equipment || section2 != ChangingRoomSection.Shop)
		{
			_changingRoomGui.MoneyInfo.gameObject.SetActive(value: false);
			list.Add(new Folder<Item>(() => LOC.UI.Equipment_All, (Item x) => x.ItemLocation == Item.ItemLocationEnum.Carried || x.ItemLocation == Item.ItemLocationEnum.BorrowedFromShop));
			list.Add(new Folder<Item>(() => LOC.UI.ItemBorrowedShort, (Item x) => x.ItemLocation == Item.ItemLocationEnum.BorrowedFromShop));
			disposable = CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithFolders(list)
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.BuildAndPrepare(_changingRoomGui.ItemList, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.AllItemsEnumerable()), out _compoundGameItemListAdapter, out var adapter);
			adapter.OnFocusedItemChanged = SwitchEquipmentItem;
			_itemListAdapter = adapter;
		}
		else
		{
			_changingRoomGui.MoneyInfo.gameObject.SetActive(value: true);
			list.Add(Folder<Item>.AllFolder);
			List<ItemSlot> slots = new List<ItemSlot>
			{
				ItemSlot.SlotManager.UpperBody,
				ItemSlot.SlotManager.UpperBodyOuter,
				ItemSlot.SlotManager.LowerBody,
				ItemSlot.SlotManager.Panties
			};
			list.AddRange(slots.Select((ItemSlot s) => new Folder<Item>(() => s.FriendlyNameSls.GetValue(), (Item x) => x.CanGoInto(s), hideIfEmpty: true)));
			list.Add(new Folder<Item>(() => LOC.UI.Equipment_Other, (Item x) => !slots.Any(x.CanGoInto), hideIfEmpty: true));
			list.Add(new Folder<Item>(() => LOC.UI.ItemBorrowedShort, (Item x) => x.ItemLocation == Item.ItemLocationEnum.BorrowedFromShop, hideIfEmpty: true));
			disposable = CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithFolders(list)
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.BuildAndPrepareConvert(_changingRoomGui.ItemList, out _compoundGameItemListAdapter, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.ShopManager.GetItems("clothier")), (ShopItem x) => x.Item, out var convertAdapter);
			convertAdapter.OnFocusedItemChanged = SwitchShopItem;
			_itemListAdapter = convertAdapter;
		}
		_compoundGameItemListAdapter.ItemRightClickedAction = (GameItemContainer item) =>
		{
			_itemListAdapter.Focus(item.Index);
			_changingRoomGui.ItemActionButtonList.ShowAsContextMenu();
		};
		disposable.Dispose();
		UpdateSectionButtons();
	}

	private void UpdateSectionButtons()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (_changingRoomGui.SectionButtonList.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, (_section == ChangingRoomSection.Equipment) ? CommonButtonColorType.Green : CommonButtonColorType.Normal);
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Room_Equipment);
			buttonListWrapper.OnClick = () =>
			{
				SwitchSection(ChangingRoomSection.Equipment);
			};
			ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(10, (_section == ChangingRoomSection.Shop) ? CommonButtonColorType.Green : CommonButtonColorType.Normal);
			buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Shop);
			buttonListWrapper2.OnClick = () =>
			{
				SwitchSection(ChangingRoomSection.Shop);
			};
		}
	}

	private void UpdateSideButtons()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (_changingRoomGui.SideButtonList.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_Zoom);
			buttonListWrapper.OnClick = ZoomButtonClicked;
			ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(10);
			buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.ReturnToDefault);
			buttonListWrapper2.OnClick = () =>
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.ReturnToDefaultPrompt, () =>
				{
					Item.ItemManager itemManager = GameVariables.Current.itemManager;
					using (itemManager.DelayCallbacks("ReturnToDefault"))
					{
						itemManager.UnequipAllBotItems();
						itemManager.ReturnAllBorrowedItems("clothier");
						_equipmentSetBefore.Equip();
					}
				});
			};
			ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton(20);
			buttonListWrapper3.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Undress);
			buttonListWrapper3.OnClick = () =>
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.UndressPrompt, () =>
				{
					GameVariables.Current.itemManager.UnequipAllBotClothes();
				});
			};
		}
	}

	private void ReturnButtonShow(IModificationPeriodButtonList buttonList, Item item)
	{
		ButtonListWrapper buttonListWrapper = buttonList.AddButton(20, CommonButtonColorType.Red);
		buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.ReturnChangingRoomItem);
		buttonListWrapper.OnClick = () =>
		{
			GameVariables.Current.itemManager.ReturnItemToShop("clothier", item);
		};
	}

	private void EquipButtonShow(IModificationPeriodButtonList buttonList, Item item)
	{
		ButtonListWrapper buttonListWrapper = buttonList.AddButton(10);
		if (item.IsEquipped())
		{
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_ButtonUnequip);
			buttonListWrapper.OnClick = () =>
			{
				UiOverlay.Instance.UnequipItemOrAsk(item);
			};
		}
		else
		{
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_ButtonEquip);
			buttonListWrapper.OnClick = () =>
			{
				UiOverlay.Instance.EquipItem(item);
			};
		}
	}

	private void TryBorrowedItem(Item item)
	{
		Item.ItemManager itemManager = GameVariables.Current.itemManager;
		if (item.CanBeUnpacked())
		{
			using (itemManager.DelayCallbacks("Borrow"))
			{
				Item[] array = itemManager.UnpackItem(item);
				foreach (Item item2 in array)
				{
					UiOverlay.Instance.EquipItem(item2);
				}
				return;
			}
		}
		UiOverlay.Instance.EquipItem(item);
	}

	private void SwitchShopItem((ShopItem, Item) data, int i)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (_changingRoomGui.ItemActionButtonList.DoInitialization(out modificationPeriod))
		{
			var (shopItem, _) = data;
			if (shopItem != null)
			{
				Item item = data.Item2;
				if (item != null)
				{
					_changingRoomGui.ItemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.ShopItem;
					_changingRoomGui.ItemStats.SetItem(item);
					if (item.ItemLocation == Item.ItemLocationEnum.BorrowedFromShop)
					{
						ReturnButtonShow(modificationPeriod, item);
					}
					ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
					buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Shop_Buy);
					buttonListWrapper.OnClick = () =>
					{
						Shop shop = GameVariables.Current.ShopManager.GetShop("clothier");
						shop.ShowBuyPopup(shopItem, 1, (int pricePaid) =>
						{
							GameVariables.Current.itemManager.ReturnItemToShop("clothier", shopItem.Item);
							Item item2 = shop.BuyItem(shopItem, 1, pricePaid);
							GameVariables.Current.itemManager.AddItem(item2, Item.ItemLocationEnum.Carried);
						});
					};
					if (item.GetItemPrefab().Equipable && item.ItemLocation == Item.ItemLocationEnum.BorrowedFromShop)
					{
						EquipButtonShow(modificationPeriod, item);
					}
					if ((item.GetItemPrefab().EquipableByBot || item.CanBeUnpacked()) && item.ItemLocation != Item.ItemLocationEnum.BorrowedFromShop)
					{
						ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(10, CommonButtonColorType.Green);
						buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Shop_TryOn);
						buttonListWrapper2.OnClick = () =>
						{
							GameVariables.Current.itemManager.BorrowItemFromShop(shopItem);
							TryBorrowedItem(shopItem.Item);
						};
					}
					return;
				}
			}
			_changingRoomGui.ItemStats.SetItem(null);
		}
	}

	private void SwitchEquipmentItem(Item item, int i)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (_changingRoomGui.ItemActionButtonList.DoInitialization(out modificationPeriod))
		{
			if (item != null)
			{
				_changingRoomGui.ItemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.PlayerInventory;
				_changingRoomGui.ItemStats.SetItem(item);
				if (item.ItemLocation == Item.ItemLocationEnum.BorrowedFromShop)
				{
					ReturnButtonShow(modificationPeriod, item);
				}
				if (item.GetItemPrefab().Equipable)
				{
					EquipButtonShow(modificationPeriod, item);
				}
			}
			else
			{
				_changingRoomGui.ItemStats.SetItem(null);
			}
		}
	}

	private void UpdateItems()
	{
		_itemListAdapter.UpdateAll();
	}

	public override void EnterState()
	{
		GetAndEnableStaticGui<ChangingRoomGui>(out _changingRoomGui);
		_equipmentSetBefore = EquipmentSet.GenerateFromCurrent("Before changing room");
		Item.ItemManager itemManager = GameVariables.Current.itemManager;
		itemManager.MoveBotEquippedToCarried();
		Item item = GameVariables.Current.itemManager.AllItemsReadonlyCollection().FirstOrDefault();
		if (item != null && item.ItemLocation == Item.ItemLocationEnum.BorrowedFromShop)
		{
			TryBorrowedItem(item);
		}
		SwitchSection(ChangingRoomSection.Equipment);
		itemManager.OnItemsModified += UpdateItems;
		_changingRoomGui.ReturnButton.onClick.SetListener(() =>
		{
			GameVariables.Current.itemManager.UnequipAllBotItems();
			int num = GameVariables.Current.itemManager.ReturnAllBorrowedItems("clothier");
			_equipmentSetBefore.Equip();
			if (num > 0)
			{
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Done, LOC.UI.ItemsReturnedPrompt(num), Exit);
			}
			else
			{
				Exit();
			}
		});
		UpdateSideButtons();
		UpdateSectionButtons();
		_changingRoomGui.FreeCamera.InitBounds(SingletonMonoBehaviour<ViewSingleton>.Instance.ChangingRoomView);
	}

	public override void UpdateState()
	{
		if (!UiOverlay.Instance.IsOpen)
		{
			_changingRoomGui.FreeCamera.UpdatePosition();
		}
	}

	public override void FixedUpdateState()
	{
	}

	public override void ExitState()
	{
		GameVariables.Current.itemManager.OnItemsModified -= UpdateItems;
		_changingRoomGui.SectionButtonList.ResetButtons();
		_changingRoomGui.SideButtonList.ResetButtons();
	}
}
