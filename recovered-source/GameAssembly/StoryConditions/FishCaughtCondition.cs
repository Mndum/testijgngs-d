using System;

namespace StoryConditions;

public sealed class FishCaughtCondition : StoryCondition, IEquatable<FishCaughtCondition>
{
	public readonly GameId FishId;

	public readonly int Count;

	private ItemPrefab _fishPrefab;

	public ItemPrefab FishPrefab => _fishPrefab ?? (_fishPrefab = ItemPrefab.ItemPrefabManager.GetItemById(FishId));

	public FishCaughtCondition(GameId fishId, int count, bool negate)
		: base(negate)
	{
		FishId = fishId;
		Count = count;
	}

	protected override bool InnerConditionMet()
	{
		if (FishPrefab.FishData == null)
		{
			return false;
		}
		return GameVariables.Current.fishingMinigameManager.GetSave(FishId).TimesCaught >= Count;
	}

	public override string Explain()
	{
		return string.Format("({0}{1} {2} caught)", Negate ? "Not " : "", Count, ItemPrefab.ItemPrefabManager.GetItemById(FishId).Name);
	}

	public override string GenerateCode()
	{
		return "(UNIMPLEMENTED)";
	}

	public bool Equals(FishCaughtCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other) && object.Equals(FishId, other.FishId))
		{
			return Count == other.Count;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is FishCaughtCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, FishId, Count);
	}
}
