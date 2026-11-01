using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingSongDisplay : AdvancedItemListPrefab
{
	[SerializeField]
	private TextMeshProUGUI cookingInfo;

	[SerializeField]
	private TextMeshProUGUI songInfo;

	[SerializeField]
	private Button mainButton;

	public override bool HasDefaultButton => true;

	public override Button GetDefaultButton()
	{
		return mainButton;
	}

	public void SetData((CookingGameplayModifiers, CookingSong) newValue)
	{
		var (modifiers, cookingSong) = newValue;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(cookingSong.GetMusic().Info);
			CookingSongRuntimeData runtimeData = cookingSong.RuntimeData;
			CookingSongDifficulty difficulty = runtimeData.GetDifficulty(modifiers);
			stringBuilder.AppendFormat("{0} {1:0.00}*\n", LOC.UI.CookingSong_Difficulty, difficulty.SongDifficulty);
			runtimeData.GetSongLengthDiscreet(modifiers, out var minutes, out var seconds);
			stringBuilder.AppendFormat("{0} {1:0}:{2:00}\n", LOC.UI.CookingSong_Length, minutes, seconds);
			songInfo.SetText(stringBuilder);
			stringBuilder.Clear();
			if (cookingSong.IsUnlocked)
			{
				CookingSongSave save = GameVariables.Current.cookingMinigameManager.GetSave(cookingSong);
				RecipeExecution recipeExecution = new RecipeExecution(cookingSong.Recipe);
				stringBuilder.Append(cookingSong.Recipe.ExpectedResultPrefab.Name);
				if (save.TimesPlayed == 0)
				{
					stringBuilder.AppendColorTagStart(MyColor32Presets.Green);
					stringBuilder.Append("(");
					stringBuilder.Append(LOC.UI.CookingSong_NewDish);
					stringBuilder.Append(")");
					stringBuilder.AppendColorTagEnd();
				}
				stringBuilder.AppendLine();
				recipeExecution.FillRecipeExecution(GameVariables.Current.itemManager);
				if (recipeExecution.CanBeCooked())
				{
					stringBuilder.AppendColorTagStart(MyColor32Presets.Green);
					stringBuilder.AppendLine(LOC.UI.CookingSong_ReadyToCook);
				}
				else
				{
					stringBuilder.AppendColorTagStart(MyColor32Presets.Red);
					stringBuilder.AppendLine(LOC.UI.CookingSong_MissingIngredients);
				}
				stringBuilder.AppendColorTagEnd();
				if (save.TryGetBestGradeScore(out var _, out var _bestGrade))
				{
					stringBuilder.Append(LOC.UI.CookingSong_BestGrade);
					stringBuilder.Append(": ");
					stringBuilder.AppendColorTagStart(_bestGrade.GradeToColor());
					stringBuilder.Append(_bestGrade.ToFriendlyString());
					stringBuilder.AppendColorTagEnd();
					stringBuilder.AppendLine();
				}
			}
			else
			{
				stringBuilder.AppendLine(LOC.UI.CookingSong_LockedDIsh);
			}
			cookingInfo.SetText(stringBuilder);
		}
	}
}
