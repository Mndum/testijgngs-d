using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public sealed class EquipmentSet : ISerializationCallbackReceiver
{
	public static string EquipmentSetExtension = ".mdrgset";

	[SerializeField]
	public string Name;

	[SerializeField]
	private List<Item> EquippedItems;

	[SerializeField]
	private List<SerializableModInfoStub> UsedMods;

	public string SafeFilename
	{
		get
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				return $"set{DateTime.Now.Ticks}";
			}
			return GlobalFunctions.ReplaceInvalidChars(Name) + EquipmentSetExtension;
		}
	}

	public EquipmentSet()
	{
		EquippedItems = new List<Item>();
		UsedMods = new List<SerializableModInfoStub>();
	}

	public EquipmentSet(string name, List<Item> equippedItems)
	{
		Name = name;
		EquippedItems = equippedItems;
		UsedMods = new List<SerializableModInfoStub>();
		PrepareModInfo();
	}

	public static EquipmentSet GenerateFromCurrent(string name)
	{
		IEnumerable<Item> items = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.BotEquippedItems();
		return GenerateFrom(name, items);
	}

	public static EquipmentSet GenerateFrom(string name, IEnumerable<Item> items)
	{
		return new EquipmentSet(name, (from t in items
			select (t.Clone(), t.GetItemPrefab().GetRequiredSlotCount(t.EquippedSlot)) into t
			orderby t.Item2 descending
			select t.Item1).ToList());
	}

	public void OnBeforeSerialize()
	{
		PrepareModInfo();
	}

	public void OnAfterDeserialize()
	{
		PrepareModInfo();
	}

	private void PrepareModInfo()
	{
		HashSet<Guid> hashSet;
		using (MyCollectionPool<Guid>.GetHashSet(out hashSet))
		{
			foreach (Item equippedItem in EquippedItems)
			{
				GameId gameId = equippedItem.GameId;
				if (gameId.IsModded)
				{
					hashSet.Add(gameId.Guid);
				}
			}
			ModsSingleton instance = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance;
			foreach (Guid moddedGuid in hashSet)
			{
				bool flag = instance.LocalModInfo.TryGetLocalModInfo(moddedGuid, out var lmi);
				int num = UsedMods.FindIndex((SerializableModInfoStub x) => x.ModGuid.RealGuid == moddedGuid);
				if (num >= 0)
				{
					if (flag)
					{
						UsedMods[num].ModName = lmi.ModName;
					}
				}
				else
				{
					UsedMods.Add(new SerializableModInfoStub(moddedGuid, flag ? lmi.ModName : ""));
				}
			}
			for (int num2 = UsedMods.Count - 1; num2 >= 0; num2--)
			{
				if (!hashSet.Contains(UsedMods[num2].ModGuid))
				{
					UsedMods.RemoveAtSwapBack(num2);
				}
			}
		}
	}

	public void DeleteSlot(string slot)
	{
		EquippedItems.RemoveAll((Item x) => x.EquippedSlot == slot);
	}

	public string GetSummary()
	{
		if (EquippedItems.Count == 0)
		{
			return LOC.UI.Equipment_SetEmpty;
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			ModsSingleton instance = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance;
			if (UsedMods.Count > 0)
			{
				stringBuilder.AppendLine(LOC.UI.Equipment_SetUsedMods);
				int num = 0;
				foreach (SerializableModInfoStub usedMod in UsedMods)
				{
					Guid realGuid = usedMod.ModGuid.RealGuid;
					Mod modByGuid = instance.GetModByGuid(realGuid);
					if (modByGuid != null)
					{
						stringBuilder.AppendColorTagStart(ModStatusExtensions.StatusToColor(modByGuid.Status));
						stringBuilder.AppendLine(LOC.UI.Equipment_SetModGood(realGuid, modByGuid.Name));
						stringBuilder.Append(LOC.UI.Mods_Status);
						stringBuilder.Append(" ");
						stringBuilder.AppendLine(ModStatusExtensions.StatusToName(modByGuid.Status));
						if (ModStatusExtensions.IsStatusActive(modByGuid.Status))
						{
							num++;
						}
					}
					else
					{
						stringBuilder.AppendColorTagStart(MyColor32Presets.Red);
						SerializableLocalModInfo lmi;
						if (!string.IsNullOrEmpty(usedMod.ModName))
						{
							stringBuilder.AppendLine(LOC.UI.Equipment_SetModMissing(realGuid, usedMod.ModName));
						}
						else if (instance.LocalModInfo.TryGetLocalModInfo(realGuid, out lmi))
						{
							usedMod.ModName = lmi.ModName;
							stringBuilder.AppendLine(LOC.UI.Equipment_SetModMissing(realGuid, lmi.ModName));
						}
						else
						{
							stringBuilder.AppendLine(LOC.UI.Equipment_SetModUnknown(realGuid));
						}
					}
					stringBuilder.AppendColorTagEnd();
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
				}
				stringBuilder.AppendLine(LOC.UI.Equipment_SetUsedModsStatus(num, UsedMods.Count));
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine(LOC.UI.Equipment_SetListHeader);
			foreach (Item equippedItem in EquippedItems)
			{
				equippedItem.GetNameStringBuilder(stringBuilder, ItemNameIncludeEnum.DefaultNoColorNoFavourite);
				stringBuilder.Append(" (");
				stringBuilder.TMPAppendColorGradient(LOC.Polyglot.MENU_CUSTOMIZE_COLOR, equippedItem.GetColors());
				stringBuilder.Append(")");
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
	}

	public string GetShortSummary()
	{
		return LOC.UI.Equipment_SetSummaryShort(EquippedItems.Count, EquippedItems.Sum((Item t) => t.GetPrice()));
	}

	public Result Equip(bool resetFirst = false)
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			Item.ItemManager itemManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager;
			using (itemManager.DelayCallbacks("EquipmentSet"))
			{
				if (resetFirst)
				{
					itemManager.UnequipAllBotItems();
				}
				List<Item> list;
				using ((from t in itemManager.AllItemsReadonlyCollection()
					where t.GetItemPrefab().Equipable
					select t).ToListPooled(out list))
				{
					List<Item> list2;
					using (MyCollectionPool<Item>.GetList(out list2))
					{
						List<Item> list3;
						using (EquippedItems.ToListPooled(out list3))
						{
							HashSet<string> hashSet;
							using (MyCollectionPool<string>.GetHashSetAndFill(EquippedItems.SelectMany((Item t) => from x in t.GetItemPrefab().GetRequiredSlots(t.EquippedSlot)
								select x.Id), out hashSet))
							{
								int num = 1;
								int num2 = 100;
								while (list3.Count > 0)
								{
									if (num > num2)
									{
										stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingIterationLimitReached(num2));
										list2.AddRange(list3);
										list3.Clear();
										break;
									}
									stringBuilder.Append(LOC.UI.Equipment_SetEquippingIteration(num));
									stringBuilder.AppendLine();
									bool flag = false;
									for (int num3 = list3.Count - 1; num3 >= 0; num3--)
									{
										Item targetItem = list3[num3];
										Item item = null;
										item = list.FirstOrDefault(targetItem.Equals_UniqueGuid);
										if (item == null || item.GameId != targetItem.GameId)
										{
											item = null;
											if (targetItem.GetItemPrefab().HasQuality)
											{
												item = list.FirstOrDefault((Item t) => t.Equals_IdAndEqualsColorsAndQuality(targetItem));
												if (item == null)
												{
													stringBuilder.AppendLine(LOC.UI.Equipment_SetEquipping1);
												}
											}
											if (item == null)
											{
												item = list.FirstOrDefault((Item t) => t.Equals_IdAndColors(targetItem));
												if (item == null)
												{
													stringBuilder.AppendLine(LOC.UI.Equipment_SetEquipping2);
													item = list.FirstOrDefault((Item t) => t.GameId == targetItem.GameId);
													if (item == null && (hashSet.Contains(targetItem.EquippedSlot) || ItemSlot.SlotManager.GetSlotType(targetItem.EquippedSlot) == ItemSlot.SlotType.BotPart))
													{
														stringBuilder.AppendLine(LOC.UI.Equipment_SetEquipping3);
														item = list.FirstOrDefault((Item t) => t.GetItemPrefab().CanGoInto(targetItem.EquippedSlot));
													}
												}
											}
										}
										if (item != null)
										{
											if (itemManager.Equip(item, ItemSlot.SlotManager.GetSlot(targetItem.EquippedSlot), forceRemoveForbidden: true, stringBuilder) == Item.ItemManager.EquipResults.Success)
											{
												list3.RemoveAt(num3);
												flag = true;
												stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingAs(item.GetName(), targetItem.GetName()));
												ItemPrefab itemPrefab = item.GetItemPrefab();
												if (itemPrefab.CanChangeColor)
												{
													stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingChangingColor(item.GetName()));
													ColorSlot[] colorSlots = item.GetItemPrefab().ColorSlots;
													for (int num4 = 0; num4 < colorSlots.Length; num4++)
													{
														item.SetColor(num4, targetItem.GetColor(num4));
													}
												}
												if (itemPrefab.ItemModifiableVariables.Length != 0)
												{
													ItemModifiableVariable[] itemModifiableVariables = itemPrefab.ItemModifiableVariables;
													foreach (ItemModifiableVariable itemModifiableVariable in itemModifiableVariables)
													{
														if (!(itemModifiableVariable is ItemModifiableVariableIntBackedBool itemModifiableVariableIntBackedBool))
														{
															if (itemModifiableVariable is ItemModifiableVariableFloat01Percent itemModifiableVariableFloat01Percent)
															{
																float value = itemModifiableVariableFloat01Percent.GetValue(targetItem);
																itemModifiableVariableFloat01Percent.SetValue(item, value);
																stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingCopiedFloatVariable(value, itemModifiableVariable.GetLocalizedName()));
															}
															else
															{
																stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingCannotCopyVariable);
															}
														}
														else
														{
															bool value2 = itemModifiableVariableIntBackedBool.GetValue(targetItem);
															itemModifiableVariableIntBackedBool.SetValueRaw(item, value2);
															stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingCopiedBooleanVariable(value2 ? LOC.UI.SpecialItemVariableEnabled : LOC.UI.SpecialItemVariableDisabled, itemModifiableVariable.GetLocalizedName()));
														}
													}
													item.EnforceAllVariableRequirements();
												}
											}
											else
											{
												stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingCannotEquip(item.GetName()));
											}
										}
										else
										{
											list3.RemoveAt(num3);
											stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingMissing(targetItem.GetName()));
											list2.Add(targetItem);
										}
										stringBuilder.AppendLine();
									}
									stringBuilder.AppendLine();
									if (!flag)
									{
										break;
									}
									num++;
								}
								stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingFinished);
								if (list2.Count != 0)
								{
									stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingDroppedListHeader);
									foreach (Item item2 in list2)
									{
										stringBuilder.AppendLine("-" + item2.GetName());
									}
								}
								if (list3.Count != 0)
								{
									stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingLeftListHeader);
									foreach (Item item3 in list3)
									{
										stringBuilder.AppendLine("-" + item3.GetName());
									}
								}
								if (list3.Count == 0 && list2.Count == 0)
								{
									stringBuilder.AppendLine(LOC.UI.Equipment_SetEquippingAllItemsFound);
								}
								return Result.SuccessWithDescription(stringBuilder.ToString());
							}
						}
					}
				}
			}
		}
	}

	public bool IsCurrentlyEquipped()
	{
		List<Item> list = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.BotEquippedItems().ToList();
		if (list.Count != EquippedItems.Count)
		{
			return false;
		}
		for (int num = EquippedItems.Count - 1; num >= 0; num--)
		{
			Item targetItem = EquippedItems[num];
			if (targetItem.GetItemPrefab().HasQuality)
			{
				int num2 = list.FindIndex((Item t) => t.Equals_IdAndEqualsColorsAndQuality(targetItem));
				if (num2 < 0)
				{
					return false;
				}
				list.RemoveAt(num2);
			}
			else
			{
				int num3 = list.FindIndex((Item t) => t.Equals_IdAndColors(targetItem));
				if (num3 < 0)
				{
					return false;
				}
				list.RemoveAt(num3);
			}
		}
		return true;
	}
}
