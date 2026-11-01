using System;

namespace ItemConditions;

public class ItemScopeConditionCustom : ItemScopeCondition
{
	public Func<Item, bool> ConditionFunc;

	private ItemScopeConditionCustom(Func<Item, bool> conditionFunc)
	{
		ConditionFunc = conditionFunc;
	}

	public static ItemScopeConditionCustom CreateInstance(Func<Item, bool> conditionFunc)
	{
		return new ItemScopeConditionCustom(conditionFunc);
	}

	public override bool IsActive(Item item)
	{
		if (ConditionFunc != null)
		{
			return ConditionFunc(item);
		}
		return false;
	}

	public override string ToLuaString()
	{
		return "ItemScopeConditionCustom.CreateInstance(function() return true end)";
	}

	public override ItemScopeCondition Clone()
	{
		return (ItemScopeCondition)MemberwiseClone();
	}
}
