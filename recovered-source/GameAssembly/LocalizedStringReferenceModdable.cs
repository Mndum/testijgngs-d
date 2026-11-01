using System;
using UnityEngine.Localization.Tables;

[Serializable]
public class LocalizedStringReferenceModdable : LocalizedStringReference
{
	public override TableReference TargetTable => LOC.Moddable.TABLE_REFERENCE;

	public LocalizedStringReferenceModdable Clone()
	{
		return (LocalizedStringReferenceModdable)MemberwiseClone();
	}
}
