using System;
using System.Collections.Generic;
using System.Text;

public class RecipeIngredientExactItem : RecipeIngredient
{
	private readonly GameId _itemGameId;

	private ItemPrefab _itemPrefab;

	public ItemPrefab RelatedPrefab => _itemPrefab ?? (_itemPrefab = ItemPrefab.ItemPrefabManager.GetItemById(_itemGameId));

	public RecipeIngredientExactItem(GameId gameId, int count, bool isOptional)
		: base(count, isOptional)
	{
		_itemGameId = gameId;
	}

	protected override int GetApproximateSinglePrice()
	{
		return RelatedPrefab.Price;
	}

	protected override void AppendSingleName(StringBuilder stringBuilder)
	{
		stringBuilder.Append(RelatedPrefab.Name);
	}

	public override IEnumerable<Item> FindInInventory(Item.ItemManager itemManager)
	{
		if (itemManager.TryGetItemsByGameId(_itemGameId, out var gameIdItems))
		{
			return gameIdItems;
		}
		return Array.Empty<Item>();
	}
}
