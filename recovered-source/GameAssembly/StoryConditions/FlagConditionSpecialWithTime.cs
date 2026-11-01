using System;

namespace StoryConditions;

public sealed class FlagConditionSpecialWithTime : StoryCondition, IEquatable<FlagConditionSpecialWithTime>
{
	private readonly string Flag;

	private readonly GameVariables.CheckType CheckType;

	private readonly int TimePeriod;

	private readonly GameVariables.TimeCheckType TimeCheck;

	public FlagConditionSpecialWithTime(string flag, GameVariables.CheckType checkType, int timePeriod, GameVariables.TimeCheckType timeCheck, bool negate)
		: base(negate)
	{
		Flag = flag;
		CheckType = checkType;
		TimePeriod = timePeriod;
		TimeCheck = timeCheck;
	}

	protected override bool InnerConditionMet()
	{
		return GameVariables.Current.CheckFlagSpecialWithTime(Flag, CheckType, TimePeriod, TimeCheck);
	}

	public override string Explain()
	{
		string text = GameVariables.TimeCheckTypeString(TimeCheck, GlobalFunctions.TimePeriod(TimePeriod));
		string text2 = CheckType.ToString();
		if (Negate)
		{
			return "is not set \"" + Flag + "\" " + text + " " + text2;
		}
		return "is set \"" + Flag + "\" " + text + " " + text2;
	}

	public override string GenerateCode()
	{
		return string.Format("{0}.{1}(\"{2}\", {3},  {4}, {5}{6})", "StoryCondition", "FlagSetSpecialWithTime", Flag, StoryCondition.CheckTypeToCode(CheckType), TimePeriod, StoryCondition.TimeCheckTypeToCode(TimeCheck), GenerateCodeNegationParameter());
	}

	public bool Equals(FlagConditionSpecialWithTime other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other) && Flag == other.Flag && CheckType == other.CheckType && TimePeriod == other.TimePeriod)
		{
			return TimeCheck == other.TimeCheck;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is FlagConditionSpecialWithTime other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Flag, (int)CheckType, TimePeriod, (int)TimeCheck);
	}
}
