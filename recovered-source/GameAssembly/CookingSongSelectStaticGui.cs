using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CookingSongSelectStaticGui : StaticGuiBase
{
	[Header("Manual")]
	[SerializeField]
	private PooledPrefabReference<CookingSongDisplay> cookingSongDisplayPrefab;

	[SerializeField]
	private PooledPrefabReference<CookingRecipeExecutionEntryDisplay> cookingRecipeExecutionEntryPrefab;

	private CompoundItemList.CompoundItemListAdapter<CookingSong, CookingSongDisplay> _compoundAdapter;

	private AdvancedItemList.AdvancedItemListAdapter<CookingSong, CookingSongDisplay> _songsListAdapter;

	public static readonly StringRepresentationHelper<CookingSong> _stringRepresentationHelper = StringRepresentationHelper<CookingSong>.Builder().WithGetNameFunc((CookingSong x, StringRepresentationHelperEnum b) => x.GetMusic().SongName).WithoutAppendNameAction()
		.WithSearchFunc((CookingSong song, string s) => song.GetMusic().HasText(s))
		.Build();

	private const string LinkBestScore = "s";

	private const string LinkBestAccuracyScore = "a";

	private const string LinkBestGradeScore = "g";

	private const string LinkBestDishQualityScore = "q";

	private Action<CookingGameplayStartInfo> _onSongSelectionDone;

	private RecipeExecution _selectedSongRecipeExecution;

	private AdvancedItemList.AdvancedItemListAdapter<RecipeExecutionEntry, CookingRecipeExecutionEntryDisplay> _recipeExecutionAdapter;

	private CookingSong _selectedSong;

	private CookingGameplayModifiers _selectedModifiers;

	private CancellationTokenSource _playMusicCancellationTokenSource;

	private AudioSource _currentSource;

	[Header("Auto")]
	[SerializeField]
	private CompoundItemList A_CompoundItemList;

	[SerializeField]
	private TextMeshProUGUI A_Modifier_Description;

	[SerializeField]
	private TextMeshProUGUI A_Effects_Description;

	[SerializeField]
	private ButtonList A_Modifier_Buttons;

	[SerializeField]
	private TextMeshProUGUI A_Song_Name;

	[SerializeField]
	private TextMeshProUGUI A_Song_DescriptionTextMeshProUGUI;

	[SerializeField]
	private TextMeshProUGUILinkHandler A_Song_DescriptionTextMeshProUGUILinkHandler;

	[SerializeField]
	private TextMeshProUGUI A_Ingredient_Description;

	[SerializeField]
	private AdvancedItemList A_Ingredient_List;

	[SerializeField]
	private ButtonList A_Song_Buttons;

	[SerializeField]
	private BetterButton A_Return_Button;

	[SerializeField]
	private BetterButton A_Help_Button;

	private void ModifiersChanged()
	{
		UpdateAdditionalSettings();
		UpdatePitch();
		_songsListAdapter.UpdateAll();
	}

	private void ModifierToggle(CookingGameplayModifiers modifierToToggle)
	{
		if ((modifierToToggle & _selectedModifiers) == 0 && (modifierToToggle & CookingGameplayModifiers.AllSpeedMods) != 0)
		{
			_selectedModifiers &= ~CookingGameplayModifiers.AllSpeedMods;
		}
		_selectedModifiers ^= modifierToToggle;
		ModifiersChanged();
	}

	private void UpdateAdditionalSettings()
	{
		IModificationPeriodButtonList modificationPeriod;
		using (A_Modifier_Buttons.DoInitialization(out modificationPeriod))
		{
			ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Alternative);
			buttonListWrapper.SetText(LOC.UI.CookingSong_Modifiers);
			buttonListWrapper.OnClick = () =>
			{
				List<PopupChoice> list = new List<PopupChoice>();
				if (_selectedModifiers != CookingGameplayModifiers.NoMods)
				{
					list.Add(new PopupChoice(CookingGameplayModifiers.NoMods.ToLocalizedStringSingle(), () =>
					{
						_selectedModifiers = CookingGameplayModifiers.NoMods;
						ModifiersChanged();
					}));
				}
				CookingGameplayModifiers[] allModifiers2 = CookingGameplayUtilities.AllModifiers;
				foreach (CookingGameplayModifiers modifier in allModifiers2)
				{
					if (_selectedModifiers.HasFlagFast(modifier))
					{
						list.Add(new PopupChoice(LOC.UI.CookingSong_DeactiveModifier(modifier.ToLocalizedStringSingle()), () =>
						{
							ModifierToggle(modifier);
						}));
					}
					else
					{
						list.Add(new PopupChoice(modifier.ToLocalizedStringSingle(), () =>
						{
							ModifierToggle(modifier);
						}));
					}
				}
				UiOverlay.Instance.OpenContextMenuPopup(list);
			};
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				if (_selectedModifiers == CookingGameplayModifiers.NoMods)
				{
					stringBuilder.AppendLine(LOC.UI.CookingSong_NoActiveModifiers);
				}
				else
				{
					stringBuilder.AppendLine(LOC.UI.CookingSong_ActiveModifiers);
					CookingGameplayModifiers[] allModifiers = CookingGameplayUtilities.AllModifiers;
					foreach (CookingGameplayModifiers cookingGameplayModifiers in allModifiers)
					{
						if (_selectedModifiers.HasFlagFast(cookingGameplayModifiers))
						{
							stringBuilder.AppendLine(cookingGameplayModifiers.ToLocalizedStringSingle());
						}
					}
				}
				A_Modifier_Description.SetText(stringBuilder);
				stringBuilder.Clear();
				stringBuilder.AppendLine(LOC.UI.CookingSong_ModifierEffects);
				int length = stringBuilder.Length;
				double num2 = _selectedModifiers.DetermineScoreMultiplier() - 1.0;
				double num3 = (double)_selectedModifiers.DetermineTargetPitch() - 1.0;
				if (num2 > 0.0010000000474974513)
				{
					stringBuilder.AppendColorTagStart(MyColor32Presets.Green);
					stringBuilder.AppendLine(LOC.UI.CookingSong_MoreScore(num2));
					stringBuilder.AppendColorTagEnd();
				}
				else if (num2 < -0.0010000000474974513)
				{
					stringBuilder.AppendColorTagStart(MyColor32Presets.Red);
					stringBuilder.AppendLine(LOC.UI.CookingSong_LessScore(0.0 - num2));
					stringBuilder.AppendColorTagEnd();
				}
				if (num3 > 0.0010000000474974513)
				{
					stringBuilder.AppendLine(LOC.UI.CookingSong_FasterSong(num3));
				}
				else if (num3 < -0.0010000000474974513)
				{
					stringBuilder.AppendLine(LOC.UI.CookingSong_SlowerSong(0.0 - num3));
				}
				if (_selectedModifiers.HasFlagFast(CookingGameplayModifiers.Practice))
				{
					stringBuilder.AppendLine(LOC.UI.CookingSong_ModifierPracticeInfo);
				}
				if (length == stringBuilder.Length)
				{
					stringBuilder.AppendLine(LOC.UI.CookingSong_ModifierEffectsNone);
				}
				A_Effects_Description.SetText(stringBuilder);
			}
		}
	}

	private void PlayAutoButtonClicked()
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.CookingSong_PlayAutoPrompt, () =>
		{
			CookingGameplayStartInfo currentStartInfo = GetCurrentStartInfo();
			currentStartInfo.ActiveModifiers = CookingGameplayModifiers.AutoPlay;
			_onSongSelectionDone?.Invoke(currentStartInfo);
		});
	}

	private void CookInstantlyCheatButtonClicked()
	{
		List<PopupChoice> list = new List<PopupChoice>();
		list.Add(new PopupChoice(LOC.UI.Cheat_PlayCookiningCheatSaveScore, () =>
		{
			CookingGameplayStartInfo currentStartInfo = GetCurrentStartInfo();
			currentStartInfo.ActiveModifiers = CookingGameplayModifiers.AutoPlay;
			currentStartInfo.CheatInfo = CookingGameplayCheatStartInfo.CheatSave;
			_onSongSelectionDone?.Invoke(currentStartInfo);
		}));
		list.Add(new PopupChoice(LOC.UI.Cheat_PlayCookiningCheatDiscardScore, () =>
		{
			CookingGameplayStartInfo currentStartInfo = GetCurrentStartInfo();
			currentStartInfo.ActiveModifiers = CookingGameplayModifiers.AutoPlay;
			currentStartInfo.CheatInfo = CookingGameplayCheatStartInfo.CheatNoSave;
			_onSongSelectionDone?.Invoke(currentStartInfo);
		}));
		list.Add(new PopupChoice(LOC.Polyglot.MENU_LABEL_NO));
		UiOverlay.Instance.Popup(LOC.UI.UI_AreYouSure, LOC.UI.Cheat_CookInstantlyPrompt, list);
	}

	public void PlayButtonClicked()
	{
		if (_selectedModifiers.HasFlagFast(CookingGameplayModifiers.Practice))
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.CookingSong_PracticePlayPrompt, () =>
			{
				_onSongSelectionDone?.Invoke(GetCurrentStartInfo());
			});
		}
		else
		{
			_onSongSelectionDone?.Invoke(GetCurrentStartInfo());
		}
	}

	public void StartGui(Action<CookingGameplayStartInfo> OnSongSelectionDone)
	{
		GameVariables.Current.cookingMinigameManager.MarkDirty();
		_onSongSelectionDone = OnSongSelectionDone;
		UpdateAdditionalSettings();
		StartPlayMusicTask();
		SortType<CookingSong>[] sortTypes = new SortType<CookingSong>[2]
		{
			new SortType<CookingSong>(() => LOC.UI.Sort_ByDifficulty, Comparer<CookingSong>.Create((CookingSong a, CookingSong b) => a.RuntimeData.GetDefaultDifficulty().SongDifficulty.CompareTo(b.RuntimeData.GetDefaultDifficulty().SongDifficulty))),
			new SortType<CookingSong>(() => LOC.UI.Sort_AlphabeticallyBySongName, Comparer<CookingSong>.Create((CookingSong a, CookingSong b) => string.Compare(a.GetMusic().SongName, b.GetMusic().SongName, StringComparison.OrdinalIgnoreCase)))
		};
		List<Folder<CookingSong>> list = MyCollectionPool<Folder<CookingSong>>.GetList();
		list.Add(Folder<CookingSong>.AllFolder);
		CompoundItemList.StartBuilder<CookingSong, CookingSongDisplay>().WithStringRepresentationHelper(_stringRepresentationHelper).WithSortTypes(sortTypes)
			.WithFolders(list)
			.WithSetItem((CookingSong song, CookingSongDisplay display, CompoundItemList.CompoundItemListAdapter<CookingSong, CookingSongDisplay> adapter) =>
			{
				display.SetData((_selectedModifiers, song));
				display.SetFocusOnClick();
			})
			.WithListPool(cookingSongDisplayPrefab.GetPool())
			.BuildAndPrepare(A_CompoundItemList, DataSourceUtils.CreateMultipleUse(CookingSongPrefabManager.GetAllVisibleSongs), out _compoundAdapter, out _songsListAdapter);
		_songsListAdapter.OnFocusedItemChanged = OnSelectedSongChanged;
		_compoundAdapter.UpdateAll();
		_songsListAdapter.Focus(UnityEngine.Random.Range(0, _songsListAdapter.ProcessedItemCount));
		A_Return_Button.onClick.SetListener(SingletonLoadableMonoBehaviour<GameScript>.Instance.GoHome);
		A_Help_Button.onClick.SetListener(HelpButtonClicked);
		A_Song_DescriptionTextMeshProUGUILinkHandler.OnLinkClicked = OnLinkClicked;
	}

	private void HelpButtonClicked()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(LOC.UI.CookingMInigame_Help1);
			stringBuilder.AppendLine(LOC.UI.CookingMInigame_Help2("Z", "X"));
			UiOverlay.Instance.OkPopup(LOC.UI.HairSettings_Help, stringBuilder.ToString());
		}
	}

	private void OnLinkClicked(string linkId)
	{
		if (_selectedSong == null)
		{
			return;
		}
		CookingSongSave save = _selectedSong.GetSave();
		CookingGameplayScoreData _bestScore = null;
		switch (linkId)
		{
		case "s":
			save.TryGetBestScore(out _bestScore);
			break;
		case "a":
			save.TryGetBestAccuracyScore(out _bestScore);
			break;
		case "g":
		{
			save.TryGetBestGradeScore(out _bestScore, out var _);
			break;
		}
		case "q":
		{
			save.TryGetBestDishScore(out _bestScore, out var _);
			break;
		}
		}
		if (_bestScore == null)
		{
			return;
		}
		CookingGameplayScoreData perfectScoreReadonly = _selectedSong.RuntimeData.GetPerfectScoreReadonly();
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			_bestScore.AppendString(stringBuilder, perfectScoreReadonly);
			UiOverlay.Instance.OkPopup(LOC.UI.CookingMinigame_Score, stringBuilder.ToString());
		}
	}

	private void OnSelectedSongChanged(CookingSong song, int i)
	{
		_selectedSong = song;
		if (song != null)
		{
			CookingSongRuntimeData runtimeData = song.RuntimeData;
			A_Song_Name.SetText(song.GetMusic().Info);
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.Clear();
				stringBuilder.Append(LOC.UI.CookingSong_BPM);
				stringBuilder.Append(" ");
				stringBuilder.AppendFormat("{0:0}", runtimeData.AverageBPM);
				if (Math.Abs(runtimeData.MaxBPM - runtimeData.MinBPM) > 1.0)
				{
					stringBuilder.Append(" (");
					stringBuilder.Append(LOC.UI.CookingSong_MinBPM);
					stringBuilder.Append(" ");
					stringBuilder.AppendFormat("{0:0}", runtimeData.MinBPM);
					stringBuilder.Append(", ");
					stringBuilder.Append(LOC.UI.CookingSong_MaxBPM);
					stringBuilder.Append(" ");
					stringBuilder.AppendFormat("{0:0}", runtimeData.MaxBPM);
					stringBuilder.Append(")");
				}
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat("{0} {1:0.00}*\n", LOC.UI.CookingSong_Difficulty, runtimeData.GetDifficulty(_selectedModifiers).SongDifficulty);
				runtimeData.GetSongLengthDiscreet(_selectedModifiers, out var minutes, out var seconds);
				stringBuilder.AppendFormat("{0} {1:0}:{2:00}\n", LOC.UI.CookingSong_Length, minutes, seconds);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				if (song.IsUnlocked)
				{
					CookingSongSave save = GameVariables.Current.cookingMinigameManager.GetSave(song);
					stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_TimesPlayed, save.TimesPlayed);
					stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_TimesFinished, save.TimesFinished);
					if (save.TryGetBestScore(out var _bestScore))
					{
						stringBuilder.TMPAppendLinkTag("s");
						stringBuilder.AppendIntDisplay(LOC.UI.CookingMinigame_BestScore, _bestScore.GetScoreWithMultipliers());
						if (_bestScore.ActiveModifiers.AnyModifiers())
						{
							stringBuilder.AppendSurroundParentheses(_bestScore.ActiveModifiers.ToLocalizedString());
						}
						stringBuilder.TMPAppendCloseLinkTag();
						stringBuilder.AppendLine();
					}
					if (save.TryGetBestAccuracyScore(out var _bestAccuracyScore))
					{
						stringBuilder.TMPAppendLinkTag("a");
						CookingGameplayScoreData perfectScoreReadonly = runtimeData.GetPerfectScoreReadonly();
						stringBuilder.AppendPercentDisplay(LOC.UI.CookingMinigame_BestAccuracy, _bestAccuracyScore.GetAccuracyScorePercent(perfectScoreReadonly));
						if (_bestAccuracyScore.ActiveModifiers.AnyModifiers())
						{
							stringBuilder.AppendSurroundParentheses(_bestAccuracyScore.ActiveModifiers.ToLocalizedString());
						}
						stringBuilder.TMPAppendCloseLinkTag();
						stringBuilder.AppendLine();
					}
					if (save.TryGetBestGradeScore(out var _, out var _bestGrade))
					{
						stringBuilder.TMPAppendLinkTag("g");
						stringBuilder.Append(LOC.UI.CookingSong_BestGrade);
						stringBuilder.Append(": ");
						stringBuilder.AppendColorTagStart(_bestGrade.GradeToColor());
						stringBuilder.Append(_bestGrade.ToFriendlyString());
						stringBuilder.AppendColorTagEnd();
						stringBuilder.TMPAppendCloseLinkTag();
						stringBuilder.AppendLine();
					}
					if (save.TryGetBestDishScore(out var _bestDishScore, out var bestDishQuality))
					{
						stringBuilder.TMPAppendLinkTag("q");
						stringBuilder.AppendPercentDisplay(LOC.UI.CookingSong_BestDishQuality, bestDishQuality);
						if (_bestDishScore.ActiveModifiers.AnyModifiers())
						{
							stringBuilder.AppendSurroundParentheses(_bestDishScore.ActiveModifiers.ToLocalizedString());
						}
						stringBuilder.AppendColorTagEnd();
						stringBuilder.TMPAppendCloseLinkTag();
						stringBuilder.AppendLine();
					}
				}
				A_Song_DescriptionTextMeshProUGUI.SetText(stringBuilder.ToString());
			}
		}
		else
		{
			_selectedSongRecipeExecution = null;
			A_Song_DescriptionTextMeshProUGUI.SetText("");
		}
		UpdateIngredients();
	}

	private void IngredientClicked(int index)
	{
		if (!_selectedSongRecipeExecution.RecipeExecutionEntries.TryGetValue(index, out var value))
		{
			return;
		}
		RecipeIngredient recipeIngredient = _selectedSongRecipeExecution.OriginalRecipe.AllIngredients[index];
		Selection<Item> startSelection = value.ChosenItems.Clone();
		_selectedSongRecipeExecution.ClearAssignedItems(value);
		Item.ItemManager itemManager = GameVariables.Current.itemManager;
		if (recipeIngredient.FindInInventory(itemManager).Any((Item x) => x.GetCount() - _selectedSongRecipeExecution.AllChosenItems.GetItemSelectedCount(x) > 0))
		{
			CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithAllFolder()
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.Build(recipeIngredient.FindInInventory(GameVariables.Current.itemManager).ToFrozenDataSource())
				.ToChooseItemsComplexPopupBuilderItem()
				.WithStartingSelection(startSelection, _selectedSongRecipeExecution.AllChosenItems.Clone())
				.WithOnFinished((UiOverlay.PopupFinishTypesEnum a, Selection<Item> b) =>
				{
					if (a == UiOverlay.PopupFinishTypesEnum.Successful)
					{
						_selectedSongRecipeExecution.SetItems(index, b);
						UpdateIngredients();
					}
				})
				.OpenPopup(LOC.UI.IngredientSelectionTitle, LOC.UI.IngredientSelectionPrompt(recipeIngredient.GetName()), allowClosing: true, showCancel: true);
		}
		else
		{
			UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.RecipeChoosingItemsNoValidItemsFoundPrompt(recipeIngredient.GetName()));
		}
	}

	private void UpdateIngredients()
	{
		if (_selectedSong == null || !_selectedSong.IsUnlocked)
		{
			A_Song_Buttons.ResetButtons();
			_selectedSongRecipeExecution = null;
			A_Ingredient_Description.SetText("");
			A_Ingredient_List.RemoveAdapter();
			return;
		}
		IModificationPeriodButtonList modificationPeriod;
		using (A_Song_Buttons.DoInitialization(out modificationPeriod))
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				Recipe recipe = _selectedSong.Recipe;
				if (_selectedSongRecipeExecution == null || _selectedSongRecipeExecution.OriginalRecipe != _selectedSong.Recipe)
				{
					_selectedSongRecipeExecution = new RecipeExecution(recipe);
					_selectedSongRecipeExecution.FillRecipeExecution(GameVariables.Current.itemManager);
					_recipeExecutionAdapter = A_Ingredient_List.SetAdapter(DataSourceUtils.CreateMultipleUse(() => _selectedSongRecipeExecution.RecipeExecutionEntries), cookingRecipeExecutionEntryPrefab.GetPool(), (RecipeExecutionEntry data, CookingRecipeExecutionEntryDisplay display) =>
					{
						display.SetData(data);
						display.SetDefaultButtonActionWithIndex(IngredientClicked);
					});
				}
				stringBuilder.AppendLine(LOC.UI.CookingSong_IngredientList);
				A_Ingredient_Description.SetText(stringBuilder);
				if (_selectedSongRecipeExecution.CanBeCooked())
				{
					ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Alternative);
					buttonListWrapper.SetText(LOC.UI.CookingSong_Play);
					buttonListWrapper.OnClick = PlayButtonClicked;
					if (GameVariables.Current.cookingMinigameManager.GetSave(_selectedSong).IsPassed())
					{
						ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(10, CommonButtonColorType.Alternative);
						buttonListWrapper2.SetText(LOC.UI.CookingSong_PlayAuto);
						buttonListWrapper2.OnClick = PlayAutoButtonClicked;
					}
					if (OptionsStatic.GetCheatTier() >= CheatTierEnum.AdvancedCheats)
					{
						ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton(20, CommonButtonColorType.Alternative);
						buttonListWrapper3.SetText(LOC.UI.Cheat_PlayCookiningCheat);
						buttonListWrapper3.OnClick = CookInstantlyCheatButtonClicked;
					}
				}
			}
		}
		_recipeExecutionAdapter.UpdateAll();
	}

	private CookingGameplayStartInfo GetCurrentStartInfo()
	{
		return new CookingGameplayStartInfo(_selectedSong, _selectedModifiers, _selectedSongRecipeExecution);
	}

	private void UpdatePitch()
	{
		if (_currentSource != null)
		{
			_currentSource.pitch = _selectedModifiers.DetermineTargetPitch();
		}
	}

	private void KillSongChangedTask()
	{
		if (_playMusicCancellationTokenSource != null)
		{
			_playMusicCancellationTokenSource.Cancel();
			_playMusicCancellationTokenSource.Dispose();
			_playMusicCancellationTokenSource = null;
		}
	}

	private void StartPlayMusicTask()
	{
		KillSongChangedTask();
		_playMusicCancellationTokenSource = new CancellationTokenSource();
		PlayMusicTask(_playMusicCancellationTokenSource.Token).Forget();
	}

	private async UniTask PlayMusicTask(CancellationToken cancellationToken)
	{
		AudioClipDisposableWrapper handleCurrent = null;
		AudioClipDisposableWrapper handleLast = null;
		_currentSource = SingletonMonoBehaviour<SoundSingleton>.Instance.GetFreeAudioSource();
		try
		{
			CookingSong currentSong = null;
			while (true)
			{
				await UniTask.WaitUntil(() => _selectedSong != currentSong, PlayerLoopTiming.Update, cancellationToken);
				if (_selectedSong != null)
				{
					currentSong = _selectedSong;
					handleLast = handleCurrent;
					UniTask<AudioClipDisposableWrapper> loadingTask = _selectedSong.GetMusic().GetAudioClip();
					if (_currentSource.clip != null && _currentSource.isPlaying)
					{
						await _currentSource.TweenVolumeAsync(0f, 0.2f, cancellationToken);
					}
					handleCurrent = await loadingTask;
					_currentSource.volume = 0f;
					_currentSource.clip = handleCurrent.AudioClip;
					_currentSource.loop = true;
					float num = (float)currentSong.RuntimeData.GetDefaultDifficulty().MaxNoteDifficultyTime - 3f;
					float value = ((num > 0f) ? num : 0f);
					_currentSource.time = Mathf.Clamp(value, 0f, _currentSource.clip.length);
					UpdatePitch();
					_currentSource.Play();
					if (handleLast != null)
					{
						handleLast.Dispose();
						handleLast = null;
					}
					await _currentSource.TweenVolumeAsync(SoundSingleton.MusicVolume, 0.2f, cancellationToken);
				}
			}
		}
		finally
		{
			if (_currentSource != null)
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.ReturnToPool(_currentSource);
				_currentSource = null;
			}
			handleLast?.Dispose();
			handleCurrent?.Dispose();
		}
	}

	public void EndGui()
	{
		KillSongChangedTask();
		A_CompoundItemList.ClearEverything();
		_onSongSelectionDone = null;
		_selectedSong = null;
		_songsListAdapter = null;
		A_Song_Buttons.ResetButtons();
		A_Return_Button.onClick.RemoveAllListeners();
		A_Help_Button.onClick.RemoveAllListeners();
	}
}
