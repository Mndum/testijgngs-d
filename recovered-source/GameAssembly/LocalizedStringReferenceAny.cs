using System;
using UnityEngine.Localization.Tables;

[Serializable]
public class LocalizedStringReferenceAny : LocalizedStringReference
{
	public TableReference TableReference;

	public override TableReference TargetTable => TableReference;

	public LocalizedStringReferenceAny(TableReference tableReference)
	{
		TableReference = tableReference;
	}

	public LocalizedStringReferenceAny Clone()
	{
		return (LocalizedStringReferenceAny)MemberwiseClone();
	}
}
