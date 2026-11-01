using System;
using UnityEngine.Localization.Metadata;

[Serializable]
[Metadata(AllowedTypes = MetadataType.Locale)]
public class LocaleTranslationProgress : IMetadata
{
	public float fullProgress;

	public float fullProgressMT;

	public float RelativeProgress => 1f - fullProgressMT / fullProgress;
}
