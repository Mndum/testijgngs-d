using System;
using UnityEngine.Localization.Tables;

[Serializable]
public class LocalizedStringReferenceStory : LocalizedStringReference
{
	public override TableReference TargetTable => LOC.Story.TABLE_REFERENCE;

	public LocalizedStringReferenceStory Clone()
	{
		return (LocalizedStringReferenceStory)MemberwiseClone();
	}

	public void ReportSeen()
	{
		SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.Save.CheckIfUnseenStoryTextIdReport(KeyId);
	}
}
