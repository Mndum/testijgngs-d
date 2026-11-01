using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class RecipeExecution
{
	public readonly Recipe OriginalRecipe;

	private Selection<Item> _allItems = SelectionFactory.CreateInstance<Item>();

	private List<RecipeExecutionEntry> _recipeExecutionEntries;

	public Selection<Item> AllChosenItems => _allItems;

	public IReadOnlyCollection<RecipeExecutionEntry> RecipeExecutionEntries => _recipeExecutionEntries;

	public RecipeExecution(Recipe originalRecipe)
	{
		OriginalRecipe = originalRecipe;
		_recipeExecutionEntries = new List<RecipeExecutionEntry>();
		foreach (RecipeIngredient allIngredient in OriginalRecipe.AllIngredients)
		{
			_recipeExecutionEntries.Add(new RecipeExecutionEntry(allIngredient));
		}
	}

	public bool CanBeCooked()
	{
		foreach (RecipeExecutionEntry recipeExecutionEntry in _recipeExecutionEntries)
		{
			if (recipeExecutionEntry.GetRecipeExecutionStatus() != RecipeExecutionStatusEnum.Full && !recipeExecutionEntry.RelatedIngredient.IsOptional)
			{
				return false;
			}
		}
		return true;
	}

	public IEnumerable<Item> GetConsumeItemsEnumerator()
	{
		Item.ItemManager itemManager = GameVariables.Current.itemManager;
		foreach (RecipeExecutionEntry recipeExecutionEntry in _recipeExecutionEntries)
		{
			foreach (var (item2, count) in recipeExecutionEntry.ChosenItems.GetAllItemsWithCounts())
			{
				if (itemManager.ConsumeItem(item2, count))
				{
					item2.SetCount(count);
					yield return item2;
				}
				else
				{
					Item item3 = item2.Clone();
					item3.SetCount(count);
					yield return item3;
				}
			}
		}
	}

	public void SetItems(int index, Selection<Item> itemSelection)
	{
		if (!_recipeExecutionEntries.TryGetValue(index, out var value))
		{
			return;
		}
		RecipeIngredient relatedIngredient = value.RelatedIngredient;
		ClearAssignedItems(value);
		foreach (var (item2, num2) in itemSelection.GetAllItemsWithCounts())
		{
			AddItemToEntryInternal(value, item2, Mathf.Min(num2, relatedIngredient.Count, item2.GetCount()));
		}
	}

	public void ClearAssignedItems(int index)
	{
		if (_recipeExecutionEntries.TryGetValue(index, out var value))
		{
			ClearAssignedItems(value);
		}
	}

	public void FillRecipeExecution(Item.ItemManager itemManager)
	{
		ReadOnlyCollection<RecipeIngredient> allIngredients = OriginalRecipe.AllIngredients;
		for (int i = 0; i < _recipeExecutionEntries.Count; i++)
		{
			RecipeIngredient recipeIngredient = allIngredients[i];
			RecipeExecutionEntry recipeExecutionEntry = _recipeExecutionEntries[i];
			if (recipeIngredient.IsOptional)
			{
				continue;
			}
			using IEnumerator<Item> enumerator = recipeIngredient.FindInInventory(itemManager).GetEnumerator();
			while (recipeExecutionEntry.AssignedItemsCount < recipeIngredient.Count && enumerator.MoveNext())
			{
				Item current = enumerator.Current;
				int num = current.GetCount() - _allItems.GetItemSelectedCount(current);
				if (num > 0)
				{
					int count = Mathf.Min(recipeIngredient.Count - recipeExecutionEntry.AssignedItemsCount, num);
					AddItemToEntryInternal(recipeExecutionEntry, current, count);
				}
			}
		}
	}

	private void AddItemToEntryInternal(RecipeExecutionEntry entry, Item item, int count)
	{
		int toAddCount = _allItems.AddItem(item, count);
		entry.ChosenItems.AddItem(item, toAddCount);
	}

	public void ClearAssignedItems(RecipeExecutionEntry entry)
	{
		foreach (var (item2, toRemoveCount) in entry.ChosenItems.GetAllItemsWithCounts())
		{
			_allItems.RemoveItem(item2, toRemoveCount);
		}
		entry.ChosenItems.Clear();
	}
}
