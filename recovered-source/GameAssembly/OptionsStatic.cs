using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Fungus;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Pool;

public static class OptionsStatic
{
	public struct DisplaySettings(bool fullScreen, Vector2Int resolution)
	{
		public readonly bool FullScreen = fullScreen;

		public readonly Vector2Int Resolution = resolution;

		public bool Equals(DisplaySettings other)
		{
			if (FullScreen == other.FullScreen)
			{
				return Resolution.Equals(other.Resolution);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is DisplaySettings other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			bool fullScreen = FullScreen;
			return (fullScreen.GetHashCode() * 397) ^ Resolution.GetHashCode();
		}
	}

	public struct ResolutionData
	{
		public readonly string Description;

		public readonly Vector2Int Resolution;

		public readonly double BestRefreshRate;

		public ResolutionData(Vector2Int resolution, double bestRefreshRate, Vector2Int nativeResolution)
		{
			Resolution = resolution;
			BestRefreshRate = bestRefreshRate;
			if (resolution == nativeResolution)
			{
				Description = $"{resolution.x}x{resolution.y} (Native)";
				return;
			}
			if (nativeResolution.x % resolution.x == 0 && nativeResolution.y % resolution.y == 0)
			{
				int num = nativeResolution.x / resolution.x;
				int num2 = nativeResolution.y / resolution.y;
				if (num == num2 && num >= 1)
				{
					Description = $"{resolution.x}x{resolution.y} (Native 1/{Mathf.RoundToInt(num)})";
					return;
				}
			}
			else if (resolution.x % nativeResolution.x == 0 && resolution.y % nativeResolution.y == 0)
			{
				int num3 = resolution.x / nativeResolution.x;
				int num4 = resolution.y / nativeResolution.y;
				if (num3 == num4 && num3 >= 1)
				{
					Description = $"{resolution.x}x{resolution.y} (Native x{Mathf.RoundToInt(num3)})";
					return;
				}
			}
			Description = $"{resolution.x}x{resolution.y}";
		}
	}

	public delegate void VolumeChangedEvent(float sfx, float music);

	private static Vector2Int MinResolution = new Vector2Int(640, 360);

	private static Vector2Int MaxResolution = new Vector2Int(7680, 4320);

	private static Vector2Int MainSmallResolution = new Vector2Int(1280, 720);

	private static Vector2Int MainBigResolution = new Vector2Int(1920, 1080);

	private static Resolution[] DefaultResolutions = new Resolution[2]
	{
		VectorToResolution(MainSmallResolution),
		VectorToResolution(MainBigResolution)
	};

	public static PlayerPrefsBooleanKey FullScreenPref = new PlayerPrefsBooleanKey(Application.platform == RuntimePlatform.Android, "fullscreen");

	public static PlayerPrefsVector2IntKey ResolutionPref = new PlayerPrefsVector2IntKey(MainSmallResolution, "resolution");

	private static readonly Comparison<ResolutionData> SortComparison = (ResolutionData a, ResolutionData b) =>
	{
		long value = (long)a.Resolution.x * (long)a.Resolution.y;
		int num = -((long)b.Resolution.x * (long)b.Resolution.y).CompareTo(value);
		if (num != 0)
		{
			return num;
		}
		double bestRefreshRate = b.BestRefreshRate;
		return -bestRefreshRate.CompareTo(a.BestRefreshRate);
	};

	private static bool _subscribed;

	private static float _masterVolume;

	public static PlayerPrefsFloatKey MasterVolumePref = new PlayerPrefsFloatKey(0.5f, "mastervolume")
	{
		SaveAfterwards = false
	};

	private static float _musicAloneVolume;

	public static PlayerPrefsFloatKey MusicAloneVolumePref = new PlayerPrefsFloatKey(0.6f, "musicvolume")
	{
		SaveAfterwards = false
	};

	private static float _sfxAloneVolume;

	public static PlayerPrefsFloatKey SfxAloneVolumePref = new PlayerPrefsFloatKey(0.6f, "sfxvolume")
	{
		SaveAfterwards = false
	};

