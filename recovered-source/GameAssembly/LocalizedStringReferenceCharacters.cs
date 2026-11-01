using System;
using UnityEngine.Localization.Tables;

[Serializable]
public class LocalizedStringReferenceCharacters : LocalizedStringReference
{
	public override TableReference TargetTable => LOC.Common.TABLE_REFERENCE;

	public LocalizedStringReferenceCharacters Clone()
	{
		return (LocalizedStringReferenceCharacters)MemberwiseClone();
	}
}
