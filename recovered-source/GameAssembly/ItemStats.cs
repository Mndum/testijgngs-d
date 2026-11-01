using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStats : MonoBehaviour, IPooledPrefab
{
	public enum ItemDisplayTypeEnum
	{
		PlayerInventory,
		ShopItem,
		CustomItemShop,
		NoModificationsAllowed
	}

	[SerializeField]
	private TextMeshProUGUI itemName;

	[SerializeField]
	private TextMeshProUGUI itemDescription;

	[SerializeField]
	private Transform itemColor;

	[SerializeField]
	private TextMeshProUGUI itemInfo;

	[SerializeField]
	private TextMeshProUGUILinkHandler linkHandler;

	[SerializeField]
	private Image favouriteImage;

	public ItemDisplayTypeEnum ItemDisplayType;

	public int priceMulti = 1;

	[SerializeField]
	private Button cheatsButton;

	private Item _currentItem;

	public const string SylphyLinkId = "SyplyLink";

	public const string CosplayHouseId = "CosplayHouse";

	public const string HotPinkId = "HotPink";

	public const string InHeatId = "InHeat";

	public const string WyldSpaceId = "WyldSpaceId";

	public const string DestinationDeliriumId = "DestinationDelirium";

	public const string SilumanId = "Siluman";

	public const string MirthalId = "Mirthal";

	public const string AGUIId = "AGUI";

	public const string SpecialHaremClassId = "SHC";

	public const string BedabotsId = "Bedabots";

	public const string ProjektMelodyId = "ProjektMelody";

	public const string WorldTamerId = "WorldTamer";

	public const string XoulionId = "Xoulion";

	public const string CelestynId = "Celestyn";

	public const string FlowerKidVId = "FlowerKidV";

	public const string NutekuId = "NutekuId";

	public const string HikkeiruId = "Hikkeiru";

	public const string ToniSMajesticId = "ToniSM";

	public const string MadId = "Mad";

	public const string SJ68Id = "SJ";

	public const string LightSonicId = "LS";

	public const string RottingSteelId = "RS";

	public const string NitroriId = "NTR";

	public const string ShadyCornerId = "SC";

	public const string RelatedItemsId = "RI";

	private static readonly Dictionary<string, string> linksDictionary = new Dictionary<string, string>
	{
		["SyplyLink"] = "https://store.steampowered.com/app/1728550/Phantom_Thief_Sylphy/",
		["CosplayHouse"] = "https://pixeldottod.itch.io/cosplay-house",
		["HotPink"] = "https://hotpink.itch.io/",
		["InHeat"] = "https://monsterboxgames.itch.io/in-heat",
		["DestinationDelirium"] = "https://cyberscherzoff.itch.io/destination-delirium",
		["Siluman"] = "https://store.steampowered.com/app/997240/Siluman_Fantasy__First_Half/",
		["WyldSpaceId"] = "https://wyldspace.itch.io/wyldspace",
		["Mirthal"] = "https://mirthal.itch.io/aurelia",
		["AGUI"] = "https://aguisextoy.com/",
		["SHC"] = "https://kagetodev.itch.io/special-harem-class",
		["Bedabots"] = "https://twitter.com/bedabots",
		["ProjektMelody"] = "https://www.patreon.com/bigbangstudio",
		["WorldTamer"] = "https://deniam.itch.io/worldtamer",
		["Xoulion"] = "https://store.steampowered.com/app/2736290/My_Life_In_A_Monster_Girl_Paradise/",
		["Celestyn"] = "https://celestyn.ictasis.group/",
		["FlowerKidV"] = "https://linktr.ee/flowerkidv",
		["NutekuId"] = "https://nuteku.itch.io/monster-tutor",
		["Hikkeiru"] = "https://hikkeiru.itch.io/gosa",
		["ToniSM"] = "https://open.spotify.com/artist/56KlxxdDBaltZJf4nBsY4E",
		["Mad"] = "https://www.youtube.com/@mad0517",
		["SJ"] = "https://open.spotify.com/artist/5ESG0Zk4AaBkaQCcR19JRW",
		["LS"] = "https://www.youtube.com/@lightsonicwaves",
		["RS"] = "https://www.youtube.com/@rottingsteelcreations",
		["NTR"] = "https://www.youtube.com/@NitroriN2",
		["SC"] = "https://shadycorner.com/"
	};

	public Action<Item> CurrentItemInternallyChanged;

	private List<Item> relatedOwnedItems;

	public bool CanFavouriteItems => ItemDisplayType == ItemDisplayTypeEnum.PlayerInventory;

	public bool CanChangeHyperCamoColor
	{
		get
		{
			if (ItemDisplayType != ItemDisplayTypeEnum.PlayerInventory)
			{
				return ItemDisplayType == ItemDisplayTypeEnum.CustomItemShop;
			}
			return true;
		}
	}

	public bool CanChangeColorsOfAllItems => ItemDisplayType == ItemDisplayTypeEnum.CustomItemShop;

	public bool ShowInfoAboutOwned
	{
		get
		{
			if (ItemDisplayType != ItemDisplayTypeEnum.ShopItem)
			{
				return ItemDisplayType == ItemDisplayTypeEnum.ShopItem;
			}
			return true;
		}
	}

	public Item CurrentItem => _currentItem;

	public GameObject SourcePrefab { get; set; }

	public void ReportCurrentItemInternalChange(Item item)
	{
		SetItem(item);
		CurrentItemInternallyChanged?.Invoke(item);
	}

	public void ItemColorPopup(Item item)
	{
		ColorSlot[] colorSlots = item.GetItemPrefab().ColorSlots;
		if (colorSlots == null)
		{
			return;
		}
		if (colorSlots.Length > 1)
		{
			UiOverlay.Instance.OpenContextMenuPopup(colorSlots.Select((ColorSlot t, int i) => new PopupChoice
			{
				Text = t.Name,
				Action = () =>
				{
					ChooseColor(i);
				}
			}).ToList());
		}
		else if (colorSlots.Length == 1)
		{
			ChooseColor(0);
		}
		void ChooseColor(int index)
		{
			UiOverlay.Instance.ColorInputPopup(LOC.UI.ColorPicker, LOC.UI.ColorPickerItemPrompt(item.GetName(ItemNameIncludeEnum.DefaultNoColorNoFavourite)), (Color c) =>
			{
				item.SetColor(index, c);
				ReportCurrentItemInternalChange(item);
			}, null, item.GetColor(index));
		}
	}

	private void Awake()
	{
		cheatsButton.onClick.SetListener(() =>
		{
			ItemStatsSheep.ItemStatsCheatsButtonClicked(this);
		});
	}

	public void SetPrefab(ItemPrefab prefab)
	{
		itemName.rectTransform.SetRight(0f);
		cheatsButton.gameObject.SetActive(value: false);
		favouriteImage.gameObject.SetActive(value: false);
		itemColor.gameObject.SetActive(value: false);
		linkHandler.OnLinkClicked = null;
		if (prefab != null)
		{
			itemName.SetText(prefab.Name);
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.AppendLine(prefab.Description);
				if (prefab.IsEquipable(out var slotEquipData))
				{
					stringBuilder.AppendLineStringDisplay(LOC.UI.TargetSlot, slotEquipData.TargetSlot.FriendlyName);
				}
				itemDescription.SetText(stringBuilder);
				stringBuilder.Clear();
				StringBuilder stringBuilder2 = stringBuilder;
				stringBuilder2.AppendLine(LOC.UI.UI_Price(prefab.Price));
				if (prefab.HasQuality)
				{
					stringBuilder2.AppendLine(LOC.UI.PriceVariesByQuality);
				}
				itemInfo.SetText(stringBuilder2);
				return;
			}
		}
		DisableCommon();
	}

	public void SetItem(Item item)
	{
		_currentItem = item;
		GameVariables current = GameVariables.Current;
		bool flag = GameVariables.Current != null;
		if (((OptionsStatic.GetCheatTier() >= CheatTierEnum.AdvancedCheats) & flag) && (ItemDisplayType == ItemDisplayTypeEnum.PlayerInventory || OptionsStatic.DebugEnabled))
		{
			cheatsButton.gameObject.SetActive(value: true);
		}
		else
		{
			cheatsButton.gameObject.SetActive(value: false);
		}
		itemName.rectTransform.SetRight(CanFavouriteItems ? favouriteImage.rectTransform.sizeDelta.x : 0f);
		if (item != null)
		{
			item.EnforceAllVariableRequirements();
			ItemPrefab itemPrefab = item.GetItemPrefab();
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				item.GetNameStringBuilder(stringBuilder, ItemNameIncludeEnum.DefaultNoFavourite);
				if (item.GameId.IsModded)
				{
					Mod modByGuid = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModByGuid(item.GameId.Guid);
					if (modByGuid != null)
					{
						stringBuilder.AppendLine();
						stringBuilder.Append("<size=40%>");
						stringBuilder.AppendLine(LOC.UI.Equipment_FromMod(modByGuid.Name));
					}
				}
				itemName.SetText(stringBuilder);
				if (CanFavouriteItems)
				{
					favouriteImage.gameObject.SetActive(value: true);
					favouriteImage.color = (item.IsFavourite ? Color.red : Color.white);
					favouriteImage.GetComponent<BetterButton>().onClick.SetListener(() =>
					{
						item.IsFavourite = !item.IsFavourite;
						SetItem(item);
						CurrentItemInternallyChanged?.Invoke(item);
					});
				}
				else
				{
					favouriteImage.gameObject.SetActive(value: false);
				}
				stringBuilder.Clear();
				item.GetDescriptionStringBuilder(stringBuilder);
				if (itemPrefab.IsEquipable(out var slotEquipData))
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendStringDisplay(LOC.UI.TargetSlot, slotEquipData.TargetSlot.FriendlyName);
				}
				if (itemPrefab.IsPersonalityModule)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendColorTagStart(MyColor32Presets.Green);
					stringBuilder.AppendLine(LOC.UI.Equipment_PersonalityModuleInfo);
					stringBuilder.AppendColorTagEnd();
				}
				if (item.HasAnyItemModifiers)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					stringBuilder.Append(LOC.UI.ItemModifierListHeader);
					stringBuilder.AppendLine();
					int num = 0;
					foreach (ItemModifier itemModifier in item.ItemModifiers)
					{
						stringBuilder.TMPAppendLinkTag(num.ToString(CultureInfo.InvariantCulture));
						stringBuilder.TMPAppendColorGradient(itemModifier.Name, itemModifier.Colors);
						stringBuilder.AppendFormat("({0:F1})", itemModifier.Level);
						stringBuilder.TMPAppendCloseLinkTag();
						stringBuilder.AppendLine();
						num++;
					}
				}
				if (itemPrefab.HasAnyModifiableVariables)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					stringBuilder.TMPAppendFinePrintSizeTag();
					stringBuilder.TMPAppendBoldTag();
					stringBuilder.AppendLine(LOC.UI.ModifableVariables_Header);
					stringBuilder.TMPAppendCloseBoldTag();
					string modifiableVariables_CanBeChanged = LOC.UI.ModifiableVariables_CanBeChanged;
					ItemModifiableVariable[] itemModifiableVariables = itemPrefab.ItemModifiableVariables;
					for (int num2 = 0; num2 < itemModifiableVariables.Length; num2++)
					{
						itemModifiableVariables[num2].GetLocalizedDescription(item, stringBuilder);
						stringBuilder.Append(" (");
						stringBuilder.Append(modifiableVariables_CanBeChanged);
						stringBuilder.Append(")\n");
					}
					stringBuilder.TMPAppendCloseSizeTag();
				}
				linkHandler.OnLinkClicked = (string s) =>
				{
					string value;
					if (s == "RI")
					{
						ShowRelatedOwnedItems();
					}
					else if (linksDictionary.TryGetValue(s, out value))
					{
						UiOverlay.Instance.OpenExternalURLPopup(value, StringUtils.PrepareUtmLink(value, $"{item.GameId.Id}_item"));
					}
					else
					{
						StringBuilder stringBuilder3;
						if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) && item.ItemModifiers.TryGetValue(result, out var value2))
						{
							using (StringBuilderCache.AcquireWithUsing(out stringBuilder3))
							{
								stringBuilder3.AppendLine(LOC.UI.ItemModifierInfoDescription(value2.Name));
								stringBuilder3.EnsureLastCharWhiteSpaceOrBeginning();
								stringBuilder3.AppendLine(value2.Description);
								stringBuilder3.AppendLine();
								stringBuilder3.Append(LOC.UI.ItemModifierLevel);
								stringBuilder3.EnsureLastCharWhiteSpaceOrBeginning();
								stringBuilder3.AppendFormat("{0:F1}", value2.Level);
								UiOverlay.Instance.OkPopup(LOC.UI.ItemModifierInfo, stringBuilder3.ToString());
								return;
							}
						}
						Debug.LogError("Link with id \"" + s + "\" not found!");
					}
				};
				if (flag)
				{
					Item.ItemManager itemManager = current.itemManager;
					List<EquippedItemRequirement> list;
					using (MyCollectionPool<EquippedItemRequirement>.GetList(out list))
					{
						current.cocktractManager.GetAllRequirements(ref list);
						if (list.Any((EquippedItemRequirement x) => x.Fulfills(item)))
						{
							stringBuilder.AppendLine();
							stringBuilder.AppendLine();
							stringBuilder.AppendColorTagStart(Color.green);
							stringBuilder.AppendLine(LOC.UI.Equipment_ItemIsInCurrentContract);
							stringBuilder.AppendColorTagEnd();
						}
						if (ShowInfoAboutOwned)
						{
							if (relatedOwnedItems == null)
							{
								relatedOwnedItems = MyCollectionPool<Item>.GetList();
							}
							else
							{
								relatedOwnedItems.Clear();
							}
							stringBuilder.AppendLine();
							stringBuilder.TMPAppendHalfSizeTag();
							stringBuilder.TMPAppendLinkTag("RI");
							bool flag2 = false;
							if (current.itemManager.TryGetItemsByGameId(item.GameId, out var gameIdItems))
							{
								relatedOwnedItems.AddRange(gameIdItems);
								int num3 = gameIdItems.Sum((Item x) => x.GetCount());
								stringBuilder.AppendColorTagStart(Color.green);
								stringBuilder.Append(LOC.UI.Equipment_Own(num3));
								if (item.HasColor && !gameIdItems.Any((Item x) => x.Equals_IdAndColors(item)))
								{
									stringBuilder.Append(" ");
									stringBuilder.AppendColorTagStart(Color.yellow);
									stringBuilder.Append(LOC.UI.Equipment_DontOwnColor);
									stringBuilder.AppendColorTagEnd();
								}
								stringBuilder.AppendColorTagEnd();
								flag2 = true;
							}
							else
							{
								stringBuilder.AppendColorTagStart(Color.yellow);
								stringBuilder.Append(LOC.UI.Equipment_DontOwn);
								stringBuilder.AppendColorTagEnd();
							}
							if (itemPrefab.OnUnpack != null && itemPrefab.OnUnpackGameIdHint.Length != 0)
							{
								int count = relatedOwnedItems.Count;
								GameId[] onUnpackGameIdHint = itemPrefab.OnUnpackGameIdHint;
								foreach (GameId gameId in onUnpackGameIdHint)
								{
									if (itemManager.AppendItemsByGameId(gameId, relatedOwnedItems))
									{
										flag2 = true;
									}
								}
								stringBuilder.AppendLine();
								int num4 = relatedOwnedItems.Count - count;
								if (num4 > 0)
								{
									stringBuilder.AppendColorTagStart(Color.green);
									stringBuilder.Append(LOC.UI.Equipment_OwnPackage(num4));
									stringBuilder.AppendColorTagEnd();
								}
								else
								{
									stringBuilder.AppendColorTagStart(Color.yellow);
									stringBuilder.Append(LOC.UI.Equipment_DontOwnPackage);
									stringBuilder.AppendColorTagEnd();
								}
							}
							if (itemPrefab.IsEquipable(out slotEquipData))
							{
								stringBuilder.AppendLine();
								int count2 = relatedOwnedItems.Count;
								relatedOwnedItems.AddRange(from x in current.itemManager.AllItemsEnumerable()
									where x.GetItemPrefab().IsEquipable(out var slotEquipData2) && slotEquipData2.TargetSlot == slotEquipData.TargetSlot
									select x);
								int num5 = relatedOwnedItems.Count - count2;
								if (num5 > 0)
								{
									flag2 = true;
									stringBuilder.AppendColorTagStart(Color.green);
									stringBuilder.Append(LOC.UI.Equipment_OwnSlot(num5, slotEquipData.TargetSlot.FriendlyName));
									stringBuilder.AppendColorTagEnd();
								}
								else
								{
									stringBuilder.AppendColorTagStart(Color.yellow);
									stringBuilder.Append(LOC.UI.Equipment_DontOwnSlot);
									stringBuilder.AppendColorTagEnd();
								}
							}
							if (flag2)
							{
								stringBuilder.AppendLine();
								stringBuilder.AppendColorTagStart(Color.green);
								stringBuilder.Append("(");
								stringBuilder.Append(LOC.UI.Equipment_ViewRelated);
								stringBuilder.Append(")");
								stringBuilder.AppendColorTagEnd();
							}
							stringBuilder.TMPAppendCloseLinkTag();
							stringBuilder.TMPAppendCloseSizeTag();
						}
						else
						{
							ClearRelatedOwnedItems();
						}
					}
				}
				itemDescription.SetText(stringBuilder);
				stringBuilder.Clear();
				StringBuilder stringBuilder2 = stringBuilder;
				stringBuilder2.AppendLine(LOC.UI.UI_Price(item.GetPrice() * priceMulti));
				if (itemPrefab.HasQuality)
				{
					stringBuilder2.AppendLine(LOC.UI.UI_Condition(item.GetQualityString()));
				}
				itemInfo.SetText(stringBuilder2);
				if (item.HasColor)
				{
					itemColor.gameObject.SetActive(value: true);
					TextMeshProUGUI componentInChildren = itemColor.GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
					ColorableUIImage componentInChildren2 = itemColor.GetComponentInChildren<ColorableUIImage>(includeInactive: true);
					Color[] colors = item.GetColors();
					componentInChildren2.color = Color.white;
					List<Color> colors2 = componentInChildren2.Colors;
					colors2.Clear();
					colors2.AddRange(colors);
					componentInChildren2.Colors = colors2;
					Button componentInChildren3 = itemColor.GetComponentInChildren<Button>(includeInactive: true);
					componentInChildren3.onClick.RemoveAllListeners();
					if ((CanChangeHyperCamoColor && item.CanChangeColor) || CanChangeColorsOfAllItems)
					{
						componentInChildren.gameObject.SetActive(value: true);
						componentInChildren3.interactable = true;
						componentInChildren3.onClick.AddListener(() =>
						{
							ItemColorPopup(item);
						});
					}
					else
					{
						componentInChildren.gameObject.SetActive(value: false);
						componentInChildren3.interactable = false;
					}
				}
				else
				{
					itemColor.gameObject.SetActive(value: false);
				}
				return;
			}
		}
		DisableCommon();
	}

	private void ClearRelatedOwnedItems()
	{
		if (relatedOwnedItems != null)
		{
			MyCollectionPool<Item>.Release(relatedOwnedItems);
			relatedOwnedItems = null;
		}
	}

	private void ShowRelatedOwnedItems()
	{
		if (relatedOwnedItems != null && relatedOwnedItems.Count > 0 && _currentItem != null)
		{
			Item cur = _currentItem;
			CompoundItemList.StartBuilder<Item, GameItemContainer>().WithDefaultGameItemName().WithDefaultGameItemSorting()
				.WithFolders(new List<Folder<Item>>
				{
					new Folder<Item>(() => LOC.UI.Equipment_All),
					new Folder<Item>(() => LOC.UI.Equipment_ExactItemMatch, (Item item) => item.GameId == cur.GameId, hideIfEmpty: true),
					new Folder<Item>(() => LOC.UI.Equipment_RelatedItems, null, hideIfEmpty: true, remainingFolder: true)
				})
				.WithDefaultGameItemSetItem()
				.WithDefaultGameItemListItemPool()
				.Build(relatedOwnedItems.Distinct().ToFrozenDataSource())
				.ToChooseItemsComplexPopupBuilderItem()
				.WithNoChoosingSelection()
				.WithOnFinished((UiOverlay.PopupFinishTypesEnum status, Selection<Item> selection) =>
				{
				})
				.OpenPopup(LOC.UI.Equipment_RelatedOwnedItems, LOC.UI.Equipment_RelatedOwnedItemsInfoText(cur.GetName()));
		}
	}

	private void DisableCommon()
	{
		favouriteImage.gameObject.SetActive(value: false);
		itemName.text = LOC.UI.Equipment_NoItemSelected;
		itemDescription.text = "";
		itemInfo.text = "";
		itemColor.gameObject.SetActive(value: false);
	}

	public void AfterReturnedToPool()
	{
		SetItem(null);
		ClearRelatedOwnedItems();
	}

	public void BeforeGetFromPool()
	{
		ItemDisplayType = ItemDisplayTypeEnum.NoModificationsAllowed;
	}
}
