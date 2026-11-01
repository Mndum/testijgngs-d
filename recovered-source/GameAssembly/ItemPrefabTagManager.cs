using System.Linq;
using UnityEngine;

public class ItemPrefabTagManager
{
	public bool IsValid(ItemPrefab itemPrefab, ItemPrefabTag itemPrefabTag)
	{
		switch (itemPrefabTag)
		{
		case ItemPrefabTag.None:
			return false;
		case ItemPrefabTag.Doggy_Skirt_AlwaysUp:
		{
			if (itemPrefab.IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot == ItemSlot.SlotManager.LowerBody && slotEquipData.ControllerElementOverride.Contains(Live2DController.SkirtId))
			{
				return true;
			}
			Debug.LogError($"{itemPrefabTag} tag is only valid on skirts! (Target slot = {ItemSlot.SlotManager.LowerBody.Id}, Element = {Live2DController.SkirtId})");
			return false;
		}
		default:
			return false;
		}
	}
}
