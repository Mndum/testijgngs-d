using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionsPopup : PrefabPopup, ILocaleChangedReceiver
{
	private bool _restartRequired;

	[SerializeField]
	private Toggle skipUnseenTextToggle;

	[SerializeField]
	private Toggle greyOutSeenTextToggle;

	[SerializeField]
	private Toggle cumFlashBangToggle;

	[SerializeField]
	private Scrollbar sensitivityScrollbar;

	private List<(int index, Locale locale, LocaleName localeName)> _languageDropdownData;

	[SerializeField]
	private TMP_Dropdown languageDropdown;

	[SerializeField]
	private TextMeshProUGUI translationProgressLabel;

	private List<OptionsStatic.ResolutionData> _currentResolutions;

	[SerializeField]
	private TMP_Dropdown resultionDropdown;

	[SerializeField]
	private Toggle fullscreenToggle;

	[SerializeField]
	private Toggle keepRunningWhenMinimized;

	[SerializeField]
	private Toggle LowMemoryToggle;

	[SerializeField]
	private RectTransform AllLowMemorySettings;

	[SerializeField]
	private Scrollbar LowMemoryScaleScrollbar;

	[SerializeField]
	private TextMeshProUGUI LowMemoryScaleInfo;

	[SerializeField]
	private Scrollbar masterVolumeScrollbar;

	[SerializeField]
	private Scrollbar musicVolumeScrollbar;

	[SerializeField]
	private Scrollbar sfxVolumeScrollbar;

	[SerializeField]
	private Toggle showMusicPopupToggle;

	[FormerlySerializedAs("ToyConnectButton")]
	[SerializeField]
	private Button ToySettingsButton;

	[SerializeField]
	private Button resetGameDataButton;

	[FormerlySerializedAs("DisableDebugButton")]
	[SerializeField]
	private Button DisableCheatsButton;

	[SerializeField]
	private Button ExportLogButton;

	[SerializeField]
	private Button ClearVerificationButton;

	public static OptionsPopup CurrentInstance { get; private set; }

	private void InitGameplay()
	{
		sensitivityScrollbar.value = OptionsStatic.SensitivityInstanceScaled;
		sensitivityScrollbar.onValueChanged.SetListener(SensitivityChanged);
		skipUnseenTextToggle.isOn = OptionsStatic.SkipUnseenTextPref;
		skipUnseenTextToggle.onValueChanged.SetListener(OptionsStatic.SkipUnseenTextPref.SetValue);
		greyOutSeenTextToggle.isOn = OptionsStatic.GreyOutSeenTextPref;
		greyOutSeenTextToggle.onValueChanged.SetListener(OptionsStatic.GreyOutSeenTextPref.SetValue);
		cumFlashBangToggle.isOn = OptionsStatic.CumFlashBangEnabledPref;
		cumFlashBangToggle.onValueChanged.SetListener(OptionsStatic.CumFlashBangEnabledPref.SetValue);
	}

	private void SensitivityChanged(float value)
	{
		OptionsStatic.SensitivityInstanceScaled = value;
		OptionsStatic.SensitivityPref.SetValue(OptionsStatic.SensitivityInstance);
	}

	private void InitLocalization()
	{
		languageDropdown.ClearOptions();
		List<TMP_Dropdown.OptionData> options = languageDropdown.options;
		_ = LocalizationSettings.ProjectLocale;
		_languageDropdownData = (from t in LocalizationSettings.AvailableLocales.Locales.Select((Locale locale, int localeIndex) => (localeIndex: localeIndex, locale: locale, locale.Metadata.GetMetadata<LocaleName>()))
			orderby t.Item3.nameInEnglish
			select t).ToList();
		Locale selectedLocale = LocalizationSettings.SelectedLocale;
		int value = 0;
		for (int num = 0; num < _languageDropdownData.Count; num++)
		{
			(int index, Locale locale, LocaleName localeName) tuple = _languageDropdownData[num];
			Locale item = tuple.locale;
			LocaleName item2 = tuple.localeName;
			if (item == selectedLocale)
			{
				value = num;
			}
			LocaleTranslationProgress metadata = item.Metadata.GetMetadata<LocaleTranslationProgress>();
			if (item2 != null)
			{
				if (metadata != null)
				{
					options.Add(new TMP_Dropdown.OptionData($"{item2.name} - {item2.nameInEnglish} - {metadata.RelativeProgress:P}"));
				}
				else
				{
					options.Add(new TMP_Dropdown.OptionData(item2.nameInEnglish ?? ""));
				}
			}
			else
			{
				options.Add(new TMP_Dropdown.OptionData("ERROR!"));
			}
		}
		languageDropdown.options = options;
		languageDropdown.value = value;
		languageDropdown.onValueChanged.SetListener(LanguageChanged);
		UpdateTranslationProgressLabel(LocalizationSettings.SelectedLocale);
	}

	private void LanguageChanged(int index)
	{
		(int, Locale, LocaleName) tuple = _languageDropdownData[index];
		OptionsStatic.SetLocale(LocalizationSettings.AvailableLocales.Locales[tuple.Item1]);
	}

	private void UpdateTranslationProgressLabel(Locale currentLocale)
	{
		LocaleTranslationProgress metadata = currentLocale.Metadata.GetMetadata<LocaleTranslationProgress>();
		if (metadata == null)
		{
			translationProgressLabel.gameObject.SetActive(value: false);
			return;
		}
		translationProgressLabel.text = LOC.UI.Options_TranslationProgress(metadata.fullProgress);
		float num = metadata.fullProgressMT / metadata.fullProgress;
		if (num > 0.01f)
		{
			TextMeshProUGUI textMeshProUGUI = translationProgressLabel;
			textMeshProUGUI.text = textMeshProUGUI.text + "\n" + LOC.UI.Options_MachineTranslation(num);
		}
		translationProgressLabel.gameObject.SetActive(value: true);
	}

	public void SyncFullscreenDisplay()
	{
		fullscreenToggle.SetIsOnWithoutNotify(OptionsStatic.FullScreenPref);
		UpdateResolutionDropdown();
	}

	private void ShowDisplaySettingsTimeoutPopup(OptionsStatic.DisplaySettings previousDisplaySettings)
	{
		UiOverlay.Instance.SimplePopupWithTimeout(LOC.UI.UI_AreYouSure, (TimeSpan x) => LOC.UI.Options_DisplaySettingCheckPrompt((int)Math.Round(x.TotalSeconds)).TMPColorSurround(Color.red), new List<PopupChoice>
		{
			new PopupChoice(LOC.Polyglot.MENU_LABEL_YES),
			new PopupChoice(LOC.Polyglot.MENU_LABEL_NO, () =>
			{
				OptionsStatic.CurrentDisplaySettings = previousDisplaySettings;
				SyncFullscreenDisplay();
				OptionsStatic.SyncResolution();
			}, CommonButtonColorType.Red)
		}, 1, TimeSpan.FromSeconds(15.0));
	}

	public void FullscreenChanged(bool value)
	{
		OptionsStatic.DisplaySettings currentDisplaySettings = OptionsStatic.CurrentDisplaySettings;
		OptionsStatic.ChangeFullscreen(value);
		if (!currentDisplaySettings.Equals(OptionsStatic.CurrentDisplaySettings))
		{
			ShowDisplaySettingsTimeoutPopup(currentDisplaySettings);
		}
	}

	private void ResolutionChanged(int value)
	{
		OptionsStatic.DisplaySettings currentDisplaySettings = OptionsStatic.CurrentDisplaySettings;
		if (_currentResolutions.TryGetValue(value, out var value2))
		{
			OptionsStatic.ResolutionPref.SetValue(value2.Resolution);
		}
		if (!currentDisplaySettings.Equals(OptionsStatic.CurrentDisplaySettings))
		{
			OptionsStatic.SyncResolution();
			ShowDisplaySettingsTimeoutPopup(currentDisplaySettings);
		}
	}

	public void UpdateResolutionDropdown()
	{
		_currentResolutions = _currentResolutions.ClearOrCreate();
		OptionsStatic.FillResolutions(_currentResolutions, out var currentIndex);
		resultionDropdown.options.ResizeList(_currentResolutions.Count, () => new TMP_Dropdown.OptionData());
		for (int num = 0; num < _currentResolutions.Count; num++)
		{
			resultionDropdown.options[num].text = _currentResolutions[num].Description;
		}
		resultionDropdown.SetValueWithoutNotify(currentIndex);
		resultionDropdown.RefreshShownValue();
	}

	private void InitResolution()
	{
		_currentResolutions = _currentResolutions.ClearOrCreate();
		SyncFullscreenDisplay();
		fullscreenToggle.onValueChanged.SetListener(FullscreenChanged);
		UpdateResolutionDropdown();
		resultionDropdown.onValueChanged.SetListener(ResolutionChanged);
	}

	private void LowMemoryToggleClicked(bool value)
	{
		_restartRequired = true;
		OptionsStatic.LowMemoryModeEnabled.SetValue(value);
		UpdateLowMemory();
		if (value)
		{
			OptionsStatic.LowMemoryDivisionLevel.SetValue(OptionsStatic.LowMemoryDivisionLevel.DefaultValue);
			UpdateLowMemoryScaleInfo();
		}
	}

	private void LowMemoryScaleChanged(float v = 0f)
	{
		int valueInt = LowMemoryScaleScrollbar.GetValueInt();
		if (valueInt != OptionsStatic.LowMemoryDivisionLevel.Value)
		{
			_restartRequired = true;
			OptionsStatic.LowMemoryDivisionLevel.SetValue(valueInt);
			UpdateLowMemoryScaleInfo();
		}
	}

	private void UpdateLowMemory()
	{
		bool active = OptionsStatic.LowMemoryModeEnabled;
		AllLowMemorySettings.gameObject.SetActive(active);
	}

	private void UpdateLowMemoryScaleInfo()
	{
		int lowMemoryDivisionLevelPow = OptionsStatic.LowMemoryDivisionLevelPow;
		int num = 4096;
		int num2 = num / lowMemoryDivisionLevelPow;
		float num3 = 1f - (float)(num2 * num2) / (float)(num * num);
		LowMemoryScaleInfo.SetText(LOC.UI.LowMemoryTextureScaleOptionsInfo($"{num}x{num}", $"{num2}x{num2}", num3));
	}

	private void InitLowMemory()
	{
		bool isOn = OptionsStatic.LowMemoryModeEnabled;
		LowMemoryToggle.isOn = isOn;
		LowMemoryToggle.onValueChanged.SetListener(LowMemoryToggleClicked);
		LowMemoryScaleScrollbar.numberOfSteps = 4;
		LowMemoryScaleScrollbar.SetValueInt(OptionsStatic.LowMemoryDivisionLevel);
		LowMemoryScaleScrollbar.onValueChanged.SetListener(LowMemoryScaleChanged);
		UpdateLowMemoryScaleInfo();
		UpdateLowMemory();
	}

	private void MasterVolumeChanged(float value)
	{
		OptionsStatic.MasterVolumePref.SetValue(value);
		OptionsStatic.MasterVolumeSession = value;
	}

	private void MusicVolumeChanged(float value)
	{
		OptionsStatic.MusicAloneVolumeSession = value;
		OptionsStatic.MusicAloneVolumePref.SetValue(value);
	}

	private void SfxVolumeChanged(float value)
	{
		OptionsStatic.SfxAloneVolumeSession = value;
		OptionsStatic.SfxAloneVolumePref.SetValue(value);
	}

	private void InitSound()
	{
		masterVolumeScrollbar.value = OptionsStatic.MasterVolumeSession;
		masterVolumeScrollbar.onValueChanged.SetListener(MasterVolumeChanged);
		musicVolumeScrollbar.value = OptionsStatic.MusicAloneVolumeSession;
		musicVolumeScrollbar.onValueChanged.SetListener(MusicVolumeChanged);
		sfxVolumeScrollbar.value = OptionsStatic.SfxAloneVolumeSession;
		sfxVolumeScrollbar.onValueChanged.SetListener(SfxVolumeChanged);
		EventTrigger orAddComponent = sfxVolumeScrollbar.gameObject.GetOrAddComponent<EventTrigger>();
		if (orAddComponent.triggers.Count == 0)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry
			{
				eventID = EventTriggerType.PointerDown
			};
			entry.callback.AddListener((BaseEventData t) =>
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.PlayRandom(1f);
			});
			orAddComponent.triggers.Add(entry);
		}
		sfxVolumeScrollbar.value = OptionsStatic.SfxAloneVolumeSession;
		sfxVolumeScrollbar.onValueChanged.SetListener(SfxVolumeChanged);
		showMusicPopupToggle.isOn = OptionsStatic.ShowMusicPopupPref;
		showMusicPopupToggle.onValueChanged.SetListener((bool a) =>
		{
			OptionsStatic.ShowMusicPopupPref.SetValue(a);
		});
	}

	private void InitRemaining()
	{
		resetGameDataButton.onClick.SetListener(ResetGameDataButtonClicked);
		keepRunningWhenMinimized.gameObject.SetActive(value: true);
		keepRunningWhenMinimized.isOn = OptionsStatic.KeepRunningWhenMinimizedPref;
		keepRunningWhenMinimized.onValueChanged.SetListener((bool a) =>
		{
			OptionsStatic.KeepRunningWhenMinimizedPref.SetValue(a);
		});
		UpdateCheatsButton();
		UpdateResolutionDropdown();
		if (SingletonMonoBehaviour<ToySingleton>.Instance.AnyToyAvailable)
		{
			ToySettingsButton.onClick.SetListener(UiOverlay.Instance.OpenToyPopup);
		}
		else
		{
			ToySettingsButton.transform.parent.gameObject.SetActive(value: false);
		}
		if (string.IsNullOrEmpty(Application.consoleLogPath))
		{
			ExportLogButton.gameObject.SetActive(value: false);
		}
		else
		{
			ExportLogButton.gameObject.SetActive(value: true);
			ExportLogButton.onClick.SetListener(ExportLogButtonClicked);
		}
		ClearVerificationButton.gameObject.SetActive(value: false);
	}

	private void ResetGameDataButtonClicked()
	{
		UiOverlay.Instance.SimplePopup(LOC.UI.Options_ResetGameData, LOC.UI.Options_ResetGameDataPrompt, () =>
		{
			UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Options_ResetGameDataPrompt2, () =>
			{
				SingletonLoadableMonoBehaviour<SaveSingleton>.Instance.ResetAllData();
			});
		});
	}

	public void UpdateCheatsButton()
	{
		if (OptionsStatic.DebugEnabledPref.Value)
		{
			DisableCheatsButton.gameObject.SetActive(value: true);
			DisableCheatsButton.onClick.SetListener(() =>
			{
				UiOverlay.Instance.SimplePopup(LOC.UI.UI_AreYouSure, LOC.UI.Options_DisableDebugPrompt, () =>
				{
					OptionsStatic.DebugEnabledPref.SetValue(value: false);
					UpdateCheatsButton();
				});
			});
		}
		else
		{
			DisableCheatsButton.gameObject.SetActive(value: false);
			DisableCheatsButton.onClick.RemoveAllListeners();
		}
	}

	private void ExportLogButtonClicked()
	{
		_ = Application.consoleLogPath;
		string text = Application.consoleLogPath + ".tmp";
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		try
		{
			File.Copy(Application.consoleLogPath, text);
			string data = File.ReadAllText(text);
			UiOverlay.Instance.FileSavePopup("log.txt", data, (MyFileBrowserSingleton.DownloadResult result) =>
			{
				if (result.Success)
				{
					UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.LogExportSuccessful);
				}
				else
				{
					UiOverlay.Instance.OkPopup(LOC.UI.UI_Error, LOC.UI.LogExportFailed);
				}
			});
		}
		finally
		{
			File.Delete(text);
		}
	}

	private void ClearVerificationButtonClicked()
	{
		UiOverlay.Instance.SimplePopup(LOC.Polyglot.MENU_LABEL_WARNING, LOC.UI.UI_AreYouSure, () =>
		{
			ClearVerificationButton.gameObject.SetActive(value: false);
			OptionsStatic.ClearVerification();
		});
	}

	public override void Open()
	{
		CurrentInstance = this;
		InitGameplay();
		InitResolution();
		InitLocalization();
		InitSound();
		InitRemaining();
		InitLowMemory();
		base.Open();
		LocalizationSingleton.Register(this);
	}

	public void OnLocaleChanged(Locale newLocale)
	{
		UpdateTranslationProgressLabel(newLocale);
	}

	public new void OnDisable()
	{
		base.OnDisable();
		CurrentInstance = null;
		PlayerPrefs.SaveIfChanged();
		LocalizationSingleton.Unregister(this);
		if (_restartRequired)
		{
			UiOverlay.Instance.OkPopup(LOC.UI.Mods_ClosingTheGame, LOC.UI.OptionsRestartRequired, QuitHandler.ForceQuitGame);
		}
	}

	public override bool CanClose()
	{
		return true;
	}
}
