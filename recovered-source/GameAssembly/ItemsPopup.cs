using System.Collections.Generic;
using UnityEngine;

public class ItemsPopup : PrefabPopup<ItemsPopup>
{
	[SerializeField]
	private CompoundItemList _gameItemList;

	private CompoundItemList.CompoundItemListAdapter<Item, GameItemContainer> _compoundGameItemListAdapter;

	private AdvancedItemList.AdvancedItemListAdapter<Item, GameItemContainer> _gameItemListAdapter;

	[SerializeField]
	private ItemStats _itemStats;

	[SerializeField]
	private ButtonList _itemStatsButtonList;

	private Item.ItemManager _registeredItemManager;

	public ButtonList ItemStatsButtonList => _itemStatsButtonList;

	private void Prepare()
	{
		List<Folder<Item>> list = MyCollectionPool<Folder<Item>>.GetList();
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Food, (Item x) =>
		{
			ItemCategory category = x.GetItemPrefab().Category;
			return category == ItemCategory.Food || category == ItemCategory.FoodCookedDish;
		}));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Fish, (Item x) => x.GetItemPrefab().Category == ItemCategory.Fish, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Folder_CookingIngredients, (Item x) => x.GetItemPrefab().Category == ItemCategory.CookingIngredient, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Medicine, (Item x) => x.GetItemPrefab().Category == ItemCategory.Meds, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Package, (Item x) => x.GetItemPrefab().Category == ItemCategory.Package, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Modules, (Item x) => x.GetItemPrefab().Category == ItemCategory.Modules, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Clothes, (Item x) => x.GetItemPrefab().Category == ItemCategory.Clothing, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.FishingFishingRods, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot == ItemSlot.SlotManager.Anon_FishingRod, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.FishingBait, (Item x) => x.GetItemPrefab().IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot == ItemSlot.SlotManager.Anon_FishingBait, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Shower_Soap, (Item x) => x.GetItemPrefab().Category == ItemCategory.Soap, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Other, null, hideIfEmpty: false, remainingFolder: true));
		EquippedItemRequirement.AddRequiredFolderIfContractNeedsAnyItem(list);
		list.Add(Folder<Item>.AllFolder);
		list.Add(new Folder<Item>(() => LOC.UI.Item_Favourite, (Item x) => x.IsFavourite, hideIfEmpty: true));
		using (CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
			.WithFolders(list)
			.WithDefaultGameItemSetItem()
			.WithDefaultGameItemListItemPool()
			.BuildAndPrepare(_gameItemList, DataSourceUtils.CreateMultipleUse(() => GameVariables.Current.itemManager.AllItemsEnumerable()), out _compoundGameItemListAdapter, out _gameItemListAdapter))
		{
			_compoundGameItemListAdapter.ItemsMovedAction = (GameItemContainer target, Item source, int sourceIndex) =>
			{
				GameVariables.Current.itemManager.MoveItemToOtherItemsPosition(target.Current, source);
			};
			_compoundGameItemListAdapter.ItemRightClickedAction = (GameItemContainer item) =>
			{
				_gameItemListAdapter.Focus(item.Index);
				_itemStatsButtonList.ShowAsContextMenu();
			};
			_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
		}
	}

	private void SwitchDisplayedItem(Item item, int i)
	{
		IModificationPeriodButtonList modificationPeriod;
		using (ItemStatsButtonList.DoInitialization(out modificationPeriod))
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

	public override void Open()
	{
		_registeredItemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
		Prepare();
		_registeredItemManager.OnItemsModified += _compoundGameItemListAdapter.UpdateAll;
		_itemStats.CurrentItemInternallyChanged = GameVariables.Current.itemManager.ReportChangeInItemData;
		base.Open();
	}

	public override bool CloseInner()
	{
		base.CloseInner();
		_registeredItemManager.OnItemsModified -= _compoundGameItemListAdapter.UpdateAll;
		_itemStats.CurrentItemInternallyChanged = null;
		return true;
	}
}
