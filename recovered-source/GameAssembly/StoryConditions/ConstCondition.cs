using System;

namespace StoryConditions;

public sealed class ConstCondition : StoryCondition, IEquatable<ConstCondition>
{
	public ConstCondition(bool negate)
		: base(negate)
	{
	}

	protected override bool InnerConditionMet()
	{
		return true;
	}

	public override string Explain()
	{
		if (Negate)
		{
			return "always false";
		}
		return "always true";
	}

	public override string GenerateCode()
	{
		if (Negate)
		{
			return "StoryCondition.AlwaysFalseCondition()";
		}
		return "StoryCondition.AlwaysTrueCondition()";
	}

	public bool Equals(ConstCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		return Negate == other.Negate;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is ConstCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, 69);
	}
}
