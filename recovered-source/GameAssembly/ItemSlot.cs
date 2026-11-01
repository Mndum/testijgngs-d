using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

public sealed class ItemSlot
{
	public enum SlotType
	{
		BotPart,
		BotClothes,
		BotHair,
		BotSomething,
		Other,
		Unknown
	}

	private sealed class IdRelationalComparer : IComparer<ItemSlot>
	{
		public int Compare(ItemSlot x, ItemSlot y)
		{
			if (x == y)
			{
				return 0;
			}
			if (y == null)
			{
				return 1;
			}
			if (x == null)
			{
				return -1;
			}
			return string.Compare(x.Id, y.Id, StringComparison.Ordinal);
		}
	}

	public class SlotManager
	{
		private static Dictionary<string, ItemSlot> _specialSlots = new Dictionary<string, ItemSlot>();

		public static readonly ItemSlot LegL = CreateInstance("LegL", LOC.UI.TupleReferences.Slot_LegL);

		public static readonly ItemSlot LegR = CreateInstance("LegR", LOC.UI.TupleReferences.Slot_LegR);

		public static readonly ItemSlot ShoeL = CreateInstance("ShoeL", LOC.UI.TupleReferences.Slot_ShoeL);

		public static readonly ItemSlot ShoeR = CreateInstance("ShoeR", LOC.UI.TupleReferences.Slot_ShoeR);

		public static readonly ItemSlot StockingL = CreateInstance("StockingL", LOC.UI.TupleReferences.Slot_StockingL);

		public static readonly ItemSlot StockingR = CreateInstance("StockingR", LOC.UI.TupleReferences.Slot_StockingR);

		public static readonly ItemSlot ArmL = CreateInstance("ArmL", LOC.UI.TupleReferences.Slot_ArmL);

		public static readonly ItemSlot ArmR = CreateInstance("ArmR", LOC.UI.TupleReferences.Slot_ArmR);

		public static readonly ItemSlot Skin = CreateInstance("Skin", LOC.UI.TupleReferences.Slot_Skin);

		public static readonly ItemSlot Eyes = CreateInstance("Eyes", LOC.UI.TupleReferences.Slot_Eyes);

		public static readonly ItemSlot HeartPupils = CreateInstance("HeartPupils", LOC.UI.TupleReferences.Slot_HeartPupils);

		public static readonly ItemSlot Flesh = CreateInstance("Flesh", LOC.UI.TupleReferences.Slot_Flesh);

		public static readonly ItemSlot Head = CreateInstance("Head", LOC.UI.TupleReferences.Slot_Head);

		public static readonly ItemSlot Ears = CreateInstance("Ears", LOC.UI.TupleReferences.Slot_Ears);

		public static readonly ItemSlot Teeth = CreateInstance("Teeth", LOC.UI.TupleReferences.Slot_Teeth);

		public static readonly ItemSlot Tail = CreateInstance("Tail", LOC.UI.TupleReferences.Slot_Tail);

		public static readonly ItemSlot Hologram = CreateInstance("Hologram", LOC.UI.TupleReferences.Slot_Hologram);

		public static readonly ItemSlot HairMain = CreateInstance("HairMain", LOC.UI.TupleReferences.Slot_Hair, SlotType.BotHair);

		public static readonly ItemSlot HairAdditional = CreateInstance("HairAdditional", LOC.UI.TupleReferences.Slot_HairAdditional, SlotType.BotHair);

		public static readonly ItemSlot HairAugmentation = CreateInstance("HairAugmentation", LOC.UI.TupleReferences.Slot_HairAugmentation, SlotType.BotHair);

		public static readonly ItemSlot FeetBodyModules = CreateInstance("FeetBodyModules", LOC.UI.TupleReferences.Slot_FeetBodyModules);

		public static readonly ItemSlot ThighBodyModules = CreateInstance("ThighBodyModules", LOC.UI.TupleReferences.Slot_ThighBodyModules);

		public static readonly ItemSlot AssBodyModules = CreateInstance("AssBodyModules", LOC.UI.TupleReferences.Slot_AssBodyModules);

		public static readonly ItemSlot CalfBodyModules = CreateInstance("CalfBodyModules", LOC.UI.TupleReferences.Slot_CalfBodyModules);

		public static readonly ItemSlot BoobsBodyModules = CreateInstance("BoobsBodyModules", LOC.UI.TupleReferences.Slot_BoobBodyModules);

