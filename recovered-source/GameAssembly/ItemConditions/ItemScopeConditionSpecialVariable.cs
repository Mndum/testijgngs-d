namespace ItemConditions;

public class ItemScopeConditionSpecialVariable : ItemScopeCondition
{
	public string VariableName;

	public ItemScopeConditionSpecialVariable(string variableName)
	{
		VariableName = variableName;
	}

	public static ItemScopeConditionSpecialVariable CreateInstance(string variableName)
	{
		return new ItemScopeConditionSpecialVariable(variableName);
	}

	public override bool IsActive(Item item)
	{
		ItemModifiableVariable[] itemModifiableVariables = item.GetItemPrefab().ItemModifiableVariables;
		foreach (ItemModifiableVariable itemModifiableVariable in itemModifiableVariables)
		{
			if (itemModifiableVariable.VarId == VariableName && itemModifiableVariable is ItemModifiableVariableIntBackedBool itemModifiableVariableIntBackedBool)
			{
				return itemModifiableVariableIntBackedBool.GetValue(item);
			}
		}
		return item.GetIntSpecialVariableOrDefaultCastBool(VariableName);
	}

	public override string ToLuaString()
	{
		return "ItemScopeConditionSpecialVariable.CreateInstance(" + VariableName.ToLuaString() + ")";
	}

	public override ItemScopeCondition Clone()
	{
		return (ItemScopeCondition)MemberwiseClone();
	}
}
