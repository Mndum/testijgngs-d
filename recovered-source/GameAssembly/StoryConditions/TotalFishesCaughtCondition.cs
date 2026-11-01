using System;

namespace StoryConditions;

public sealed class TotalFishesCaughtCondition : StoryCondition, IEquatable<TotalFishesCaughtCondition>
{
	public readonly int Count;

	public TotalFishesCaughtCondition(int count, bool negate)
		: base(negate)
	{
		Count = count;
	}

	protected override bool InnerConditionMet()
	{
		return GameVariables.Current.fishingMinigameManager.FishCaughtCount >= Count;
	}

	public override string Explain()
	{
		return string.Format("({0}{1} total fish caught)", Negate ? "Not " : "", Count);
	}

	public override string GenerateCode()
	{
		return "(UNIMPLEMENTED)";
	}

	public bool Equals(TotalFishesCaughtCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other))
		{
			return Count == other.Count;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is TotalFishesCaughtCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Count);
	}
}