	public static PlayerPrefsFloatKey SensitivityPref = new PlayerPrefsFloatKey((Application.platform == RuntimePlatform.Android) ? 2f : 1f, "sensitivity")
	{
		SaveAfterwards = false
	};

	public static PlayerPrefsStringKey CKeyPref = new PlayerPrefsStringKey("", "CKey");

	public static PlayerPrefsStringKey DProofPref = new PlayerPrefsStringKey("", "D");

	public static PlayerPrefsBooleanKey DebugEnabledPref = new PlayerPrefsBooleanKey(defaultValue: false, "debugEnabledNew");

	public static PlayerPrefsBooleanKey AcceptedModsDangerWarning = new PlayerPrefsBooleanKey(defaultValue: false, "shownModsDanger");

	public static PlayerPrefsBooleanKey LastLoadingSucceded = new PlayerPrefsBooleanKey(defaultValue: true, "lastLoadingSucceded");

	public static PlayerPrefsBooleanKey ShowWebGLSaveWarning = new PlayerPrefsBooleanKey(defaultValue: true, "showWebGLSaveWarning");

	public static PlayerPrefsBooleanKey ShowWebGLMobileWarning = new PlayerPrefsBooleanKey(defaultValue: true, "showWebGLMobileWarning");

	public static PlayerPrefsBooleanKey ShowMusicPopupPref = new PlayerPrefsBooleanKey(defaultValue: true, "showMusicPopup");

	public static PlayerPrefsBooleanKey KeepRunningWhenMinimizedPref = new PlayerPrefsBooleanKey(defaultValue: true, "keepRunningWhenMinimized")
	{
		OnValueChanged = (bool value) =>
		{
			Application.runInBackground = value;
		}
	};

	public static PlayerPrefsBooleanKey CumFlashBangEnabledPref = new PlayerPrefsBooleanKey(defaultValue: true, "cumFlashbangEnabled");

	public static PlayerPrefsBooleanKey SkipUnseenTextPref = new PlayerPrefsBooleanKey(defaultValue: false, "skipUnseenText")
	{
		OnValueChanged = (bool value) =>
		{
			Writer.SkipUnseenTextPref = value;
		}
	};

	public static PlayerPrefsBooleanKey GreyOutSeenTextPref = new PlayerPrefsBooleanKey(defaultValue: true, "greyOutSeenText")
	{
		OnValueChanged = (bool value) =>
		{
			Writer.GrayOutSeenTextPref = value;
		}
	};

	public const int AGUISensorCount = 10;

	public static PlayerPrefsBooleanKey AGUIUseDriverDepth = new PlayerPrefsBooleanKey(defaultValue: true, "AGUIUseDriverDepth");

	public static PlayerPrefsFloatArrayKey AGUIDefaultMinCalibration = new PlayerPrefsFloatArrayKey(0f, "AGUIDefaultMinCalibration", 10);

	public static PlayerPrefsFloatArrayKey AGUIDefaultMaxCalibration = new PlayerPrefsFloatArrayKey(1f, "AGUIDefaultMaxCalibration", 10);

	public const int LowMemoryMaxScale = 4;

	public static PlayerPrefsBooleanKey LowMemoryModeEnabled = new PlayerPrefsBooleanKey(LowMemoryDefault, "LowMemoryModeEnabled");

	public static PlayerPrefsIntegerKey LowMemoryDivisionLevel = new PlayerPrefsIntegerKey(0, "LowMemoryDivisionCount");

	public static PlayerPrefsGuidKey IId = new PlayerPrefsGuidKey("IId");

	public static PlayerPrefsGuidKey FhId = new PlayerPrefsGuidKey("FhId");

	public static PlayerPrefsStringKey VerData = new PlayerPrefsStringKey(null, "VerData");

	public static PlayerPrefsLongKey MillisSpentPlaying = new PlayerPrefsLongKey(0L, "millisSpentPlaying");

