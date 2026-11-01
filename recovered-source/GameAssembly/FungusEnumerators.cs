using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using Fungus;
using GameItems;
using StoryConditions;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class FungusEnumerators : SingletonMonoBehaviour<FungusEnumerators>
{
	[Serializable]
	public class PlayerNamingStrings : LocalizedStringReferenceContainer
	{
		public SerializedLocalizedConversation sheep;

		public SerializedLocalizedConversation racoon;

		public SerializedLocalizedConversation meanName;

		[Header("Player name")]
		public SerializedLocalizedConversation changingPlayerNameStart;

		public SerializedLocalizedConversation changingPlayerNameMeanName;

		public SerializedLocalizedConversation changingPlayerNameShanice;

		public SerializedLocalizedConversation changingPlayerNameBrother;

		public SerializedLocalizedConversation changingPlayerNameGuts;

		public SerializedLocalizedConversation changingPlayerNameGeneric1;

		public SerializedLocalizedConversation changingPlayerNameGeneric2;

		public SerializedLocalizedConversation changingPlayerNameGeneric3;

		[Header("Bot name")]
		public SerializedLocalizedConversation changingBotNameStart;

		public SerializedLocalizedConversation changingBotNameMeanName;

		public SerializedLocalizedConversation changingBotNameShanice;

		public SerializedLocalizedConversation changingBotNameSister;

		public SerializedLocalizedConversation changingBotNameNeko;

		public SerializedLocalizedConversation changingBotNameNekoArc;

		public SerializedLocalizedConversation changingBotNameAsuka;

		public SerializedLocalizedConversation changingBotNameRinTohsaka;

		public SerializedLocalizedConversation changingBotNameSaber;

		public SerializedLocalizedConversation changingBotNameMegumin;

		public SerializedLocalizedConversation changingBotNameCC;

		public SerializedLocalizedConversation changingBotNameGeneric1;

		public SerializedLocalizedConversation changingBotNameGeneric2;

		public SerializedLocalizedConversation changingBotNameGeneric3;

		public override string ContainerName => "PlayerAndBotNaming";
	}

	[Serializable]
	public class ClothierStrings : LocalizedStringReferenceContainer
	{
		public LocalizedStringReferenceStory ShopMenuText;

		public LocalizedStringReferenceStory CustomOrdersMenuText;

		public LocalizedStringReferenceStory ChangingRoomMenuText;

		public LocalizedStringReferenceStory LeaveMenuText;

		public override string ContainerName => "ClothierStrings";
	}

	public enum FishingJointDuelId
	{
		None,
		Hobo,
		PoliceMan,
		PriestBot,
		BigDaddyHurt,
		Landlord,
		Bot
	}

	[NonSerialized]
	private EquipmentSet _savedEquipmentSet;

	public static readonly GameId[] UnhealthyFoodGameIds = new GameId[6]
	{
		ItemEnum.Chips.ToGameId(),
		ItemEnum.InstantNoodles.ToGameId(),
		ItemEnum.EnergyDrink.ToGameId(),
		ItemEnum.Soyrent.ToGameId(),
		ItemEnum.Soda.ToGameId(),
		ItemEnum.Sweets.ToGameId()
	};

	public const string IntegerVariableName = "Integer";

	private readonly string[] meanNames = new string[15]
	{
		"bitch", "whore", "prostitute", "trash", "waste", "pig", "femoid", "slut", "cumdump", "shitface",
		"cocksucker", "motherfucker", "asshole", "dick head", "bastard"
	};

	private readonly string[] brotherNames = new string[5] { "brother", "onii-chan", "oniichan", "onii", "bro" };

	private readonly string[] sisterNames = new string[4] { "sis", "sister", "nee-chan", "nee" };

	private bool FungusBoolean
	{
		get
		{
			return SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.GetBooleanVariable("Boolean");
		}
		set
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetBooleanVariable("Boolean", value);
		}
	}

	private PlayerNamingStrings playerAndBotNaming => SingletonScriptableObject<CFLocalizationDataSO>.Instance.PlayerNamingStrings;

	private ClothierStrings clothierStrings => SingletonScriptableObject<CFLocalizationDataSO>.Instance.ClothierStrings;

	public void IncreaseSusFromShow()
	{
		GameVariables current = GameVariables.Current;
		current.search = Mathf.Min(current.search, 0.99f);
	}

	[Preserve]
	public void RemoveAllBotClothes(bool leavePanties = false)
	{
		Item.ItemManager itemManager = GameVariables.Current.itemManager;
		using (itemManager.DelayCallbacks("Undress"))
		{
			_savedEquipmentSet = EquipmentSet.GenerateFromCurrent("Before bot undressed");
			Item firstItemAtSlot = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.Panties.Id);
			itemManager.UnequipAllBotClothes();
			if (firstItemAtSlot != null)
			{
				itemManager.Equip(firstItemAtSlot, forceRemoveForbidden: true);
			}
		}
	}

	[Preserve]
	public void RestoreLastClothes()
	{
		if (_savedEquipmentSet == null)
		{
			Debug.LogError("Equipment set not saved!");
			return;
		}
		_savedEquipmentSet.Equip(resetFirst: true);
		_savedEquipmentSet = null;
	}

	[Preserve]
	public void RemoveAllUnhealthyFood()
	{
		List<Item> list;
		using (MyCollectionPool<Item>.GetList(out list))
		{
			Item.ItemManager itemManager = GameVariables.Current.itemManager;
			GameId[] unhealthyFoodGameIds = UnhealthyFoodGameIds;
			foreach (GameId gameId in unhealthyFoodGameIds)
			{
				if (!itemManager.TryGetItemsByGameId(gameId, out var gameIdItems))
				{
					continue;
				}
				list.AddRange(gameIdItems);
				foreach (Item item in list)
				{
					itemManager.RemoveItem(item);
				}
			}
		}
	}

	[Preserve]
	public IEnumerator GetPrologueLimbs()
	{
		List<Item> list;
		using (MyCollectionPool<Item>.GetList(out list))
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				float value = UnityEngine.Random.value;
				if (value < 0.33f)
				{
					list.Add(new Item(ItemEnum.BasicLegL, UnityEngine.Random.Range(0.4f, 0.8f)));
					list.Add(new Item(ItemEnum.BasicLegR, UnityEngine.Random.Range(0.4f, 0.8f)));
				}
				else if (value < 0.5f)
				{
					list.Add(new Item(ItemEnum.BasicLegL, UnityEngine.Random.Range(0.4f, 0.8f)));
					list.Add(new Item(ItemEnum.BasicArmR, UnityEngine.Random.Range(0.4f, 0.8f)));
				}
				else if (value < 0.75f)
				{
					list.Add(new Item(ItemEnum.BasicArmL, UnityEngine.Random.Range(0.4f, 0.8f)));
					list.Add(new Item(ItemEnum.BasicArmR, UnityEngine.Random.Range(0.4f, 0.8f)));
				}
				else
				{
					list.Add(new Item(ItemEnum.BasicLegR, UnityEngine.Random.Range(0.4f, 0.8f)));
					list.Add(new Item(ItemEnum.BasicArmR, UnityEngine.Random.Range(0.4f, 0.8f)));
				}
				foreach (Item item in list)
				{
					GameVariables.Current.itemManager.AddItem(item);
					stringBuilder.AppendLine(LOC.UI.itemAddedToYourInventory(item.GetName()));
				}
				yield return DoConversation(stringBuilder.ToString());
			}
		}
	}

	[Preserve]
	public IEnumerator GetRandomIllegalItem()
	{
		Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
		Item item = Item.CreateRandomItem(ItemPrefab.ItemPrefabManager.AllNonDefaultItems.Where(((GameId id, ItemPrefab prefab) t) => t.prefab.Equipable && t.prefab.IsIllegal && t.prefab.HasQuality).ToList().GetRandom()
			.id);
			itemManager.AddItem(item);
			SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusSetCustomText(item.GetName());
			yield break;
		}

		[Preserve]
		public void PeeOutCum()
		{
			GameVariables current = GameVariables.Current;
			float maxCum = (current.RemainingCum = 375f);
			current.MaxCum = maxCum;
		}

		[Preserve]
		public IEnumerator CheckIfCanGoOutsideWithBot()
		{
			return CheckIfCanGoOutsideWithBotCustom(showInfoWhenCan: true);
		}

		[Preserve]
		public IEnumerator CheckIfCanGoOutsideWithBotCustom(bool showInfoWhenCan)
		{
			EquipmentGameStats equipmentGameStats = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.GetGameStats().EquipmentGameStats;
			StringBuilderCache.AcquireManual(out var stringBuilder);
			if (!equipmentGameStats.CanGoOutside.IsSuccess)
			{
				stringBuilder.AppendLine(LOC.Other.Outside_CantGo);
				stringBuilder.AppendLine(equipmentGameStats.CanGoOutside.GetDescription());
				FungusBoolean = false;
			}
			else
			{
				if (showInfoWhenCan)
				{
					string text = "\n" + LOC.Other.Outside_Info;
					if (equipmentGameStats.SusRating > 0.5f)
					{
						stringBuilder.AppendLine(LOC.Other.Outside_VeryDangerous + text);
					}
					else if (equipmentGameStats.SusRating > 0.35f)
					{
						stringBuilder.AppendLine(LOC.Other.Outside_Dangerous + text);
					}
					else if (equipmentGameStats.SusRating > 0.25f)
					{
						stringBuilder.AppendLine(LOC.Other.Outside_Medium + text);
					}
					else if (equipmentGameStats.SusRating > 0.15f)
					{
						stringBuilder.AppendLine(LOC.Other.Outside_AlmostSafe);
					}
					else
					{
						stringBuilder.AppendLine(LOC.Other.Outside_Safe);
					}
					if (!equipmentGameStats.IsCovered(SusArea.Pussy))
					{
						stringBuilder.AppendLine(LOC.Other.Outside_NoPanties);
					}
				}
				FungusBoolean = true;
			}
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.RiotsActive)
			{
				stringBuilder.Clear();
				stringBuilder.AppendLine(LOC.Other.Outside_RiotsDanger);
				FungusBoolean = false;
			}
			if (StoryCondition.CommonConditions.JoinUsDAKDanger.ConditionMet())
			{
				stringBuilder.Clear();
				stringBuilder.AppendLine(LOC.Other.Outside_DAKDanger);
				FungusBoolean = false;
			}
			yield return DoConversation(StringBuilderCache.ReleaseWithToString(stringBuilder));
		}

		[UsedInLua]
		public IEnumerator DispenserGet()
		{
			List<Item> l = new List<Item>
			{
				new Item(ItemEnum.BreadFromDispenser),
				new Item(ItemEnum.Chips),
				new Item(ItemEnum.Soda),
				new Item(ItemEnum.Sweets),
				new Item(ItemEnum.Soyrent)
			};
			int integerVariable = SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.GetIntegerVariable("Integer");
			if (l.TryGetValue(integerVariable, out var value))
			{
				value.SetCount(UnityEngine.Random.Range(1, 5));
				GameVariables.Current.itemManager.AddItem(value, Item.ItemLocationEnum.Carried);
				yield return DoConversation(LOC.Other.GotItem(value.GetName()));
			}
		}

		[UsedInLua]
		public IEnumerator EquipLimbsIfPossible()
		{
			Item.ItemManager im = GameVariables.Current.itemManager;
			using (im.DelayCallbacks("EquipLimbsIfPossible"))
			{
				EquipBestItemAt(ItemSlot.SlotManager.LegL);
				EquipBestItemAt(ItemSlot.SlotManager.LegR);
				EquipBestItemAt(ItemSlot.SlotManager.ArmL);
				EquipBestItemAt(ItemSlot.SlotManager.ArmR);
			}
			void EquipBestItemAt(ItemSlot itemSlot)
			{
				if (!im.HasItemAtSlot(itemSlot) && im.TryFindBestItemForSlot(itemSlot, out var bestItem))
				{
					im.Equip(bestItem, itemSlot, forceRemoveForbidden: true);
				}
			}
			yield break;
		}

		[UsedInLua]
		public IEnumerator GetLandlordsStove()
		{
			GameVariables.Current.itemManager.AddItem(Item.CreateRandomItem(ItemEnum.LandlordsOldStove.ToGameId()));
			yield break;
		}

		[UsedInLua]
		public IEnumerator UpgradeLandlordsStove()
		{
			Item.ItemManager itemManager = GameVariables.Current.itemManager;
			if (itemManager.TryGetItemsByGameId(ItemEnum.LandlordsOldStove.ToGameId(), out var gameIdItems))
			{
				itemManager.RemoveItem(gameIdItems.First());
				itemManager.AddItem(Item.CreateRandomItem(ItemEnum.FixedLandlordsOldStove.ToGameId()));
			}
			yield break;
		}

		public void HideGameUI()
		{
			UiOverlay.Instance.GetStaticGUI<GameStaticGui>(out var staticGui);
			staticGui.gameObject.SetActive(value: false);
		}

		public void RemoveAndDestroyOneArm()
		{
			Item.ItemManager itemManager = GameVariables.Current.itemManager;
			Item firstItemAtSlot = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmL.Id);
			Item firstItemAtSlot2 = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.ArmR.Id);
			if (firstItemAtSlot != null)
			{
				itemManager.RemoveItem(firstItemAtSlot);
			}
			else if (firstItemAtSlot2 != null)
			{
				itemManager.RemoveItem(firstItemAtSlot2);
			}
			else
			{
				Debug.LogError("Can't remove an arm! Bot has no arms!");
			}
		}

		[Preserve]
		public IEnumerator StartPlayerNaming()
		{
			CoroutineUtilities.WaitForResult<string> waitForResult = CoroutineUtilities.PrepareWaitForResultBetter<string>();
			UiOverlay.Instance.InputPopupWithValidation(LOC.UI.PlayerName, LOC.UI.PlayerName_Prompt, LOC.UI.Naming_AreYouSure, waitForResult.SetResult, LOC.UI.PlayerName_DefaultMaster, TMP_InputField.ContentType.Standard, UiOverlay.NameLengthValidation, UiOverlay.NameTrimProcessing);
			yield return waitForResult;
			string newPlayerName = waitForResult.Result;
			try
			{
				if (newPlayerName.Equals("sheep", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.sheep.GetConversationEnumerator();
					newPlayerName = "Not sheep";
					yield break;
				}
				if (newPlayerName.Equals("racoon", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.racoon.GetConversationEnumerator();
					newPlayerName = "Not racoon";
					yield break;
				}
				SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetStringVariable("Text", newPlayerName);
				if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotSmart)
				{
					yield break;
				}
				yield return playerAndBotNaming.changingPlayerNameStart.GetConversationEnumerator();
				if (newPlayerName.Contains("shanice", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingPlayerNameShanice.GetConversationEnumerator();
					yield break;
				}
				if (brotherNames.Any((string x) => x.Equals(newPlayerName, StringComparison.InvariantCultureIgnoreCase)) || string.Equals(newPlayerName, LOC.Other.Word_Brother, StringComparison.CurrentCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingPlayerNameBrother.GetConversationEnumerator();
					yield break;
				}
				if (newPlayerName.Equals("guts", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingPlayerNameGuts.GetConversationEnumerator();
					yield break;
				}
				if (meanNames.Any((string x) => x.Equals(newPlayerName, StringComparison.InvariantCultureIgnoreCase)))
				{
					yield return playerAndBotNaming.changingPlayerNameMeanName.GetConversationEnumerator();
					yield break;
				}
				float value = UnityEngine.Random.value;
				if (value < 0.3f)
				{
					yield return playerAndBotNaming.changingPlayerNameGeneric1.GetConversationEnumerator();
				}
				else if ((double)value < 0.6)
				{
					yield return playerAndBotNaming.changingPlayerNameGeneric2.GetConversationEnumerator();
				}
				else
				{
					yield return playerAndBotNaming.changingPlayerNameGeneric3.GetConversationEnumerator();
				}
			}
			finally
			{
				GameVariables.Current.SetPlayerName(newPlayerName);
			}
		}

		[Preserve]
		public IEnumerator StartBotNaming()
		{
			CoroutineUtilities.WaitForResult<string> waitForResult = CoroutineUtilities.PrepareWaitForResultBetter<string>();
			UiOverlay.Instance.InputPopupWithValidation(LOC.UI.BotName, LOC.UI.BotName_Prompt, LOC.UI.Naming_AreYouSure, waitForResult.SetResult, LOC.UI.BotName_Default, TMP_InputField.ContentType.Standard, UiOverlay.NameLengthValidation, UiOverlay.NameTrimProcessing);
			yield return waitForResult;
			string newBotName = waitForResult.Result;
			try
			{
				if (newBotName.Equals("sheep", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.sheep.GetConversationEnumerator();
					newBotName = "Not sheep";
					yield break;
				}
				if (newBotName.Equals("racoon", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.racoon.GetConversationEnumerator();
					newBotName = "Not racoon";
					yield break;
				}
				SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.SetStringVariable("Text", newBotName);
				if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.IsBotSmart)
				{
					if (meanNames.Any((string x) => x.Equals(newBotName, StringComparison.InvariantCultureIgnoreCase)))
					{
						yield return playerAndBotNaming.meanName.GetConversationEnumerator();
						if (GameVariables.Current.sympathy < 200)
						{
							GameVariables.Current.sympathy -= 200;
						}
					}
					yield break;
				}
				yield return playerAndBotNaming.changingBotNameStart.GetConversationEnumerator();
				if (newBotName.Contains("shanice", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameShanice.GetConversationEnumerator();
					yield break;
				}
				if (newBotName.Contains("neko", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameNeko.GetConversationEnumerator();
					yield break;
				}
				if (newBotName.Contains("neco-arc", StringComparison.InvariantCultureIgnoreCase) || newBotName.Contains("necoarc", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameNekoArc.GetConversationEnumerator();
					yield break;
				}
				if (newBotName.Contains("asuka", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameAsuka.GetConversationEnumerator();
					yield break;
				}
				if (newBotName.Contains("rin tohsaka", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameRinTohsaka.GetConversationEnumerator();
					yield break;
				}
				if (newBotName.Contains("saber", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameSaber.GetConversationEnumerator();
					yield break;
				}
				if (newBotName.Contains("megumin", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameMegumin.GetConversationEnumerator();
					yield break;
				}
				if (newBotName.Contains("CC", StringComparison.InvariantCultureIgnoreCase) || newBotName.Contains("C.C.", StringComparison.InvariantCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameCC.GetConversationEnumerator();
					yield break;
				}
				if (sisterNames.Any((string x) => x.Equals(newBotName, StringComparison.InvariantCultureIgnoreCase)) || string.Equals(newBotName, LOC.Other.Word_Sister, StringComparison.CurrentCultureIgnoreCase))
				{
					yield return playerAndBotNaming.changingBotNameSister.GetConversationEnumerator();
					yield break;
				}
				if (meanNames.Any((string x) => x.Equals(newBotName, StringComparison.InvariantCultureIgnoreCase)))
				{
					yield return playerAndBotNaming.changingBotNameMeanName.GetConversationEnumerator();
					yield break;
				}
				float value = UnityEngine.Random.value;
				if (value < 0.3f)
				{
					yield return playerAndBotNaming.changingBotNameGeneric1.GetConversationEnumerator();
				}
				else if ((double)value < 0.6)
				{
					yield return playerAndBotNaming.changingBotNameGeneric2.GetConversationEnumerator();
				}
				else
				{
					yield return playerAndBotNaming.changingBotNameGeneric3.GetConversationEnumerator();
				}
			}
			finally
			{
				GameVariables.Current.SetBotName(newBotName);
			}
		}

		[Preserve]
		public IEnumerator StartChangingRoom()
		{
			var (enumerator, onFinished) = CoroutineUtilities.PrepareWaitForAction();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new ChangingRoomState(SingletonMonoBehaviour<ViewSingleton>.Instance.ChangingRoomScene.GetBotCharacter(), onFinished));
			yield return enumerator;
		}

		[Preserve]
		public IEnumerator StartClothierShop(bool isWithBot)
		{
			int itemsBoughtBefore = GameVariables.Current.ClothierItemsBought;
			yield return ClothierRequestInfo();
			while (true)
			{
				ClothierStrings clothierStrings = this.clothierStrings;
				CoroutineUtilities.WaitForResult<int> waitForResult = CoroutineUtilities.PrepareWaitForResultBetter<int>();
				List<MenuOption> list = new List<MenuOption>();
				list.Add(new MenuOption(clothierStrings.ShopMenuText.GetString(), () =>
				{
					waitForResult.SetResult(0);
				}));
				if (GameVariables.Current.CheckFlag("CustomClothing"))
				{
					list.Add(new MenuOption(clothierStrings.CustomOrdersMenuText.GetString(), () =>
					{
						waitForResult.SetResult(1);
					}));
				}
				if (isWithBot)
				{
					list.Add(new MenuOption(clothierStrings.ChangingRoomMenuText.GetString(), () =>
					{
						waitForResult.SetResult(2);
					}));
				}
				list.Add(new MenuOption(clothierStrings.LeaveMenuText.GetString(), () =>
				{
					waitForResult.SetResult(3);
				}));
				SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(list);
				yield return waitForResult;
				int result = waitForResult.Result;
				var (enumerator, action) = CoroutineUtilities.PrepareWaitForAction();
				switch (result)
				{
				case 0:
					UiOverlay.Instance.OpenAnnalieShop(action, isWithBot);
					yield return enumerator;
					break;
				case 1:
					UiOverlay.Instance.OpenClothierOrdersShop(action);
					yield return enumerator;
					break;
				case 2:
					SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Outside_Clothier_ChangingRoom, action);
					yield return enumerator;
					break;
				default:
				{
					int num = GameVariables.Current.ClothierItemsBought - itemsBoughtBefore;
					ModsSingleton.LuaEnvironment.Interpreter.Globals["ItemsBoughtThisVisit"] = num;
					yield break;
				}
				}
			}
		}

		[UsedInLua]
		public IEnumerator NunRepairShop()
		{
			NunRepairPopup shop = UiOverlay.Instance.OpenNunRepairShop();
			yield return new WaitUntil(() => shop == null);
		}

		[UsedInLua]
		public IEnumerator PickupNunRepairOrder()
		{
			Result result = GameVariables.Current.itemManager.FinishCurrentNunRepairOrder();
			FungusBoolean = result.IsSuccess;
			if (result.IsSuccess)
			{
				yield return DoConversation(result.GetDescription());
			}
		}

		[Preserve]
		public IEnumerator ClothierRequestInfo()
		{
			List<ItemOrder> notReadyOrders = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.ClothierOrders.Where((ItemOrder t) => t.GetStatusConsideringTime() == ItemOrder.OrderStatus.Created).ToList();
			List<ItemOrder> list = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.ClothierOrders.Where((ItemOrder t) => t.GetStatusConsideringTime() == ItemOrder.OrderStatus.Done).ToList();
			if (list.Count > 0)
			{
				string[] collection = LOC.Other.Shop_ReadyEndings.Split(',', '、');
				StringBuilderCache.AcquireManual(out var stringBuilder);
				foreach (ItemOrder item in list)
				{
					stringBuilder.AppendLine(LOC.Other.Shop_StatusAnnalie(item.Item.GetName(), collection.GetRandom()));
				}
				yield return DoConversation(StringBuilderCache.ReleaseWithToString(stringBuilder));
			}
			if (notReadyOrders.Count <= 0)
			{
				yield break;
			}
			string[] collection2 = LOC.Other.Shop_NotReadyEndings.Split(',', '、');
			StringBuilderCache.AcquireManual(out var stringBuilder2);
			foreach (ItemOrder item2 in notReadyOrders)
			{
				stringBuilder2.AppendLine(LOC.Other.Shop_StatusAnnalie(item2.Item.GetName(), collection2.GetRandom()));
			}
			yield return DoConversation(StringBuilderCache.ReleaseWithToString(stringBuilder2));
		}

		[Preserve]
		public IEnumerator DoConversation(string text, bool waitForInput = true)
		{
			return BetterConversationManager.DoConversation(text, BetterConversationManager.ConversationItemWriteSettings.Get(waitForInput));
		}

		[Preserve]
		public IEnumerator DoMenu(List<MenuOption> options, Action<int, MenuOption> OnChosen, Action<MenuDialog> afterStarted = null)
		{
			(IEnumerator, Action) tuple = CoroutineUtilities.PrepareWaitForAction();
			IEnumerator item = tuple.Item1;
			Action callback = tuple.Item2;
			for (int i = 0; i < options.Count; i++)
			{
				int b = i;
				MenuOption option = options[i];
				option.beforeNextBlock = () =>
				{
					callback();
					OnChosen(b, option);
				};
			}
			MenuDialog obj = SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartMenu(options, automaticUIOverlay: true);
			afterStarted?.Invoke(obj);
			return item;
		}

		public void UpdateRentStatus()
		{
			GameVariables.Current.UpdateRentStatus();
		}

		public void PayRent()
		{
			GameVariables current = GameVariables.Current;
			current.ForceTakeMoney(current.WeeklyRent);
			current.AddFlag("LandlordEvent");
			current.UpdateRentStatus();
		}

		[Preserve]
		public IEnumerator AddHalloweenLandlordDecorations()
		{
			Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			itemManager.AddItem(new Item(ItemEnum.BatBanner));
			itemManager.AddItem(new Item(ItemEnum.GhostDecoration));
			yield break;
		}

		[Preserve]
		public IEnumerator HalloweenHasSweetsOrTake()
		{
			Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			GameId sweetsItemId = GameId.CreateVanilla(ItemEnum.Sweets);
			Item item = itemManager.AllItemsReadonlyCollection().FirstOrDefault((Item t) => t.GameId == sweetsItemId && t.GetCount() >= 3);
			bool flag = item != null;
			if (flag)
			{
				itemManager.ConsumeItem(item, 3);
			}
			FungusBoolean = flag;
			yield break;
		}

		[Preserve]
		public IEnumerator HalloweenAddSweets()
		{
			Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			GameId id = GameId.CreateVanilla(ItemEnum.Sweets);
			int num = UnityEngine.Random.Range(3, 10);
			Item item = new Item(id);
			item.SetCount(num);
			itemManager.AddItem(item);
			yield return SingletonMonoBehaviour<FungusEnumerators>.Instance.DoConversation(LOC.Other.GotSweets(num));
		}

		[Preserve]
		public IEnumerator Lose10percentOfYourMoney()
		{
			int num = Mathf.RoundToInt((float)SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.money * 0.1f);
			if (num > 0)
			{
				yield return SingletonMonoBehaviour<FungusEnumerators>.Instance.DoConversation(LOC.Other.LostMoney(num));
			}
		}

		[Preserve]
		public IEnumerator HalloweenAddItemsToClothierShop()
		{
			if (!SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.ShopManager.TryGetShop("clothier", out var shop))
			{
				yield break;
			}
			foreach (ItemEnum item in new List<ItemEnum>
			{
				ItemEnum.PointyEars,
				ItemEnum.HyperCamoPointyEars,
				ItemEnum.TinyWizardHat,
				ItemEnum.PumpkinHairpin,
				ItemEnum.SpiderPlush,
				ItemEnum.PumpkinMat
			})
			{
				GameId gameId = item.ToGameId();
				if (shop.ShopItems.All((ShopItem t) => t.Item.GameId != gameId))
				{
					shop.AddItem(new ShopItem(Item.CreateRandomItem(gameId), ShopItem.ShopItemType.SingleBuy));
				}
			}
		}

		public async UniTask StartFishingJointDuel(FishingJointDuelId targetDuel)
		{
			GameId fishingLocationEnum = ((targetDuel != FishingJointDuelId.Bot) ? FishingLocationEnumGI.ImaginarySea.ToGameId() : FishingLocationEnumGI.PremiumFishFarm.ToGameId());
			FishingStartInfo fishingStartInfo = new FishingStartInfo
			{
				FishingJointDuelId = targetDuel,
				FishingLengthMinutes = 100,
				FishingLocation = FishingLocationDataManager.GetLocationData(fishingLocationEnum)
			};
			UniTaskCompletionSource<FishingResults> fishingResults = new UniTaskCompletionSource<FishingResults>();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new FishingGameplayState(fishingStartInfo)
			{
				OnResultOverride = (FishingResults r) =>
				{
					fishingResults.TrySetResult(r);
				}
			});
			FungusBoolean = (await fishingResults.Task).TryGetDuelResults(out var duelResults) && duelResults.IsPlayerWinning();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
		}

		public FadeViewUniTaskCallback FadeToView(View view, float fadeDuration = 1f, bool fadeOut = true, Color? fadeColor = null)
		{
			Color valueOrDefault = fadeColor.GetValueOrDefault();
			Color color;
			if (!fadeColor.HasValue)
			{
				valueOrDefault = Color.black;
				fadeColor = valueOrDefault;
				color = valueOrDefault;
			}
			else
			{
				color = valueOrDefault;
			}
			Color fadeColor2 = color;
			FadeViewUniTaskCallback fadeViewUniTaskCallback = new FadeViewUniTaskCallback();
			FungusManager.Instance.CameraManager.FadeToViewMainCamera(view, fadeDuration, fadeOut, fadeColor2, fadeViewUniTaskCallback);
			return fadeViewUniTaskCallback;
		}

		private async UniTask EnsureGameStopped()
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
			if (GameScript.HasGameStarted)
			{
				UiOverlay.Instance.GetStaticGUI<GameStaticGui>(out var staticGui);
				staticGui.gameObject.SetActive(value: false);
				await FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.GetNotCurrentViewSetSprite(null, Color.black), 0.25f).AwaitIntroStart3();
				SingletonLoadableMonoBehaviour<GameScript>.Instance.EndGame();
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
			}
		}

		public async UniTask StartMainMenu()
		{
			await EnsureGameStopped();
			await FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.MenuView).AwaitIntroStart3();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new GameScript.MainMenuState());
		}

		public async UniTask StartDeveloperRoom()
		{
			await EnsureGameStopped();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
			FungusManager.Instance.CameraManager.FadeToViewMainCamera(SingletonMonoBehaviour<ViewSingleton>.Instance.DeveloperRoomView, 1f, fadeOut: true, Color.black);
		}

		public async UniTask RollCreditsForEnding(string endingId)
		{
			SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler.AddAchievement(endingId);
			SingletonLoadableMonoBehaviour<AchievementSingleton>.Instance.AchievementHandler.Save();
			UniTaskCompletionSource completionSource = new UniTaskCompletionSource();
			GameScript.CreditsState newState = new GameScript.CreditsState
			{
				EndingId = endingId,
				OnResultOverride = () =>
				{
					completionSource.TrySetResult();
				}
			};
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(newState);
			await completionSource.Task;
		}

		public async UniTask RollCreditsAndWaitTillEnd()
		{
			UniTaskCompletionSource completionSource = new UniTaskCompletionSource();
			GameScript.CreditsState newState = new GameScript.CreditsState
			{
				OnResultOverride = () =>
				{
					completionSource.TrySetResult();
				}
			};
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(newState);
			await completionSource.Task;
		}

		public async UniTask StartFirstFishing()
		{
			Item item = Item.CreateRandomItem(ItemEnum.BasicFishingRod.ToGameId(), allowModifiers: false);
			Item item2 = Item.CreateRandomItem(ItemEnum.WormBait.ToGameId(), allowModifiers: false);
			item2.SetCount(5);
			Item.ItemManager itemManager = GameVariables.Current.itemManager;
			itemManager.AddItem(item);
			itemManager.AddItem(item2);
			itemManager.Equip(item, forceRemoveForbidden: true);
			itemManager.Equip(item2, forceRemoveForbidden: true);
			await SingletonLoadableMonoBehaviour<GameScript>.Instance.WaitTillStatesResultOverrideDefault(new FishingGameplayState(new FishingStartInfo
			{
				FishingLengthMinutes = 30,
				FishingLocation = FishingLocationDataManager.GetLocationData(FishingLocationEnumGI.MicroplasticsCoast.ToGameId())
			}));
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryState();
		}

		public UniTask StartFishing()
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new FishingPrepareState());
			return UniTask.CompletedTask;
		}

		public async UniTask StartFishingGameplay(FishingStartInfo startInfo)
		{
			await FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.GetNotCurrentViewSetSprite(startInfo.FishingLocation.BackgroundSprite?.GetResource(), Color.white)).AwaitIntroStart3();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new FishingGameplayState(startInfo));
		}

		public async UniTask OpenFisherShop()
		{
			UniTaskCompletionSource completionSource = new UniTaskCompletionSource();
			UiOverlay.Instance.OpenFisherSellFish(() =>
			{
				completionSource.TrySetResult();
			});
			await completionSource.Task;
		}

		[Preserve]
		public async UniTask StartShower()
		{
			await FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.GetNotCurrentViewSetSprite(null, Color.black)).AwaitIntroStart3();
			await FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.ShowerView).AwaitIntroStart3();
		}

		[Preserve]
		public async UniTask StartCooking()
		{
			await FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.GetNotCurrentViewSetSprite(null, Color.black)).AwaitIntroStart3();
			await FadeToView(SingletonMonoBehaviour<ViewSingleton>.Instance.RoomStoryViewWithoutAutomaticBot).AwaitIntroStart3();
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new CookingSongSelectState());
		}

		[Preserve]
		public async UniTask StartBotFullLeadSex()
		{
			GameVariables.Current.AddFlag("botRaep");
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Stamina += 0.25f;
			await SingletonLoadableMonoBehaviour<GameScript>.Instance.SleepUniTask(60, null, suppressEvents: true, GameVariables.StaminaSyncType.Recovery);
			EyeClosingShader eyeClosingScript = UiOverlay.Instance.MainCamera.GetComponent<EyeClosingShader>();
			eyeClosingScript.CloseInstantly();
			_ = SingletonMonoBehaviour<ViewSingleton>.Instance.CowgirlScene;
			View cowgirlView = SingletonMonoBehaviour<ViewSingleton>.Instance.CowgirlView;
			FadeViewUniTaskCallback fadeViewUniTaskCallback = new FadeViewUniTaskCallback();
			FungusManager.Instance.CameraManager.FadeToViewSceneWithArguments<FrickScene, FrickSceneTransitionData>(cowgirlView, 1f, fadeOut: true, Color.black, new FrickSceneTransitionData(FrickSceneInitFlags.BotFullLead), fadeViewUniTaskCallback);
			await fadeViewUniTaskCallback.AwaitIntroStart3();
			await UniTask.WaitForSeconds(0.5f);
			R.Sound.Frick_unzip_unzip.Get().Play();
			await UniTask.WaitForSeconds(2f);
			await eyeClosingScript.WakeUp();
		}

		[Preserve]
		public UniTask StartSex(bool streamOn, View view)
		{
			FrickSceneInitFlags flags = (streamOn ? FrickSceneInitFlags.StreamOn : FrickSceneInitFlags.None);
			return StartSex(view, flags);
		}

		[Preserve]
		public async UniTask StartSex(View view, FrickSceneInitFlags flags)
		{
			_ = (FrickScene)view.Scene;
			FadeViewUniTaskCallback fadeViewUniTaskCallback = new FadeViewUniTaskCallback();
			FungusManager.Instance.CameraManager.FadeToViewSceneWithArguments<FrickScene, FrickSceneTransitionData>(view, 1f, fadeOut: true, Color.black, new FrickSceneTransitionData(flags), fadeViewUniTaskCallback);
			await fadeViewUniTaskCallback.AwaitIntroStart3();
		}

		[Preserve]
		public async UniTask SwitchSexScene(View view, bool disableFungusOverlay = true)
		{
			_ = (FrickScene)view.Scene;
			FrickSceneInitFlags initFlags = FrickSceneInitFlags.None;
			if (SingletonLoadableMonoBehaviour<GameScript>.Instance.TryGetStateAs<NewFrickState>(out var state))
			{
				initFlags = state.InitFlags;
			}
			else
			{
				Debug.LogError("Trying to switch sex scene while not in a sex scene WTF!?!");
			}
			FadeViewUniTaskCallback fadeViewUniTaskCallback = new FadeViewUniTaskCallback();
			FungusManager.Instance.CameraManager.FadeToViewSceneWithArguments<FrickScene, FrickSceneTransitionData>(view, 1f, fadeOut: true, Color.black, new FrickSceneTransitionData(initFlags), fadeViewUniTaskCallback);
			await fadeViewUniTaskCallback.AwaitIntroStart3();
			if (disableFungusOverlay)
			{
				UiOverlay.Instance.CloseFungusOverlay();
			}
		}
	}
