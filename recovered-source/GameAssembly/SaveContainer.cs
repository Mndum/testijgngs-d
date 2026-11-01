using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

[Serializable]
public class SaveContainer : ISerializationCallbackReceiver
{
	public enum SaveType
	{
		Manual,
		Auto
	}

	[Serializable]
	public sealed class SaveSlotData
	{
		[SerializeField]
		private string notes;

		[SerializeField]
		private string description;

		[SerializeField]
		private long _time;

		[SerializeField]
		private int ingameTime;

		[SerializeField]
		private int slot;

		[SerializeField]
		private string savedata;

		[SerializeField]
		private SaveType _saveType;

		public const string SlotSaveEnding = "mdrgslot";

		private const string AutoSavePrefix = "A";

		private const string ManualSavePrefix = "M";

		[NonSerialized]
		private string _cachedFileName;

		public string Notes => notes;

		public long TimeUtc => _time;

		public DateTime TimeDateTime => DateTime.FromFileTimeUtc(_time);

		public int Slot => slot;

		public int SlotOneIndexed => slot + 1;

		public SaveType SaveType => _saveType;

		public bool HasData => !string.IsNullOrEmpty(savedata);

		private SaveSlotData(int slotZeroIndexed, DateTime time, int ingameTime, string savedata)
		{
			slot = slotZeroIndexed;
			_time = time.ToFileTimeUtc();
			this.ingameTime = ingameTime;
			this.savedata = savedata;
		}

		public static SaveSlotData FromGameVariablesNow(int slotZeroIndexed, GameVariables gameVariables)
		{
			return new SaveSlotData(slotZeroIndexed, DateTime.Now, gameVariables.time, JsonUtility.ToJson(gameVariables));
		}

		public void SetNotes(string newNotes)
		{
			notes = newNotes;
		}

		public string GetDescription()
		{
			if (!string.IsNullOrWhiteSpace(description))
			{
				return description;
			}
			return GameVariables.TimeToDateString(ingameTime) + " " + StringUtils.GreyColorTagStart + LOC.UI.Save_Description(TimeDateTime);
		}

		public void EnsureSaveType(SaveType saveType)
		{
			_saveType = saveType;
			_cachedFileName = null;
		}

		private string DetermineTargetFileName()
		{
			SaveType saveType = _saveType;
			if (saveType == SaveType.Manual || saveType != SaveType.Auto)
			{
				return "M" + SlotOneIndexed + ".mdrgslot";
			}
			return "A" + SlotOneIndexed + ".mdrgslot";
		}

		private string GetFileName()
		{
			return _cachedFileName ?? (_cachedFileName = DetermineTargetFileName());
		}

		private void PushToFile()
		{
			SaveFileHelper.SaveString(DetermineTargetFileName(), savedata);
		}

		public void BeginSaveToJson()
		{
			if (!string.IsNullOrEmpty(savedata))
			{
				PushToFile();
				return;
			}
			Result result = TryToGetSaveDataStringFromFile(out var saveDataString);
			if (result.IsSuccess)
			{
				savedata = saveDataString;
			}
			else
			{
				Debug.LogError(result.GetDescription());
			}
		}

		public void EndSaveToJson()
		{
			string fileName = GetFileName();
			if (SaveFileHelper.FileExists(fileName, savedata))
			{
				Debug.LogError(fileName + " doesn't exist, but we tried to delete the save data! Something is very wrong!!!");
			}
			else
			{
				savedata = "";
			}
		}

		public void PushToFileIfNeeded()
		{
			if (!string.IsNullOrEmpty(savedata))
			{
				PushToFile();
				savedata = "";
			}
		}

		private Result TryToGetSaveDataStringFromFile(out string saveDataString)
		{
			string fileName = GetFileName();
			if (!SaveFileHelper.TryLoadFile(fileName, out saveDataString))
			{
				return Result.FailWithDescription("Failed to load \"" + fileName + "\"!");
			}
			return Result.Success;
		}

		public Result TryGetSaveDataString(out string saveDataString)
		{
			if (!string.IsNullOrEmpty(savedata))
			{
				saveDataString = savedata;
				return Result.Success;
			}
			Result result = TryToGetSaveDataStringFromFile(out saveDataString);
			if (result.IsFail)
			{
				saveDataString = null;
				return result;
			}
			return Result.Success;
		}

		public Result TryGetAsGameVariables(out GameVariables gameVariables)
		{
			gameVariables = null;
			Result result = TryGetSaveDataString(out var saveDataString);
			if (result.IsFail)
			{
				return result;
			}
			gameVariables = JsonUtility.FromJson<GameVariables>(saveDataString);
			if (gameVariables == null)
			{
				return Result.FailWithDescription("Failed to parse the save slot! It's somehow fucked up!");
			}
			return Result.Success;
		}

