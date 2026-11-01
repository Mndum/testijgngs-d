using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class SerializedLocalizedConversation : LocalizedConversation, IHasConversationReferenceNotBase<Func<int, string>>
{
	[HideInInspector]
	public List<LocalizedStringReferenceStory> localizedLines = new List<LocalizedStringReferenceStory>();

	public override IEnumerable<LocalizedStringReference> GetLocalizedLines()
	{
		return localizedLines;
	}

	public override void CreateLinesAtEnd(int count)
	{
		for (int i = 0; i < count; i++)
		{
			localizedLines.Add(new LocalizedStringReferenceStory());
		}
	}

	public bool UpdateKeyNames(Func<int, string> keyNameGenerator)
	{
		bool flag = false;
		for (int i = 0; i < localizedLines.Count; i++)
		{
			flag |= localizedLines[i].UpdateKeyName(keyNameGenerator(i));
		}
		MarkDirty();
		return flag;
	}

	public void ClearWithoutDeletingEntries()
	{
		foreach (LocalizedStringReferenceStory localizedLine in localizedLines)
		{
			localizedLine.ClearWithoutDeletingEntry();
		}
		MarkDirty();
	}

	public void ClearAndDeleteEntries()
	{
		foreach (LocalizedStringReferenceStory localizedLine in localizedLines)
		{
			localizedLine.Delete();
		}
		MarkDirty();
	}
}
