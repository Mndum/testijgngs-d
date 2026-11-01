using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ModsPopup : PrefabPopup<ModsPopup>
{
	[SerializeField]
	private CompoundItemList _modItemList;

	private CompoundItemList.CompoundItemListAdapter<Mod, ModDisplay> _compoundGameItemListAdapter;

	private AdvancedItemList.AdvancedItemListAdapter<Mod, ModDisplay> _gameItemListAdapter;

	[SerializeField]
	private ButtonList _selectedModButtonList;

	[FormerlySerializedAs("_specialButtonClicked")]
	[SerializeField]
	private Button _specialButton;

	[SerializeField]
	private Button _translationButton;

	[SerializeField]
	private Button _createNewMod;

	[SerializeField]
	private Button _openModFolder;

	[SerializeField]
	private Button _loadMods;

	[SerializeField]
	private Transform _itemHolder;

	[SerializeField]
	private TextMeshProUGUI _basicModInfo;

	private const string ModStatusLinkId = "M";

	private const string ModTranslationIssueLinkId = "T";

	private void Awake()
	{
		_specialButton.onClick.SetListener(SpecialButtonClicked);
		_translationButton.onClick.SetListener(TranslationButtonClicked);
	}

	private void SwitchDisplayedItem(Mod mod, int i)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			IModificationPeriodButtonList modificationPeriod;
			using (_selectedModButtonList.DoInitialization(out modificationPeriod))
			{
				if (mod != null)
				{
					stringBuilder.TMPAppendHeaderSizeTag();
					stringBuilder.Append(mod.Name);
					stringBuilder.TMPAppendCloseSizeTag();
					stringBuilder.AppendLine();
					stringBuilder.Append(LOC.UI.Mods_ModVersion);
					stringBuilder.Append(" ");
					stringBuilder.Append(mod.ModVersion);
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					stringBuilder.AppendLine($"{LOC.UI.Mods_DataFiles} {mod.FilesIncluded.Count}");
					stringBuilder.AppendLine(LOC.UI.Mods_ApproximateExtractedSize + " " + GlobalFunctions.FormatBytes(mod.GetApproximateFileSizeInBytes()));
					stringBuilder.AppendLineStringDisplay(LOC.UI.Mods_ModsOriginalLangauge, mod.MainModInfoFile.Locale.Metadata.GetMetadata<LocaleName>().name);
					stringBuilder.AppendLine();
					stringBuilder.TMPAppendLinkTag("M");
					stringBuilder.Append(LOC.UI.Mods_Status + " " + ModStatusExtensions.StatusToName(mod.Status).TMPColorSurround(ModStatusExtensions.StatusToColor(mod.Status)));
					stringBuilder.TMPAppendCloseLinkTag();
					stringBuilder.AppendLine();
					ParsedVersion.VersionComparisonStruct versionComparisonStruct = mod.TargetGameVersion.CompareWith(ParsedVersion.CurrentGameVersion);
					if (!versionComparisonStruct.IsExactlyTheSame)
					{
						stringBuilder.AppendColorTagStart(Color.magenta);
						stringBuilder.AppendLine(versionComparisonStruct.GetLocalizedDifferenceDescription());
						stringBuilder.AppendLine(LOC.UI.Mods_VersionMismatch(mod.TargetGameVersion, Application.version));
						if (versionComparisonStruct.IsNewer)
						{
							stringBuilder.AppendLine(LOC.UI.Mods_UpdateYourGame);
						}
						stringBuilder.AppendColorTagEnd();
					}
					if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.GetLocalModInfo(mod, createIfNotExists: true).LoadOnStart)
					{
						stringBuilder.AppendLine(LOC.UI.Mods_Activating);
						if (mod.Status == ModStatus.Ready)
						{
							stringBuilder.AppendLine(LOC.UI.Mods_RestartToActivate);
						}
						ButtonListWrapper buttonListWrapper = modificationPeriod.AddButton(0, CommonButtonColorType.Red);
						buttonListWrapper.SetText(LOC.UI.Mods_Deactivate);
						buttonListWrapper.OnClick = ActivateButtonClicked;
					}
					else
					{
						stringBuilder.AppendLine(LOC.UI.Mods_NotActivating);
						if (mod.Status == ModStatus.Active)
						{
							stringBuilder.AppendLine(LOC.UI.Mods_RestartToDeactivate);
						}
						ButtonListWrapper buttonListWrapper2 = modificationPeriod.AddButton(0, CommonButtonColorType.Green);
						buttonListWrapper2.SetText(LOC.UI.Mods_Activate);
						buttonListWrapper2.OnClick = ActivateButtonClicked;
					}
					stringBuilder.AppendLine();
					stringBuilder.TMPAppendSubHeaderSizeTag();
					stringBuilder.Append(LOC.UI.Mods_ModDescription);
					stringBuilder.TMPAppendCloseSizeTag();
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(mod.MainModInfoFile.description);
					if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.RestartRequired)
					{
						stringBuilder.AppendLine(LOC.UI.Mods_RestartRequired.TMPColorSurround(Color.red));
					}
					ButtonListWrapper buttonListWrapper3 = modificationPeriod.AddButton(10, CommonButtonColorType.Red);
					buttonListWrapper3.SetText(LOC.UI.UI_Delete);
					buttonListWrapper3.OnClick = DeleteButtonClicked;
					stringBuilder.AppendLine();
					if (mod.TryGetLoadedTranslations(out var translations))
					{
						stringBuilder.TMPAppendSubHeaderSizeTag();
						stringBuilder.AppendLine(LOC.UI.Mods_ImportedTranslations);
						stringBuilder.TMPAppendCloseSizeTag();
						int num = 0;
						foreach (KeyValuePair<Locale, ModsSingleton.ModTranslationDataLoaded> item in translations)
						{
							Locale translationLocale = item.Value.TranslationLocale;
							ModTranslationData data = item.Value.Data;
							LocaleName metadata = translationLocale.Metadata.GetMetadata<LocaleName>();
							stringBuilder.Append("-");
							stringBuilder.Append(metadata.name);
							stringBuilder.Append("  ");
							if (item.Value.TryGetTranslationStatus(out var translationStatus))
							{
								stringBuilder.Append("(");
								float percentGood = translationStatus.GetPercentGood();
								if (percentGood >= 1f)
								{
									stringBuilder.Append(LOC.UI.FullyTranslated);
								}
								else
								{
									stringBuilder.Append(LOC.UI.Options_TranslationProgress(percentGood));
								}
								stringBuilder.Append(")");
							}
							stringBuilder.AppendLine();
							stringBuilder.Append("\t");
							stringBuilder.AppendStartDisplay(LOC.UI.Mods_TargetModVersion);
							ParsedVersion.VersionComparisonStruct versionComparisonStruct2 = data.ParsedTargetModVersion.CompareWith(mod.ModVersion);
							if (!versionComparisonStruct2.IsExactlyTheSame)
							{
								using (stringBuilder.StartColorTagUsing(Color.magenta))
								{
									stringBuilder.Append(data.ParsedTargetModVersion);
									stringBuilder.Append(", ");
									stringBuilder.Append(versionComparisonStruct2.GetLocalizedDifferenceDescription());
								}
							}
							else
							{
								using (stringBuilder.StartColorTagUsing(Color.green))
								{
									stringBuilder.Append(data.ParsedTargetModVersion);
								}
							}
							stringBuilder.AppendLine();
							if (translationStatus != null && translationStatus.HasAnyIssues())
							{
								stringBuilder.Append("\t");
								stringBuilder.TMPAppendLinkTag("T" + num);
								if (translationStatus.HasImportantIssues())
								{
									stringBuilder.AppendColorTagStart(Color.red);
									stringBuilder.Append(LOC.UI.Mods_TranslationIssuesBroken);
								}
								else
								{
									stringBuilder.AppendColorTagStart(Color.yellow);
									stringBuilder.Append(LOC.UI.Mods_TranslationIssues);
								}
								stringBuilder.Append("(");
								stringBuilder.Append(LOC.UI.HoverToView);
								stringBuilder.Append(")");
								stringBuilder.AppendColorTagEnd();
								stringBuilder.TMPAppendCloseLinkTag();
								stringBuilder.AppendLine();
							}
							if (!string.IsNullOrWhiteSpace(data.ShortNotes))
							{
								stringBuilder.Append("\t");
								stringBuilder.AppendStartDisplay(LOC.UI.Mods_ModTranslationNotes);
								stringBuilder.Append(data.ShortNotes.TruncateLongString(50));
								stringBuilder.AppendLine();
							}
							stringBuilder.AppendLine();
							num++;
						}
					}
					else
					{
						stringBuilder.TMPAppendSubHeaderSizeTag();
						stringBuilder.Append(LOC.UI.Mods_NoTranslationData);
						stringBuilder.TMPAppendCloseSizeTag();
						stringBuilder.AppendLine();
					}
				}
				else
				{
					stringBuilder.AppendLine(LOC.UI.Mods_NoModSelected);
				}
				_basicModInfo.SetText(stringBuilder);
				if (_basicModInfo.transform.parent is RectTransform rt)
				{
					rt.ForceRebuildLayoutRecursive();
				}
			}
		}
	}

	public new void OnDisable()
	{
		base.OnDisable();
		if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.RestartRequired)
		{
			UiOverlay.Instance.OkPopup(LOC.UI.Mods_ClosingTheGame, LOC.UI.Mods_ClosingTheGamePrompt, QuitHandler.ForceQuitGame);
		}
		else if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalizationReloadRequired)
		{
			OptionsStatic.SetLocale(LocalizationSettings.SelectedLocale);
		}
	}

	public override bool CanClose()
	{
		return true;
	}

	public override void Open()
	{
		base.Open();
		_basicModInfo.GetComponent<TooltipDisplayAreaTextLink>().TextFunc = ModInfoLinkHovered;
		_createNewMod.onClick.RemoveAllListeners();
		_createNewMod.onClick.AddListener(CreateNewModButtonClicked);
		_openModFolder.onClick.RemoveAllListeners();
		_openModFolder.onClick.AddListener(() =>
		{
			UiOverlay.Instance.OpenNativeFolderPath(SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.ModPath);
		});
		PointerDownEvent.OnPointerDownEvent onPointerDownUnityEvent = _loadMods.gameObject.GetOrAddComponent<PointerDownEvent>().OnPointerDownUnityEvent;
		onPointerDownUnityEvent.RemoveAllListeners();
		onPointerDownUnityEvent.AddListener((PointerEventData t) =>
		{
			LoadModsButtonClicked();
		});
		SortType<Mod>[] sortTypes = new SortType<Mod>[2]
		{
			new SortType<Mod>(() => LOC.UI.Mods_LoadOrder, Comparer<Mod>.Create((Mod x, Mod y) => x.GetLocalModInfo().ModOrder.CompareTo(y.GetLocalModInfo().ModOrder)), forceAllowItemMove: true),
			new SortType<Mod>(() => LOC.UI.Equipment_SortAlphabetically, Comparer<Mod>.Create((Mod x, Mod y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal)))
		};
		using (CompoundItemList.StartBuilder<Mod, ModDisplay>().WithToStringOnly((Mod mod, StringRepresentationHelperEnum type) => mod.Name).WithSortTypes(sortTypes)
			.WithAllFolder()
			.WithSetItem((Mod mod, ModDisplay s, CompoundItemList.CompoundItemListAdapter<Mod, ModDisplay> adapter) =>
			{
				s.SetData(mod);
			})
			.WithListPool(UiOverlay.Instance.modItemListDisplay.GetPool())
			.BuildAndPrepare(_modItemList, DataSourceUtils.CreateMultipleUse(() => SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.AllMods), out _compoundGameItemListAdapter, out _gameItemListAdapter))
		{
			_compoundGameItemListAdapter.ItemsMovedAction = (ModDisplay targetDisplay, Mod source, int index) =>
			{
				Mod current = targetDisplay.Current;
				if (current != source)
				{
					source.GetLocalModInfo();
					SerializableLocalModInfo localModInfo = current.GetLocalModInfo();
					SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.ChangeModOrder(source, localModInfo.ModOrder);
					_compoundGameItemListAdapter.UpdateAll();
					_gameItemListAdapter.Focus(_gameItemListAdapter.DetermineIndexOfItem(source));
				}
			};
			_gameItemListAdapter.OnFocusedItemChanged = SwitchDisplayedItem;
			_compoundGameItemListAdapter.ItemRightClickedAction = (ModDisplay modDisplay) =>
			{
				_gameItemListAdapter.Focus(modDisplay.Index);
				_selectedModButtonList.ShowAsContextMenu();
			};
		}
	}

	private string ModInfoLinkHovered(string linkId)
	{
		if (linkId.StartsWith("T"))
		{
			if (int.TryParse(linkId.Substring("T".Length), out var result) && _gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item) && item.TryGetLoadedTranslations(out var translations))
			{
				StringBuilder stringBuilder;
				if (translations.ElementAtOrDefault(result).Value.TryGetTranslationStatus(out var translationStatus) && translationStatus.HasAnyIssues())
				{
					using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
					{
						using (stringBuilder.StartColorTagUsing(Color.red))
						{
							if (translationStatus.MissingKeys > 0)
							{
								stringBuilder.AppendLineIntDisplay(LOC.UI.Mods_TranslationIssuesMissingKeys, translationStatus.MissingKeys);
							}
							if (translationStatus.BrokenKeys > 0)
							{
								stringBuilder.AppendLineIntDisplay(LOC.UI.Mods_TranslationIssuesBrokenKeys, translationStatus.BrokenKeys);
							}
							if (translationStatus.ExtraKeys > 0)
							{
								stringBuilder.AppendLineIntDisplay(LOC.UI.Mods_TranslationIssuesExtraKeys, translationStatus.ExtraKeys);
							}
						}
						stringBuilder.AppendLine(LOC.UI.Mods_TranslationIssuesLogInfo);
						return stringBuilder.ToString();
					}
				}
				return LOC.UI.Mods_NoTranslationIssuesFound;
			}
			return null;
		}
		if (linkId == "M")
		{
			return ModStatusExtensions.GenerateStatusExplaination();
		}
		return null;
	}

	private void TranslationButtonClicked()
	{
		List<PopupChoice> list = new List<PopupChoice>();
		list.Add(new PopupChoice(LOC.UI.Mods_ImportModTranslationFiles, ImportLocalizationButtonClicked, CommonButtonColorType.Green));
		if (_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			list.Add(new PopupChoice(LOC.UI.Mods_GenerateModTranslationFile, GenerateLocalizationButtonClicked));
			if (item.LoadedTranslations != null)
			{
				list.Add(new PopupChoice(LOC.UI.Mods_DeleteInstalledTranslationModFiles, DeleteLocalizationButtonClicked, CommonButtonColorType.Red));
				list.Add(new PopupChoice(LOC.UI.Mods_ExportInstalledTranslationModFiles, ExportLocalizationButtonClicked));
			}
		}
		UiOverlay.Instance.OpenContextMenuPopup(list);
	}

	private void SpecialButtonClicked()
	{
		if (_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			List<PopupChoice> list = new List<PopupChoice>();
			if (!ItemPrefab.ItemPrefabManager.HasAnyItems(item.ModGuid))
			{
				list.Add(new PopupChoice(LOC.UI.Mods_Edit, EditButtonClicked));
			}
			list.Add(new PopupChoice(LOC.UI.Mods_Export, ExportButtonClicked));
			list.Add(new PopupChoice(LOC.UI.Mods_ExportLog, ExportLogButtonClicked));
			list.Add(new PopupChoice(LOC.UI.Mods_ViewLogs, ViewLogButtonClicked));
			UiOverlay.Instance.OpenContextMenuPopup(list);
		}
	}

	private void EditButtonClicked()
	{
		if (!_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var mod))
		{
			return;
		}
		UiOverlay.Instance.OkPopup("Advanced mod creator", "This mod's copy will open in the advanced mod creator. This will ONLY work with mods that add new equippable items. Everything other than items will be ignored. ItemPrefab methods (OnUse, SpecialEffectAction) can be loaded from, but not exported to lua, so they aren't supported by this editor.\nHave fun :)", () =>
		{
			UiOverlay.Instance.OpenModCreatorPopup((from t in ItemPrefab.ItemPrefabManager.AllPrefabsFromSource(mod.ModGuid)
				select t.prefab.Clone()).ToList());
		});
	}

	public void DeleteButtonClicked()
	{
		if (_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var mod))
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Mods_DeleteModPrompt(mod.Name), () =>
			{
				SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.RemoveAndDeleteMod(mod);
				UpdateAll();
			});
		}
	}

	private void ExportLogButtonClicked()
	{
		if (_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.SaveToFile((MyFileBrowserSingleton.DownloadResult t) =>
			{
				_ = t.Success;
			}, Encoding.UTF8.GetBytes(item.Log), item.Name + "_LoadingLog.txt");
		}
	}

	private void ViewLogButtonClicked()
	{
		if (_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			UiOverlay.Instance.OkPopup(LOC.UI.Mods_ViewLogs, item.Log);
		}
	}

	public void ExportButtonClicked()
	{
		if (_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			byte[] data = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.SaveModToZipData(item);
			UiOverlay.Instance.FileSavePopup(item.InternalFilename, data, null);
		}
	}

	private void ActivateButtonClicked()
	{
		if (_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item))
		{
			SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.ToggleLoadOnStart(item);
			UpdateAll();
		}
	}

	private void GenerateLocalizationButtonClicked()
	{
		if (!_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var mod))
		{
			return;
		}
		CompoundItemList.StartBuilder<Locale, StringAdvancedItemListItemPrefab>().WithToStringOnly((Locale a, StringRepresentationHelperEnum b) => a.LocaleName).WithBasicToStringEverything()
			.Build(LocalizationSettings.Instance.GetAvailableLocales().Locales.ToFrozenDataSource())
			.ToChooseItemsComplexPopupBuilderGenericString()
			.WithSingularStartingSelectionSingleItemAllowed()
			.WithSuccessfulOnlyOnFinished((Selection<Locale> selection) =>
			{
				if (selection.TryGetFirstItem(out var item))
				{
					if (!SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.TryGenerateTranslationData(mod, item, out var translationData))
					{
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.FailedGeneratingTranslationFile);
					}
					else
					{
						UiOverlay.Instance.FileSavePopup(translationData.GetDefaultFileName(), translationData.ToJsonPrettyPrint(), (MyFileBrowserSingleton.DownloadResult result) =>
						{
							if (result.Success)
							{
								UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.SuccessfulyGenerateTranslationFile(mod.Name, result.Path));
							}
						});
					}
				}
			})
			.OpenPopup(LOC.UI.ChooseLanguage, LOC.UI.ChooseModTranslationGenerationLanguage, allowClosing: true, showCancel: true);
	}

	private void DeleteLocalizationButtonClicked()
	{
		if (!_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var mod) || mod.LoadedTranslations == null)
		{
			return;
		}
		CompoundItemList.StartBuilder<ModsSingleton.ModTranslationDataLoaded, StringAdvancedItemListItemPrefab>().WithToStringOnly((ModsSingleton.ModTranslationDataLoaded a, StringRepresentationHelperEnum b) => ((uint)b <= 1u || b != StringRepresentationHelperEnum.Description) ? a.TranslationLocale.LocaleName : a.Describe()).WithBasicToStringEverything()
			.Build(mod.LoadedTranslations.Values.ToFrozenDataSource())
			.ToChooseItemsComplexPopupBuilderGenericString()
			.WithSingularStartingSelection()
			.WithSuccessfulOnlyOnFinished((Selection<ModsSingleton.ModTranslationDataLoaded> selection) =>
			{
				StringBuilder stringBuilder;
				if (selection.SelectedItemsCount > 0)
				{
					using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
					{
						stringBuilder.AppendJoin(", ", from x in selection.GetAllItems()
							select x.Describe());
						stringBuilder.AppendLine();
						stringBuilder.AppendLine(LOC.UI.Mods_DeleteTranslationModFIlesPrompt);
						UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, stringBuilder.ToString(), () =>
						{
							StringBuilder stringBuilder2;
							using (StringBuilderCache.AcquireWithUsing(out stringBuilder2))
							{
								foreach (ModsSingleton.ModTranslationDataLoaded allItem in selection.GetAllItems())
								{
									stringBuilder2.Append(allItem.Describe());
									stringBuilder2.Append(" : ");
									ModsSingleton.ModTranslationFileDeleteStatusEnum modTranslationFileDeleteStatusEnum = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.DeleteTranslation(mod, allItem);
									if (modTranslationFileDeleteStatusEnum == ModsSingleton.ModTranslationFileDeleteStatusEnum.Success)
									{
										stringBuilder2.Append(LOC.UI.UI_Success);
									}
									else
									{
										stringBuilder2.Append(modTranslationFileDeleteStatusEnum.ToString());
									}
									stringBuilder2.AppendLine();
								}
								UpdateAll();
								UiOverlay.Instance.OkPopup(LOC.UI.Mods_DeleteInstalledTranslationModFiles, stringBuilder2.ToString());
							}
						});
					}
				}
			})
			.OpenPopup(LOC.UI.ChooseLanguage, LOC.UI.ChooseModTranslationGenerationLanguage, allowClosing: true, showCancel: true);
	}

	private void ExportLocalizationButtonClicked()
	{
		if (!_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var item) || item.LoadedTranslations == null)
		{
			return;
		}
		CompoundItemList.StartBuilder<ModsSingleton.ModTranslationDataLoaded, StringAdvancedItemListItemPrefab>().WithToStringOnly((ModsSingleton.ModTranslationDataLoaded a, StringRepresentationHelperEnum b) => ((uint)b <= 1u || b != StringRepresentationHelperEnum.Description) ? a.TranslationLocale.LocaleName : a.Describe()).WithBasicToStringEverything()
			.Build(item.LoadedTranslations.Values.ToFrozenDataSource())
			.ToChooseItemsComplexPopupBuilderGenericString()
			.WithSingularStartingSelection()
			.WithSuccessfulOnlyOnFinished((Selection<ModsSingleton.ModTranslationDataLoaded> selection) =>
			{
				if (selection.SelectedItemsCount > 0)
				{
					if (selection.SelectedItemsCount == 1)
					{
						ModTranslationData data = selection.GetFirstItemOrNull().Data;
						UiOverlay.Instance.FileSavePopup(data.GetDefaultFileName(), data.ToJsonPrettyPrint(), null);
					}
					else
					{
						List<MyZipUtility.ZipEntryStub> list = new List<MyZipUtility.ZipEntryStub>();
						foreach (ModsSingleton.ModTranslationDataLoaded allItem in selection.GetAllItems())
						{
							list.Add(new MyZipUtility.ZipEntryStub(allItem.Data.GetDefaultFileName(), allItem.Data.ToJsonPrettyPrint()));
						}
						byte[] data2 = MyZipUtility.CreateZipFile(list);
						UiOverlay.Instance.FileSavePopup("multiple translations.zip", data2, null);
					}
				}
			})
			.OpenPopup(LOC.UI.ChooseLanguage, LOC.UI.Mods_ExportTranslationModFIlesPickerPrompt, allowClosing: true, showCancel: true);
	}

	private void ImportLocalizationButtonClicked()
	{
		if (!_gameItemListAdapter.TryGetCurrentlyFocusedItem(out var _))
		{
			return;
		}
		UiOverlay.Instance.FileLoadPopup(LOC.UI.SelectModTranslationFilesPrompt, (MyFileBrowserSingleton.UploadResult result) =>
		{
			if (result.GotFiles)
			{
				bool flag = false;
				StringBuilder stringBuilder;
				using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
				{
					foreach (MyFileBrowserSingleton.UploadResult.FileResult result in result.Results)
					{
						string fileName = Path.GetFileName(result.Path);
						ModsSingleton.ModTranslationFileLoadStatusEnum modTranslationFileLoadStatusEnum = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.ValidateTranslationFile(result.StringData, out var data, out var targetMod);
						if (modTranslationFileLoadStatusEnum == ModsSingleton.ModTranslationFileLoadStatusEnum.Success)
						{
							string rememberedNameOrGuid = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.GetRememberedNameOrGuid(data.TargetModGuid);
							SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.ReplaceSavedTranslation(targetMod, data, forceReplace: false);
							stringBuilder.AppendLine(LOC.UI.SuccessfulyLoadedModTranslationFile(fileName, rememberedNameOrGuid, data.ParsedTargetModVersion.StringRepresentation));
						}
						else
						{
							stringBuilder.AppendLine(LOC.UI.FailedLoadingModTranslationFile(fileName, modTranslationFileLoadStatusEnum.ToString()));
							flag = true;
						}
					}
					UiOverlay.Instance.OkPopup(flag ? LOC.UI.UI_Error : LOC.UI.UI_Success, stringBuilder.ToString());
					UpdateAll();
				}
			}
		}, new List<MyFileBrowserSingleton.FileType>
		{
			new MyFileBrowserSingleton.FileType(LOC.UI.ModTranslationFiles, ".json")
		}, multifile: true, allowAllFiles: false);
	}

	public void CreateNewModButtonClicked()
	{
		UiOverlay.Instance.OpenContextMenuPopup(new List<PopupChoice>
		{
			new PopupChoice
			{
				Text = "Empty mod",
				Action = () =>
				{
					Mod.CreateMinimal().SaveToFilePopup();
				}
			},
			new PopupChoice
			{
				Text = "Mod with items",
				Action = () =>
				{
					SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.ResetAllTextures();
					IEnumerable<ItemPrefab> initialData = from x in ItemPrefab.ItemPrefabManager.AllNonDefaultItems
						where x.prefab.Equipable
						select x.prefab;
					CompoundItemList.StartBuilder<ItemPrefab, StringAdvancedItemListItemPrefab>().WithDefaultItemPrefabName().WithDefaultItemPrefabSorting()
						.WithDefaultItemPrefabFolders()
						.WithDefaultStringAdvancedPrefabSetItem()
						.WithDefaultListItemPool()
						.Build(initialData.ToFrozenDataSource())
						.ToChooseItemsComplexPopupBuilderItemPrefab()
						.WithSingularStartingSelection()
						.WithOnFinished((UiOverlay.PopupFinishTypesEnum a, Selection<ItemPrefab> b) =>
						{
							if (a == UiOverlay.PopupFinishTypesEnum.Successful)
							{
								ModCreationLive2DControllersContainer data = SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.GetModCreationData("Quick mod with items");
								try
								{
									Mod.CreateItemMod((from t in b.GetAllItems()
										select t.CloneWithTextures(data)).ToList()).SaveToFilePopup();
								}
								finally
								{
									if (data != null)
									{
										((IDisposable)data).Dispose();
									}
								}
							}
						})
						.OpenPopup("Choose items", "Choose items to generate minimal mod with their textures.", allowClosing: true, showCancel: true);
				}
			},
			new PopupChoice
			{
				Text = "Advanced mod with items",
				Action = () =>
				{
					UiOverlay.Instance.OpenModCreatorPopup();
				}
			}
		});
	}

	private void LoadModInner()
	{
		SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.GetFromFile((MyFileBrowserSingleton.UploadResult t) =>
		{
			if (t.GotFiles)
			{
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (MyFileBrowserSingleton.UploadResult.FileResult result2 in t.Results)
				{
					if (result2.Success)
					{
						SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.ClearLog();
						Result result = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LoadModFromZipDataFromOutside(result2.Data);
						if (result.IsSuccess)
						{
							num++;
						}
						else
						{
							stringBuilder.Append(result.GetDescription());
						}
						stringBuilder.Append(SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.Log);
					}
					else
					{
						stringBuilder.Append("Error loading " + result2.Path + "!");
					}
				}
				if (stringBuilder.Length == 0 && num == t.Results.Count)
				{
					UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.Mods_LoadingSuccess(t.Results.Count));
				}
				else
				{
					UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.Mods_LoadingError(num, t.Results.Count) + stringBuilder);
				}
				UpdateAll();
			}
		}, new List<MyFileBrowserSingleton.FileType>
		{
			new MyFileBrowserSingleton.FileType(LOC.UI.UI_Mods, ".zip")
		}, multifile: true, allowAllFiles: false);
	}

	public void LoadModsButtonClicked()
	{
		if (OptionsStatic.AcceptedModsDangerWarning.Value)
		{
			LoadModInner();
			return;
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(LOC.UI.Mods_SafetyWarning1);
			using (stringBuilder.StartColorTagUsing(Color.red))
			{
				stringBuilder.AppendLine(LOC.UI.Mods_SafetyWarning2);
			}
			stringBuilder.AppendLine(LOC.UI.Mods_SafetyWarning3);
			stringBuilder.AppendLine(LOC.UI.Mods_SafetyWarning4);
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, stringBuilder.ToString(), () =>
			{
				OptionsStatic.AcceptedModsDangerWarning.SetValue(value: true);
				LoadModInner();
			});
		}
	}

	private void UpdateAll()
	{
		_compoundGameItemListAdapter.UpdateAll();
	}
}
