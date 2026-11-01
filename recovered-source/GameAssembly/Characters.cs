using System;
using UnityEngine;
using UnityEngine.Localization.Metadata;
using UnityEngine.Serialization;

[Serializable]
[Metadata(AllowedTypes = MetadataType.Locale)]
public class Characters : IMetadata
{
	[FormerlySerializedAs("m_Characters")]
	[SerializeField]
	[Multiline]
	public string characters;
}
