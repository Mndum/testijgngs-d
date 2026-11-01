using System;
using System.Collections.Generic;

public static class ItemPrefabCompoundListAdapterExtensions
{
	public static CompoundItemList.CompoundItemListAdapter<ItemPrefab, TListItemPrefab>.IBuilderSetItem WithDefaultItemPrefabFolders<TListItemPrefab>(this CompoundItemList.CompoundItemListAdapter<ItemPrefab, TListItemPrefab>.IBuilderFolders foldersStage) where TListItemPrefab : AdvancedItemListPrefab
	{
		List<Folder<ItemPrefab>> pooledWithAllFolder = Folder<ItemPrefab>.GetPooledWithAllFolder();
		pooledWithAllFolder.Add(new Folder<ItemPrefab>(() => LOC.UI.Equipment_PersonalityModules, (ItemPrefab prefab) => prefab.IsPersonalityModule, hideIfEmpty: true));
		pooledWithAllFolder.Add(new Folder<ItemPrefab>(() => LOC.UI.Equipment_Clothes, (ItemPrefab prefab) => prefab.Category == ItemCategory.Clothing, hideIfEmpty: true));
		pooledWithAllFolder.Add(new Folder<ItemPrefab>(() => LOC.UI.Equipment_Modules, (ItemPrefab prefab) => prefab.Category == ItemCategory.Modules, hideIfEmpty: true));
		pooledWithAllFolder.Add(new Folder<ItemPrefab>(() => LOC.UI.Equipment_Food, (ItemPrefab prefab) => prefab.Category == ItemCategory.Food, hideIfEmpty: true));
		pooledWithAllFolder.Add(new Folder<ItemPrefab>(() => LOC.UI.Folder_CookingIngredients, (ItemPrefab prefab) => prefab.Category == ItemCategory.CookingIngredient, hideIfEmpty: true));
		return foldersStage.WithFolders(pooledWithAllFolder);
	}

	public static CompoundItemList.CompoundItemListAdapter<ItemPrefab, TListItemPrefab>.IBuilderFolders WithDefaultItemPrefabSorting<TListItemPrefab>(this CompoundItemList.CompoundItemListAdapter<ItemPrefab, TListItemPrefab>.IBuilderSortingStage sortingStage) where TListItemPrefab : AdvancedItemListPrefab
	{
		return sortingStage.WithSortTypes(new SortType<ItemPrefab>[3]
		{
			new SortType<ItemPrefab>(() => LOC.UI.Equipment_SortNoSort, null),
			new SortType<ItemPrefab>(() => LOC.UI.Equipment_SortAlphabetically, Comparer<ItemPrefab>.Create((ItemPrefab a, ItemPrefab b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal))),
			new SortType<ItemPrefab>(() => LOC.UI.Equipment_SortPrice, Comparer<ItemPrefab>.Create((ItemPrefab a, ItemPrefab b) => a.Price.CompareTo(b.Price)))
		});
	}

	public static CompoundItemList.CompoundItemListAdapter<ItemPrefab, TListItemPrefab>.IBuilderSortingStage WithDefaultItemPrefabName<TListItemPrefab>(this CompoundItemList.CompoundItemListAdapter<ItemPrefab, TListItemPrefab>.IBuilderNames nameStage) where TListItemPrefab : AdvancedItemListPrefab
	{
		return nameStage.WithToStringOnly((ItemPrefab item, StringRepresentationHelperEnum type) => type switch
		{
			StringRepresentationHelperEnum.Full => item.Name, 
			StringRepresentationHelperEnum.Description => item.Description, 
			_ => item.Name, 
		});
	}
}