	public static PlayerPrefsLongKey LastShillingInfoAt = new PlayerPrefsLongKey(0L, "millisLastShillingInfoAt");

	public static PlayerPrefsLongKey LastShillingRatingInfoAt = new PlayerPrefsLongKey(0L, "millisLastRatingShillingInfoAt");

	private static DateTime? LastCheckpointTime;

	private static PlayerPrefsBooleanKey DisableShillingPopups = new PlayerPrefsBooleanKey(defaultValue: false, "shillingInfoDisabled");

	private static string localePlayerprefs = "selected-locale";

	public static DisplaySettings CurrentDisplaySettings
	{
		get
		{
			return new DisplaySettings(FullScreenPref.Value, ResolutionPref.Value);
		}
		set
		{
			FullScreenPref.Value = value.FullScreen;
			ResolutionPref.Value = value.Resolution;
		}
	}

	public static float MasterVolumeSession
	{
		get
		{
			return _masterVolume;
		}
		set
		{
			_masterVolume = value;
			InvokeVolumeChanged();
		}
	}

	public static float MusicAloneVolumeSession
	{
		get
		{
			return _musicAloneVolume;
		}
		set
		{
			_musicAloneVolume = value;
			InvokeVolumeChanged();
		}
	}

	public static float SfxAloneVolumeSession
	{
		get
		{
			return _sfxAloneVolume;
		}
		set
		{
			_sfxAloneVolume = value;
			InvokeVolumeChanged();
		}
	}

	public static float SfxVolume => SfxAloneVolumeSession * MasterVolumeSession;

	public static float MusicVolume => MusicAloneVolumeSession * MasterVolumeSession;

	public static float SensitivityInstanceScaled
	{
		get
		{
			return (SensitivityInstance - 0.5f) / 5f;
		}
		set
		{
			SensitivityInstance = value * 5f + 0.5f;
		}
	}

	public static float SensitivityInstance { get; set; }

	public static bool DebugEnabled => GetCheatTier() == CheatTierEnum.Debug;

	public static bool AnyCheatsEnabled => GetCheatTier() > CheatTierEnum.NoCheats;

