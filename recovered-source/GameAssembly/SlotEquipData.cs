using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemConditions;
using JetBrains.Annotations;
using MoonSharp.Interpreter.Interop;
using UnityEngine;

public class SlotEquipData : LuaUtilities.ILuaTransferable, ICloneable<SlotEquipData>, ICloneable
{
	private ItemScopeCondition[] _elementActiveConditions = Array.Empty<ItemScopeCondition>();

	private ItemScopeCondition[] _textureActiveConditions = Array.Empty<ItemScopeCondition>();

	public HashSet<ItemSlot> RequiredSlots = new HashSet<ItemSlot>();

	public HashSet<ItemSlot> ForbiddenSlots = new HashSet<ItemSlot>();

	public bool IsValidSlot => TargetSlot != null;

	public ItemSlot TargetSlot
	{
		[CanBeNull]
		get;
		[MoonSharpVisible(true)]
		set;
	}

	public string TargetSlotString
	{
		[CanBeNull]
		get
		{
			return TargetSlot?.Id;
		}
		[MoonSharpVisible(true)]
		set
		{
			TargetSlot = ItemSlot.SlotManager.GetSlotPossibleNull(value);
		}
	}

	public string[] ControllerElementOverride
	{
		[CanBeNull]
		get;
		[MoonSharpVisible(true)]
		set;
	}

	public ItemScopeCondition[] ElementActiveConditions
	{
		get
		{
			return _elementActiveConditions;
		}
		set
		{
			_elementActiveConditions = value ?? Array.Empty<ItemScopeCondition>();
		}
	}

	public ItemScopeCondition[] TextureActiveConditions
	{
		get
		{
			return _textureActiveConditions;
		}
		set
		{
			_textureActiveConditions = value ?? Array.Empty<ItemScopeCondition>();
		}
	}

	[UsedInLua]
	public static SlotEquipData CreateInstance(string slotString = null)
	{
		return new SlotEquipData
		{
			TargetSlotString = slotString
		};
	}

	public void FillCurrent(Live2DController.ItemWithCurrentData itemWithCurrentData, string controllerEnumId)
	{
		Item item = itemWithCurrentData.Item;
		ItemPrefab prefab = itemWithCurrentData.Prefab;
		List<PackedTexturesContainer.PackedTexture> activeTextures = itemWithCurrentData.ActiveTextures;
		PackedTexturesContainer[] textures = prefab.Textures;
		if (textures != null && textures.Length > 0)
		{
			for (int i = 0; i < textures.Length; i++)
			{
				if ((object)textures[i] == null || (_textureActiveConditions.TryGetValue(i, out var value) && !value.IsActive(item)))
				{
					continue;
				}
				foreach (PackedTexturesContainer.PackedTexture packedTexture in textures[i].PackedTextures)
				{
					if (packedTexture.HasTarget(controllerEnumId))
					{
						activeTextures.Add(packedTexture);
					}
				}
			}
		}
		List<string> activeElements = itemWithCurrentData.ActiveElements;
		FillActiveElementsDefault(activeElements);
		for (int num = Mathf.Min(activeElements.Count, _elementActiveConditions.Length) - 1; num >= 0; num--)
		{
			if (!_elementActiveConditions[num].IsActive(item))
			{
				activeElements.RemoveAtSwapBack(num);
			}
		}
	}

	public bool CanGoInto(ItemSlot slot1)
	{
		return TargetSlot == slot1;
	}

	public bool CanGoInto(ItemSlot slot1, ItemSlot slot2)
	{
		if (TargetSlot != slot1)
		{
			return TargetSlot == slot2;
		}
		return true;
	}

	public bool CanGoInto(ItemSlot slot1, ItemSlot slot2, ItemSlot slot3)
	{
		if (TargetSlot != slot1 && TargetSlot != slot2)
		{
			return TargetSlot == slot3;
		}
		return true;
	}

	public bool CanGoInto(ItemSlot[] bodySizeModuleSlots)
	{
		for (int i = 0; i < bodySizeModuleSlots.Length; i++)
		{
			if (TargetSlot == bodySizeModuleSlots[i])
			{
				return true;
			}
		}
		return false;
	}

