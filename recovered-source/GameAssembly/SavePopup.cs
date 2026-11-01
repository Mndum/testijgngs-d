using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SavePopup : PrefabPopup<SavePopup>
{
	public struct SaveSlotEntry
	{
		public int Slot;

		public SaveContainer.SaveSlotData SaveData;

		public void Reset(int slot)
		{
			Slot = slot;
			SaveData = null;
		}

		public void Set(SaveContainer.SaveSlotData saveSlotData)
		{
			SaveData = saveSlotData;
		}
	}

	public enum CurrentSavePopupStateEnum
	{
		Save,
		Load
	}

	[SerializeField]
	private ItemList _folderItemList;

	[SerializeField]
	private AdvancedItemList _advancedItemList;

	[SerializeField]
	private PooledPrefabReference<SaveSlotPrefabScript> _saveSlotPrefabReference;

	private SaveContainer.SaveSlotData _latestSaveSlotData;

	private SaveSlotEntry[] _saveSlotEntries = new SaveSlotEntry[133];

	private AdvancedItemList.AdvancedItemListAdapter<SaveSlotEntry, SaveSlotPrefabScript> _advancedItemListAdapter;

	[SerializeField]
	private TextMeshProUGUI _title;

	[SerializeField]
	private GameObject _importButton;

	[SerializeField]
	private GameObject _exportButton;

	private CurrentSavePopupStateEnum _currentSaveState;

	private SaveContainer.SaveType _saveType;

	private int _currentFolder;

	public bool IsSaveState => _currentSaveState == CurrentSavePopupStateEnum.Save;

	public bool IsAutoSave => _saveType == SaveContainer.SaveType.Auto;

	public SaveContainer.SaveType SaveType => _saveType;

	public bool IsLatestSave(SaveContainer.SaveSlotData saveData)
	{
		return saveData == _latestSaveSlotData;
	}

	private void ReloadSaveSlotEntriesAndUpdateLatest()
	{
		for (int i = 0; i < _saveSlotEntries.Length; i++)
		{
			_saveSlotEntries[i].Reset(i);
		}
		IReadOnlyCollection<SaveContainer.SaveSlotData> saveSlotReadonly = SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.GetSaveSlotReadonly(_saveType);
		long num = _latestSaveSlotData?.TimeUtc ?? (-9223372036854775808L);
		foreach (SaveContainer.SaveSlotData item in saveSlotReadonly)
		{
			if (_saveSlotEntries.IsInRange(item.Slot))
			{
				_saveSlotEntries[item.Slot].Set(item);
			}
			if (item.TimeUtc > num)
			{
				_latestSaveSlotData = item;
			}
		}
	}

	private IEnumerable<SaveSlotEntry> GetCurrentFolderEnumerable()
	{
		ReloadSaveSlotEntriesAndUpdateLatest();
		if (IsAutoSave)
		{
			return _saveSlotEntries.Take(30);
		}
		return _saveSlotEntries.Skip(_currentFolder * 7).Take(7);
	}

	public void Awake()
	{
		_folderItemList.ItemCountFunc = () => 20;
		_folderItemList.ButtonFindFunc = (GameObject tmp) => tmp.GetComponent<Button>();
		_folderItemList.ItemClickedAction = (int i) =>
		{
			FlushNotes();
			_currentFolder = i;
			_saveType = ((i >= 19) ? SaveContainer.SaveType.Auto : SaveContainer.SaveType.Manual);
			UpdateAll();
			_folderItemList.UpdateAll();
		};
		_folderItemList.ItemUpdatedAction = (GameObject tmp, int i, bool focused) =>
		{
			Button component = tmp.GetComponent<Button>();
			if (_currentFolder != i)
			{
				component.interactable = true;
				ColorBlock colors = component.colors;
				colors.disabledColor = UiOverlay.Instance.InactiveColor;
				component.colors = colors;
			}
			else
			{
				component.interactable = false;
				ColorBlock colors2 = component.colors;
				colors2.disabledColor = UiOverlay.Instance.ActiveColor;
				component.colors = colors2;
			}
			if (i < 19)
			{
				tmp.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
			}
			else
			{
				tmp.GetComponentInChildren<TextMeshProUGUI>().text = LOC.UI.UI_AutoSave;
				RectTransform rectTransform = (RectTransform)tmp.transform;
				rectTransform.sizeDelta = new Vector2(150f, rectTransform.sizeDelta.y);
			}
		};
		_advancedItemListAdapter = _advancedItemList.SetAdapter(DataSourceUtils.CreateMultipleUse(GetCurrentFolderEnumerable), _saveSlotPrefabReference.GetPool(), (SaveSlotEntry data, SaveSlotPrefabScript display) =>
		{
			display.SetSaveData(this, data);
		});
		PointerDownEvent.OnPointerDownEvent onPointerDownUnityEvent = _importButton.GetOrAddComponent<PointerDownEvent>().OnPointerDownUnityEvent;
		onPointerDownUnityEvent.RemoveAllListeners();
		onPointerDownUnityEvent.AddListener(ImportFromFileButtonClicked);
		PointerDownEvent.OnPointerDownEvent onPointerDownUnityEvent2 = _exportButton.GetOrAddComponent<PointerDownEvent>().OnPointerDownUnityEvent;
		onPointerDownUnityEvent2.RemoveAllListeners();
		onPointerDownUnityEvent2.AddListener(ExportToFileButtonClicked);
	}

	private void ExportToFileButtonClicked(PointerEventData eventData)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.GetSaveContainerAsString());
		SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.SaveToFile((MyFileBrowserSingleton.DownloadResult t) =>
		{
			if (t.Success)
			{
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.SAVE_ExportSuccess);
			}
			else
			{
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Failed, LOC.UI.SAVE_ExportFailed + (t.Info ?? "Unknown"));
			}
		}, bytes, $"saved-on-{DateTime.Now:yyyy-MM-dd}-at-{DateTime.Now:HH-mm-ss}.mdrg");
	}

	private void ImportFromFileButtonClicked(PointerEventData eventData)
	{
		SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.GetFromFile((MyFileBrowserSingleton.UploadResult t) =>
		{
			if (t.GotFiles)
			{
				MyFileBrowserSingleton.UploadResult.FileResult fileResult = t.FirstFile;
				if (fileResult.Success)
				{
					try
					{
						GameVariables parsedGameVariables = JsonUtility.FromJson<GameVariables>(fileResult.StringData);
						StringBuilder stringBuilder;
						if (parsedGameVariables != null && parsedGameVariables.IsAValidSaveAfter075Update())
						{
							using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
							{
								stringBuilder.AppendLine(LOC.UI.SAVE_LoadFromSlotFileChooseSlotPrompt);
								stringBuilder.AppendLine(LOC.UI.NumbersRangeInfo(1, 133));
								stringBuilder.AppendLine(LOC.UI.SAVE_SlotLoadingWarning);
								stringBuilder.AppendLine();
								string text = stringBuilder.ToString();
								UiOverlay.Instance.InputPopup(LOC.Polyglot.MENU_IO_LOAD, text, (string slot) =>
								{
									if (!int.TryParse(slot, out var result))
									{
										UiOverlay.Instance.OkPopupError(LOC.UI.SAVE_InvalidSlotNumberNotNumber(text));
									}
									else if (result < 1 || result > 133)
									{
										UiOverlay.Instance.OkPopupError(LOC.UI.SAVE_InvalidSlotNumberOutOfRange(result) + "\n" + LOC.UI.NumbersRangeInfo(1, 133));
									}
									else if (SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.SlotExists(result, SaveContainer.SaveType.Manual))
									{
										UiOverlay.Instance.OkPopupError(LOC.UI.SAVE_InvalidSlotNumberAlreadyTaken);
									}
									else
									{
										int num = result - 1;
										SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.OverrideSlot(SaveContainer.SaveSlotData.FromGameVariablesNow(num, parsedGameVariables));
										SetFolderToOneContainingSlot(num);
										UpdateAll();
										UiOverlay.Instance.OkPopupSuccess(LOC.UI.SAVE_SuccessfullyLoadedSlotDataIntoSlotNumber(result));
									}
								}, "1", TMP_InputField.ContentType.IntegerNumber);
								return;
							}
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception, this);
					}
					try
					{
						SaveContainer saveContainer;
						switch (SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.TryLoadSaveContainerFromString(fileResult.StringData, out saveContainer))
						{
						case SaveSingleton.SaveContainerLoadResult.Success:
							UiOverlay.Instance.SimplePopup(LOC.Polyglot.MENU_IO_LOAD, LOC.UI.SAVE_LoadFromFilePrompt, () =>
							{
								UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.SAVE_LoadFromFilePrompt2, () =>
								{
									SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.ReplaceSaveContainer(saveContainer);
									UpdateAll();
									UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.SAVE_LoadSuccessPrompt(fileResult.Path));
								});
							});
							break;
						case SaveSingleton.SaveContainerLoadResult.FailedToValidate_LoadedSaveFromSaveFile:
							UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.SAVE_LoadErrorInvalidSavePrompt);
							break;
						default:
							UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.SAVE_LoadErrorPrompt);
							break;
						}
						return;
					}
					catch (Exception ex)
					{
						Debug.LogException(ex, this);
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.SAVE_LoadErrorPrompt + "\nLog\n" + ex.Message);
						return;
					}
				}
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.SAVE_ImportFailure);
			}
		}, new List<MyFileBrowserSingleton.FileType>
		{
			new MyFileBrowserSingleton.FileType(LOC.UI.SAVE_FileExtension, ".mdrg"),
			new MyFileBrowserSingleton.FileType(LOC.UI.SAVE_SlotFileExtension, ".mdrgslot")
		});
	}

	public void FlushNotes()
	{
		if (_saveType != SaveContainer.SaveType.Manual)
		{
			return;
		}
		foreach (SaveSlotPrefabScript item in _advancedItemListAdapter.GetCurrentlyVisibleRepresentation())
		{
			item.FlushNotes();
		}
	}

	public new void OnDisable()
	{
		base.OnDisable();
		FlushNotes();
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
	}

	public override bool CanClose()
	{
		return true;
	}

	public override void Open()
	{
		base.Open();
		InnerOpen(CurrentSavePopupStateEnum.Load);
	}

	public void OpenSave()
	{
		base.Open();
		InnerOpen(CurrentSavePopupStateEnum.Save);
	}

	public void OpenLoad()
	{
		base.Open();
		InnerOpen(CurrentSavePopupStateEnum.Load);
	}

	public void UpdateAll()
	{
		UpdateLatestSlot();
		_advancedItemListAdapter.UpdateAll();
	}

	private void UpdateLatestSlot()
	{
		_latestSaveSlotData = SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.GetLatestSlot(SaveType);
	}

	private void InnerOpen(CurrentSavePopupStateEnum state)
	{
		_currentSaveState = state;
		_title.text = GetTitleText();
		UpdateLatestSlot();
		if (_latestSaveSlotData != null)
		{
			SetFolderToOneContainingSlot(_latestSaveSlotData.Slot);
		}
		UpdateAll();
		if (!GameScript.HasGameStarted)
		{
			_importButton.gameObject.SetActive(value: true);
			_exportButton.gameObject.SetActive(value: true);
		}
		else
		{
			_importButton.gameObject.SetActive(value: false);
			_exportButton.gameObject.SetActive(value: false);
		}
	}

	private void SetFolderToOneContainingSlot(int latestSaveSlot)
	{
		int currentFolder = Mathf.Clamp(latestSaveSlot / 7, 0, 18);
		_currentFolder = currentFolder;
		_folderItemList.UpdateAll();
	}

	private string GetTitleText()
	{
		if (!IsSaveState)
		{
			return LOC.Polyglot.MENU_IO_LOAD;
		}
		return LOC.Polyglot.MENU_IO_SAVE;
	}

	public string GetButtonText()
	{
		if (!IsSaveState)
		{
			return LOC.Polyglot.MENU_IO_LOAD;
		}
		return LOC.Polyglot.MENU_IO_SAVE;
	}
}