		public Result TryLoad(GameScript.LoadProcedureData lpd = null)
		{
			Result result = TryGetAsGameVariables(out var gameVariables);
			if (result.IsFail)
			{
				return result;
			}
			SingletonLoadableMonoBehaviour<GameScript>.Instance.LoadGame(gameVariables, lpd).ShowResultIfFail();
			return Result.Success;
		}
	}

	private bool isDirty;

	[SerializeField]
	private List<string> visitedWebsites = new List<string>();

	[SerializeField]
	private SerializableHashSetString achievements = new SerializableHashSetString();

	[SerializeField]
	private SerializableDictionary<string, GlobalFlag> flags = new SerializableDictionary<string, GlobalFlag>();

	private HashSet<long> seenStoryTextIds = new HashSet<long>();

	[HideInInspector]
	[SerializeField]
	private string storyTextIds_Comp;

	[SerializeField]
	private List<SaveSlotData> saves = new List<SaveSlotData>();

	[SerializeField]
	private List<SaveSlotData> autoSaves = new List<SaveSlotData>();

	[SerializeField]
	private int nextAutoSaveIndex = -1;

	public IReadOnlyCollection<string> VisitedWebsites => visitedWebsites;

	public IReadOnlyCollection<string> UnlockedAchievements => achievements;

	public bool HasAnySaves
	{
		get
		{
			if (saves.Count <= 0)
			{
				return autoSaves.Count > 0;
			}
			return true;
		}
	}

	private IEnumerable<SaveSlotData> AllSaves => saves.Concat(autoSaves);

	public void MarkDirty()
	{
		isDirty = true;
	}

	public void ClearDirty()
	{
		isDirty = false;
	}

	public bool IsDirty()
	{
		return isDirty;
	}

	public void ReportWebsiteSeen(string address)
	{
		if (!visitedWebsites.Contains(address))
		{
			visitedWebsites.Add(address);
			MarkDirty();
		}
	}

	public void ValidateVisitedWebsites()
	{
		if (visitedWebsites.RemoveAll((string x) => !SingletonMonoBehaviour<InternetSingleton>.Instance.IsValidListedWebsiteExactMatch(x)) > 0)
		{
			MarkDirty();
		}
	}

	public void UnlockAchievement(string name)
	{
		if (achievements.Add(name))
		{
			MarkDirty();
		}
	}

	public void RemoveAchievement(string name)
	{
		if (achievements.Remove(name))
		{
			MarkDirty();
		}
	}

	public bool HasAchievement(string name)
	{
		return achievements.Contains(name);
	}

	public void AddGlobalFlag(string flagName)
	{
		if (flags.TryGetValue(flagName, out var value))
		{
			value.SetFlag();
		}
		else
		{
			flags.Add(flagName, new GlobalFlag());
		}
		MarkDirty();
	}

	public bool TryGetGlobalFlag(string flagName, out GlobalFlag globalFlag)
	{
		return flags.TryGetValue(flagName, out globalFlag);
	}

	public bool HasGlobalFlag(string flagName)
	{
		return flags.ContainsKey(flagName);
	}