		public static readonly ItemSlot[] BodySizeModuleSlots = new ItemSlot[5] { FeetBodyModules, ThighBodyModules, CalfBodyModules, BoobsBodyModules, AssBodyModules };

		public static readonly ItemSlot PersonalityModule = CreateInstance("PersonalityModule", LOC.UI.TupleReferences.Slot_PersonalityModule);

		public static readonly ItemSlot UpperBody = CreateInstance("UpperBody", LOC.UI.TupleReferences.Slot_UpperBody, SlotType.BotClothes);

		public static readonly ItemSlot LowerBody = CreateInstance("LowerBody", LOC.UI.TupleReferences.Slot_LowerBody, SlotType.BotClothes);

		public static readonly ItemSlot UpperBodyOuter = CreateInstance("UpperBodyOuter", LOC.UI.TupleReferences.Slot_UpperBodyOuter, SlotType.BotClothes);

		public static readonly ItemSlot Bra = CreateInstance("Bra", LOC.UI.TupleReferences.Slot_Bra, SlotType.BotClothes);

		public static readonly ItemSlot Panties = CreateInstance("Panties", LOC.UI.TupleReferences.Slot_Panties, SlotType.BotClothes);

		public static readonly ItemSlot HairAccessoryL = CreateInstance("HairAccessoryL", LOC.UI.TupleReferences.Slot_HairAccessoryL, SlotType.BotClothes);

		public static readonly ItemSlot Manicure = CreateInstance("Manicure", LOC.UI.TupleReferences.Slot_Manicure, SlotType.BotClothes);

		public static readonly ItemSlot Pedicure = CreateInstance("Pedicure", LOC.UI.TupleReferences.Slot_Pedicure, SlotType.BotClothes);

		public static readonly ItemSlot NeckAccessory = CreateInstance("NeckAccessory", LOC.UI.TupleReferences.Slot_NeckAccessory, SlotType.BotClothes);

		public static readonly ItemSlot Glasses = CreateInstance("Glasses", LOC.UI.TupleReferences.Slot_Glasses, SlotType.BotClothes);

		public static readonly ItemSlot TongueAccessory = CreateInstance("TongueAccessory", LOC.UI.TupleReferences.Slot_TongueAccessory, SlotType.BotClothes);

		public static readonly ItemSlot EyeMakeup = CreateInstance("EyeMakeup", LOC.UI.TupleReferences.Slot_EyeMakeup, SlotType.BotClothes);

		public static readonly ItemSlot Septum = CreateInstance("Septum", LOC.UI.TupleReferences.Slot_Septum, SlotType.BotClothes);

		public static readonly ItemSlot Lips = CreateInstance("Lips", LOC.UI.TupleReferences.Slot_Lips, SlotType.BotClothes);

		public static readonly ItemSlot Navel = CreateInstance("Navel", LOC.UI.TupleReferences.Slot_Navel, SlotType.BotClothes);

		public static readonly ItemSlot NippleLeft = CreateInstance("NippleLeft", LOC.UI.TupleReferences.Slot_NippleLeft, SlotType.BotClothes);

		public static readonly ItemSlot NippleRight = CreateInstance("NippleRight", LOC.UI.TupleReferences.Slot_NippleRight, SlotType.BotClothes);

		public static readonly string AnonSlotStart = "Anon_";

		public static readonly ItemSlot Anon_Glasses = CreateInstance(AnonSlotStart + "Glasses", LOC.UI.TupleReferences.Slot_Anon_Glasses, SlotType.Other);

		public static readonly ItemSlot Anon_FishingRod = CreateInstance(AnonSlotStart + "FishingRod", LOC.UI.TupleReferences.FishingFishingRod, SlotType.Other);

		public static readonly ItemSlot Anon_FishingBait = CreateInstance(AnonSlotStart + "FishingBait", LOC.UI.TupleReferences.FishingBait, SlotType.Other);

		public static readonly string RoomSlotStart = "Room_";

		public static readonly ItemSlot Room_Mat = CreateInstance(RoomSlotStart + "Mat", LOC.UI.TupleReferences.Slot_Mat, SlotType.Other);

		public static readonly ItemSlot Room_Plush = CreateInstance(RoomSlotStart + "Plush", LOC.UI.TupleReferences.Slot_Plush, SlotType.Other);

