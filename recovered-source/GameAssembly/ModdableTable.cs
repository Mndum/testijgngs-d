using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ModdableTable
{
	public class TranslationStatus
	{
		public readonly int AllCount;

		public readonly int MissingKeys;

		public readonly int ExtraKeys;

		public readonly int BrokenKeys;

		public TranslationStatus(int allCount, int missingKeys, int extraKeys, int brokenKeys)
		{
			AllCount = allCount;
			MissingKeys = missingKeys;
			ExtraKeys = extraKeys;
			BrokenKeys = brokenKeys;
		}

		public bool HasImportantIssues()
		{
			if (MissingKeys == 0)
			{
				return BrokenKeys != 0;
			}
			return true;
		}

		public bool HasAnyIssues()
		{
			if (MissingKeys == 0 && ExtraKeys == 0)
			{
				return BrokenKeys != 0;
			}
			return true;
		}

		public float GetPercentGood()
		{
			if (AllCount <= 0)
			{
				return 1f;
			}
			return (float)(AllCount - MissingKeys) / (float)AllCount;
		}
	}

	private static async UniTask PrepareLocale(Locale locale)
	{
		AsyncOperationHandle<StringTable> tableAsync = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Moddable.TABLE_REFERENCE, locale);
		StringTable stringTable = await tableAsync;
		Addressables.ResourceManager.Acquire(tableAsync);
		stringTable.Clear();
		stringTable.SharedData.Clear();
	}

	[LoadableStaticMethod(null)]
	public static UniTask PrepareModdableTable(IProgressManager progressManager)
	{
		progressManager.ReportProgress(0f, "Preparing moddable table");
		return UniTask.WhenAll(LocalizationSettings.AvailableLocales.Locales.Select(PrepareLocale));
	}

	public static ModdableLocalizedConversation CreateModdableConversation(string text, Guid modId, int id)
	{
		ModdableLocalizedConversation moddableLocalizedConversation = new ModdableLocalizedConversation
		{
			mainText = text
		};
		LocalizedConversation.LocalizedLinesSummaryContainer summary = moddableLocalizedConversation.GetSummary(forceUpdate: true);
		summary.FillMissing();
		moddableLocalizedConversation.CreateEmptyKeys((int lineIndex) =>
		{
			Guid guid = modId;
			return LOS.GetContainerConversationLineKey(guid.ToString() + "_Conv", id.ToString(), lineIndex);
		});
		summary.Recalculate(forceUpdate: true);
		Locale modLocaleOrProject = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetModLocaleOrProject(modId);
		summary.PushAllChanges(removeLocalization: false, modLocaleOrProject);
		return moddableLocalizedConversation;
	}

	public static void CreateCustomString(Mod mod, string key, string value, bool smartString)
	{
		Locale locale = mod.MainModInfoFile.Locale;
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Moddable.TABLE_REFERENCE, locale).Result;
		string text = mod.ModGuid.ToString() + "_";
		StringTableEntry stringTableEntry = result.AddEntry(text + key, value);
		if (smartString)
		{
			stringTableEntry.AddMetadata(new SmartFormatTag());
		}
	}

	public static string GetCustomString(Mod mod, string key)
	{
		return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(LOC.Moddable.TABLE_REFERENCE, mod.ModGuid.ToString() + "_" + key, null, FallbackBehavior.UseProjectSettings).Result;
	}

	public static string GetCustomString(Mod mod, string key, params object[] parameters)
	{
		return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(LOC.Moddable.TABLE_REFERENCE, mod.ModGuid.ToString() + "_" + key, parameters).Result;
	}

	public static void FillAllKeysFor(Mod mod, List<ModTranslationRow> rows)
	{
		Locale locale = mod.MainModInfoFile.Locale;
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Moddable.TABLE_REFERENCE, locale).Result;
		string text = mod.ModGuid.ToString() + "_";
		foreach (KeyValuePair<long, StringTableEntry> item in result)
		{
			StringTableEntry value = item.Value;
			if (value.Key.StartsWith(text))
			{
				string key = value.Key.Substring(text.Length);
				rows.Add(new ModTranslationRow(key, value.Value));
			}
		}
	}

	public static void FillAllKeysFor(Mod mod, List<string> allModKeys)
	{
		Locale locale = mod.MainModInfoFile.Locale;
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Moddable.TABLE_REFERENCE, locale).Result;
		string text = mod.ModGuid.ToString() + "_";
		foreach (KeyValuePair<long, StringTableEntry> item2 in result)
		{
			StringTableEntry value = item2.Value;
			if (value.Key.StartsWith(text))
			{
				string item = value.Key.Substring(text.Length);
				allModKeys.Add(item);
			}
		}
	}

	public static void ClearTranslation(Mod mod, Locale locale)
	{
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Moddable.TABLE_REFERENCE, locale).Result;
		List<long> list;
		using (MyCollectionPool<long>.GetList(out list))
		{
			string value = mod.ModGuid.ToString() + "_";
			foreach (KeyValuePair<long, StringTableEntry> item in result)
			{
				StringTableEntry value2 = item.Value;
				if (value2.Key.StartsWith(value))
				{
					list.Add(value2.KeyId);
				}
			}
			foreach (long item2 in list)
			{
				result.RemoveEntry(item2);
			}
		}
	}

	public static TranslationStatus LoadTranslations(Mod mod, ModsSingleton.ModTranslationDataLoaded translationData, List<string> allModKeys)
	{
		Locale locale = mod.MainModInfoFile.Locale;
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Moddable.TABLE_REFERENCE, locale).Result;
		StringTable result2 = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Moddable.TABLE_REFERENCE, translationData.TranslationLocale).Result;
		string text = mod.ModGuid.ToString() + "_";
		HashSet<string> hashSet;
		using (MyCollectionPool<string>.GetHashSet(out hashSet))
		{
			int num = 0;
			int num2 = 0;
			hashSet.AddRange(allModKeys);
			foreach (ModTranslationRow translationRow in translationData.Data.TranslationRows)
			{
				string key = translationRow.Key;
				string key2 = text + key;
				StringTableEntry entry = result.GetEntry(key2);
				if (entry == null)
				{
					num++;
					mod.LogString("\"" + key + "\" not found in " + locale.LocaleName + "!");
				}
				else if (!translationRow.HasBeenTranslated())
				{
					num2++;
					mod.LogString("\"" + key + "\" not translated yet!");
				}
				else if (entry.Value != translationRow.DefaultString)
				{
					num2++;
					mod.LogString("\"" + key + "\" doesn't match the " + locale.LocaleName + " value.\nExpected: \"" + entry.Value + "\"\nGot: \"" + translationRow.DefaultString + "\"!");
				}
				else
				{
					StringTableEntry stringTableEntry = result2.GetEntry(key2);
					if (stringTableEntry == null)
					{
						stringTableEntry = result2.AddEntry(key2, translationRow.TranslatedString);
					}
					else
					{
						stringTableEntry.Value = translationRow.TranslatedString;
					}
					if (entry.IsSmart)
					{
						stringTableEntry.IsSmart = true;
					}
					hashSet.Remove(key);
				}
			}
			foreach (string item in hashSet)
			{
				mod.LogString("\"" + item + "\" key missing!");
			}
			return new TranslationStatus(allModKeys.Count, hashSet.Count, num, num2);
		}
	}
}
