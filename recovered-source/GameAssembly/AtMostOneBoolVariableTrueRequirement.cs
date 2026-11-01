using System;
using System.Collections.Generic;
using System.Linq;

public class AtMostOneBoolVariableTrueRequirement : ItemModifiableVariablesRequirementWithTargetIndices
{
	public AtMostOneBoolVariableTrueRequirement(int[] targetIndices)
		: base(targetIndices)
	{
	}

	public static AtMostOneBoolVariableTrueRequirement CreateInstance(int[] targetIndices)
	{
		return new AtMostOneBoolVariableTrueRequirement(targetIndices);
	}

	public override void Enforce(Item item, ItemPrefab itemPrefab, int lastModified)
	{
		List<ItemModifiableVariableIntBackedBool> list;
		using (MyCollectionPool<ItemModifiableVariableIntBackedBool>.GetList(out list))
		{
			FillTargetVariables(itemPrefab, list);
			if (list.Count == 0 || Enumerable.Count(list, (ItemModifiableVariableIntBackedBool v) => v.GetValue(item)) <= 1)
			{
				return;
			}
			int num = Array.IndexOf(TargetVariableIndices, lastModified);
			ItemModifiableVariableIntBackedBool itemModifiableVariableIntBackedBool = ((num >= 0) ? list[num] : null);
			if (itemModifiableVariableIntBackedBool != null && itemModifiableVariableIntBackedBool.GetValue(item))
			{
				foreach (ItemModifiableVariableIntBackedBool item2 in list)
				{
					if (item2 != itemModifiableVariableIntBackedBool)
					{
						item2.SetValueRaw(item, value: false);
					}
				}
				return;
			}
			bool flag = false;
			foreach (ItemModifiableVariableIntBackedBool item3 in list)
			{
				if (item3.GetValue(item))
				{
					if (!flag)
					{
						flag = true;
					}
					else
					{
						item3.SetValueRaw(item, value: false);
					}
				}
			}
		}
	}

	public override bool IsBoolChangeAllowed(Item item, ItemPrefab itemPrefab, int modifiableVariableIndex, bool newValue)
	{
		if (!TargetVariableIndices.Contains(modifiableVariableIndex))
		{
			return true;
		}
		return true;
	}

	public override string ToLuaString()
	{
		return "AtMostOneBoolVariableTrueRequirement.CreateInstance(" + TargetVariableIndices.ToLuaString() + ")";
	}

	public override ItemModifiableVariablesRequirement Clone()
	{
		return new AtMostOneBoolVariableTrueRequirement(TargetVariableIndices);
	}
}
