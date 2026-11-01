using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cysharp.Threading.Tasks;
using GameItems;
using IC.AutomaticReference;
using ItemConditions;
using JetBrains.Annotations;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class ItemPrefab
{
	public enum ScratchTextureType
	{
		Universal,
		Hard
	}

	public enum CustomShaderTypeEnum
	{
		None,
		RelatedDrawables,
		Hair
	}

	public class ItemPrefabManager
	{
		private enum LocaleType
		{
			ProjectOrMod,
			Current
		}

		public class Localization
		{
			[LoadableStaticMethod(new Type[]
			{
				typeof(ItemPrefabManager),
				typeof(ModsSingleton)
			})]
			public static UniTask RunItemsLocalization()
			{
				LocalizeInternal();
				if (Application.isPlaying)
				{
					LocalizationSingleton.Register(Localize);
				}
				return UniTask.CompletedTask;
			}
		}

		private static GameIdDictionary<ItemPrefab> _itemPrefabs = new GameIdDictionary<ItemPrefab>();

		private static List<(GameId item1, GameId item2)> _knownItemPair = new List<(GameId, GameId)>();

		private static Dictionary<GameId, int> _itemToItemPairIndex = new Dictionary<GameId, int>();

		private static readonly string ItemsPrefix = "Item";

		private static readonly string NameEnding = "_Name";

		private static readonly string DescriptionEnding = "_Description";

		private static bool _IsLoaded = false;

		public static IEnumerable<(GameId id, ItemPrefab prefab)> AllNonDefaultItems => from x in _itemPrefabs.AllValuesWithIds()
			where !x.value.IsDefault
			select x;

		public static IEnumerable<(GameId id, ItemPrefab prefab)> AllItems => _itemPrefabs.AllValuesWithIds();

		public static IEnumerable<(GameId id, ItemPrefab prefab)> AllEquipableItems => from x in _itemPrefabs.AllValuesWithIds()
			where x.value.Equipable
			select x;

		public static IEnumerable<(GameId id, ItemPrefab prefab)> AllVanillaItems => AllPrefabsFromSource(Guid.Empty);

		private static GameVariables gameVariables => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;

		public static void RegisterItemPair(GameId gameId1, GameId gameId2)
		{
			bool flag = _itemToItemPairIndex.TryGetValue(gameId1, out var value);
			bool flag2 = _itemToItemPairIndex.TryGetValue(gameId2, out var value2);
			if (flag | flag2)
			{
				Debug.LogWarning("Cannot register item pair, because at least one of the items is in a different pair!\n" + $"{gameId1} ({flag} => {value}) {gameId2} ({flag2} => {value2}). ");
			}
			else
			{
				int num = _knownItemPair.Count + 1;
				_knownItemPair.Add((gameId1, gameId2));
				_itemToItemPairIndex.Add(gameId1, num);
				_itemToItemPairIndex.Add(gameId2, -num);
			}
		}

		public static bool TryGetItemPairIndex(Item item, out int itemPairIndex)
		{
			return _itemToItemPairIndex.TryGetValue(item.GameId, out itemPairIndex);
		}

		public static bool HasAnyItems(Guid guid)
		{
			if (_itemPrefabs.TryGetValue(guid, out var value))
			{
				return value.Values.Any();
			}
			return false;
		}

		public static IEnumerable<(GameId id, ItemPrefab prefab)> AllPrefabsFromSource(Guid guid)
		{
			if (_itemPrefabs.TryGetValue(guid, out var value))
			{
				return value.Select((KeyValuePair<int, ItemPrefab> t) => (GameId.CreateGeneric(guid, t.Key), Value: t.Value));
			}
			return Array.Empty<(GameId, ItemPrefab)>();
		}

		public static ItemPrefab GetItemById(GameId itemId)
		{
			if (_itemPrefabs.TryGetValue(itemId, out var t))
			{
				return t;
			}
			ItemPrefab itemPrefab;
			if (itemId.IsVanilla)
			{
				itemPrefab = new ItemPrefab(LOC.UI.Equipment_MissingItem, LOC.UI.Equipment_MissingItemVanilla(itemId.Id), null, 0);
			}
			else
			{
				itemPrefab = ((!SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.TryGetLocalModInfo(itemId.Guid, out var lmi)) ? new ItemPrefab(LOC.UI.Equipment_MissingItem, LOC.UI.Equipment_MissingItemUnknownMod(itemId.Id, itemId.Guid), null, 0) : new ItemPrefab(LOC.UI.Equipment_MissingItem, LOC.UI.Equipment_MissingItemKnownMod(itemId.Id, itemId.Guid, lmi.ModName), null, 0));
			}
			itemPrefab.IsMissingItem = true;
			AddModItem(itemId.Guid, itemPrefab, itemId.Id);
			return itemPrefab;
		}

		private static IEnumerable<(string keyPrefix, StringTable targetTable, Guid guid, Dictionary<int, ItemPrefab> itemsDictionary)> GetLocalizationItems(LocaleType localeType)
		{
			foreach (var (guid2, item) in _itemPrefabs)
			{
				StringTable item2;
				string item3;
				if (guid2 == Guid.Empty)
				{
					item2 = localeType switch
					{
						LocaleType.ProjectOrMod => LocalizationSettings.StringDatabase.GetTableAsync("Items", LocalizationSettings.ProjectLocale).Result, 
						_ => LocalizationSettings.StringDatabase.GetTableAsync("Items").Result, 
					};
					item3 = ItemsPrefix + "_";
				}
				else
				{
					item2 = localeType switch
					{
						LocaleType.ProjectOrMod => LocalizationSettings.StringDatabase.GetTableAsync("Moddable", SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModLocaleOrProject(guid2)).Result, 
						_ => LocalizationSettings.StringDatabase.GetTableAsync("Moddable").Result, 
					};
					item3 = guid2.ToString() + "_" + ItemsPrefix + "_";
				}
				yield return (keyPrefix: item3, targetTable: item2, guid: guid2, itemsDictionary: item);
			}
		}

		private static string ColorSlotEnding(int i)
		{
			return i switch
			{
				0 => "_Color+0", 
				1 => "_Color+1", 
				2 => "_Color+2", 
				3 => "_Color+3", 
				4 => "_Color+4", 
				_ => "_Color+" + i, 
			};
		}

		private static void LocalizeInternal()
		{
			foreach (var localizationItem in GetLocalizationItems(LocaleType.Current))
			{
				var (text, table, _, _) = localizationItem;
				foreach (var (num2, itemPrefab2) in localizationItem.itemsDictionary)
				{
					if (itemPrefab2.IsDefault)
					{
						continue;
					}
					string text2 = text + num2;
					StringTableEntry entryWithFallback = table.GetEntryWithFallback(text2 + NameEnding);
					StringTableEntry entryWithFallback2 = table.GetEntryWithFallback(text2 + DescriptionEnding);
					if (entryWithFallback == null || entryWithFallback2 == null)
					{
						if (num2 == 0)
						{
						}
						continue;
					}
					itemPrefab2.Name = entryWithFallback.Value;
					itemPrefab2.Description = entryWithFallback2.Value;
					for (int i = 0; i < itemPrefab2.ColorSlots.Length; i++)
					{
						ColorSlot colorSlot = itemPrefab2.ColorSlots[i];
						StringTableEntry entryWithFallback3 = table.GetEntryWithFallback(text2 + ColorSlotEnding(i));
						if (entryWithFallback3 != null)
						{
							colorSlot.Name = entryWithFallback3.Value;
						}
					}
				}
			}
		}

		public static void CreateLocalizationEntries()
		{
			foreach (var (text, stringTable, guid, dictionary) in GetLocalizationItems(LocaleType.ProjectOrMod))
			{
				if (guid == Guid.Empty)
				{
					continue;
				}
				foreach (var (num2, itemPrefab2) in dictionary)
				{
					if (!itemPrefab2.IsDefault)
					{
						string text2 = text + num2;
						stringTable.AddEntry(text2 + NameEnding, itemPrefab2.Name);
						stringTable.AddEntry(text2 + DescriptionEnding, itemPrefab2.Description);
						for (int i = 0; i < itemPrefab2.ColorSlots.Length; i++)
						{
							ColorSlot colorSlot = itemPrefab2.ColorSlots[i];
							stringTable.AddEntry(text2 + ColorSlotEnding(i), colorSlot.Name);
						}
					}
				}
			}
		}

		public static SimpleLocalizedString GetItemNameSls(Item item)
		{
			StringTable tableSafe = LOS.GetTableSafe("Items");
			string text = ItemsPrefix + "_" + item.GameId.ToShortString() + NameEnding;
			if (tableSafe.GetEntryWithFallback(text) == null)
			{
				return SimpleLocalizedString.CreateInstanceReplacement(item.GetName(ItemNameIncludeEnum.Basic));
			}
			return SimpleLocalizedString.CreateInstance(tableSafe.TableCollectionName, text);
		}

		private static void Localize(Locale locale)
		{
			if (_IsLoaded)
			{
				LocalizeInternal();
			}
		}

		public static GameId AddModItem(Guid guid, ItemPrefab itemPrefab, int id = -1)
		{
			if (!_itemPrefabs.TryGetValue(guid, out var value))
			{
				value = (_itemPrefabs[guid] = new Dictionary<int, ItemPrefab>());
			}
			if (id < 0)
			{
				id = ((value.Keys.Count != 0) ? (value.Keys.Max() + 1) : 0);
				if (DebugDefines.ShowModLogs)
				{
					Debug.Log($"Automatic id generation for {itemPrefab.Name} assigned id={id}");
				}
			}
			if (value.TryGetValue(id, out var value2) && DebugDefines.ShowModLogs)
			{
				Debug.LogWarning($"Overriding item {value2.Name} with {itemPrefab.Name} at id={id}");
			}
			value[id] = itemPrefab;
			if (DebugDefines.ShowModLogs)
			{
				Debug.Log($"Adding item {itemPrefab.Name} id:{id}");
			}
			GameId gameId = GameId.CreateGeneric(guid, id);
			itemPrefab.InitGameId(gameId);
			return gameId;
		}

		[LoadableStaticMethod(new Type[]
		{
			typeof(ItemSlot.SlotManager),
			typeof(CocktractPartnerPrefabManager),
			typeof(ItemModifierPrefabManager)
		})]
		public static UniTask PrepareItems()
		{
			int num = 12500;
			TextureSingleton instance = SingletonMonoBehaviour<TextureSingleton>.Instance;
			Dictionary<int, ItemPrefab> dictionary = (_itemPrefabs[Guid.Empty] = new Dictionary<ItemEnum, ItemPrefab>
			{
				[ItemEnum.DefaultSkin] = CreateDefault(ItemSlot.SlotManager.Skin, ColorPaletteManager.DefaultSkin),
				[ItemEnum.DefaultEyes] = CreateDefault(ItemSlot.SlotManager.Eyes, ColorPaletteManager.JunsPupils, ColorPaletteManager.JunsPupils, ColorPaletteManager.JunsIrises, ColorPaletteManager.JunsIrises, Color.white),
				[ItemEnum.DefaultHeartPupils] = CreateDefault(ItemSlot.SlotManager.HeartPupils, ColorPaletteManager.JunsBlush),
				[ItemEnum.DefaultFlesh] = CreateDefault(ItemSlot.SlotManager.Flesh, ColorPaletteManager.JunsFlesh, ColorPaletteManager.JunsFlesh, ColorPaletteManager.JunsFlesh),
				[ItemEnum.DefaultTeeth] = CreateDefault(ItemSlot.SlotManager.Teeth, Color.white),
				[ItemEnum.DefaultHologram] = CreateDefault(ItemSlot.SlotManager.Hologram, Color.white).SetControllerElementOverride("Hologram_BasicHologram"),
				[ItemEnum.CatEars] = new ItemPrefab("Cat ears", "An item that unites zoophiles and weebs. Limited furry edition.", ItemSlot.SlotManager.Head, 10000)
				{
					IsIllegal = true,
					HasQuality = true,
					Category = ItemCategory.Modules,
					SubCategory = ItemSubCategory.Modules_CatEars
				}.SetDefaultColorSlots(new List<string> { "Main color", "Fluff color" }).SetControllerElementOverride(Live2DController.CatEarsElementId),
				[ItemEnum.BasicArmL] = new ItemPrefab("Left arm", "Left arm compatible with your sexbot!", ItemSlot.SlotManager.ArmL, num)
				{
					IsIllegal = true,
					HasColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.BasicArmR] = new ItemPrefab("Right arm", "Right arm compatible with your sexbot!", ItemSlot.SlotManager.ArmR, num)
				{
					IsIllegal = true,
					HasColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.BasicLegL] = new ItemPrefab("Left leg", "Even a standalone left leg can provide ample pleasure", ItemSlot.SlotManager.LegL, num + 1000)
				{
					IsIllegal = true,
					HasColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.BasicLegR] = new ItemPrefab("Right leg", "Even a standalone right leg can provide ample pleasure", ItemSlot.SlotManager.LegR, num + 1000)
				{
					IsIllegal = true,
					HasColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.SharpTeeth] = new ItemPrefab("Sharp teeth", "Give your bot sharper teeth! Why?", ItemSlot.SlotManager.Teeth, num)
				{
					IsIllegal = true,
					HasColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				}.SetControllerElementOverride(Live2DController.SharpTeethId),
				[ItemEnum.HyperCamoArmL] = new ItemPrefab("Hypercamo left arm", "Left arm compatible with your sexbot!" + HypercamoDescriptionAppendix, ItemSlot.SlotManager.ArmL, 18000)
				{
					IsIllegal = true,
					HasColor = true,
					CanChangeColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.HyperCamoArmR] = new ItemPrefab("Hypercamo right arm", "Right arm compatible with your sexbot!" + HypercamoDescriptionAppendix, ItemSlot.SlotManager.ArmR, 18000)
				{
					IsIllegal = true,
					HasColor = true,
					CanChangeColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.HyperCamoLegL] = new ItemPrefab("Hypercamo left leg", "Even a standalone left leg can provide ample pleasure." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.LegL, 19000)
				{
					IsIllegal = true,
					HasColor = true,
					CanChangeColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.HyperCamoLegR] = new ItemPrefab("Hypercamo right leg", "Even a standalone right leg can provide ample pleasure." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.LegR, 19000)
				{
					IsIllegal = true,
					HasColor = true,
					CanChangeColor = true,
					HasQuality = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				},
				[ItemEnum.HyperCamoSkin] = new ItemPrefab("Hypercamo skin", "Somehow works out of the box." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.Skin, 19000)
				{
					IsIllegal = true,
					HasColor = true,
					CanChangeColor = true,
					Category = ItemCategory.Modules
				}.AddSusModifier(SusArea.Skin, 0.5f),
				[ItemEnum.HyperCamoEyes] = new ItemPrefab("Hypercamo eyes", "Customise the eyes as you see fit! " + HypercamoDescriptionAppendix, ItemSlot.SlotManager.Eyes, 18000)
				{
					IsIllegal = true,
					HasQuality = true,
					CanChangeColor = true,
					ScratchType = ScratchTextureType.Hard,
					Category = ItemCategory.Modules
				}.SetDefaultColorSlots(new List<string> { "Left Pupil", "Right Pupil", "Left Iris", "Right Iris", "Sclera" }),
				[ItemEnum.HyperCamoFlesh] = new ItemPrefab("Hypercamo flesh", "Change the color of lips, vagina, nipples to your liking with the newest hypercamo invention." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.Flesh, 18000)
				{
					IsIllegal = true,
					HasQuality = true,
					CanChangeColor = true,
					Category = ItemCategory.Modules
				}.SetDefaultColorSlots(new List<string> { "Mouth", "Nipples", "Vagina" }),
				[ItemEnum.HyperCamoCatEars] = new ItemPrefab("Hypercamo cat ears", "An item that unites zoophiles and weebs. Limited furry edition." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.Head, 18000)
				{
					IsIllegal = true,
					HasQuality = true,
					CanChangeColor = true,
					Category = ItemCategory.Modules,
					SubCategory = ItemSubCategory.Modules_CatEars
				}.SetDefaultColorSlots(new List<string> { "Main color", "Fluff color" }).SetControllerElementOverride(Live2DController.CatEarsElementId),
				[ItemEnum.HyperCamoPointyEars] = new ItemPrefab("Hypercamo pointy ears", "Pointy ears just like an elf." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.Ears, 4500)
				{
					CanChangeColor = true,
					Category = ItemCategory.Modules,
					HasColor = true
				},
				[ItemEnum.HightechHyperCamoSkin] = new ItemPrefab("High-tech Hypercamo skin", "High-tech hypercamo skin. For some reason it's extremely durable and doesn't get worn. Makes the bot look more human!\nSomehow works out of the box." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.Skin, 54000)
				{
					IsIllegal = true,
					HasColor = true,
					CanChangeColor = true,
					Category = ItemCategory.Modules
				}.AddTexture(instance.hightechHypercamoSkin).AddSusModifier(SusArea.Skin, 0.9f),
				[ItemEnum.RainbowSkin] = new ItemPrefab("Rainbow skin", "Somehow works out of the box. VERY rare.", ItemSlot.SlotManager.Skin, 190000)
				{
					IsIllegal = true,
					Category = ItemCategory.Modules
				}.AddSusModifier(SusArea.Skin, 0.5f).SetRainbowShader(),
				[ItemEnum.HologramDisable] = new ItemPrefab("Hologram disable", "Simple module that allows you to disable the hologram above your bot's hair.", ItemSlot.SlotManager.Hologram, 2500)
				{
					IsIllegal = true,
					Category = ItemCategory.Modules
				}.SetControllerElementOverride(),
				[ItemEnum.Antidepressants] = new ItemPrefab("Dr. Blau’s Special Brain Mix", "Should help with mental health. Now without prescription. Will it really help though? Said to be very addictive", null, 50, (Item _) =>
				{
					gameVariables.Health -= 0.01f;
					if (gameVariables.RealMentalHealth >= 0.1f)
					{
						gameVariables.MentalHealth -= 0.1f;
						gameVariables.MentalHealthTemporary += 0.1f;
					}
					gameVariables.MentalHealthTemporary += 0.5f;
					return true;
				})
				{
					IsStackable = true,
					Category = ItemCategory.Meds
				},
				[ItemEnum.SempillX] = new ItemPrefab("SempillX", "Regenerates most of your cum, but makes you drowsy. Apparently doesn't affect your health.", null, 5000, (Item _) =>
				{
					gameVariables.RemainingCum += gameVariables.MaxCum * 0.75f;
					gameVariables.Stamina -= 0.125f;
					return true;
				})
				{
					IsStackable = true,
					Category = ItemCategory.Meds
				},
				[ItemEnum.Vinegara] = new ItemPrefab("Vinegara", "Allows you to maintain erection for up to 2 hours in exchange for your soul. Smells like vinegar. Taking more than one doesn't seem to make a difference.", null, 3000, (Item _) =>
				{
					gameVariables.Health -= 0.05f;
					gameVariables.vinegaraEffectEnd = gameVariables.time + 120 + UnityEngine.Random.Range(-20, 20);
					return true;
				})
				{
					IsStackable = true,
					Category = ItemCategory.Meds
				},
				[ItemEnum.EndurancePlus] = new ItemPrefab("Endurance+", "Magical blend of 40 spices. Increases your semen production and lets you cum more than ever. Safe dose for a healthy adult is one pill a week. More than that and your body might not last.", null, 10000, (Item i) =>
				{
					gameVariables.Health -= 0.5f;
					gameVariables.MaxCum += 20f;
					return true;
				})
				{
					IsStackable = true,
					Category = ItemCategory.Meds
				}.SetNotExpectedInShop(),
				[ItemEnum.VinegaraPremium] = new ItemPrefab("Vinegara PREMIUM", "Premium version of normal vinegara. Allows you to maintain erection for up to 24 hours in exchange for your soul. Smells like very strong vinegar. Taking more than one doesn't seem to make a difference.", null, 50000, (Item _) =>
				{
					gameVariables.Health -= 0.15f;
					gameVariables.vinegaraEffectEnd = gameVariables.time + 1440 + UnityEngine.Random.Range(-50, 50);
					return true;
				})
				{
					IsStackable = true,
					Category = ItemCategory.Meds
				},
				[ItemEnum.DeathGrip] = new ItemPrefab("Death Grip", "Drug named after a medical condition referring to men that masturbate with an overly tight grip on their penises. Desensitizes one's penis, making it harder to cum. Works for about 4 hours.", null, 1000, (Item i) =>
				{
					gameVariables.Health -= 0.15f;
					gameVariables.deathGripEffectEnd = gameVariables.time + 240 + UnityEngine.Random.Range(-30, 30);
					return true;
				})
				{
					IsStackable = true,
					Category = ItemCategory.Meds
				},
				[ItemEnum.PixelGlasses] = new ItemPrefab("Pixel glasses", "Tired of the way world looks? Wear these glasses to turn it into pixels!\n" + "IT ALSO AFFECTS THE GUI!".TMPColorSurround(Color.red), ItemSlot.SlotManager.Anon_Glasses, 1000)
				{
					SpecialEffectAction = (bool t, Item item) =>
					{
						UiOverlay.Instance.MainCamera.GetComponent<PixelShaderScript>().enabled = item.IsEquipped() & t;
					},
					Category = ItemCategory.Other
				}.SetNotExpectedInShop(),
				[ItemEnum.PumpkinMat] = new ItemPrefab("Pumpkin mat", "Spice up your room with this pumpkin shaped mat!", ItemSlot.SlotManager.Room_Mat, 1000)
				{
					SpecialEffectAction = TextureOverriderManager.RoomManager.GetSpecialEffectsAction("/Room/mat", R.AltBackgrounds.Pumpkin_Mat),
					Category = ItemCategory.Room,
					OverrideIconReference = R.Icons.halloween
				},
				[ItemEnum.SpiderPlush] = new ItemPrefab("Spider plush", "Spice up your room with this spider plush.", ItemSlot.SlotManager.Room_Plush, 1000)
				{
					SpecialEffectAction = TextureOverriderManager.RoomManager.GetSpecialEffectsAction("/Room/SpiderPlush", R.AltBackgrounds.SpiderPlushy),
					Category = ItemCategory.Room,
					OverrideIconReference = R.Icons.halloween
				},
				[ItemEnum.BatBanner] = new ItemPrefab("Bat banner", "Spice up your room with this bat banner.", ItemSlot.SlotManager.Room_Banner, 1000)
				{
					SpecialEffectAction = TextureOverriderManager.RoomManager.GetSpecialEffectsAction("/Room/BatBanner", R.AltBackgrounds.Bat_Banner),
					Category = ItemCategory.Room,
					OverrideIconReference = R.Icons.halloween
				}.SetNotExpectedInShop(),
				[ItemEnum.GhostDecoration] = new ItemPrefab("Ghost decoration", "Spice up your room with this ghost wall decoration.", ItemSlot.SlotManager.Room_WallDecoration, 1000)
				{
					SpecialEffectAction = TextureOverriderManager.RoomManager.GetSpecialEffectsAction("/Room/GhostBackground", R.AltBackgrounds.Ghost_Background),
					Category = ItemCategory.Room,
					OverrideIconReference = R.Icons.halloween
				}.SetNotExpectedInShop(),
				[ItemEnum.BingChillingMagazineBundle] = new ItemPrefab("\"Bing Chilling\" magazine bundle", "Weird bundle of magazines you got as a reward from Cazino.", null, 100)
				{
					OnUse = (Item item) =>
					{
						UiOverlay.Instance.CloseUiOverlay();
						SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Item_CazinoBundleEvent);
						return true;
					},
					ThrowingOutForbidden = true,
					Category = ItemCategory.Other
				}.SetNotExpectedInShop(),
				[ItemEnum.LingerieCatalogue] = new ItemPrefab("Lingerie catalogue", "Lingerie catalogue. For some reason it was bundled with one of your ladyparts purchases.", null, 100)
				{
					OnUse = (Item item) =>
					{
						UiOverlay.Instance.CloseUiOverlay();
						SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Item_BdhAdviceLingerieCatalogue);
						return true;
					},
					ThrowingOutForbidden = true,
					Category = ItemCategory.Other
				}.SetNotExpectedInShop(),
				[ItemEnum.LandlordsOldStove] = new ItemPrefab("Landlord's old stove", "A stove you got from the landlord.", null, 199)
				{
					ThrowingOutForbidden = true,
					Category = ItemCategory.Other
				}.SetNotExpectedInShop(),
				[ItemEnum.FixedLandlordsOldStove] = new ItemPrefab("Fixed Landlord's old stove", "A stove you got from the landlord. It's been fixed and improved! Efficiency is up by 15% and it has modern safety mechanisms!", null, 220)
				{
					ThrowingOutForbidden = true,
					Category = ItemCategory.Other
				}.SetNotExpectedInShop()
			}.ToDictionary((KeyValuePair<ItemEnum, ItemPrefab> x) => (int)x.Key, (KeyValuePair<ItemEnum, ItemPrefab> x) => x.Value));
			Dictionary<int, ItemPrefab> vanillaDict = dictionary;
			vanillaDict[10000] = new ItemPrefab("T-shirt", "Simple T-shirt", ItemSlot.SlotManager.UpperBody, 400)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.UpperBody, 0.8f).SetControllerElementOverride(Live2DController.ShirtId);
			vanillaDict[11000] = new ItemPrefab("Plain panties", "Simple panties.", ItemSlot.SlotManager.Panties, 300)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.Pussy, 1f);
			vanillaDict[14000] = new ItemPrefab("Plain bra", "Simple bra.", ItemSlot.SlotManager.Bra, 400)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.Boobs, 1f);
			vanillaDict[11003] = new ItemPrefab("Bikini panties", "Panties used for swimming. Two color.", ItemSlot.SlotManager.Panties, 4000)
			{
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(new ColorSlot[2]
			{
				new ColorSlot("Main color", ColorPaletteManager.DefaultPartPalette),
				new ColorSlot("Accent Color")
			}).SetControllerElementOverride(Live2DController.BikiniBottomId).AddTexture(instance.bikiniBottom)
				.AddSusModifier(SusArea.Pussy, 0.7f);
			vanillaDict[14002] = new ItemPrefab("Bikini bra", "Bra used for swimming. Two color.", ItemSlot.SlotManager.Bra, 4000)
			{
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(new ColorSlot[2]
			{
				new ColorSlot("Main color", ColorPaletteManager.DefaultPartPalette),
				new ColorSlot("Accent Color")
			}).SetControllerElementOverride(Live2DController.BikiniTopId).AddSusModifier(SusArea.Boobs, 0.7f);
			vanillaDict[11150] = new ItemPrefab("Bikini set", "Two color set of bikini bra and panties.", null, 9000).SetDefaultColorSlots(new ColorSlot[2]
			{
				new ColorSlot("Main color", ColorPaletteManager.DefaultPartPalette),
				new ColorSlot("Accent Color")
			}).MakePair(ItemEnum.BikiniPanties.ToGameId(), ItemEnum.BikiniBra.ToGameId(), registerAsMatching: true);
			vanillaDict[11004] = new ItemPrefab("Transparent bikini panties", "Panties used for swimming. Two color. Main fabric is slightly see-through.", ItemSlot.SlotManager.Panties, 50000)
			{
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(new ColorSlot[2]
			{
				new ColorSlot("Main color", ColorPaletteManager.DefaultPartPalette, ColorSlotTransparencyEnum.TransparencyForced50percent),
				new ColorSlot("Accent Color")
			}).SetControllerElementOverride(Live2DController.BikiniBottomId).AddTexture(instance.bikiniBottom)
				.AddSusModifier(SusArea.Pussy, 0.4f);
			vanillaDict[14003] = new ItemPrefab("Transparent bikini bra", "Bra used for swimming. Two color. Main fabric is slightly see-through.", ItemSlot.SlotManager.Bra, 50000)
			{
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(new ColorSlot[2]
			{
				new ColorSlot("Main color", ColorPaletteManager.DefaultPartPalette, ColorSlotTransparencyEnum.TransparencyForced50percent),
				new ColorSlot("Accent Color")
			}).SetControllerElementOverride(Live2DController.BikiniTopId).AddSusModifier(SusArea.Boobs, 0.4f);
			vanillaDict[11151] = new ItemPrefab("Transparent bikini set", "Two color set of transparent bikini bra and panties. Lewd...", null, 110000).SetDefaultColorSlots(new ColorSlot[2]
			{
				new ColorSlot("Main color", ColorPaletteManager.DefaultPartPalette, ColorSlotTransparencyEnum.TransparencyForced50percent),
				new ColorSlot("Accent Color")
			}).MakePair(ItemEnum.TransparentBikiniPanties.ToGameId(), ItemEnum.TransparentBikiniBra.ToGameId(), registerAsMatching: true);
			vanillaDict[12000] = new ItemPrefab("Left plain stocking", "Simple left sock!", ItemSlot.SlotManager.StockingL, 270)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddSusModifier(SusArea.LegL, 0.5f);
			vanillaDict[12001] = new ItemPrefab("Right plain stocking", "Simple right sock!", ItemSlot.SlotManager.StockingR, 270)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddSusModifier(SusArea.LegL, 0.5f);
			vanillaDict[12500] = new ItemPrefab("Pair of stockings", "Includes both the left and and the right sock.", null, 540)
			{
				HasColor = true
			}.MakePair(ItemEnum.StockingL.ToGameId(), ItemEnum.StockingR.ToGameId(), registerAsMatching: true);
			vanillaDict[12002] = new ItemPrefab("Left lingerie stocking", "Sexy left sock!", ItemSlot.SlotManager.StockingL, 400)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.lingerieSockL).AddSusModifier(SusArea.LegL, 0.5f);
			vanillaDict[12003] = new ItemPrefab("Right lingerie stocking", "Sexy right sock!", ItemSlot.SlotManager.StockingR, 400)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.lingerieSockR).AddSusModifier(SusArea.LegR, 0.5f);
			vanillaDict[12501] = new ItemPrefab("Pair of lingerie stockings", "Includes both the left and and the right sock.", null, 800)
			{
				HasColor = true
			}.MakePair(ItemEnum.LingerieStockingL.ToGameId(), ItemEnum.LingerieStockingR.ToGameId(), registerAsMatching: true);
			vanillaDict[12004] = new ItemPrefab("Left long sock", "Very long socks. Useful for covering bot's mismatched leg.", ItemSlot.SlotManager.StockingL, 600)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.longSockL).AddSusModifier(SusArea.LegL, 1f);
			vanillaDict[12005] = new ItemPrefab("Right long sock", "Very long socks. Useful for covering bot's mismatched leg.", ItemSlot.SlotManager.StockingR, 600)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.longSockR).AddSusModifier(SusArea.LegR, 1f);
			vanillaDict[12502] = new ItemPrefab("Pair of long socks", "Very long socks. Useful for covering bot's mismatched leg. Includes both the left and and the right sock.", null, 1200)
			{
				HasColor = true
			}.MakePair(ItemEnum.LongSockL.ToGameId(), ItemEnum.LongSockR.ToGameId(), registerAsMatching: true);
			vanillaDict[12006] = new ItemPrefab("Left striped sock", "Striped sock. Popular among the Linus community.", ItemSlot.SlotManager.StockingL, 450)
			{
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().SetDefaultColorSlots(new List<string> { "Stripe color 1", "Stripe color 2" }).AddTexture(instance.stripedSockL)
				.AddSusModifier(SusArea.LegL, 0.55f);
			vanillaDict[12007] = new ItemPrefab("Right striped sock", "Striped sock. Popular among the Linus community.", ItemSlot.SlotManager.StockingR, 450)
			{
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().SetDefaultColorSlots(new List<string> { "Stripe color 1", "Stripe color 2" }).AddTexture(instance.stripedSockR)
				.AddSusModifier(SusArea.LegR, 0.55f);
			vanillaDict[12503] = new ItemPrefab("Pair of striped socks", "Striped socks. Popular among the Linus community. Includes both the left and and the right sock.", null, 900)
			{
				HasColor = true
			}.SetDefaultColorSlots(new List<string> { "Stripe color 1", "Stripe color 2" }).MakePair(ItemEnum.StripedSockL.ToGameId(), ItemEnum.StripedSockR.ToGameId(), registerAsMatching: true);
			vanillaDict[12008] = new ItemPrefab("Left stirrup stocking", "This type of stocking reveals part of the foot. For the left leg.", ItemSlot.SlotManager.StockingL, 2000)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.stirrupStockingL).AddSusModifier(SusArea.LegL, 0.5f);
			vanillaDict[12009] = new ItemPrefab("Right stirrup stocking", "This type of stocking reveals part of the foot. For the right leg.", ItemSlot.SlotManager.StockingR, 2000)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.stirrupStockingR).AddSusModifier(SusArea.LegR, 0.5f);
			vanillaDict[12504] = new ItemPrefab("Pair of stirrup stockings", "Includes both the left and and the right stirrup stocking. A type of stocking that reveals part of the foot", null, 4000)
			{
				HasColor = true
			}.MakePair(ItemEnum.StirrupStockingL.ToGameId(), ItemEnum.StirrupStockingR.ToGameId(), registerAsMatching: true);
			vanillaDict[12010] = new ItemPrefab("Left short sock", "Very short sock.", ItemSlot.SlotManager.StockingL, 300)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.shortSockL).AddSusModifier(SusArea.LegL, 0.15f);
			vanillaDict[12011] = new ItemPrefab("Right short sock", "Very short sock.", ItemSlot.SlotManager.StockingR, 300)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.shortSockR).AddSusModifier(SusArea.LegR, 0.15f);
			vanillaDict[12505] = new ItemPrefab("Pair of short socks", "Includes both the left and and the right short sock.", null, 600)
			{
				HasColor = true
			}.MakePair(ItemEnum.ShortSockL.ToGameId(), ItemEnum.ShortSockR.ToGameId(), registerAsMatching: true);
			vanillaDict[12012] = new ItemPrefab("Left two-striped stocking", "Left stocking with two stripes at the top.", ItemSlot.SlotManager.StockingL, 1000)
			{
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().SetDefaultColorSlots(new List<string> { "Main color", "Stripe color" }).AddTexture(instance.twoStripedStockingL)
				.AddSusModifier(SusArea.LegL, 0.55f);
			vanillaDict[12013] = new ItemPrefab("Right two-striped stockings", "Right stocking with two stripes at the top.", ItemSlot.SlotManager.StockingR, 1000)
			{
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().SetDefaultColorSlots(new List<string> { "Main color", "Stripe color" }).AddTexture(instance.twoStripedStockingR)
				.AddSusModifier(SusArea.LegR, 0.55f);
			vanillaDict[12506] = new ItemPrefab("Pair of two-striped stockings", "Stockings with two stripes at the top. Includes both left and right stocking.", null, 2000).SetDefaultColorSlots(new List<string> { "Main color", "Stripe color" }).MakePair(ItemEnum.TwoStripedStockingL.ToGameId(), ItemEnum.TwoStripedStockingR.ToGameId(), registerAsMatching: true);
			vanillaDict[12014] = new ItemPrefab("Left knee-high sock", "Left sock that reached below the knee.", ItemSlot.SlotManager.StockingL, 800)
			{
				Category = ItemCategory.Clothing,
				HasColor = true
			}.SetNotExpectedInShop().AddTexture(instance.kneeHighL).AddSusModifier(SusArea.LegL, 0.4f);
			vanillaDict[12015] = new ItemPrefab("Right knee-high sock", "Right sock that reached below the knee.", ItemSlot.SlotManager.StockingR, 800)
			{
				Category = ItemCategory.Clothing,
				HasColor = true
			}.SetNotExpectedInShop().AddTexture(instance.kneeHighR).AddSusModifier(SusArea.LegR, 0.4f);
			vanillaDict[12507] = new ItemPrefab("Pair of knee-high socks", "Stockings that reach below the knee. Includes both left and right stocking.", null, 1600)
			{
				HasColor = true
			}.MakePair(ItemEnum.KneeHighSockL.ToGameId(), ItemEnum.KneeHighSockR.ToGameId(), registerAsMatching: true);
			vanillaDict[11002] = new ItemPrefab("Striped panties", "Striped panties inspired by bogaloid Kitsune Piku. Now in unique colors!", ItemSlot.SlotManager.Panties, 1000)
			{
				Category = ItemCategory.Clothing
			}.AddTexture(instance.strippedPanties).SetDefaultColorSlots(new List<string> { "Main color", "Stripes color" }).AddSusModifier(SusArea.Pussy, 1f);
			vanillaDict[11001] = new ItemPrefab("Lingerie panties", "Sexy panties!", ItemSlot.SlotManager.Panties, 760)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddTexture(instance.lingeriePanties).AddSusModifier(SusArea.Pussy, 1f);
			vanillaDict[14001] = new ItemPrefab("Lingerie bra", "Sexy bra!", ItemSlot.SlotManager.Bra, 600)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddTexture(instance.lingerieBra).AddSusModifier(SusArea.Boobs, 1f);
			RegisterItemPair(ItemEnum.LingeriePanties.ToGameId(), ItemEnum.LingerieBra.ToGameId());
			vanillaDict[15000] = new ItemPrefab("Pleated skirt", "Often used by tenis players!", ItemSlot.SlotManager.LowerBody, 700)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.LowerBody, 0.7f).SetControllerElementOverride(Live2DController.SkirtId);
			vanillaDict[15002] = new ItemPrefab("Miniskirt", "Skimpy version of the usual skirt.", ItemSlot.SlotManager.LowerBody, 5000)
			{
				HasColor = true,
				Category = ItemCategory.Clothing,
				Tags = new ItemPrefabTag[1] { ItemPrefabTag.Doggy_Skirt_AlwaysUp }
			}.AddSusModifier(SusArea.LowerBody, 0.5f).AddTexture(instance.miniSkirt).SetControllerElementOverride(Live2DController.SkirtId);
			vanillaDict[13000] = new ItemPrefab("Cute bow", "Cute bow!", ItemSlot.SlotManager.HairAccessoryL, 50)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetControllerElementOverride(Live2DController.CuteBowElementId);
			vanillaDict[13027] = new ItemPrefab("Headband", "Simple headband. Looks pretty cute!", ItemSlot.SlotManager.HairAccessoryL, 600)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetControllerElementOverride(Live2DController.HeadbandId);
			vanillaDict[13001] = new ItemPrefab("Heart choker", "Choker with a heart shape in the middle.", ItemSlot.SlotManager.NeckAccessory, 300)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddTexture(instance.heartChoker);
			vanillaDict[13002] = new ItemPrefab("Heart glasses", "Fake glasses with hearts on the frame. Just for the looks.", ItemSlot.SlotManager.Glasses, 200)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddTexture(instance.heartGlasses);
			vanillaDict[13012] = new ItemPrefab("Oval glasses", "Fake glasses. Used as a fashion statement.", ItemSlot.SlotManager.Glasses, 690)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddTexture(instance.ovalGlasses);
			vanillaDict[13013] = new ItemPrefab("Colored oval glasses", "Fake glasses with a colored lens. Used as a fashion statement.", ItemSlot.SlotManager.Glasses, 3400)
			{
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(new List<string> { "Frame color", "Lens color" }).AddTexture(instance.ovalGlasses);
			vanillaDict[13014] = new ItemPrefab("Eye patch", "Used by pirates. Now available for your sexbot.", ItemSlot.SlotManager.Glasses, 2456)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.AddTexture(instance.eyePatch).SetTextureAtIndexConditionBoolModifiableVariable(0, "right", LOC.UI.TupleReferences.Eyepatch_Right).SetTextureAtIndexConditionBoolModifiableVariable(1, "left", LOC.UI.TupleReferences.Eyepatch_Left, defaultValue: false)
				.SetTextureAtIndexCondition(2, new ItemScopeConditionSpecialVariable("right"))
				.SetTextureAtIndexCondition(3, new ItemScopeConditionSpecialVariable("left"))
				.AddOnlyOneEnabledAllModifiableVariablesRequirement();
			vanillaDict[13016] = new ItemPrefab("Tongue piercing", "A circular piercing. Thanks to the use of magnets there's no need to pierce bot's tongue.", ItemSlot.SlotManager.TongueAccessory, 690)
			{
				HasColor = true,
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_Piercing
			}.AddTexture(instance.tonguePiercing);
			vanillaDict[13015] = new ItemPrefab("Glossy hair clips", "A pair of simple hair clips.", ItemSlot.SlotManager.HairAccessoryL, 666)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetControllerElementOverride(Live2DController.CuteBowElementId).AddTexture(instance.hairClips);
			vanillaDict[13017] = new ItemPrefab("Colored heart glasses", "Fake glasses with hearts on the frame. Just for the looks. Has more colors.", ItemSlot.SlotManager.Glasses, 4200)
			{
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(new List<string> { "Frame color", "Lens color", "Heart color" }).AddTexture(instance.heartGlasses);
			vanillaDict[13018] = new ItemPrefab("Manicure Set", "Full set of nails and polishes. You can color the right and left hand separately.", ItemSlot.SlotManager.Manicure, 15000)
			{
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp,
				CanChangeColor = true
			}.SetDefaultColorSlots(new List<string> { "Right hand nails color", "Left hand nails color" }).AddRequiredSlot(ItemSlot.SlotManager.ArmL).AddRequiredSlot(ItemSlot.SlotManager.ArmR)
				.AddTexture(instance.manicure);
			vanillaDict[13019] = new ItemPrefab("Pedicure Set", "Full set of nails and polishes. You can color the right and left foot separately.", ItemSlot.SlotManager.Pedicure, 16000)
			{
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp,
				CanChangeColor = true
			}.SetDefaultColorSlots(new List<string> { "Right foot nails color", "Left foot nails color" }).AddRequiredSlot(ItemSlot.SlotManager.LegL).AddRequiredSlot(ItemSlot.SlotManager.LegR)
				.AddTexture(instance.pedicure);
			vanillaDict[13020] = new ItemPrefab("Leather buckle collar", "An necklace resembling a belt. Usually for animals, but some humans like to wear it too.", ItemSlot.SlotManager.NeckAccessory, 3000)
			{
				Category = ItemCategory.Clothing,
				RandomItemPostProcessing = (Item x) =>
				{
					x.SetColor(0, ColorPaletteManager.LeatherPalette.GetRandomColor());
					x.SetColor(1, Color.white);
				}
			}.SetDefaultColorSlots(new List<string> { "Belt color", "Buckle color" }).SetControllerElementOverride(Live2DController.BellChokerElementId).AddTexture(instance.beltCollar);
			vanillaDict[13025] = new ItemPrefab("Colored buckle collar", "An necklace resembling a belt. Comes in unusual colors. Usually for animals, but some humans like to wear it too.", ItemSlot.SlotManager.NeckAccessory, 5000)
			{
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(new List<string> { "Belt color", "Buckle color" }).SetControllerElementOverride(Live2DController.BellChokerElementId).AddTexture(instance.beltCollar);
			vanillaDict[13026] = new ItemPrefab("Spiked collar", "Necklace with spikes. Originates from sheep guarding dogs.", ItemSlot.SlotManager.NeckAccessory, 4000)
			{
				Category = ItemCategory.Clothing,
				HasColor = true
			}.SetControllerElementOverride(Live2DController.BellChokerElementId).AddTexture(instance.spikedCollar);
			vanillaDict[13028] = new ItemPrefab("Navel piercing", "A circular piercing. Thanks to the use of magnets there's no need to pierce bot's skin.", ItemSlot.SlotManager.Navel, 7000)
			{
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_Piercing
			}.SetControllerElementOverride(Live2DController.NavelPiercingId);
			vanillaDict[13029] = new ItemPrefab("Right nipple piercing", "A circular piercing. Thanks to the use of strong magnets there's no need to pierce bot's skin.", ItemSlot.SlotManager.NippleLeft, 7500)
			{
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_Piercing
			}.SetControllerElementOverride(Live2DController.NippleLeftPiercingId).SetNotExpectedInShop();
			vanillaDict[13030] = new ItemPrefab("Left nipple piercing", "A circular piercing. Thanks to the use of strong magnets there's no need to pierce bot's skin.", ItemSlot.SlotManager.NippleRight, 7500)
			{
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_Piercing
			}.SetControllerElementOverride(Live2DController.NippleRightPiercingId).SetNotExpectedInShop();
			vanillaDict[13031] = new ItemPrefab("Pair of nipple piercings", "Pair of circular piercings. Thanks to the use of strong magnets there's no need to pierce bot's skin.", null, 15000).MakePair(ItemEnum.NipplePiercingL.ToGameId(), ItemEnum.NipplePiercingR.ToGameId(), registerAsMatching: true);
			vanillaDict[13003] = new ItemPrefab("Eyeshadow", "A coloured cosmetic applied to the eyelids or to the skin around the eyes to accentuate them. Comes with an assortment of colors.", ItemSlot.SlotManager.EyeMakeup, 1200)
			{
				HasColor = true,
				CanChangeColor = true,
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp
			}.AddTexture(instance.eyeShadow);
			vanillaDict[13004] = new ItemPrefab("Goth eye makeup", "Eye makeup with a goth theme. Comes with an assortment of colors.", ItemSlot.SlotManager.EyeMakeup, 1200)
			{
				HasColor = true,
				CanChangeColor = true,
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp
			}.AddTexture(instance.gothEyeMakeup);
			vanillaDict[13021] = new ItemPrefab("Eyeliner", "Eyeliner, a line beneath the eye.  Comes with an assortment of colors.", ItemSlot.SlotManager.EyeMakeup, 1400)
			{
				HasColor = true,
				CanChangeColor = true,
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp
			}.AddTexture(instance.eyeliner);
			vanillaDict[13022] = new ItemPrefab("Heart sticker", "Heart sticker you can put under the right eye. Comes with an assortment of colors.", ItemSlot.SlotManager.EyeMakeup, 700)
			{
				HasColor = true,
				CanChangeColor = true,
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp
			}.AddTexture(instance.heartEyeSticker);
			vanillaDict[13023] = new ItemPrefab("Natural eyelashes", "Eyelashes on the bottom of the eye. Comes with an assortment of colors.", ItemSlot.SlotManager.EyeMakeup, 1400)
			{
				HasColor = true,
				CanChangeColor = true,
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp
			}.AddTexture(instance.naturalEyelashes);
			vanillaDict[13005] = new ItemPrefab("Bell choker", "Choker with a ringing bell attached to it.", ItemSlot.SlotManager.NeckAccessory, 10000)
			{
				Category = ItemCategory.Clothing,
				SpecialEffectAction = (bool value, Item items) =>
				{
					if (value && items.IsEquipped())
					{
						SingletonMonoBehaviour<SoundSingleton>.Instance.Play(R.Sound.Bell_All.GetRandom().Get());
					}
				},
				ItemModiferAdditionFunction = (Live2DController c, Item item) => new Live2DController.CatBellLive2DControllerItemModifier()
			}.SetControllerElementOverride(Live2DController.BellChokerElementId);
			vanillaDict[13024] = new ItemPrefab("Simple choker", "Simple ribbon worn around the neck.", ItemSlot.SlotManager.NeckAccessory, 1630)
			{
				Category = ItemCategory.Clothing,
				HasColor = true
			}.SetControllerElementOverride(Live2DController.BellChokerElementId).AddTexture(instance.simpleChoker);
			vanillaDict[13006] = new ItemPrefab("Tiny wizard hat", "Tiny hat for a wizard!", ItemSlot.SlotManager.Head, 1000)
			{
				Category = ItemCategory.Clothing
			}.SetControllerElementOverride(Live2DController.WizardHatElementId);
			vanillaDict[13007] = new ItemPrefab("Pumpkin hairpin", "Halloween themed hairpin shaped like a pumpkin.", ItemSlot.SlotManager.HairAccessoryL, 600)
			{
				Category = ItemCategory.Clothing
			}.SetControllerElementOverride(Live2DController.CuteBowElementId).AddTexture(instance.pumpkinHairpin);
			vanillaDict[13008] = new ItemPrefab("Pointy ears", "Pointy ears just like an elf.", ItemSlot.SlotManager.Ears, 200)
			{
				Category = ItemCategory.Modules
			}.SetDefaultColorSlots(new ColorSlot[1]
			{
				new ColorSlot("Main color", ColorPaletteManager.SkinTonePalette)
			});
			vanillaDict[13009] = new ItemPrefab("Septum ring piercing", "Shaped like a ring. Piercing that goes through the bottom part of the nose between the nostrils. Thanks to magnets it doesn't need a hole!", ItemSlot.SlotManager.Septum, 250)
			{
				Category = ItemCategory.Clothing,
				HasColor = true,
				SubCategory = ItemSubCategory.Clothing_Piercing
			}.AddTexture(instance.septumPiercing);
			vanillaDict[13010] = new ItemPrefab("Septum pincher piercing", "Pincher shaped. Piercing that goes through the bottom part of the nose between the nostrils. Thanks to magnets it doesn't need a hole!", ItemSlot.SlotManager.Septum, 250)
			{
				Category = ItemCategory.Clothing,
				HasColor = true,
				SubCategory = ItemSubCategory.Clothing_Piercing
			}.AddTexture(instance.septumPiercing2);
			vanillaDict[13011] = new ItemPrefab("Lipstick", "Makes the lips pop. Single color.", ItemSlot.SlotManager.Lips, 400)
			{
				HasColor = true,
				Category = ItemCategory.Clothing,
				SubCategory = ItemSubCategory.Clothing_MakeUp
			}.AddTexture(instance.lipstick);
			vanillaDict[16000] = new ItemPrefab("Left plain sneaker", "Plain left sneaker.", ItemSlot.SlotManager.ShoeL, 500)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop();
			vanillaDict[16001] = new ItemPrefab("Right plain sneaker", "Plain right sneaker", ItemSlot.SlotManager.ShoeR, 500)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop();
			vanillaDict[16002] = new ItemPrefab("Pair of plain sneakers", "A pair of plain sneakers.", null, 1000)
			{
				HasColor = true
			}.MakePair(ItemEnum.PlainSneakerL.ToGameId(), ItemEnum.PlainSneakerR.ToGameId(), registerAsMatching: true);
			vanillaDict[1000001] = new ItemPrefab("Left sneaker", "Left sneaker WITH DRIP.", ItemSlot.SlotManager.ShoeL, 2000)
			{
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.sneakerL).SetDefaultColorSlots(new ColorSlot[3]
			{
				new ColorSlot("Base color", ColorPaletteManager.GrayScalePalette),
				new ColorSlot("Decal color 1"),
				new ColorSlot("Decal color 2")
			});
			vanillaDict[1000002] = new ItemPrefab("Right sneaker", "Right sneaker WITH DRIP", ItemSlot.SlotManager.ShoeR, 2000)
			{
				Category = ItemCategory.Clothing
			}.SetNotExpectedInShop().AddTexture(instance.sneakerR).SetDefaultColorSlots(new ColorSlot[3]
			{
				new ColorSlot("Base color", ColorPaletteManager.GrayScalePalette),
				new ColorSlot("Decal color 1"),
				new ColorSlot("Decal color 2")
			});
			vanillaDict[1000003] = new ItemPrefab("Pair of sneakers", "A pair of sneakers WITH DRIP.", null, 4000).SetDefaultColorSlots(new ColorSlot[3]
			{
				new ColorSlot("Base color", ColorPaletteManager.GrayScalePalette),
				new ColorSlot("Decal color 1"),
				new ColorSlot("Decal color 2")
			}).MakePair(ItemEnum.SneakerL.ToGameId(), ItemEnum.SneakerR.ToGameId(), registerAsMatching: true);
			vanillaDict[16003] = new ItemPrefab("Left classy shoe", "Classy left shoe.", ItemSlot.SlotManager.ShoeL, 3000)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(1, ColorPaletteManager.LeatherPalette).AddTexture(instance.classyShoeL).SetNotExpectedInShop();
			vanillaDict[16004] = new ItemPrefab("Right classy shoe", "Classy right shoe.", ItemSlot.SlotManager.ShoeR, 3000)
			{
				HasColor = true,
				Category = ItemCategory.Clothing
			}.SetDefaultColorSlots(1, ColorPaletteManager.LeatherPalette).AddTexture(instance.classyShoeR).SetNotExpectedInShop();
			vanillaDict[16005] = new ItemPrefab("Pair of classy shoes", "A pair of classy shoes.", null, 6000)
			{
				HasColor = true
			}.SetDefaultColorSlots(1, ColorPaletteManager.LeatherPalette).MakePair(ItemEnum.ClassyShoeL.ToGameId(), ItemEnum.ClassyShoeR.ToGameId(), registerAsMatching: true);
			vanillaDict[15001] = new ItemPrefab("Baggy pants", "Now WITH DRIP", ItemSlot.SlotManager.LowerBody, 4000)
			{
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.LowerBody, 0.9f).SetDefaultColorSlots(new ColorSlot[2]
			{
				new ColorSlot("Base color"),
				new ColorSlot("Belt color", ColorPaletteManager.GrayScalePalette)
			}).SetControllerElementOverride(Live2DController.PantsId)
				.AddRequiredSlot(ItemSlot.SlotManager.LegL)
				.AddRequiredSlot(ItemSlot.SlotManager.LegR);
			vanillaDict[17000] = new ItemPrefab("Baggy hoodie", "Now WITH DRIP", ItemSlot.SlotManager.UpperBodyOuter, 5000)
			{
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.UpperBody, 0.8f).AddSusModifier(SusArea.ArmL, 0.8f).AddSusModifier(SusArea.ArmR, 0.8f)
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Base color"),
					new ColorSlot("String color", ColorPaletteManager.GrayScalePalette)
				})
				.SetControllerElementOverride(Live2DController.HoodieId)
				.AddRequiredSlot(ItemSlot.SlotManager.ArmL)
				.AddRequiredSlot(ItemSlot.SlotManager.ArmR);
			vanillaDict[18000] = new ItemPrefab("Vintage dress", "Long dress with sleeves covering most of the body.", ItemSlot.SlotManager.UpperBody, 20000)
			{
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.UpperBody, 0.8f).AddSusModifier(SusArea.LowerBody, 0.8f).AddSusModifier(SusArea.LegL, 0.8f)
				.AddSusModifier(SusArea.LegR, 0.8f)
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Base color", ColorPaletteManager.GrayScalePalette),
					new ColorSlot("Secondary Color")
				})
				.AddTexture(instance.vintageDress)
				.SetControllerElementOverride(Live2DController.Dress2Id)
				.AddRequiredSlot(ItemSlot.SlotManager.ArmL)
				.AddRequiredSlot(ItemSlot.SlotManager.ArmR)
				.AddForbiddenSlot(ItemSlot.SlotManager.LowerBody)
				.AddForbiddenSlot(ItemSlot.SlotManager.UpperBodyOuter);
			vanillaDict[18001] = new ItemPrefab("Slip dress", "Slip dress with a high slit exposing the right leg. Very classy", ItemSlot.SlotManager.UpperBody, 40000)
			{
				Category = ItemCategory.Clothing
			}.AddSusModifier(SusArea.UpperBody, 0.5f).AddSusModifier(SusArea.LowerBody, 0.8f).AddSusModifier(SusArea.LegL, 0.8f)
				.AddSusModifier(SusArea.LegR, 0.1f)
				.SetDefaultColorSlots(new ColorSlot[1]
				{
					new ColorSlot("Base color")
				})
				.SetControllerElementOverride(Live2DController.Dress1Id)
				.AddForbiddenSlot(ItemSlot.SlotManager.LowerBody)
				.AddForbiddenSlot(ItemSlot.SlotManager.UpperBodyOuter);
			ItemPrefab itemPrefab = (vanillaDict[1005] = new ItemPrefab("Hypercamo hair", "Probably better than a wig." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.HairMain, 19250)
			{
				IsIllegal = true,
				HasColor = true,
				CanChangeColor = true,
				HasQuality = true,
				Category = ItemCategory.Modules
			}.SetDefaultColorSlots(new List<string> { "Main Color", "Secondary Color", "Accessory Color" }).SetControllerElementOverride("Hair0_Top", "Hair0_Front", "Hair0_FrontClippedUp", "Hair0_Side", "Hair0_Back", "Hair0_Braid", "Eyebrows_BasicEyebrows").SetElementAtIndexConditionBoolModifiableVariable(0, "Ahoge", LOC.UI.TupleReferences.Hair_Ahoge)
				.SetElementAtIndexConditionBoolModifiableVariable(1, "Front", LOC.UI.TupleReferences.Hair_Front)
				.SetElementAtIndexConditionBoolModifiableVariable(2, "FrontClippedUp", LOC.UI.TupleReferences.Hair_FrontClippedUp)
				.SetElementAtIndexConditionBoolModifiableVariable(3, "Side", LOC.UI.TupleReferences.Hair_Side)
				.SetElementAtIndexConditionBoolModifiableVariable(4, "Back", LOC.UI.TupleReferences.Hair_Back)
				.SetElementAtIndexConditionBoolModifiableVariable(5, "Braid", LOC.UI.TupleReferences.Hair_Braid)
				.AddAtLeastOneEnabledAllModifiableVariablesRequirement()
				.AddAtMostOneEnabledModifiableVariablesRequirement(new int[2] { 1, 2 })
				.SetRandomItemPostProcessing((Item item) =>
				{
					item.SetIntSpecialVariableCastBool("Front", value: true);
					item.SetIntSpecialVariableCastBool("FrontClippedUp", value: false);
				}));
			ItemPrefab itemPrefab3 = itemPrefab;
			itemPrefab = (vanillaDict[1501] = itemPrefab3.Clone().ChangeNameDescriptionAndPrice("Default hair", "Default hair used internally!", 0).SetRandomItemPostProcessing((Item item) =>
			{
				item.SetColor(0, ColorPaletteManager.JunsGrey);
				item.SetColor(1, ColorPaletteManager.JunsMagenta);
			}));
			ItemPrefab itemPrefab5 = itemPrefab;
			itemPrefab5.IsDefault = true;
			itemPrefab5.SetNotExpectedInShop();
			itemPrefab5.HasQuality = false;
			vanillaDict[200] = new ItemPrefab("Hypercamo clothier hair", "Familiar hairstyle... " + HypercamoDescriptionAppendix, ItemSlot.SlotManager.HairMain, 23000)
			{
				IsIllegal = true,
				HasColor = true,
				CanChangeColor = true,
				HasQuality = true,
				Category = ItemCategory.Modules
			}.SetDefaultColorSlots(new List<string> { "Main Color", "Secondary Color" }).SetControllerElementOverride("Hair1_Top", "Hair1_Front", "Hair1_Side", "Hair1_Back", "Eyebrows_BasicEyebrows").SetElementAtIndexConditionBoolModifiableVariable(0, "Ahoge", LOC.UI.TupleReferences.Hair_Ahoge)
				.SetElementAtIndexConditionBoolModifiableVariable(1, "Front", LOC.UI.TupleReferences.Hair_Front)
				.SetElementAtIndexConditionBoolModifiableVariable(2, "Side", LOC.UI.TupleReferences.Hair_Side)
				.SetElementAtIndexConditionBoolModifiableVariable(3, "Back", LOC.UI.TupleReferences.Hair_Back)
				.AddTexture(instance.clothierHair)
				.AddAtLeastOneEnabledAllModifiableVariablesRequirement();
			vanillaDict[202] = new ItemPrefab("Hypercamo hime cut hair", "Long hair cut in a \"Hime cut\" hairstyle. " + HypercamoDescriptionAppendix, ItemSlot.SlotManager.HairMain, 23000)
			{
				IsIllegal = true,
				HasColor = true,
				CanChangeColor = true,
				HasQuality = true,
				Category = ItemCategory.Modules
			}.SetDefaultColorSlots(new List<string> { "Main Color", "Secondary Color" }).SetControllerElementOverride("Hair3_Front", "Hair3_Side", "Hair3_Back", "Eyebrows_BasicEyebrows").SetElementAtIndexConditionBoolModifiableVariable(0, "Front", LOC.UI.TupleReferences.Hair_Front)
				.SetElementAtIndexConditionBoolModifiableVariable(1, "Side", LOC.UI.TupleReferences.Hair_Side)
				.SetElementAtIndexConditionBoolModifiableVariable(2, "Back", LOC.UI.TupleReferences.Hair_Back)
				.AddTexture(instance.himeCutHair)
				.AddAtLeastOneEnabledAllModifiableVariablesRequirement();
			vanillaDict[203] = new ItemPrefab("Hypercamo ponytail hair", "Long pony tail that attaches to the back of the hair! " + HypercamoDescriptionAppendix, ItemSlot.SlotManager.HairAdditional, 23000)
			{
				IsIllegal = true,
				HasColor = true,
				CanChangeColor = true,
				HasQuality = true,
				Category = ItemCategory.Modules
			}.SetDefaultColorSlots(new List<string> { "Main Color", "Scrunchie Color" }).SetControllerElementOverride("Hair4_Ponytail", "Hair4_Knot");
			vanillaDict[201] = new ItemPrefab("Hypercamo eyecovering bang", "Bang that covers bot's eyes." + HypercamoDescriptionAppendix, ItemSlot.SlotManager.HairAdditional, 19250)
			{
				IsIllegal = true,
				HasColor = true,
				CanChangeColor = true,
				HasQuality = true,
				Category = ItemCategory.Modules
			}.SetDefaultColorSlots(new List<string> { "Main Color", "Secondary Color" }).AddTexture(instance.eyecoveringBangHair).SetControllerElementOverride("Hair2_Front");
			vanillaDict[1201] = new ItemPrefab("Rainbow hair augmentation", "Very rare type of hair augmentation module that changes it's color over time.", ItemSlot.SlotManager.HairAugmentation, 180000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			}.SetRainbowShader(CustomShaderTypeEnum.Hair);
			vanillaDict[1203] = new ItemPrefab("Galaxy hair augmentation", "Very rare type of hair augmentation. It looks like peering into space.", ItemSlot.SlotManager.HairAugmentation, 216000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			}.SetMovingTextureShader(null, CustomShaderTypeEnum.Hair);
			vanillaDict[1204] = new ItemPrefab("Fourth dimension hair augmentation", "An extremely rare type of hair augmentation. It looks otherworldly.", ItemSlot.SlotManager.HairAugmentation, 360000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			}.SetRayMarchingShader(CustomShaderTypeEnum.Hair);
			vanillaDict[2030000] = new ItemPrefab("Basic soap", "Basic soap. Doesn't clean too well, but it's good enough for daily use. You probably don't want to know what type of fat was used in its production. Can be used when taking shower.", null, 1069)
			{
				Category = ItemCategory.Soap,
				IsStackable = true
			};
			vanillaDict[2030001] = new ItemPrefab("Colorful soap", "A better quality soap. It doesn't differ much from it's cheaper counterpart, but has one benefit - the color makes it more fun! Can be used when taking shower.", null, 4200)
			{
				Category = ItemCategory.Soap
			}.SetDefaultColorSlots(1, ColorPaletteManager.DefaultPartPalette);
			vanillaDict[2000000] = new ItemPrefab("Fishing rod", "Basic, wooden fishing rod. It gets the job done. Can only catch small fishes.", ItemSlot.SlotManager.Anon_FishingRod, 1000)
			{
				Category = ItemCategory.AnonEquip,
				FishingRodItemPrefabData = FishingRodItemPrefabData.CreateInstance(0.2f, 0.2f, 0.2f, 3f),
				AllowedModifiersData = AllowedModifiers.FishingRodAllowedModifiers
			};
			vanillaDict[2000001] = new ItemPrefab("Reinforced fishing rod", "Wooden fishing rod reinforced with steel. You can get some decent catch with this one.", ItemSlot.SlotManager.Anon_FishingRod, 10000)
			{
				Category = ItemCategory.AnonEquip,
				FishingRodItemPrefabData = FishingRodItemPrefabData.CreateInstance(0.3f, 0.3f, 0.3f, 7.5f),
				AllowedModifiersData = AllowedModifiers.FishingRodAllowedModifiers
			};
			vanillaDict[2000002] = new ItemPrefab("Fiber glass fishing rod", "Medium grade fishing rod made from fiber glass.", ItemSlot.SlotManager.Anon_FishingRod, 50000)
			{
				Category = ItemCategory.AnonEquip,
				FishingRodItemPrefabData = FishingRodItemPrefabData.CreateInstance(0.4f, 0.4f, 0.4f, 25f),
				AllowedModifiersData = AllowedModifiers.FishingRodAllowedModifiers
			};
			vanillaDict[2000003] = new ItemPrefab("Carbon fiber fishing rod", "Expensive fishing rod made from carbon fiber. Light and sturdy.", ItemSlot.SlotManager.Anon_FishingRod, 100000)
			{
				Category = ItemCategory.AnonEquip,
				FishingRodItemPrefabData = FishingRodItemPrefabData.CreateInstance(0.5f, 0.5f, 0.5f, 80f),
				AllowedModifiersData = AllowedModifiers.FishingRodAllowedModifiers
			};
			vanillaDict[2000004] = new ItemPrefab("Hyper steel fishing rod", "This fishing rod is made of exotic materials and is full of technology that allows you catch the biggest fishes. Extremely expensive.", ItemSlot.SlotManager.Anon_FishingRod, 10000000)
			{
				Category = ItemCategory.AnonEquip,
				FishingRodItemPrefabData = FishingRodItemPrefabData.CreateInstance(0.8f, 0.8f, 0.8f, 200f),
				AllowedModifiersData = AllowedModifiers.FishingRodAllowedModifiers
			};
			vanillaDict[2001000] = CreateFish("Bluegill", 0.5f, 2.2f, 0.3f, "This fish has a deep, laterally compressed body with a distinct blue coloration on its lower jaw.", FishPreferences.OmnivorousFishPreferences, FishingCatchBehaviourType.Fighter);
			vanillaDict[2001001] = CreateFish("Largemouth bass", 0.3f, 11.4f, 0.4f, "This fish features a large mouth that extends beyond its eyes and has a dark green hue with horizontal stripes.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Sinker);
			vanillaDict[2001002] = CreateFish("Northern pike", 0.3f, 28.4f, 0.5f, "With a long, slender body, this fish exhibits olive green coloration and light spots across its length.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Floater);
			vanillaDict[2001003] = CreateFish("Yellow perch", 0.5f, 1.9f, 0.6f, "This fish displays a golden-yellow body with distinct vertical black bars.", FishPreferences.OmnivorousFishPreferences, FishingCatchBehaviourType.Fighter);
			vanillaDict[2001004] = CreateFish("Walleye", 0.5f, 13f, 0.5f, "This fish is characterized by its olive to golden body and its large, reflective eyes.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Normal);
			vanillaDict[2001005] = CreateFish("Rainbow trout", 0.5f, 9f, 0.6f, "This fish is adorned with a vibrant pink stripe along its sides and small black spots scattered across its body.", FishPreferences.OmnivorousFishPreferences, FishingCatchBehaviourType.Fighter);
			vanillaDict[2001006] = CreateFish("Smallmouth bass", 0.5f, 5.4f, 0.5f, "This fish boasts a robust body with bronze coloring and vertical bands along its sides.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Sinker);
			vanillaDict[2001007] = CreateFish("Catfish", 0.05f, 200f, 0.4f, "This fish has a slender, elongated body with a deeply forked tail and whisker-like barbels around its mouth.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Normal);
			vanillaDict[2001008] = CreateFish("Brown trout", 0.5f, 3f, 0.7f, "This fish features a golden-brown body with dark spots surrounded by light halos.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Normal);
			vanillaDict[2001009] = CreateFish("Carp", 0.3f, 40f, 0.2f, "This fish displays a thick, heavy body with large scales and a bronze-gold hue.", FishPreferences.BottomFeedersAndScavengersPreferences, FishingCatchBehaviourType.Normal);
			vanillaDict[2001010] = CreateFish("Red snapper", 0.5f, 23f, 0.8f, "This fish is recognized by its red to pinkish body and prominent, triangular face.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Normal);
			vanillaDict[2001011] = CreateFish("Mackerel", 0.5f, 3.5f, 0.5f, "This fish has a streamlined body with iridescent blue-green coloring and dark wavy stripes on its back.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Normal);
			vanillaDict[2001012] = CreateFish("Flounder", 0.5f, 12f, 0.7f, "This flat fish has both eyes on one side of its head and a mottled brown body to blend with the seabed.", FishPreferences.BenthicFeedersPreferences, FishingCatchBehaviourType.Sinker);
			vanillaDict[2001013] = CreateFish("Black sea bass", 0.5f, 4f, 0.8f, "This fish features a dark, dusky color with blue and black markings on its body.", FishPreferences.BenthicFeedersPreferences, FishingCatchBehaviourType.Sinker);
			vanillaDict[2001014] = CreateFish("Halibut", 0.06f, 226f, 0.9f, "This large flatfish has a dark brown upper side and a white underside, adapted for bottom dwelling.", FishPreferences.BenthicFeedersPreferences, FishingCatchBehaviourType.Sinker);
			vanillaDict[2001015] = CreateFish("Salmon", 0.25f, 57f, 0.8f, "This fish has a streamlined, silver body with a few dark spots near its head and upper body.", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Fighter, 1.2f);
			vanillaDict[2001016] = CreateFish("Golden salmon", 0.9f, 100f, 2f, "This fish has a streamlined, golden body with a few dark spots near its head and upper body. Very rare!", FishPreferences.PredatoryFishPreferences, FishingCatchBehaviourType.Fighter, 1.5f);
			vanillaDict[2001017] = CreateFish("Tilapia", 0.5f, 4f, 0.3f, "This fish typically has a deep body with a silvery hue and vertical stripes along its sides.", FishPreferences.BottomFeedersAndScavengersPreferences, FishingCatchBehaviourType.Normal);
			vanillaDict[2003000] = CreateTrash("Old can", "Old can. The label is long gone.");
			vanillaDict[2003001] = CreateTrash("Old shoe", "It's wet and smells bad...");
			vanillaDict[2003002] = CreateTrash("Old syringe", "Disgusting old syringe.");
			itemPrefab = (vanillaDict[2003003] = CreateTrash("Old grenade", "Old grenade. It no longer works?"));
			itemPrefab.OnUse = (Item item) =>
			{
				UiOverlay.Instance.CloseUiOverlay();
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Item_Grenade);
				return true;
			};
			vanillaDict[2003004] = CreateTrash("Rusted bucket", "Rusted bucket full of holes. You aren't sure if this can even be called a bucket at this point.");
			vanillaDict[2003005] = CreateTrash("Oil drum lid", "Lid of a big oil drum. What was it doing in the water... you will never know.");
			vanillaDict[2003006] = CreateTrash("Bone fragments", "Old fragments of bone. Wet and corroded. You hope those aren't human.");
			vanillaDict[2003007] = CreateTrash("Shattered smartphone", "Old smartphone with shattered screen. Somebody wanted more lithium in the water?");
			vanillaDict[2003008] = CreateTrash("Old Dr. Sugar can", "Old can. Surprisingly, you can still read it's Dr. Sugar.");
			vanillaDict[2003009] = CreateTrash("Plastic grocery bag", "Old plastic grocery bag. Those can apparently survive for thousands of years.");
			vanillaDict[2003010] = CreateTrash("Old fishing net", "Old fishing net full of holes. No longer usable.");
			vanillaDict[2003011] = CreateTrash("Old car battery", "Soaked through and dirty, but still recognizable. Somebody really dumped this into the water?");
			vanillaDict[2002000] = CreateBait("Worm fishing bait", "Earthworm. Bait that can be used to catch fish who like worms.", 100, FishingBaitItemPrefabData.CreateInstance(FishingBaitType.WormBait, 1f));
			vanillaDict[2002001] = CreateBait("Larvae fishing bait", "Still alive and wriggling. Bait that can be used to catch fish who like insects.", 100, FishingBaitItemPrefabData.CreateInstance(FishingBaitType.InsectBait, 1f));
			vanillaDict[2002002] = CreateBait("Meat fishing bait", "Meat of questionable origins. Bait that can be used to catch fish who like meat.", 200, FishingBaitItemPrefabData.CreateInstance(FishingBaitType.MeatBait, 1f));
			vanillaDict[2002003] = CreateBait("Fish chunks bait", "Offcuts and refuse from fish butchering combined into hot dog like consistency. Bait that can be used to catch fish who like eating other fish.", 500, FishingBaitItemPrefabData.CreateInstance(FishingBaitType.FishBait, 1f));
			vanillaDict[2002004] = CreateBait("Corn bait", "Canned corn. Can has \"NOT FOR HUMAN CONSUMPTION, CONTAINS TOXIC AMOUNTS OF LEAD\" written on the label. Bait that can be used to catch fish who like plants. Cheap and effective.", 40, FishingBaitItemPrefabData.CreateInstance(FishingBaitType.PlantBait, 1f));
			vanillaDict[2002005] = CreateBait("Shrimp bait", "Low quality shrimp. Bait that can be used to catch fish who like crustacean(Mostly saltwater fish).", 2000, FishingBaitItemPrefabData.CreateInstance(FishingBaitType.CrustaceanBait, 1f));
			vanillaDict[2002006] = CreateBait("Mussels bait", "Low quality mussels. Bait that can be used to catch fish who like mollusks(Mostly saltwater fish).", 2200, FishingBaitItemPrefabData.CreateInstance(FishingBaitType.MolluskBait, 1f));
			vanillaDict[2004] = CreateFood("Energy drink", "Gives you strength to fuck (or game) all night.", 20, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Beverage, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.05f);
				nutrition.RawHealthChange.SetValue(-0.02f);
				nutrition.StaminaChange.SetValue(0.125f);
				nutrition.EatSound.SetValue(GameVariables.EatSound.CrackOpenAColdOne);
			}));
			vanillaDict[2005] = CreateFood("Protein bar", "Tasty snack. Very small portion", 5, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.2f);
				nutrition.RawHealthChange.SetValue(0.01f);
			}));
			vanillaDict[2006] = CreateFood("Chips", "*cronch cronch*", 5, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.2f);
				nutrition.RawHealthChange.SetValue(-0.01f);
			}));
			vanillaDict[2007] = CreateFood("Soda", "Sweet drink. Tastes like freedom.", 5, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Beverage, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.2f);
				nutrition.RawHealthChange.SetValue(-0.01f);
				nutrition.EatSound.SetValue(GameVariables.EatSound.CrackOpenAColdOne);
			}));
			vanillaDict[2008] = CreateFood("Sweets", "Immense sweetness. Makes your teeth hurt.", 2, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.01f);
				nutrition.RawHealthChange.SetValue(-0.01f);
				nutrition.MentalHealthChange.SetValue(0.001f);
			}));
			vanillaDict[2011] = CreateFood("Soyrent", "Balls shrivel, retract into the body. A vegan alternative to a nutritious diet.", 50, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Beverage, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.55f);
				nutrition.MaxSafeCumChange.SetValue(-1f);
				nutrition.EatSound.SetValue(GameVariables.EatSound.CrackOpenAColdOne);
			}));
			vanillaDict[2012] = CreateFood("Chicken nuggets", "Dino shaped. Tasty, but likely contain no chicken. Designed to be eaten raw, without heating up. When heated, they exhume a plastic-like smell.", 45, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.6f);
				nutrition.MentalHealthChange.SetValue(0.005f);
			}));
			vanillaDict[3000] = CreateFood("Bread from the dispenser", "Bread substitute you got from the dispenser outside. Tastes terrible, but will keep you alive.", 1, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Bread, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.5f);
				nutrition.RawHealthChange.SetValue(-0.01f);
				nutrition.MentalHealthChange.SetValue(-0.03f);
			})).SetNotExpectedInShop();
			vanillaDict[2000] = CreateFood("Can of baked beans", "Best canned food you can get. God bless the Queen.", 40, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Bean, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.6f);
				nutrition.RawHealthChange.SetValue(0.01f);
			}));
			vanillaDict[2001] = CreateFood("Pickled onions", "Opposite of soy. Pickled.", 100, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.MainVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.5f);
				nutrition.RawHealthChange.SetValue(0.01f);
				nutrition.MaxSafeCumChange.SetValue(0.25f);
			}));
			vanillaDict[2009] = CreateCookingIngredient("Taco shell", "Required to make tacos! Can be eaten raw if hungry enough.", 150, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.1f);
			}));
			vanillaDict[2010] = CreateFood("Genetically modified vegetables", "Thanks to the technology advancements they will never go bad, but will always taste like trash.", 7, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.MainVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.35f);
				nutrition.RawHealthChange.SetValue(-0.01f);
				nutrition.MentalHealthChange.SetValue(-0.01f);
			}));
			vanillaDict[2002] = CreateFood("Sausage", "Imported from Poland. Expensive and maybe with tapeworm.", 50, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Meat, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.5f);
				nutrition.RawHealthChange.SetValue(0.01f);
			}));
			vanillaDict[2003] = CreateFood("Instant noodles", "Filled with additives. Very tasty, but unhealthy.", 4, EdibleItemPrefabData.CreateInstance(EdibleTypeEnum.Pasta, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.3f);
				nutrition.RawHealthChange.SetValue(-0.01f);
			}));
			vanillaDict[2005000] = CreateCookingIngredient("Rice", "A staple food. Still widely available, but much pricier than it used to be.", 1500, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.2f);
				nutrition.RawHealthChange.SetValue(0.02f);
			}));
			vanillaDict[2005001] = CreateCookingIngredient("Potatoes", "A versatile and hearty vegetable.", 1800, new EdibleItemPrefabData(EdibleTypeEnum.MainVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.1f);
				nutrition.RawHealthChange.SetValue(0.02f);
			}));
			vanillaDict[2005002] = CreateCookingIngredient("Flour", "Essential for baking and cooking.", 1400, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.41f);
				nutrition.RawHealthChange.SetValue(0.02f);
			}));
			vanillaDict[2005003] = CreateCookingIngredient("Tomato", "Fresh tomatoes are harder to come by these days. Very important in Italian dishes.", 2500, new EdibleItemPrefabData(EdibleTypeEnum.MainVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.1f);
				nutrition.RawHealthChange.SetValue(0.02f);
			}));
			vanillaDict[2005004] = CreateCookingIngredient("Carrot", "A sweet and crunchy root vegetable. Apparently boosts your fertility.", 2000, new EdibleItemPrefabData(EdibleTypeEnum.MainVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.1f);
				nutrition.RawHealthChange.SetValue(0.02f);
				nutrition.MaxSafeCumChange.SetValue(0.5f);
			}));
			vanillaDict[2005005] = CreateCookingIngredient("Raw steak", "Real beef. So expensive many people never get to taste it's opulence.", 22000, new EdibleItemPrefabData(EdibleTypeEnum.Meat, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(1f);
				nutrition.RawHealthChange.SetValue(0.05f);
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005006] = CreateCookingIngredient("Raw chicken meat", "Very expensive for what it is.", 4000, new EdibleItemPrefabData(EdibleTypeEnum.Meat, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.5f);
				nutrition.RawHealthChange.SetValue(0.05f);
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005007] = CreateCookingIngredient("Onion", "Stinky vegetable that is essential in cooking. Apparently boosts your fertility.", 2200, new EdibleItemPrefabData(EdibleTypeEnum.FlavourVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.2f);
				nutrition.RawHealthChange.SetValue(0.02f);
				nutrition.MaxSafeCumChange.SetValue(0.5f);
			}));
			vanillaDict[2005008] = CreateCookingIngredient("Egg", "Chicken eggs. Great source of protein.", 3750, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.2f);
				nutrition.RawHealthChange.SetValue(0.02f);
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005009] = CreateCookingIngredient("Milk", "Pasteurized Milk 3% fat. Unfortunately the label doesn't say what animal it comes from.", 2500, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.3f);
				nutrition.RawHealthChange.SetValue(0.02f);
			}));
			vanillaDict[2005010] = CreateCookingIngredient("Garlic", "Garlic adds flavor to any dish.", 3000, new EdibleItemPrefabData(EdibleTypeEnum.FlavourVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.1f);
				nutrition.RawHealthChange.SetValue(0.02f);
			}));
			vanillaDict[2005011] = CreateCookingIngredient("Ginger", "Healthy and with great flavour. Must have for a lot of dishes.", 3500, new EdibleItemPrefabData(EdibleTypeEnum.FlavourVegetable, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.1f);
				nutrition.RawHealthChange.SetValue(0.05f);
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005012] = CreateCookingIngredient("Soy sauce", "A beloved condiment. Quite salty.", 1500, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005013] = CreateCookingIngredient("Vinegar", "Tangy condiment.", 1500, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.MaxSafeCumChange.SetValue(1f);
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005014] = CreateCookingIngredient("Pasta", "Useful for both soups and pasta dishes.", 1500, new EdibleItemPrefabData(EdibleTypeEnum.Pasta, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.1f);
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005015] = CreateCookingIngredient("Raw ground beef", "Ground beef. Cheaper than a proper steak.", 7000, new EdibleItemPrefabData(EdibleTypeEnum.Meat, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.5f);
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005016] = CreateCookingIngredient("Sugar", "Required for sweet dishes!", 4000, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005017] = CreateCookingIngredient("Butter", "Made from milk.", 6500, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.IsSafeToEat.SetValue(value: false);
			}));
			vanillaDict[2005018] = CreateCookingIngredient("Cheese", "Rich in flavour and very expensive", 21000, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.2f);
			}));
			vanillaDict[2005019] = CreateCookingIngredient("Yoghurt", "Apparently it's good for your gut microbiome. Can be eaten directly or cooked into dishes.", 5000, new EdibleItemPrefabData(EdibleTypeEnum.Other, (Item item, EdibleItemNutritionProfile nutrition) =>
			{
				nutrition.Satiation.SetValue(0.25f);
				nutrition.RawHealthChange.SetValue(0.05f);
			}));
			vanillaDict[2020000] = CreateDish("Beans on dispenser bread", "Simple dish. British classic", 100);
			vanillaDict[2020001] = CreateDish("Pickled onions with sausage", "Simple dish. Quite tasty.", 100);
			vanillaDict[2020002] = CreateDish("Landlord's taco", "Landlord's favourite dish. Unholy combination of a couple things you can find at a local shop...", 100);
			vanillaDict[2020003] = CreateDish("Small salad", "Simple dish made from vegetables.", 100);
			vanillaDict[2020004] = CreateDish("Simple fried fish", "Simple fried fish with no batter.", 100);
			vanillaDict[2020005] = CreateDish("Potato egg salad", "Tasty salad.", 100);
			vanillaDict[2020006] = CreateDish("Chicken noodle soup", "Staple dish. It's great!", 100);
			vanillaDict[2020007] = CreateDish("American chili", "Staple of the cuisine of the USA.", 100);
			vanillaDict[2020008] = CreateDish("Fancy carrot salad", "Great at boosting fertility.", 100);
			vanillaDict[2020009] = CreateDish("Fried rice", "Great dish. Simple to make and incredibly tasty", 100);
			vanillaDict[2020010] = CreateDish("Cooked steak", "Tasty steak!", 100);
			vanillaDict[2020011] = CreateDish("Pancakes", "Sweet and tasty. What else would you want?", 100);
			vanillaDict[2020012] = CreateDish("Fancy fried fish", "Fried fish with opulent batter.", 100);
			vanillaDict[2020013] = CreateDish("Spaghetti bolognese", "Very tasty tomato centric pasta dish.", 100);
			vanillaDict[2020014] = CreateDish("Gyros", "Greek dish. Very tasty.", 100);
			vanillaDict[2020015] = CreateDish("Omurice", "Japanese omlet filled with rice(and possibly other goodies).", 100);
			vanillaDict[2020016] = CreateDish("Spaghetti carbonara", "Rich and creamy pasta dish from Italy.", 100);
			vanillaDict[2020017] = CreateDish("Beef wellington", "Very hard to make, but amazing in taste.", 100);
			vanillaDict[2020018] = CreateDish("Sushi", "Dish that's very hard to make, but amazing in flavour.", 100);
			vanillaDict[2020020] = CreateDish("Carrot cake", "Sweet, rich and slightly orange.", 100);
			vanillaDict[2020019] = CreateDish("Sheep's curry", "Secret dish! Be happy you found it!", 100);
			itemPrefab = (vanillaDict[6] = new ItemPrefab("Cat tail", "Cat's tail. It is not cat sized. The size is adjusted to fit human sexbots.", ItemSlot.SlotManager.Tail, 14000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules,
				HasColor = true,
				ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.TailLive2DControllerItemModifier(1f)
			});
			ItemPrefab itemPrefab8 = itemPrefab;
			vanillaDict[50] = itemPrefab8.Clone().MakeHypercamo();
			itemPrefab = (vanillaDict[7] = new ItemPrefab("Cat tail different end", "Cat's tail with gradient into a second color near the end. It is not cat sized. The size is adjusted to fit human sexbots.", ItemSlot.SlotManager.Tail, 17000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules,
				HasColor = true,
				ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.TailLive2DControllerItemModifier(1f)
			}.AddTexture(instance.catTailDifferentEnd).SetDefaultColorSlots(new List<string> { "Main Color", "Tail End Color" }));
			ItemPrefab itemPrefab10 = itemPrefab;
			vanillaDict[51] = itemPrefab10.Clone().MakeHypercamo();
			itemPrefab = (vanillaDict[8] = new ItemPrefab("Fluffy tail", "Fluffy tail. Thicker compared to cat's tail.", ItemSlot.SlotManager.Tail, 25000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules,
				HasColor = true,
				ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.TailLive2DControllerItemModifier(0.8f)
			}.AddTexture(instance.tailFluffy));
			ItemPrefab itemPrefab12 = itemPrefab;
			vanillaDict[52] = itemPrefab12.Clone().MakeHypercamo();
			itemPrefab = (vanillaDict[9] = new ItemPrefab("Striped fluffy tail", "Striped fluffy tail. Inspired by raccoons! Thicker compared to cat's tail.", ItemSlot.SlotManager.Tail, 50000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules,
				HasColor = true,
				ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.TailLive2DControllerItemModifier(0.8f)
			}.AddTexture(instance.tailFluffy).AddTexture(instance.tailFluffyStripes).SetDefaultColorSlots(new List<string> { "Main Color", "Stripe Color" }));
			ItemPrefab itemPrefab14 = itemPrefab;
			vanillaDict[53] = itemPrefab14.Clone().MakeHypercamo();
			itemPrefab = (vanillaDict[1400] = new ItemPrefab("Thigh size module", "Allows you to modify the size of her thighs to some extent.", ItemSlot.SlotManager.ThighBodyModules, 200000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			});
			ItemPrefab itemPrefab16 = itemPrefab;
			ItemModifiableVariableFloat01Percent floatSizeVariable = ItemModifiableVariableFactory.CreateInstanceFloatVariable("thighSize", SimpleLocalizedString.CreateInstance(LOC.UI.TupleReferences.BodyMod_ThighSize), 0.5f);
			itemPrefab16.ItemModifiableVariables = new ItemModifiableVariable[1] { floatSizeVariable };
			itemPrefab16.ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.ParameterLive2DControllerItemModifier(new Live2DController.ParameterLive2DControllerItemModifier.Data(floatSizeVariable, (Live2DController live2DController) => (!(live2DController is ILive2DController_BodyMod_ThighSize live2DController_BodyMod_ThighSize)) ? null : live2DController_BodyMod_ThighSize.ParamThighSize));
			itemPrefab = (vanillaDict[1401] = new ItemPrefab("Calf size module", "Allows you to modify the size of her calfs to some extent.", ItemSlot.SlotManager.CalfBodyModules, 100000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			});
			ItemPrefab itemPrefab18 = itemPrefab;
			ItemModifiableVariableFloat01Percent floatSizeVariable2 = ItemModifiableVariableFactory.CreateInstanceFloatVariable("calfSize", SimpleLocalizedString.CreateInstance(LOC.UI.TupleReferences.BodyMod_CalfSize), 0.5f);
			itemPrefab18.ItemModifiableVariables = new ItemModifiableVariable[1] { floatSizeVariable2 };
			itemPrefab18.ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.ParameterLive2DControllerItemModifier(new Live2DController.ParameterLive2DControllerItemModifier.Data(floatSizeVariable2, (Live2DController live2DController) => (!(live2DController is ILive2DController_BodyMod_CalfSize live2DController_BodyMod_CalfSize)) ? null : live2DController_BodyMod_CalfSize.ParamCalfSize));
			itemPrefab = (vanillaDict[1402] = new ItemPrefab("Feet size module", "Allows you to modify the size of her feet to some extent.", ItemSlot.SlotManager.FeetBodyModules, 690420)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			});
			ItemPrefab itemPrefab20 = itemPrefab;
			ItemModifiableVariableFloat01Percent floatLengthVariable = ItemModifiableVariableFactory.CreateInstanceFloatVariable("feetLength", SimpleLocalizedString.CreateInstance(LOC.UI.TupleReferences.BodyMod_FootLength), 0.5f);
			ItemModifiableVariableFloat01Percent floatWidthVariable = ItemModifiableVariableFactory.CreateInstanceFloatVariable("feetWidth", SimpleLocalizedString.CreateInstance(LOC.UI.TupleReferences.BodyMod_FootWidth), 0.5f);
			itemPrefab20.ItemModifiableVariables = new ItemModifiableVariable[2] { floatLengthVariable, floatWidthVariable };
			itemPrefab20.ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.ParameterLive2DControllerItemModifier(new Live2DController.ParameterLive2DControllerItemModifier.Data(floatLengthVariable, (Live2DController live2DController) => (!(live2DController is ILive2DController_BodyMod_FootSize live2DController_BodyMod_FootSize)) ? null : live2DController_BodyMod_FootSize.ParamFootLength), new Live2DController.ParameterLive2DControllerItemModifier.Data(floatWidthVariable, (Live2DController live2DController) => (!(live2DController is ILive2DController_BodyMod_FootSize live2DController_BodyMod_FootSize)) ? null : live2DController_BodyMod_FootSize.ParamFootWidth));
			itemPrefab = (vanillaDict[1403] = new ItemPrefab("Boob size module", "Allows you to modify the size of her boobs to some extent.", ItemSlot.SlotManager.BoobsBodyModules, 12000000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			});
			ItemPrefab itemPrefab22 = itemPrefab;
			ItemModifiableVariableFloat01Percent floatSizeVariable3 = ItemModifiableVariableFactory.CreateInstanceFloatVariable("boobSize", SimpleLocalizedString.CreateInstance(LOC.UI.TupleReferences.BodyMod_BoobSize), 0.5f);
			itemPrefab22.ItemModifiableVariables = new ItemModifiableVariable[1] { floatSizeVariable3 };
			itemPrefab22.ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.ParameterLive2DControllerItemModifier(new Live2DController.ParameterLive2DControllerItemModifier.Data(floatSizeVariable3, (Live2DController live2DController) => (!(live2DController is ILive2DController_BodyMod_BoobSize live2DController_BodyMod_BoobSize)) ? null : live2DController_BodyMod_BoobSize.ParamBoobSize));
			itemPrefab = (vanillaDict[1404] = new ItemPrefab("Ass size module", "Allows you to modify the size of her her ass to some extent.", ItemSlot.SlotManager.AssBodyModules, 4000000)
			{
				IsIllegal = true,
				Category = ItemCategory.Modules
			});
			ItemPrefab itemPrefab24 = itemPrefab;
			ItemModifiableVariableFloat01Percent floatSizeVariable4 = ItemModifiableVariableFactory.CreateInstanceFloatVariable("assSize", SimpleLocalizedString.CreateInstance(LOC.UI.TupleReferences.BodyMod_AssSize), 0.5f);
			itemPrefab24.ItemModifiableVariables = new ItemModifiableVariable[1] { floatSizeVariable4 };
			itemPrefab24.ItemModiferAdditionFunction = (Live2DController controller, Item item) => new Live2DController.ParameterLive2DControllerItemModifier(new Live2DController.ParameterLive2DControllerItemModifier.Data(floatSizeVariable4, (Live2DController live2DController) => (!(live2DController is ILive2DController_BodyMod_AssSize live2DController_BodyMod_AssSize)) ? null : live2DController_BodyMod_AssSize.ParamAssSize));
			ItemPrefab itemById = GetItemById(ItemEnum.PlainShirt.ToGameId());
			SpriteTextureDataGenerated.OtherLogosContainer otherLogos = R.OtherLogos;
			vanillaDict[10100] = itemById.Clone().AddShirtLogoTextureAll(otherLogos.fishFearMeLogo).ChangeNameDescriptionAndPrice("Fishing T-shirt", "Cool shirt saying \"Women want me\" and \"Fish fear me\". Hip among youngsters.", itemById.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.NavyGreen);
			vanillaDict[10101] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.WithStupid).ChangeNameDescriptionAndPrice("With stupid T-shirt", "Cool shirt saying \"I'm with stupid\" and an arrow pointing right. Hip among youngsters.", itemById.Price * 2);
			vanillaDict[10102] = itemById.Clone().AddShirtLogoTextureAll(otherLogos.Temple).ChangeNameDescriptionAndPrice("Temple T-shirt", "Shirt featuring the emblem of the church.", itemById.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.TempleCyan);
			vanillaDict[10103] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.GamerTshirt).ChangeNameDescriptionAndPrice("Gamer T-shirt", "Shirt with the text \"Eat, Game, Sleep, Repeat\"", itemById.Price * 2 + 10);
			vanillaDict[10104] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.BotLogo1).ChangeNameDescriptionAndPrice("Bot T-shirt", "Shirt with a design very similar to how bot looks. Says \"かわいい\" Weird.", itemById.Price * 3)
				.SetRandomItemForcedColor(ColorPaletteManager.VeryDarkGrey);
			vanillaDict[10105] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.DogeCoin).ChangeNameDescriptionAndPrice("Dogecoin T-shirt", "Shirt with a DogeCoin design. Apparently this cryptocurrency is used to this day.", itemById.Price * 3);
			vanillaDict[10106] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.Sheep).ChangeNameDescriptionAndPrice("Sheep T-shirt", "Shirt with a pixel art sheep. Very cute.", itemById.Price * 10);
			vanillaDict[10107] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.sylphy_chibi).ChangeNameDescriptionAndPrice("Sylphy T-shirt", "Shirt featuring Sylphy from the game \"Phantom Thief Sylphy\".", itemById.Price * 2)
				.SetShillingLinkId("SyplyLink");
			vanillaDict[10117] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.HotPinkGames_Logo).ChangeNameDescriptionAndPrice("HotPink Games T-shirt", "When you can't choose between boobs and butts, just wear a butt on your boobs - HP", itemById.Price * 2)
				.SetDefaultColorSlots(new List<string> { "Main Color" }, ColorPaletteManager.HotPinkPalette)
				.SetShillingLinkId("HotPink");
			vanillaDict[10118] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.InHeatLogo).ChangeNameDescriptionAndPrice("In Heat T-shirt", "4 girls, one night, What could possibly go wrong?!", itemById.Price * 2)
				.SetShillingLinkId("InHeat")
				.SetRandomItemForcedColor(ColorPaletteManager.Black);
			vanillaDict[10119] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.DDlogo1).ChangeNameDescriptionAndPrice("Destination Delirium T-shirt", "T-shirt with logo of Destination Delirium.", itemById.Price * 2)
				.SetShillingLinkId("DestinationDelirium")
				.SetRandomItemForcedColor(ColorPaletteManager.Black);
			vanillaDict[10120] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.siluman_logo).ChangeNameDescriptionAndPrice("Siluman T-shirt", "T-shirt with logo of Siluman.", itemById.Price * 2)
				.SetShillingLinkId("Siluman");
			vanillaDict[10121] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.siluman_Alice).ChangeNameDescriptionAndPrice("Siluman Alice T-shirt", "T-shirt with a chibi of Alice from \"Siluman Fantasy\"", itemById.Price * 2)
				.SetShillingLinkId("Siluman");
			vanillaDict[10122] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.Mushroom).ChangeNameDescriptionAndPrice("Fungus T-shirt", "T-shirt with the fungus logo.", itemById.Price * 2);
			vanillaDict[10123] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.yaranaika).ChangeNameDescriptionAndPrice("Yaranaika T-shirt", "T-shirt with a bootleg rendition of famous meme. You can almost hear the \"やらないか?\".", Mathf.RoundToInt((float)itemById.Price * 2.5f));
			vanillaDict[10124] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.Baka).ChangeNameDescriptionAndPrice("Baka T-shirt", "T-shirt with text in Japanese. It says \"ばかじゃないよ！\" meaning \"I'm not an idiot!\" the \"I'm not\" part is written in a smaller font. Weird design.", Mathf.RoundToInt((float)itemById.Price * 2.5f));
			vanillaDict[10125] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.CIA).ChangeNameDescriptionAndPrice("CIA T-shirt", "T-shirt with a very abstract design.", Mathf.RoundToInt((float)itemById.Price * 2.5f));
			vanillaDict[10134] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.ufo, 1).ChangeNameDescriptionAndPrice("Alien T-shirt", "T-shirt with a weird alien.", Mathf.RoundToInt((float)itemById.Price * 2.5f))
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Main Color"),
					new ColorSlot("Alien Color")
				});
			vanillaDict[10126] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.Polandball).ChangeNameDescriptionAndPrice("Polandball T-shirt", "A t-shirt with an ugly polandball design.", Mathf.RoundToInt((float)itemById.Price * 2.5f));
			vanillaDict[10127] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.WyldSpace).ChangeNameDescriptionAndPrice("WyldSpace T-shirt", "Experience VR like never before! Struggle as hero or conspire as monster!", itemById.Price * 2)
				.SetShillingLinkId("WyldSpaceId");
			vanillaDict[10128] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.priestbot_tshirt).SetRandomItemForcedColor(Color.white)
				.ChangeNameDescriptionAndPrice("True american patriot T-shirt", "Recognized as a hate symbol in all states. Given to you as a present from the Priestbot. Not yet approved by the Vatican.", itemById.Price * 50)
				.SetNotExpectedInShop();
			vanillaDict[10129] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.mirthal).ChangeNameDescriptionAndPrice("Mirthal T-shirt", "Take the role of a young man that inherits his grandfather's house and, on a sudden twist of fate, becomes entangled with a mysterious sorceress. Explore a fantastic medieval world and meet beautiful women, exotic races, and monster girls.", itemById.Price * 2)
				.SetShillingLinkId("Mirthal");
			vanillaDict[10130] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.AGUI_Logo).SetRandomItemForcedColor(Color.white)
				.ChangeNameDescriptionAndPrice("A-GUI T-shirt", "T-shirt featuring the logo of A-GUI. Onahole that can be used as input device in lewd games.", itemById.Price * 2)
				.SetShillingLinkId("AGUI");
			vanillaDict[10131] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.Bedabots_Logo).SetDefaultColorSlots(1, ColorPaletteManager.GrayScalePalette)
				.ChangeNameDescriptionAndPrice("Bedabots T-shirt", "The best companion you'll ever have. \nLove, affection, something to hold hands with.\nA partner to warm up the cold night of winter, and to make your summers all the more hot and steamy.\nBedabots. Get yours today.", itemById.Price * 2)
				.SetShillingLinkId("Bedabots");
			vanillaDict[10132] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.projekt_melody_nbtw_logo).AddShirtLogoTextureSleeveOnly(otherLogos.projekt_melody_nbtw_69)
				.ChangeNameDescriptionAndPrice("Projekt Melody: ANBW T-shirt", "Get ready to bust some nuts and save the world with the official 'Projekt Melody: A Nut Between Worlds' t-shirt. Adds +69% perversion. May induce feelings of inadequacy to those who do not own one of their own.", itemById.Price * 2)
				.SetShillingLinkId("ProjektMelody");
			vanillaDict[10133] = itemById.Clone().AddShirtLogoTextureMainOnly(otherLogos.XoulionLogo, 1).ChangeNameDescriptionAndPrice("Xoullion T-shirt", "A T-shirt with a cute demonic logo in the middle is an excellent choice for everyday use if you want to attract a little bit of crowd attention.", itemById.Price * 3)
				.SetShillingLinkId("Xoulion")
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Main Color", ColorPaletteManager.XoulionMainPalette),
					new ColorSlot("Logo Color", ColorPaletteManager.XoulionLogoPalette)
				});
			ItemPrefab itemById2 = GetItemById(ItemEnum.PlainPanties.ToGameId());
			vanillaDict[11100] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.fishFearMeLogo).ChangeNameDescriptionAndPrice("Fishing panties", "Cool panties saying \"Women want me\" and \"Fish fear me\". Hip among youngsters.", itemById2.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.NavyGreen);
			vanillaDict[11102] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.Banana).ChangeNameDescriptionAndPrice("Banana panties", "Panties with a banana design.", itemById2.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.White);
			vanillaDict[11103] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.USP).ChangeNameDescriptionAndPrice("USB panties", "Panties with a funny design featuring USB", itemById2.Price * 2);
			vanillaDict[11104] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.yaranaika).ChangeNameDescriptionAndPrice("Yaranaika panties", "Panties with a bootleg rendition of famous meme. You can almost hear the \"やらないか?\".", itemById2.Price * 2);
			vanillaDict[11105] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.WyldSpace).ChangeNameDescriptionAndPrice("WyldSpace panties", "Experience VR like never before! Struggle as hero or conspire as monster!", itemById2.Price * 2)
				.SetShillingLinkId("WyldSpaceId");
			vanillaDict[11202] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.Avocado).ChangeNameDescriptionAndPrice("Avocado panties", "Panties with a pixel art avocado logo.", itemById2.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.AvocadoGreen);
			vanillaDict[11205] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.XoulionLogo, 1).ChangeNameDescriptionAndPrice("Xoulion panties", "Panties with a cute demonic logo.", itemById2.Price * 3)
				.SetShillingLinkId("Xoulion")
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Main Color", ColorPaletteManager.XoulionMainPalette),
					new ColorSlot("Logo Color", ColorPaletteManager.XoulionLogoPalette)
				});
			vanillaDict[11106] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.HikkeiruLogo).ChangeNameDescriptionAndPrice("Panties of harem, guilds and spices", "A stolen trophy from a world of guilds, magic, and harem, where unforgettable adventures unfold. The world awaits you...Can you face it?", itemById2.Price * 3)
				.SetRandomItemForcedColor(Color.red)
				.SetShillingLinkId("Hikkeiru");
			vanillaDict[11107] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.radioactive, 1).ChangeNameDescriptionAndPrice("Radioactive panties", "Panties with the \"radioactive\" symbol. Probably not radioactive themselves.", itemById2.Price * 2)
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Main Color"),
					new ColorSlot("Symbol Color")
				});
			ItemPrefab itemById3 = GetItemById(ItemEnum.StripedPanties.ToGameId());
			vanillaDict[11200] = itemById3.Clone().AddPantiesLogoTexture(otherLogos.Strawberry).ChangeNameDescriptionAndPrice("Strawberry striped panties", "Striped panties with a pixel art strawberry.", itemById3.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.BasicWhite);
			vanillaDict[11201] = itemById3.Clone().AddPantiesLogoTexture(otherLogos.Peach).ChangeNameDescriptionAndPrice("Peach striped panties", "Striped panties with a pixel art peach.", itemById3.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.BasicWhite);
			vanillaDict[11203] = itemById3.Clone().AddPantiesLogoTexture(otherLogos.Cherry).ChangeNameDescriptionAndPrice("Cherry striped panties", "Striped panties with a pixel art cherry.", itemById3.Price * 2)
				.SetRandomItemForcedColor(ColorPaletteManager.BasicWhite);
			vanillaDict[11204] = itemById3.Clone().AddPantiesLogoTexture(otherLogos.shcPanties).ChangeNameDescriptionAndPrice("Special harem panties", "Panties with a harem game's logo. Apparently it's modeled after a specific character's underwear. Made by a mysterious veiled woman.", itemById2.Price * 3)
				.SetRandomItemForcedColor(new Color32(75, 63, 87, 255), new Color32(178, 73, 166, 255))
				.SetShillingLinkId("SHC");
			ItemPrefab itemById4 = GetItemById(ItemEnum.BaggyHoodie.ToGameId());
			vanillaDict[17100] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.DogeCoin).ChangeNameDescriptionAndPrice("Dogecoin hoodie", "A baggy hoodie with a DogeCoin design. Apparently this cryptocurrency is used to this day.", itemById4.Price * 3);
			vanillaDict[17101] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.WeebDesign).ChangeNameDescriptionAndPrice("Weeb hoodie", "Weeb hoodie with a reference to a popular visual novel.", itemById4.Price * 3)
				.SetRandomItemForcedColor(new List<Color>
				{
					ColorPaletteManager.LunarPrincessGrey,
					ColorPaletteManager.OldDreamyCyan
				});
			vanillaDict[17102] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.BloodyMoon).ChangeNameDescriptionAndPrice("Bloody moon hoodie", "Doesn't actually have picture of the real moon. Just a bloody Japanese character 月.", itemById4.Price * 3)
				.SetRandomItemForcedColor(new List<Color>
				{
					ColorPaletteManager.VeryDarkGrey,
					new Color32(219, 34, 32, 255)
				});
			vanillaDict[17103] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.CosplayHouse).ChangeNameDescriptionAndPrice("Cosplay House hoodie", "Hoodie featuring \"Cosplay House\" game logo.", itemById4.Price * 2)
				.SetRandomItemForcedColor(new List<Color>
				{
					ColorPaletteManager.VeryDarkGrey,
					new Color32(233, 134, 174, 255)
				})
				.SetShillingLinkId("CosplayHouse");
			vanillaDict[17113] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.MILF).ChangeNameDescriptionAndPrice("Milf hunter hoodie", "A hoodie with the words\"MILF Hunter\" on them in a familiar design.", itemById4.Price * 3);
			vanillaDict[17114] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.Polandball).ChangeNameDescriptionAndPrice("Polandball hoodie", "A hoodie with an ugly polandball design.", itemById4.Price * 3);
			vanillaDict[17115] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.AGUI_Logo).SetRandomItemForcedColor(Color.white, new Color32(22, 150, 179, 255))
				.ChangeNameDescriptionAndPrice("A-GUI hoodie", "A hoodie featuring the logo of A-GUI. Onahole that can be used as input device in lewd games.", itemById4.Price * 2)
				.SetShillingLinkId("AGUI");
			vanillaDict[17116] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.shcHoodie).SetRandomItemForcedColor(ColorPaletteManager.BasicBlack, new Color32(249, 0, 219, 255))
				.ChangeNameDescriptionAndPrice("Special Harem hoodie", "Hoodie with a harem game's logo. It reminds you of five beautiful girls and their pervert teacher. Made by a mysterious veiled woman.", itemById4.Price * 3)
				.SetShillingLinkId("SHC");
			vanillaDict[17117] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.Bedabots_Logo).SetDefaultColorSlots(1, ColorPaletteManager.GrayScalePalette)
				.ChangeNameDescriptionAndPrice("Bedabots hoodie", "The best companion you'll ever have. \nLove, affection, something to hold hands with.\nA partner to warm up the cold night of winter, and to make your summers all the more hot and steamy.\nBedabots. Get yours today.", itemById4.Price * 2)
				.SetShillingLinkId("Bedabots");
			vanillaDict[17118] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.projekt_melody_nbtw_logo).SetRandomItemForcedColor(new Color32(163, 90, 192, 255), Color.white)
				.ChangeNameDescriptionAndPrice("Projekt Melody: NBW hoodie", "Get ready to bust some nuts and save the world with the official 'Projekt Melody: A Nut Between Worlds' hoodie. Adds +69% perversion. May induce feelings of inadequacy to those who do not own one of their own.", itemById4.Price * 2)
				.SetShillingLinkId("ProjektMelody");
			vanillaDict[17119] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.world_tamer_logo).SetRandomItemForcedColor(new Color32(145, 227, 255, 255), Color.white)
				.ChangeNameDescriptionAndPrice("World Tamer hoodie", "Join the world of fun with fox, bunny, wolf, and any monster girl you could think to tame with this adorable ‘World Tamer’ hoodie!", itemById4.Price * 2)
				.SetShillingLinkId("WorldTamer");
			vanillaDict[17120] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.XoulionLogo, 1).ChangeNameDescriptionAndPrice("Xoullion hoodie", "A hoodie with a cute demonic logo in the middle is an excellent choice for everyday use if you want to attract a little bit of crowd attention.", itemById.Price * 3)
				.SetShillingLinkId("Xoulion")
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Main Color", ColorPaletteManager.XoulionMainPalette),
					new ColorSlot("Logo Color", ColorPaletteManager.XoulionLogoPalette)
				});
			vanillaDict[17121] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.CELESTYN).ChangeNameDescriptionAndPrice("CELESTYN branded hoodie", "CELESTYN - NEO-Malaysia's best Breakcore producer, founder of CYNTHIA//NTWK", itemById.Price * 3)
				.SetShillingLinkId("Celestyn")
				.SetDefaultColorSlots(new ColorSlot[1]
				{
					new ColorSlot("Main Color", ColorPaletteManager.GrayScalePalette)
				});
			vanillaDict[17122] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.Flowerkidv).ChangeNameDescriptionAndPrice("FlowerKidV branded hoodie", "Elevate your style with the FlowerKidV hoodie, featuring the iconic logo of Japan’s top music producer. ", itemById.Price * 3)
				.SetShillingLinkId("FlowerKidV")
				.SetDefaultColorSlots(new ColorSlot[1]
				{
					new ColorSlot("Main Color", ColorPaletteManager.GrayScalePalette)
				});
			vanillaDict[17123] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.NutekuLogo).SetRandomItemForcedColor(ColorPaletteManager.Black, new Color32(233, 0, 59, 255))
				.ChangeNameDescriptionAndPrice("Monster Tutor hoodie", "A hoodie with a monster girl game's logo. It's given out to students at the Netherworld Academy.", itemById.Price * 3)
				.SetShillingLinkId("NutekuId");
			vanillaDict[17124] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.skull, 1).ChangeNameDescriptionAndPrice("Skull hoodie", "A hoodie with a small skull. Kind of scary.", itemById4.Price * 3)
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Main Color"),
					new ColorSlot("Skull Color")
				});
			vanillaDict[17125] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.tonis_album).ChangeNameDescriptionAndPrice("Toni S Majestic hoodie", "Hoodie with the cover of an album by Toni S!", itemById4.Price * 3)
				.SetRandomItemForcedColor(Color.white)
				.SetShillingLinkId("ToniSM");
			vanillaDict[17126] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.MadLogo).ChangeNameDescriptionAndPrice("Mad Joram hoodie", "Hoodie with the logo of Mad Joram!", itemById4.Price * 3)
				.SetShillingLinkId("Mad");
			vanillaDict[17127] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.SJ68WHITE, 1).ChangeNameDescriptionAndPrice("SJ68 hoodie", "Hoodie with the logo of SJ68.", itemById4.Price * 3)
				.SetShillingLinkId("SJ");
			vanillaDict[17128] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.LightSonic).ChangeNameDescriptionAndPrice("Light Sonic hoodie", "A hoodie with the logo of LightSonic(aka LightM).", itemById4.Price * 3)
				.SetShillingLinkId("LS");
			vanillaDict[17129] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.RottingSteelLogo).ChangeNameDescriptionAndPrice("RottingSteel hoodie", "A hoodie with the logo of RottingSteel.", itemById4.Price * 3)
				.SetShillingLinkId("RS");
			vanillaDict[17130] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.NitroriLogo).ChangeNameDescriptionAndPrice("Nitrori hoodie", "A hoodie with the logo of Nitrori.", itemById4.Price * 3)
				.SetShillingLinkId("NTR");
			vanillaDict[17131] = itemById4.Clone().AddHoodieLogoTexture(otherLogos.Shady_Corner_Logo).ChangeNameDescriptionAndPrice("Shady Corner hoodie", "A hoodie with the logo of Shady Corner.", itemById4.Price * 3)
				.SetShillingLinkId("SC");
			vanillaDict[11101] = itemById2.Clone().AddPantiesLogoTexture(otherLogos.Rose).ChangeNameDescriptionAndPrice("Luxe Rose panties", "Panties with a rose print. Produced by Luxe.", itemById2.Price * 5)
				.SetRandomItemForcedColor(ColorPaletteManager.SatinBlack)
				.SetPartnerId(CocktractPartnerPrefabManager.luxeId)
				.SetNotExpectedInShop();
			vanillaDict[11300] = GetItemById(ItemEnum.LingeriePanties.ToGameId()).Clone().AddPantiesLogoTexture(otherLogos.Bow, 1).ChangeNameDescriptionAndPrice("Luxe Lingerie panties", "Fancy panties made by Luxe. They have a pretty bow!", itemById2.Price * 5)
				.SetRandomItemForcedColor(ColorPaletteManager.SatinBlack, ColorPaletteManager.EBRed)
				.SetPartnerId(CocktractPartnerPrefabManager.luxeId)
				.SetDefaultColorSlots(new ColorSlot[2]
				{
					new ColorSlot("Main color"),
					new ColorSlot("Bow color")
				})
				.SetNotExpectedInShop();
			vanillaDict[10108] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.luxeId);
			vanillaDict[17104] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.luxeId);
			vanillaDict[10109] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.stilouId);
			vanillaDict[17105] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.stilouId);
			vanillaDict[10110] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.volksAutoId);
			vanillaDict[17106] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.volksAutoId);
			string text = " For some reason it fits onto your bot. Perhaps it was custom made.";
			vanillaDict[100] = GetItemById(ItemEnum.BasicArmL.ToGameId()).Clone().SetPartnerId(CocktractPartnerPrefabManager.volksAutoId).ChangeNameDescriptionAndPrice("VolksAuto left arm", "Prosthetic left arm produced by VolksAuto-RehabTech GmbH." + text, 30000)
				.SetNotExpectedInShop()
				.SetRandomItemForcedColor(ColorPaletteManager.VolksAutoGrey);
			vanillaDict[101] = GetItemById(ItemEnum.BasicArmR.ToGameId()).Clone().SetPartnerId(CocktractPartnerPrefabManager.volksAutoId).ChangeNameDescriptionAndPrice("VolksAuto right arm", "Prosthetic right arm produced by VolksAuto-RehabTech GmbH." + text, 30000)
				.SetNotExpectedInShop()
				.SetRandomItemForcedColor(ColorPaletteManager.VolksAutoGrey);
			vanillaDict[103] = GetItemById(ItemEnum.BasicLegL.ToGameId()).Clone().SetPartnerId(CocktractPartnerPrefabManager.volksAutoId).ChangeNameDescriptionAndPrice("VolksAuto left leg", "Prosthetic left leg produced by VolksAuto-RehabTech GmbH." + text, 30000)
				.SetNotExpectedInShop()
				.SetRandomItemForcedColor(ColorPaletteManager.VolksAutoGrey);
			vanillaDict[102] = GetItemById(ItemEnum.BasicLegR.ToGameId()).Clone().SetPartnerId(CocktractPartnerPrefabManager.volksAutoId).ChangeNameDescriptionAndPrice("VolksAuto right leg", "Prosthetic right leg produced by VolksAuto-RehabTech GmbH." + text, 30000)
				.SetNotExpectedInShop()
				.SetRandomItemForcedColor(ColorPaletteManager.VolksAutoGrey);
			vanillaDict[10111] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.monizmedId);
			vanillaDict[17107] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.monizmedId);
			vanillaDict[10112] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteTransparent);
			vanillaDict[17108] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteTransparent);
			vanillaDict[10114] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteColaPow, DiabeteCocktractsPrefab.ColaSet, "Diabete Cola Pow T-shirt", "T-shirt with logo of Cola Pow. One of the brands from the Diabete Conglomerate.");
			vanillaDict[17110] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteColaPow, DiabeteCocktractsPrefab.ColaSet, "Diabete Cola Pow hoodie", "Baggy hoodie with logo of Cola Pow. One of the brands from the Diabete Conglomerate.");
			vanillaDict[10115] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteDrSugarTrans, DiabeteCocktractsPrefab.DrSugarSet, "Diabete Dr. Sugar T-shirt", "T-shirt with logo of Dr. Sugar. One of the brands from the Diabete Conglomerate.");
			vanillaDict[17111] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteDrSugarTrans, DiabeteCocktractsPrefab.DrSugarSet, "Diabete Dr. Sugar hoodie", "Baggy hoodie with logo of Dr. Sugar. One of the brands from the Diabete Conglomerate.");
			vanillaDict[10116] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteSweetPotatoTrans, DiabeteCocktractsPrefab.SweetPotatoSet, "Diabete Sweet Potato T-shirt", "T-shirt with logo of Sweet Potato. One of the brands from the Diabete Conglomerate.");
			vanillaDict[17112] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.diabeteId, R.PartnerLogos.Alternative_DiabeteSweetPotatoTrans, DiabeteCocktractsPrefab.SweetPotatoSet, "Diabete Sweet Potato hoodie", "Baggy hoodie with logo of Sweet Potato. One of the brands from the Diabete Conglomerate.");
			vanillaDict[10113] = itemById.Clone().MakeShirtMerch(CocktractPartnerPrefabManager.cazinoId);
			vanillaDict[17109] = itemById4.Clone().MakeHoodieMerch(CocktractPartnerPrefabManager.cazinoId);
			itemPrefab = (vanillaDict[104] = GetItemById(ItemEnum.BasicArmL.ToGameId()).Clone().AddTexture(instance.experimentalArmL).ChangeNameDescriptionAndPrice("Experimental left arm", "Unusual left arm. Has less visible seams, but its quality isn't great. Looks a bit rubbery.", 15000));
			ItemPrefab itemPrefab26 = itemPrefab;
			itemPrefab = (vanillaDict[105] = GetItemById(ItemEnum.BasicArmR.ToGameId()).Clone().AddTexture(instance.experimentalArmR).ChangeNameDescriptionAndPrice("Experimental right arm", "Unusual right arm. Has less visible seams, but its quality isn't great. Looks a bit rubbery.", 15000));
			ItemPrefab itemPrefab28 = itemPrefab;
			itemPrefab = (vanillaDict[107] = GetItemById(ItemEnum.BasicLegL.ToGameId()).Clone().AddTexture(instance.experimentalLegL).ChangeNameDescriptionAndPrice("Experimental left leg", "Unusual left leg. Has less visible seams, but its quality isn't great. Looks a bit rubbery.", 15000));
			ItemPrefab itemPrefab30 = itemPrefab;
			itemPrefab = (vanillaDict[106] = GetItemById(ItemEnum.BasicLegR.ToGameId()).Clone().AddTexture(instance.experimentalLegR).ChangeNameDescriptionAndPrice("Experimental right leg", "Unusual right leg. Has less visible seams, but its quality isn't great. Looks a bit rubbery.", 15000));
			ItemPrefab itemPrefab32 = itemPrefab;
			vanillaDict[108] = itemPrefab26.Clone().MakeHypercamo();
			vanillaDict[109] = itemPrefab28.Clone().MakeHypercamo();
			vanillaDict[111] = itemPrefab30.Clone().MakeHypercamo();
			vanillaDict[110] = itemPrefab32.Clone().MakeHypercamo();
			vanillaDict[501] = GetItemById(ItemEnum.SharpTeeth.ToGameId()).Clone().MakeHypercamo();
			vanillaDict[750] = new ItemPrefab("Cyber Libido Destroyer ⚲", "This weird item significantly decreases the lust of your sexbot. Because of internal DRM it can be only used once.", null, 500000)
			{
				OnUse = (Item item) =>
				{
					int lust = GameVariables.Current.lust;
					lust /= 4;
					lust -= 200;
					GameVariables.Current.lust = Mathf.Max(1, lust);
					GameVariables.Current.Longing = 0f;
					GameVariables.Current.AddFlag("libido_destroyer");
					return true;
				},
				ThrowingOutForbidden = true,
				Category = ItemCategory.Other
			};
			vanillaDict[751] = new ItemPrefab("Cyber Libido Amplifier ⚤", "This weird item increases the lust of your sexbot. Because of internal DRM it can be only used once. USE WITH CAUTION!!!", null, 1500000)
			{
				OnUse = (Item item) =>
				{
					GameVariables.Current.lust += 200;
					GameVariables.Current.Longing = 10f;
					GameVariables.Current.AddFlag("libido_amplifier");
					return true;
				},
				ThrowingOutForbidden = true,
				Category = ItemCategory.Other
			};
			StorySingleton.AllDefinedPersonalities allDefinedPersonalities = SingletonMonoBehaviour<StorySingleton>.Instance.GetAllDefinedPersonalities();
			AddPersonalityItem(ItemEnum.TsunTsunPersonalityModule, allDefinedPersonalities.TsunTsunPersonality, "TsunTsun personality module", "Do NOT confuse with \"Tsundere\" there is no dere here. This module makes your sexbot absolutely hate your ass.", 20000);
			AddPersonalityItem(ItemEnum.CatgirlPersonalityModule, allDefinedPersonalities.CatgirlPersonality, "Catgirl personality module", "Makes her meow <b>a lot.</b>.", 20000);
			AddPersonalityItem(ItemEnum.DriverPersonalityModule, allDefinedPersonalities.DriverPersonality, "Driver personality module", "Replaces all dialogues with \"I drive.\". Highly illegal. Rumors say it gives sexbots amazing vehicle handing skills, but truth is it's just a cheap dialogue replacement module. Don't buy it.", 4000);
			AddPersonalityItem(ItemEnum.RobotPersonalityModule, allDefinedPersonalities.RobotPersonality, "Robot personality module", "Makes the dialogues more \"robotic\". Beep boops and such.", 10000, new GenericGameStatModifier(3000, (GameVariables gv, GameStats gs) =>
			{
				gs.PersonalityGameStats.ChanceToSayExactCumMlWhenSwallowing.Value = 0.7f;
			}));
			AddPersonalityItem(ItemEnum.YanderePersonalityModule, allDefinedPersonalities.YanderePersonality, "Yandere personality module", "Makes the sexbot REALLY into you!(Also crazy)", 30000);
			AddPersonalityItem(ItemEnum.SlutPersonalityModule, allDefinedPersonalities.SlutPersonality, "Slut personality module", "Not much comment needed. Makes your sexbot behave like a total slut. Pretty much addicted to cock.", 30000);
			AddPersonalityItem(ItemEnum.FemcelPersonalityModule, allDefinedPersonalities.FemcelPersonality, "Femcel personality module", "Makes the sexbot behave like a femcel. Aka. girl with no self esteem, some mental issues and extremely into you.", 15000);
			AddPersonalityItem(ItemEnum.HimederePersonalityModule, allDefinedPersonalities.HimederePersonality, "Himedere personality module", "Makes the sexbot behave like a spoiled princess. Also makes her think you are a peasant.", 15000);
			AddPersonalityItem(ItemEnum.SillyCatPersonalityModule, allDefinedPersonalities.SillyCatPersonality, "Silly Cat personality module", "Very rare. Based on a famous meme. It's the kind of cat that'd plant a pipebomb in your mailbox.", 6000000);
			AddPersonalityItem(ItemEnum.AraAraPersonalityModule, allDefinedPersonalities.AraAraPersonality, "Ara Ara personality module", "Uses \"Ara ara\" and calls you a good boy.", 30000);
			foreach (var item in _itemPrefabs.AllValuesWithIds())
			{
				var (itemGameId, _) = item;
				item.value.InitGameId(itemGameId);
			}
			vanillaDict[0] = new ItemPrefab("Empty", "Empty", null, 0).SetNotExpectedInShop();
			_IsLoaded = true;
			return UniTask.CompletedTask;
			void AddPersonalityItem(ItemEnum personalityModuleId, StorySingleton.PersonalityModuleDefinition personalityDefinition, string name, string description, int price, IGameStatModifier modifier = null)
			{
				Dictionary<int, ItemPrefab> dictionary3 = vanillaDict;
				ItemPrefab itemPrefab33 = new ItemPrefab(name, description, ItemSlot.SlotManager.PersonalityModule, price)
				{
					Category = ItemCategory.Modules
				};
				ItemPrefab itemPrefab34 = itemPrefab33;
				dictionary3[(int)personalityModuleId] = itemPrefab33;
				ItemPrefab itemPrefab35 = itemPrefab34;
				itemPrefab35.TurnIntoPersonalityModule(personalityModuleId.ToGameId(), personalityDefinition);
				if (modifier != null)
				{
					itemPrefab35.FillGameStatsModifiersAction = (Item item, List<IGameStatModifier> list) =>
					{
						list.Add(modifier);
					};
				}
			}
			static ItemPrefab CreateBait(string name, string description, int price, FishingBaitItemPrefabData baitItemPrefabData)
			{
				return new ItemPrefab(name, description, ItemSlot.SlotManager.Anon_FishingBait, price)
				{
					IsStackable = true,
					Category = ItemCategory.AnonEquip,
					FishingBaitItemPrefabData = baitItemPrefabData
				};
			}
			static ItemPrefab CreateCookingIngredient(string name, string description, int price, EdibleItemPrefabData edibleItemPrefabData)
			{
				return new ItemPrefab(name, description, null, price)
				{
					Category = ItemCategory.CookingIngredient,
					IsStackable = true,
					EdibleData = edibleItemPrefabData
				};
			}
			static ItemPrefab CreateDish(string name, string description, int price)
			{
				return new ItemPrefab(name, description, null, price)
				{
					Category = ItemCategory.FoodCookedDish,
					EdibleData = EdibleItemPrefabData.CreateInstanceDish()
				}.SetNotExpectedInShop();
			}
			static ItemPrefab CreateFish(string name, float sizeDistribution01, float maxWeight, float price01, string description, FishPreferences fishPreferences, FishingCatchBehaviourType fishingCatchBehaviourType, float minigameSpeed = 1f)
			{
				return new ItemPrefab(name, description, null, Mathf.CeilToInt(maxWeight / 2f * price01 * 400f))
				{
					Category = ItemCategory.Fish,
					FishData = FishItemPrefabData.CreateInstance(sizeDistribution01, maxWeight, fishPreferences, fishingCatchBehaviourType, minigameSpeed),
					EdibleData = new EdibleItemPrefabData(EdibleTypeEnum.Fish, (Item item, EdibleItemNutritionProfile nutrition) =>
					{
						nutrition.IsSafeToEat.Value = false;
						nutrition.Satiation.SetValue(item.FishNutrition);
						if (item.FishPollution > 0.5f)
						{
							nutrition.MaxSafeCumChange.SetValue((0f - item.FishPollution) * 10f);
							nutrition.MentalHealthChange.SetValue((0f - item.FishPollution) * 0.01f);
							nutrition.RawHealthChange.SetValue((0f - item.FishPollution) * 0.1f);
						}
					})
				}.SetNotExpectedInShop();
			}
			static ItemPrefab CreateFood(string name, string description, int price, EdibleItemPrefabData edibleItemPrefabData)
			{
				return new ItemPrefab(name, description, null, price)
				{
					Category = ItemCategory.Food,
					EdibleData = edibleItemPrefabData,
					IsStackable = true
				};
			}
			static ItemPrefab CreateTrash(string name, string description)
			{
				return new ItemPrefab(name, description, null, 0)
				{
					Category = ItemCategory.FishingTrash,
					IsStackable = true
				}.SetNotExpectedInShop();
			}
		}
	}

	private ItemModifiableVariable[] _itemModifiableVariables = Array.Empty<ItemModifiableVariable>();

	private ItemModifiableVariablesRequirement[] _itemModifiableVariablesRequirements = Array.Empty<ItemModifiableVariablesRequirement>();

	private ColorSlot[] _colorSlots = Array.Empty<ColorSlot>();

	private string[] _partners = Array.Empty<string>();

	private PackedTexturesContainer[] _textures = Array.Empty<PackedTexturesContainer>();

	private ItemPrefabTag[] _tags = Array.Empty<ItemPrefabTag>();

	private static readonly int DetailTex = Shader.PropertyToID("_DetailTex");

	private GameId? _itemGameId;

	private const int HypercamoPriceIncreaseAdd = 18000;

	private static readonly string HypercamoDescriptionAppendix = " Allows you to change the color of this part freely thanks to the use of very high-tech electrochromic materials.".TMPColorSurround(Color.green);

	public string Name
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public string Description
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public int Price
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public ItemModifiableVariable[] ItemModifiableVariables
	{
		get
		{
			return _itemModifiableVariables;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_itemModifiableVariables = value ?? Array.Empty<ItemModifiableVariable>();
		}
	}

	public ItemModifiableVariablesRequirement[] ItemModifiableVariablesRequirements
	{
		get
		{
			return _itemModifiableVariablesRequirements;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_itemModifiableVariablesRequirements = value ?? Array.Empty<ItemModifiableVariablesRequirement>();
		}
	}

	public AllowedModifiers AllowedModifiersData
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	[CanBeNull]
	public SlotEquipData SlotData
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public Func<Item, bool> OnUse
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public Func<Item, Item[]> OnUnpack
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public GameId[] OnUnpackGameIdHint
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	} = Array.Empty<GameId>();

	public Action<bool, Item> SpecialEffectAction
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public Action<Item> RandomItemPostProcessing
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public Func<Live2DController, Item, Live2DController.Live2DControllerItemModifier> ItemModiferAdditionFunction
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public Action<Item, List<IGameStatModifier>> FillGameStatsModifiersAction
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool IsIllegal
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool CanChangeColor
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool HasQuality
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool ExpectedInShop
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	} = true;

	public bool IsPersonalityModule
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool IsMissingItem
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool ThrowingOutForbidden
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool IsStove
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public ColorSlot[] ColorSlots
	{
		get
		{
			return _colorSlots;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_colorSlots = value ?? Array.Empty<ColorSlot>();
		}
	}

	public ScratchTextureType ScratchType
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool IsStackable
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public ItemCategory Category
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public ItemSubCategory SubCategory
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public string[] Partners
	{
		get
		{
			return _partners;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_partners = value ?? Array.Empty<string>();
		}
	}

	public SusModifier[] SusModifiers
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	} = Array.Empty<SusModifier>();

	public FishItemPrefabData FishData
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public FishingRodItemPrefabData FishingRodItemPrefabData
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public FishingBaitItemPrefabData FishingBaitItemPrefabData
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public EdibleItemPrefabData EdibleData
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public PackedTexturesContainer[] Textures
	{
		get
		{
			return _textures;
		}
		[MoonSharpVisible(true)]
		set
		{
			_textures = value ?? Array.Empty<PackedTexturesContainer>();
		}
	}

	public bool HasAnyTextures => Textures.Length != 0;

	public ItemPrefabTag[] Tags
	{
		get
		{
			return _tags;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_tags = value ?? Array.Empty<ItemPrefabTag>();
		}
	}

	public IReference<Sprite> OverrideIconReference { get; set; }

	public string ShillingLinkId
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public CustomShaderTypeEnum CustomShaderType
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public IReference<Shader> CustomShader
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public Action<Material, List<UnityEngine.Object>> CustomShaderMaterialPostProcessing
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public bool IsDefault
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	[Obsolete]
	private List<string> RequiredSlots
	{
		[MoonSharpVisible(true)]
		set
		{
			GetOrCreateSlotData().SetRequiredSlotsString(value);
		}
	}

	[Obsolete]
	public List<string> ForbiddenSlots
	{
		[MoonSharpVisible(true)]
		set
		{
			GetOrCreateSlotData().SetForbiddenSlotsString(value);
		}
	}

	[Obsolete]
	public Sprite OverrideIcon
	{
		set
		{
			Debug.LogError("Custom icon ignored!\n\"OverrideIcon\" is not longer supported!\nUse \"OverrideIconReference\" instead! Remember to replace GetSprite with GetSpriteReference");
		}
	}

	[Obsolete]
	private List<string> PossibleEquipmentSlots
	{
		[MoonSharpVisible(true)]
		set
		{
			SlotEquipData orCreateSlotData = GetOrCreateSlotData();
			if (value == null || value.Count == 0)
			{
				orCreateSlotData.TargetSlot = null;
			}
			else
			{
				orCreateSlotData.TargetSlotString = value[0];
			}
		}
	}

	private List<string[]> ControllerElementOverrides
	{
		[MoonSharpVisible(true)]
		set
		{
			SlotEquipData orCreateSlotData = GetOrCreateSlotData();
			if (value == null || value.Count == 0)
			{
				orCreateSlotData.ControllerElementOverride = null;
			}
			else
			{
				orCreateSlotData.ControllerElementOverride = value[0];
			}
		}
	}

	public bool Equipable
	{
		get
		{
			SlotEquipData slotEquipData;
			return IsEquipable(out slotEquipData);
		}
	}

	public bool EquipableByBot
	{
		get
		{
			if (IsEquipable(out var slotEquipData))
			{
				return ItemSlot.IsBotSlot(slotEquipData.TargetSlot.Type);
			}
			return false;
		}
	}

	public bool Usable => OnUse != null;

	public bool HasAnyModifiableVariables => _itemModifiableVariables.Length != 0;

	public bool HasColor
	{
		get
		{
			if (ColorSlots != null)
			{
				return ColorSlots.Length != 0;
			}
			return false;
		}
		[MoonSharpVisible(true)]
		private set
		{
			if (value)
			{
				if (!HasColor)
				{
					SetDefaultColorSlots(new List<string> { "Main color" });
				}
			}
			else
			{
				ColorSlots = null;
			}
		}
	}

	public ItemPrefab(string name, string description, ItemSlot possibleSlot, int price, Func<Item, bool> onUse = null)
	{
		Name = name;
		Description = description;
		Price = price;
		if (possibleSlot != null)
		{
			GetOrCreateSlotData().TargetSlot = possibleSlot;
		}
		OnUse = onUse;
	}

	public static ItemPrefab CreateDefault(ItemSlot slot, params Color[] defaultColors)
	{
		return new ItemPrefab("Default " + slot.FriendlyName, "This is used internally to handle default equipment! DO NOT USE THIS! IF YOU ADDED IT USING DEBUG DELETE THIS!", slot, 0)
		{
			HasColor = (defaultColors.Length != 0),
			ExpectedInShop = false,
			IsDefault = true
		}.SetDefaultColorSlots(defaultColors.Length).SetRandomItemForcedColor(defaultColors);
	}

	private SlotEquipData GetOrCreateSlotData()
	{
		return SlotData ?? (SlotData = new SlotEquipData());
	}

	public bool IsEquipable(out SlotEquipData slotEquipData)
	{
		slotEquipData = SlotData;
		if (SlotData != null)
		{
			return SlotData.IsValidSlot;
		}
		return false;
	}

	public bool BoolModifiableVariableChangeAllowed(Item item, int variableIndex, bool newValue)
	{
		if (!ItemModifiableVariables.TryGetValue(variableIndex, out var value) || !(value is ItemModifiableVariableIntBackedBool itemModifiableVariableIntBackedBool))
		{
			return false;
		}
		if (itemModifiableVariableIntBackedBool.GetValue(item) == newValue)
		{
			return false;
		}
		ItemModifiableVariablesRequirement[] itemModifiableVariablesRequirements = ItemModifiableVariablesRequirements;
		for (int i = 0; i < itemModifiableVariablesRequirements.Length; i++)
		{
			if (!itemModifiableVariablesRequirements[i].IsBoolChangeAllowed(item, this, variableIndex, newValue))
			{
				return false;
			}
		}
		return true;
	}

	public bool SetBoolModifiableVariableRespectRequirements(Item item, ItemModifiableVariableIntBackedBool variable, bool newValue)
	{
		int num = Array.IndexOf(ItemModifiableVariables, variable);
		if (num < 0)
		{
			return false;
		}
		if (variable.GetValue(item) == newValue)
		{
			return false;
		}
		ItemModifiableVariablesRequirement[] itemModifiableVariablesRequirements = ItemModifiableVariablesRequirements;
		for (int i = 0; i < itemModifiableVariablesRequirements.Length; i++)
		{
			if (!itemModifiableVariablesRequirements[i].IsBoolChangeAllowed(item, this, num, newValue))
			{
				return false;
			}
		}
		variable.SetValueRaw(item, newValue);
		item.EnforceAllVariableRequirements(num);
		return variable.GetValue(item) == newValue;
	}

	private void CloneCommon(ItemPrefab target)
	{
		target.ColorSlots = ColorSlots?.CloneCloneableElementArray();
		target.SusModifiers = SusModifiers?.CloneConstElementArray();
		target.Partners = Partners?.CloneConstElementArray();
		target.SlotData = SlotData?.Clone();
		target.ItemModifiableVariables = ItemModifiableVariables?.CloneCloneableElementArray();
		target.ItemModifiableVariablesRequirements = ItemModifiableVariablesRequirements?.CloneCloneableElementArray();
		target.Tags = Tags?.CloneConstElementArray();
		target.FishData = FishData?.Clone();
		target.FishingBaitItemPrefabData = FishingBaitItemPrefabData?.Clone();
		target.FishingRodItemPrefabData = FishingRodItemPrefabData?.Clone();
		target.EdibleData = EdibleData?.Clone();
		target._itemGameId = null;
	}

	public ItemPrefab Clone()
	{
		ItemPrefab itemPrefab = (ItemPrefab)MemberwiseClone();
		CloneCommon(itemPrefab);
		itemPrefab.Textures = Textures.Select((PackedTexturesContainer t) => t.Clone()).ToArray();
		return itemPrefab;
	}

	public ItemPrefab CloneWithTextures(ModCreationLive2DControllersContainer allControllers)
	{
		ItemPrefab itemPrefab = (ItemPrefab)MemberwiseClone();
		CloneCommon(itemPrefab);
		itemPrefab.Textures = GetTexturesCloneOrCreate(allControllers);
		return itemPrefab;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			foreach (PropertyInfo item in from t in GetType().GetProperties()
				where t.CanWrite && t.CanRead
				select t)
			{
				object value = item.GetValue(this);
				string text;
				if (value is IList source)
				{
					text = "[" + string.Join(", ", from object t in source
						select t?.ToString() ?? "null") + "]";
				}
				else
				{
					text = ((value == null) ? "null" : value.ToString());
				}
				stringBuilder.AppendLine(item.Name + " : " + text);
			}
			return stringBuilder.ToString();
		}
	}

	public void ToLua(string prefabVar, StringBuilder script)
	{
		script.AppendLine("local " + prefabVar + " = ModUtilities.CreateItemPrefab()");
		script.AppendLine(prefabVar + ".Name = " + Name.ToLuaString());
		script.AppendLine(prefabVar + ".Description = " + Description.ToLuaString());
		script.AppendLine(prefabVar + ".Price = " + Price.ToLuaString());
		if (AllowedModifiersData != null)
		{
			script.AppendLine(prefabVar + ".AllowedModifiersData = " + AllowedModifiersData.ToLuaString());
		}
		if (SlotData != null)
		{
			script.AppendLine(prefabVar + ".SlotData = " + SlotData.ToLuaString());
		}
		if (ItemModifiableVariables.Length != 0)
		{
			script.AppendLine(prefabVar + ".ItemModifiableVariables = " + ItemModifiableVariables.ToLuaStringTransferable());
		}
		if (ItemModifiableVariablesRequirements.Length != 0)
		{
			script.AppendLine(prefabVar + ".ItemModifiableVariablesRequirements = " + ItemModifiableVariablesRequirements.ToLuaStringTransferable());
		}
		script.AppendLine(prefabVar + ".IsIllegal = " + IsIllegal.ToLuaString());
		script.AppendLine(prefabVar + ".HasQuality = " + HasQuality.ToLuaString());
		script.AppendLine(prefabVar + ".ThrowingOutForbidden = " + ThrowingOutForbidden.ToLuaString());
		if (IsPersonalityModule)
		{
			script.AppendLine(prefabVar + ".IsPersonalityModule = " + LuaUtilities.ToLuaString(value: true));
		}
		script.AppendLine(prefabVar + ".IsStackable = " + IsStackable.ToLuaString());
		script.AppendLine(prefabVar + ".Category = " + Category.ToLuaString());
		if (SubCategory != ItemSubCategory.Default)
		{
			script.AppendLine(prefabVar + ".SubCategory = " + SubCategory.ToLuaString());
		}
		script.AppendLine(prefabVar + ".CanChangeColor = " + CanChangeColor.ToLuaString());
		script.AppendLine(prefabVar + ".ColorSlots = " + ColorSlots.ToLuaStringTransferable());
		script.AppendLine(prefabVar + ".Partners = " + Partners.ToLuaString());
		script.AppendLine(prefabVar + ".ScratchType = " + ScratchType.ToLuaString());
		script.AppendLine(prefabVar + ".SusModifiers = " + SusModifiers.ToLuaStringTransferable());
		if (FishData != null)
		{
			script.AppendLine(prefabVar + ".FishData = " + FishData.ToLuaString());
		}
		if (FishingRodItemPrefabData != null)
		{
			script.AppendLine(prefabVar + ".FishingRodItemPrefabData = " + FishingRodItemPrefabData.ToLuaString());
		}
		if (EdibleData != null)
		{
			script.AppendLine(prefabVar + ".EdibleData = " + EdibleData.ToLuaString());
		}
		if (FishingBaitItemPrefabData != null)
		{
			script.AppendLine(prefabVar + ".FishingBaitItemPrefabData = " + FishingBaitItemPrefabData.ToLuaString());
		}
	}

	public bool IsMadeByPartner(string partnerId)
	{
		if (Partners == null)
		{
			return false;
		}
		return Partners.Contains(partnerId);
	}

	public bool CanGoInto(string slot)
	{
		if (IsEquipable(out var slotEquipData))
		{
			return slotEquipData.TargetSlot.Id == slot;
		}
		return false;
	}

	public bool CanGoInto(ItemSlot slot)
	{
		if (IsEquipable(out var slotEquipData))
		{
			return slotEquipData.TargetSlot == slot;
		}
		return false;
	}

	public bool HasTag(ItemPrefabTag tag)
	{
		return Tags.Contains(tag);
	}

	public ItemPrefab ChangeNameDescriptionAndPrice(string name, string description, int price)
	{
		Name = name;
		Description = description;
		Price = price;
		return this;
	}

	public ItemPrefab SetDefaultColorSlots(List<string> colorSlots, ColorPaletteManager.ColorPalette palette = null)
	{
		if (palette == null)
		{
			palette = ColorPaletteManager.DefaultPartPalette;
		}
		ColorSlots = colorSlots.Select((string t) => ColorSlot.CreateInstance(t, palette)).ToArray();
		return this;
	}

	public ItemPrefab SetDefaultColorSlots(int count, ColorPaletteManager.ColorPalette palette = null)
	{
		if (palette == null)
		{
			palette = ColorPaletteManager.DefaultPartPalette;
		}
		ColorSlots = (from t in Enumerable.Range(0, count)
			select ColorSlot.CreateInstance($"Color {t}", palette)).ToArray();
		return this;
	}

	public ItemPrefab SetDefaultColorSlots(ColorSlot[] colorSlots)
	{
		ColorSlots = colorSlots;
		return this;
	}

	public ItemPrefab MakePair(GameId i1, GameId i2)
	{
		return MakeSet(new GameId[2] { i1, i2 });
	}

	public ItemPrefab MakePair(GameId i1, GameId i2, bool registerAsMatching)
	{
		if (registerAsMatching)
		{
			ItemPrefabManager.RegisterItemPair(i1, i2);
		}
		return MakeSet(new GameId[2] { i1, i2 });
	}

	public ItemPrefab MakeSet(GameId[] id)
	{
		Category = ItemCategory.Package;
		Description += "\nThis item is still in its packaging. You have to unpack it first!".TMPColorSurround(Color.green);
		OnUnpack = (Item i) => Enumerable.Select(id, i.UnpackClone).ToArray();
		OnUnpackGameIdHint = id.ToArray();
		return this;
	}

	public ItemPrefab AddSusModifier(SusArea area, float cover)
	{
		SusModifiers = SusModifiers.AppendToArrayCopy(SusModifier.CreateInstance(area, cover));
		return this;
	}

	public ItemPrefab AddTexture(SpecialTextureHolder holder)
	{
		foreach (PackedTexturesContainer texture in holder.textures)
		{
			AddTexture(texture);
		}
		return this;
	}

	public ItemPrefab AddModdableLogos(IEnumerable<(string name, DrawableTypeEnum drawableTypeEnum)> drawables, IReference<Sprite> spriteReference, int colorIndex = -1, int layer = 0)
	{
		List<PackedTexturesContainer.PackedDrawable> list = new List<PackedTexturesContainer.PackedDrawable>();
		Rect rect = spriteReference.GetResource().rect;
		RectInt rectInt = new RectInt((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
		foreach (var drawable in drawables)
		{
			list.Add(new PackedTexturesContainer.PackedDrawable(drawable.name, rectInt, colorIndex, drawable.drawableTypeEnum, bypassColorScaler: false));
		}
		PackedTexturesContainer packedTexturesContainer = PackedTexturesContainer.CreateEmpty();
		packedTexturesContainer.PackedTextures = new List<PackedTexturesContainer.PackedTexture>
		{
			new PackedTexturesContainer.PackedTexture
			{
				Layer = layer,
				Targets = Live2DControllerSingleton.AllValidControllersEnums.Select((ControllerEnum t) => t.GetControllerId()).ToArray(),
				RuntimeTextureReference = spriteReference.ConvertToTexture2D(),
				PackedDrawables = list
			}
		};
		Textures = Textures.AppendToArrayCopy(packedTexturesContainer);
		return this;
	}

	public ItemPrefab AddPantiesLogoTexture(IReference<Sprite> textureReference, int colorIndex = -1, int layer = 0)
	{
		return AddModdableLogos(new (string, DrawableTypeEnum)[1] { ("ModdablePantiesLogo", DrawableTypeEnum.Logo) }, textureReference, colorIndex, layer);
	}

	public ItemPrefab AddHoodieLogoTexture(IReference<Sprite> textureReference, int colorIndex = -1, int layer = 0)
	{
		return AddModdableLogos(new (string, DrawableTypeEnum)[1] { ("ModdableHoodieLogo", DrawableTypeEnum.Logo) }, textureReference, colorIndex, layer);
	}

	public ItemPrefab AddShirtLogoTextureAll(IReference<Sprite> textureReference, int colorIndex = -1, int layer = 0)
	{
		return AddModdableLogos(new (string, DrawableTypeEnum)[3]
		{
			("ModdableShirtLogo", DrawableTypeEnum.Logo),
			("ModdableShirtLeftSleeveLogo", DrawableTypeEnum.Logo),
			("ModdableShirtRightSleeveLogo", DrawableTypeEnum.Logo)
		}, textureReference, colorIndex, layer);
	}

	public ItemPrefab AddShirtLogoTextureAllMirroredSleeve(IReference<Sprite> textureReference, int colorIndex = -1, int layer = 0)
	{
		return AddModdableLogos(new (string, DrawableTypeEnum)[3]
		{
			("ModdableShirtLogo", DrawableTypeEnum.Logo),
			("ModdableShirtLeftSleeveLogo", DrawableTypeEnum.Logo),
			("ModdableShirtRightSleeveLogo", DrawableTypeEnum.LogoReflectHorizontally)
		}, textureReference, colorIndex, layer);
	}

	public ItemPrefab AddShirtLogoTextureMainOnly(IReference<Sprite> textureReference, int colorIndex = -1, int layer = 0)
	{
		return AddModdableLogos(new (string, DrawableTypeEnum)[1] { ("ModdableShirtLogo", DrawableTypeEnum.Logo) }, textureReference, colorIndex, layer);
	}

	public ItemPrefab AddShirtLogoTextureSleeveOnly(IReference<Sprite> textureReference, int colorIndex = -1, int layer = 0)
	{
		return AddModdableLogos(new (string, DrawableTypeEnum)[2]
		{
			("ModdableShirtLeftSleeveLogo", DrawableTypeEnum.Logo),
			("ModdableShirtRightSleeveLogo", DrawableTypeEnum.Logo)
		}, textureReference, colorIndex, layer);
	}

	public ItemPrefab AddTexture(ControllerEnum target, PackedTexturesContainer packed)
	{
		if ((object)packed == null)
		{
			Debug.LogError("Trying to add a null texture!");
			return this;
		}
		packed.AddAllTexturesTarget(target.GetControllerId());
		if (!Textures.Contains(packed))
		{
			Textures = Textures.AppendToArrayCopy(packed);
		}
		return this;
	}

	public ItemPrefab AddTexture(PackedTexturesContainer packed)
	{
		if ((object)packed == null)
		{
			Debug.LogError("Trying to add a null texture!");
			return this;
		}
		if (!Textures.Contains(packed))
		{
			Textures = Textures.AppendToArrayCopy(packed);
		}
		return this;
	}

	public ItemPrefab SetRandomItemPostProcessing(Action<Item> randomItemPostprocessing)
	{
		RandomItemPostProcessing = randomItemPostprocessing;
		return this;
	}

	public ItemPrefab SetRandomItemForcedColor(IList<Color> randomItemColors)
	{
		RandomItemPostProcessing = (Item item) =>
		{
			if (item.GetItemPrefab().ColorSlots != null)
			{
				item.SetColors(randomItemColors);
			}
		};
		return this;
	}

	public ItemPrefab SetRandomItemForcedColor(params Color[] randomItemColors)
	{
		return SetRandomItemForcedColor((IList<Color>)randomItemColors);
	}

	public static void SetBrandColor(Item item, CocktractPartnerPrefab.BrandColorsSet colorSet)
	{
		ColorSlot[] colorSlots = item.GetItemPrefab().ColorSlots;
		List<Color> list = colorSet.GetBrandColors(colorSlots.Length).ToList();
		for (int i = 0; i < colorSlots.Length && i < list.Count; i++)
		{
			item.SetColor(i, list[i]);
		}
	}

	public static void SetBrandColor(Item item)
	{
		CocktractPartnerPrefab.BrandColorsSet randomBrandColorsSet = CocktractPartnerPrefabManager.GetCocktractsCompanyPrefab(item.GetItemPrefab().Partners.GetRandom()).GetRandomBrandColorsSet();
		SetBrandColor(item, randomBrandColorsSet);
	}

	public ItemPrefab SetPartnerBrandColorForcedColor()
	{
		if (Partners.Length == 0)
		{
			return this;
		}
		RandomItemPostProcessing = SetBrandColor;
		return this;
	}

	public ItemPrefab SetBrandColorSetForcedColor(CocktractPartnerPrefab.BrandColorsSet forcedBrandColorsSet = null)
	{
		RandomItemPostProcessing = (Item item) =>
		{
			SetBrandColor(item, forcedBrandColorsSet);
		};
		return this;
	}

	public ItemPrefab SetShillingLinkId(string linkId)
	{
		ShillingLinkId = linkId;
		return this;
	}

	public ItemPrefab SetNotExpectedInShop()
	{
		ExpectedInShop = false;
		return this;
	}

	public ItemPrefab SetPartnerId(string partnerId)
	{
		Partners = new string[1] { partnerId };
		return this;
	}

	public ItemPrefab MakeShirtMerch(string partnerId, IReference<Sprite> forcedLogo = null, CocktractPartnerPrefab.BrandColorsSet forcedBrandColorsSet = null, string name = null, string description = null)
	{
		CocktractPartnerPrefab cocktractsCompanyPrefab = CocktractPartnerPrefabManager.GetCocktractsCompanyPrefab(partnerId);
		AddShirtLogoTextureMainOnly(forcedLogo ?? cocktractsCompanyPrefab.LogoSprite);
		string valueProjectLocale = cocktractsCompanyPrefab.Name.GetValueProjectLocale();
		ChangeNameDescriptionAndPrice(name ?? (valueProjectLocale + " Merch T-Shirt"), description ?? ("Shirt with " + valueProjectLocale + "'s logo."), Price);
		SetPartnerId(partnerId);
		SetNotExpectedInShop();
		if (forcedBrandColorsSet == null)
		{
			SetPartnerBrandColorForcedColor();
		}
		else
		{
			SetBrandColorSetForcedColor(forcedBrandColorsSet);
		}
		return this;
	}

	public ItemPrefab MakeHoodieMerch(string partnerId, IReference<Sprite> forcedLogo = null, CocktractPartnerPrefab.BrandColorsSet forcedBrandColorsSet = null, string name = null, string description = null)
	{
		CocktractPartnerPrefab cocktractsCompanyPrefab = CocktractPartnerPrefabManager.GetCocktractsCompanyPrefab(partnerId);
		AddHoodieLogoTexture(forcedLogo ?? cocktractsCompanyPrefab.LogoSprite);
		string valueProjectLocale = cocktractsCompanyPrefab.Name.GetValueProjectLocale();
		ChangeNameDescriptionAndPrice(name ?? (valueProjectLocale + " Merch Hoodie"), description ?? ("Baggy hoodie with " + valueProjectLocale + "'s logo."), Price);
		SetPartnerId(partnerId);
		SetNotExpectedInShop();
		if (forcedBrandColorsSet == null)
		{
			SetPartnerBrandColorForcedColor();
		}
		else
		{
			SetBrandColorSetForcedColor(forcedBrandColorsSet);
		}
		return this;
	}

	public ItemPrefab AddRequiredSlot(string slot)
	{
		GetOrCreateSlotData().AddRequiredSlotString(slot);
		return this;
	}

	public ItemPrefab AddRequiredSlot(ItemSlot slot)
	{
		GetOrCreateSlotData().RequiredSlots.Add(slot);
		return this;
	}

	private ItemPrefab AddForbiddenSlot(string slot)
	{
		GetOrCreateSlotData().AddForbiddenSlotString(slot);
		return this;
	}

	private ItemPrefab AddForbiddenSlot(ItemSlot slot)
	{
		GetOrCreateSlotData().ForbiddenSlots.Add(slot);
		return this;
	}

	public ItemPrefab SetCustomShader(IReference<Shader> shader, Action<Material, List<UnityEngine.Object>> customShaderPostProcessing = null, CustomShaderTypeEnum customShaderType = CustomShaderTypeEnum.RelatedDrawables)
	{
		CustomShaderType = customShaderType;
		CustomShader = shader;
		if (customShaderPostProcessing != null)
		{
			CustomShaderMaterialPostProcessing = customShaderPostProcessing;
		}
		return this;
	}

	public ItemPrefab SetRainbowShader(CustomShaderTypeEnum customShaderType = CustomShaderTypeEnum.RelatedDrawables)
	{
		return SetCustomShader(R.Live2DShaders.Rainbow, null, customShaderType);
	}

	public ItemPrefab SetRayMarchingShader(CustomShaderTypeEnum customShaderType = CustomShaderTypeEnum.RelatedDrawables)
	{
		return SetCustomShader(R.Live2DShaders.RayMarching, null, customShaderType);
	}

	public ItemPrefab MakeHypercamo()
	{
		CanChangeColor = true;
		Price = Mathf.RoundToInt((float)(Price + 18000) * 1.5f);
		Name = "Hypercamo " + Name.FirstCharToLower();
		Description += HypercamoDescriptionAppendix;
		return this;
	}

	public void TurnIntoPersonalityModule(GameId id, StorySingleton.PersonalityModuleDefinition personalityDefinition)
	{
		IsPersonalityModule = true;
		IsIllegal = true;
		personalityDefinition.AddItem(id);
	}

	public ItemPrefab SetMovingTextureShader(IReference<Texture2D> textureReference = null, CustomShaderTypeEnum shaderType = CustomShaderTypeEnum.RelatedDrawables)
	{
		if (textureReference == null)
		{
			textureReference = R.OtherLogos.galaxy.ConvertToTexture2D();
		}
		return SetCustomShader(R.Live2DShaders.MovingTexture, (Material m, List<UnityEngine.Object> required) =>
		{
			Texture2D resource = textureReference.GetResource();
			m.SetTexture(DetailTex, resource);
			required.Add(resource);
		}, shaderType);
	}

	public IEnumerable<ItemSlot> GetRequiredSlots(string equippedSlot)
	{
		if (ItemSlot.SlotManager.TryGetSlot(equippedSlot, out var itemSlot))
		{
			foreach (ItemSlot item in itemSlot.Needed)
			{
				yield return item;
			}
		}
		if (SlotData == null || SlotData.RequiredSlots.Count == 0)
		{
			yield break;
		}
		foreach (ItemSlot requiredSlot in SlotData.RequiredSlots)
		{
			yield return requiredSlot;
		}
	}

	public int GetRequiredSlotCount(string equippedSlot)
	{
		int num = 0;
		if (ItemSlot.SlotManager.TryGetSlot(equippedSlot, out var itemSlot))
		{
			num += itemSlot.Needed.Count;
		}
		if (SlotData == null)
		{
			return num;
		}
		return num + SlotData.RequiredSlots.Count;
	}

	public IEnumerable<ItemSlot> GetForbiddenSlots(string equippedSlot)
	{
		if (SlotData == null || SlotData.ForbiddenSlots.Count == 0)
		{
			return Array.Empty<ItemSlot>();
		}
		return SlotData.ForbiddenSlots;
	}

	public int GetForbiddenSlotCount(string equippedSlot)
	{
		if (SlotData == null)
		{
			return 0;
		}
		return SlotData.ForbiddenSlots.Count;
	}

	public ItemPrefab SetControllerElementOverride(params string[] element)
	{
		GetOrCreateSlotData().ControllerElementOverride = element;
		return this;
	}

	public ItemPrefab SetSlotAndOverride(ItemSlot slot, params string[] element)
	{
		SlotEquipData orCreateSlotData = GetOrCreateSlotData();
		orCreateSlotData.TargetSlot = slot;
		orCreateSlotData.ControllerElementOverride = element;
		return this;
	}

	public ItemPrefab AddBoolModifiableVariable(string Id, SimpleLocalizedString simpleLocalizedString, bool defaultValue)
	{
		ItemModifiableVariables = ItemModifiableVariables.AppendToArrayCopy(ItemModifiableVariableFactory.CreateInstanceBoolBackedVariable(Id, simpleLocalizedString, defaultValue));
		return this;
	}

	public ItemPrefab AddItemModifiableVariablesRequirement(ItemModifiableVariablesRequirement itemModifiableVariablesRequirement)
	{
		ItemModifiableVariablesRequirements = ItemModifiableVariablesRequirements.AppendToArrayCopy(itemModifiableVariablesRequirement);
		return this;
	}

	public ItemPrefab AddAtLeastOneEnabledAllModifiableVariablesRequirement()
	{
		int[] array = new int[ItemModifiableVariables.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = i;
		}
		return AddAtLeastOneEnabledModifiableVariablesRequirement(array);
	}

	public ItemPrefab AddOnlyOneEnabledAllModifiableVariablesRequirement()
	{
		int[] array = new int[ItemModifiableVariables.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = i;
		}
		return AddOnlyOneEnabledModifiableVariablesRequirement(array);
	}

	public ItemPrefab AddOnlyOneEnabledModifiableVariablesRequirement(int[] indices)
	{
		EnsureValidModifiableVariableIndices(indices);
		return AddItemModifiableVariablesRequirement(ExactlyOneBoolVariableTrueRequirement.CreateInstance(indices));
	}

	public ItemPrefab AddAtMostOneEnabledModifiableVariablesRequirement(int[] indices)
	{
		EnsureValidModifiableVariableIndices(indices);
		return AddItemModifiableVariablesRequirement(AtMostOneBoolVariableTrueRequirement.CreateInstance(indices));
	}

	public ItemPrefab AddAtLeastOneEnabledModifiableVariablesRequirement(int[] indices)
	{
		EnsureValidModifiableVariableIndices(indices);
		return AddItemModifiableVariablesRequirement(AtLeastOneBoolVariableTrueRequirement.CreateInstance(indices));
	}

	private void EnsureValidModifiableVariableIndices(int[] indices)
	{
		foreach (int num in indices)
		{
			if (!ItemModifiableVariables.IsInRange(num))
			{
				Debug.LogError($"Index {num} out of range in \"{Name}\"!!!");
			}
		}
	}

	public bool HasAtLeastOneBoolVariableTrueRequirement()
	{
		ItemModifiableVariablesRequirement[] itemModifiableVariablesRequirements = ItemModifiableVariablesRequirements;
		for (int i = 0; i < itemModifiableVariablesRequirements.Length; i++)
		{
			if (itemModifiableVariablesRequirements[i] is AtLeastOneBoolVariableTrueRequirement)
			{
				return true;
			}
		}
		return false;
	}

	public ItemPrefab SetElementAtIndexCondition(int index, ItemScopeCondition condition)
	{
		SlotEquipData orCreateSlotData = GetOrCreateSlotData();
		orCreateSlotData.ElementActiveConditions = orCreateSlotData.ElementActiveConditions.SetValueAtIndexCopy(index, condition);
		return this;
	}

	public ItemPrefab SetTextureAtIndexCondition(int index, ItemScopeCondition condition)
	{
		SlotEquipData orCreateSlotData = GetOrCreateSlotData();
		orCreateSlotData.TextureActiveConditions = orCreateSlotData.TextureActiveConditions.SetValueAtIndexCopy(index, condition);
		return this;
	}

	public ItemPrefab SetElementAtIndexConditionBoolModifiableVariable(int index, string variableId, SimpleLocalizedString variableSls, bool defaultValue = true)
	{
		SlotEquipData orCreateSlotData = GetOrCreateSlotData();
		ItemModifiableVariables = ItemModifiableVariables.AppendToArrayCopy(ItemModifiableVariableFactory.CreateInstanceBoolBackedVariable(variableId, variableSls, defaultValue));
		orCreateSlotData.ElementActiveConditions = orCreateSlotData.ElementActiveConditions.SetValueAtIndexCopy(index, new ItemScopeConditionSpecialVariable(variableId));
		return this;
	}

	public ItemPrefab SetTextureAtIndexConditionBoolModifiableVariable(int index, string variableId, SimpleLocalizedString variableSls, bool defaultValue = true)
	{
		SlotEquipData orCreateSlotData = GetOrCreateSlotData();
		ItemModifiableVariables = ItemModifiableVariables.AppendToArrayCopy(ItemModifiableVariableFactory.CreateInstanceBoolBackedVariable(variableId, variableSls, defaultValue));
		orCreateSlotData.TextureActiveConditions = orCreateSlotData.TextureActiveConditions.SetValueAtIndexCopy(index, new ItemScopeConditionSpecialVariable(variableId));
		return this;
	}

	private PackedTexturesContainer[] GetTexturesCloneOrCreate(ModCreationLive2DControllersContainer allControllers)
	{
		bool flag = Textures.Length != 0;
		Dictionary<ControllerEnum, List<PackedTexturesContainer>> dictionary = Textures.ToDictionaryMultipleKeys((PackedTexturesContainer x) => x.GetAffectedControllers(), (PackedTexturesContainer x) => x);
		List<PackedTexturesContainer> list;
		using (MyCollectionPool<PackedTexturesContainer>.GetList(out list))
		{
			if (flag)
			{
				list.AddRange(Textures.Select((PackedTexturesContainer t) => t.Clone()));
			}
			HashSet<string> hashSet;
			using (MyCollectionPool<string>.GetHashSet(out hashSet))
			{
				foreach (Live2DController controller in allControllers.Controllers)
				{
					hashSet.Clear();
					string controllerId = controller.ControllerEnum.GetControllerId();
					if (dictionary.TryGetValue(controller.ControllerEnum, out var value) && value.Count > 0)
					{
						foreach (PackedTexturesContainer item in value)
						{
							foreach (PackedTexturesContainer.PackedTexture packedTexture in item.PackedTextures)
							{
								if (packedTexture.HasTarget(controllerId) && (packedTexture.DontIncludeVanillaLayers || packedTexture.Layer == 0))
								{
									hashSet.AddRange(packedTexture.PackedDrawables.Select((PackedTexturesContainer.PackedDrawable x) => x.Name));
								}
							}
						}
					}
					if (!IsEquipable(out var slotEquipData))
					{
						continue;
					}
					List<string> list2;
					using (MyCollectionPool<string>.GetList(out list2))
					{
						slotEquipData.FillActiveElementsDefault(list2);
						foreach (string item2 in list2)
						{
							Live2DController.Live2DControllerElement elementById = controller.GetElementById(item2);
							if (elementById == null)
							{
								continue;
							}
							List<Live2DController.Part> list3;
							using (MyCollectionPool<Live2DController.Part>.GetList(out list3))
							{
								foreach (Live2DController.Part item3 in elementById.AllPartsNonSegmented(controller))
								{
									if (!hashSet.Contains(item3.FullName))
									{
										list3.Add(item3);
									}
								}
								if (list3.Count > 0)
								{
									PackedTexturesContainer packedTexturesContainer = SingletonMonoBehaviour<MeshUtilitiesManager>.Instance.PackRenderers(list3, separateTextures: false, 0);
									if (packedTexturesContainer != null)
									{
										list.Add(packedTexturesContainer);
									}
								}
							}
						}
					}
				}
				return list.ToArray();
			}
		}
	}

	public Sprite GetIcon()
	{
		if (AutomaticReferenceExtensions.TryGet(OverrideIconReference, out var resource))
		{
			return resource;
		}
		ItemSubCategory subCategory = SubCategory;
		if (subCategory != ItemSubCategory.Default && subCategory == ItemSubCategory.Modules_CatEars)
		{
			return R.Icons.cat_ears.Get();
		}
		switch (Category)
		{
		case ItemCategory.Modules:
		{
			if (IsEquipable(out var slotEquipData3))
			{
				ItemSlot targetSlot3 = slotEquipData3.TargetSlot;
				if (targetSlot3 == ItemSlot.SlotManager.LegL || targetSlot3 == ItemSlot.SlotManager.LegR)
				{
					return R.Icons.leg.Get();
				}
				if (targetSlot3 == ItemSlot.SlotManager.ArmL || targetSlot3 == ItemSlot.SlotManager.ArmR)
				{
					return R.Icons.arm.Get();
				}
				if (targetSlot3 == ItemSlot.SlotManager.Tail)
				{
					return R.Icons.tail.Get();
				}
				if (targetSlot3 == ItemSlot.SlotManager.PersonalityModule)
				{
					return R.Icons.chip.Get();
				}
			}
			return R.Icons.parts.Get();
		}
		case ItemCategory.Clothing:
		{
			if (IsEquipable(out var slotEquipData2))
			{
				ItemSlot targetSlot2 = slotEquipData2.TargetSlot;
				if (targetSlot2 == ItemSlot.SlotManager.StockingL || targetSlot2 == ItemSlot.SlotManager.StockingR)
				{
					return R.Icons.socks.Get();
				}
				if (targetSlot2 == ItemSlot.SlotManager.Bra)
				{
					return R.Icons.bra.Get();
				}
				if (targetSlot2 == ItemSlot.SlotManager.ShoeL || targetSlot2 == ItemSlot.SlotManager.ShoeR)
				{
					return R.Icons.shoes.Get();
				}
				if (targetSlot2 == ItemSlot.SlotManager.UpperBody)
				{
					List<string> elements;
					using (slotEquipData2.FillActiveElementsDefaultPooled(out elements))
					{
						if (elements.Contains(Live2DController.ShirtId))
						{
							return R.Icons.shirt.Get();
						}
						if (elements.Contains(Live2DController.Dress2Id) || elements.Contains(Live2DController.Dress1Id))
						{
							return R.Icons.dress.Get();
						}
					}
				}
				else
				{
					if (targetSlot2 == ItemSlot.SlotManager.UpperBodyOuter)
					{
						return R.Icons.hoodie.Get();
					}
					if (targetSlot2 == ItemSlot.SlotManager.Panties)
					{
						return R.Icons.thong.Get();
					}
					if (targetSlot2 == ItemSlot.SlotManager.LowerBody)
					{
						List<string> elements2;
						using (slotEquipData2.FillActiveElementsDefaultPooled(out elements2))
						{
							if (elements2.Contains(Live2DController.SkirtId))
							{
								return R.Icons.skirt.Get();
							}
							if (elements2.Contains(Live2DController.PantsId))
							{
								return R.Icons.pants.Get();
							}
						}
					}
					else
					{
						if (targetSlot2 == ItemSlot.SlotManager.Glasses)
						{
							return R.Icons.glasses.Get();
						}
						if (targetSlot2 == ItemSlot.SlotManager.NeckAccessory)
						{
							return R.Icons.necklace.Get();
						}
					}
				}
			}
			return R.Icons.stuff.Get();
		}
		case ItemCategory.Food:
			return R.Icons.food.Get();
		case ItemCategory.Fish:
			return R.Icons.fish.Get();
		case ItemCategory.CookingIngredient:
			return R.Icons.ingredient.Get();
		case ItemCategory.Usable:
			return R.Icons.special.Get();
		case ItemCategory.Package:
			return R.Icons.gift.Get();
		case ItemCategory.Meds:
			return R.Icons.medicine.Get();
		case ItemCategory.FoodCookedDish:
			return R.Icons.dish_1.Get();
		case ItemCategory.Room:
			return R.Icons.special.Get();
		case ItemCategory.FishingTrash:
			return R.Icons.Icon_trash.Get();
		case ItemCategory.AnonEquip:
		{
			if (IsEquipable(out var slotEquipData))
			{
				ItemSlot targetSlot = slotEquipData.TargetSlot;
				if (targetSlot == ItemSlot.SlotManager.Anon_FishingRod)
				{
					return R.Icons.fishing_rod.Get();
				}
				if (targetSlot == ItemSlot.SlotManager.Anon_FishingBait)
				{
					return R.Icons.bait.Get();
				}
			}
			break;
		}
		case ItemCategory.Soap:
			return R.Icons.Icon_Soap.Get();
		}
		return R.Icons.stuff.Get();
	}

	public void ReloadTextures(StringBuilder logSb)
	{
		for (int i = 0; i < Textures.Length; i++)
		{
			logSb?.AppendLine($"Packed texture {i}:");
			logSb?.AppendLine();
			Textures[i].ReloadTexturesFromFiles(logSb);
		}
	}

	[MoonSharpHidden]
	public void InitGameId(GameId itemGameId)
	{
		_itemGameId = itemGameId;
	}

	public bool TryGetItemGameId(out GameId gameId)
	{
		if (_itemGameId.HasValue)
		{
			gameId = _itemGameId.Value;
			return true;
		}
		gameId = default;
		return false;
	}
}
