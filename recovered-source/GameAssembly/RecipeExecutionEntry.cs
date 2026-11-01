using UnityEngine;

public class RecipeExecutionEntry
{
	public RecipeIngredient RelatedIngredient { get; private set; }

	public Selection<Item> ChosenItems { get; private set; }

	public int TargetItemCount => RelatedIngredient.Count;

	public int AssignedItemsCount => ChosenItems.SelectedItemsCount;

	public RecipeExecutionEntry(RecipeIngredient ingredient)
	{
		RelatedIngredient = ingredient;
		ChosenItems = SelectionFactory.CreateInstance<Item>();
		ChosenItems.MaxTotalItemCount = RelatedIngredient.Count;
	}

	public RecipeExecutionStatusEnum GetRecipeExecutionStatus()
	{
		if (RelatedIngredient.Count == AssignedItemsCount)
		{
			return RecipeExecutionStatusEnum.Full;
		}
		if (AssignedItemsCount > 0)
		{
			return RecipeExecutionStatusEnum.Partial;
		}
		return RecipeExecutionStatusEnum.Missing;
	}

	public Color GetRecipeExecutionStatusColor()
	{
		return GetRecipeExecutionStatus() switch
		{
			RecipeExecutionStatusEnum.Missing => RelatedIngredient.IsOptional ? Color.yellow : Color.red, 
			RecipeExecutionStatusEnum.Partial => Color.yellow, 
			RecipeExecutionStatusEnum.Full => Color.green, 
			_ => Color.magenta, 
		};
	}
}
