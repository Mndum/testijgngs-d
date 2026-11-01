using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingResultsStaticGui : StaticGuiBase
{
	private StringBuilder stringBuilder;

	private const int TimePerDish = 120;

	private bool _speedUp;

	[Header("Auto")]
	[SerializeField]
	private ScrollRect A_Scroll_View;

	[SerializeField]
	private TextMeshProUGUI A_Result_Text;

	[SerializeField]
	private ButtonList A_Bottom_Buttons;

	private async UniTask DisplayAndShake()
	{
		A_Result_Text.SetText(stringBuilder);
		LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
		A_Scroll_View.ScrollToBottom();
		if (!_speedUp)
		{
			R.Sound.Generic_Whoosh_All.GetRandom().Get().Play();
			await (Tween)transform.DOShakePosition(0.2f, 20f, 20);
			await UniTask.Delay(50);
		}
	}

	private async UniTask DisplayResultsEnumerator(CookingResults results)
	{
		StringBuilderCache.AcquireManual(out stringBuilder);
		bool isPractice = results.ActiveModifiers.HasFlagFast(CookingGameplayModifiers.Practice);
		bool isAutoPlay = results.ActiveModifiers.HasFlagFast(CookingGameplayModifiers.AutoPlay);
		CookingGameplayScoreData score = results.Score;
		CookingSongSave save = GameVariables.Current.cookingMinigameManager.GetSave(results.Song);
		CookingGameplayScoreData perfectScore = results.StartInfo.Song.RuntimeData.GetPerfectScoreReadonly();
		CookingGrade grade = score.GetScoreGrade(perfectScore);
		save.SaveResults(results);
		save.TryGetBestScore(out var bestScore);
		save.TryGetBestAccuracyScore(out var bestAccuracyScore);
		save.TryGetBestDishScore(out var bestDishQualityScore, out var _);
		stringBuilder.TMPAppendHeaderSizeTag();
		stringBuilder.AppendLine(results.Song.GetMusic().Info);
		stringBuilder.TMPAppendCloseSizeTag();
		await DisplayAndShake();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.PassTimeInstantly(120, GameVariables.StaminaSyncType.Normal);
		stringBuilder.AppendStartDisplay(LOC.UI.Cooking_SpentTime);
		stringBuilder.AppendLine(GlobalFunctions.TimePeriodHours(120));
		await DisplayAndShake();
		if (!isAutoPlay)
		{
			if (isPractice)
			{
				stringBuilder.AppendLine(LOC.UI.CookingMinigame_ModifierPracticeInfoResults);
				await DisplayAndShake();
			}
			stringBuilder.AppendLine(LOC.UI.CookingMinigame_Results);
			await DisplayAndShake();
			if (score.ActiveModifiers != CookingGameplayModifiers.NoMods)
			{
				stringBuilder.AppendLineStringDisplay(LOC.UI.CookingSong_ActiveModifiers, score.ActiveModifiers.ToLocalizedString());
				await DisplayAndShake();
			}
			stringBuilder.AppendLineStringDisplay(LOC.UI.CookingMinigame_Grade, grade.ToFriendlyString());
			stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Great, score.GreatCount);
			await DisplayAndShake();
			stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Good, score.GoodCount);
			await DisplayAndShake();
			stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Bad, score.BadCount);
			await DisplayAndShake();
			stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Miss, score.MissCount);
			await DisplayAndShake();
			stringBuilder.AppendIntDisplay(LOC.UI.CookingMinigame_MaxCombo, score.MaxCombo);
			if (score.MaxCombo == perfectScore.MaxCombo)
			{
				stringBuilder.AppendColorTagStart(Color.green);
				stringBuilder.AppendSurroundParentheses(LOC.UI.CookingMinigame_FullCombo);
				stringBuilder.AppendColorTagEnd();
			}
			stringBuilder.AppendLine();
			await DisplayAndShake();
			stringBuilder.AppendIntDisplay(LOC.UI.CookingMinigame_Score, score.GetScoreWithMultipliers());
			if (bestScore == score)
			{
				stringBuilder.AppendColorTagStart(Color.green);
				stringBuilder.AppendSurroundParentheses(LOC.UI.CookingMinigame_NewRecord);
				stringBuilder.AppendColorTagEnd();
			}
			stringBuilder.AppendLine();
			await DisplayAndShake();
			stringBuilder.AppendPercentDisplay(LOC.UI.CookingMinigame_Accuracy, score.GetAccuracyScorePercent(perfectScore));
			if (bestAccuracyScore == score)
			{
				stringBuilder.AppendColorTagStart(Color.green);
				stringBuilder.AppendSurroundParentheses(LOC.UI.CookingMinigame_NewRecord);
				stringBuilder.AppendColorTagEnd();
			}
			stringBuilder.AppendLine();
			await DisplayAndShake();
			stringBuilder.AppendPercentDisplay(LOC.UI.DishQuality_Header, score.GetDishQuality(perfectScore));
			if (bestDishQualityScore == score)
			{
				stringBuilder.AppendColorTagStart(Color.green);
				stringBuilder.AppendSurroundParentheses(LOC.UI.CookingMinigame_NewRecord);
				stringBuilder.AppendColorTagEnd();
			}
			stringBuilder.AppendLine();
			await DisplayAndShake();
		}
		if (!results.Score.IsComplete)
		{
			stringBuilder.AppendColorTagStart(MyColor32Presets.Red);
			stringBuilder.AppendLine(LOC.UI.CookingMinigame_Incomplete);
			if (!isPractice)
			{
				foreach (Item item2 in results.StartInfo.RecipeExecution.GetConsumeItemsEnumerator())
				{
					_ = item2;
				}
				stringBuilder.AppendLine(LOC.UI.CookingMinigame_LostIngredients);
			}
		}
		else if (!isPractice)
		{
			RecipeExecution recipeExecution = results.StartInfo.RecipeExecution;
			float dishQuality = score.GetDishQuality(perfectScore);
			List<Item> list;
			using (MyCollectionPool<Item>.GetListAndFill(recipeExecution.GetConsumeItemsEnumerator(), out list))
			{
				Item item = results.StartInfo.Song.Recipe.CreateDish(results, list);
				item.FillDishData(dishQuality, list);
				GameVariables.Current.itemManager.AddItem(item);
				stringBuilder.AppendLine();
				stringBuilder.TMPAppendHeaderSizeTag();
				stringBuilder.AppendLine(LOC.UI.CookingMinigame_CookedDish);
				stringBuilder.TMPAppendCloseSizeTag();
				item.GetNameStringBuilder(stringBuilder);
				stringBuilder.AppendLine();
				item.GetDescriptionStringBuilder(stringBuilder);
				stringBuilder.AppendLine();
			}
		}
		await DisplayAndShake();
		IModificationPeriodButtonList modificationPeriod;
		using (A_Bottom_Buttons.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Alternative);
			buttonListWrapper.SetText(LOC.Polyglot.MENU_LABEL_EXIT);
			buttonListWrapper.OnClick = () =>
			{
				if (isPractice)
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new CookingSongSelectState());
				}
				else
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.GoHome();
				}
			};
		}
		StringBuilderCache.Release(stringBuilder);
	}

	public void Update()
	{
		if (Input.anyKeyDown)
		{
			_speedUp = true;
		}
	}

	public void DisplayResults(CookingResults results)
	{
		_speedUp = false;
		A_Bottom_Buttons.ResetButtons();
		DisplayResultsEnumerator(results).Forget();
	}
}
