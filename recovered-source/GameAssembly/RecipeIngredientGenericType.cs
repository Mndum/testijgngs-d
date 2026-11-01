using System.Collections.Generic;
using System.Text;
using GameItems;

public class RecipeIngredientGenericType : RecipeIngredient
{
	private readonly EdibleTypeEnum _desiredItemType;

	public override bool RequiresHelp => true;

	public RecipeIngredientGenericType(EdibleTypeEnum desiredItemType, int count, bool isOptional)
		: base(count, isOptional)
	{
		_desiredItemType = desiredItemType;
	}

	public override string GetHelpDescription()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(LOC.UI.GenericIngredientHelpStart);
			foreach (var allNonDefaultItem in ItemPrefab.ItemPrefabManager.AllNonDefaultItems)
			{
				ItemPrefab item = allNonDefaultItem.prefab;
				EdibleItemPrefabData edibleData = item.EdibleData;
				if (edibleData != null && edibleData.EdibleType == _desiredItemType)
				{
					stringBuilder.Append("-");
					stringBuilder.Append(item.Name);
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}
	}

	protected override int GetApproximateSinglePrice()
	{
		return _desiredItemType switch
		{
			EdibleTypeEnum.Meat => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.ChickenMeat.ToGameId()).Price, 
			EdibleTypeEnum.Fish => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.Carp.ToGameId()).Price, 
			EdibleTypeEnum.MainVegetable => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.Carrot.ToGameId()).Price, 
			EdibleTypeEnum.FlavourVegetable => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.Onions.ToGameId()).Price, 
			EdibleTypeEnum.Bean => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.CanOfBakedBeans.ToGameId()).Price, 
			EdibleTypeEnum.Pasta => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.Pasta.ToGameId()).Price, 
			EdibleTypeEnum.Bread => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.BreadFromDispenser.ToGameId()).Price, 
			EdibleTypeEnum.Beverage => ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.Soda.ToGameId()).Price, 
			_ => 100, 
		};
	}

	protected override void AppendSingleName(StringBuilder stringBuilder)
	{
		switch (_desiredItemType)
		{
		case EdibleTypeEnum.Other:
			stringBuilder.Append(LOC.UI.Ingredient_AnyOther);
			break;
		case EdibleTypeEnum.Meat:
			stringBuilder.Append(LOC.UI.Ingredient_AnyMeat);
			break;
		case EdibleTypeEnum.Fish:
			stringBuilder.Append(LOC.UI.Ingredient_AnyFish);
			break;
		case EdibleTypeEnum.MainVegetable:
			stringBuilder.Append(LOC.UI.Ingredient_AnyMainVegetable);
			break;
		case EdibleTypeEnum.FlavourVegetable:
			stringBuilder.Append(LOC.UI.Ingredient_AnyFlavourVegetable);
			break;
		case EdibleTypeEnum.Bean:
			stringBuilder.Append(LOC.UI.Ingredient_AnyBean);
			break;
		case EdibleTypeEnum.Pasta:
			stringBuilder.Append(LOC.UI.Ingredient_AnyPasta);
			break;
		case EdibleTypeEnum.Bread:
			stringBuilder.Append(LOC.UI.Ingredient_AnyBread);
			break;
		case EdibleTypeEnum.Beverage:
			stringBuilder.Append(LOC.UI.Ingredient_AnyBeverage);
			break;
		case EdibleTypeEnum.Dish:
			stringBuilder.Append(LOC.UI.Ingredient_AnyCookedDish);
			break;
		default:
			stringBuilder.Append(LOC.UI.UI_Error);
			break;
		}
	}

	public override IEnumerable<Item> FindInInventory(Item.ItemManager itemManager)
	{
		foreach (Item item in itemManager.AllItemsEnumerable())
		{
			EdibleItemPrefabData edibleData = item.GetItemPrefab().EdibleData;
			if (edibleData != null && edibleData.EdibleType == _desiredItemType)
			{
				yield return item;
			}
		}
	}
}
