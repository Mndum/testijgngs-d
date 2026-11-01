using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class SaveSingleton : SingletonLoadableMonoBehaviour<SaveSingleton>
{
	public enum SaveContainerLoadResult
	{
		Success,
		FailedToParse,
		FailedToValidate_LoadedSaveFromSaveFile
	}

	[SerializeField]
	private SaveContainer saveContainer;

	private static readonly string _playerPrefName = "0.70saves";

	private static readonly string SaveFileName = "save.mdrg";

	private const int TimeBetweenAutoSaveAllDataSeconds = 15;

	private double _lastSaveTime;

	public const int SavesPerFolder = 7;

	public const int FolderCount = 19;

	public const int MaxSaveSlotNumber = 133;

	public const int AutoSaveCount = 30;

	private DateTime _lastAutoSaveTime = DateTime.Now;

	public static readonly TimeSpan MinimumTimeBetweenAutoSaves = TimeSpan.FromMinutes(3.0);

	public SaveContainer Save => saveContainer;

	public bool HasAnySaves
	{
		get
		{
			if (saveContainer != null)
			{
				return saveContainer.HasAnySaves;
			}
			return false;
		}
	}

	public override Type[] GetDependenciesArray()
	{
		return new Type[8]
		{
			typeof(ModsSingleton),
			typeof(CookingSongPrefabManager),
			typeof(FishingLocationDataManager),
			typeof(ShopPrefabManager),
			typeof(StockCompanyPrefabManager),
			typeof(EmailPrefabManager),
			typeof(ItemPrefab.ItemPrefabManager),
			typeof(CocktractPartnerPrefabManager)
		};
	}

	protected override async UniTask LoadInner(IProgressManager progressManager)
	{
		try
		{
			if (SaveFileHelper.TryLoadFileToJson<SaveContainer>(SaveFileName, out var data))
			{
				saveContainer = data;
				return;
			}
			string text = UnityEngine.PlayerPrefs.GetString(_playerPrefName, null);
			if (text != null && SaveFileHelper.TryParseStringToJson<SaveContainer>(text, out var output))
			{
				saveContainer = output;
				return;
			}
			Debug.Log("Load unsuccesful. Had to create new save container.");
			saveContainer = new SaveContainer();
		}
		finally
		{
			EnsureWarningFileExists();
			AutoSaveLoop().Forget();
		}
	}

	private void EnsureWarningFileExists()
	{
		try
		{
			if (Application.platform != RuntimePlatform.WebGLPlayer)
			{
				string text = "DO NOT BACKUP SAVES STORED HERE! GO TO MAIN MENU→LOAD→TO FILE!!!";
				SaveFileHelper.EnsureDummyFile("! " + text);
				SaveFileHelper.EnsureDummyFile("Ω " + text);
			}
		}
		catch (Exception exception)
		{
			Debug.LogError("Failed to create dummy files!");
			Debug.LogException(exception);
		}
	}

	public SaveContainerLoadResult TryLoadSaveContainerFromString(string saveString, out SaveContainer save)
	{
		if (!SaveFileHelper.TryParseStringToJson<SaveContainer>(saveString, out save))
		{
			return SaveContainerLoadResult.FailedToParse;
		}
		if (!save.ValidateAfterLoadingFromString())
		{
			return SaveContainerLoadResult.FailedToValidate_LoadedSaveFromSaveFile;
		}
		return SaveContainerLoadResult.Success;
	}

	public string GetSaveContainerAsString()
	{
		try
		{
			saveContainer.BeginSaveToJson();
			return JsonUtility.ToJson(saveContainer, prettyPrint: true);
		}
		finally
		{
			saveContainer.EndSaveToJson();
		}
	}

	private async UniTaskVoid AutoSaveLoop()
	{
		while (true)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(15.0), ignoreTimeScale: true);
			try
			{
				double num = Time.realtimeSinceStartupAsDouble - _lastSaveTime;
				if (saveContainer != null && saveContainer.IsDirty() && num > 15.0)
				{
					SaveAllChanges();
				}
			}
			catch (Exception exception)
			{
				Debug.LogError("Exception in the AutoSave loop!");
				Debug.LogException(exception);
			}
		}
	}

	public void SaveAllChanges()
	{
		if (IsLoaded)
		{
			saveContainer.PushSlotChangesToFiles();
			SaveFileHelper.SaveJson(SaveFileName, saveContainer);
			saveContainer.ClearDirty();
			_lastSaveTime = Time.realtimeSinceStartupAsDouble;
		}
	}

	public void ResetAllData()
	{
		saveContainer = new SaveContainer();
		SaveAllChanges();
	}

	public void ReplaceSaveContainer(SaveContainer save)
	{
		saveContainer = save;
		SaveAllChanges();
	}

	public void TryMakeAutoSave()
	{
		OptionsStatic.TimeCheckPoint();
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables != null)
		{
			TimeSpan timeSpan = DateTime.Now - _lastAutoSaveTime;
			if (timeSpan <= MinimumTimeBetweenAutoSaves)
			{
				SaveAllChanges();
				return;
			}
			_lastAutoSaveTime = DateTime.Now;
			int num = saveContainer.DoAutosave();
			Debug.Log($"Saved autosave to slot {num} previus autosave happened {timeSpan.Hours:D2} hrs, {timeSpan.Minutes:D2} mins, {timeSpan.Seconds:D2} secs ago");
			SaveAllChanges();
		}
	}
}
