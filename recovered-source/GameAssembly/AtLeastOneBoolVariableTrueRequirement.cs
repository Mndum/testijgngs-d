using System.Collections.Generic;
using System.Linq;

public class AtLeastOneBoolVariableTrueRequirement : ItemModifiableVariablesRequirementWithTargetIndices
{
	public AtLeastOneBoolVariableTrueRequirement(int[] targetIndices)
		: base(targetIndices)
	{
	}

	public static AtLeastOneBoolVariableTrueRequirement CreateInstance(int[] targetIndices)
	{
		return new AtLeastOneBoolVariableTrueRequirement(targetIndices);
	}

	public override void Enforce(Item item, ItemPrefab itemPrefab, int lastModified)
	{
		List<ItemModifiableVariableIntBackedBool> list;
		using (MyCollectionPool<ItemModifiableVariableIntBackedBool>.GetList(out list))
		{
			FillTargetVariables(itemPrefab, list);
			foreach (ItemModifiableVariableIntBackedBool item2 in list)
			{
				if (item2.GetValue(item))
				{
					return;
				}
			}
			foreach (ItemModifiableVariableIntBackedBool item3 in list)
			{
				if (item3.GetValue(item))
				{
					item3.SetValueRaw(item, value: true);
					break;
				}
			}
		}
	}

	public override bool IsBoolChangeAllowed(Item item, ItemPrefab itemPrefab, int modifiableVariableIndex, bool newValue)
	{
		if (newValue)
		{
			return true;
		}
		if (!TargetVariableIndices.Contains(modifiableVariableIndex))
		{
			return true;
		}
		List<ItemModifiableVariableIntBackedBool> list;
		using (MyCollectionPool<ItemModifiableVariableIntBackedBool>.GetList(out list))
		{
			FillTargetVariables(itemPrefab, list);
			int num = 0;
			foreach (ItemModifiableVariableIntBackedBool item2 in list)
			{
				if (item2.GetValue(item))
				{
					num++;
					if (num >= 2)
					{
						break;
					}
				}
			}
			return num >= 2;
		}
	}

	public override string ToLuaString()
	{
		return "AtLeastOneBoolVariableTrueRequirement.CreateInstance(" + TargetVariableIndices.ToLuaString() + ")";
	}

	public override ItemModifiableVariablesRequirement Clone()
	{
		return new AtLeastOneBoolVariableTrueRequirement(TargetVariableIndices.ToArray());
	}
}
