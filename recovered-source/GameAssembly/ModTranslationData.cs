using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModTranslationData : ISerializationCallbackReceiver
{
	[SerializeField]
	private string TargetModVersion;

	private ParsedVersion _parsedTargetModVersion;

	public SerializableGuid TargetModGuid;

	public string LocaleKeyCode;

	public string ShortNotes;

	public List<ModTranslationRow> TranslationRows;

	public ParsedVersion ParsedTargetModVersion
	{
		get
		{
			return _parsedTargetModVersion;
		}
		set
		{
			_parsedTargetModVersion = value;
			TargetModVersion = _parsedTargetModVersion.StringRepresentation;
		}
	}

	public bool IsValid()
	{
		if (TranslationRows.Count > 0 && TargetModGuid.RealGuid != Guid.Empty)
		{
			return !string.IsNullOrWhiteSpace(LocaleKeyCode);
		}
		return false;
	}

	public string ToJsonPrettyPrint()
	{
		return JsonUtility.ToJson(this, prettyPrint: true);
	}

	public string GetDefaultFileName()
	{
		return SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.GetRememberedNameOrGuid(TargetModGuid).TruncateLongString(36) + " translation - " + LocaleKeyCode + ".json";
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		ParsedTargetModVersion = ParsedVersion.CreateFromString(TargetModVersion);
	}
}
