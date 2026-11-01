using System;

namespace StoryConditions;

public sealed class FlagConditionBasic : StoryCondition, IEquatable<FlagConditionBasic>
{
	public readonly string Flag;

	public FlagConditionBasic(string flag, bool negate)
		: base(negate)
	{
		Flag = flag;
	}

	protected override bool InnerConditionMet()
	{
		return GameVariables.Current.CheckFlag(Flag);
	}

	public override string Explain()
	{
		if (Negate)
		{
			return "is not set \"" + Flag + "\"";
		}
		return "is set \"" + Flag + "\"";
	}

	public override string GenerateCode()
	{
		if (Negate)
		{
			return "StoryCondition.FlagNotSet(\"" + Flag + "\")";
		}
		return "StoryCondition.FlagSet(\"" + Flag + "\")";
	}

	public bool Equals(FlagConditionBasic other)
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
			return Flag == other.Flag;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is FlagConditionBasic other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Flag);
	}
}
