using System;
using UnityEngine;

namespace StoryConditions;

public sealed class StoryEntryPointOptionsExistCondition : StoryCondition, IEquatable<StoryEntryPointOptionsExistCondition>
{
	public enum EntryTypeEnum
	{
		Auto,
		Menu,
		MenuConditional
	}

	public readonly EntryTypeEnum EntryType;

	public readonly string EntryPointName;

	public StoryEntryPointOptionsExistCondition(EntryTypeEnum entryType, string entryPointName, bool negate)
		: base(negate)
	{
		EntryType = entryType;
		EntryPointName = entryPointName;
	}

	protected override bool InnerConditionMet()
	{
		switch (EntryType)
		{
		case EntryTypeEnum.Auto:
		{
			StoryAutoEntryPoint storyAutoEntryPoint = SingletonMonoBehaviour<StorySingleton>.Instance.GetStoryAutoEntryPoint(EntryPointName);
			if (storyAutoEntryPoint == null)
			{
				Debug.LogWarning("Auto entry point \"" + EntryPointName + "\" not found!");
				return false;
			}
			return storyAutoEntryPoint.AnyPossible();
		}
		case EntryTypeEnum.Menu:
		case EntryTypeEnum.MenuConditional:
		{
			StoryMenuEntryPoint storyMenuEntryPoint = SingletonMonoBehaviour<StorySingleton>.Instance.GetStoryMenuEntryPoint(EntryPointName);
			if (storyMenuEntryPoint == null)
			{
				Debug.LogWarning("Menu entry point \"" + EntryPointName + "\" not found!");
				return false;
			}
			if (EntryType == EntryTypeEnum.MenuConditional)
			{
				return storyMenuEntryPoint.AnyWithConditionsOrFlagPossible();
			}
			return storyMenuEntryPoint.AnyPossible();
		}
		default:
			return false;
		}
	}

	public override string Explain()
	{
		return EntryType switch
		{
			EntryTypeEnum.Auto => "(StoryAutoEntryPoint \"" + EntryPointName + "\" has any options)", 
			EntryTypeEnum.Menu => "(StoryMenuEntryPoint \"" + EntryPointName + "\" has any options)", 
			EntryTypeEnum.MenuConditional => "(StoryMenuEntryPoint \"" + EntryPointName + "\" has any option(with conditions or flag))", 
			_ => "ERROR: Invalid entry type!", 
		};
	}

	public override string GenerateCode()
	{
		return "//Unimplemented";
	}

	public bool Equals(StoryEntryPointOptionsExistCondition other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Equals((StoryCondition)other) && EntryType == other.EntryType)
		{
			return EntryPointName == other.EntryPointName;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is StoryEntryPointOptionsExistCondition other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(EntryType, Negate, EntryPointName);
	}
}
