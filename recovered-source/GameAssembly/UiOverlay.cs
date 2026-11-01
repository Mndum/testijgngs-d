using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using Fungus;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiOverlay : UIManager
{
	public delegate Result StringValidationDelegate(string input);

	public delegate string StringProcessingDelegate(string input);

	public enum PopupFinishTypesEnum
	{
		Closed,
		Cancelled,
		Successful
	}

	public TextMeshProUGUI developmentBuildText;

	[Header("Say Dialogs")]
	[SerializeField]
	public SayDialogPopup _mainSayDialogPopupGameObject;

	[SerializeField]
	public SayDialogPopup _speechSayDialogPopup;

	[Header("Common Prefabs")]
	public PooledPrefabReference<DraggedItem> draggedItemPrefab;

	public PooledPrefabReference<CommonButtonHandler> commonButton;

	public PooledPrefabReference<GameItemContainer> gameItemContainerPrefab;

	public PooledPrefabReference<Tooltip> defaultTooltip;

	public PooledPrefabReference<FloatingText> defaultFloatingText;

	public PooledPrefabReference<ItemStats> itemStats;

	public PooledPrefabReference<ItemColorSlotPicker> itemColorSlotPicker;

	public PooledPrefabReference<StringAdvancedItemListItemPrefab> stringAdvancedItemListPrefab;

	public PooledPrefabReference<StringGenericDataDisplay> stringGenericDataDisplay;

	public PooledPrefabReference<ItemModifiableVariableDisplay> itemModifiableVariableDisplay;

	public PooledPrefabReference<ModDisplay> modItemListDisplay;

	[Header("Popup Holder")]
	[SerializeField]
	private SerializableAutomaticReference<SimplePopup> _popupPrefabRef;

	[SerializeField]
	private SerializableAutomaticReference<TimeoutSimplePopup> _timeoutPopupPrefabRef;

	[SerializeField]
	private SerializableAutomaticReference<InputPopup> _textInputPopupPrefabRef;

	[SerializeField]
	private SerializableAutomaticReference<ColorInputPopup> _colorInputPopupPrefabRef;

	[SerializeField]
	private SerializableAutomaticReference<FungusOverlayPopup> _fungusOverlayPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ProgressPopup> _progressPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<NarrativeLogPopup> _narrativeLogPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ClothierOrdersPopup> _clothierOrdersPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<FisherSellFishPopup> _fisherSellFishPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<NunRepairPopup> _nunRepairPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<AnnalieShopPopup> _annalieShopPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ShopPopup> _shopPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<SavePopup> _savePopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ItemsPopup> _itemsPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<AchievementsPopup> _achievementsPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<OptionsPopup> _optionsPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ModCreatorPopup> _modCreatorPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ModsPopup> _modsPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<BrowserPopup> _browserPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ContextMenuPopup> _contextMenuPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ToyPopup> _toyPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<ChooseItemsComplexPopup> _chooseGameItemsPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<FishingJournalPopup> _fishingJournalPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<BotStatusAppPopup> _botStatusAppPopupRef;

	[SerializeField]
	private SerializableAutomaticReference<SheepPopup> _sheepPopup;

	[SerializeField]
	private SerializableAutomaticReference<CheatsPopup> _cheatsPopup;

	[Header("UI Colors")]
	public Color NormalColor;

	public Color AltNormalColor;

	public Color ActiveColor;

	public Color InactiveColor;

	public Color GreenColor;

	public Color RedColor;

	public static Color DefaultFungusOverlayColor = new Color(0f, 0f, 0f, 0.2f);

	public static readonly int FunugsOverlayLayer = 40;

	public static readonly int PopupLayer = 40;

	public static readonly int SayDialogLayer = 50;

	public static readonly int MenuDialogLayer = 60;

	public static readonly StringValidationDelegate NameLengthValidation = (string input) =>
	{
		if (input.Length < 2)
		{
			return Result.FailWithDescription(LOC.UI.Naming_TooShort);
		}
		return (input.Length > 20) ? Result.FailWithDescription(LOC.UI.Naming_TooLong) : Result.Success;
	};

	public static readonly StringProcessingDelegate NameTrimProcessing = (string input) => input.Trim();

	public static readonly StringProcessingDelegate NameTrimToLowerProcessing = (string input) => input.Trim().ToLowerInvariant();

	private static readonly int[] _stackableCountsAdd = new int[4] { 1, 5, 15, 50 };

	private static readonly int[] _nonStackableArrayCountsAdd = new int[3] { 1, 2, 3 };

	public new static UiOverlay Instance => (UiOverlay)UIManager.Instance;

	private void UpdateDevBuildInfoText()
	{
		bool flag = false;
		developmentBuildText.gameObject.SetActive(flag);
		if (flag)
		{
			developmentBuildText.text = "Version " + Application.version;
		}
	}

	public new void Awake()
	{
		base.Awake();
		SayDialog.MainSayDialog = _mainSayDialogPopupGameObject.sayDialog;
		UpdateDevBuildInfoText();
	}

	public ButtonColorPalette GetButtonColorPalette(CommonButtonColorType buttonColorType)
	{
		return buttonColorType switch
		{
			CommonButtonColorType.Normal => new ButtonColorPalette(NormalColor, ActiveColor), 
			CommonButtonColorType.Green => new ButtonColorPalette(GreenColor, ActiveColor), 
			CommonButtonColorType.Red => new ButtonColorPalette(RedColor, ActiveColor), 
			_ => new ButtonColorPalette(AltNormalColor, ActiveColor), 
		};
	}

	public void OpenFungusOverlay()
	{
		OpenFungusOverlay(DefaultFungusOverlayColor);
	}

	public void OpenFungusOverlay(Color overlayColor)
	{
		CloseFungusOverlay();
		FungusOverlayPopup fungusOverlayPopup = Instance._fungusOverlayPopupRef.InstantiatePrefab();
		fungusOverlayPopup.sortOrder = FunugsOverlayLayer;
		fungusOverlayPopup.Open();
		fungusOverlayPopup.GetComponent<Image>().color = overlayColor;
	}

	public bool HasFungusOverlay()
	{
		for (int num = currentlyOpenPopups.Count - 1; num >= 0; num--)
		{
			if (currentlyOpenPopups[num] is FungusOverlayPopup)
			{
				return true;
			}
		}
		return false;
	}

	public void CloseFungusOverlay()
	{
		for (int num = currentlyOpenPopups.Count - 1; num >= 0; num--)
		{
			if (currentlyOpenPopups[num] is FungusOverlayPopup popup)
			{
				ClosePopup(popup);
			}
		}
	}

	public void ProgressPopup(string title, string text, Func<IProgressManager, UniTask> loadingCreationFunc, Action OnFinished = null)
	{
		ProgressPopup progressPopup = _progressPopupRef.InstantiatePrefab();
		progressPopup.sortOrder = PopupLayer;
		progressPopup.Open(title, text, loadingCreationFunc, OnFinished);
	}

	public void BuyPopup(string title, int price, Action boughtaction, string text = null, Action noaction = null, Action nomoneyaction = null)
	{
		if (text == null)
		{
			text = LOC.UI.Shop_BuyPrompt(price);
		}
		SimplePopup(title, text, () =>
		{
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.TryTakeMoney(price))
			{
				boughtaction?.Invoke();
			}
			else
			{
				OkPopup(LOC.UI.UI_Failed, LOC.UI.Shop_NotEnoughMoney, nomoneyaction);
			}
		}, noaction);
	}

	public TimeoutSimplePopup SimplePopupWithTimeout(string title, Func<TimeSpan, string> textGenerator, List<PopupChoice> choices, int defaultChoiceIndex, TimeSpan timeout)
	{
		if (choices.Count == 0)
		{
			choices.Add(new PopupChoice
			{
				Text = LOC.Polyglot.MENU_LABEL_OK
			});
		}
		TimeoutSimplePopup timeoutSimplePopup = _timeoutPopupPrefabRef.InstantiatePrefab();
		timeoutSimplePopup.sortOrder = PopupLayer;
		timeoutSimplePopup.Open(title, textGenerator, choices, defaultChoiceIndex, timeout);
		return timeoutSimplePopup;
	}

	public SimplePopup SimplePopup(string title, string text, Action yesaction, Action noaction = null)
	{
		string text2 = (LoadingManager.LocalizationLoadingSuccessful ? LOC.Polyglot.MENU_LABEL_YES : "Yes");
		string text3 = (LoadingManager.LocalizationLoadingSuccessful ? LOC.Polyglot.MENU_LABEL_NO : "No");
		List<PopupChoice> choices = new List<PopupChoice>
		{
			new PopupChoice
			{
				Text = text2,
				Action = yesaction
			},
			new PopupChoice
			{
				Text = text3,
				Action = noaction
			}
		};
		return Popup(title, text, choices);
	}

	public SimplePopup Popup(string title, string text, List<PopupChoice> choices)
	{
		if (choices.Count == 0)
		{
			choices.Add(new PopupChoice
			{
				Text = LOC.Polyglot.MENU_LABEL_OK
			});
		}
		SimplePopup simplePopup = _popupPrefabRef.InstantiatePrefab();
		simplePopup.sortOrder = PopupLayer;
		simplePopup.Open(title, text, choices);
		return simplePopup;
	}

	public void FileLoadPopup(string description, Action<MyFileBrowserSingleton.UploadResult> finishFunc, List<MyFileBrowserSingleton.FileType> filters = null, bool multifile = false, bool allowAllFiles = true)
	{
		List<PopupChoice> choices = new List<PopupChoice>
		{
			new PopupChoice
			{
				Text = LOC.Polyglot.MENU_LABEL_OK,
				Action = () =>
				{
					SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.GetFromFile(finishFunc, filters, multifile, allowAllFiles);
				},
				Trigger = PopupChoice.TriggerType.PointerDown
			}
		};
		Popup(LOC.UI.UI_ImportFile, description, choices);
	}

	public void FileSavePopup(string filename, byte[] data, Action<MyFileBrowserSingleton.DownloadResult> after)
	{
		List<PopupChoice> choices = new List<PopupChoice>
		{
			new PopupChoice
			{
				Text = LOC.Polyglot.MENU_LABEL_OK,
				Action = () =>
				{
					SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.SaveToFile(after, data, filename);
				},
				Trigger = PopupChoice.TriggerType.PointerDown
			}
		};
		Popup(LOC.UI.UI_ExportFile, LOC.UI.UI_ExportFilePrompt(filename), choices);
	}

	public void FileSavePopup(string filename, string data, Action<MyFileBrowserSingleton.DownloadResult> after)
	{
		List<PopupChoice> choices = new List<PopupChoice>
		{
			new PopupChoice
			{
				Text = LOC.Polyglot.MENU_LABEL_OK,
				Action = () =>
				{
					SingletonMonoBehaviour<MyFileBrowserSingleton>.Instance.SaveToFile(after, data, filename);
				},
				Trigger = PopupChoice.TriggerType.PointerDown
			}
		};
		Popup(LOC.UI.UI_ExportFile, LOC.UI.UI_ExportFilePrompt(filename), choices);
	}

	public SimplePopup OkPopup(string title, string text, Action okaction = null)
	{
		string text2 = (LoadingManager.LocalizationLoadingSuccessful ? LOC.Polyglot.MENU_LABEL_OK : "OK");
		List<PopupChoice> choices = new List<PopupChoice>
		{
			new PopupChoice
			{
				Text = text2,
				Action = okaction
			}
		};
		return Popup(title, text, choices);
	}

	public SimplePopup OkPopupError(string text, Action okaction = null)
	{
		return OkPopup(LOC.UI.UI_Error, text, okaction);
	}

	public SimplePopup OkPopupSuccess(string text, Action okaction = null)
	{
		return OkPopup(LOC.UI.UI_Success, text, okaction);
	}

	public SimplePopup OkPopupWarning(string text, Action okaction = null)
	{
		return OkPopup(LOC.Polyglot.MENU_LABEL_WARNING, text, okaction);
	}

	public InputPopup InputPopup(string title, string text, Action<string> okaction, string defaultInput = "", TMP_InputField.ContentType type = TMP_InputField.ContentType.Standard)
	{
		List<PopupStringChoice> list = new List<PopupStringChoice>();
		list.Add(new PopupStringChoice
		{
			Text = LOC.Polyglot.MENU_LABEL_OK,
			Action = okaction
		});
		InputPopup inputPopup = _textInputPopupPrefabRef.InstantiatePrefab();
		inputPopup.sortOrder = PopupLayer;
		inputPopup.Open(list, title, text, defaultInput, type);
		return inputPopup;
	}

	public void InputPopupWithValidation(string title, string text, Func<string, string> areYouSureText, Action<string> OnPicked, string defaultInput = "", TMP_InputField.ContentType type = TMP_InputField.ContentType.Standard, StringValidationDelegate validate = null, StringProcessingDelegate process = null)
	{
		if (validate == null)
		{
			validate = (string x) => Result.Success;
		}
		if (process == null)
		{
			process = (string x) => x;
		}
		Start();
		void Start()
		{
			Instance.InputPopup(title, text, (string t) =>
			{
				string value = process(t);
				Result result = validate(value);
				if (result.IsSuccess)
				{
					Instance.SimplePopup(LOC.UI.UI_AreYouSure, areYouSureText(value), () =>
					{
						OnPicked?.Invoke(value);
					}, Start);
				}
				else
				{
					Instance.OkPopup(LOC.UI.UI_Error, result.GetDescription(), Start);
				}
			}, defaultInput, type);
		}
	}

	public void OpenNarrativeLog()
	{
		if (!TryGetTopPopupOfType<NarrativeLogPopup>(out var _))
		{
			NarrativeLogPopup narrativeLogPopup = _narrativeLogPopupRef.InstantiatePrefab();
			narrativeLogPopup.sortOrder = MenuDialogLayer + 1;
			narrativeLogPopup.Open();
		}
	}

	public AnnalieShopPopup OpenAnnalieShop(Action onFinished, bool isWithBot)
	{
		AnnalieShopPopup annalieShopPopup = _annalieShopPopupRef.InstantiatePrefab();
		annalieShopPopup.Open(onFinished, isWithBot);
		return annalieShopPopup;
	}

	public ClothierOrdersPopup OpenClothierOrdersShop(Action onFinished)
	{
		ClothierOrdersPopup clothierOrdersPopup = _clothierOrdersPopupRef.InstantiatePrefab();
		clothierOrdersPopup.Open(onFinished);
		return clothierOrdersPopup;
	}

	public FisherSellFishPopup OpenFisherSellFish(Action onFinished)
	{
		FisherSellFishPopup fisherSellFishPopup = _fisherSellFishPopupRef.InstantiatePrefab();
		fisherSellFishPopup.Open(onFinished);
		return fisherSellFishPopup;
	}

	public NunRepairPopup OpenNunRepairShop()
	{
		NunRepairPopup nunRepairPopup = _nunRepairPopupRef.InstantiatePrefab();
		nunRepairPopup.Open();
		return nunRepairPopup;
	}

	public void OpenShop(string id, Action onFinished)
	{
		_shopPopupRef.InstantiatePrefab().Open(id, onFinished);
	}

	public void OpenSaveMenu()
	{
		_savePopupRef.InstantiatePrefab().OpenSave();
	}

	public void OpenLoadMenu()
	{
		_savePopupRef.InstantiatePrefab().OpenLoad();
	}

	public void OpenItemsMenu()
	{
		_itemsPopupRef.InstantiatePrefab().Open();
	}

	public void OpenAchievements()
	{
		_achievementsPopupRef.InstantiatePrefab().Open();
	}

	public void OpenOptions()
	{
		_optionsPopupRef.Get().InstantiatePrefab().Open();
	}

	public void OpenToyPopup()
	{
		_toyPopupRef.Get().InstantiatePrefab().Open();
	}

	public void OpenModCreatorPopup(List<ItemPrefab> items = null)
	{
		_modCreatorPopupRef.InstantiatePrefab().Open(items ?? new List<ItemPrefab>());
	}

	public void OpenModsPopup()
	{
		_modsPopupRef.InstantiatePrefab().Open();
	}

	public void OpenFishingJournal()
	{
		_fishingJournalPopupRef.InstantiatePrefab().Open();
	}

	public BotStatusAppPopup OpenBotStatusApp()
	{
		BotStatusAppPopup botStatusAppPopup = _botStatusAppPopupRef.InstantiatePrefab();
		botStatusAppPopup.Open(isPrologue: false);
		return botStatusAppPopup;
	}

	public BotStatusAppPopup OpenBotStatusAppPrologue()
	{
		BotStatusAppPopup botStatusAppPopup = _botStatusAppPopupRef.InstantiatePrefab();
		botStatusAppPopup.Open(isPrologue: true);
		return botStatusAppPopup;
	}

	public void OpenAddItemCheat()
	{
		CompoundItemList.StartBuilder<ItemPrefab, StringAdvancedItemListItemPrefab>().WithDefaultItemPrefabName().WithDefaultItemPrefabSorting()
			.WithDefaultItemPrefabFolders()
			.WithDefaultStringAdvancedPrefabSetItem()
			.WithDefaultListItemPool()
			.Build(DataSourceUtils.CreateMultipleUse(() => ItemPrefab.ItemPrefabManager.AllNonDefaultItems.Select(((GameId id, ItemPrefab prefab) x) => x.prefab)))
			.ToChooseItemsComplexPopupBuilderItemPrefab()
			.WithSingularStartingSelection()
			.WithOnFinished((PopupFinishTypesEnum finishedType, Selection<ItemPrefab> selection) =>
			{
				if (finishedType == PopupFinishTypesEnum.Successful && selection.SelectedItemsCount > 0)
				{
					GameVariables.Current.MarkAsCheater();
					if (selection.SelectedItemsCount == 1)
					{
						ItemPrefab itemPrefab = selection.GetFirstItemOrNull();
						if (itemPrefab != null && itemPrefab.TryGetItemGameId(out var gameId))
						{
							Item item = Item.CreateRandomItem(gameId);
							List<PopupChoice> choices = (item.IsStackable ? _stackableCountsAdd : _nonStackableArrayCountsAdd).Select((int x) => new PopupChoice(itemPrefab.Name + " x " + x, () =>
							{
								Item.ItemManager itemManager2 = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
								using (itemManager2.DelayCallbacks("AddItems"))
								{
									if (item.IsStackable)
									{
										item.SetCount(x);
										itemManager2.AddItem(item);
									}
									else
									{
										itemManager2.AddItem(item);
										for (int i = 0; i < x - 1; i++)
										{
											itemManager2.AddItem(Item.CreateRandomItem(gameId));
										}
									}
								}
							})).ToList();
							Instance.OpenContextMenuPopup(choices);
						}
					}
					else
					{
						if (!selection.GetAllItems().All((ItemPrefab x) => x.IsStackable))
						{
							Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
							using (itemManager.DelayCallbacks("AddItems"))
							{
								foreach (ItemPrefab allItem in selection.GetAllItems())
								{
									if (allItem.TryGetItemGameId(out var gameId2))
									{
										itemManager.AddItem(Item.CreateRandomItem(gameId2));
									}
								}
								return;
							}
						}
						List<PopupChoice> choices2 = _stackableCountsAdd.Select((int x) => new PopupChoice("All x " + x, () =>
						{
							Item.ItemManager itemManager2 = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
							using (itemManager2.DelayCallbacks("AddItems"))
							{
								foreach (ItemPrefab allItem2 in selection.GetAllItems())
								{
									if (allItem2.TryGetItemGameId(out var gameId3) && allItem2.IsStackable)
									{
										Item item2 = Item.CreateRandomItem(gameId3);
										item2.SetCount(x);
										itemManager2.AddItem(item2);
									}
								}
							}
						})).ToList();
						Instance.OpenContextMenuPopup(choices2);
					}
				}
			})
			.OpenPopup(LOC.UI.UI_ChooseItems, LOC.UI.UI_ChooseItemsPrompt, allowClosing: true, showCancel: true);
	}

	public SheepPopup OpenSheepPopup()
	{
		SheepPopup sheepPopup = _sheepPopup.InstantiatePrefab();
		sheepPopup.Open();
		return sheepPopup;
	}

	public CheatsPopup OpenCheatsPopup()
	{
		CheatsPopup cheatsPopup = _cheatsPopup.InstantiatePrefab();
		cheatsPopup.Open();
		return cheatsPopup;
	}

	public void ColorInputPopup(string title, string text, Action<Color> yesaction, Action<Color> noaction, Color32 startColor)
	{
		ColorInputPopup colorInputPopup = _colorInputPopupPrefabRef.InstantiatePrefab();
		colorInputPopup.sortOrder = PopupLayer;
		colorInputPopup.Open(title, text, yesaction, noaction, startColor);
	}

	public void OpenContextMenuPopup(List<PopupChoice> choices, bool addCancel = true)
	{
		ContextMenuPopup contextMenuPopup = _contextMenuPopupRef.InstantiatePrefab();
		if (addCancel)
		{
			choices.Add(new PopupChoice(LOC.UI.UI_Cancel));
		}
		contextMenuPopup.Open(choices);
	}

	public void OpenSlotEquipItemPopup(ItemSlot targetSlot, Action<PopupFinishTypesEnum> onFinished = null)
	{
		Selection<Item> selection = SelectionFactory.CreateInstance<Item>();
		selection.PartialAllowed = false;
		if (!targetSlot.AllowedMultipleItems)
		{
			selection.MaxUniqueItemCount = 1;
		}
		if (GameVariables.Current.itemManager.TryGetItemsAtSlot(targetSlot.Id, out var _items))
		{
			foreach (Item item in _items)
			{
				selection.AddItemFull(item);
			}
		}
		List<Folder<Item>> list;
		using (MyCollectionPool<Folder<Item>>.GetList(out list))
		{
			list.Add(Folder<Item>.AllFolder);
			list.Add(new Folder<Item>(() => LOC.UI.Item_Favourite, (Item x) => x.IsFavourite));
			CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithFolders(list)
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.Build(DataSourceUtils.CreateMultipleUse(() => from x in GameVariables.Current.itemManager.AllItemsEnumerable()
					where x.CanGoInto(targetSlot)
					select x))
				.ToChooseItemsComplexPopupBuilderItem()
				.WithStartingSelection(selection)
				.WithOnFinished((PopupFinishTypesEnum finishedType, Selection<Item> endSelection) =>
				{
					if (finishedType == PopupFinishTypesEnum.Successful)
					{
						Item.ItemManager itemManager = GameVariables.Current.itemManager;
						using (itemManager.DelayCallbacks("Equipping items popup"))
						{
							foreach (Item allItem in endSelection.GetAllItems())
							{
								if (allItem.GetItemPrefab().IsEquipable(out var slotEquipData) && !allItem.IsEquipped())
								{
									itemManager.Equip(allItem, slotEquipData.TargetSlot, forceRemoveForbidden: true);
								}
							}
						}
					}
					onFinished?.Invoke(finishedType);
				})
				.OpenPopup(LOC.UI.UI_Change, LOC.UI.ChooseEquipmentPrompt(targetSlot.FriendlyName), allowClosing: true, showCancel: true);
		}
	}

	[MoonSharpHidden]
	public ChooseItemsComplexPopup ChooseItemsComplexPopup(string title, string text, ChooseItemsComplexPopup.IBuilderAdapterReady adapterReady, bool allowClosing, bool showCancel)
	{
		ChooseItemsComplexPopup chooseItemsComplexPopup = _chooseGameItemsPopupRef.InstantiatePrefab();
		chooseItemsComplexPopup.Prepare(title, text, allowClosing, showCancel);
		chooseItemsComplexPopup.Open(adapterReady);
		return chooseItemsComplexPopup;
	}

	[MoonSharpHidden]
	public void OpenExternalURLPopup(string url, string fullUrl = null)
	{
		if (fullUrl == null)
		{
			fullUrl = url;
		}
		Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.LinkPrompt(url.TMPColorSurround(Color.green)), () =>
		{
			Application.OpenURL(fullUrl);
		});
	}

	[UsedInLua]
	public BrowserPopup OpenBrowser(string website = null)
	{
		if (website == null)
		{
			website = SingletonMonoBehaviour<InternetSingleton>.Instance.DefaultWebsite;
		}
		if (TryGetTopPopupOfType<BrowserPopup>(out var popup))
		{
			popup.ChangeWebsite(website);
			return popup;
		}
		BrowserPopup browserPopup = _browserPopupRef.InstantiatePrefab();
		browserPopup.Open(website);
		return browserPopup;
	}

	public BotStatusAppPopup GetCurrentlyOpenedBotStatusApp()
	{
		if (TryGetTopPopupOfType<BotStatusAppPopup>(out var popup))
		{
			return popup;
		}
		return null;
	}

	[UsedInLua]
	public BrowserPopup GetCurrentlyOpenedBrowser()
	{
		if (TryGetTopPopupOfType<BrowserPopup>(out var popup))
		{
			return popup;
		}
		return OpenBrowser();
	}

	public void OpenNativeFolderPath(string instanceModPath)
	{
		OkPopup(LOC.UI.UI_Error, LOC.UI.OpeningFolderNotSupportedOnThisPlatform(instanceModPath) + "\n\n" + LOC.UI.PathCopiedToClipboard);
		GUIUtility.systemCopyBuffer = instanceModPath;
	}

	public void ShowFloatingTextAtMouse(string text)
	{
		ShowFloatingText(MouseToWorld().ToVector2(), text);
	}

	public void ShowFloatingText(Vector2 position, string text)
	{
		FloatingText fromPool = defaultFloatingText.GetFromPool(OverlayTransform);
		fromPool.transform.position = position;
		fromPool.StartFloating(text);
	}

	public void ShowFloatingText(Vector2 position, StringBuilder text)
	{
		FloatingText fromPool = defaultFloatingText.GetFromPool(OverlayTransform);
		fromPool.transform.position = position;
		fromPool.StartFloating(text);
	}

	private void EquipItemSingleSlot(Item item, ItemSlot slot, Action finished)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			switch (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.Equip(item, slot, forceRemoveForbidden: false, stringBuilder))
			{
			case Item.ItemManager.EquipResults.Success:
				finished?.Invoke();
				break;
			case Item.ItemManager.EquipResults.FailedBlocked:
				stringBuilder.Insert(0, "\n");
				stringBuilder.Insert(0, LOC.UI.Equipment_CannotEquipPromptStart);
				stringBuilder.AppendLine(LOC.UI.Equipment_CannotEquipPromptEnd(item.GetName()));
				Instance.SimplePopup(LOC.UI.Equipment_CannotEquipPopupTitle, stringBuilder.ToString(), () =>
				{
					SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.Equip(item, slot, forceRemoveForbidden: true);
					finished?.Invoke();
				});
				break;
			case Item.ItemManager.EquipResults.FailedMissingRequired:
				Instance.OkPopup(LOC.UI.Equipment_CannotEquipPopupTitle, stringBuilder.ToString(), () =>
				{
					finished?.Invoke();
				});
				break;
			default:
				finished?.Invoke();
				break;
			}
		}
	}

	public void EquipItem(Item item, Action finished = null)
	{
		if (item.GetItemPrefab().IsEquipable(out var slotEquipData))
		{
			EquipItemSingleSlot(item, slotEquipData.TargetSlot, finished);
		}
	}

	public void UnequipItemOrAsk(Item item, Action finished = null)
	{
		Result result = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.UnequipItemDescription(item);
		if (result.IsSuccess)
		{
			finished?.Invoke();
			return;
		}
		Instance.SimplePopup(LOC.UI.Equipment_ButtonUnequip, result.GetDescription() + "\n" + LOC.UI.Equipment_UnequipDependencyPrompt, () =>
		{
			Instance.OkPopup(LOC.UI.UI_Success, SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.UnequipItemDescription(item, withDependencies: true).GetDescription());
			finished?.Invoke();
		});
	}
}
