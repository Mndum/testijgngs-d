using System;

namespace StoryConditions;

public sealed class DelegateCondition : StoryCondition, IEquatable<DelegateCondition>
{
	public readonly Func<bool> Condition;

	public DelegateCondition(Func<bool> condition, bool negate)
		: base(negate)
	{
		Condition = condition;
	}

	protected override bool InnerConditionMet()
	{
		return Condition();
	}

	public override string Explain()
	{
		if (Negate)
		{
			return $"not delegate \"{Condition}\"";
		}
		return $"delegate \"{Condition}\"";
	}

	public override string GenerateCode()
	{
		return "//Unimplemented";
	}

	public bool Equals(DelegateCondition other)
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
			return object.Equals(Condition, other.Condition);
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is DelegateCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Condition);
	}
}
