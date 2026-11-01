using System;
using System.Linq;

namespace StoryConditions;

public sealed class AndCondition : StoryCondition, IEquatable<AndCondition>
{
	public readonly StoryCondition[] Operands;

	public AndCondition(StoryCondition[] operands, bool negate)
		: base(negate)
	{
		Operands = operands;
	}

	protected override bool InnerConditionMet()
	{
		StoryCondition[] operands = Operands;
		for (int i = 0; i < operands.Length; i++)
		{
			if (!operands[i].ConditionMet())
			{
				return false;
			}
		}
		return true;
	}

	public override string Explain()
	{
		if (Negate)
		{
			return "(" + string.Join(" and ", Operands.Select((StoryCondition t) => t.Explain())) + ")";
		}
		return "not (" + string.Join(" and ", Operands.Select((StoryCondition t) => t.Explain())) + ")";
	}

	public override string GenerateCode()
	{
		if (Negate)
		{
			return "StoryCondition.NotAnd(" + string.Join(", ", Operands.Select((StoryCondition t) => t.GenerateCode())) + ")";
		}
		return "StoryCondition.And(" + string.Join(", ", Operands.Select((StoryCondition t) => t.GenerateCode())) + ")";
	}

	public bool Equals(AndCondition other)
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
			if (obj is AndCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), Operands);
	}
}
