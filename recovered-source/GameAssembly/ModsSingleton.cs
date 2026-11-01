using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Cysharp.Threading.Tasks;
using Fungus;
using GameItems;
using IC;
using IC.AutomaticReference;
using ItemConditions;
using JetBrains.Annotations;
using Melanchall.DryWetMidi.Core;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using StoryConditions;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class ModsSingleton : SingletonLoadableMonoBehaviour<ModsSingleton>, ILocaleChangedReceiver
{
	public enum ModTranslationFileLoadStatusEnum
	{
		Success,
		Empty,
		FailedToParse,
		ModNotFound,
		InvalidTranslationFile,
		InvalidLocaleCode
	}

	public enum ModTranslationFileDeleteStatusEnum
	{
		Success,
		InvalidPath,
		ErrorDeletingFile,
		FileNotFound
	}

	public enum ReplaceTranslationStatusEnum
	{
		Success,
		FailedToParseLocale,
		HasNewerVersion,
		FailedToDeleteOld
	}

	public sealed class ModTranslationDataLoaded : IEquatable<ModTranslationDataLoaded>
	{
		public enum ModTranslationDataLoadedStatusEnum
		{
			NotLoaded,
			LoadedAllGood,
			LoadedWithIssues
		}

		public readonly string Path;

		public readonly Locale TranslationLocale;

		public readonly ModTranslationData Data;

		private ModdableTable.TranslationStatus _translationStatusData;

		public ModTranslationDataLoadedStatusEnum Status { get; private set; }

		public ModTranslationDataLoaded(string path, Locale locale, ModTranslationData data)
		{
			Path = path;
			TranslationLocale = locale;
			Data = data;
		}

		public bool TryGetTranslationStatus(out ModdableTable.TranslationStatus translationStatus)
		{
			translationStatus = _translationStatusData;
			return _translationStatusData != null;
		}

		public void AfterLoad(ModdableTable.TranslationStatus loadTranslations)
		{
			_translationStatusData = loadTranslations;
			Status = ((!_translationStatusData.HasImportantIssues()) ? ModTranslationDataLoadedStatusEnum.LoadedAllGood : ModTranslationDataLoadedStatusEnum.LoadedWithIssues);
		}

		public bool Equals(ModTranslationDataLoaded other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			return object.Equals(TranslationLocale, other.TranslationLocale);
		}

		public override bool Equals(object obj)
		{
			if (this != obj)
			{
				if (obj is ModTranslationDataLoaded other)
				{
					return Equals(other);
				}
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			if (!(TranslationLocale != null))
			{
				return 0;
			}
			return TranslationLocale.GetHashCode();
		}

		public string Describe()
		{
			return TranslationLocale.Metadata.GetMetadata<LocaleName>().name + " " + Data.ParsedTargetModVersion.StringRepresentation;
		}

		public override string ToString()
		{
			return string.Format("{0}(Path: {1}: Locale:{2}, Data:{3})", "ModTranslationDataLoaded", Path, TranslationLocale, Data);
		}
	}

	[Serializable]
	public class LocalModInfoContainer
	{
		[SerializeField]
		private List<SerializableLocalModInfo> localModInfos = new List<SerializableLocalModInfo>();

		public void RemoveLocalModInfo(Mod mod)
		{
			Guid guid = mod.ModGuid;
			int num = localModInfos.FindIndex((SerializableLocalModInfo t) => t.ModGuid.RealGuid == guid);
			if (num != -1)
			{
				localModInfos.RemoveAt(num);
			}
		}

		public void ChangeModOrder(Mod mod, int newOrder)
		{
			SerializableLocalModInfo localModInfo = GetLocalModInfo(mod, createIfNotExists: true);
			if (newOrder != localModInfo.ModOrder)
			{
				int num = localModInfos.FindIndex((SerializableLocalModInfo t) => t.ModOrder == newOrder);
				if (num != -1)
				{
					localModInfos[num].ModOrder = localModInfo.ModOrder;
					localModInfo.ModOrder = newOrder;
					SingletonLoadableMonoBehaviour<ModsSingleton>.Instance._restartRequired = true;
				}
				else
				{
					localModInfo.ModOrder = newOrder;
					SingletonLoadableMonoBehaviour<ModsSingleton>.Instance._restartRequired = true;
				}
			}
		}

		private SerializableLocalModInfo CreateLocalModInfo(Mod mod)
		{
			int modOrder = ((localModInfos.Count != 0) ? (localModInfos.Max((SerializableLocalModInfo t) => t.ModOrder) + 10) : 0);
			SerializableLocalModInfo serializableLocalModInfo = SerializableLocalModInfo.GenerateFromMod(mod, modOrder);
			localModInfos.Add(serializableLocalModInfo);
			return serializableLocalModInfo;
		}

		public bool TryGetLocalModInfo(Guid guid, out SerializableLocalModInfo lmi)
		{
			lmi = localModInfos.FirstOrDefault((SerializableLocalModInfo t) => t.ModGuid.RealGuid == guid);
			return lmi != null;
		}

		public string GetRememberedNameOrGuid(Guid guid)
		{
			if (TryGetLocalModInfo(guid, out var lmi))
			{
				return lmi.ModName;
			}
			return guid.ToString();
		}

		public SerializableLocalModInfo GetLocalModInfo(Mod mod, bool createIfNotExists = false)
		{
			Guid guid = mod.ModGuid;
			SerializableLocalModInfo serializableLocalModInfo = localModInfos.FirstOrDefault((SerializableLocalModInfo t) => t.ModGuid.RealGuid == guid);
			if (serializableLocalModInfo == null)
			{
				if (createIfNotExists)
				{
					return CreateLocalModInfo(mod);
				}
			}
			else
			{
				serializableLocalModInfo.ModName = mod.Name;
			}
			return serializableLocalModInfo;
		}

		public void RemoveInvalid()
		{
			localModInfos.RemoveAll((SerializableLocalModInfo t) => t.ModGuid.RealGuid == Guid.Empty);
		}
	}

	[Serializable]
	public class ModScriptWindow
	{
		public List<string> luaFiles = new List<string>();

		[NonSerialized]
		private Mod mod;

		[NonSerialized]
		private List<Closure> _luaFilesClosures = new List<Closure>();

		public const string CurrentModGuidVariableName = "CurrentModGuid";

		public void CreateClosures(string name, Mod mod)
		{
			this.mod = mod;
			string internalFilename = mod.InternalFilename;
			for (int i = 0; i < luaFiles.Count; i++)
			{
				string text = luaFiles[i];
				ModFileBase modFileBase = mod.FindFile(text);
				if (modFileBase == null)
				{
					throw new Exception("File \"" + text + "\" mentioned in script window not found in mod files!");
				}
				mod.LogString("Loading " + modFileBase.NormalizedPath + " to lua");
				Closure closure = LuaEnvironment.LoadLuaFunction(modFileBase.StringData, $"{internalFilename}_{name}_{text}_closure{i + 1}");
				if (closure != null)
				{
					_luaFilesClosures.Add(closure);
					continue;
				}
				throw new Exception("File \"" + text + "\" script is broken!!!");
			}
		}

		public void Execute()
		{
			LuaEnvironment.Interpreter.Globals["CurrentModGuid"] = mod.ModGuid;
			foreach (Closure luaFilesClosure in _luaFilesClosures)
			{
				luaFilesClosure?.Call();
			}
		}
	}

	private Dictionary<string, Locale> _localeCodeToLocale;

	private bool _restartRequired;

	private bool _localizationReloadRequired;

	private StringBuilder _log = new StringBuilder();

	private LocalModInfoContainer localModInfoContainer;

	[SerializeField]
	private LuaEnvironment luaEnvironment;

	public bool RestartRequired => _restartRequired;

	public bool LocalizationReloadRequired => _localizationReloadRequired;

	public string Log => _log.ToString();

	public string ModPath => Defines.MyDataPath + "/Mods";

	public string TmpModPath => Defines.MyDataPath + "/ModsTmp";

	public string MainWorkingModPath => Defines.MyDataPath + "/ModsWorking";

	public string ModTranslationsPath => Defines.MyDataPath + "/ModsTranslations";

	private string LocalModInfoFileName => "localmodinfo.json";

	public List<Mod> AllMods { get; private set; } = new List<Mod>();

	public IEnumerable<(Mod mod, SerializableLocalModInfo modInfo)> AllModsWithModInfo => AllMods.Select((Mod t) => (t: t, LocalModInfo.GetLocalModInfo(t)));

	public LocalModInfoContainer LocalModInfo => localModInfoContainer;

	public static LuaEnvironment LuaEnvironment => SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.luaEnvironment;

	private void FillLocaleInfo()
	{
		_localeCodeToLocale = LocalizationSettings.Instance.GetAvailableLocales().Locales.ToDictionary((Locale x) => x.Identifier.Code, (Locale x) => x);
	}

	public Locale GetLocaleFromCodeOrProject(string localeCode)
	{
		if (localeCode != null && _localeCodeToLocale.TryGetValue(localeCode, out var value))
		{
			return value;
		}
		return LocalizationSettings.ProjectLocale;
	}

	public ModTranslationFileLoadStatusEnum ValidateTranslationFile(string rawData, out ModTranslationData data, out Mod targetMod)
	{
		data = null;
		targetMod = null;
		if (string.IsNullOrWhiteSpace(rawData))
		{
			return ModTranslationFileLoadStatusEnum.Empty;
		}
		data = JsonUtility.FromJson<ModTranslationData>(rawData);
		if (data == null)
		{
			return ModTranslationFileLoadStatusEnum.FailedToParse;
		}
		if (!data.IsValid())
		{
			return ModTranslationFileLoadStatusEnum.InvalidTranslationFile;
		}
		if (!_localeCodeToLocale.ContainsKey(data.LocaleKeyCode))
		{
			return ModTranslationFileLoadStatusEnum.InvalidLocaleCode;
		}
		targetMod = GetModByGuid(data.TargetModGuid);
		if (targetMod == null)
		{
			return ModTranslationFileLoadStatusEnum.ModNotFound;
		}
		return ModTranslationFileLoadStatusEnum.Success;
	}

	public ModTranslationFileDeleteStatusEnum DeleteTranslation(Mod mod, ModTranslationDataLoaded translationData)
	{
		string path = translationData.Path;
		string translationsDirectory = GetTranslationsDirectory(mod);
		if (!path.StartsWith(translationsDirectory))
		{
			return ModTranslationFileDeleteStatusEnum.InvalidPath;
		}
		if (!File.Exists(path))
		{
			return ModTranslationFileDeleteStatusEnum.FileNotFound;
		}
		try
		{
			_localizationReloadRequired = true;
			File.Delete(path);
			mod.RemoveTranslation(translationData.TranslationLocale);
			return ModTranslationFileDeleteStatusEnum.Success;
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			return ModTranslationFileDeleteStatusEnum.ErrorDeletingFile;
		}
	}

	public ReplaceTranslationStatusEnum ReplaceSavedTranslation(Mod mod, ModTranslationData modTranslationData, bool forceReplace)
	{
		if (!_localeCodeToLocale.TryGetValue(modTranslationData.LocaleKeyCode, out var value))
		{
			return ReplaceTranslationStatusEnum.FailedToParseLocale;
		}
		if (mod.TryGetLoadedTranslation(value, out var translation) && translation.Data.ParsedTargetModVersion.IsNewer(modTranslationData.ParsedTargetModVersion))
		{
			if (!forceReplace)
			{
				return ReplaceTranslationStatusEnum.HasNewerVersion;
			}
			if (DeleteTranslation(mod, translation) != ModTranslationFileDeleteStatusEnum.Success)
			{
				return ReplaceTranslationStatusEnum.FailedToDeleteOld;
			}
		}
		_localizationReloadRequired = true;
		string path = SaveFileHelper.SaveJson(modTranslationData.LocaleKeyCode + ".json", modTranslationData, GetTranslationsDirectory(mod));
		mod.LoadNewLocalizationAtRuntime(new ModTranslationDataLoaded(path, value, modTranslationData));
		return ReplaceTranslationStatusEnum.Success;
	}

	public bool TryGetAllAvailableTranslations(Mod mod, out HashSet<ModTranslationDataLoaded> foundTranslations)
	{
		foundTranslations = null;
		string translationsDirectory = GetTranslationsDirectory(mod);
		if (string.IsNullOrWhiteSpace(translationsDirectory) || !Directory.Exists(translationsDirectory))
		{
			return false;
		}
		foundTranslations = new HashSet<ModTranslationDataLoaded>();
		foreach (string item in GlobalFunctions.DirectorySearchFull(translationsDirectory, recursive: false))
		{
			ModTranslationData modTranslationData = JsonUtility.FromJson<ModTranslationData>(File.ReadAllText(item, Encoding.UTF8));
			if (modTranslationData != null && _localeCodeToLocale.TryGetValue(modTranslationData.LocaleKeyCode, out var value))
			{
				foundTranslations.Add(new ModTranslationDataLoaded(item, value, modTranslationData));
			}
		}
		return foundTranslations.Count > 0;
	}

	public bool TryGenerateTranslationData(Mod mod, Locale locale, out ModTranslationData translationData)
	{
		translationData = new ModTranslationData();
		translationData.TargetModGuid = mod.ModGuid;
		translationData.ParsedTargetModVersion = mod.ModVersion;
		translationData.LocaleKeyCode = locale.Identifier.Code;
		List<ModTranslationRow> list = (translationData.TranslationRows = new List<ModTranslationRow>());
		ModdableTable.FillAllKeysFor(mod, list);
		return list.Count > 0;
	}

	public Locale GetModLocaleOrProject(Guid modId)
	{
		Mod modByGuid = GetModByGuid(modId);
		if (modByGuid == null)
		{
			return LocalizationSettings.ProjectLocale;
		}
		return modByGuid.MainModInfoFile.Locale;
	}

	public void OnLocaleChanged(Locale newLocale)
	{
		_localizationReloadRequired = false;
	}

	public void ClearLog()
	{
		_log.Clear();
	}

	public void PrepareTmpPath()
	{
		GlobalFunctions.EnsureDirectory(TmpModPath, clear: true, makeHidden: true);
	}

	public string PrepareAndGetWorkingPath(MainModInfoFile infoFile)
	{
		GlobalFunctions.EnsureDirectory(MainWorkingModPath, clear: false, makeHidden: true);
		string text = MainWorkingModPath + "/" + infoFile.DirectoryName;
		GlobalFunctions.EnsureDirectory(text, clear: false, makeHidden: true);
		return text;
	}

	public string GetWorkingPath(Mod mod)
	{
		return MainWorkingModPath + "/" + mod.MainModInfoFile.DirectoryName;
	}

	public string GetTranslationsDirectory(Mod mod)
	{
		return ModTranslationsPath + "/" + mod.MainModInfoFile.DirectoryName;
	}

	public string PrepareAndGetTranslationsPath(Mod mod)
	{
		string translationsDirectory = GetTranslationsDirectory(mod);
		GlobalFunctions.EnsureDirectory(translationsDirectory, clear: false, makeHidden: true);
		return translationsDirectory;
	}

	private Mod LoadModFromFile(string path)
	{
		return LoadModFromStream(File.OpenRead(path));
	}

	public Result LoadModFromZipDataFromOutside(byte[] data)
	{
		try
		{
			using MemoryStream data2 = new MemoryStream(data);
			Mod mod = LoadModFromStream(data2);
			File.WriteAllBytes(ModPath + "/" + mod.InternalFilename, data);
			if (mod.Status == ModStatus.Ready && localModInfoContainer.GetLocalModInfo(mod, createIfNotExists: true).LoadOnStart)
			{
				_restartRequired = true;
			}
			return Result.Success;
		}
		catch (Exception ex)
		{
			return Result.FailWithDescription("Exception:\n" + ex);
		}
	}

	private Mod LoadModFromStream(Stream data)
	{
		using MyZipUtility.PreparedModZip preparedModZip = new MyZipUtility.PreparedModZip(data);
		return LoadModFromModFiles(preparedModZip.GetModFiles());
	}

	private Mod LoadModFromModFiles(IEnumerable<ModFileBase> modFileBases)
	{
		Mod mod = Mod.LoadModIntoItsWorkingDirectory(modFileBases);
		int num = AllMods.FindIndex((Mod t) => t.ModGuid == mod.ModGuid);
		if (num == -1)
		{
			AllMods.Add(mod);
		}
		else
		{
			AllMods[num] = mod;
			_log.AppendLine("Mod \"" + mod.MainModInfoFile.name + "\" overriden.");
		}
		mod.LogString("Mod fully loaded!");
		return mod;
	}

	public void RemoveAndDeleteMod(Mod mod)
	{
		int num = AllMods.IndexOf(mod);
		if (num >= 0)
		{
			if (mod.Status == ModStatus.Active)
			{
				_restartRequired = true;
			}
			File.Delete(ModPath + "/" + mod.InternalFilename);
			AllMods.RemoveAt(num);
		}
	}

	public byte[] SaveModToZipData(Mod mod)
	{
		PrepareTmpPath();
		mod.SaveToFolder(TmpModPath);
		return MyZipUtility.CreateZipFile(TmpModPath);
	}

	protected override async UniTask LoadInner(IProgressManager progressManager)
	{
		FillLocaleInfo();
		GlobalFunctions.EnsureDirectory(ModPath);
		GlobalFunctions.EnsureDirectory(MainWorkingModPath);
		LoadLocalModInfo();
		progressManager.ReportProgress(0.01f);
		float loadModPercent = 0.2f;
		AllMods.Clear();
		PrepareLuaEnvironment();
		string[] files = Directory.GetFiles(ModPath, "*.zip");
		for (int i = 0; i < files.Length; i++)
		{
			string fullPath = files[i];
			string filename = Path.GetFileName(fullPath);
			progressManager.ReportProgress(LOC.UI.Mods_Preparing(filename));
			await progressManager.YieldIfTooLongSinceFrame();
			string internalFilename = LoadModFromFile(fullPath).InternalFilename;
			if (internalFilename != filename)
			{
				string text = ModPath + "/" + internalFilename;
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Move(fullPath, text);
			}
			progressManager.ReportProgress(0.05f + (float)i / (float)files.Length * loadModPercent);
		}
		progressManager.ReportProgress(0.05f + loadModPercent);
		DirectoryInfo directoryInfo = new DirectoryInfo(MainWorkingModPath);
		if (directoryInfo.Exists)
		{
			HashSet<string> hashSet;
			using (MyCollectionPool<string>.GetHashSetAndFill(from x in directoryInfo.GetDirectories()
				select x.Name, out hashSet))
			{
				foreach (Mod allMod in AllMods)
				{
					hashSet.Remove(allMod.MainModInfoFile.DirectoryName);
					localModInfoContainer.GetLocalModInfo(allMod, createIfNotExists: true);
				}
				foreach (string item in hashSet)
				{
					Directory.Delete(MainWorkingModPath + "/" + item, recursive: true);
				}
			}
		}
		SaveLocalModInfo();
		AllMods.Sort(Mod.ModOrderComparer);
		progressManager.ReportProgress(0.2f);
		float startModPercent = 0.8f;
		for (int i = 0; i < AllMods.Count; i++)
		{
			Mod mod = AllMods[i];
			SerializableLocalModInfo localModInfo = localModInfoContainer.GetLocalModInfo(mod);
			if (localModInfo != null && localModInfo.LoadOnStart)
			{
				progressManager.ReportProgress(LOC.UI.Mods_Starting(mod.Name));
				await progressManager.YieldIfTooLongSinceFrame();
				mod.Start();
			}
			progressManager.ReportProgress(0.2f + (float)i / (float)AllMods.Count * startModPercent);
		}
		progressManager.ReportProgress(LOC.UI.Mods_LocalizingMods);
		ItemPrefab.ItemPrefabManager.CreateLocalizationEntries();
		for (int i = 0; i < AllMods.Count; i++)
		{
			Mod mod = AllMods[i];
			SerializableLocalModInfo localModInfo2 = localModInfoContainer.GetLocalModInfo(mod);
			if (localModInfo2 != null && localModInfo2.LoadOnStart)
			{
				await progressManager.YieldIfTooLongSinceFrame();
				mod.LoadLocalization();
			}
		}
	}

	[CanBeNull]
	public Mod GetModByGuid(Guid guid)
	{
		return AllMods.FirstOrDefault((Mod t) => t.ModGuid == guid);
	}

	public override Type[] GetDependenciesArray()
	{
		return new Type[8]
		{
			typeof(CookingSongPrefabManager),
			typeof(FishingLocationDataManager),
			typeof(ShopPrefabManager),
			typeof(StockCompanyPrefabManager),
			typeof(EmailPrefabManager),
			typeof(ItemPrefab.ItemPrefabManager),
			typeof(CocktractPartnerPrefabManager),
			typeof(ModdableTable)
		};
	}

	private void LoadLocalModInfo()
	{
		if (SaveFileHelper.TryLoadFileToJson<LocalModInfoContainer>(LocalModInfoFileName, out var data, ModPath))
		{
			localModInfoContainer = data;
			return;
		}
		localModInfoContainer = new LocalModInfoContainer();
		Debug.Log("Failed to load localmodinfo.json file! Creating a new one!");
	}

	public void SaveLocalModInfo()
	{
		if (localModInfoContainer != null)
		{
			SaveFileHelper.SaveJson(LocalModInfoFileName, localModInfoContainer, ModPath);
		}
	}

	public void ToggleLoadOnStart(Mod mod)
	{
		SerializableLocalModInfo localModInfo = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.GetLocalModInfo(mod, createIfNotExists: true);
		localModInfo.LoadOnStart = !localModInfo.LoadOnStart;
		SaveLocalModInfo();
		_restartRequired = true;
	}

	public void ChangeModOrder(Mod value, int newOrder)
	{
		LocalModInfo.ChangeModOrder(value, newOrder);
		SaveLocalModInfo();
	}

	public void OnDestroy()
	{
		if (IsLoaded)
		{
			SaveLocalModInfo();
		}
	}

	private void PrepareLuaEnvironment()
	{
		luaEnvironment.InitEnvironment();
		ScriptHelper.RegisterAll();
		ScriptHelper.RegisterSimpleFunc<Item, bool>();
		ScriptHelper.RegisterSimpleAction<bool, Item>();
		ScriptHelper.RegisterSimpleAction<Item>();
		ScriptHelper.RegisterSimpleAction<TextureOverriderManager, Item>();
		ScriptHelper.RegisterSimpleAction<Material>();
		ScriptHelper.RegisterSimpleAction<Material, List<UnityEngine.Object>>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ControllerEnum>();
		Table globals = luaEnvironment.Interpreter.Globals;
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Guid>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<GameId>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Item>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemPrefab>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemPrefab.ItemPrefabManager>();
		UserData.RegisterType<SpecialVariablesHolder>();
		UserData.RegisterType<PackedTexturesContainer>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemCategory>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemSubCategory>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemPrefab.ScratchTextureType>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<SusArea>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ColorSlot>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ColorSlotTransparencyEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<SusModifier>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<AllowedModifiers>();
		UserData.RegisterType<AllowedModifiers.Builder>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<CommonAllowedModifiersEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<SlotEquipData>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemScopeCondition>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemScopeConditionCustom>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemScopeConditionSpecialVariable>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemScopeConditionAlwaysTrueCondition>();
		ScriptHelper.RegisterSimpleFunc<Live2DController, Item, Live2DController.Live2DControllerItemModifier>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemModifiableVariableFactory>();
		UserData.RegisterType<ItemModifiableVariable>();
		UserData.RegisterType<ItemModifiableVariableIntBackedBool>();
		UserData.RegisterType<ItemModifiableVariableFloat01Percent>();
		UserData.RegisterType<ItemModifiableVariableFloat01Percent.FloatVariableTypeEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemModifiableVariablesRequirement>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<AtLeastOneBoolVariableTrueRequirement>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ExactlyOneBoolVariableTrueRequirement>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<AtMostOneBoolVariableTrueRequirement>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<EdibleItemPrefabData>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<EdibleItemNutritionProfile>();
		ScriptHelper.RegisterSimpleAction<Item, EdibleItemNutritionProfile>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<EdibleTypeEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<FishItemPrefabData>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<FishPreferences>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<FishingRodItemPrefabData>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<FishingBaitItemPrefabData>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<FishingBaitType>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<FishingCatchBehaviourType>();
		UserData.RegisterType<GameStatFixedValue>();
		UserData.RegisterType<GameStat<bool>>();
		UserData.RegisterType<GameStat<GameVariables.EatSound>>();
		UserData.RegisterType<GameStatPercentBoost>();
		UserData.RegisterType<GameStatRandomPercentage>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Shop>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ShopManager>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ShopItem>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ColorPaletteManager>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ColorPaletteManager.ColorPalette>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ColorPaletteManager.ColorPaletteEntry>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemSlot.SlotManager>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemSlot>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ItemSlot.SlotType>();
		UserData.RegisterType<GameVariables>();
		UserData.RegisterType<Item.ItemManager>();
		UserData.RegisterType<EventManager>();
		UserData.RegisterType<CockTwitchManager>();
		UserData.RegisterType<CocktractManager>();
		UserData.RegisterType<DeliveryManager>();
		UserData.RegisterType<NewsDataManager>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<NewsDataManager.NewsId>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<GameVariables.TimeCheckType>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<GameVariables.CheckType>();
		UserData.RegisterType<Live2DController>();
		UserData.RegisterType<Live2DLewdSceneController>();
		UserData.RegisterType<Live2DBlowjobController>();
		UserData.RegisterType<Live2DCowgirlController>();
		UserData.RegisterType<Live2DInteractionController>();
		UserData.RegisterType<Live2DShowerController>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Live2DControllerSingleton>();
		UserData.RegisterType<MidiFile>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<CookingSong>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Recipe>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<NoteButtonTypeEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<CookingSongRuntimeData>();
		UserData.RegisterType<CookingSongRuntimeData.Builder>();
		ScriptHelper.RegisterSimpleFunc<CookingSong, CookingSongRuntimeData>();
		ScriptHelper.RegisterSimpleFunc<CookingMinigameManager, bool>();
		UserData.RegisterType<IReference<MidiFile>>();
		UserData.RegisterType<IReference<IMusicWithInfo>>();
		UserData.RegisterType<ModMusicWithInfo>();
		R.RegisterLua(globals);
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<TextureOverriderManager>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Result>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StorySingleton>();
		UserData.RegisterType<StorySingleton.PersonalityModuleDefinition>();
		UserData.RegisterType<StorySingleton.LuaPersonalityModuleDefinition>();
		UserData.RegisterType<StoryBotDialogueBranchContainer>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryCondition>();
		UserData.RegisterType<StoryCondition.CommonConditionsContainer>();
		UserData.RegisterType<FlagConditionBasic>();
		UserData.RegisterType<FlagConditionWithTime>();
		UserData.RegisterType<FlagConditionSpecialWithTime>();
		UserData.RegisterType<ItemCondition>();
		UserData.RegisterType<ItemInSlotCondition>();
		UserData.RegisterType<ConstCondition>();
		UserData.RegisterType<LuaStoryCondition>();
		UserData.RegisterType<DelegateCondition>();
		UserData.RegisterType<StoryEntryPointOptionsExistCondition>();
		UserData.RegisterType<OrCondition>();
		UserData.RegisterType<AndCondition>();
		UserData.RegisterType<DataCondition<int>>();
		UserData.RegisterType<DataCondition<float>>();
		UserData.RegisterType<DataCondition<bool>>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryBotContainerTypeEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryBotDialogueBranch>();
		UserData.RegisterType<StoryBotDialogueBranchContainer>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryAutoBranch>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryMenuBranch>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryBranchTarget>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryBlockTarget>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryEnumeratorTarget>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<StoryLocalizedConversationBranchTarget>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<DataConditionComparators.SingleArgumentComparators>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<DataConditionComparators.TwoArgumentComparators>();
		UserData.RegisterType<ManagedCoroutineWithTarget>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<ManagedCoroutine.FinishedType>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<SimpleLocalizedString>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<SaveSingleton>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<SaveContainer>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<GlobalFlag>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Live2DController.CustomizableLive2DControllerItemModifier>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<Live2DController.UpdateTexturesFromCurrentlyEquippedTypeEnum>();
		ScriptHelper.RegisterSimpleAction<Live2DController>();
		ScriptHelper.RegisterSimpleAction<Live2DController, Live2DController.UpdateTexturesFromCurrentlyEquippedTypeEnum>();
		UserData.RegisterType<ModelBrain>();
		UserData.RegisterType<Live2DExpression>();
		UserData.RegisterType<InteractionMemory>();
		UserData.RegisterType<SimpleEventMemory>();
		UserData.RegisterType<ComplexEventMemory>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<InteractionMemoryEventEnum>();
		luaEnvironment.RegisterTypeAndAddTypeToGlobalTable<InteractionMemoryEnum>();
		UserData.RegisterType<CustomConversionReference<Texture2D, Sprite>>();
		UserData.RegisterType<WaitForSeconds>();
		UserData.RegisterType<Flowchart>();
		UserData.RegisterType<ClothierViewScene>();
		UserData.RegisterType<BrowserPopup>();
		UserData.RegisterType<BotStatusAppPopup>();
		UserData.RegisterType<SimplePopup>();
		UserData.RegisterType<NunRepairPopup>();
		UserData.RegisterType<FisherSellFishPopup>();
		UserData.RegisterType<ClothierOrdersPopup>();
		UserData.RegisterType<AnnalieShopPopup>();
		UserData.RegisterType<TimeoutSimplePopup>();
		luaEnvironment.RegisterTypeAndAddValueToGlobalTable(SingletonMonoBehaviour<SoundSingleton>.Instance, "SoundSingleton");
		luaEnvironment.RegisterTypeAndAddValueToGlobalTable(SingletonMonoBehaviour<ModUtilities>.Instance, "ModUtilities");
		luaEnvironment.RegisterTypeAndAddValueToGlobalTable(UiOverlay.Instance, "UiOverlay");
		luaEnvironment.RegisterTypeAndAddValueToGlobalTable(SingletonMonoBehaviour<GameUtilities>.Instance, "GameUtilities");
		luaEnvironment.RegisterTypeAndAddValueToGlobalTable(SingletonMonoBehaviour<ViewSingleton>.Instance, "ViewSingleton");
		UserData.RegisterType<ModCustomStringReference>();
		UserData.RegisterType<IReference<AudioClip>>();
		UserData.RegisterType<AudioClip>();
	}

	private void DescribeTable(StringBuilder sb, Table table, int depth = 0)
	{
		if (depth > 5)
		{
			sb.Append("Table too deep! Max depth = 5");
			return;
		}
		foreach (TablePair pair in table.Pairs)
		{
			sb.Append('\t', depth);
			sb.Append(pair.Key);
			sb.Append(" - ");
			if (pair.Value.Type == DataType.UserData)
			{
				UserData userData = pair.Value.UserData;
				sb.Append("(Userdata Type:");
				sb.Append(userData.Descriptor.Type);
				sb.Append(")");
			}
			if (pair.Value.Type == DataType.String)
			{
				sb.Append('"');
				sb.Append(pair.Value.String.TruncateLongString(10).Replace("\n", "\\n"));
				sb.Append("(Rest truncated)\"");
			}
			else if (pair.Value.Type == DataType.Table)
			{
				if (pair.Value.Table == table)
				{
					sb.Append("Self reference");
				}
				else
				{
					sb.Append('\t', depth);
					sb.AppendLine("[");
					DescribeTable(sb, pair.Value.Table, depth + 1);
					sb.Append('\t', depth);
					sb.AppendLine("]");
				}
			}
			else
			{
				sb.Append(pair.Value);
			}
			sb.AppendLine();
		}
	}

	public string GenerateLuaDocumentation()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("This file has all types and methods accessible from lua scripts.\nIt might be helpful while writing more advanced mods!");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("All values in the global table:");
			DescribeTable(stringBuilder, luaEnvironment.Interpreter.Globals);
			stringBuilder.AppendLine("-------------------");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Parameter names are in the following format: name_type");
			stringBuilder.AppendLine("For example:");
			stringBuilder.AppendLine("System.Int32 fun(a__System.Int32, b__System.Int32)");
			stringBuilder.AppendLine("is a function that looks like this");
			stringBuilder.AppendLine("int fun(int a, int b)");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("All types and members defined on them:");
			foreach (KeyValuePair<Type, IUserDataDescriptor> item in UserData.GetRegisteredTypesFull())
			{
				if (item.Value is DispatchingUserDataDescriptor dispatchingUserDataDescriptor)
				{
					if (dispatchingUserDataDescriptor.Type.IsEnum)
					{
						stringBuilder.AppendLine("Enum: " + item.Value.Name);
						stringBuilder.AppendLine("Enum values: ");
						foreach (KeyValuePair<string, IMemberDescriptor> member in dispatchingUserDataDescriptor.Members)
						{
							stringBuilder.AppendLine(member.Value.Name);
						}
						stringBuilder.AppendLine();
					}
					else
					{
						stringBuilder.Append(item.Value.Name);
						Type type = item.Value.Type;
						Type[] array = type.GetParentTypes().ToArray();
						if (array.Length != 0)
						{
							stringBuilder.Append(" inherits from ");
							stringBuilder.AppendJoin(", ", array.Select((Type t) => t.Name));
						}
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("Defined members: ");
						foreach (KeyValuePair<string, IMemberDescriptor> member2 in dispatchingUserDataDescriptor.Members)
						{
							IMemberDescriptor value = member2.Value;
							if (value is OverloadedMethodMemberDescriptor overloadedMethodMemberDescriptor)
							{
								if (value.Name.StartsWith("get_") || value.Name.StartsWith("set_"))
								{
									continue;
								}
								foreach (IOverloadableMemberDescriptor overload in overloadedMethodMemberDescriptor.Overloads)
								{
									if (overload is MethodMemberDescriptor { MethodInfo: MethodInfo methodInfo })
									{
										if (Attribute.IsDefined(methodInfo, typeof(ObsoleteAttribute)) || methodInfo.DeclaringType != type)
										{
											continue;
										}
										stringBuilder.Append(methodInfo.ReturnType.Name);
										stringBuilder.Append(" ");
									}
									stringBuilder.Append(overloadedMethodMemberDescriptor.Name);
									stringBuilder.Append("(");
									stringBuilder.AppendJoin(", ", overload.Parameters.Select((ParameterDescriptor p) => p.Name + "__" + p.Type));
									stringBuilder.Append(")");
									stringBuilder.AppendLine();
								}
							}
							else if (value is PropertyMemberDescriptor { PropertyInfo: var propertyInfo } propertyMemberDescriptor)
							{
								if (!Attribute.IsDefined(propertyInfo, typeof(ObsoleteAttribute)))
								{
									stringBuilder.Append(propertyInfo.PropertyType.ToString());
									stringBuilder.Append(" ");
									stringBuilder.Append(value.Name);
									stringBuilder.Append(" { ");
									if (propertyMemberDescriptor.CanRead)
									{
										stringBuilder.Append("get;");
									}
									if (propertyMemberDescriptor.CanWrite)
									{
										stringBuilder.Append(" set;");
									}
									stringBuilder.Append(" }");
									stringBuilder.AppendLine();
								}
							}
							else if (value is FieldMemberDescriptor { FieldInfo: var fieldInfo })
							{
								if (!Attribute.IsDefined(fieldInfo, typeof(ObsoleteAttribute)))
								{
									stringBuilder.Append(fieldInfo.FieldType.ToString());
									stringBuilder.Append(" ");
									stringBuilder.Append(value.Name);
									stringBuilder.AppendLine();
								}
							}
							else
							{
								stringBuilder.Append(value.Name + " " + value.GetType());
								stringBuilder.AppendLine();
							}
						}
					}
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
	}

	public string GenerateEmptyPersonalityDefinition()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("-- Good luck :)");
		string text = "personality";
		stringBuilder.AppendLine("local " + text + " = ModUtilities.PrepareNewPersonalityDefinition()");
		StorySingleton instance = SingletonMonoBehaviour<StorySingleton>.Instance;
		StoryBotDialogueStage stage = instance.Stage3;
		int num = 0;
		foreach (StoryBotDialogueTagDefinition allPersonalityModifiableTag in StorySingleton.PersonalityModuleDefinition.GetAllPersonalityModifiableTags(instance))
		{
			string text2 = "";
			if (stage.TryGetTag(allPersonalityModifiableTag.Id, out var container))
			{
				StoryBotDialogueBranch storyBotDialogueBranch = container.GetBranches().FirstOrDefault();
				if (storyBotDialogueBranch != null)
				{
					if (storyBotDialogueBranch.BranchTarget is StoryLocalizedConversationBranchTarget storyLocalizedConversationBranchTarget)
					{
						text2 = storyLocalizedConversationBranchTarget.Conversation.mainText;
					}
					else if (storyBotDialogueBranch.BranchTarget is StoryLocalizedStringBranchTarget storyLocalizedStringBranchTarget)
					{
						text2 = storyLocalizedStringBranchTarget.TargetText.GetValueProjectLocale().Split('|').First();
					}
				}
			}
			stringBuilder.AppendLine(text + ".PrepareContainer(" + allPersonalityModifiableTag.Id.ToLuaString() + ").AddBranch(" + LuaUtilities.LuaConstructor("StoryBotDialogueBranch", text2.ToLuaString(), "CurrentModGuid", num.ToString()) + ")");
			num++;
		}
		stringBuilder.AppendLine();
		stringBuilder.AppendLine("-- rename this to the prefab and gameid in your mod");
		stringBuilder.AppendLine("itemprefab0.TurnIntoPersonalityModule(itemgameid0, " + text + ")");
		return stringBuilder.ToString();
	}
}
