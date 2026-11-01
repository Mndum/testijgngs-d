using System;

namespace StoryConditions;

public sealed class ItemInSlotCondition : StoryCondition, IEquatable<ItemInSlotCondition>
{
	public readonly string Slot;

	public ItemInSlotCondition(bool negate, string slot)
		: base(negate)
	{
		Slot = slot;
	}

	protected override bool InnerConditionMet()
	{
		return GameVariables.Current.itemManager.GetFirstItemAtSlot(Slot) != null;
	}

	public override string Explain()
	{
		return "(" + (Negate ? "No" : "Any") + " item in slot \"" + Slot + "\")";
	}

	public override string GenerateCode()
	{
		return "(UNIMPLEMENTED)";
	}

	public bool Equals(ItemInSlotCondition other)
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
			return Slot == other.Slot;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is ItemInSlotCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Slot);
	}
}