	public void AddRequiredSlotString(string requiredSlot)
	{
		RequiredSlots.Add(ItemSlot.SlotManager.GetSlot(requiredSlot));
	}

	public void AddRequiredSlotsString(IEnumerable<string> requiredSlots)
	{
		foreach (string requiredSlot in requiredSlots)
		{
			AddRequiredSlotString(requiredSlot);
		}
	}

	public void SetRequiredSlotsString(IEnumerable<string> requiredSlots)
	{
		RequiredSlots.Clear();
		AddRequiredSlotsString(requiredSlots);
	}

	public void AddForbiddenSlotString(string forbiddenSlot)
	{
		ForbiddenSlots.Add(ItemSlot.SlotManager.GetSlot(forbiddenSlot));
	}

	public void AddForbiddenSlotsString(IEnumerable<string> forbiddenSlots)
	{
		foreach (string forbiddenSlot in forbiddenSlots)
		{
			AddForbiddenSlotString(forbiddenSlot);
		}
	}

	public void SetForbiddenSlotsString(IEnumerable<string> forbiddenSlots)
	{
		ForbiddenSlots.Clear();
		AddForbiddenSlotsString(forbiddenSlots);
	}

	public PooledList<string> FillActiveElementsDefaultPooled(out List<string> elements)
	{
		PooledList<string> list = MyCollectionPool<string>.GetList(out elements);
		FillActiveElementsDefault(elements);
		return list;
	}

	public void FillActiveElementsDefault(List<string> output)
	{
		if (TargetSlot != null)
		{
			if (ControllerElementOverride != null)
			{
				output.AddRange(ControllerElementOverride);
			}
			else
			{
				output.Add(TargetSlot.Id);
			}
		}
	}

	public string ToLuaString()
	{
		bool flag = TargetSlot != null;
		string[] controllerElementOverride = ControllerElementOverride;
		bool flag2 = controllerElementOverride != null && controllerElementOverride.Length > 0;
		bool flag3 = RequiredSlots.Count > 0;
		bool flag4 = ForbiddenSlots.Count > 0;
		bool flag5 = ElementActiveConditions.Length != 0 || TextureActiveConditions.Length != 0;
		if (!flag3 && !flag4 && !flag2 && !flag5)
		{
			return "SlotEquipData.CreateInstance(" + TargetSlotString.ToLuaString() + ")";
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("(function()");
			stringBuilder.AppendLine("\tlocal sd = SlotEquipData.CreateInstance()");
			if (flag)
			{
				stringBuilder.AppendLine("\tsd.TargetSlotString = " + TargetSlotString.ToLuaString());
			}
			if (flag2)
			{
				stringBuilder.AppendLine("\tsd.ControllerElementOverride = " + ControllerElementOverride.ToLuaString());
			}
			if (flag3)
			{
				stringBuilder.AppendLine("\tsd.SetRequiredSlotsString(" + RequiredSlots.Select((ItemSlot x) => x.Id).ToLuaString() + ")");
			}
			if (flag4)
			{
				stringBuilder.AppendLine("\tsd.SetForbiddenSlotsString(" + ForbiddenSlots.Select((ItemSlot x) => x.Id).ToLuaString() + ")");
			}
			if (flag5)
			{
				stringBuilder.AppendLine("\tsd.ElementActiveConditions = " + ElementActiveConditions.ToLuaStringTransferable());
				stringBuilder.AppendLine("\tsd.TextureActiveConditions = " + TextureActiveConditions.ToLuaStringTransferable());
			}
			stringBuilder.Append("\treturn sd end)()");
			return stringBuilder.ToString();
		}
	}

	public SlotEquipData Clone()
	{
		SlotEquipData slotEquipData = new SlotEquipData();
		slotEquipData.TargetSlot = TargetSlot;
		slotEquipData.ControllerElementOverride = ControllerElementOverride?.CloneConstElementArray();
		slotEquipData.RequiredSlots.UnionWith(RequiredSlots);
		slotEquipData.ForbiddenSlots.UnionWith(ForbiddenSlots);
		slotEquipData.ElementActiveConditions = ElementActiveConditions?.CloneCloneableElementArray();
		slotEquipData.TextureActiveConditions = TextureActiveConditions?.CloneCloneableElementArray();
		return slotEquipData;
	}
}
