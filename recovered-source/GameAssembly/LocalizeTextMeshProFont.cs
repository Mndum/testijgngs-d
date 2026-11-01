using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizeTextMeshProFont : MonoBehaviour, ILocaleChangedReceiver
{
	[SerializeField]
	private LocaleFontMetadata.FontType _fontType;

	[SerializeField]
	[ReadOnly]
	private TextMeshProUGUI textMeshProUGUI;

	private Locale _currentLocale;

	private static readonly HashSet<string> BlackFonartoIdentifiers = new HashSet<string> { "ja", "zh-Hans", "ko", "th", "zh-Hant" };

	public void OnValidate()
	{
		textMeshProUGUI = GetComponent<TextMeshProUGUI>();
	}

	private void ApplyLocale(Locale locale)
	{
		TMP_FontAsset font = LocaleFontMetadata.GetFont(locale, _fontType);
		if ((object)font == null)
		{
			return;
		}
		textMeshProUGUI.font = font;
		textMeshProUGUI.material = font.material;
		if (_fontType == LocaleFontMetadata.FontType.Fontarto)
		{
			if (BlackFonartoIdentifiers.Contains(locale.Identifier.Code))
			{
				textMeshProUGUI.color = Color.black;
			}
			else
			{
				textMeshProUGUI.color = Color.white;
			}
		}
	}

	public void OnLocaleChanged(Locale newLocale)
	{
		SelectedLocaleChanged(newLocale);
	}

	private void SelectedLocaleChanged(Locale locale)
	{
		if ((object)_currentLocale != locale)
		{
			_currentLocale = locale;
			ApplyLocale(locale);
		}
	}

	public void UpdateLocale()
	{
		Locale selectedLocale = LocalizationSettings.SelectedLocale;
		SelectedLocaleChanged(selectedLocale);
	}

	private void OnEnable()
	{
		LocalizationSingleton.Register(this);
		if (LocalizationSettings.InitializationOperation.IsDone)
		{
			UpdateLocale();
		}
	}

	private void OnDisable()
	{
		LocalizationSingleton.Unregister(this);
	}
}
