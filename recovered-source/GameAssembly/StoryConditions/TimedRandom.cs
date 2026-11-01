using System;
using UnityEngine;

namespace StoryConditions;

public sealed class TimedRandom : StoryCondition, IEquatable<TimedRandom>
{
	public readonly TimedRandomType Type;

	public readonly int Seed;

	public readonly float Odds;

	public TimedRandom(TimedRandomType type, int seed, float odds, bool negate = false)
		: base(negate)
	{
		Type = type;
		Seed = seed;
		Odds = Mathf.Clamp01(odds);
	}

	protected override bool InnerConditionMet()
	{
		GameVariables current = GameVariables.Current;
		int seed = Type switch
		{
			TimedRandomType.HourDay => HashCode.Combine(current.Hour, current.Day, Seed), 
			TimedRandomType.Day => HashCode.Combine(current.Day, Seed), 
			_ => 0, 
		};
		UnityEngine.Random.State state = UnityEngine.Random.state;
		UnityEngine.Random.InitState(seed);
		float value = UnityEngine.Random.value;
		UnityEngine.Random.state = state;
		return value < Odds;
	}

	public override string Explain()
	{
		string arg = ((Type == TimedRandomType.HourDay) ? "hour" : "day");
		float num = ((!Negate) ? Odds : (1f - Odds));
		string arg2;
		if (num >= 1f)
		{
			arg2 = "always";
		}
		else
		{
			arg2 = ((num <= 0f) ? "never" : $"{num:P0} chance each {arg}");
		}
		return $"random check passes ({arg2}, seed: {Seed})";
	}

	public override string GenerateCode()
	{
		return "(UNIMPLEMENTED)";
	}

	public bool Equals(TimedRandom other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other) && Type == other.Type && Seed == other.Seed)
		{
			float odds = Odds;
			return odds.Equals(other.Odds);
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as TimedRandom);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), Type, Seed, Odds);
	}
}
