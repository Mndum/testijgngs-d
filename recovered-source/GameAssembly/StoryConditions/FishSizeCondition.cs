using System;

namespace StoryConditions;

public sealed class FishSizeCondition : StoryCondition, IEquatable<FishSizeCondition>
{
	public readonly GameId FishId;

	public readonly float Percentile;

	private ItemPrefab _fishPrefab;

	public ItemPrefab FishPrefab => _fishPrefab ?? (_fishPrefab = ItemPrefab.ItemPrefabManager.GetItemById(FishId));

	public FishSizeCondition(GameId fishId, float percentile, bool negate)
		: base(negate)
	{
		FishId = fishId;
		Percentile = percentile;
	}

	protected override bool InnerConditionMet()
	{
		FishItemPrefabData fishData = FishPrefab.FishData;
		if (fishData == null)
		{
			return false;
		}
		FishSave save = GameVariables.Current.fishingMinigameManager.GetSave(FishId);
		if (!save.WasEverCaught)
		{
			return false;
		}
		return fishData.DetermineFishPercentile(save.MaxWeightCaught) >= Percentile;
	}

	public override string Explain()
	{
		return string.Format("({0}caught a {1} that's at least {2}% of the maximum size)", Negate ? "Not " : "", FishPrefab.Name, Percentile * 100f);
	}

	public override string GenerateCode()
	{
		return "(UNIMPLEMENTED)";
	}

	public bool Equals(FishSizeCondition other)
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
			float percentile = Percentile;
			return percentile.Equals(other.Percentile);
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is FishSizeCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, FishId, Percentile);
	}
}
