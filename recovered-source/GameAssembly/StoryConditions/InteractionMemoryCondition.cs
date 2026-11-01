using System;

namespace StoryConditions;

public sealed class InteractionMemoryCondition : StoryCondition, IEquatable<InteractionMemoryCondition>
{
	public enum MemoryConditionTypeEnum
	{
		ActiveFor,
		ActiveOrRecent
	}

	public readonly InteractionMemoryEnum MemoryType;

	public readonly MemoryConditionTypeEnum ConditionType;

	public readonly float Time;

	public InteractionMemoryCondition(InteractionMemoryEnum memoryType, MemoryConditionTypeEnum conditionType, float time, bool negate = false)
		: base(negate)
	{
		MemoryType = memoryType;
		ConditionType = conditionType;
		Time = time;
	}

	protected override bool InnerConditionMet()
	{
		if (ModelBrain.TryGet("bot", out var brain))
		{
			ComplexEventMemory complexMemory = brain.InteractionMemory.GetComplexMemory(MemoryType);
			return ConditionType switch
			{
				MemoryConditionTypeEnum.ActiveFor => complexMemory.BeenActiveFor(Time), 
				MemoryConditionTypeEnum.ActiveOrRecent => complexMemory.IsActiveOrRecent(Time), 
				_ => false, 
			};
		}
		return false;
	}

	public override string Explain()
	{
		string text = MemoryType.ToString();
		string text2 = ConditionType switch
		{
			MemoryConditionTypeEnum.ActiveFor => $"active for {Time} seconds", 
			MemoryConditionTypeEnum.ActiveOrRecent => $"active or recent (within {Time} seconds)", 
			_ => "unknown", 
		};
		return "(" + (Negate ? " not" : "") + " " + text + " " + text2 + ")";
	}

	public override string GenerateCode()
	{
		return "(UNIMPLEMENTED)";
	}

	public bool Equals(InteractionMemoryCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other) && MemoryType == other.MemoryType && ConditionType == other.ConditionType)
		{
			float time = Time;
			return time.Equals(other.Time);
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is InteractionMemoryCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, (int)MemoryType, (int)ConditionType, Time);
	}
}
