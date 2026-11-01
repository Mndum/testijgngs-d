using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EquipmentStaticGui : StaticGuiBase
{
	private abstract class EquipmentStaticGuiState : BasicStateMachineState<EquipmentStaticGuiState, EquipmentStaticGui>
	{
		public abstract EquipmentStaticGuiStateEnum GetStateEnum();

		public virtual void HelpButtonClicked()
		{
		}

		public virtual bool HasHelpButton()
		{
			return false;
		}
	}

	private class EquipmentState : EquipmentStaticGuiState
	{
		private EquipmentStaticGuiStateEnum _state;

		private ItemStats _itemStats;

		private Item.ItemManager _registeredItemManager;

		private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

		private AdvancedItemList.AdvancedItemListAdapter<Item, GameItemContainer> _gameItemListAdapter;

		public EquipmentState(EquipmentStaticGuiStateEnum state)
		{
			_state = state;
		}

		private void Prepare()
		{
			List<Folder<Item>> list = MyCollectionPool<Folder<Item>>.GetList();
			DataSource<Item> itemSource;
			if (_state == EquipmentStaticGuiStateEnum.Equipment)
			{
				itemSource = DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.AllItemsEnumerable().Where((Item x) =>
				{
					ItemPrefab itemPrefab = x.GetItemPrefab();
					return (itemPrefab.EquipableByBot && itemPrefab.Category != ItemCategory.Clothing) || itemPrefab.Category == ItemCategory.Package;
				}));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_All));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Legs, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.LegL, ItemSlot.SlotManager.LegR), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Arms, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.ArmL, ItemSlot.SlotManager.ArmR), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Hair, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot.Type == ItemSlot.SlotType.BotHair, hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Slot_Skin, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.Skin), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Slot_Ears, (Item x) => (x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.Ears)) || x.GetItemPrefab().SubCategory == ItemSubCategory.Modules_CatEars, hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Slot_Tail, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.Tail), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_BodySizeModules, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.BodySizeModuleSlots), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Slot_PersonalityModule, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.PersonalityModule), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Other, null, hideIfEmpty: false, remainingFolder: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Equipped, (Item x) => x.IsEquipped(), hideIfEmpty: false, remainingFolder: false, ignoreForRemainingFolder: true));
				if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.AllMods.Count > 0)
				{
					list.Add(new Folder<Item>(() => LOC.UI.Equipment_Modded, (Item x) => !x.GameId.IsVanilla, hideIfEmpty: true, remainingFolder: false, ignoreForRemainingFolder: true));
				}
				list.Add(new Folder<Item>(() => LOC.UI.Item_Favourite, (Item x) => x.IsFavourite, hideIfEmpty: true, remainingFolder: false, ignoreForRemainingFolder: true));
			}
			else
			{
				itemSource = DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.AllItemsEnumerable().Where((Item x) =>
				{
					ItemPrefab itemPrefab = x.GetItemPrefab();
					return (itemPrefab.EquipableByBot && itemPrefab.Category == ItemCategory.Clothing) || itemPrefab.Category == ItemCategory.Package;
				}));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_All));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_LowerBody, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.LowerBody), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_UpperBody, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.UpperBody), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Outer, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.UpperBodyOuter), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Slot_Bra, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.Bra), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Slot_Panties, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.Panties), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Socks, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.StockingL, ItemSlot.SlotManager.StockingR), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Shoes, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.ShoeL, ItemSlot.SlotManager.ShoeR), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Slot_Glasses, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.Glasses), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Neck, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.CanGoInto(ItemSlot.SlotManager.NeckAccessory), hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_MakeUp, (Item x) => x.GetItemPrefab().SubCategory == ItemSubCategory.Clothing_MakeUp, hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Piercing, (Item x) => x.GetItemPrefab().SubCategory == ItemSubCategory.Clothing_Piercing, hideIfEmpty: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Other, null, hideIfEmpty: false, remainingFolder: true));
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Equipped, (Item x) => x.IsEquipped(), hideIfEmpty: false, remainingFolder: false, ignoreForRemainingFolder: true));
				if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.AllMods.Count > 0)
				{
					list.Add(new Folder<Item>(() => LOC.UI.Equipment_Modded, (Item x) => !x.GameId.IsVanilla, hideIfEmpty: true, remainingFolder: false, ignoreForRemainingFolder: true));
				}
				list.Add(new Folder<Item>(() => LOC.UI.Item_Favourite, (Item x) => x.IsFavourite, hideIfEmpty: true, remainingFolder: false, ignoreForRemainingFolder: true));
			}
			list.Add(new Folder<Item>(() => LOC.UI.Equipment_Package, (Item x) => x.GetItemPrefab().Category == ItemCategory.Package, hideIfEmpty: true));
			EquippedItemRequirement.AddRequiredFolderIfContractNeedsAnyItem(list);
			using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithFolders(list)
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.BuildAndPrepare(Owner.A_ContentItems, itemSource, out _compoundGameItemListAdapter, out _gameItemListAdapter))
			{
				_compoundGameItemListAdapter.ItemsMovedAction = (GameItemContainer target, Item source, int index) =>
				{
					GameVariables.Current.itemManager.MoveItemToOtherItemsPosition(target.Current, source);
				};
				_compoundGameItemListAdapter.ItemRightClickedAction = (GameItemContainer item) =>
				{
					_gameItemListAdapter.Focus(item.Index);
					Owner._itemStatsButtonList.ShowAsContextMenu();
				};
				_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
			}
		}

		private void SwitchDisplayedItem(Item item, int i)
		{
			IModificationPeriodButtonList modificationPeriod;
			using (Owner._itemStatsButtonList.DoInitialization(out modificationPeriod))
			{
				if (item != null)
				{
					_itemStats.SetItem(item);
					item.FillUIButtons(modificationPeriod);
				}
				else
				{
					_itemStats.SetItem(null);
				}
			}
		}

		public override void EnterState()
		{
			Owner.SetMainLayout(listOnly: false);
			Owner.A_Equipment_Sets.gameObject.SetActive(value: true);
			_itemStats = UiOverlay.Instance.itemStats.GetFromPool(Owner.A_ItemDescriptionArea);
			_itemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.PlayerInventory;
			_registeredItemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			Prepare();
			_registeredItemManager.OnItemsModified += _compoundGameItemListAdapter.UpdateAll;
			_itemStats.CurrentItemInternallyChanged = GameVariables.Current.itemManager.ReportChangeInItemData;
			List<Item> list;
			using (MyCollectionPool<Item>.GetListAndFill(from t in _registeredItemManager.AllItemsReadonlyCollection()
				where t.CanBeUnpacked()
				select t, out list))
			{
				if (list.Any())
				{
					UiOverlay.Instance.OkPopup(LOC.UI.Equipment_UnpackedItems, LOC.UI.Equipment_UnpackedItemsPrompt(list.Count) + "\n" + string.Join("\n", list.Select((Item t) => t.GetName())));
				}
			}
		}

		public override void ExitState()
		{
			Owner.A_Equipment_Sets.gameObject.SetActive(value: false);
			_registeredItemManager.OnItemsModified -= _compoundGameItemListAdapter.UpdateAll;
			_itemStats.CurrentItemInternallyChanged = null;
			_itemStats.ReturnToPool();
			_itemStats = null;
			Owner.A_ContentItems.ClearEverything();
		}

		public override EquipmentStaticGuiStateEnum GetStateEnum()
		{
			return _state;
		}
	}

	private class HairState : EquipmentStaticGuiState
	{
		private ItemStats _itemStats;

		private Item.ItemManager _registeredItemManager;

		private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

		private AdvancedItemList.AdvancedItemListAdapter<Item, GameItemContainer> _gameItemListAdapter;

		private void Prepare()
		{
			List<Folder<Item>> list = MyCollectionPool<Folder<Item>>.GetList();
			list.Add(Folder<Item>.AllFolder);
			list.Add(new Folder<Item>(() => LOC.UI.Equipment_Equipped, (Item x) => x.GetItemPrefab().EquipableByBot && x.IsEquipped()));
			list.Add(new Folder<Item>(() => LOC.UI.Equipment_NotEquipped, (Item x) => x.GetItemPrefab().EquipableByBot && !x.IsEquipped()));
			list.Add(new Folder<Item>(() => LOC.UI.Slot_Hair, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot == ItemSlot.SlotManager.HairMain, hideIfEmpty: true));
			list.Add(new Folder<Item>(() => LOC.UI.Slot_HairAdditional, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot == ItemSlot.SlotManager.HairAdditional, hideIfEmpty: true));
			list.Add(new Folder<Item>(() => LOC.UI.Slot_HairAugmentation, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot == ItemSlot.SlotManager.HairAugmentation, hideIfEmpty: true));
			list.Add(new Folder<Item>(() => LOC.UI.Item_Favourite, (Item x) => x.GetItemPrefab().EquipableByBot && x.IsFavourite, hideIfEmpty: true));
			if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.AllMods.Count > 0)
			{
				list.Add(new Folder<Item>(() => LOC.UI.Equipment_Modded, (Item x) => x.GetItemPrefab().EquipableByBot && !x.GameId.IsVanilla, hideIfEmpty: true));
			}
			EquippedItemRequirement.AddRequiredFolderIfContractNeedsAnyItem(list);
			using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithFolders(list)
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.BuildAndPrepare(Owner.A_ContentItems, DataSourceUtils.CreateMultipleUse(() => from x in GameVariables.Current.itemManager.AllItemsEnumerable()
					where x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot.Type == ItemSlot.SlotType.BotHair
					select x), out _compoundGameItemListAdapter, out _gameItemListAdapter))
			{
				_compoundGameItemListAdapter.ItemsMovedAction = (GameItemContainer target, Item source, int index) =>
				{
					GameVariables.Current.itemManager.MoveItemToOtherItemsPosition(target.Current, source);
				};
				_compoundGameItemListAdapter.ItemRightClickedAction = (GameItemContainer item) =>
				{
					_gameItemListAdapter.Focus(item.Index);
					Owner._itemStatsButtonList.ShowAsContextMenu();
				};
				_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
			}
		}

		private void SwitchDisplayedItem(Item item, int i)
		{
			IModificationPeriodButtonList modificationPeriod;
			using (Owner._itemStatsButtonList.DoInitialization(out modificationPeriod))
			{
				if (item != null)
				{
					_itemStats.SetItem(item);
					item.FillUIButtons(modificationPeriod, Item.ItemUIAllowedButtons.EquipUnequip | Item.ItemUIAllowedButtons.Modify);
				}
				else
				{
					_itemStats.SetItem(null);
				}
			}
		}

		private string BuildHairHelp()
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendLine(LOC.UI.HairSetting_HelpDescription1);
				stringBuilder.AppendLine(LOC.UI.HairSetting_HelpDescription2(ItemSlot.SlotManager.HairMain.FriendlyName, ItemSlot.SlotManager.HairAdditional.FriendlyName));
				stringBuilder.AppendLine(LOC.UI.HairSetting_HelpDescription3(LOC.UI.Equipment_ButtonModify));
				stringBuilder.AppendLine(LOC.UI.HairSetting_HelpDescription4(ItemSlot.SlotManager.HairMain.FriendlyName));
				return stringBuilder.ToString();
			}
		}

		public override void HelpButtonClicked()
		{
			UiOverlay.Instance.OkPopup(LOC.UI.HairSettings_Help, BuildHairHelp());
		}

		public override bool HasHelpButton()
		{
			return true;
		}

		private void OnItemsModified()
		{
			_compoundGameItemListAdapter.UpdateAll();
		}

		public override void EnterState()
		{
			Owner.SetMainLayout(listOnly: false);
			_itemStats = UiOverlay.Instance.itemStats.GetFromPool(Owner.A_ItemDescriptionArea);
			_itemStats.ItemDisplayType = ItemStats.ItemDisplayTypeEnum.PlayerInventory;
			_registeredItemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			Prepare();
			_registeredItemManager.OnItemsModified += OnItemsModified;
			_itemStats.CurrentItemInternallyChanged = GameVariables.Current.itemManager.ReportChangeInItemData;
		}

		public override void ExitState()
		{
			_registeredItemManager.OnItemsModified -= OnItemsModified;
			_itemStats.CurrentItemInternallyChanged = null;
			_itemStats.ReturnToPool();
			_itemStats = null;
			Owner.A_ContentItems.ClearEverything();
		}

		public override EquipmentStaticGuiStateEnum GetStateEnum()
		{
			return EquipmentStaticGuiStateEnum.Hair;
		}
	}

	private class ColorsState : EquipmentStaticGuiState
	{
		private CancellationTokenSource _cancellationTokenSource;

		private ItemColorSlotPicker _colorSlotPicker;

		private Item.ItemManager _registeredItemManager;

		private CompoundItemList.CompoundItemListAdapter<(Item, ColorSlot), StringAdvancedItemListItemPrefab> _compoundGameItemListAdapter;

		private AdvancedItemList.AdvancedItemListAdapter<(Item, ColorSlot), StringAdvancedItemListItemPrefab> _gameItemListAdapter;

		private void Prepare()
		{
			_colorSlotPicker.PullPresetsFromGameVariables();
			List<Folder<(Item, ColorSlot)>> list = MyCollectionPool<Folder<(Item, ColorSlot)>>.GetList();
			list.Add(new Folder<(Item, ColorSlot)>(() => LOC.UI.Folder_Hypercamo, ((Item, ColorSlot) tuple) => tuple.Item1.CanChangeColor));
			list.Add(Folder<(Item, ColorSlot)>.AllFolder);
			list.Add(new Folder<(Item, ColorSlot)>(() => LOC.UI.Folder_NotHypercamo, ((Item, ColorSlot) tuple) => !tuple.Item1.CanChangeColor));
			MultipleUseDataSource<(Item, ColorSlot)> itemSource = DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.BotEquippedItems().SelectMany((Item item) => item.GetItemPrefab().ColorSlots.Select((ColorSlot x) => (item: item, x: x))));
			using (CompoundItemList.StartBuilder<(Item, ColorSlot), StringAdvancedItemListItemPrefab>().WithToStringOnly(((Item, ColorSlot) tuple, StringRepresentationHelperEnum type) => tuple.Item1.GetItemPrefab().Name).WithAlphabeticalSortingOnly()
				.WithFolders(list)
				.WithSetItem(((Item, ColorSlot) tuple, StringAdvancedItemListItemPrefab s, CompoundItemList.CompoundItemListAdapter<(Item, ColorSlot), StringAdvancedItemListItemPrefab> adapter) =>
				{
					bool isFocused = s.IsFocused;
					StringBuilder stringBuilder;
					using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
					{
						if (isFocused)
						{
							stringBuilder.Append("<i><b>");
						}
						stringBuilder.Append(tuple.Item1.GetItemPrefab().Name);
						stringBuilder.Append(" - ");
						stringBuilder.Append(tuple.Item2.Name);
						if (isFocused)
						{
							stringBuilder.Append("(");
							stringBuilder.Append(LOC.UI.Selected);
							stringBuilder.Append(")</i></b>");
						}
						s.VisibleText.SetText(stringBuilder);
					}
				})
				.WithDefaultListItemPool()
				.BuildAndPrepare(Owner.A_ContentItems, itemSource, out _compoundGameItemListAdapter, out _gameItemListAdapter))
			{
				_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
			}
		}

		private void UpdateCurrentItemColorData()
		{
			Color currentColor = _colorSlotPicker.CurrentColor;
			if (_colorSlotPicker.TryGetValidChangeableItemSlot(out var item, out var index) && item.GetColor(index) != currentColor)
			{
				item.SetColor(index, currentColor);
				_registeredItemManager.ReportChangeInItemData(item);
			}
		}

		private void SwitchDisplayedItem((Item, ColorSlot) valueTuple, int i)
		{
			UpdateCurrentItemColorData();
			var (item, colorSlot) = valueTuple;
			_colorSlotPicker.SetItem(item, colorSlot);
			Owner._itemStatsButtonList.UpdateCurrentButtonState();
		}

		private void PrepareButtons()
		{
			IModificationPeriodButtonList modificationPeriod;
			using (Owner._itemStatsButtonList.DoInitialization(out modificationPeriod))
			{
				ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton();
				buttonListWrapper.SetText(LOC.UI.ColorsSection_PickFromPresets);
				buttonListWrapper.OnClick = PickFromPresetButtonClicked;
				buttonListWrapper.ActiveFunc = () => _colorSlotPicker.TryGetValidChangeableItemSlot(out var _, out var _);
			}
		}

		private void PickFromPresetButtonClicked()
		{
			if (_colorSlotPicker.TryGetValidChangeableItemSlot(out var item, out var colorIndex))
			{
				_colorSlotPicker.PushPresetsToGameVariables();
				UiOverlay.Instance.ColorInputPopup(LOC.UI.ColorPicker, LOC.UI.ColorPickerItemPrompt(item.GetName(ItemNameIncludeEnum.DefaultNoColorNoFavourite)), (Color c) =>
				{
					item.SetColor(colorIndex, c);
					_registeredItemManager.ReportChangeInItemData(item);
					_colorSlotPicker.PullPresetsFromGameVariables();
					_colorSlotPicker.ForceChangeColor(c);
				}, null, item.GetColor(colorIndex));
			}
		}

		private async UniTask ItemUpdateTimer(CancellationToken cancellationToken)
		{
			while (true)
			{
				await UniTask.WaitForSeconds(OptionsStatic.LowMemoryModeEnabled ? 1f : 0.25f, ignoreTimeScale: false, PlayerLoopTiming.Update, cancellationToken);
				UpdateCurrentItemColorData();
			}
		}

		private void OnItemsModified()
		{
			_compoundGameItemListAdapter.UpdateAll();
		}

		public override void EnterState()
		{
			Owner.SetMainLayout(listOnly: false);
			_cancellationTokenSource = new CancellationTokenSource();
			ItemUpdateTimer(_cancellationTokenSource.Token).Forget();
			_colorSlotPicker = UiOverlay.Instance.itemColorSlotPicker.GetFromPool(Owner.A_ItemDescriptionArea);
			_registeredItemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			Prepare();
			PrepareButtons();
			_registeredItemManager.OnItemsModified += OnItemsModified;
		}

		public override void ExitState()
		{
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource.Dispose();
			_cancellationTokenSource = null;
			_registeredItemManager.OnItemsModified -= OnItemsModified;
			_colorSlotPicker.ReturnToPool();
			_colorSlotPicker = null;
			Owner.A_ContentItems.ClearEverything();
			Owner._itemStatsButtonList.ResetButtons();
		}

		public override EquipmentStaticGuiStateEnum GetStateEnum()
		{
			return EquipmentStaticGuiStateEnum.Colors;
		}
	}

	private class SlidersState : EquipmentStaticGuiState
	{
		private Item.ItemManager _registeredItemManager;

		private CompoundItemList.CompoundItemListAdapter<(Item, ItemModifiableVariable), ItemModifiableVariableDisplay> _compoundGameItemListAdapter;

		private AdvancedItemList.AdvancedItemListAdapter<(Item, ItemModifiableVariable), ItemModifiableVariableDisplay> _gameItemListAdapter;

		private void Prepare()
		{
			List<Folder<(Item, ItemModifiableVariable)>> list = MyCollectionPool<Folder<(Item, ItemModifiableVariable)>>.GetList();
			list.Add(Folder<(Item, ItemModifiableVariable)>.AllFolder);
			list.Add(new Folder<(Item, ItemModifiableVariable)>(() => LOC.UI.Folder_Sliders, ((Item, ItemModifiableVariable) x) => x.Item2 is ItemModifiableVariableFloat01Percent, hideIfEmpty: true));
			list.Add(new Folder<(Item, ItemModifiableVariable)>(() => LOC.UI.Folder_Toggles, ((Item, ItemModifiableVariable) x) => x.Item2 is ItemModifiableVariableIntBackedBool, hideIfEmpty: true));
			MultipleUseDataSource<(Item, ItemModifiableVariable)> itemSource = DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.BotEquippedItems().SelectMany((Item item) => item.GetItemPrefab().ItemModifiableVariables.Select((ItemModifiableVariable x) => (item: item, x: x))));
			using (CompoundItemList.StartBuilder<(Item, ItemModifiableVariable), ItemModifiableVariableDisplay>().WithToStringOnly(((Item, ItemModifiableVariable) tuple, StringRepresentationHelperEnum type) => tuple.Item2.GetLocalizedName()).WithAlphabeticalSortingOnly()
				.WithFolders(list)
				.WithSetItem(((Item, ItemModifiableVariable) tuple, ItemModifiableVariableDisplay s, CompoundItemList.CompoundItemListAdapter<(Item, ItemModifiableVariable), ItemModifiableVariableDisplay> adapter) =>
				{
					s.SetData(tuple, adapter);
				})
				.WithListPool(UiOverlay.Instance.itemModifiableVariableDisplay.GetPool())
				.BuildAndPrepare(Owner.A_ContentItems, itemSource, out _compoundGameItemListAdapter, out _gameItemListAdapter))
			{
				_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
			}
		}

		private void SwitchDisplayedItem((Item, ItemModifiableVariable) tuple, int i)
		{
			Owner._itemStatsButtonList.UpdateCurrentButtonState();
		}

		private void PrepareButtons()
		{
			IModificationPeriodButtonList modificationPeriod;
			using (Owner._itemStatsButtonList.DoInitialization(out modificationPeriod))
			{
			}
		}

		private void OnItemsModified()
		{
			_compoundGameItemListAdapter.UpdateAll();
		}

		public override void EnterState()
		{
			Owner.SetMainLayout(listOnly: true);
			_registeredItemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			Prepare();
			PrepareButtons();
			_registeredItemManager.OnItemsModified += OnItemsModified;
		}

		public override void ExitState()
		{
			_registeredItemManager.OnItemsModified -= OnItemsModified;
			Owner.A_ContentItems.ClearEverything();
			Owner._itemStatsButtonList.ResetButtons();
		}

		public override EquipmentStaticGuiStateEnum GetStateEnum()
		{
			return EquipmentStaticGuiStateEnum.Sliders;
		}
	}

	[Header("Other")]
	[SerializeField]
	private FreeCamera freeCamera;

	[SerializeField]
	private ButtonList _itemStatsButtonList;

	private BasicStateMachine<EquipmentStaticGuiState, EquipmentStaticGui> _stateMachine;

	[Header("Auto")]
	[SerializeField]
	private ButtonList A_Header_Button_List;

	[SerializeField]
	private BetterButton A_Help_Button;

	[SerializeField]
	private RectTransform A_ContentLeft;

	[SerializeField]
	private RectTransform A_ItemDescriptionArea;

	[SerializeField]
	private EquipmentSetsUI A_Equipment_Sets;

	[SerializeField]
	private CompoundItemList A_ContentItems;

	[SerializeField]
	private ButtonList A_Side_Button_List;

	[SerializeField]
	private Live2DToolSelector A_Live2D_Tool_Selector;

	public FreeCamera FreeCamera => freeCamera;

	public ButtonList SideButtonList => A_Side_Button_List;

	public Live2DToolSelector ToolSelector => A_Live2D_Tool_Selector;

	private EquipmentStaticGuiStateEnum CurrentSection => _stateMachine.CurrentState?.GetStateEnum() ?? EquipmentStaticGuiStateEnum.NoSection;

	private void SetMainLayout(bool listOnly)
	{
		RectTransform rectTransform = (RectTransform)A_ContentLeft.transform;
		_ = (RectTransform)A_ContentItems.transform;
		if (listOnly)
		{
			rectTransform.gameObject.SetActive(value: false);
		}
		else
		{
			rectTransform.gameObject.SetActive(value: true);
		}
	}

	private void UpdateHelpButton()
	{
		EquipmentStaticGuiState currentState = _stateMachine.CurrentState;
		if (currentState != null && currentState.HasHelpButton())
		{
			A_Help_Button.gameObject.SetActive(value: true);
			A_Help_Button.onClick.SetListener(HelpButtonClicked);
		}
		else
		{
			A_Help_Button.gameObject.SetActive(value: false);
			A_Help_Button.onClick.RemoveAllListeners();
		}
	}

	private void HelpButtonClicked()
	{
		_stateMachine.CurrentState.HelpButtonClicked();
	}

	private void UpdateHeaderButtons()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (A_Header_Button_List.DoInitialization(out modificationPeriod))
		{
			EquipmentStaticGuiStateEnum stateEnum = _stateMachine.CurrentState.GetStateEnum();
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, (stateEnum == EquipmentStaticGuiStateEnum.Equipment) ? CommonButtonColorType.Green : CommonButtonColorType.Normal);
			buttonListWrapper.SetText(LOC.UI.Room_Equipment);
			buttonListWrapper.OnClick = () =>
			{
				SwitchSection(EquipmentStaticGuiStateEnum.Equipment);
			};
			ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(0, (stateEnum == EquipmentStaticGuiStateEnum.Clothes) ? CommonButtonColorType.Green : CommonButtonColorType.Normal);
			buttonListWrapper2.SetText(LOC.UI.Equipment_Clothes);
			buttonListWrapper2.OnClick = () =>
			{
				SwitchSection(EquipmentStaticGuiStateEnum.Clothes);
			};
			ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton(10, (stateEnum == EquipmentStaticGuiStateEnum.Hair) ? CommonButtonColorType.Green : CommonButtonColorType.Normal);
			buttonListWrapper3.SetText(LOC.UI.Equipment_HairSection);
			buttonListWrapper3.OnClick = () =>
			{
				SwitchSection(EquipmentStaticGuiStateEnum.Hair);
			};
			ButtonListWrapper buttonListWrapper4 = modificationPeriod.AddButton(20, (stateEnum == EquipmentStaticGuiStateEnum.Colors) ? CommonButtonColorType.Green : CommonButtonColorType.Normal);
			buttonListWrapper4.SetText(LOC.UI.Equipment_ColorSection);
			buttonListWrapper4.OnClick = () =>
			{
				SwitchSection(EquipmentStaticGuiStateEnum.Colors);
			};
			ButtonListWrapper buttonListWrapper5 = modificationPeriod.AddButton(40, (stateEnum == EquipmentStaticGuiStateEnum.Sliders) ? CommonButtonColorType.Green : CommonButtonColorType.Normal);
			buttonListWrapper5.SetText(LOC.UI.Equipment_ButtonModify);
			buttonListWrapper5.OnClick = () =>
			{
				SwitchSection(EquipmentStaticGuiStateEnum.Sliders);
			};
		}
	}

	protected override void FillReferencesInherited()
	{
		transform.EnsureComponent<FreeCamera>(out freeCamera);
	}

	public void SwitchSection(EquipmentStaticGuiStateEnum target)
	{
		if (CurrentSection != target)
		{
			switch (target)
			{
			default:
				_stateMachine.ChangeState(new EquipmentState(EquipmentStaticGuiStateEnum.Equipment));
				break;
			case EquipmentStaticGuiStateEnum.Clothes:
				_stateMachine.ChangeState(new EquipmentState(EquipmentStaticGuiStateEnum.Clothes));
				break;
			case EquipmentStaticGuiStateEnum.Hair:
				_stateMachine.ChangeState(new HairState());
				break;
			case EquipmentStaticGuiStateEnum.Colors:
				_stateMachine.ChangeState(new ColorsState());
				break;
			case EquipmentStaticGuiStateEnum.Sliders:
				_stateMachine.ChangeState(new SlidersState());
				break;
			}
			UpdateHeaderButtons();
			UpdateHelpButton();
		}
	}

	public void Init(EquipmentStaticGuiStateEnum startState)
	{
		if (_stateMachine == null)
		{
			_stateMachine = new BasicStateMachine<EquipmentStaticGuiState, EquipmentStaticGui>(this, "Equipment Static Gui");
		}
		SwitchSection(startState);
	}

	public void OnEnable()
	{
		A_Equipment_Sets.gameObject.SetActive(value: false);
	}

	public void OnDisable()
	{
		_stateMachine.KillStateMachine();
		_stateMachine = null;
	}
}
