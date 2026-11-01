using System;
using UnityEngine;
using UnityEngine.Localization.Metadata;

[Serializable]
[Metadata(AllowedTypes = MetadataType.Locale)]
public class LocaleName : IMetadata
{
	[SerializeField]
	public string name;

	[SerializeField]
	public string nameInEnglish;
}
