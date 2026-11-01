using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace StoryConditions;

public static class ConditionExtensions
{
	public static string Explain(this IList<StoryCondition> conditions, bool richText, bool currentState)
	{
		if (conditions == null || conditions.Count == 0)
		{
			return "No conditions!";
		}
		if (richText)
		{
			if (currentState)
			{
				return string.Join(" and ".TMPColorSurround(Color.blue), conditions.Select((StoryCondition t) => t.ConditionMet() ? t.Explain().TMPColorSurround(Color.green) : t.Explain().TMPColorSurround(Color.red)));
			}
			return string.Join(" and ".TMPColorSurround(Color.blue), conditions.Select((StoryCondition t) => t.Explain()));
		}
		return string.Join(" and ", conditions.Select((StoryCondition t) => t.Explain()));
	}

	public static bool ConditionsMet(this IEnumerable<StoryCondition> conditions)
	{
		return conditions?.All((StoryCondition x) => x.ConditionMet()) ?? true;
	}

	public static bool ConditionsMet(this StoryCondition[] conditions)
	{
		if (conditions == null)
		{
			return true;
		}
		for (int i = 0; i < conditions.Length; i++)
		{
			if (!conditions[i].ConditionMet())
			{
				return false;
			}
		}
		return true;
	}

	[MustUseReturnValue]
	public static StoryCondition[] AddConditionsAtEnd([NotNull] this StoryCondition[] conditions, [NotNull] params StoryCondition[] toAdd)
	{
		if (toAdd.Length == 0)
		{
			return conditions;
		}
		if (conditions.Length == 0)
		{
			return toAdd;
		}
		return conditions.Concat(toAdd).ToArray();
	}

	[MustUseReturnValue]
	public static StoryCondition[] AddConditionsAtFront([NotNull] this StoryCondition[] conditions, [NotNull] params StoryCondition[] toAdd)
	{
		if (toAdd.Length == 0)
		{
			return conditions;
		}
		if (conditions.Length == 0)
		{
			return toAdd;
		}
		return toAdd.Concat(conditions).ToArray();
	}
}
