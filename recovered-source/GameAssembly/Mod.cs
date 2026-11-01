using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public sealed class Mod
{
	private class UnityLogManager : IDisposable
	{
		private Application.LogCallback _logCallback;

		public bool ReceivedSomeSortOfAnError;

		public UnityLogManager(Application.LogCallback logCallback)
		{
			_logCallback = logCallback;
			Application.logMessageReceivedThreaded += _logCallback;
			Application.logMessageReceivedThreaded += UnityLog;
		}

		private void UnityLog(string logString, string stackTrace, LogType type)
		{
			if (type == LogType.Exception || type == LogType.Error || type == LogType.Assert)
			{
				ReceivedSomeSortOfAnError = true;
			}
		}

		public void Dispose()
		{
			Application.logMessageReceivedThreaded -= _logCallback;
			Application.logMessageReceivedThreaded -= UnityLog;
		}
	}

	private sealed class ModOrderRelationalComparer : IComparer<Mod>
	{
		public int Compare(Mod x, Mod y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			SerializableLocalModInfo localModInfo = x.GetLocalModInfo();
			SerializableLocalModInfo localModInfo2 = y.GetLocalModInfo();
			return localModInfo.ModOrder.CompareTo(localModInfo2.ModOrder);
		}
	}

	public enum ModLocalizationStatus
	{
		NativeModLanguage,
		Translated,
		BrokenTranslation,
		NotTranslated
	}

	private Stopwatch modStopwatch = new Stopwatch();

	[TextArea(3, 10)]
	private StringBuilder log = new StringBuilder();

	[NonSerialized]
	private MainModInfoFile _mainModInfoFile;

	private SerializableLocalModInfo _localModInfo;

	private Dictionary<string, ModCustomStringReference> _customStrings = new Dictionary<string, ModCustomStringReference>();

	[CanBeNull]
	private Dictionary<Locale, ModsSingleton.ModTranslationDataLoaded> _translations;

	private List<ModFileBase> _filesIncluded = new List<ModFileBase>();

	public string Log => log.ToString();

	public MainModInfoFile MainModInfoFile => _mainModInfoFile ?? (_mainModInfoFile = MainModInfoFile.CreateMinimal());

	public string DirectoryName => MainModInfoFile.DirectoryName;

	public string InternalFilename => MainModInfoFile.guid.ToString() + ".zip";

	public string HumanFilename => GlobalFunctions.ReplaceInvalidChars(MainModInfoFile.name) + ".zip";

	public string Name => MainModInfoFile.name;

	public Guid ModGuid => MainModInfoFile.guid;

	public ParsedVersion TargetGameVersion => MainModInfoFile.ParsedTargetGameVersion;

	public ParsedVersion ModVersion => MainModInfoFile.ParsedModVersion;

	public static IComparer<Mod> ModOrderComparer { get; } = new ModOrderRelationalComparer();

	[CanBeNull]
	public IReadOnlyDictionary<Locale, ModsSingleton.ModTranslationDataLoaded> LoadedTranslations => _translations;

	public string WorkingDirectory => SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetWorkingPath(this);

	public ReadOnlyCollection<ModFileBase> FilesIncluded => _filesIncluded.AsReadOnly();

	public ModStatus Status { get; private set; } = ModStatus.Not_Loaded;

	private Mod()
	{
		modStopwatch.Start();
	}

	private void LogTimeStamp()
	{
		log.Append(modStopwatch.ElapsedMilliseconds).Append(" ms: ");
	}

	private void LogNewLine()
	{
		log.AppendLine();
	}

	private void UnityLog(string logString, string stackTrace, LogType type)
	{
		LogTimeStamp();
		log.AppendLine($"Log from unity. Type: {type}");
		log.AppendLine(logString);
	}

	private void LogStringHeader(string str)
	{
		LogNewLine();
		log.Append(">");
		log.Append(str);
		log.AppendLine();
	}

	public void LogString(string str)
	{
		LogTimeStamp();
		log.AppendLine(str);
	}

	public static Mod CreateMinimal()
	{
		Mod mod = new Mod();
		using (new UnityLogManager(mod.UnityLog))
		{
			mod._mainModInfoFile = MainModInfoFile.CreateMinimal();
			mod.CreateClosures();
			return mod;
		}
	}

	public static Mod CreateItemMod(List<ItemPrefab> items)
	{
		Mod mod = new Mod();
		using (new UnityLogManager(mod.UnityLog))
		{
			mod._mainModInfoFile = MainModInfoFile.CreateMinimal();
			StringBuilderCache.AcquireManual(out var stringBuilder);
			stringBuilder.AppendLine("--This script was generated automatically!\n--Good luck with making the mod!\n--May Sheep be with you!");
			stringBuilder.AppendLine("do");
			stringBuilder.AppendLine();
			HashSet<string> hashSet;
			using (MyCollectionPool<string>.GetHashSet(out hashSet))
			{
				foreach (var item5 in items.Select((ItemPrefab item5, int i) => (item: item5, i: i)))
				{
					ItemPrefab item = item5.item;
					int item2 = item5.i;
					string text = $"itemprefab{item2}";
					item.ToLua(text, stringBuilder);
					stringBuilder.Append("\n\n");
					string text2 = GlobalFunctions.ReplaceInvalidChars(item.Name);
					if (hashSet.Contains(text2))
					{
						text2 += item2;
					}
					hashSet.Add(text2);
					foreach (var item6 in item.Textures.Select((PackedTexturesContainer x, int o) => (x: x, o: o)))
					{
						PackedTexturesContainer item3 = item6.x;
						int item4 = item6.o;
						string text3 = "/" + text2 + "/";
						string nameUsingAffectedControllersId = item3.GetNameUsingAffectedControllersId();
						string luaNameUsingAffectedControllersId = item3.GetLuaNameUsingAffectedControllersId();
						string text4 = $"tex {item4 + 1} - {nameUsingAffectedControllersId}";
						string text5 = text3 + text4 + ".json";
						string text6 = $"itemprefab{item2}_{luaNameUsingAffectedControllersId}_texture{item4}";
						mod._filesIncluded.AddRange(item3.SaveToModfiles(text5));
						stringBuilder.AppendLine("local " + text6 + " = ModUtilities.GetPackedTexture(CurrentModGuid, " + text5.ToLuaString() + ")\n");
						stringBuilder.AppendLine(text + ".AddTexture(" + text6 + ")\n");
					}
					string text7 = $"itemgameid{item2}";
					stringBuilder.AppendLine("local " + text7 + " = ModUtilities.CreateNewItemAutoAssignId(CurrentModGuid, " + text + ")");
					if (item.IsIllegal)
					{
						stringBuilder.AppendLine("ModUtilities.AddSingleBuyItemToShop(" + "ladyparts.ic".ToLuaString() + ", " + text7 + ")\n\n");
					}
					else
					{
						stringBuilder.AppendLine("ModUtilities.AddSingleBuyItemToShop(" + "clothier".ToLuaString() + ", " + text7 + ")\n\n");
					}
				}
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("end");
				string text8 = "script.lua";
				mod._filesIncluded.Add(new ModFileInMemory(text8, StringBuilderCache.ReleaseWithToString(stringBuilder)));
				mod.MainModInfoFile.OnGameStart.luaFiles.Add(text8);
				mod.CreateClosures();
				return mod;
			}
		}
	}

	public static Mod LoadModIntoItsWorkingDirectory(IEnumerable<ModFileBase> rawModFiles)
	{
		Mod mod = new Mod();
		using (new UnityLogManager(mod.UnityLog))
		{
			try
			{
				mod.LogStringHeader("Starting mod loading!");
				List<ModFileBase> filesIncluded = mod._filesIncluded;
				filesIncluded.AddRange(rawModFiles);
				string target = "/" + MainModInfoFile.ModInfoFileName;
				int num = filesIncluded.FindIndex((ModFileBase t) => t.NormalizedPath == target);
				if (num == -1)
				{
					mod.LogString("\"" + MainModInfoFile.ModInfoFileName + "\" file not found!");
					mod.Status = ModStatus.Exception;
					return mod;
				}
				if (!SaveFileHelper.TryParseStringToJson<MainModInfoFile>(filesIncluded[num].StringData, out var output))
				{
					mod.LogString("\"" + MainModInfoFile.ModInfoFileName + "\" file found, but it has a wrong format!");
					mod.Status = ModStatus.Exception;
					return mod;
				}
				filesIncluded.RemoveAt(num);
				if (output.guid == Guid.Empty)
				{
					mod.LogString("Guid is wrong! It cannot be empty!");
					mod.Status = ModStatus.Exception;
					return mod;
				}
				if (output.IsDefault())
				{
					mod.LogString("Change name in " + MainModInfoFile.ModInfoFileName + "! Mod REQUIRES a name!");
				}
				mod._mainModInfoFile = output;
				string text = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.PrepareAndGetWorkingPath(output);
				if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.TryGetLocalModInfo(output.guid, out var lmi) && !lmi.LoadOnStart)
				{
					mod.LogStringHeader("Mod set to not load on start! stopping the loading process");
					mod.Status = ModStatus.Not_Loaded;
					return mod;
				}
				mod.LogStringHeader("Moving the mod to its working directory in \"" + text + "\"(All other paths are relative)");
				HashSet<string> hashSet = GlobalFunctions.DirectorySearchRelative(text).ToHashSet();
				hashSet.Remove(MainModInfoFile.ModInfoFileName);
				mod.LogString($"Zip has {filesIncluded.Count} files!");
				mod.LogString($"Found {hashSet.Count} files in the directory: {hashSet.ToStringBetter()}");
				mod.LogStringHeader("Creating directories");
				HashSet<string> hashSet2;
				using (MyCollectionPool<string>.GetHashSet(out hashSet2))
				{
					foreach (ModFileBase item in filesIncluded)
					{
						hashSet2.Add(Path.GetDirectoryName(item.NormalizedPath));
					}
					foreach (string item2 in hashSet2)
					{
						string text2 = text + item2;
						if (!Directory.Exists(text2))
						{
							Directory.CreateDirectory(text2);
							mod.LogString("Creating directory \"" + text2 + "\"");
						}
					}
				}
				mod.LogString("All directories ready!");
				mod.LogStringHeader("Moving files");
				for (int num2 = 0; num2 < filesIncluded.Count; num2++)
				{
					ModFileBase modFileBase = filesIncluded[num2];
					string normalizedPath = modFileBase.NormalizedPath;
					WorkingDirectoryModFileOnHardDrive workingDirectoryModFileOnHardDrive = new WorkingDirectoryModFileOnHardDrive(normalizedPath, mod);
					hashSet.Remove(normalizedPath);
					filesIncluded[num2] = workingDirectoryModFileOnHardDrive;
					mod.LogString("Copying mod file: \"" + normalizedPath + "\"");
					using Stream stream = modFileBase.OpenReadStream();
					try
					{
						using FileStream fileStream = File.Open(workingDirectoryModFileOnHardDrive.HardDrivePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
						if (StreamUtilities.AreStreamsEqual(stream, fileStream))
						{
							mod.LogString("File exists! Skipped!");
							continue;
						}
						mod.LogString("File changed! Saving changes!");
						fileStream.SetLength(0L);
						using Stream source = modFileBase.OpenReadStream();
						source.CopyToUsingCommonBuffer(fileStream);
						mod.LogString("Done!");
					}
					catch (FileNotFoundException)
					{
						stream.WriteToFile(workingDirectoryModFileOnHardDrive.HardDrivePath);
						mod.LogString("Done!");
					}
				}
				if (hashSet.Count == 0)
				{
					mod.LogString("No files to remove!");
				}
				else
				{
					mod.LogStringHeader($"{hashSet.Count} additional files in the working directory. Removing them!");
					foreach (string item3 in hashSet)
					{
						string text3 = text + item3;
						mod.LogString(text3);
						File.Delete(text3);
					}
					mod.LogString("Done");
				}
				mod.CreateClosures();
			}
			catch (Exception ex2)
			{
				mod.LogStringHeader("Exception happened while loading the mod:");
				mod.LogString(ex2.ToString());
				mod.Status = ModStatus.Exception;
			}
			return mod;
		}
	}

	private void CreateClosures()
	{
		LogStringHeader("Creating closures");
		try
		{
			LogString("Processing lua files(if they exist!)");
			MainModInfoFile.OnGameStart.CreateClosures("OnGameStart", this);
			LogString("Loading successful!");
			Status = ModStatus.Ready;
		}
		catch (Exception ex)
		{
			LogString("Creating closures failed:");
			LogString(ex.ToString());
			Status = ModStatus.Exception;
		}
	}

	public ModFileBase FindFile(string file)
	{
		if (!file.StartsWith("\\") && !file.StartsWith("/"))
		{
			file = "/" + file;
		}
		return _filesIncluded.FirstOrDefault((ModFileBase t) => t.NormalizedPath == file);
	}

	public SerializableLocalModInfo GetLocalModInfo(bool createIfNotExits = true)
	{
		return _localModInfo ?? (_localModInfo = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.LocalModInfo.GetLocalModInfo(this, createIfNotExits));
	}

	public SerializableLocalModInfo GetLocalModInfo(ModsSingleton.LocalModInfoContainer localModInfoContainer, bool createIfNotExits)
	{
		return _localModInfo ?? (_localModInfo = localModInfoContainer.GetLocalModInfo(this, createIfNotExits));
	}

	public ModCustomStringReference CreateCustomLocalizedString(string key, string value, bool smartString)
	{
		if (!_customStrings.TryGetValue(key, out var value2))
		{
			value2 = new ModCustomStringReference(this, key, smartString);
			_customStrings.Add(key, value2);
		}
		ModdableTable.CreateCustomString(this, key, value, smartString);
		return value2;
	}

	public bool TryGetCustomLocalizedString(string key, out ModCustomStringReference customString)
	{
		return _customStrings.TryGetValue(key, out customString);
	}

	public bool RemoveTranslation(Locale locale)
	{
		if (_translations != null)
		{
			bool result = _translations.Remove(locale);
			ModdableTable.ClearTranslation(this, locale);
			if (_translations.Count == 0)
			{
				_translations = null;
			}
			return result;
		}
		return false;
	}

	public bool TryGetLoadedTranslations(out IReadOnlyDictionary<Locale, ModsSingleton.ModTranslationDataLoaded> translations)
	{
		translations = _translations;
		return translations != null;
	}

	public bool TryGetLoadedTranslation(Locale locale, out ModsSingleton.ModTranslationDataLoaded translation)
	{
		translation = null;
		if (_translations == null)
		{
			return false;
		}
		if (_translations.TryGetValue(locale, out translation))
		{
			return true;
		}
		return false;
	}

	public ModLocalizationStatus GetLocalizationStatus(Locale gameLanguage)
	{
		if (_translations != null && _translations.TryGetValue(gameLanguage, out var value))
		{
			if (value.TryGetTranslationStatus(out var translationStatus) && !translationStatus.HasImportantIssues())
			{
				return ModLocalizationStatus.Translated;
			}
			return ModLocalizationStatus.BrokenTranslation;
		}
		if (!(gameLanguage == MainModInfoFile.Locale))
		{
			return ModLocalizationStatus.NotTranslated;
		}
		return ModLocalizationStatus.NativeModLanguage;
	}

	public void LoadNewLocalizationAtRuntime(ModsSingleton.ModTranslationDataLoaded modTranslationDataLoaded)
	{
		LogStringHeader("Adding new localization");
		if (_translations == null)
		{
			_translations = new Dictionary<Locale, ModsSingleton.ModTranslationDataLoaded>();
		}
		List<string> list;
		using (MyCollectionPool<string>.GetList(out list))
		{
			ModdableTable.FillAllKeysFor(this, list);
			LoadLocalizationInner(modTranslationDataLoaded, list);
			LogStringHeader("Adding new localization done");
		}
	}

	public void LoadLocalization()
	{
		LogStringHeader("Localization loading starting!");
		if (SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.TryGetAllAvailableTranslations(this, out var foundTranslations))
		{
			List<string> list;
			using (MyCollectionPool<string>.GetList(out list))
			{
				ModdableTable.FillAllKeysFor(this, list);
				if (_translations == null)
				{
					_translations = new Dictionary<Locale, ModsSingleton.ModTranslationDataLoaded>();
				}
				foreach (ModsSingleton.ModTranslationDataLoaded item in foundTranslations)
				{
					LoadLocalizationInner(item, list);
				}
			}
		}
		else
		{
			LogString("No localizations found!");
		}
		LogString("Localization loading finished!");
	}

	private void LoadLocalizationInner(ModsSingleton.ModTranslationDataLoaded translation, List<string> allKeys)
	{
		LogStringHeader("Localization " + translation.TranslationLocale.LocaleName + " \nat \"" + translation.Path + "\"!");
		_translations.Add(translation.TranslationLocale, translation);
		translation.AfterLoad(ModdableTable.LoadTranslations(this, translation, allKeys));
	}

	public long GetApproximateFileSizeInBytes()
	{
		return _filesIncluded.Sum((ModFileBase x) => x.GetApproximateSizeInBytes());
	}

	public void Start()
	{
		if (Status == ModStatus.Ready)
		{
			LogStringHeader("Calling OnGameStart");
			using UnityLogManager unityLogManager = new UnityLogManager(UnityLog);
			try
			{
				MainModInfoFile.OnGameStart.Execute();
				if (MainModInfoFile.ParsedTargetGameVersion.IsOutdated())
				{
					Status = ModStatus.ActiveButOutdated;
					if (MainModInfoFile.ParsedTargetGameVersion.IsExactlyTheSame(ParsedVersion.Version0_85_69))
					{
						foreach (var item3 in ItemPrefab.ItemPrefabManager.AllPrefabsFromSource(ModGuid))
						{
							ItemPrefab item = item3.prefab;
							if (item.IsEquipable(out var slotEquipData) && slotEquipData.TargetSlot.Id == "shirt")
							{
								item.SetSlotAndOverride(ItemSlot.SlotManager.UpperBody, Live2DController.ShirtId);
							}
							string controllerId = ControllerEnum.Interaction.GetControllerId();
							string controllerId2 = ControllerEnum.Cowgirl.GetControllerId();
							string[] targets = new string[2] { controllerId, controllerId2 };
							PackedTexturesContainer[] textures = item.Textures;
							for (int i = 0; i < textures.Length; i++)
							{
								foreach (PackedTexturesContainer.PackedTexture packedTexture in textures[i].PackedTextures)
								{
									if (packedTexture.HasTarget(controllerId))
									{
										packedTexture.Targets = targets;
									}
								}
							}
						}
					}
					if (MainModInfoFile.ParsedTargetGameVersion.IsOlder(ParsedVersion.Version0_87_0))
					{
						foreach (var item4 in ItemPrefab.ItemPrefabManager.AllPrefabsFromSource(ModGuid))
						{
							ItemPrefab item2 = item4.prefab;
							if (!item2.IsEquipable(out var slotEquipData2))
							{
								continue;
							}
							string id = slotEquipData2.TargetSlot.Id;
							if (!(id == "UpperBody_Shirt"))
							{
								if (id == "Teeth_SharpTeeth")
								{
									item2.SetSlotAndOverride(ItemSlot.SlotManager.Teeth, Live2DController.SharpTeethId);
								}
							}
							else
							{
								item2.SetSlotAndOverride(ItemSlot.SlotManager.UpperBody, Live2DController.ShirtId);
							}
						}
					}
					if (MainModInfoFile.ParsedTargetGameVersion.IsOlder(ParsedVersion.Version0_96_1))
					{
						foreach (var item5 in ItemPrefab.ItemPrefabManager.AllPrefabsFromSource(ModGuid))
						{
							PackedTexturesContainer[] textures = item5.prefab.Textures;
							for (int i = 0; i < textures.Length; i++)
							{
								foreach (PackedTexturesContainer.PackedTexture packedTexture2 in textures[i].PackedTextures)
								{
									foreach (PackedTexturesContainer.PackedDrawable packedDrawable in packedTexture2.PackedDrawables)
									{
										if (packedDrawable.Name == "CuteBow2")
										{
											packedDrawable.Name = "CuteBow";
										}
									}
								}
							}
						}
					}
				}
				else if (MainModInfoFile.ParsedTargetGameVersion.IsNewerThanGameVersion())
				{
					Status = ModStatus.ActiveButGameOutdated;
				}
				else
				{
					Status = ModStatus.Active;
				}
				if (unityLogManager.ReceivedSomeSortOfAnError)
				{
					LogString("During activating some sort of an error occured!");
					Status = ModStatus.Exception;
				}
				return;
			}
			catch (ScriptRuntimeException ex)
			{
				Debug.LogError(ex.DecoratedMessage);
				LogString(ex.DecoratedMessage);
				LogString("Activating caused an exception!");
				Status = ModStatus.Exception;
				return;
			}
			catch (Exception ex2)
			{
				Debug.LogException(ex2);
				LogString(ex2.ToString());
				LogString("Activating caused an exception!");
				Status = ModStatus.Exception;
				return;
			}
		}
		LogString("Mod was skipped during activating, because it wasn't ready.");
	}

	public void SaveToFilePopup()
	{
		byte[] data = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.SaveModToZipData(this);
		UiOverlay.Instance.FileSavePopup(InternalFilename, data, (MyFileBrowserSingleton.DownloadResult t) =>
		{
			if (t.Success)
			{
				UiOverlay.Instance.OkPopup("Done!", "Mods are zip files containing data(as files) and a mod.json file which puts it all together. After playing around with the file you can import it with 'Load mods' button. Have fun!");
			}
		});
	}

	public void SaveToFolder(string folder)
	{
		if (!Directory.Exists(folder))
		{
			Directory.CreateDirectory(folder);
		}
		string contents = JsonUtility.ToJson(MainModInfoFile, prettyPrint: true);
		File.WriteAllText(folder + "/" + MainModInfoFile.ModInfoFileName, contents);
		HashSet<string> hashSet;
		using (MyCollectionPool<string>.GetHashSet(out hashSet))
		{
			foreach (ModFileBase item in FilesIncluded)
			{
				string directoryName = Path.GetDirectoryName(item.NormalizedPath);
				hashSet.Add(directoryName);
			}
			hashSet.Remove(null);
			foreach (string item2 in hashSet)
			{
				if (!Directory.Exists(folder + item2))
				{
					Directory.CreateDirectory(folder + item2);
				}
			}
			foreach (ModFileBase item3 in FilesIncluded)
			{
				File.WriteAllBytes(folder + item3.NormalizedPath, item3.Data);
			}
		}
	}
}