		public static readonly ItemSlot Room_Banner = CreateInstance(RoomSlotStart + "Banner", LOC.UI.TupleReferences.Slot_Banner, SlotType.Other);

		public static readonly ItemSlot Room_WallDecoration = CreateInstance(RoomSlotStart + "WallDecoration", LOC.UI.TupleReferences.Slot_WallDecoration, SlotType.Other);

		private static SlotType DetermineSlotTypeByString(string slotId)
		{
			if (slotId.StartsWith(AnonSlotStart) || slotId.StartsWith(RoomSlotStart))
			{
				return SlotType.Other;
			}
			return SlotType.BotSomething;
		}

		public static SlotType GetSlotType(string slotId)
		{
			if (slotId == null)
			{
				return SlotType.Other;
			}
			if (_specialSlots.TryGetValue(slotId, out var value))
			{
				return value.Type;
			}
			return DetermineSlotTypeByString(slotId);
		}

		public static bool TryGetSlot(string slotId, out ItemSlot itemSlot)
		{
			return _specialSlots.TryGetValue(slotId, out itemSlot);
		}

		public static ItemSlot GetSlot(string slotId)
		{
			if (_specialSlots.TryGetValue(slotId, out var value))
			{
				return value;
			}
			return CreateInstance(slotId, SimpleLocalizedString.CreateInstanceReplacement(slotId), GetSlotType(slotId));
		}

		[CanBeNull]
		public static ItemSlot GetSlotPossibleNull(string slotId)
		{
			if (string.IsNullOrEmpty(slotId))
			{
				return null;
			}
			return GetSlot(slotId);
		}

		public static void AddSlot(ItemSlot slot)
		{
			_specialSlots.Add(slot.Id, slot);
		}

		[LoadableStaticMethod(null)]
		private static UniTask Load()
		{
			HairMain.AllowMultipleItems();
			HairAdditional.AllowMultipleItems();
			EyeMakeup.AllowMultipleItems();
			StockingL.MakeDependent(LegL);
			StockingR.MakeDependent(LegR);
			ShoeL.MakeDependent(LegL);
			ShoeR.MakeDependent(LegR);
			FeetBodyModules.MakeDependent(LegL);
			FeetBodyModules.MakeDependent(LegR);
			CalfBodyModules.MakeDependent(LegL);
			CalfBodyModules.MakeDependent(LegR);
			return UniTask.CompletedTask;
		}
	}

	public readonly string Id;

	public SimpleLocalizedString FriendlyNameSls;

	public SlotType Type;

	private List<ItemSlot> _needed = new List<ItemSlot>();

	private List<ItemSlot> _needThis = new List<ItemSlot>();

	public static IComparer<ItemSlot> IdComparer { get; } = new IdRelationalComparer();

	public string FriendlyName => FriendlyNameSls.Value;

	public IReadOnlyCollection<ItemSlot> Needed => _needed;

	public IReadOnlyCollection<ItemSlot> NeedThis => _needThis;

	public bool AllowedMultipleItems { get; private set; }

	private ItemSlot(string id, SimpleLocalizedString nameSls, SlotType type = SlotType.BotPart)
	{
		Id = id;
		FriendlyNameSls = nameSls;
		Type = type;
		SlotManager.AddSlot(this);
	}

	public static ItemSlot CreateInstance(string id, SimpleLocalizedString friendlyName, SlotType type = SlotType.BotPart)
	{
		return new ItemSlot(id, friendlyName, type);
	}

	public static bool IsBotSlot(SlotType slotType)
	{
		if (slotType != SlotType.BotPart && slotType != SlotType.BotClothes && slotType != SlotType.BotSomething)
		{
			return slotType == SlotType.BotHair;
		}
		return true;
	}

	public static bool IsBotSlot(string slot)
	{
		SlotType slotType = SlotManager.GetSlotType(slot);
		if (slotType != SlotType.BotPart && slotType != SlotType.BotClothes && slotType != SlotType.BotSomething)
		{
			return slotType == SlotType.BotHair;
		}
		return true;
	}

	public void AllowMultipleItems()
	{
		AllowedMultipleItems = true;
	}

	public void MakeDependent(ItemSlot on)
	{
		on._needThis.Add(this);
		_needed.Add(on);
	}

	public bool IsDependantOn(string slotName)
	{
		return _needed.Any((ItemSlot t) => t.Id == slotName);
	}
}