	public double GetPercentageOfSeenStoryText()
	{
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Story.TABLE_REFERENCE).Result;
		return (double)seenStoryTextIds.Count / (double)result.Count;
	}

	public bool CheckIfUnseenStoryTextIdReport(long id)
	{
		if (seenStoryTextIds.Add(id))
		{
			MarkDirty();
			return true;
		}
		return false;
	}

	public void OnBeforeSerialize_StoryTextIds()
	{
		if (seenStoryTextIds.Count == 0)
		{
			storyTextIds_Comp = string.Empty;
			return;
		}
		using MemoryStream memoryStream = new MemoryStream();
		using (DeflateStream output = new DeflateStream(memoryStream, CompressionLevel.Fastest, leaveOpen: true))
		{
			using BinaryWriter binaryWriter = new BinaryWriter(output, Encoding.UTF8, leaveOpen: true);
			foreach (long seenStoryTextId in seenStoryTextIds)
			{
				binaryWriter.Write(seenStoryTextId);
			}
		}
		storyTextIds_Comp = Convert.ToBase64String(memoryStream.ToArray());
	}

	public void OnAfterDeserialize_StoryTextIds()
	{
		seenStoryTextIds.Clear();
		if (string.IsNullOrEmpty(storyTextIds_Comp))
		{
			return;
		}
		try
		{
			using MemoryStream stream = new MemoryStream(Convert.FromBase64String(storyTextIds_Comp));
			using DeflateStream input = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen: true);
			using BinaryReader binaryReader = new BinaryReader(input);
			while (true)
			{
				try
				{
					seenStoryTextIds.Add(binaryReader.ReadInt64());
				}
				catch (EndOfStreamException)
				{
					break;
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Failed to deserialize seenStoryTextIds.");
			Debug.LogException(exception);
			seenStoryTextIds.Clear();
		}
	}

	private bool TryInnerCreateSaveFromCurrentGameState(int slot, out SaveSlotData saveSlotData)
	{
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		if (gameVariables == null)
		{
			saveSlotData = null;
			return false;
		}
		saveSlotData = SaveSlotData.FromGameVariablesNow(slot, gameVariables);
		return true;
	}

	public bool TrySaveFromCurrentGameState(int slot, out SaveSlotData newSave)
	{
		if (!TryInnerCreateSaveFromCurrentGameState(slot, out newSave))
		{
			return false;
		}
		OverrideSlot(newSave);
		return true;
	}

	public int DoAutosave()
	{
		int num = Mathf.Clamp(nextAutoSaveIndex, 0, 29);
		if (!TryInnerCreateSaveFromCurrentGameState(num, out var saveSlotData))
		{
			return -1;
		}
		saveSlotData.SetNotes("AutoSave");
		saveSlotData.EnsureSaveType(SaveType.Auto);
		OverrideSlot(saveSlotData);
		int num2 = num + 1;
		if (num2 >= 30)
		{
			num2 = 0;
		}
		nextAutoSaveIndex = num2;
		return num;
	}

	private List<SaveSlotData> GetSaveSlotList(SaveType saveType)
	{
		if (saveType == SaveType.Manual || saveType != SaveType.Auto)
		{
			return saves;
		}
		return autoSaves;
	}

	public IReadOnlyCollection<SaveSlotData> GetSaveSlotReadonly(SaveType saveType)
	{
		return GetSaveSlotList(saveType);
	}

	[CanBeNull]
	public SaveSlotData GetLatestSlot(SaveType saveType)
	{
		List<SaveSlotData> saveSlotList = GetSaveSlotList(saveType);
		if (saveSlotList.Count > 0)
		{
			return saveSlotList.MaxBy((SaveSlotData t) => t.TimeUtc).source;
		}
		return null;
	}

	public bool TryGetSlot(int slot, SaveType saveType, out SaveSlotData saveSlotData)
	{
		foreach (SaveSlotData saveSlot in GetSaveSlotList(saveType))
		{
			if (saveSlot.Slot == slot)
			{
				saveSlotData = saveSlot;
				return true;
			}
		}
		saveSlotData = null;
		return false;
	}

	public void DeleteSlot(SaveSlotData shownSaveData)
	{
		if (GetSaveSlotList(shownSaveData.SaveType).Remove(shownSaveData))
		{
			MarkDirty();
		}
	}

	public void DeleteSlot(int slot, SaveType saveType)
	{
		if (GetSaveSlotList(saveType).RemoveAll((SaveSlotData x) => x.Slot == slot) > 0)
		{
			MarkDirty();
		}
	}

	public bool SlotExists(int slot, SaveType saveType)
	{
		SaveSlotData saveSlotData;
		return TryGetSlot(slot, saveType, out saveSlotData);
	}

	public void OverrideSlot(SaveSlotData saveSlotData)
	{
		List<SaveSlotData> saveSlotList = GetSaveSlotList(saveSlotData.SaveType);
		int slot = saveSlotData.Slot;
		saveSlotList.RemoveAll((SaveSlotData x) => x.Slot == slot);
		saveSlotList.Add(saveSlotData);
		MarkDirty();
	}

	public void PushSlotChangesToFiles()
	{
		foreach (SaveSlotData allSafe in AllSaves)
		{
			allSafe.PushToFileIfNeeded();
		}
	}

	public void BeginSaveToJson()
	{
		foreach (SaveSlotData allSafe in AllSaves)
		{
			allSafe.BeginSaveToJson();
		}
	}

	public void EndSaveToJson()
	{
		foreach (SaveSlotData allSafe in AllSaves)
		{
			allSafe.EndSaveToJson();
		}
	}

	public void OnBeforeSerialize()
	{
		OnBeforeSerialize_StoryTextIds();
	}

	public void OnAfterDeserialize()
	{
		OnAfterDeserialize_StoryTextIds();
		saves.Sort((SaveSlotData x, SaveSlotData y) => x.Slot.CompareTo(y.Slot));
		autoSaves.Sort((SaveSlotData x, SaveSlotData y) => x.Slot.CompareTo(y.Slot));
		foreach (SaveSlotData safe in saves)
		{
			safe.EnsureSaveType(SaveType.Manual);
		}
		foreach (SaveSlotData autoSafe in autoSaves)
		{
			autoSafe.EnsureSaveType(SaveType.Auto);
		}
	}

	public bool ValidateAfterLoadingFromString()
	{
		if (saves.RemoveAll((SaveSlotData x) => !x.HasData) > 0)
		{
			return saves.Any((SaveSlotData x) => x.HasData);
		}
		return true;
	}
}
