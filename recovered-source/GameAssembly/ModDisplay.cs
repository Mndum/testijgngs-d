using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ModDisplay : AdvancedItemListPrefab
{
	[SerializeField]
	private Button mainButton;

	[SerializeField]
	private TextMeshProUGUI description;

	[SerializeField]
	private Image icon;

	[SerializeField]
	private TextMeshProUGUI statusText;

	public override bool HasDefaultButton => true;

	public Mod Current { get; private set; }

	public override Button GetDefaultButton()
	{
		return mainButton;
	}

	public void SetData(Mod mod)
	{
		Current = mod;
		SerializableLocalModInfo localModInfo = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.GetLocalModInfo(mod, createIfNotExists: true);
		description.text = mod.Name;
		description.fontStyle = (IsFocused ? FontStyles.Bold : FontStyles.Normal);
		if (localModInfo.LoadOnStart)
		{
			icon.sprite = R.Icons.Icon_check.Get();
			icon.color = Color.green;
		}
		else
		{
			icon.sprite = R.Icons.Icon_close.Get();
			icon.color = Color.red;
		}
		Locale selectedLocale = LocalizationSettings.SelectedLocale;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine(ModStatusExtensions.StatusToName(mod.Status));
			switch (mod.GetLocalizationStatus(selectedLocale))
			{
			case Mod.ModLocalizationStatus.NativeModLanguage:
				stringBuilder.AppendColorTagStart("green");
				stringBuilder.AppendLine(LOC.Polyglot.MENU_LANGUAGE_THIS);
				break;
			case Mod.ModLocalizationStatus.Translated:
				stringBuilder.AppendColorTagStart("green");
				stringBuilder.Append(LOC.UI.Mods_ModTranslatedToLanguage);
				stringBuilder.Append(" (");
				stringBuilder.Append(LOC.Polyglot.MENU_LANGUAGE_THIS);
				stringBuilder.Append(")");
				break;
			case Mod.ModLocalizationStatus.BrokenTranslation:
				stringBuilder.AppendColorTagStart("yellow");
				stringBuilder.Append(LOC.UI.Mods_TranslationIssuesBroken);
				stringBuilder.Append(" (");
				stringBuilder.Append(LOC.Polyglot.MENU_LANGUAGE_THIS);
				stringBuilder.Append(")");
				break;
			default:
				stringBuilder.AppendColorTagStart("red");
				stringBuilder.Append(LOC.UI.Mods_ModNotTranslatedToLangauge);
				stringBuilder.Append(" (");
				stringBuilder.Append(LOC.Polyglot.MENU_LANGUAGE_THIS);
				stringBuilder.Append(")");
				break;
			}
			stringBuilder.AppendColorTagEnd();
			statusText.SetText(stringBuilder);
		}
		statusText.color = ModStatusExtensions.StatusToColor(mod.Status);
	}
}
