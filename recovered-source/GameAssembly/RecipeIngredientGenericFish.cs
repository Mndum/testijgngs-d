using System.Collections.Generic;
using System.Text;
using GameItems;

public class RecipeIngredientGenericFish : RecipeIngredient
{
	private readonly float _minWeight;

	public RecipeIngredientGenericFish(float minWeightKg, int count, bool isOptional)
		: base(count, isOptional)
	{
		_minWeight = minWeightKg;
	}

	protected override int GetApproximateSinglePrice()
	{
		return ItemPrefab.ItemPrefabManager.GetItemById(ItemEnum.Carp.ToGameId()).Price;
	}

	protected override void AppendSingleName(StringBuilder stringBuilder)
	{
		stringBuilder.Append(LOC.UI.CookingSong_IngredientFishAtLeastXkg(_minWeight));
	}

	public override IEnumerable<Item> FindInInventory(Item.ItemManager itemManager)
	{
		foreach (Item item in itemManager.AllItemsEnumerable())
		{
			if (item.GetItemPrefab().FishData != null && item.FishWeight >= _minWeight && !item.FishUnsafeToEatAtAll)
			{
				yield return item;
			}
		}
	}
}
