using System;

namespace StoryConditions;

public sealed class FlagConditionWithTime : StoryCondition, IEquatable<FlagConditionWithTime>
{
	private readonly string Flag;

	private readonly int TimePeriod;

	private readonly GameVariables.TimeCheckType TimeCheck;

	public FlagConditionWithTime(string flag, int timePeriod, GameVariables.TimeCheckType timeCheck, bool negate)
		: base(negate)
	{
		Flag = flag;
		TimePeriod = timePeriod;
		TimeCheck = timeCheck;
	}

	protected override bool InnerConditionMet()
	{
		return GameVariables.Current.CheckFlagWithTime(Flag, TimePeriod, TimeCheck);
	}

	public override string Explain()
	{
		string text = GameVariables.TimeCheckTypeString(TimeCheck, GlobalFunctions.TimePeriod(TimePeriod));
		if (Negate)
		{
			return "is not set \"" + Flag + "\" " + text;
		}
		return "is set \"" + Flag + "\" " + text;
	}

	public override string GenerateCode()
	{
		return TimeCheck switch
		{
			GameVariables.TimeCheckType.BeforeRelative => string.Format("{0}.{1}(\"{2}\", {3}{4})", "StoryCondition", "FlagSetBeforeRelative", Flag, -TimePeriod, GenerateCodeNegationParameter()), 
			GameVariables.TimeCheckType.AfterRelative => string.Format("{0}.{1}(\"{2}\",  {3}{4})", "StoryCondition", "FlagSetAfterRelative", Flag, -TimePeriod, GenerateCodeNegationParameter()), 
			_ => string.Format("{0}.{1}(\"{2}\",  {3}, {4}{5})", "StoryCondition", "FlagSetWithTime", Flag, TimePeriod, StoryCondition.TimeCheckTypeToCode(TimeCheck), GenerateCodeNegationParameter()), 
		};
	}

	public bool Equals(FlagConditionWithTime other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other) && Flag == other.Flag && TimePeriod == other.TimePeriod)
		{
			return TimeCheck == other.TimeCheck;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is FlagConditionWithTime other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Flag, TimePeriod, (int)TimeCheck);
	}
}
