using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class StoryEmoteItemReference
{
	private sealed class SlotIdItemIdEqualityComparer : IEqualityComparer<StoryEmoteItemReference>
	{
		public bool Equals(StoryEmoteItemReference x, StoryEmoteItemReference y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null)
			{
				return false;
			}
			if (y == null)
			{
				return false;
			}
			if (string.Equals(x.SlotId, y.SlotId, StringComparison.OrdinalIgnoreCase))
			{
				return string.Equals(x.ItemId, y.ItemId, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		public int GetHashCode(StoryEmoteItemReference obj)
		{
			return HashCode.Combine(obj.SlotId.GetHashCode(StringComparison.OrdinalIgnoreCase), obj.ItemId.GetHashCode(StringComparison.OrdinalIgnoreCase));
		}
	}

	[FormerlySerializedAs("slotId")]
	[SerializeField]
	public string SlotId;

	[FormerlySerializedAs("itemId")]
	[SerializeField]
	public string ItemId;

	public static IEqualityComparer<StoryEmoteItemReference> SlotIdItemIdComparer { get; } = new SlotIdItemIdEqualityComparer();

	public StoryEmoteItemReference()
	{
	}

	public StoryEmoteItemReference(SimpleCharacter.ISimpleCharacterSlot slot, SimpleCharacter.ISimpleCharacterItem item)
		: this(slot.GetId(), item.GetId())
	{
	}

	public StoryEmoteItemReference(string slotId, string itemId)
	{
		SlotId = slotId;
		ItemId = itemId;
	}

	public override string ToString()
	{
		return SlotId + "." + ItemId;
	}

	public EmoteData ToActivationData(bool instant)
	{
		return new EmoteData(SlotId, ItemId, instant);
	}
}
