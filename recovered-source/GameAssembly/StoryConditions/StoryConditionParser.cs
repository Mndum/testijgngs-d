using System;
using System.Linq;

namespace StoryConditions;

public static class StoryConditionParser
{
	private static void FormatError(string text, string fullRawInput)
	{
		DialogueChain.FormatError(text, fullRawInput);
	}

	private static StoryCondition FlagCreate(string[] data, string fullrawinput)
	{
		if (data.Length == 2 || data.Length == 4)
		{
			string text = data[0];
			string flag = data[1];
			bool negate;
			if (!(text == "isset"))
			{
				if (!(text == "isnotset"))
				{
					FormatError("Wrong command!", fullrawinput);
					return null;
				}
				negate = true;
			}
			else
			{
				negate = false;
			}
			if (data.Length != 4)
			{
				return StoryCondition.FlagSet(flag);
			}
			if (Enum.TryParse<GameVariables.TimeCheckType>(data[2], ignoreCase: true, out var result))
			{
				if (int.TryParse(data[3], out var result2))
				{
					return StoryCondition.FlagSetWithTime(flag, result2, result, negate);
				}
				FormatError("'" + data[3] + "' is not a number", fullrawinput);
			}
			else
			{
				FormatError("'" + data[2] + "' is not a TimeCheckType!", fullrawinput);
			}
		}
		FormatError("Wrong amount of parameters!", fullrawinput);
		return null;
	}

	private static StoryCondition LuaCreate(string[] data, string fullrawinput)
	{
		LuaStoryCondition luaStoryCondition = StoryCondition.Lua(string.Join(",", data));
		try
		{
			luaStoryCondition.GenerateClosureUnsafe();
		}
		catch (Exception ex)
		{
			FormatError(ex.ToString(), fullrawinput);
		}
		return luaStoryCondition;
	}

	public static StoryCondition TryCreate(string[] data, string fullrawinput)
	{
		if (data.Length < 2)
		{
			FormatError("Not enough arguments in the condition", fullrawinput);
			return null;
		}
		string text = data[1].TrimStart(' ');
		string[] array = data.Skip(2).ToArray();
		if (text == "lua")
		{
			return LuaCreate(array, fullrawinput);
		}
		if (text == "flag")
		{
			return FlagCreate(array, fullrawinput);
		}
		try
		{
			if (DataConditionValues.TryCreate(text, out DataConditionValues.DataConditionValue<int> value))
			{
				var (comparator, negate) = DataConditionComparators.GetDataConditionComparator<int>(array);
				return new DataCondition<int>(value, comparator, negate);
			}
			if (DataConditionValues.TryCreate(text, out DataConditionValues.DataConditionValue<long> value2))
			{
				var (comparator2, negate2) = DataConditionComparators.GetDataConditionComparator<long>(array);
				return new DataCondition<long>(value2, comparator2, negate2);
			}
			if (DataConditionValues.TryCreate(text, out DataConditionValues.DataConditionValue<float> value3))
			{
				var (comparator3, negate3) = DataConditionComparators.GetDataConditionComparator<float>(array);
				return new DataCondition<float>(value3, comparator3, negate3);
			}
			if (DataConditionValues.TryCreate(text, out DataConditionValues.DataConditionValue<bool> value4))
			{
				if (data.Length == 2)
				{
					return new DataCondition<bool>(value4, new DataConditionComparators.EqualityConditionComparator<bool>(compareValue: true));
				}
				var (comparator4, negate4) = DataConditionComparators.GetDataConditionComparator<bool>(array);
				return new DataCondition<bool>(value4, comparator4, negate4);
			}
		}
		catch (ArgumentException ex)
		{
			FormatError(ex.ToString(), fullrawinput);
			return null;
		}
		FormatError("No data source named \"" + text + "\" found!", fullrawinput);
		return null;
	}
}
