using System;

namespace StoryConditions;

public sealed class ItemCondition : StoryCondition, IEquatable<ItemCondition>
{
	public enum ItemComparisonTypeEnum
	{
		EquippedById,
		InInventoryById
	}

	public readonly GameId RelatedItem;

	public readonly ItemComparisonTypeEnum ItemComparisonType;

	public ItemCondition(GameId relatedItem, ItemComparisonTypeEnum itemComparisonType, bool negate)
		: base(negate)
	{
		RelatedItem = relatedItem;
		ItemComparisonType = itemComparisonType;
	}

	protected override bool InnerConditionMet()
	{
		ItemComparisonTypeEnum itemComparisonType = ItemComparisonType;
		if (itemComparisonType == ItemComparisonTypeEnum.EquippedById || itemComparisonType != ItemComparisonTypeEnum.InInventoryById)
		{
			return GameVariables.Current.itemManager.IsEquipped(RelatedItem);
		}
		return GameVariables.Current.itemManager.HasItem(RelatedItem);
	}

	public override string Explain()
	{
		ItemPrefab itemById = ItemPrefab.ItemPrefabManager.GetItemById(RelatedItem);
		ItemComparisonTypeEnum itemComparisonType = ItemComparisonType;
		if (itemComparisonType == ItemComparisonTypeEnum.EquippedById || itemComparisonType != ItemComparisonTypeEnum.InInventoryById)
		{
			return "(" + itemById.Name + " " + (Negate ? "not" : "") + "equipped)";
		}
		return "(" + itemById.Name + " " + (Negate ? "not" : "") + " in inventory)";
	}

	public override string GenerateCode()
	{
		return "(UNIMPLEMENTED)";
	}

	public bool Equals(ItemCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other) && object.Equals(RelatedItem, other.RelatedItem))
		{
			return ItemComparisonType == other.ItemComparisonType;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is ItemCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, RelatedItem, (int)ItemComparisonType);
	}
}
