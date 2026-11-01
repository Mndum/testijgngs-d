using JetBrains.Annotations;

namespace ItemConditions;

public static class ItemScopeConditionsUtilities
{
	[MustUseReturnValue]
	public static ItemScopeCondition[] SetValueAtIndexCopy(this ItemScopeCondition[] conditions, int index, ItemScopeCondition value)
	{
		if (conditions.Length < index)
		{
			conditions[index] = value;
			return conditions;
		}
		int times = index - conditions.Length + 1;
		ItemScopeCondition[] array = conditions.AppendToArrayCopy(ItemScopeConditionAlwaysTrueCondition.Instance, times);
		array[index] = value;
		return array;
	}
}
