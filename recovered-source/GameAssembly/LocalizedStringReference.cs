using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

[Serializable]
public abstract class LocalizedStringReference
{
	private sealed class KeyIdEqualityComparer : IEqualityComparer<LocalizedStringReference>
	{
		public bool Equals(LocalizedStringReference x, LocalizedStringReference y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null)
			{
				return false;
			}
			if (y == null)
			{
				return false;
			}
			if (x.GetType() != y.GetType())
			{
				return false;
			}
			return x.KeyId == y.KeyId;
		}

		public int GetHashCode(LocalizedStringReference obj)
		{
			return obj.KeyId.GetHashCode();
		}
	}

	[SerializeField]
	public long KeyId;

	private static readonly char[] SmartChars = "{}".ToCharArray();

	private static Regex _uncommentRegex = new Regex("{{.*?}}");

	public abstract TableReference TargetTable { get; }

	public bool Created => KeyId != 0;

	public bool Exists => GetSharedEntry() != null;

	public bool Valid
	{
		get
		{
			if (Created)
			{
				return Exists;
			}
			return false;
		}
	}

	public bool ValidAndHasData
	{
		get
		{
			if (!Created)
			{
				return false;
			}
			StringTableEntry entryProjectLocale = GetEntryProjectLocale();
			if (entryProjectLocale == null)
			{
				return false;
			}
			return !string.IsNullOrEmpty(entryProjectLocale.Value);
		}
	}

	public static IEqualityComparer<LocalizedStringReference> KeyIdAndSourceComparer { get; } = new KeyIdEqualityComparer();

	protected LocalizedStringReference()
	{
	}

	protected LocalizedStringReference(long keyId)
	{
		KeyId = keyId;
	}

	public void ClearWithoutDeletingEntry()
	{
		KeyId = 0L;
	}

	public void SetSmart(bool smart)
	{
		foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
		{
			LocalizedDatabase<StringTable, StringTableEntry>.TableEntryResult tableEntry = LocalizationSettings.StringDatabase.GetTableEntry(TargetTable, KeyId, locale, FallbackBehavior.DontUseFallback);
			tableEntry.Entry.IsSmart = smart;
			LOS.SetTableDirty(tableEntry.Entry);
		}
	}

	private StringTableEntry CreateEntry(string key)
	{
		Locale mainLocale = LocalizationSettings.ProjectLocale;
		StringTableEntry stringTableEntry = LOS.GetTableSafe(TargetTable, mainLocale).AddEntry(key, "No text!");
		KeyId = stringTableEntry.KeyId;
		foreach (Locale item in LocalizationSettings.AvailableLocales.Locales.Where((Locale t) => t != mainLocale))
		{
			LOS.GetTableSafe(TargetTable, item).AddEntry(KeyId, "");
		}
		LOS.SetTableDirty(stringTableEntry);
		return stringTableEntry;
	}

	private string FindFreeKeyName(string key, StringTableEntry entry)
	{
		while (true)
		{
			StringTableEntry tableEntrySafe = LOS.GetTableEntrySafe(TargetTable, key);
			if (tableEntrySafe == null || tableEntrySafe == entry)
			{
				break;
			}
			key += "+TMP";
		}
		return key;
	}

	public bool CreateIfNotExists(string key)
	{
		StringTableEntry entry;
		return CreateIfNotExists(key, out entry);
	}

	public bool CreateIfNotExists(string key, out StringTableEntry entry)
	{
		if (Created)
		{
			StringTableEntry entry2 = GetEntry();
			if (entry2 != null && entry2.SharedEntry != null)
			{
				entry = entry2;
				return false;
			}
			Debug.LogError($"Key with id {KeyId} not found! Creating new!");
		}
		key = FindFreeKeyName(key, null);
		entry = CreateEntry(key);
		return true;
	}

	public void SetStringProjectLocale(string value)
	{
		SetString(value, LocalizationSettings.ProjectLocale);
	}

	public void SetString(string value, Locale targetLocale = null)
	{
		StringTableEntry entryInLocale = GetEntryInLocale(targetLocale);
		if (entryInLocale != null)
		{
			entryInLocale.Value = value;
			entryInLocale.IsSmart = entryInLocale.Value.IndexOfAny(SmartChars) != -1;
			LOS.SetTableDirty(entryInLocale);
		}
	}

	public void CopyFrom(LocalizedStringReference value, bool copyMachineTranslated = false)
	{
		LocalizationSettings.Instance.GetStringDatabase();
		foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
		{
			StringTableEntry stringTableEntry = GetEntryInLocale(locale);
			StringTableEntry entryInLocale = value.GetEntryInLocale(locale);
			if (entryInLocale != null && !string.IsNullOrWhiteSpace(entryInLocale.Value))
			{
				if (stringTableEntry == null)
				{
					stringTableEntry = LOS.GetTableSafe(TargetTable, locale).AddEntry(KeyId, entryInLocale.Value);
				}
				else
				{
					stringTableEntry.Value = entryInLocale.Value;
				}
				stringTableEntry.IsSmart = entryInLocale.IsSmart;
			}
			LOS.SetTableDirty(stringTableEntry);
		}
	}

	public bool UpdateKeyName(string key)
	{
		if (!Valid)
		{
			return false;
		}
		StringTableEntry entry = GetEntry();
		if (entry != null && entry.Key != key)
		{
			entry.Key = FindFreeKeyName(key, entry);
			LOS.SetTableDirty(entry);
			return true;
		}
		return false;
	}

	public StringTableEntry GetEntry()
	{
		return LOS.GetTableEntrySafe(TargetTable, KeyId);
	}

	public StringTableEntry GetEntryProjectLocale()
	{
		return GetEntryInLocale(LocalizationSettings.ProjectLocale);
	}

	public StringTable GetTable(Locale locale = null)
	{
		return LOS.GetTableSafe(TargetTable, locale);
	}

	public StringTableEntry GetEntryInLocale(Locale locale = null)
	{
		if (!Created)
		{
			return null;
		}
		StringTableEntry tableEntrySafe = LOS.GetTableEntrySafe(TargetTable, KeyId, locale, FallbackBehavior.DontUseFallback);
		if (tableEntrySafe != null)
		{
			return tableEntrySafe;
		}
		return LOS.GetTableSafe(TargetTable, locale).AddEntry(KeyId, "");
	}

	public SharedTableData.SharedTableEntry GetSharedEntry()
	{
		return LOS.GetTableSafe(TargetTable, LocalizationSettings.ProjectLocale).SharedData.GetEntry(KeyId);
	}

	public string GetString()
	{
		try
		{
			string result = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TargetTable, KeyId, null, FallbackBehavior.UseProjectSettings).Result;
			if (string.IsNullOrWhiteSpace(result))
			{
				return "";
			}
			return _uncommentRegex.Replace(result, (Match match) => match.Value.Substring(1, match.Value.Length - 2));
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
			return $"Error in Story.{KeyId}";
		}
	}

	public void Delete()
	{
		if (Valid)
		{
			StringTable tableSafe = LOS.GetTableSafe(TargetTable, LocalizationSettings.ProjectLocale);
			tableSafe.SharedData.RemoveKey(KeyId);
			LOS.SetTableDirty(tableSafe);
			ClearWithoutDeletingEntry();
		}
	}

	public TableEntryReference GetTableEntryReference()
	{
		StringTableEntry entry = GetEntry();
		return entry.Key;
	}

	public LOS.TupleReference GetReference()
	{
		return new LOS.TupleReference(TargetTable.TableCollectionName, GetTableEntryReference());
	}

	public static LocalizedStringReference FromTupleReference(LOS.TupleReference tupleReference)
	{
		TableReference tableReference = tupleReference.GetTableReference();
		LocalizedStringReference localizedStringReference;
		if (tableReference.Equals(LOC.Story.TABLE_REFERENCE))
		{
			localizedStringReference = new LocalizedStringReferenceStory();
		}
		else if (tableReference.Equals(LOC.Moddable.TABLE_REFERENCE))
		{
			localizedStringReference = new LocalizedStringReferenceModdable();
		}
		else
		{
			localizedStringReference = ((!tableReference.Equals(LOC.Contracts.TABLE_REFERENCE)) ? ((LocalizedStringReference)new LocalizedStringReferenceAny(tableReference)) : ((LocalizedStringReference)new LocalizedStringReferenceCharacters()));
		}
		TableEntryReference tableEntryReference = tupleReference.GetTableEntryReference();
		if (tableEntryReference.ReferenceType == TableEntryReference.Type.Id)
		{
			localizedStringReference.KeyId = tableEntryReference.KeyId;
		}
		else if (tableEntryReference.ReferenceType == TableEntryReference.Type.Name)
		{
			StringTable tableSafe = LOS.GetTableSafe(tableReference);
			localizedStringReference.KeyId = tableSafe.SharedData.GetId(tableEntryReference.Key);
		}
		return localizedStringReference;
	}
}
