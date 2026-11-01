using System;
using System.Collections.Generic;

public abstract class ItemModifiableVariablesRequirementWithTargetIndices : ItemModifiableVariablesRequirement
{
	private int[] _targetVariableIndices;

	public int[] TargetVariableIndices
	{
		get
		{
			return _targetVariableIndices;
		}
		set
		{
			_targetVariableIndices = value ?? Array.Empty<int>();
		}
	}

	public ItemModifiableVariablesRequirementWithTargetIndices(int[] targetVariableIndices)
	{
		TargetVariableIndices = targetVariableIndices;
	}

	protected void FillTargetVariables(ItemPrefab itemPrefab, List<ItemModifiableVariableIntBackedBool> output)
	{
		ItemModifiableVariable[] itemModifiableVariables = itemPrefab.ItemModifiableVariables;
		int[] targetVariableIndices = _targetVariableIndices;
		foreach (int at in targetVariableIndices)
		{
			if (itemModifiableVariables.TryGetValue(at, out var value) && value is ItemModifiableVariableIntBackedBool item)
			{
				output.Add(item);
			}
		}
	}
}