	private static bool LowMemoryDefault
	{
		get
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return Application.isMobilePlatform;
			}
			return false;
		}
	}

	public static int LowMemoryDivisionLevelPow => Mathf.ClosestPowerOfTwo((int)Mathf.Pow(2f, (int)LowMemoryDivisionLevel + 1));

	public static int MinutesSpentPlaying => Mathf.RoundToInt((float)(long)MillisSpentPlaying / 60000f);

	private static bool ShouldShowShillingInfo => (long)MillisSpentPlaying - (long)LastShillingInfoAt > 14400000;

	private static bool ShouldShowShillingRatingInfo => (long)MillisSpentPlaying - (long)LastShillingRatingInfoAt > 21600000;

	public static event VolumeChangedEvent VolumeChanged;

	private static void ClampResolutionToValid(ref Vector2Int resolution)
	{
		resolution.x = Mathf.Clamp(resolution.x, MinResolution.x, MaxResolution.x);
		resolution.y = Mathf.Clamp(resolution.y, MinResolution.y, MaxResolution.y);
	}

	private static Resolution VectorToResolution(Vector2Int vector, uint refreshRate = 60u)
	{
		return new Resolution
		{
			width = vector.x,
			height = vector.y,
			refreshRateRatio = new RefreshRate
			{
				numerator = refreshRate,
				denominator = 1u
			}
		};
	}

	private static bool Is16By9(int width, int height)
	{
		return 16 * height == 9 * width;
	}

	public static void FillResolutions(List<ResolutionData> data, out int currentIndex)
	{
		bool flag = !FullScreenPref.Value;
		Resolution[] resolutions = Screen.resolutions;
		data.Clear();
		Dictionary<Vector2Int, double> bestMap;
		using (CollectionPool<Dictionary<Vector2Int, double>, KeyValuePair<Vector2Int, double>>.Get(out bestMap))
		{
			Resolution[] array = resolutions;
			for (int i = 0; i < array.Length; i++)
			{
				Resolution resolution = array[i];
				Vector2Int key = Normalize(new Vector2Int(resolution.width, resolution.height));
				double value = resolution.refreshRateRatio.value;
				if (!flag || Is16By9(key.x, key.y))
				{
					AddToMap(key, value);
				}
			}
			Display main = Display.main;
			Vector2Int vector2Int;
			if (bestMap.Count == 0)
			{
				double value2 = Screen.mainWindowDisplayInfo.refreshRate.value;
				array = DefaultResolutions;
				for (int i = 0; i < array.Length; i++)
				{
					Resolution resolution2 = array[i];
					AddToMap(new Vector2Int(resolution2.width, resolution2.height), value2);
				}
				vector2Int = new Vector2Int(main.renderingWidth, main.renderingHeight);
				AddToMap(vector2Int, value2);
			}
			else
			{
				vector2Int = new Vector2Int(main.systemWidth, main.systemHeight);
			}
			if (bestMap.TryGetValue(vector2Int, out var value3))
			{
				if (TryGetScaled(vector2Int, 2, out var scaledResolution))
				{
					AddToMap(scaledResolution, value3);
				}
				if (TryGetScaled(vector2Int, 3, out scaledResolution))
				{
					AddToMap(scaledResolution, value3);
				}
				if (TryGetScaled(vector2Int, 4, out scaledResolution))
				{
					AddToMap(scaledResolution, value3);
				}
			}
			foreach (var (resolution3, bestRefreshRate) in bestMap)
			{
				data.Add(new ResolutionData(resolution3, bestRefreshRate, vector2Int));
			}
			data.Sort(SortComparison);
			Vector2Int value4 = ResolutionPref.Value;
			for (int j = 0; j < data.Count; j++)
			{
				if (data[j].Resolution == value4)
				{
					currentIndex = j;
					return;
				}
			}
			currentIndex = 0;
			data.Insert(0, new ResolutionData(value4, 60.0, vector2Int));
		}
		void AddToMap(Vector2Int key2, double rate)
		{
			if (!bestMap.TryGetValue(key2, out var value5) || rate > value5)
			{
				bestMap[key2] = rate;
			}
		}
		static Vector2Int Normalize(Vector2Int res)
		{
			if (res.x < res.y)
			{
				return new Vector2Int(res.y, res.x);
			}
			return res;
		}
		static bool TryGetScaled(Vector2Int vector2Int3, int scalingFactor, out Vector2Int reference)
		{
			reference = vector2Int3 / scalingFactor;
			if (vector2Int3.x % scalingFactor != 0)
			{
				return false;
			}
			if (vector2Int3.y % scalingFactor != 0)
			{
				return false;
			}
			if (reference.x < MinResolution.x || reference.y < MinResolution.y)
			{
				return false;
			}
			return true;
		}
	}

	public static void SyncResolution()
	{
		bool flag = FullScreenPref;
		Vector2Int resolution = ResolutionPref.Value;
		ClampResolutionToValid(ref resolution);
		FullScreenMode fullscreenMode = (flag ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
		Screen.SetResolution(resolution.x, resolution.y, fullscreenMode);
		SyncLetterbox();
	}

	public static void SyncLetterbox()
	{
		Vector2 vector = new Vector2(16f, 9f);
		Camera mainCamera = UiOverlay.Instance.MainCamera;
		if (!(mainCamera == null))
		{
			float num = (float)Screen.width / (float)Screen.height;
			float num2 = vector.x / vector.y;
			if (Mathf.Approximately(num, num2))
			{
				mainCamera.rect = new Rect(0f, 0f, 1f, 1f);
			}
			else if (num > num2)
			{
				float num3 = num2 / num;
				float x = (1f - num3) / 2f;
				mainCamera.rect = new Rect(x, 0f, num3, 1f);
			}
			else
			{
				float num4 = num / num2;
				float y = (1f - num4) / 2f;
				mainCamera.rect = new Rect(0f, y, 1f, num4);
			}
		}
	}

	public static Vector2Int DetermineBestFullScreenResolution(List<ResolutionData> data)
	{
		Display main = Display.main;
		Vector2Int vector2Int = new Vector2Int(main.systemWidth, main.systemHeight);
		for (int num = data.Count - 1; num >= 0; num--)
		{
			if (data[num].Resolution == vector2Int)
			{
				return data[num].Resolution;
			}
		}
		float b = (float)vector2Int.x / (float)vector2Int.y;
		for (int num2 = data.Count - 1; num2 >= 0; num2--)
		{
			Vector2Int resolution = data[num2].Resolution;
			if (Mathf.Approximately((float)resolution.x / (float)resolution.y, b))
			{
				return resolution;
			}
		}
		return data[data.Count - 1].Resolution;
	}

	public static void ChangeFullscreen(bool value)
	{
		FullScreenPref.SetValue(value);
		List<ResolutionData> data = new List<ResolutionData>();
		FillResolutions(data, out var _);
		ResolutionPref.SetValue(value ? DetermineBestFullScreenResolution(data) : MainSmallResolution);
		if (OptionsPopup.CurrentInstance != null)
		{
			OptionsPopup.CurrentInstance.SyncFullscreenDisplay();
		}
		SyncResolution();
	}

	private static void LoadVolumeSettings()
	{
		MasterVolumeSession = MasterVolumePref;
		MusicAloneVolumeSession = MusicAloneVolumePref;
		SfxAloneVolumeSession = SfxAloneVolumePref;
		if (!_subscribed)
		{
			_subscribed = true;
			VolumeChanged += FungusManager.Instance.MusicManager.SyncVolume;
			VolumeChanged += (float a, float b) =>
			{
				SingletonMonoBehaviour<SoundSingleton>.Instance.UpdateMusicVolume();
			};
		}
		InvokeVolumeChanged();
	}

	private static void InvokeVolumeChanged()
	{
		VolumeChanged?.Invoke(SfxVolume, MusicVolume);
	}

	public static void LoadSensitivitySettings()
	{
		SensitivityInstance = SensitivityPref;
	}

	public static CheatTierEnum GetCheatTier()
	{
		if (string.IsNullOrWhiteSpace(CKeyPref.Value))
		{
			return CheatTierEnum.NoCheats;
		}
		int status;
		int status2;
		switch (Sheep.CheckIfSheepIsHappy(CKeyPref.Value, out status, out status2))
		{
		case 10:
			return CheatTierEnum.BasicCheats;
		case 20:
			if ((bool)DebugEnabledPref)
			{
				return CheatTierEnum.Debug;
			}
			return CheatTierEnum.AdvancedCheats;
		default:
			CKeyPref.SetValue(CKeyPref.DefaultValue);
			return CheatTierEnum.NoCheats;
		}
	}

	public static void ClearCheatCode()
	{
		CKeyPref.SetValue(CKeyPref.DefaultValue);
	}

	public static void ClearVerification()
	{
		VerData.SetValue("");
	}

	public static void ShowShillingInfo()
	{
		if ((bool)DisableShillingPopups)
		{
			return;
		}
		string text = "Patreon";
		string text2 = "SubscribeStar";
		string text3 = "ItchIO Rating";
		string shillingPopupUtm = "popup";
		Dictionary<string, Action> linkIdToAction = new Dictionary<string, Action>
		{
			[text] = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenDirectSubscribePatreon(shillingPopupUtm);
			},
			[text2] = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenSubscribeStar(shillingPopupUtm);
			},
			[text3] = () =>
			{
				SingletonLoadableMonoBehaviour<GameScript>.Instance.OpenItchIORating(shillingPopupUtm);
			}
		};
		if (ShouldShowShillingInfo)
		{
			LastShillingInfoAt.SetValue(MillisSpentPlaying);
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				stringBuilder.TMPLinkSurroundStringBuilderAppend(text, "Patreon (" + LOC.UI.ClickToOpen + ")");
				stringBuilder.AppendLine();
				stringBuilder.TMPLinkSurroundStringBuilderAppend(text2, "SubscribeStar (" + LOC.UI.ClickToOpen + ")");
				stringBuilder.AppendLine();
				string text4 = LOC.UI.ShillingPrompt(GlobalFunctions.TimePeriodHours(MinutesSpentPlaying), stringBuilder.ToString());
				UiOverlay.Instance.Popup(LOC.UI.ThankYou, text4, new List<PopupChoice>
				{
					new PopupChoice(LOC.Polyglot.MENU_LABEL_OK, null, CommonButtonColorType.Green),
					new PopupChoice(LOC.UI.UI_DontShowTheWarningAgain, () =>
					{
						DisableShillingPopups.SetValue(value: true);
					}, CommonButtonColorType.Red)
				}).AddLinkHandler(linkIdToAction);
				return;
			}
		}
		if (!ShouldShowShillingRatingInfo)
		{
			return;
		}
		LastShillingRatingInfoAt.SetValue(MillisSpentPlaying);
		StringBuilder stringBuilder2;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder2))
		{
			stringBuilder2.AppendLine(LOC.UI.Achievements_EstimatedTimePlayed(GlobalFunctions.TimePeriodHours(MinutesSpentPlaying)));
			stringBuilder2.AppendLine();
			stringBuilder2.AppendLine(LOC.UI.ShillingRatingPrompt);
			stringBuilder2.TMPLinkSurroundStringBuilderAppend(text3, LOC.UI.RateOnX("itch.io") + " (" + LOC.UI.ClickToOpen + ")");
			stringBuilder2.AppendLine();
			UiOverlay.Instance.Popup(LOC.UI.ThankYou, stringBuilder2.ToString(), new List<PopupChoice>
			{
				new PopupChoice(LOC.Polyglot.MENU_LABEL_OK, null, CommonButtonColorType.Green),
				new PopupChoice(LOC.UI.UI_DontShowTheWarningAgain, () =>
				{
					DisableShillingPopups.SetValue(value: true);
				}, CommonButtonColorType.Red)
			}).AddLinkHandler(linkIdToAction);
		}
	}

	public static void TimeStartCheckPoint()
	{
		LastCheckpointTime = DateTime.Now;
	}

	public static void TimeCheckPoint()
	{
		if (LastCheckpointTime.HasValue)
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - LastCheckpointTime.Value;
			if (timeSpan > TimeSpan.Zero)
			{
				MillisSpentPlaying.SetValue((long)MillisSpentPlaying + (long)timeSpan.TotalMilliseconds);
			}
			LastCheckpointTime = now;
		}
		else
		{
			TimeStartCheckPoint();
		}
	}

	public static void LoadSettings()
	{
		SyncResolution();
		LoadVolumeSettings();
		LoadSensitivitySettings();
		VerData.InitializeWithOnValueChanged();
		SkipUnseenTextPref.InitializeWithOnValueChanged();
		GreyOutSeenTextPref.InitializeWithOnValueChanged();
	}

	public static async UniTask InnerLocalizationEnumerator(IProgressManager manager)
	{
		await LocalizationSettings.InitializationOperation.ToUniTask(manager);
		SingletonMonoBehaviour<LocalizationSingleton>.Instance.Init();
	}

	private static async UniTask LocalizationEnumerator(IProgressManager manager)
	{
		await InnerLocalizationEnumerator(manager.PrepareSubTask(0f, 0.9f));
		SingletonMonoBehaviour<LocalizationSingleton>.Instance.ReportLocaleChanged();
		manager.ReportProgress(1f, "Finished!");
	}

	public static void SetLocale(Locale locale)
	{
		Debug.Log($"Locale changed to {locale}!");
		LocaleFontMetadata.ClearFontCache();
		LocalizationSettings.SelectedLocale = locale;
		UnityEngine.PlayerPrefs.SetString(localePlayerprefs, locale.CustomFormatterCode);
		UiOverlay.Instance.ProgressPopup("Wait!", "Wait!", LocalizationEnumerator);
	}
}
