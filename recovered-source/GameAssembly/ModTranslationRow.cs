using System;

[Serializable]
public struct ModTranslationRow(string key, string defaultString)
{
	public const string NoTranslationPlaceholder = "PUT TRANSLATED TEXT HERE";

	public string Key = key;

	public string DefaultString = defaultString;

	public string TranslatedString = "PUT TRANSLATED TEXT HERE";

	public bool HasBeenTranslated()
	{
		return TranslatedString != "PUT TRANSLATED TEXT HERE";
	}
}
