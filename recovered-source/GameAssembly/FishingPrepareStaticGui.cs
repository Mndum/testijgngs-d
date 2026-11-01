using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class FishingPrepareStaticGui : StaticGuiBase
{
	[SerializeField]
	private RectTransform fishingLocationFolderContent;

	[SerializeField]
	private RectTransform fishingRodSelectionFolder;

	[SerializeField]
	private RectTransform baitSelectionFolder;

	[SerializeField]
	private ItemSlotPicker fishingRodPicker;

	[SerializeField]
	private ItemSlotPicker baitPicker;

	private FishingStartInfo _fishingStartInfo;

	private List<FishingLocationData> fishingLocationsSaved = new List<FishingLocationData>();

	private Action<FishingStartInfo> _finishedCallback;

	[Header("Auto")]
	[SerializeField]
	private TextMeshProUGUI A_Main_Summry_Text;

	[SerializeField]
	private BetterButton A_Journal_Button;

	[SerializeField]
	private TMP_Dropdown A_Location_Dropdown;

	[SerializeField]
	private TextMeshProUGUI A_Location_Text;

	[SerializeField]
	private ButtonList A_Location_ButtonList;

	[SerializeField]
	private BetterButton A_Help_Button;

	[SerializeField]
	private BetterButton A_Return_Button;

	private Color ScoreToColor(float score, bool is10Good = true)
	{
		if (!is10Good)
		{
			score = 10f - score;
		}
		if (score < 5f)
		{
			return Color.Lerp(Color.red, Color.yellow, score / 5f);
		}
		return Color.Lerp(Color.yellow, Color.green, (score - 5f) / 5f);
	}

	private void SetSelectedLocation(FishingLocationData fishingLocationData, int index = -1)
	{
		_fishingStartInfo.FishingLocation = fishingLocationData;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append("<size=150%>");
			stringBuilder.AppendLine(fishingLocationData.Name);
			stringBuilder.Append("</size>");
			stringBuilder.AppendLine(fishingLocationData.Description);
			stringBuilder.AppendColorTagStart(ScoreToColor(fishingLocationData.Cleanliness));
			stringBuilder.AppendFormat("{0}: {1:0}/10\n", LOC.UI.Fishing_FishingSpot_Cleanliness, fishingLocationData.Cleanliness);
			stringBuilder.AppendColorTagEnd();
			stringBuilder.AppendColorTagStart(ScoreToColor(fishingLocationData.FishQuantity));
			stringBuilder.AppendFormat("{0}: {1:0}/10\n", LOC.UI.Fishing_FishingSpot_FishQuantity, fishingLocationData.FishQuantity);
			stringBuilder.AppendColorTagEnd();
			stringBuilder.AppendColorTagStart(ScoreToColor(fishingLocationData.FishSizePotential));
			stringBuilder.AppendFormat("{0}: {1:0}/10\n", LOC.UI.Fishing_FishingSpot_FishSize, fishingLocationData.FishSizePotential);
			stringBuilder.AppendColorTagEnd();
			if (fishingLocationData.PriceToEnter > 0)
			{
				stringBuilder.Append(LOC.UI.Fishing_FishingSpot_Price(fishingLocationData.PriceToEnter));
				stringBuilder.AppendLine();
			}
			if (!GameVariables.Current.EnoughStaminaForLongActivity)
			{
				stringBuilder.AppendColorTagStart(Color.red);
				stringBuilder.AppendLine(LOC.UI.TooTiredToFishInfo);
				stringBuilder.AppendColorTagEnd();
			}
			A_Location_Text.SetText(stringBuilder);
			if (index == -1)
			{
				index = fishingLocationsSaved.IndexOf(fishingLocationData);
			}
			A_Location_Dropdown.SetValueWithoutNotify(index);
		}
	}

	private void SetSelectedLocation(int index)
	{
		if (fishingLocationsSaved.TryGetValue(index, out var value))
		{
			SetSelectedLocation(value, index);
		}
	}

	private void PrepareLocationDropdown()
	{
		fishingLocationsSaved.Clear();
		List<TMP_Dropdown.OptionData> options = A_Location_Dropdown.options;
		options.Clear();
		foreach (FishingLocationData allUnlockedLocation in FishingLocationDataManager.GetAllUnlockedLocations())
		{
			options.Add(new TMP_Dropdown.OptionData(allUnlockedLocation.Name));
			fishingLocationsSaved.Add(allUnlockedLocation);
		}
		A_Location_Dropdown.options = options;
		A_Location_Dropdown.onValueChanged.SetListener(SetSelectedLocation);
		int index = UnityEngine.Random.Range(0, options.Count);
		SetSelectedLocation(fishingLocationsSaved[index], index);
	}

	private void FishButtonClicked()
	{
		if (!GameVariables.Current.EnoughStaminaForLongActivity)
		{
			UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.TooTiredToFishInfo);
		}
		else if (GameVariables.Current.itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.Anon_FishingRod.Id) == null)
		{
			UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.FishingNoFishingRodEquippedPrompt);
		}
		else if (_fishingStartInfo.IsValid())
		{
			if (_fishingStartInfo.FishingLocation.PriceToEnter > 0)
			{
				UiOverlay.Instance.BuyPopup(LOC.UI.UI_AreYouSure, _fishingStartInfo.FishingLocation.PriceToEnter, StartFishing);
			}
			else
			{
				StartFishing();
			}
		}
	}

	private void UpdateFishingButtons()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (A_Location_ButtonList.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Green);
			buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Fishing_StartFishing);
			buttonListWrapper.OnClick = FishButtonClicked;
		}
	}

	private void UpdateFishingSummary()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			FishingMinigameManager fishingMinigameManager = GameVariables.Current.fishingMinigameManager;
			HashSet<GameId> fishIds;
			using (FishingLocationDataManager.GetAllPossibleFishes(out fishIds))
			{
				stringBuilder.AppendLineIntDisplay(LOC.UI.Fishing_TotalFishesCaught, fishingMinigameManager.FishCaughtCount);
				stringBuilder.AppendStartDisplay(LOC.UI.Fishing_DifferentFishesCaught);
				stringBuilder.Append(fishingMinigameManager.DifferentFishesCaughtCount);
				stringBuilder.Append("/");
				stringBuilder.Append(fishIds.Count);
				stringBuilder.AppendLine();
				stringBuilder.AppendStartDisplay(LOC.UI.Fishing_BigFishesCaught);
				stringBuilder.Append(fishingMinigameManager.BigFishesCaughtCount);
				stringBuilder.Append("/");
				stringBuilder.Append(fishIds.Count);
				stringBuilder.AppendLine();
				float fishJointStatBoost = GameVariables.Current.FishJointStatBoost;
				if (!Mathf.Approximately(fishJointStatBoost, 1f))
				{
					stringBuilder.AppendStartDisplay(LOC.UI.FishingFishJointStatBoost);
					using (stringBuilder.StartColorTagUsing((fishJointStatBoost > 1f) ? Color.green : Color.red))
					{
						stringBuilder.AppendPercent(fishJointStatBoost - 1f);
					}
				}
				A_Main_Summry_Text.SetText(stringBuilder);
			}
		}
	}

	private void UpdateEverything()
	{
		UpdateFishingSummary();
		UpdateFishingButtons();
	}

	private void StartFishing()
	{
		_fishingStartInfo.FishingLengthMinutes = 60;
		OnFinishedStartFish();
	}

	private void OnFinishedStartFish()
	{
		OnFinished(_fishingStartInfo);
	}

	private void OnFinishedReturn()
	{
		OnFinished(null);
	}

	private void OnFinished(FishingStartInfo startInfo)
	{
		if (_finishedCallback != null)
		{
			_finishedCallback(startInfo);
			_finishedCallback = null;
		}
	}

	public void Init(Action<FishingStartInfo> onFinished)
	{
		_fishingStartInfo = new FishingStartInfo();
		_finishedCallback = onFinished;
		A_Journal_Button.onClick.SetListener(() =>
		{
			UiOverlay.Instance.OpenFishingJournal();
		});
		A_Return_Button.onClick.SetListener(OnFinishedReturn);
		A_Help_Button.onClick.SetListener(OnHelpClicked);
		fishingRodPicker.SetTargetSlot(ItemSlot.SlotManager.Anon_FishingRod, () => LOC.UI.FishingNoFishinRodEquipped);
		baitPicker.SetTargetSlot(ItemSlot.SlotManager.Anon_FishingBait, () => LOC.UI.FishingNoBaitEquipped);
		PrepareLocationDropdown();
		UpdateEverything();
	}

	private void OnHelpClicked()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(LOC.UI.FishingMinigame_Help1);
			stringBuilder.AppendLine(LOC.UI.FishingMinigame_Help2);
			stringBuilder.AppendLine(LOC.UI.FishingMinigame_Help3);
			stringBuilder.AppendLine(LOC.UI.FishingMinigame_Help4);
			stringBuilder.AppendLine(LOC.UI.FishingMinigame_Help5);
			UiOverlay.Instance.OkPopup(LOC.Polyglot.MENU_MODE_HELP, stringBuilder.ToString());
		}
	}
}
