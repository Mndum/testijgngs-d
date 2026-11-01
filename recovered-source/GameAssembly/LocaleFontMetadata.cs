using System;
using System.Collections.Generic;
using System.Linq;
using IC.AutomaticReference;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Settings;

[Serializable]
[Metadata(AllowedTypes = MetadataType.Locale)]
public class LocaleFontMetadata : IMetadata
{
	public enum FontType
	{
		MainFont,
		CommisionerFont,
		NotoSans,
		Fontarto,
		Monospace
	}

	private static Dictionary<(Locale, FontType), TMP_FontAsset> _fontAssets = new Dictionary<(Locale, FontType), TMP_FontAsset>();

	public SerializableAutomaticReference<TMP_FontAsset> mainFontRef;

	public SerializableAutomaticReference<TMP_FontAsset> commisionerFontRef;

	public SerializableAutomaticReference<TMP_FontAsset> notoSansFontRef;

	public SerializableAutomaticReference<TMP_FontAsset> fontartoFontRef;

	public SerializableAutomaticReference<TMP_FontAsset> monospaceFontRef;

	public static FontType[] AllFontTypes()
	{
		return (FontType[])Enum.GetValues(typeof(FontType)).Cast<FontType>();
	}

	private static List<LocaleFontMetadata> GetLocaleFontFallbacks(Locale locale)
	{
		return (from l in locale.GetFallbacks()
			select l.Metadata.GetMetadata<LocaleFontMetadata>() into f
			where f != null
			select f).ToList();
	}

	public static TMP_FontAsset GetFontCurrentLocale(FontType fontType)
	{
		return GetFont(LocalizationSettings.SelectedLocale, fontType);
	}

	public static void ClearFontCache()
	{
		_fontAssets.Clear();
	}

	public static TMP_FontAsset GetFont(Locale locale, FontType fontType)
	{
		if (_fontAssets.TryGetValue((locale, fontType), out var value))
		{
			return value;
		}
		return _fontAssets[(locale, fontType)] = GetFontInner(locale, fontType);
	}

	public static TMP_FontAsset GetFontInner(Locale locale, FontType fontType)
	{
		LocaleFontMetadata metadata = locale.Metadata.GetMetadata<LocaleFontMetadata>();
		if (metadata != null)
		{
			TMP_FontAsset font = metadata.GetFont(fontType);
			if (font != null)
			{
				return font;
			}
		}
		foreach (LocaleFontMetadata localeFontFallback in GetLocaleFontFallbacks(locale))
		{
			TMP_FontAsset font2 = localeFontFallback.GetFont(fontType);
			if (font2 != null)
			{
				return font2;
			}
		}
		Debug.LogError($"Font {fontType} not found for {locale}!");
		return null;
	}

	public TMP_FontAsset GetFont(FontType fontType)
	{
		return fontType switch
		{
			FontType.CommisionerFont => commisionerFontRef.Get(), 
			FontType.NotoSans => notoSansFontRef.Get(), 
			FontType.Fontarto => fontartoFontRef.Get(), 
			FontType.Monospace => monospaceFontRef.Get(), 
			_ => mainFontRef.Get(), 
		};
	}

	public static string GetExtraCharsForFont(FontType fontType)
	{
		if (fontType != FontType.Monospace)
		{
			return "";
		}
		return "♥█▒";
	}
}
