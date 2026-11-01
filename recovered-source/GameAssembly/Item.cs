using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GameItems;
using JetBrains.Annotations;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

[Serializable]
public sealed class Item : SpecialVariablesHolder, ISerializationCallbackReceiver, ISelectable
{
	public class QualityDescriptor
	{
		public float MinQuality;

		public SimpleLocalizedString SimpleLocalizedString;

		public QualityDescriptor(float minQuality, SimpleLocalizedString simpleLocalizedString)
		{
			MinQuality = minQuality;
			SimpleLocalizedString = simpleLocalizedString;
		}
	}

	[Serializable]
	public class AdditionalDataHolder
	{
		public List<AdditionalDataSlot> slots = new List<AdditionalDataSlot>();
	}

	[Serializable]
	public class AdditionalDataSlot
	{
		[FormerlySerializedAs("SlotName")]
		public string SlotId;

		public string Data;

		public Color GetDataAsColor()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(Data))
				{
					throw new Exception("Additional slot is empty and cannot be parsed as Color!");
				}
				return JsonUtility.FromJson<Color>(Data);
			}
			catch (Exception message)
			{
				Debug.LogWarning("Additional data \"" + Data + "\" in slot \"" + SlotId + "\" cannot be parsed as a Color! Using default color: Color.White");
				Debug.LogWarning(message);
				return Color.white;
			}
		}

		public AdditionalDataSlot Clone()
		{
			return (AdditionalDataSlot)MemberwiseClone();
		}
	}

	public enum ItemLocationEnum
	{
		AtHome,
		Carried,
		BorrowedFromShop
	}

	[Flags]
	public enum ItemUIAllowedButtons
	{
		None = 0,
		EquipUnequip = 1,
		Eat = 2,
		Use = 4,
		Unpack = 8,
		Modify = 0x10,
		ThrowOut = 0x20,
		All = -1
	}

	[Serializable]
	public sealed class ItemManager : GameVariablesSubClass, ISerializationCallbackReceiver
	{
		public enum EquipResults
		{
			Success,
			FailedBlocked,
			FailedMissingRequired,
			FailedNonEquipable
		}

		[Flags]
		public enum ItemManagerCallbacks
		{
			None = 0,
			UnequippedItemsModified = 1,
			EquippedItemsModified = 2
		}

		public class DelayItemManagerCallback : IDisposable
		{
			public string Source { get; private set; }

			public void FillData(string source)
			{
				Source = source;
			}

			public void Dispose()
			{
				GameVariables.Current.itemManager.RemoveCallbackDelay(this);
			}
		}

		public enum ItemChangeType
		{
			Added,
			Removed,
			Unequip,
			Equip,
			OrderChange,
			InnerDataChange
		}

		public class ItemPairContainer : IDisposable
		{
			public List<Item> Left;

			public List<Item> Right;

			public bool Valid
			{
				get
				{
					if (Left != null)
					{
						return Right != null;
					}
					return false;
				}
			}

			public bool TryGetRandomPairMatchColor(out Item i1, out Item i2)
			{
				i1 = (i2 = null);
				if (!Valid || Left.Count == 0 || Right.Count == 0)
				{
					return false;
				}
				for (int j = 0; j < 100; j++)
				{
					int index = UnityEngine.Random.Range(0, Left.Count);
					i1 = Left[index];
					int num = Right.LowerBound(i1, ItemSortColor.Comparer);
					if (num < Right.Count && ItemSortColor.Comparer.Compare(Right[num], i1) == 0)
					{
						int num2 = Right.UpperBound(i1, ItemSortColor.Comparer) - num;
						if (num2 > 0)
						{
							int index2 = num + UnityEngine.Random.Range(0, num2);
							i2 = Right[index2];
							return true;
						}
					}
				}
				return false;
			}

			public (Item i1, Item i2) GetRandomPair()
			{
				if (!Valid || Left.Count == 0 || Right.Count == 0)
				{
					return (i1: null, i2: null);
				}
				int index = UnityEngine.Random.Range(0, Left.Count);
				int index2 = UnityEngine.Random.Range(0, Right.Count);
				return (i1: Left[index], i2: Right[index2]);
			}

			public void Prepare()
			{
				Left.Sort(ItemSortColor.Comparer);
				Right.Sort(ItemSortColor.Comparer);
				if (Left.Count > Right.Count)
				{
					List<Item> right = Right;
					List<Item> left = Left;
					Left = right;
					Right = left;
				}
			}

			public void Dispose()
			{
				MyCollectionPool<Item>.ReleaseAndSetNull(ref Left);
				MyCollectionPool<Item>.ReleaseAndSetNull(ref Right);
			}

			public override string ToString()
			{
				string[] array = new string[5] { "(L:", null, null, null, null };
				List<Item> left = Left;
				array[1] = ((left != null) ? left.ToStringBetter() : null) ?? "null";
				array[2] = " R:";
				List<Item> right = Right;
				array[3] = ((right != null) ? right.ToStringBetter() : null) ?? "null";
				array[4] = ")";
				return string.Concat(array);
			}
		}

		private Dictionary<string, List<Item>> slotToEquippedItems = new Dictionary<string, List<Item>>();

		private List<Item> allEquippedItems = new List<Item>();

		private List<Item> botEquippedItems = new List<Item>();

		private Dictionary<GameId, List<Item>> gameIdToEquippedItems = new Dictionary<GameId, List<Item>>();

		private Dictionary<GameId, List<Item>> itemsById = new Dictionary<GameId, List<Item>>();

		private static BetterObjectPool<DelayItemManagerCallback> _delayedCallbackPool = new BetterObjectPool<DelayItemManagerCallback>();

		private ItemManagerCallbacks _delayedCallbacks;

		private List<DelayItemManagerCallback> _callbackDelayers = new List<DelayItemManagerCallback>();

		[SerializeField]
		private List<EquipmentSet> sets = new List<EquipmentSet>();

		[SerializeField]
		private List<ItemRepairOrder> nunRepairOrders = new List<ItemRepairOrder>();

		[SerializeField]
		private List<ItemOrder> clothierOrders = new List<ItemOrder>();

		[SerializeField]
		private List<Item> items = new List<Item>();

		[SerializeField]
		private ShopManager shopManager = ShopManager.CreateInstance();

		public IReadOnlyCollection<EquipmentSet> Sets => sets;

		public bool NunBusyWithRepair => nunRepairOrders.Any();

		public IReadOnlyCollection<ItemOrder> ClothierOrders => clothierOrders;

		public ShopManager ShopManager => shopManager;

		public event Action<string, Item> OnItemEquipped;

		public event Action<string, Item> OnItemUnequipped;

		public event Action OnItemsModified;

		public event Action OnEquippedItemsModified;

		public static ItemManager CreateInstance()
		{
			return new ItemManager();
		}

		public Item[] UnpackItem(Item item)
		{
			using (DelayCallbacks("UnpackItem"))
			{
				if (item == null)
				{
					return Array.Empty<Item>();
				}
				Func<Item, Item[]> onUnpack = item.GetItemPrefab().OnUnpack;
				if (onUnpack == null)
				{
					return Array.Empty<Item>();
				}
				RemoveItem(item);
				Item[] array = onUnpack(item);
				Item[] array2 = array;
				foreach (Item item2 in array2)
				{
					AddItem(item2, item.itemLocation);
				}
				return array;
			}
		}

		public void UseItem(Item item)
		{
			using (DelayCallbacks("UseItem"))
			{
				ItemPrefab itemPrefab = item.GetItemPrefab();
				if (itemPrefab.OnUse == null)
				{
					Debug.LogWarning("Tried to use an item without OnUse defined!");
					return;
				}
				bool flag = true;
				try
				{
					flag = itemPrefab.OnUse(item);
				}
				catch (ScriptRuntimeException exception)
				{
					Debug.LogError($"Error while using item {item.GetName()}. Gameid: {item.GameId}");
					Debug.LogException(exception);
				}
				if (flag)
				{
					ConsumeItem(item, 1);
				}
			}
		}

		public bool ConsumeItem(Item item, int count)
		{
			if (item.IsStackable)
			{
				if (item.SetCount(item.GetCount() - count))
				{
					RemoveItem(item);
					return true;
				}
				ReportChangeInItem(item, ItemChangeType.InnerDataChange);
				return false;
			}
			RemoveItem(item);
			return true;
		}

		public bool ConsumeItem(Item item, int count, out Item outCopy)
		{
			int count2 = Mathf.Min(item.GetCount(), count);
			outCopy = item.Clone();
			outCopy.SetCount(count2);
			return ConsumeItem(item, count);
		}

		public IEnumerable<(string label, string data)> InventoryDebugStrings()
		{
			yield return (label: "slotToEquippedItems", data: slotToEquippedItems.ToStringBetter((List<Item> i) => i.ToStringBetter((Item x) => x.GetName()), (string x) => x, "\n"));
			yield return (label: "allEquippedItems", data: allEquippedItems.ToStringBetter((Item item) => item.GetName()));
			yield return (label: "botEquippedItems", data: botEquippedItems.ToStringBetter((Item item) => item.GetName()));
			yield return (label: "gameIdToEquippedItems", data: gameIdToEquippedItems.ToStringBetter((List<Item> i) => i.ToStringBetter((Item x) => x.GetName()), (GameId x) => x.ToShortString(), "\n"));
		}

		public ReadOnlyDictionary<string, List<Item>> AllEquippedItemsDictionary()
		{
			return new ReadOnlyDictionary<string, List<Item>>(slotToEquippedItems);
		}

		public IEnumerable<Item> AllEquippedItems()
		{
			return allEquippedItems;
		}

		public IEnumerable<Item> AllEquippedItemsInEquipmentOrder()
		{
			return items.Where((Item x) => x.IsEquipped());
		}

		public IEnumerable<Item> BotEquippedItems()
		{
			return botEquippedItems;
		}

		public bool TryGetItemsAtSlot(string slot, out List<Item> _items)
		{
			return slotToEquippedItems.TryGetValue(slot, out _items);
		}

		public bool TryGetItemsAtSlot(ItemSlot slot, out List<Item> _items)
		{
			return slotToEquippedItems.TryGetValue(slot.Id, out _items);
		}

		public bool TryGetItemAtSlot(ItemSlot slot, out Item item)
		{
			if (TryGetItemsAtSlot(slot, out var _items) && _items.Count > 0)
			{
				item = _items[0];
				return true;
			}
			item = null;
			return false;
		}

		public bool HasItemAtSlot(ItemSlot slot)
		{
			Item item;
			return TryGetItemAtSlot(slot, out item);
		}

		[CanBeNull]
		public Item GetFirstItemAtSlot(string slot)
		{
			if (TryGetItemsAtSlot(slot, out var _items))
			{
				return _items.FirstOrDefault();
			}
			return null;
		}

		public bool FirstItemAtSlotHasTag(string slot, ItemPrefabTag tag)
		{
			if (TryGetItemsAtSlot(slot, out var _items))
			{
				return _items.First().HasTag(tag);
			}
			return false;
		}

		public bool AllItemRequiredSlotsEquipped(Item item, string slot)
		{
			List<ItemSlot> list;
			using (MyCollectionPool<ItemSlot>.GetListAndFill(item.GetItemPrefab().GetRequiredSlots(slot), out list))
			{
				if (list.Count == 0)
				{
					return true;
				}
				foreach (ItemSlot item2 in list)
				{
					if (!slotToEquippedItems.ContainsKey(item2.Id))
					{
						return false;
					}
				}
				return true;
			}
		}

		public IEnumerable<Item> GetItemsThatRequireThisEquipped(Item itemToRemove)
		{
			ItemSlot mainItemSlot = ItemSlot.SlotManager.GetSlot(itemToRemove.EquippedSlot);
			foreach (Item item in AllEquippedItems())
			{
				string equippedSlot = item.EquippedSlot;
				if (item.GetItemPrefab().GetRequiredSlots(equippedSlot).Contains(mainItemSlot))
				{
					yield return item;
				}
			}
		}

		public EquipResults Equip(Item item, bool forceRemoveForbidden)
		{
			if (item.GetItemPrefab().IsEquipable(out var slotEquipData))
			{
				return Equip(item, slotEquipData.TargetSlot, forceRemoveForbidden);
			}
			Debug.LogWarning("Trying to equip a non equipable item " + item.GetItemPrefab().Name);
			return EquipResults.FailedNonEquipable;
		}

		public void UnequipAllBotClothes()
		{
			UnequipAllByPredicate((Item item) => item.GetItemPrefab().Category == ItemCategory.Clothing);
		}

		public EquipResults Equip(Item newItem, string slotId, bool forceRemoveForbidden = false)
		{
			ItemSlot slot = ItemSlot.SlotManager.GetSlot(slotId);
			return Equip(newItem, slot, forceRemoveForbidden);
		}

		public void UnequipItemWithDependencies(string slot)
		{
			UnequipItemWithDependencies(GetFirstItemAtSlot(slot));
		}

		public void UnequipAllItemsAtSlotWithDependencies(string slot)
		{
			if (!TryGetItemsAtSlot(slot, out var _items))
			{
				return;
			}
			List<Item> list;
			using (MyCollectionPool<Item>.GetListAndFill(_items, out list))
			{
				foreach (Item item in list)
				{
					UnequipItemWithDependencies(item);
				}
			}
		}

		public Result UnequipItem(string slot, bool withDependencies = false)
		{
			return UnequipItemDescription(GetFirstItemAtSlot(slot), withDependencies);
		}

		public void UnequipAllBotItems()
		{
			using (DelayCallbacks("UnequipAllBotItems"))
			{
				List<Item> list;
				using (MyCollectionPool<Item>.GetListAndFill(BotEquippedItems(), out list))
				{
					foreach (Item item in list)
					{
						UnequipItemWithDependencies(item);
					}
				}
			}
		}

		public void UnequipAllByPredicate(Func<Item, bool> predicate)
		{
			using (DelayCallbacks("UnequipAllByPredicate"))
			{
				List<Item> list;
				using (MyCollectionPool<Item>.GetListAndFill(AllEquippedItems(), out list))
				{
					foreach (Item item in list)
					{
						if (predicate(item))
						{
							UnequipItemWithDependencies(item);
						}
					}
				}
			}
		}

		public EquipResults Equip(Item newItem, ItemSlot slot, bool forceRemoveForbidden = false, StringBuilder infoStringBuilder = null)
		{
			if (newItem._equipedSlot == slot.Id)
			{
				return EquipResults.Success;
			}
			using (DelayCallbacks("Equip"))
			{
				string id = slot.Id;
				List<Item> list;
				using (MyCollectionPool<Item>.GetList(out list))
				{
					foreach (Item allEquippedItem in allEquippedItems)
					{
						if (allEquippedItem.GetItemPrefab().GetForbiddenSlots(allEquippedItem.EquippedSlot).Contains(slot))
						{
							list.Add(allEquippedItem);
						}
					}
					foreach (ItemSlot forbiddenSlot in newItem.GetItemPrefab().GetForbiddenSlots(id))
					{
						if (slotToEquippedItems.TryGetValue(forbiddenSlot.Id, out var value))
						{
							list.AddRange(value);
						}
					}
					if (list.Count > 0)
					{
						if (forceRemoveForbidden)
						{
							foreach (Item item in list)
							{
								UnequipItemWithDependencies(item);
							}
						}
						else if (list.Count != 1 || slotToEquippedItems.ContainsKey(id) || !UnequipItem(list[0].EquippedSlot).IsSuccess)
						{
							if (infoStringBuilder != null)
							{
								infoStringBuilder.AppendLine(LOC.UI.Equipment_CannotEquipItemsToRemove);
								if (slotToEquippedItems.TryGetValue(id, out var value2))
								{
									list.AddRange(value2);
								}
								HashSet<Item> hashSet;
								using (MyCollectionPool<Item>.GetHashSet(out hashSet))
								{
									foreach (Item item2 in list)
									{
										hashSet.Add(item2);
										foreach (Item item3 in GetItemsThatRequireThisEquipped(item2))
										{
											hashSet.Add(item3);
										}
									}
									foreach (Item item4 in hashSet)
									{
										infoStringBuilder.Append("- ");
										infoStringBuilder.AppendLine(item4.GetName());
									}
								}
							}
							return EquipResults.FailedBlocked;
						}
					}
				}
				List<ItemSlot> list2;
				using (MyCollectionPool<ItemSlot>.GetListAndFill(newItem.GetItemPrefab().GetRequiredSlots(id), out list2))
				{
					if (list2.Count == 0)
					{
						EquipItemInner(newItem, id);
						return EquipResults.Success;
					}
					bool flag = false;
					foreach (ItemSlot item5 in list2)
					{
						if (!slotToEquippedItems.ContainsKey(item5.Id))
						{
							infoStringBuilder?.AppendLine(LOC.UI.Equipment_CannotEquipWithoutSlot(item5.FriendlyName));
							flag = true;
						}
					}
					if (flag)
					{
						return EquipResults.FailedMissingRequired;
					}
					EquipItemInner(newItem, id);
					return EquipResults.Success;
				}
			}
		}

		public void UnequipItemWithDependencies(Item item)
		{
			if (item == null || !item.IsEquipped())
			{
				return;
			}
			using (DelayCallbacks("UnequipItemWithDependencies"))
			{
				List<Item> list;
				using (MyCollectionPool<Item>.GetListAndFill(GetItemsThatRequireThisEquipped(item), out list))
				{
					UnequipItemInner(item);
					foreach (Item item2 in list)
					{
						UnequipItemWithDependencies(item2);
					}
				}
			}
		}

		public Result UnequipItemDescription(Item item, bool withDependencies = false)
		{
			if (item == null)
			{
				return true;
			}
			if (!item.IsEquipped())
			{
				return true;
			}
			using (DelayCallbacks("UnequipItemDescription"))
			{
				List<Item> list;
				using (MyCollectionPool<Item>.GetListAndFill(GetItemsThatRequireThisEquipped(item), out list))
				{
					if (!withDependencies)
					{
						if (!list.Any())
						{
							UnequipItemInner(item);
							return Result.SuccessWithDescription(LOC.UI.Equipment_Unequipped(item.GetName()));
						}
						StringBuilder stringBuilder;
						using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
						{
							foreach (Item item2 in list)
							{
								stringBuilder.AppendLine(LOC.UI.Equipment_CannotRemove(item2.GetName()));
							}
							return Result.FailWithDescription(stringBuilder.ToString());
						}
					}
					UnequipItemInner(item);
					StringBuilder stringBuilder2;
					using (StringBuilderCache.AcquireWithUsing(out stringBuilder2))
					{
						stringBuilder2.Append(LOC.UI.Equipment_Unequipped(item.GetName()));
						foreach (Item item3 in list)
						{
							Result result = UnequipItemDescription(item3, withDependencies: true);
							if (result.HasDescription)
							{
								stringBuilder2.AppendLine(result.GetDescription());
							}
						}
						return Result.FailWithDescription(stringBuilder2.ToString());
					}
				}
			}
		}

		private void EquipItemInnerNotMainDictionary(Item item, string slot)
		{
			gameIdToEquippedItems.AddToLookup(item.GameId, item, cachedList: true);
			allEquippedItems.Add(item);
			if (ItemSlot.IsBotSlot(ItemSlot.SlotManager.GetSlotType(slot)))
			{
				botEquippedItems.Add(item);
			}
			item.EnforceAllVariableRequirements();
		}

		private void EquipItemInner(Item item, string slot)
		{
			List<Item> value;
			if (ItemSlot.SlotManager.GetSlot(slot).AllowedMultipleItems)
			{
				slotToEquippedItems.AddToLookup(slot, item, cachedList: true);
			}
			else if (slotToEquippedItems.TryGetValue(slot, out value))
			{
				foreach (Item item2 in value)
				{
					item2.EquippedSlot = "";
					UnequipItemInnerNotMainDictionary(item2, slot);
				}
				value.Clear();
				value.Add(item);
			}
			else
			{
				slotToEquippedItems.AddToLookup(slot, item, cachedList: true);
			}
			item.EquippedSlot = slot;
			EquipItemInnerNotMainDictionary(item, slot);
			OnItemEquipped?.Invoke(slot, item);
			ReportChangeInItem(item, ItemChangeType.Equip);
		}

		private void UnequipItemInnerNotMainDictionary(Item item, string slot)
		{
			gameIdToEquippedItems.RemoveFromLookupSingle(item.GameId, item, cachedList: true);
			allEquippedItems.Remove(item);
			if (ItemSlot.IsBotSlot(ItemSlot.SlotManager.GetSlotType(slot)))
			{
				botEquippedItems.Remove(item);
			}
		}

		private void UnequipItemInner(Item item)
		{
			string equippedSlot = item.EquippedSlot;
			item.EquippedSlot = "";
			if (slotToEquippedItems.RemoveFromLookupSingle(equippedSlot, item, cachedList: true))
			{
				UnequipItemInnerNotMainDictionary(item, equippedSlot);
				OnItemUnequipped?.Invoke(equippedSlot, item);
				ReportChangeInItem(item, ItemChangeType.Unequip);
			}
			else
			{
				Debug.LogError($"Removing an item \"{item}\" from slot while it's not equipped! Something's fucked!");
			}
		}

		public void ReloadTexturesInAllEquippedItems(StringBuilder logSb)
		{
			foreach (Item item in AllEquippedItems())
			{
				item.GetItemPrefab().ReloadTextures(logSb);
			}
			SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.ResetAllTextures();
			SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.UpdateAllTextures();
		}

		public void ToggleSpecialEffects(bool value)
		{
			using (DelayCallbacks("ToggleSpecialEffects"))
			{
				foreach (Item item in items)
				{
					item.CallSpecialEffectsAction(value);
				}
			}
		}

		public void AddItem(Item item, ItemLocationEnum itemLocationEnum = ItemLocationEnum.AtHome)
		{
			if (item.IsEquipped())
			{
				item._equipedSlot = "";
				Debug.LogError("Tried to add an already equipped item into the inventory! Something's fucked!(Uneqipped it)");
			}
			item.itemLocation = itemLocationEnum;
			if (item.IsStackable && TryGetItemsByGameId(item.GameId, out var gameIdItems))
			{
				Item item2 = gameIdItems.FirstOrDefault((Item t) => t.itemLocation == item.itemLocation);
				if (item2 != null)
				{
					item2.SetCount(item2.GetCount() + item.GetCount());
					return;
				}
			}
			AddItemInner(item);
		}

		public void RemoveItem(Item item)
		{
			using (DelayCallbacks("RemoveItem"))
			{
				UnequipItemWithDependencies(item);
				items.Remove(item);
				RemoveFromGameIdDictionary(item);
				ReportChangeInItem(item, ItemChangeType.Removed);
			}
		}

		public void RemoveItemAt(int index, Item item)
		{
			using (DelayCallbacks("RemoveItem"))
			{
				UnequipItemWithDependencies(item);
				items.RemoveAt(index);
				RemoveFromGameIdDictionary(item);
				ReportChangeInItem(item, ItemChangeType.Removed);
			}
		}

		public bool MoveItemToOtherItemsPosition(Item target, Item source)
		{
			if (target == source)
			{
				return false;
			}
			int num = items.IndexOf(target);
			int num2 = items.IndexOf(source);
			if (num < 0 || num2 < 0)
			{
				return false;
			}
			using (DelayCallbacks("MoveItemToOtherItemsPosition"))
			{
				items.RemoveAt(num2);
				items.Insert(num, source);
				ReportChangeInItem(source, ItemChangeType.OrderChange);
				ReportChangeInItem(target, ItemChangeType.OrderChange);
				return true;
			}
		}

		private void AddToGameIdDictionary(Item item)
		{
			if (itemsById.TryGetValue(item.GameId, out var value))
			{
				if (!value.Contains(item))
				{
					value.Add(item);
				}
			}
			else
			{
				value = MyCollectionPool<Item>.GetList();
				value.Add(item);
				itemsById.Add(item.GameId, value);
			}
		}

		private void RemoveFromGameIdDictionary(Item item)
		{
			itemsById.RemoveFromLookupSingle(item.GameId, item, cachedList: true);
		}

		private void AddItemInner(Item item)
		{
			items.Insert(0, item);
			AddToGameIdDictionary(item);
			ReportChangeInItem(item, ItemChangeType.Added);
		}

		private void DoCallbacksDirectInner(ItemManagerCallbacks callback)
		{
			if (callback != ItemManagerCallbacks.None)
			{
				if (callback.HasFlagFast(ItemManagerCallbacks.UnequippedItemsModified))
				{
					OnItemsModified?.Invoke();
				}
				if (callback.HasFlagFast(ItemManagerCallbacks.EquippedItemsModified))
				{
					RelatedGameVariables.InvalidateGameStats();
					SingletonMonoBehaviour<Live2DControllerSingleton>.Instance.UpdateAllTextures();
					OnEquippedItemsModified?.Invoke();
				}
			}
		}

		[MustDisposeResource]
		public DelayItemManagerCallback DelayCallbacks(string source)
		{
			DelayItemManagerCallback delayItemManagerCallback = _delayedCallbackPool.Acquire();
			delayItemManagerCallback.FillData(source);
			_callbackDelayers.Add(delayItemManagerCallback);
			return delayItemManagerCallback;
		}

		private void ReportCallback(ItemManagerCallbacks callback)
		{
			if (_callbackDelayers.Count == 0)
			{
				DoCallbacksDirectInner(callback);
			}
			else
			{
				_delayedCallbacks |= callback;
			}
		}

		private void RemoveCallbackDelay(DelayItemManagerCallback delay)
		{
			int num = _callbackDelayers.LastIndexOf(delay);
			if (num >= 0)
			{
				_delayedCallbackPool.Release(delay);
				_callbackDelayers.RemoveAt(num);
				if (_callbackDelayers.Count == 0)
				{
					ItemManagerCallbacks delayedCallbacks = _delayedCallbacks;
					_delayedCallbacks = ItemManagerCallbacks.None;
					DoCallbacksDirectInner(delayedCallbacks);
				}
			}
			else
			{
				Debug.LogWarning("Trying to remove a callback delay twice! It most likely was disposed twice!");
			}
		}

		private void ReportChangeInItem(Item item, ItemChangeType changeType)
		{
			switch (changeType)
			{
			case ItemChangeType.Added:
				ReportCallback(ItemManagerCallbacks.UnequippedItemsModified);
				return;
			case ItemChangeType.Unequip:
			case ItemChangeType.Equip:
				ReportCallback(ItemManagerCallbacks.EquippedItemsModified);
				ReportCallback(ItemManagerCallbacks.UnequippedItemsModified);
				return;
			}
			if (item.IsEquipped())
			{
				ReportCallback(ItemManagerCallbacks.EquippedItemsModified);
			}
			ReportCallback(ItemManagerCallbacks.UnequippedItemsModified);
		}

		public void ReportChangeInItemData(Item item)
		{
			ReportChangeInItem(item, ItemChangeType.InnerDataChange);
		}

		public void BorrowItemFromShop(ShopItem shopItem)
		{
			AddItem(shopItem.Item, ItemLocationEnum.BorrowedFromShop);
		}

		public void MoveCarriedToHome()
		{
			using (DelayCallbacks("ReturnAllBorrowedItems"))
			{
				foreach (Item item in items)
				{
					if (item.itemLocation == ItemLocationEnum.Carried)
					{
						item.itemLocation = ItemLocationEnum.AtHome;
						ReportChangeInItem(item, ItemChangeType.InnerDataChange);
					}
				}
			}
		}

		public void MoveBotEquippedToCarried()
		{
			using (DelayCallbacks("ReturnAllBorrowedItems"))
			{
				foreach (Item item in BotEquippedItems())
				{
					item.itemLocation = ItemLocationEnum.Carried;
					ReportChangeInItem(item, ItemChangeType.InnerDataChange);
				}
			}
		}

		public int ReturnAllBorrowedItems(string shopId)
		{
			using (DelayCallbacks("ReturnAllBorrowedItems"))
			{
				HashSet<Guid> hashSet;
				using (MyCollectionPool<Guid>.GetHashSet(out hashSet))
				{
					int num = 0;
					for (int num2 = items.Count - 1; num2 >= 0; num2--)
					{
						Item item = items[num2];
						if (item.itemLocation == ItemLocationEnum.BorrowedFromShop)
						{
							if (item.TryGetSourceUniqueId(out var uniqueIdOfOriginal))
							{
								hashSet.Add(uniqueIdOfOriginal);
							}
							item.itemLocation = ItemLocationEnum.Carried;
							RemoveItemAt(num2, item);
							num++;
						}
					}
					if (ShopManager.TryGetShop(shopId, out var shop))
					{
						foreach (Guid item2 in hashSet)
						{
							if (shop.TryGetItemByGuid(item2, out var shopItemOut))
							{
								shopItemOut.Item.itemLocation = ItemLocationEnum.Carried;
							}
						}
					}
					return num;
				}
			}
		}

		public void ReturnItemToShop(string shopId, Item itemToReturn)
		{
			using (DelayCallbacks("ReturnItemToShop"))
			{
				Guid sourceOrThisUniqueGuid = itemToReturn.GetSourceOrThisUniqueGuid();
				RemoveItem(itemToReturn);
				itemToReturn.itemLocation = ItemLocationEnum.Carried;
				for (int num = items.Count - 1; num >= 0; num--)
				{
					Item item = items[num];
					if (item.itemLocation == ItemLocationEnum.BorrowedFromShop && item.IsSameSource(sourceOrThisUniqueGuid))
					{
						RemoveItemAt(num, item);
						item.itemLocation = ItemLocationEnum.Carried;
					}
				}
				if (ShopManager.TryGetShop(shopId, out var shop) && shop.TryGetItemByGuid(sourceOrThisUniqueGuid, out var shopItemOut))
				{
					shopItemOut.Item.itemLocation = ItemLocationEnum.Carried;
				}
			}
		}

		public bool TryEquippingARandomItemAtSlot(ItemSlot slot)
		{
			List<Item> slotItems;
			using (GetAllItemsAtSlot(slot, out slotItems))
			{
				if (slotItems.Count > 0)
				{
					return Equip(slotItems.GetRandom(), forceRemoveForbidden: false) == EquipResults.Success;
				}
				return false;
			}
		}

		public bool TryGetItemsByGameId(GameId gameId, [NotNullWhen(true)] out IReadOnlyCollection<Item> gameIdItems)
		{
			if (itemsById.TryGetValue(gameId, out var value) && value.Count > 0)
			{
				gameIdItems = value;
				return true;
			}
			gameIdItems = null;
			return false;
		}

		public bool AppendItemsByGameId(GameId gameId, List<Item> items)
		{
			if (TryGetItemsByGameId(gameId, out var gameIdItems))
			{
				items.AddRange(gameIdItems);
				return true;
			}
			return false;
		}

		public bool IsEquipped(ItemEnum item)
		{
			return IsEquipped(GameId.CreateVanilla((int)item));
		}

		public bool IsEquipped(GameId gameId)
		{
			List<Item> value;
			return gameIdToEquippedItems.TryGetValue(gameId, out value);
		}

		public bool HasItem(GameId gameId)
		{
			IReadOnlyCollection<Item> gameIdItems;
			return TryGetItemsByGameId(gameId, out gameIdItems);
		}

		public int GetItemCount(GameId gameId)
		{
			if (!TryGetItemsByGameId(gameId, out var gameIdItems))
			{
				return 0;
			}
			return gameIdItems.Sum((Item x) => x.GetCount());
		}

		public bool HasItem(Predicate<Item> predicate)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (predicate(items[i]))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsEquippedComplex(Item item, bool enforceUniqueId, bool checkColors)
		{
			if (item == null)
			{
				return true;
			}
			if (!gameIdToEquippedItems.TryGetValue(item.GameId, out var value))
			{
				return false;
			}
			foreach (Item item2 in value)
			{
				if (item2.Equals_Complex(item, enforceUniqueId, checkColors))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsEquippedMerch(string partnerId)
		{
			foreach (Item item in AllEquippedItems())
			{
				if (item.GetItemPrefab().IsMadeByPartner(partnerId))
				{
					return true;
				}
			}
			return false;
		}

		public bool HasAnyMerch(string partnerId)
		{
			return items.Any((Item itemInInventory) => itemInInventory.GetItemPrefab().IsMadeByPartner(partnerId));
		}

		public int MerchCount(string partnerId)
		{
			return Enumerable.Count(items, (Item t) => t.GetItemPrefab().IsMadeByPartner(partnerId));
		}

		public IEnumerable<Item> GetAllRepairableItems()
		{
			return items.Where((Item t) => t.GetItemPrefab().HasQuality);
		}

		public IEnumerable<Item> GetAllFishes()
		{
			return items.Where((Item t) => t.IsFish);
		}

		public float CalculateStreamingBonus()
		{
			int num = items.Where((Item t) => t.IsEquipped()).Aggregate(0, (int i, Item item) => i + item.GetPrice());
			Item firstItemAtSlot = GetFirstItemAtSlot(ItemSlot.SlotManager.LegL.Id);
			Item firstItemAtSlot2 = GetFirstItemAtSlot(ItemSlot.SlotManager.LegR.Id);
			Item firstItemAtSlot3 = GetFirstItemAtSlot(ItemSlot.SlotManager.ArmL.Id);
			Item firstItemAtSlot4 = GetFirstItemAtSlot(ItemSlot.SlotManager.ArmR.Id);
			float num2 = 1f;
			if (firstItemAtSlot != null && firstItemAtSlot2 != null)
			{
				num2 *= 1.1f;
				if (firstItemAtSlot.GetMainColor() == firstItemAtSlot2.GetMainColor())
				{
					num2 *= 1.1f;
				}
			}
			if (firstItemAtSlot3 != null && firstItemAtSlot4 != null)
			{
				num2 *= 1.1f;
				if (firstItemAtSlot3.GetMainColor() == firstItemAtSlot4.GetMainColor())
				{
					num2 *= 1.1f;
				}
			}
			if (firstItemAtSlot != null && firstItemAtSlot2 != null && firstItemAtSlot3 != null && firstItemAtSlot4 != null)
			{
				num2 *= 1.1f;
			}
			return num2 * (1f + (float)num / 2000000f);
		}

		public IReadOnlyCollection<Item> AllItemsReadonlyCollection()
		{
			return items;
		}

		public IEnumerable<Item> AllItemsEnumerable()
		{
			return items;
		}

		[MustDisposeResource]
		public PooledList<Item> GetAllItemsAtSlot(ItemSlot slot, out List<Item> slotItems)
		{
			PooledList<Item> list = MyCollectionPool<Item>.GetList(out slotItems);
			foreach (Item item in items)
			{
				if (item.GetItemPrefab().CanGoInto(slot))
				{
					slotItems.Add(item);
				}
			}
			return list;
		}

		[MustDisposeResource]
		public PooledList<Item> GetAllPairsAtSlots(ItemSlot slot1, ItemSlot slot2, out List<Item> slotItems)
		{
			PooledList<Item> list = MyCollectionPool<Item>.GetList(out slotItems);
			List<Item> slotItems2;
			using (GetAllItemsAtSlot(slot1, out slotItems2))
			{
				List<Item> slotItems3;
				using (GetAllItemsAtSlot(slot2, out slotItems3))
				{
					slotItems2.Sort(ItemSortItemPairings.Comparer);
					slotItems3.Sort(ItemSortItemPairings.Comparer);
					int num = 0;
					int num2 = 0;
					while (num < slotItems2.Count && num2 < slotItems3.Count)
					{
						Item item = slotItems2[num];
						Item item2 = slotItems3[num2];
						int num3 = ItemSortColor.Comparer.Compare(item, item2);
						if (num3 == 0)
						{
							slotItems.Add(item);
							slotItems.Add(item2);
							num++;
							num2++;
						}
						else if (num3 < 0)
						{
							num++;
						}
						else
						{
							num2++;
						}
					}
					return list;
				}
			}
		}

		[MustDisposeResource]
		public PooledList<ItemPairContainer> GetAllPairedItems(out List<ItemPairContainer> groups)
		{
			Dictionary<int, ItemPairContainer> value;
			using (CollectionPool<Dictionary<int, ItemPairContainer>, KeyValuePair<int, ItemPairContainer>>.Get(out value))
			{
				foreach (Item item in items)
				{
					if (!ItemPrefab.ItemPrefabManager.TryGetItemPairIndex(item, out var itemPairIndex))
					{
						continue;
					}
					if (itemPairIndex < 0)
					{
						value.ModifyValueEnsureExists(-itemPairIndex, item, (ItemPairContainer container, Item item1) =>
						{
							if (container.Left == null)
							{
								container.Left = MyCollectionPool<Item>.GetList();
							}
							container.Left.Add(item1);
						});
						continue;
					}
					value.ModifyValueEnsureExists(itemPairIndex, item, (ItemPairContainer container, Item item1) =>
					{
						if (container.Right == null)
						{
							container.Right = MyCollectionPool<Item>.GetList();
						}
						container.Right.Add(item1);
					});
				}
				PooledList<ItemPairContainer> list = MyCollectionPool<ItemPairContainer>.GetList(out groups);
				foreach (ItemPairContainer value2 in value.Values)
				{
					if (value2.Valid)
					{
						value2.Prepare();
						groups.Add(value2);
					}
					else
					{
						value2.Dispose();
					}
				}
				return list;
			}
		}

		public bool TryFindBestItemForSlot(ItemSlot slot, out Item bestItem)
		{
			List<Item> slotItems;
			using (GetAllItemsAtSlot(slot, out slotItems))
			{
				slotItems.Sort(ItemSortPriceDescending.Comparer);
				if (slotItems.Count > 0)
				{
					bestItem = slotItems[0];
					return true;
				}
				bestItem = null;
				return false;
			}
		}

		public void AddEquipmentSet(EquipmentSet newSet)
		{
			sets.Insert(0, newSet);
		}

		public void RemoveEquipmentSet(EquipmentSet value)
		{
			sets.Remove(value);
		}

		public bool TryGetCurrentNunRepairOrder(out ItemRepairOrder itemRepairOrder)
		{
			if (nunRepairOrders.Count > 0)
			{
				itemRepairOrder = nunRepairOrders[0];
				return true;
			}
			itemRepairOrder = null;
			return false;
		}

		public Result FinishCurrentNunRepairOrder()
		{
			if (TryGetCurrentNunRepairOrder(out var itemRepairOrder) && itemRepairOrder.IsDone())
			{
				string qualityString = itemRepairOrder.RepairedItem.GetQualityString();
				itemRepairOrder.ApplyRepair();
				string qualityString2 = itemRepairOrder.RepairedItem.GetQualityString();
				nunRepairOrders.RemoveAt(0);
				AddItem(itemRepairOrder.RepairedItem, ItemLocationEnum.Carried);
				return Result.SuccessWithDescription(LOC.Other.NunRepair_RepairResult(itemRepairOrder.RepairedItem.GetName(), qualityString, qualityString2));
			}
			return false;
		}

		public void MakeNunRepairOrder(Item item)
		{
			RemoveItem(item);
			nunRepairOrders.Add(new ItemRepairOrder(item, RelatedGameVariables.time));
		}

		public void AddClothierOrder(ItemOrder itemOrder)
		{
			clothierOrders.Add(itemOrder);
		}

		public void RemoveOrder(ItemOrder itemOrder)
		{
			clothierOrders.Remove(itemOrder);
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			itemsById = itemsById.ClearOrCreate();
			foreach (Item item in items)
			{
				AddToGameIdDictionary(item);
			}
			foreach (Item item2 in items)
			{
				if (item2.IsWronglyEquipped())
				{
					item2._equipedSlot = "";
				}
			}
			slotToEquippedItems = slotToEquippedItems.ClearOrCreateCachedLookup();
			allEquippedItems = allEquippedItems.ClearOrCreate();
			botEquippedItems = botEquippedItems.ClearOrCreate();
			gameIdToEquippedItems = gameIdToEquippedItems.ClearOrCreateCachedLookup();
			foreach (Item item3 in items)
			{
				if (item3.IsEquipped())
				{
					slotToEquippedItems.AddToLookup(item3.EquippedSlot, item3);
					EquipItemInnerNotMainDictionary(item3, item3.EquippedSlot);
				}
			}
		}

		public void DeleteSlot(string slot)
		{
			UnequipAllItemsAtSlotWithDependencies(slot);
			foreach (EquipmentSet set in sets)
			{
				set.DeleteSlot(slot);
			}
		}
	}

	[SerializeField]
	private int _count;

	[SerializeField]
	private float _quality;

	public const float MinQuality = 0.4f;

	public const float MinQualityForNewItem = 1f;

	public const float MaxQuality = 1.2f;

	public static List<QualityDescriptor> QualityDescriptors = new List<QualityDescriptor>
	{
		new QualityDescriptor(0f, LOC.UI.TupleReferences.Condition_Trashed),
		new QualityDescriptor(0.5f, LOC.UI.TupleReferences.Condition_VeryHeavilyUsed),
		new QualityDescriptor(0.6f, LOC.UI.TupleReferences.Condition_HeavilyUsed),
		new QualityDescriptor(0.75f, LOC.UI.TupleReferences.Condition_Used),
		new QualityDescriptor(0.9f, LOC.UI.TupleReferences.Condition_NearMint),
		new QualityDescriptor(1f, LOC.UI.TupleReferences.Condition_New)
	};

	public bool IsFavourite;

	[SerializeField]
	[HideInInspector]
	private int _id;

	[SerializeField]
	private GameId _gameId;

	[Obsolete]
	[SerializeField]
	private string _additionalData;

	[Obsolete]
	[SerializeField]
	private AdditionalDataSlot[] AdditionalDataSlots = Array.Empty<AdditionalDataSlot>();

	[SerializeField]
	private SerializableGuid UniqueItemGuid;

	[SerializeField]
	private SerializableGuid[] SourceItemsUniqueGuids;

	[SerializeField]
	private string _equipedSlot;

	private ItemPrefab _prefab;

	[SerializeField]
	private ItemLocationEnum itemLocation;

	[SerializeField]
	private Color[] _colors = Array.Empty<Color>();

	private static readonly Color DefaultColor = Color.white;

	public const float SafeFishPollutionLevel = 0.5f;

	[SerializeField]
	private LazySerializableContainer<Item>[] DI = Array.Empty<LazySerializableContainer<Item>>();

	[SerializeField]
	private ItemModifier[] IM = Array.Empty<ItemModifier>();

	public float Quality => _quality;

	public float NormalizedQuality => Mathf.Min(Mathf.InverseLerp(0.4f, 1f, _quality), 1f);

	public float InverseNormalizedQuality => 1f - NormalizedQuality;

	public GameId GameId => _gameId;

	public string EquippedSlot
	{
		get
		{
			return _equipedSlot;
		}
		private set
		{
			_equipedSlot = value;
			CallSpecialEffectsAction(value: true);
		}
	}

	public static Item CurrentSpecialEffectsActionItem { get; private set; }

	public float RepairedByQuality
	{
		get
		{
			return GetFloatSpecialVariableOrDefault("RepairedByQuality");
		}
		set
		{
			SetFloatSpecialVariable("RepairedByQuality", value);
		}
	}

	public int RepairAttempts
	{
		get
		{
			return GetIntSpecialVariableOrDefault("RepairAttempts");
		}
		set
		{
			SetIntSpecialVariable("RepairAttempts", value);
		}
	}

	public bool IsIllegal => GetItemPrefab().IsIllegal;

	public bool HasColor => GetItemPrefab().HasColor;

	public bool CanChangeColor => GetItemPrefab().CanChangeColor;

	public bool IsStackable => GetItemPrefab().IsStackable;

	public bool IsEdible => GetItemPrefab().EdibleData != null;

	public ItemLocationEnum ItemLocation => itemLocation;

	public bool IsFishingRod => GetItemPrefab().FishingRodItemPrefabData != null;

	public float FishingRodSize01 => ApplyDirectItemModifiersFloat(ItemModifierDirectItemChangeFloat.FishingRodSize01, GetItemPrefab().FishingRodItemPrefabData?.FishingRodSize01 ?? 0f);

	public float FishingRodAcceleration01 => ApplyDirectItemModifiersFloat(ItemModifierDirectItemChangeFloat.FishingRodAcceleration01, GetItemPrefab().FishingRodItemPrefabData?.FishingRodAcceleration01 ?? 0f);

	public float FishingRodReelingSpeed01 => ApplyDirectItemModifiersFloat(ItemModifierDirectItemChangeFloat.FishingRodReelingSpeed01, GetItemPrefab().FishingRodItemPrefabData?.ReelingSpeed01 ?? 0f);

	public float FishingRodMaxEffectiveFishWeightKg => ApplyDirectItemModifiersFloat(ItemModifierDirectItemChangeFloat.MaxEffectiveFishWeightKg, GetItemPrefab().FishingRodItemPrefabData?.MaxEffectiveFishWeightKg ?? 0f);

	public bool IsFish => GetItemPrefab().FishData != null;

	public float FishWeight
	{
		get
		{
			return GetFloatSpecialVariableOrDefault("FishWeight");
		}
		set
		{
			SetFloatSpecialVariable("FishWeight", value);
		}
	}

	public bool FishUnsafeToEatAtAll => FishPollution > 0.5f;

	public float FishPollution
	{
		get
		{
			return GetFloatSpecialVariableOrDefault("FishPollution");
		}
		set
		{
			SetFloatSpecialVariable("FishPollution", value);
		}
	}

	public float FishNutrition => FishWeight / 2f;

	public bool IsDish
	{
		get
		{
			EdibleItemPrefabData edibleData = GetItemPrefab().EdibleData;
			if (edibleData != null)
			{
				return edibleData.EdibleType == EdibleTypeEnum.Dish;
			}
			return false;
		}
	}

	public float DishQuality
	{
		get
		{
			return GetFloatSpecialVariableOrDefault("DishQuality");
		}
		set
		{
			SetFloatSpecialVariable("DishQuality", value);
		}
	}

	public IReadOnlyCollection<ItemModifier> ItemModifiers => Array.AsReadOnly(IM);

	public bool HasAnyItemModifiers => IM.Length != 0;

	public static Item CreateRandomItem(GameId gameId, bool allowModifiers = true)
	{
		ItemPrefab itemById = ItemPrefab.ItemPrefabManager.GetItemById(gameId);
		Item item = ((!itemById.HasQuality) ? new Item(gameId) : new Item(gameId, UnityEngine.Random.Range(0.4f, 1.2f)));
		if (allowModifiers)
		{
			itemById.AllowedModifiersData?.SetRandomModifiers(item);
		}
		itemById.RandomItemPostProcessing?.Invoke(item);
		item.EnforceAllVariableRequirements();
		return item;
	}

	public static Item CreateDefaultItemForLive2DController(GameId gameId)
	{
		Item item = CreateRandomItem(gameId);
		if (item.GetItemPrefab().IsEquipable(out var slotEquipData))
		{
			item._equipedSlot = slotEquipData.TargetSlotString;
		}
		else
		{
			Debug.LogError("Default item " + item.GetName() + " isn't equippable! That's wrong!");
		}
		return item;
	}

	public Item(GameId id, float quality = 1f)
	{
		_id = -1;
		_gameId = id;
		_quality = quality;
		_equipedSlot = "";
		ItemPrefab itemById = ItemPrefab.ItemPrefabManager.GetItemById(id);
		if (itemById.ColorSlots != null)
		{
			if (itemById.CanChangeColor && UnityEngine.Random.value > 0.3f)
			{
				for (int i = 0; i < itemById.ColorSlots.Length; i++)
				{
					_ = itemById.ColorSlots[i];
					SetColor(i, UnityEngine.Random.ColorHSV(0f, 1f, 0.01f, 0.7f, 0.1f, 1f));
				}
			}
			else
			{
				for (int j = 0; j < itemById.ColorSlots.Length; j++)
				{
					Color32 color = itemById.ColorSlots[j].Palette.GetRandomColor().Color;
					SetColor(j, color);
				}
			}
		}
		UniqueItemGuid = Guid.NewGuid();
	}

	public Item(ItemEnum id, float quality = 1f)
		: this(GameId.CreateVanilla((int)id), quality)
	{
	}

	public SimpleLocalizedString GetRawNameSls()
	{
		return ItemPrefab.ItemPrefabManager.GetItemNameSls(this);
	}

	private bool NameRequiresStringBuilder(ItemPrefab prefab, ItemNameIncludeEnum itemNameIncludeEnum = ItemNameIncludeEnum.Default)
	{
		if ((!itemNameIncludeEnum.HasFlagFast(ItemNameIncludeEnum.ColorInfo) || !prefab.HasColor || prefab.CanChangeColor) && (!itemNameIncludeEnum.HasFlagFast(ItemNameIncludeEnum.ItemCount) || !prefab.IsStackable || GetCount() <= 1) && (!itemNameIncludeEnum.HasFlagFast(ItemNameIncludeEnum.Other) || ItemLocation != ItemLocationEnum.BorrowedFromShop) && (!itemNameIncludeEnum.HasFlagFast(ItemNameIncludeEnum.ShowFavourite) || !IsFavourite) && (!itemNameIncludeEnum.HasFlagFast(ItemNameIncludeEnum.ItemModifiers) || !HasAnyItemModifiers))
		{
			if (itemNameIncludeEnum.HasFlagFast(ItemNameIncludeEnum.FishEdibleFishingRod))
			{
				if (!IsDish && !IsFish)
				{
					return IsFishingRod;
				}
				return true;
			}
			return false;
		}
		return true;
	}

	public StringBuilder GetNameStringBuilder(StringBuilder stringBuilder, ItemNameIncludeEnum itemNameInclude = ItemNameIncludeEnum.Default)
	{
		ItemPrefab itemPrefab = GetItemPrefab();
		if (itemNameInclude.HasFlagFast(ItemNameIncludeEnum.ShowFavourite) && IsFavourite)
		{
			stringBuilder.Append("♥ ");
		}
		ApplyDirectItemModifiersStringBuilder(ItemModifierDirectItemChangeStringBuilder.NamePrefix, stringBuilder);
		stringBuilder.Append(itemPrefab.Name);
		ApplyDirectItemModifiersStringBuilder(ItemModifierDirectItemChangeStringBuilder.NamePostfix, stringBuilder);
		if (itemNameInclude.HasFlagFast(ItemNameIncludeEnum.ColorInfo) && itemPrefab.HasColor && !itemPrefab.CanChangeColor)
		{
			Color[] colors = GetColors();
			if (colors.Length != 0)
			{
				stringBuilder.Append("(");
				for (int i = 0; i < colors.Length; i++)
				{
					Color color = colors[i];
					stringBuilder.AppendColorTagStart(color);
					stringBuilder.Append(ColorPaletteManager.FindName(color));
					stringBuilder.AppendColorTagEnd();
					if (i != colors.Length - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				stringBuilder.Append(")");
			}
		}
		if (itemNameInclude.HasFlagFast(ItemNameIncludeEnum.ItemModifiers) && HasAnyItemModifiers)
		{
			stringBuilder.Append("(");
			ItemModifier[] iM = IM;
			foreach (ItemModifier itemModifier in iM)
			{
				stringBuilder.AppendColorTagStart(itemModifier.GetFirstColor());
				stringBuilder.Append(itemModifier.Char);
				stringBuilder.AppendColorTagEnd();
			}
			stringBuilder.Append(")");
		}
		if (itemNameInclude.HasFlagFast(ItemNameIncludeEnum.FishEdibleFishingRod))
		{
			if (itemPrefab.FishData != null)
			{
				stringBuilder.Append("(");
				if (FishPollution > 0.5f)
				{
					stringBuilder.Append(LOC.UI.Item_FishPolluted);
					stringBuilder.Append(", ");
				}
				stringBuilder.AppendFloat2AfterDot(FishWeight);
				stringBuilder.Append(" ");
				stringBuilder.Append(LOC.UI.WeightKgShort);
				stringBuilder.Append(")");
			}
			EdibleItemPrefabData edibleData = itemPrefab.EdibleData;
			if (edibleData != null && edibleData.EdibleType == EdibleTypeEnum.Dish && DI.Any())
			{
				float dishQuality = DishQuality;
				stringBuilder.Append("(");
				stringBuilder.Append(GetDishQualityDescription(dishQuality));
				stringBuilder.Append(")");
			}
		}
		if (itemNameInclude.HasFlagFast(ItemNameIncludeEnum.ItemCount) && itemPrefab.IsStackable && GetCount() > 1)
		{
			stringBuilder.Append("(");
			stringBuilder.Append(GetCount());
			stringBuilder.Append(")");
		}
		if (itemNameInclude.HasFlagFast(ItemNameIncludeEnum.Other) && ItemLocation == ItemLocationEnum.BorrowedFromShop)
		{
			stringBuilder.Append("(");
			stringBuilder.AppendColorTagStart("red");
			stringBuilder.Append(LOC.UI.ItemBorrowedShort);
			stringBuilder.AppendColorTagEnd();
			stringBuilder.Append(")");
		}
		return stringBuilder;
	}

	[MustUseReturnValue]
	public string GetName(ItemNameIncludeEnum itemNameInclude = ItemNameIncludeEnum.Default)
	{
		StringBuilder stringBuilder;
		if (NameRequiresStringBuilder(GetItemPrefab(), itemNameInclude))
		{
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				GetNameStringBuilder(stringBuilder, itemNameInclude);
				return stringBuilder.ToString();
			}
		}
		return GetItemPrefab().Name;
	}

	private bool DescriptionRequiresStringBuilder(ItemPrefab prefab)
	{
		if (!string.IsNullOrEmpty(prefab.ShillingLinkId) && (!IsStackable || GetCount() <= 1) && ItemLocation != ItemLocationEnum.BorrowedFromShop && prefab.FishData == null)
		{
			return HasAnyItemModifiers;
		}
		return true;
	}

	public void GetDescriptionStringBuilder(StringBuilder stringBuilder)
	{
		ItemPrefab itemPrefab = GetItemPrefab();
		stringBuilder.Append(itemPrefab.Description);
		ApplyDirectItemModifiersStringBuilder(ItemModifierDirectItemChangeStringBuilder.DescriptionPostfix, stringBuilder);
		FishItemPrefabData fishData = itemPrefab.FishData;
		if (fishData != null)
		{
			stringBuilder.AppendLine();
			stringBuilder.AppendLineSubHeader(LOC.UI.Item_FishInfoHeader);
			string sizeDescriptionForFishPercentile = FishingMinigameUtilities.GetSizeDescriptionForFishPercentile(fishData.DetermineFishPercentile(FishWeight));
			stringBuilder.AppendLine(sizeDescriptionForFishPercentile);
			stringBuilder.AppendLine(LOC.UI.FishItemWeightItemInfo(FishWeight));
			if (FishPollution > 0.5f)
			{
				stringBuilder.AppendLine(LOC.UI.Item_FishPollutedDescription);
			}
		}
		EdibleItemPrefabData edibleData = itemPrefab.EdibleData;
		if (edibleData != null)
		{
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			if (edibleData.EdibleType == EdibleTypeEnum.Dish && DI.Any())
			{
				float dishQuality = DishQuality;
				stringBuilder.AppendLineSubHeader(LOC.UI.DishQuality_Header);
				stringBuilder.Append(GetDishQualityDescription(dishQuality));
				stringBuilder.AppendFormat("({0:P2})", dishQuality);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.ItemDescription_Ingredients);
				for (int i = 0; i < DI.Length; i++)
				{
					((Item)DI[i]).GetNameStringBuilder(stringBuilder, ItemNameIncludeEnum.Basic);
					if (i != DI.Length - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
			}
			EdibleItemNutritionProfile pooled;
			using (EdibleItemNutritionProfile.Pool.Acquire(out pooled))
			{
				edibleData.FillNutritionProfile(this, pooled);
				stringBuilder.AppendLine();
				pooled.AppendDescribeEffects(stringBuilder);
			}
		}
		if (itemPrefab.FishingRodItemPrefabData != null)
		{
			stringBuilder.AppendLine();
			Item firstItemAtSlot = GameVariables.Current.itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.Anon_FishingRod.Id);
			float fishingRodSize = FishingRodSize01;
			float fishingRodAcceleration = FishingRodAcceleration01;
			float fishingRodReelingSpeed = FishingRodReelingSpeed01;
			float fishingRodMaxEffectiveFishWeightKg = FishingRodMaxEffectiveFishWeightKg;
			if (firstItemAtSlot != null && this != firstItemAtSlot)
			{
				float fishingRodSize2 = firstItemAtSlot.FishingRodSize01;
				float fishingRodAcceleration2 = firstItemAtSlot.FishingRodAcceleration01;
				float fishingRodReelingSpeed2 = firstItemAtSlot.FishingRodReelingSpeed01;
				float fishingRodMaxEffectiveFishWeightKg2 = firstItemAtSlot.FishingRodMaxEffectiveFishWeightKg;
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_Size);
				stringBuilder.AppendFloat2AfterDot(fishingRodSize * 10f);
				GlobalFunctions.AppendComparisonToEquipped(stringBuilder, fishingRodSize2, fishingRodSize);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_Acceleration);
				stringBuilder.AppendFloat2AfterDot(fishingRodAcceleration * 10f);
				GlobalFunctions.AppendComparisonToEquipped(stringBuilder, fishingRodAcceleration2, fishingRodAcceleration);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_ReelingSpeed);
				stringBuilder.AppendFloat2AfterDot(fishingRodReelingSpeed * 10f);
				GlobalFunctions.AppendComparisonToEquipped(stringBuilder, fishingRodReelingSpeed2, fishingRodReelingSpeed);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_MaxEffectiveFishWeight);
				stringBuilder.AppendFloat2AfterDot(fishingRodMaxEffectiveFishWeightKg);
				stringBuilder.Append(" ");
				stringBuilder.Append(LOC.UI.WeightKgShort);
				GlobalFunctions.AppendComparisonToEquipped(stringBuilder, fishingRodMaxEffectiveFishWeightKg2, fishingRodMaxEffectiveFishWeightKg);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
			}
			else
			{
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_Size);
				stringBuilder.AppendFloat2AfterDot(fishingRodSize * 10f);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_Acceleration);
				stringBuilder.AppendFloat2AfterDot(fishingRodAcceleration * 10f);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_ReelingSpeed);
				stringBuilder.AppendFloat2AfterDot(fishingRodReelingSpeed * 10f);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLineSubHeader(LOC.UI.FishingRod_MaxEffectiveFishWeight);
				stringBuilder.AppendFloat2AfterDot(fishingRodMaxEffectiveFishWeightKg);
				stringBuilder.Append(" ");
				stringBuilder.Append(LOC.UI.WeightKgShort);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
			}
		}
		if (itemPrefab.Category == ItemCategory.FishingTrash)
		{
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(LOC.UI.Item_Trash);
		}
		if (!string.IsNullOrEmpty(itemPrefab.ShillingLinkId))
		{
			stringBuilder.AppendLine();
			stringBuilder.AppendColorTagStart("red");
			stringBuilder.Append("<link=" + itemPrefab.ShillingLinkId + ">" + LOC.UI.CheckItOut + "</link>");
			stringBuilder.AppendColorTagEnd();
		}
		if (IsStackable && GetCount() > 1)
		{
			stringBuilder.AppendLine();
			stringBuilder.Append(LOC.UI.Equipment_Own(GetCount()));
		}
		if (ItemLocation == ItemLocationEnum.BorrowedFromShop)
		{
			stringBuilder.AppendLine();
			stringBuilder.AppendColorTagStart("red");
			stringBuilder.Append(LOC.UI.ItemBorrowedFromAnnalieDescription);
			stringBuilder.AppendColorTagEnd();
		}
	}

	[MustUseReturnValue]
	public string GetDescription()
	{
		ItemPrefab itemPrefab = GetItemPrefab();
		StringBuilder stringBuilder;
		if (DescriptionRequiresStringBuilder(itemPrefab))
		{
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				GetDescriptionStringBuilder(stringBuilder);
				return stringBuilder.ToString();
			}
		}
		return itemPrefab.Description;
	}

	public override string ToString()
	{
		return GetName();
	}

	public bool CanBeUsed()
	{
		return GetItemPrefab().OnUse != null;
	}

	public bool CanBeUnpacked()
	{
		return GetItemPrefab().OnUnpack != null;
	}

	public bool SetCount(int count)
	{
		count--;
		if (count < 0)
		{
			_count = -1;
		}
		else if (IsStackable)
		{
			_count = count;
		}
		else
		{
			_count = 1;
		}
		return _count < 0;
	}

	public int GetCount()
	{
		return _count + 1;
	}

	public int GetPrice()
	{
		return GetPriceAtQuality(_quality);
	}

	private int GetPriceAtQuality(float quality)
	{
		ItemPrefab itemPrefab = GetItemPrefab();
		float num = GetPrice(itemPrefab, quality);
		if (IsDish)
		{
			LazySerializableContainer<Item>[] dI = DI;
			foreach (LazySerializableContainer<Item> lazySerializableContainer in dI)
			{
				num += (float)lazySerializableContainer.GetValue().GetPrice();
			}
			num *= 0.5f + DishQuality;
		}
		else
		{
			FishItemPrefabData fishData = itemPrefab.FishData;
			if (fishData != null)
			{
				float fishWeight = FishWeight;
				float num2 = fishData.DetermineFishPercentile(fishWeight);
				float num3 = Mathf.Min(Mathf.Max(2f * num2 * num2, num2), 5f);
				num = num * num3 * 2f;
				num = Mathf.Max(num * (1f - FishPollution), 15f);
				if (FishPollution > 0.5f)
				{
					num *= 0.1f;
				}
			}
		}
		return Mathf.CeilToInt(ApplyDirectItemModifiersFloat(ItemModifierDirectItemChangeFloat.Price, num));
	}

	private static float GetPrice(ItemPrefab prefab, float quality)
	{
		int price = prefab.Price;
		if (quality < 1f)
		{
			return (float)price * quality;
		}
		return (float)price + (float)price * ((quality - 1f) * 1.2f);
	}

	public void SetQuality(float quality)
	{
		_quality = quality;
	}

	public string GetQualityString()
	{
		return GetQualityString(_quality);
	}

	public static QualityDescriptor GetQualityDescriptor(float quality)
	{
		for (int num = QualityDescriptors.Count - 1; num >= 0; num--)
		{
			if (quality >= QualityDescriptors[num].MinQuality)
			{
				return QualityDescriptors[num];
			}
		}
		return QualityDescriptors[0];
	}

	public static string GetQualityString(float quality)
	{
		return GetQualityDescriptor(quality).SimpleLocalizedString.Value;
	}

	public ItemEnum GetVanillaItemEnum()
	{
		if (_gameId.IsVanilla)
		{
			return (ItemEnum)_gameId.Id;
		}
		return ItemEnum.None;
	}

	public static string ColorSlotId(int index)
	{
		return index switch
		{
			-1 => "No_Color", 
			0 => "Color_Main", 
			1 => "Color_Secondary", 
			_ => "Color_" + index, 
		};
	}

	public void EnforceAllVariableRequirements(int lastModifiedIndex = -1)
	{
		ItemPrefab itemPrefab = GetItemPrefab();
		ItemModifiableVariablesRequirement[] itemModifiableVariablesRequirements = itemPrefab.ItemModifiableVariablesRequirements;
		for (int i = 0; i < itemModifiableVariablesRequirements.Length; i++)
		{
			itemModifiableVariablesRequirements[i].Enforce(this, itemPrefab, lastModifiedIndex);
		}
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		OnAfterDeserializeSpecialVariableHolder();
		OnAfterDeserializeColors();
		OnAfterDeserializeItemModifiers();
		OnAfterDeserializeDishData();
		if (_id != -1)
		{
			_gameId = GameId.CreateVanilla(_id);
			_id = -1;
		}
		if (UniqueItemGuid.RealGuid == Guid.Empty)
		{
			UniqueItemGuid = Guid.NewGuid();
		}
		if (!string.IsNullOrWhiteSpace(_additionalData))
		{
			try
			{
				Color color = JsonUtility.FromJson<Color>(_additionalData);
				if (color != Color.clear)
				{
					SetColor(0, color);
				}
				AdditionalDataHolder additionalDataHolder = JsonUtility.FromJson<AdditionalDataHolder>(_additionalData);
				if (additionalDataHolder != null)
				{
					for (int i = 0; i < additionalDataHolder.slots.Count; i++)
					{
						SetColor(i, additionalDataHolder.slots[i].GetDataAsColor());
					}
				}
			}
			catch
			{
			}
			_additionalData = null;
		}
		AdditionalDataSlot[] additionalDataSlots = AdditionalDataSlots;
		if (additionalDataSlots == null || additionalDataSlots.Length <= 0)
		{
			return;
		}
		int num = AdditionalDataSlots.Length;
		for (int j = 0; j < num; j++)
		{
			string key = ColorSlotId(j);
			int num2 = Array.FindIndex(AdditionalDataSlots, (AdditionalDataSlot t) => t.SlotId == key);
			if (num2 < 0)
			{
				break;
			}
			SetColor(j, AdditionalDataSlots[num2].GetDataAsColor());
		}
		AdditionalDataSlots = Array.Empty<AdditionalDataSlot>();
	}

	public Guid GetUniqueGuid()
	{
		return UniqueItemGuid;
	}

	public Guid GetSourceOrThisUniqueGuid()
	{
		if (TryGetSourceUniqueId(out var uniqueIdOfOriginal))
		{
			return uniqueIdOfOriginal;
		}
		return UniqueItemGuid;
	}

	public bool TryGetSourceUniqueId(out Guid uniqueIdOfOriginal)
	{
		if (SourceItemsUniqueGuids == null || SourceItemsUniqueGuids.Length == 0)
		{
			uniqueIdOfOriginal = Guid.Empty;
			return false;
		}
		uniqueIdOfOriginal = SourceItemsUniqueGuids[0];
		return true;
	}

	public bool IsSameSource(Guid uniqueIdOfOriginal)
	{
		return GetSourceOrThisUniqueGuid() == uniqueIdOfOriginal;
	}

	public bool CanGoInto(ItemSlot slot)
	{
		return GetItemPrefab().CanGoInto(slot);
	}

	public bool IsEquipped()
	{
		return !string.IsNullOrEmpty(_equipedSlot);
	}

	public bool IsWronglyEquipped()
	{
		if (IsEquipped())
		{
			ItemPrefab itemPrefab = GetItemPrefab();
			if (itemPrefab.IsMissingItem)
			{
				return true;
			}
			if (!itemPrefab.IsEquipable(out var slotEquipData))
			{
				return true;
			}
			if (slotEquipData.TargetSlot.Id != _equipedSlot)
			{
				return true;
			}
		}
		return false;
	}

	public void CallSpecialEffectsAction(bool value)
	{
		Action<bool, Item> specialEffectAction = GetItemPrefab().SpecialEffectAction;
		if (specialEffectAction == null)
		{
			return;
		}
		CurrentSpecialEffectsActionItem = this;
		try
		{
			specialEffectAction(value && IsEquipped(), this);
		}
		catch (InterpreterException exception)
		{
			Debug.LogException(exception);
		}
		finally
		{
			CurrentSpecialEffectsActionItem = null;
		}
	}

	public Result CanBeRepaired()
	{
		if (_quality >= 1f)
		{
			return Result.FailWithDescription(LOC.UI.NunRepair_MaxQuality);
		}
		if (RepairedByQuality > 0.3f || RepairAttempts > 1)
		{
			return Result.FailWithDescription(LOC.UI.NunRepair_AlreadyRepaired);
		}
		return Result.Success;
	}

	public float PriceChangeByRepairAmount(float repairAmount)
	{
		float num = GetPrice();
		return (float)GetPriceAtQuality(_quality + repairAmount) - num;
	}

	public void Repair(float amount)
	{
		RepairAttempts++;
		float num = Mathf.Clamp(_quality + amount, 0.4f, 1.2f);
		float num2 = num - _quality;
		_quality = num;
		RepairedByQuality += num2;
	}

	public ItemPrefab GetItemPrefab()
	{
		return _prefab ?? (_prefab = ItemPrefab.ItemPrefabManager.GetItemById(_gameId));
	}

	public bool HasTag(ItemPrefabTag tag)
	{
		return GetItemPrefab().HasTag(tag);
	}

	private void OnAfterDeserializeColors()
	{
		if (_colors == null)
		{
			_colors = Array.Empty<Color>();
		}
	}

	private void EnsureColorsSizeAtLeast(int size)
	{
		if (size > 0)
		{
			int times = size - _colors.Length;
			_colors = _colors.AppendToArrayCopy(DefaultColor, times);
		}
	}

	public Color GetColor(int index)
	{
		if (_colors == null)
		{
			return DefaultColor;
		}
		if (_colors.TryGetValue(index, out var value))
		{
			return value;
		}
		return DefaultColor;
	}

	public void SetColor(int index, Color color)
	{
		if (index >= 0)
		{
			EnsureColorsSizeAtLeast(index + 1);
			if (GetItemPrefab().ColorSlots.TryGetValue(index, out var value))
			{
				color = value.EnforceTransparency(color);
			}
			_colors[index] = color;
		}
	}

	public void SetColors(IList<Color> colors)
	{
		EnsureColorsSizeAtLeast(colors.Count);
		ColorSlot[] colorSlots = GetItemPrefab().ColorSlots;
		for (int i = 0; i < colors.Count; i++)
		{
			Color color = colors[i];
			if (colorSlots.TryGetValue(i, out var value))
			{
				color = value.EnforceTransparency(color);
			}
			_colors[i] = color;
		}
	}

	public Color[] GetColors()
	{
		return _colors;
	}

	public Color GetMainColor()
	{
		return GetColor(0);
	}

	private void CloneColors(Item clone)
	{
		clone._colors = (Color[])_colors.Clone();
	}

	public IEnumerable<Item> GetDishItems()
	{
		return DI.Select((LazySerializableContainer<Item> x) => x.GetValue());
	}

	public void FillDishData(float dishQuality, IEnumerable<Item> executionItems)
	{
		DishQuality = dishQuality;
		DI = executionItems.Select((Func<Item, LazySerializableContainer<Item>>)((Item x) => x.Clone())).ToArray();
	}

	public static string GetDishQualityDescription(float dishQuality)
	{
		if (dishQuality < 0.1f)
		{
			return LOC.Other.Dish_AbsolutelyButchered;
		}
		if (dishQuality < 0.25f)
		{
			return LOC.Other.Dish_VeryBad;
		}
		if (dishQuality < 0.5f)
		{
			return LOC.Other.Dish_Bad;
		}
		if (dishQuality < 0.6f)
		{
			return LOC.Other.Dish_Average;
		}
		if (dishQuality < 0.7f)
		{
			return LOC.Other.Dish_Tasty;
		}
		if (dishQuality < 0.8f)
		{
			return LOC.Other.Dish_Delicious;
		}
		if (dishQuality < 0.9f)
		{
			return LOC.Other.Dish_Exquisite;
		}
		if (dishQuality < 0.95f)
		{
			return LOC.Other.Dish_Masterpiece;
		}
		return LOC.Other.Dish_WorldClass;
	}

	private void CloneDishItems(Item clone)
	{
		clone.DI = ((IEnumerable<LazySerializableContainer<Item>>)DI).Select((Func<LazySerializableContainer<Item>, LazySerializableContainer<Item>>)((LazySerializableContainer<Item> x) => x.GetValue().Clone())).ToArray();
	}

	private void OnAfterDeserializeDishData()
	{
		if (DI == null)
		{
			DI = Array.Empty<LazySerializableContainer<Item>>();
		}
	}

	private void OnAfterDeserializeItemModifiers()
	{
		if (IM == null)
		{
			IM = Array.Empty<ItemModifier>();
		}
		else if (IM.Length != 0)
		{
			ItemModifier[] iM = IM;
			for (int i = 0; i < iM.Length; i++)
			{
				iM[i].Init(this);
			}
		}
	}

	public void ClearItemModifiers()
	{
		IM = Array.Empty<ItemModifier>();
	}

	public void SetItemModifiers(IReadOnlyCollection<GameId> itemModifierIds)
	{
		if (IM.Length != itemModifierIds.Count)
		{
			IM = new ItemModifier[itemModifierIds.Count];
		}
		int num = 0;
		foreach (GameId itemModifierId in itemModifierIds)
		{
			IM[num] = new ItemModifier(this, itemModifierId, UnityEngine.Random.value);
			num++;
		}
	}

	public void AddItemModifier(GameId itemModifierId)
	{
		AddItemModifier(new ItemModifier(this, itemModifierId, UnityEngine.Random.value));
	}

	public void AddItemModifier(ItemModifier itemModifier)
	{
		if (itemModifier.AllowedMultiple || !IM.Any((ItemModifier x) => x.Id == itemModifier.Id))
		{
			IM = IM.AppendToArrayCopy(itemModifier);
		}
	}

	private float ApplyDirectItemModifiersFloat(ItemModifierDirectItemChangeFloat value, float input)
	{
		if (IM.Length == 0)
		{
			return input;
		}
		float num = input;
		ItemModifier[] iM = IM;
		for (int i = 0; i < iM.Length; i++)
		{
			num = iM[i].ApplyDirect(value, num);
		}
		return num;
	}

	private void ApplyDirectItemModifiersStringBuilder(ItemModifierDirectItemChangeStringBuilder value, StringBuilder input)
	{
		if (IM.Length != 0)
		{
			ItemModifier[] iM = IM;
			for (int i = 0; i < iM.Length; i++)
			{
				iM[i].ApplyDirect(value, input);
			}
		}
	}

	public void PopulateGameStatModifierList(List<IGameStatModifier> gameStatModifiers)
	{
		ItemModifier[] iM = IM;
		foreach (ItemModifier itemModifier in iM)
		{
			if (itemModifier.IsValid)
			{
				gameStatModifiers.Add(itemModifier);
			}
			itemModifier.FillGameStatModifiers(gameStatModifiers);
		}
		GetItemPrefab().FillGameStatsModifiersAction?.Invoke(this, gameStatModifiers);
	}

	private void CloneItemModifiers(Item clone)
	{
		if (IM.Length != 0)
		{
			clone.IM = new ItemModifier[IM.Length];
			for (int i = 0; i < IM.Length; i++)
			{
				ItemModifier itemModifier = IM[i].Clone();
				clone.IM[i] = itemModifier;
				itemModifier.Init(clone);
			}
		}
	}

	public bool Equals_IdAndEqualsColorsAndQuality(Item item2)
	{
		if (Mathf.Approximately(Quality, item2.Quality))
		{
			return Equals_IdAndColors(item2);
		}
		return false;
	}

	public bool Equals_IdAndColors(Item item2)
	{
		if (GameId != item2.GameId)
		{
			return false;
		}
		if (GetItemPrefab().CanChangeColor)
		{
			return true;
		}
		return IdenticalColors(item2);
	}

	public bool Equals_ColorsChangeNotPossible(Item item2)
	{
		if (GameId != item2.GameId)
		{
			return false;
		}
		return IdenticalColors(item2);
	}

	private bool IdenticalColors(Item item2)
	{
		ItemPrefab itemPrefab = GetItemPrefab();
		if (!itemPrefab.HasColor)
		{
			return true;
		}
		int num = itemPrefab.ColorSlots.Length;
		EnsureColorsSizeAtLeast(num);
		item2.EnsureColorsSizeAtLeast(num);
		for (int i = 0; i < num; i++)
		{
			Color color = _colors[i];
			Color color2 = item2._colors[i];
			if (color != color2)
			{
				return false;
			}
		}
		return true;
	}

	public bool Equals_UniqueGuid(Item item2)
	{
		return UniqueItemGuid == item2.UniqueItemGuid;
	}

	public bool Equals_Complex(Item item2, bool enforceUniqueId, bool checkColors)
	{
		if (GameId != item2.GameId)
		{
			return false;
		}
		if (enforceUniqueId && UniqueItemGuid != item2.UniqueItemGuid)
		{
			return false;
		}
		if (checkColors && !IdenticalColors(item2))
		{
			return false;
		}
		return true;
	}

	public Item UnpackClone(GameId id)
	{
		Item item = Clone();
		item._gameId = id;
		item.SetColors(_colors);
		if (item.GameId != GameId)
		{
			item._prefab = null;
		}
		if (item.SourceItemsUniqueGuids != null)
		{
			item.SourceItemsUniqueGuids = item.SourceItemsUniqueGuids.Append(UniqueItemGuid).ToArray();
		}
		else
		{
			item.SourceItemsUniqueGuids = new SerializableGuid[1] { UniqueItemGuid };
		}
		return item;
	}

	public Item Clone()
	{
		Item item = (Item)MemberwiseClone();
		CloneSpecialVariables(item);
		CloneItemModifiers(item);
		CloneDishItems(item);
		CloneColors(item);
		item.UniqueItemGuid = Guid.NewGuid();
		return item;
	}

	public void FillUIButtons(IModificationPeriodButtonList _itemStatsButtonList, ItemUIAllowedButtons itemUIAllowedButtons = ItemUIAllowedButtons.All)
	{
		ItemPrefab prefab = GetItemPrefab();
		if (itemUIAllowedButtons.HasFlagFast(ItemUIAllowedButtons.EquipUnequip))
		{
			if (IsEquipped())
			{
				ButtonListWrapper buttonListWrapper = _itemStatsButtonList.AddButton();
				buttonListWrapper.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_ButtonUnequip);
				buttonListWrapper.OnClick = () =>
				{
					UiOverlay.Instance.UnequipItemOrAsk(this);
				};
			}
			else if (prefab.Equipable)
			{
				ButtonListWrapper buttonListWrapper2 = _itemStatsButtonList.AddButton();
				buttonListWrapper2.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_ButtonEquip);
				buttonListWrapper2.OnClick = () =>
				{
					UiOverlay.Instance.EquipItem(this);
				};
			}
		}
		if (itemUIAllowedButtons.HasFlagFast(ItemUIAllowedButtons.Eat))
		{
			EdibleItemPrefabData edibleData = GetItemPrefab().EdibleData;
			if (edibleData != null)
			{
				EdibleItemNutritionProfile pooled;
				using (EdibleItemNutritionProfile.Pool.Acquire(out pooled))
				{
					edibleData.FillNutritionProfile(this, pooled);
					if (pooled.IsSafeToEat.Value)
					{
						ButtonListWrapper buttonListWrapper3 = _itemStatsButtonList.AddButton(7);
						buttonListWrapper3.SetTextFuncLocalizedString(LOC.UI.TupleReferences.Equipment_ButtonEat);
						buttonListWrapper3.OnClick = () =>
						{
							GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
							gameVariables.Eat(this);
							UiOverlay.Instance.GetStaticGUI<GameStaticGui>(out var staticGui);
							staticGui.SyncGui(gameVariables);
						};
					}
				}
			}
		}
		if (itemUIAllowedButtons.HasFlagFast(ItemUIAllowedButtons.Use) && CanBeUsed())
		{
			ButtonListWrapper buttonListWrapper4 = _itemStatsButtonList.AddButton(10);
			buttonListWrapper4.SetText(LOC.UI.Equipment_ButtonUse);
			buttonListWrapper4.OnClick = () =>
			{
				GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
				gameVariables.itemManager.UseItem(this);
				UiOverlay.Instance.GetStaticGUI<GameStaticGui>(out var staticGui);
				staticGui.SyncGui(gameVariables);
			};
		}
		if (itemUIAllowedButtons.HasFlagFast(ItemUIAllowedButtons.Unpack) && CanBeUnpacked())
		{
			ButtonListWrapper buttonListWrapper5 = _itemStatsButtonList.AddButton(15);
			buttonListWrapper5.SetText(LOC.UI.Unpack);
			buttonListWrapper5.OnClick = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.UnpackItem(this);
			};
		}
		if (itemUIAllowedButtons.HasFlagFast(ItemUIAllowedButtons.Modify) && prefab.HasAnyModifiableVariables)
		{
			ButtonListWrapper buttonListWrapper6 = _itemStatsButtonList.AddButton(20);
			buttonListWrapper6.SetText(LOC.UI.Equipment_ButtonModify);
			buttonListWrapper6.OnClick = () =>
			{
				List<PopupChoice> list = new List<PopupChoice>();
				for (int i = 0; i < prefab.ItemModifiableVariables.Length; i++)
				{
					ItemModifiableVariable itemModifiableVariable = prefab.ItemModifiableVariables[i];
					ItemModifiableVariableIntBackedBool itemModifiableVariableIntBackedBool = itemModifiableVariable as ItemModifiableVariableIntBackedBool;
					if (itemModifiableVariableIntBackedBool == null)
					{
						ItemModifiableVariableFloat01Percent itemModifiableVariableFloat01Percent = itemModifiableVariable as ItemModifiableVariableFloat01Percent;
						if (itemModifiableVariableFloat01Percent != null)
						{
							list.Add(new PopupChoice(itemModifiableVariableFloat01Percent.GetLocalizedName(), () =>
							{
								List<PopupChoice> list2 = new List<PopupChoice>();
								float[] array = new float[5] { 0f, 0.25f, 0.5f, 0.75f, 1f };
								for (int j = 0; j < array.Length; j++)
								{
									float value = array[j];
									list2.Add(new PopupChoice(value.ToString("P0"), () =>
									{
										itemModifiableVariableFloat01Percent.SetValue(this, value);
										GameVariables.Current.itemManager.ReportChangeInItemData(this);
									}));
								}
								UiOverlay.Instance.OpenContextMenuPopup(list2);
							}));
						}
					}
					else if (itemModifiableVariableIntBackedBool.GetValue(this))
					{
						list.Add(new PopupChoice(LOC.UI.VariableBoolDisableContextMenu(itemModifiableVariable.GetLocalizedName()), () =>
						{
							prefab.SetBoolModifiableVariableRespectRequirements(this, itemModifiableVariableIntBackedBool, newValue: false);
							GameVariables.Current.itemManager.ReportChangeInItemData(this);
						})
						{
							Interactable = prefab.BoolModifiableVariableChangeAllowed(this, i, newValue: false),
							ButtonColorType = CommonButtonColorType.Red
						});
					}
					else
					{
						list.Add(new PopupChoice(LOC.UI.VariableBoolEnableContextMenu(itemModifiableVariable.GetLocalizedName()), () =>
						{
							prefab.SetBoolModifiableVariableRespectRequirements(this, itemModifiableVariableIntBackedBool, newValue: true);
							GameVariables.Current.itemManager.ReportChangeInItemData(this);
						})
						{
							Interactable = prefab.BoolModifiableVariableChangeAllowed(this, i, newValue: true),
							ButtonColorType = CommonButtonColorType.Green
						});
					}
				}
				UiOverlay.Instance.OpenContextMenuPopup(list);
			};
		}
		if (!itemUIAllowedButtons.HasFlagFast(ItemUIAllowedButtons.ThrowOut) || prefab.ThrowingOutForbidden || IsEquipped())
		{
			return;
		}
		ButtonListWrapper buttonListWrapper7 = _itemStatsButtonList.AddButton(20, CommonButtonColorType.Red);
		buttonListWrapper7.SetText(LOC.UI.Equipment_ButtonThrowAway);
		buttonListWrapper7.OnClick = () =>
		{
			if (!IsEquipped())
			{
				if (IsIllegal)
				{
					UiOverlay.Instance.CloseUiOverlay();
					SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusSetCustomText(GetName());
					SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(FungusBlocks.Trash_Throw_away, () =>
					{
						if (SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.GetIntegerVariable("Integer") == 0)
						{
							SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.RemoveItem(this);
						}
					});
				}
				else
				{
					UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Equipment_ThrowAwayPrompt(GetName()), () =>
					{
						SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.RemoveItem(this);
					});
				}
			}
			else
			{
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Failed, LOC.UI.Equipment_ThrowAwayUnequipFirst);
			}
		};
	}
}
