using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveSlotPrefabScript : AdvancedItemListPrefab
{
	[SerializeField]
	private TextMeshProUGUI description;

	[SerializeField]
	private Button button;

	[SerializeField]
	private TextMeshProUGUI buttonText;

	[SerializeField]
	private Button deleteButton;

	[SerializeField]
	private TMP_InputField notes;

	private int _shownSlot;

	private SaveContainer.SaveSlotData _shownSaveData;

	private SavePopup _savePopup;

	public override bool HasDefaultButton => true;

	private void SetNotes(bool active, string text = "", UnityAction<string> OnChange = null)
	{
		notes.gameObject.SetActive(active);
		notes.text = text;
		notes.onSubmit.SetListenerEnsureNotNull(OnChange);
	}

	private void SetDeleteButton(bool active, UnityAction OnClicked = null)
	{
		deleteButton.gameObject.SetActive(active);
		deleteButton.onClick.SetListenerEnsureNotNull(OnClicked);
	}

	private void SetSaveLoadButtonActiveButton(bool active, string text = "")
	{
		button.gameObject.SetActive(active);
		buttonText.text = text;
	}

	public override Button GetDefaultButton()
	{
		return button;
	}

	public void SetSaveData(SavePopup savePopup, SavePopup.SaveSlotEntry saveSlotEntry)
	{
		_shownSlot = saveSlotEntry.Slot;
		_shownSaveData = saveSlotEntry.SaveData;
		_savePopup = savePopup;
		bool isSaveState = savePopup.IsSaveState;
		bool isAutoSave = savePopup.IsAutoSave;
		if (_shownSaveData != null)
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.Append(isAutoSave ? LOC.UI.SAVE_AutoSlot(_shownSlot + 1) : LOC.UI.SAVE_Slot(_shownSlot + 1));
				stringBuilder.Append(" ");
				stringBuilder.Append(_shownSaveData.GetDescription());
				if (_savePopup.IsLatestSave(_shownSaveData))
				{
					stringBuilder.AppendColorTagStart("green");
					stringBuilder.Append("(" + LOC.UI.RecentSave + ")");
				}
				description.SetText(stringBuilder);
			}
			if (isSaveState)
			{
				SetSaveLoadButtonActiveButton(!isAutoSave, _savePopup.GetButtonText());
				SetNotes(!isAutoSave, _shownSaveData.Notes, _shownSaveData.SetNotes);
			}
			else
			{
				SetSaveLoadButtonActiveButton(active: true, _savePopup.GetButtonText());
				SetNotes(!isAutoSave, _shownSaveData.Notes, _shownSaveData.SetNotes);
			}
			SetDeleteButton(!isAutoSave, DeleteButtonClicked);
		}
		else
		{
			description.text = (isAutoSave ? LOC.UI.SAVE_AutoSaveSlotEmpty(_shownSlot + 1) : LOC.UI.SAVE_SlotEmpty(_shownSlot + 1));
			SetSaveLoadButtonActiveButton(isSaveState && !isAutoSave, savePopup.GetButtonText());
			SetNotes(active: false);
			SetDeleteButton(active: false);
		}
		SetDefaultButtonAction(SaveLoadButtonClicked);
	}

	public void FlushNotes()
	{
		if (_shownSaveData != null)
		{
			_shownSaveData.SetNotes(notes.text);
		}
	}

	private void DeleteButtonClicked()
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.SAVE_DeleteSavePrompt(_shownSaveData.SlotOneIndexed, _shownSaveData.GetDescription()), () =>
		{
			SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.DeleteSlot(_shownSaveData);
			_savePopup.UpdateAll();
			SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
		});
	}

	private void SaveLoadButtonClicked()
	{
		SaveContainer.SaveType saveType = _savePopup.SaveType;
		bool isAutoSave = _savePopup.IsAutoSave;
		SaveContainer save = SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save;
		if (_savePopup.IsSaveState)
		{
			SaveContainer.SaveSlotData prevSlot;
			SaveContainer.SaveSlotData newSave;
			if (isAutoSave)
			{
				Debug.LogError("Tried to save to an AutoSave slot!!!");
			}
			else if (save.TryGetSlot(_shownSlot, saveType, out prevSlot))
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_Override, LOC.UI.SAVE_OverridePrompt, () =>
				{
					if (SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.TrySaveFromCurrentGameState(_shownSlot, out var newSave2))
					{
						newSave2.SetNotes(prevSlot.Notes);
						_savePopup.UpdateAll();
						SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
					}
				});
			}
			else if (SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.TrySaveFromCurrentGameState(_shownSlot, out newSave))
			{
				_savePopup.UpdateAll();
				SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.SaveAllChanges();
			}
		}
		else
		{
			if (!save.TryGetSlot(_shownSlot, saveType, out var toLoadData))
			{
				return;
			}
			if (GameScript.HasGameStarted)
			{
				UiOverlay.Instance.SimplePopup(LOC.Polyglot.MENU_IO_LOAD, LOC.UI.SAVE_LoadPrompt, () =>
				{
					toLoadData.TryLoad().ShowResultIfFail();
				});
			}
			else
			{
				toLoadData.TryLoad().ShowResultIfFail();
			}
		}
	}

	public override void AfterReturnedToPool()
	{
		base.AfterReturnedToPool();
		SetNotes(active: false);
		SetDeleteButton(active: false);
		SetSaveLoadButtonActiveButton(active: false);
		_shownSaveData = null;
		_savePopup = null;
	}
}
