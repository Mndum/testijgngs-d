using System;
using System.Linq;

namespace StoryConditions;

public sealed class OrCondition : StoryCondition, IEquatable<OrCondition>
{
	public readonly StoryCondition[] Operands;

	public OrCondition(StoryCondition[] operands, bool negate)
		: base(negate)
	{
		Operands = operands;
	}

	protected override bool InnerConditionMet()
	{
		StoryCondition[] operands = Operands;
		for (int i = 0; i < operands.Length; i++)
		{
			if (operands[i].ConditionMet())
			{
				return true;
			}
		}
		return false;
	}

	public override string Explain()
	{
		if (Negate)
		{
			return "not (" + string.Join(" or ", Operands.Select((StoryCondition t) => t.Explain())) + ")";
		}
		return "(" + string.Join(" or ", Operands.Select((StoryCondition t) => t.Explain())) + ")";
	}

	public override string GenerateCode()
	{
		if (Negate)
		{
			return "StoryCondition.NotOr(" + string.Join(", ", Operands.Select((StoryCondition t) => t.GenerateCode())) + ")";
		}
		return "StoryCondition.Or(" + string.Join(", ", Operands.Select((StoryCondition t) => t.GenerateCode())) + ")";
	}

	public bool Equals(OrCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (!Equals((StoryCondition)other))
		{
			return false;
		}
		if (Operands != other.Operands)
		{
			return false;
		}
		for (int i = 0; i < Operands.Length; i++)
		{
			if (!object.Equals(Operands[i], other.Operands[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is OrCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Operands, 1234);
	}
}
