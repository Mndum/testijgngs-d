using System;
using System.Collections.Generic;
using System.Linq;

public class ExactlyOneBoolVariableTrueRequirement : ItemModifiableVariablesRequirementWithTargetIndices
{
	public ExactlyOneBoolVariableTrueRequirement(int[] targetIndices)
		: base(targetIndices)
	{
	}

	public static ExactlyOneBoolVariableTrueRequirement CreateInstance(int[] targetIndices)
	{
		return new ExactlyOneBoolVariableTrueRequirement(targetIndices);
	}

	public override void Enforce(Item item, ItemPrefab itemPrefab, int lastModified)
	{
		List<ItemModifiableVariableIntBackedBool> list;
		using (MyCollectionPool<ItemModifiableVariableIntBackedBool>.GetList(out list))
		{
			FillTargetVariables(itemPrefab, list);
			if (list.Count == 0)
			{
				return;
			}
			int num = Enumerable.Count(list, (ItemModifiableVariableIntBackedBool v) => v.GetValue(item));
			if (num == 1)
			{
				return;
			}
			int num2 = Array.IndexOf(TargetVariableIndices, lastModified);
			ItemModifiableVariableIntBackedBool itemModifiableVariableIntBackedBool = ((num2 >= 0) ? list[num2] : null);
			switch (num)
			{
			case 0:
				foreach (ItemModifiableVariableIntBackedBool item2 in list)
				{
					if (item2 != itemModifiableVariableIntBackedBool)
					{
						item2.SetValueRaw(item, value: true);
						return;
					}
				}
				list[0].SetValueRaw(item, value: true);
				return;
			case 2:
				if (itemModifiableVariableIntBackedBool == null || !itemModifiableVariableIntBackedBool.GetValue(item))
				{
					break;
				}
				{
					foreach (ItemModifiableVariableIntBackedBool item3 in list)
					{
						if (item3 != itemModifiableVariableIntBackedBool)
						{
							item3.SetValueRaw(item, value: false);
						}
					}
					return;
				}
			}
			list[0].SetValueRaw(item, value: true);
			for (int num3 = 1; num3 < list.Count; num3++)
			{
				list[num3].SetValueRaw(item, value: false);
			}
		}
	}

	public override bool IsBoolChangeAllowed(Item item, ItemPrefab itemPrefab, int modifiableVariableIndex, bool newValue)
	{
		TargetVariableIndices.Contains(modifiableVariableIndex);
		return true;
	}

	public override string ToLuaString()
	{
		return "ExactlyOneBoolVariableTrueRequirement.CreateInstance(" + TargetVariableIndices.ToLuaString() + ")";
	}

	public override ItemModifiableVariablesRequirement Clone()
	{
		return new ExactlyOneBoolVariableTrueRequirement(TargetVariableIndices);
	}
}
