using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingRecipeExecutionEntryDisplay : AdvancedItemListPrefab
{
	[SerializeField]
	private TextMeshProUGUI recipeIngredientInfo;

	[SerializeField]
	private TextMeshProUGUI gotIngredientInfo;

	[SerializeField]
	private Button mainButton;

	[SerializeField]
	private Button helpButton;

	private RecipeExecutionEntry _currentExecution;

	public override bool HasDefaultButton => true;

	private void Awake()
	{
		helpButton.onClick.SetListener(HelpButtonClicked);
	}

	private void HelpButtonClicked()
	{
		if (_currentExecution.RelatedIngredient != null && _currentExecution.RelatedIngredient.RequiresHelp)
		{
			UiOverlay.Instance.OkPopup(LOC.UI.HairSettings_Help, _currentExecution.RelatedIngredient.GetHelpDescription());
		}
	}

	public override Button GetDefaultButton()
	{
		return mainButton;
	}

	public override void AfterReturnedToPool()
	{
		base.AfterReturnedToPool();
		_currentExecution = null;
	}

	public void SetData(RecipeExecutionEntry execution)
	{
		_currentExecution = execution;
		RecipeIngredient relatedIngredient = execution.RelatedIngredient;
		if (relatedIngredient.RequiresHelp)
		{
			recipeIngredientInfo.rectTransform.SetRight(42f);
			helpButton.gameObject.SetActive(value: true);
		}
		else
		{
			recipeIngredientInfo.rectTransform.SetRight(10f);
			helpButton.gameObject.SetActive(value: false);
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			Color recipeExecutionStatusColor = execution.GetRecipeExecutionStatusColor();
			recipeIngredientInfo.faceColor = recipeExecutionStatusColor;
			gotIngredientInfo.faceColor = recipeExecutionStatusColor;
			relatedIngredient.AppendName(stringBuilder);
			recipeIngredientInfo.SetText(stringBuilder);
			stringBuilder.Clear();
			if (execution.ChosenItems.SelectedUniqueItemCount > 0)
			{
				stringBuilder.Append(">");
				execution.ChosenItems.AppendDescribeSelection(stringBuilder);
			}
			stringBuilder.AppendColorTagEnd();
			stringBuilder.AppendLine();
			gotIngredientInfo.SetText(stringBuilder);
		}
	}
}
