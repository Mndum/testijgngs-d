namespace ItemConditions;

public class ItemScopeConditionAlwaysTrueCondition : ItemScopeCondition
{
	public static readonly ItemScopeConditionAlwaysTrueCondition Instance = new ItemScopeConditionAlwaysTrueCondition();

	public override bool IsActive(Item item)
	{
		return true;
	}

	public override string ToLuaString()
	{
		return "ItemScopeConditionAlwaysTrueCondition.Instance";
	}

	public override ItemScopeCondition Clone()
	{
		return this;
	}
}
