using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSetsUI : MonoBehaviour
{
	[SerializeField]
	private ItemList _equipmentSetItemList;

	private void SyncEquipmentSets()
	{
		_equipmentSetItemList.UpdateAll();
	}

	public void OnEnable()
	{
		_equipmentSetItemList.ItemCountFunc = () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.Sets.Count;
		_equipmentSetItemList.ButtonFindFunc = (GameObject tmp) => tmp.transform.Find("Load Button").GetComponent<Button>();
		_equipmentSetItemList.ItemClickedAction = (int i) =>
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.Sets.TryGetValue(i, out var set))
			{
				UiOverlay.Instance.Popup(LOC.UI.UI_AreYouSure, LOC.UI.Equipment_EquipSetPrompt(i + 1, set.Name) + "\n\n" + set.GetSummary(), new List<PopupChoice>
				{
					new PopupChoice(LOC.UI.Equipment_YesReplaceCurrent, () =>
					{
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Done, set.Equip(resetFirst: true).GetDescription());
					}),
					new PopupChoice(LOC.Polyglot.MENU_LABEL_YES, () =>
					{
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Done, set.Equip().GetDescription());
					}),
					new PopupChoice(LOC.Polyglot.MENU_LABEL_NO)
				});
			}
		};
		_equipmentSetItemList.ItemUpdatedAction = (GameObject tmp, int i, bool focused) =>
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.Sets.TryGetValue(i, out var set))
			{
				TMP_InputField component = tmp.transform.Find("Notes").GetComponent<TMP_InputField>();
				component.gameObject.SetActive(value: true);
				component.text = set.Name;
				component.onSubmit.SetListener((string t) =>
				{
					set.Name = t;
				});
				component.onEndEdit.SetListener((string t) =>
				{
					set.Name = t;
				});
				tmp.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = set.GetShortSummary();
				tmp.transform.Find("Delete Button").GetComponent<Button>().onClick.SetListener(() =>
				{
					UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Equipment_SetDeletePrompt(i + 1, set.Name), () =>
					{
						SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.RemoveEquipmentSet(set);
						_equipmentSetItemList.UpdateAll();
					});
				});
				tmp.transform.Find("Export Button").GetComponent<Button>().gameObject.GetOrAddComponent<PointerDownEvent>().OnPointerDownUnityEvent.SetListener((PointerEventData T) =>
				{
					string s = JsonUtility.ToJson(set, prettyPrint: true);
					byte[] bytes = Encoding.UTF8.GetBytes(s);
					SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.SaveToFile((MyFileBrowserSingleton.DownloadResult t) =>
					{
						if (t.Success)
						{
							UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.Equipment_SetSavedSuccess(set.Name));
						}
						else
						{
							UiOverlay.Instance.OkPopup(LOC.UI.UI_Failed, LOC.UI.Equipment_SetSavedFailure(set.Name, t.Info ?? "Unknown"));
						}
					}, bytes, GlobalFunctions.ReplaceInvalidChars(set.SafeFilename));
				});
			}
		};
		Transform transform = _equipmentSetItemList.transform.Find("Buttons");
		transform.Find("New Button").GetComponent<Button>().onClick.SetListener(() =>
		{
			UiOverlay.Instance.Popup(LOC.UI.Equipment_Set, LOC.UI.Equipment_SetCreationPrompt, new List<PopupChoice>
			{
				new PopupChoice(LOC.UI.Equipment_SetCreationAllItems, () =>
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddEquipmentSet(EquipmentSet.GenerateFromCurrent("Set from " + DateTime.Now.ToString(CultureInfo.InvariantCulture)));
					_equipmentSetItemList.UpdateAll();
				}),
				new PopupChoice(LOC.UI.Equipment_SetCreationSelect, () =>
				{
					CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
						.WithAllFolder()
						.WithDefaultGameItemSetItem()
						.WithDefaultGameItemListItemPool()
						.Build(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.BotEquippedItems().ToFrozenDataSource())
						.ToChooseItemsComplexPopupBuilderItem()
						.WithSingularStartingSelection()
						.WithOnFinished((UiOverlay.PopupFinishTypesEnum status, Selection<Item> selection) =>
						{
							if (status == UiOverlay.PopupFinishTypesEnum.Successful)
							{
								SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddEquipmentSet(EquipmentSet.GenerateFrom(LOC.UI.Equipment_SetDefaultName(DateTime.Now.ToString(CultureInfo.InvariantCulture)), selection.GetAllItems()));
								_equipmentSetItemList.UpdateAll();
							}
						})
						.OpenPopup(LOC.UI.Equipment_SetCreationSelect, LOC.UI.Equipment_SetCreationSelectPrompt, allowClosing: true, showCancel: true);
				})
			});
		});
		transform.Find("File Button").gameObject.GetOrAddComponent<PointerDownEvent>().OnPointerDownUnityEvent.SetListener((PointerEventData T) =>
		{
			SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.GetFromFile((MyFileBrowserSingleton.UploadResult t) =>
			{
				if (t.GotFiles)
				{
					MyFileBrowserSingleton.UploadResult.FileResult firstFile = t.FirstFile;
					if (firstFile.Success)
					{
						if (!t.FirstFile.Path.EndsWith(".zip"))
						{
							try
							{
								EquipmentSet equipmentSet = JsonUtility.FromJson<EquipmentSet>(firstFile.StringData);
								if (equipmentSet != null)
								{
									UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.Equipment_SetLoadedSuccess(firstFile.Path));
									SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.AddEquipmentSet(equipmentSet);
									_equipmentSetItemList.UpdateAll();
								}
								else
								{
									UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.Equipment_SetLoadedFailure);
								}
								return;
							}
							catch (Exception ex)
							{
								Debug.LogException(ex, this);
								UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.Equipment_SetLoadedFailure + "\nLog\n" + ex.Message);
								return;
							}
						}
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.Equipment_SetLoadedFailureTryingToLoadMod);
					}
					else
					{
						UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.SAVE_ImportFailure);
					}
				}
			}, new List<MyFileBrowserSingleton.FileType>
			{
				new MyFileBrowserSingleton.FileType(LOC.UI.Equipment_SetFiletype, EquipmentSet.EquipmentSetExtension)
			});
		});
		SyncEquipmentSets();
	}
}
