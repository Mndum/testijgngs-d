using System;
using UnityEngine;

[Serializable]
public class EmoteData : StoryEmoteItemReference
{
	[SerializeField]
	public bool instant;

	public const string InstantArgument = "instant";

	public bool IsInstant => instant;

	public EmoteData()
	{
	}

	public EmoteData(SimpleCharacter.ISimpleCharacterSlot slot, SimpleCharacter.ISimpleCharacterItem item, bool instant = false)
		: base(slot, item)
	{
		this.instant = instant;
	}

	public EmoteData(string slotId, string itemId, bool instant = false)
		: base(slotId, itemId)
	{
		this.instant = instant;
	}

	public EmoteData(string slotId, string itemId)
	{
		SlotId = slotId;
		ItemId = itemId;
	}

	public override string ToString()
	{
		if (!instant)
		{
			return ToStringNoArguments();
		}
		return SlotId + "." + ItemId + "(instant)";
	}

	public string ToStringNoArguments()
	{
		return SlotId + "." + ItemId;
	}
}
