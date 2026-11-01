using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Fungus;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public static class LOS
{
	public enum EndingType
	{
		OldConversation,
		Conversation,
		Menu,
		TextInput
	}

	[Serializable]
	public class TupleReference
	{
		[SerializeField]
		private string TableReference;

		[SerializeField]
		private long TableEntryReferenceLong;

		[SerializeField]
		private string TableEntryReference;

		private bool TableReferenceEmpty => string.IsNullOrEmpty(TableReference);

		private bool TableEntryReferenceEmpty
		{
			get
			{
				if (string.IsNullOrEmpty(TableEntryReference))
				{
					return TableEntryReferenceLong == 0;
				}
				return false;
			}
		}

		public bool IsEmpty
		{
			get
			{
				if (!TableReferenceEmpty)
				{
					return TableEntryReferenceEmpty;
				}
				return true;
			}
		}

		public TupleReference(string tableReference, string tableEntryReference)
		{
			TableReference = tableReference;
			TableEntryReference = tableEntryReference;
		}

		public TupleReference(string tableReference, long tableEntryReferenceLong)
		{
			TableReference = tableReference;
			TableEntryReferenceLong = tableEntryReferenceLong;
		}

		public TupleReference(string tableReference, TableEntryReference tableEntryReference)
		{
			TableReference = tableReference;
			TableEntryReferenceLong = tableEntryReference.KeyId;
			TableEntryReference = tableEntryReference.Key;
		}

		public bool TryGetStringTableEntryReference(out string stringTableEntryReference)
		{
			stringTableEntryReference = null;
			if (TableEntryReferenceLong == 0L)
			{
				return false;
			}
			StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(TableReference).Result;
			if (result == null)
			{
				return false;
			}
			StringTableEntry entryWithFallback = result.GetEntryWithFallback(TableEntryReferenceLong);
			if (entryWithFallback == null)
			{
				return false;
			}
			stringTableEntryReference = entryWithFallback.Key;
			return true;
		}

		public void MoveToStringTableEntryRepresentation()
		{
			if (TryGetStringTableEntryReference(out var stringTableEntryReference))
			{
				TableEntryReferenceLong = 0L;
				TableEntryReference = stringTableEntryReference;
			}
		}

		public string GetStringTableReference()
		{
			return TableReference;
		}

		public TableReference GetTableReference()
		{
			return TableReference;
		}

		public TableEntryReference GetTableEntryReference()
		{
			if (TableEntryReferenceLong != 0L)
			{
				return TableEntryReferenceLong;
			}
			return TableEntryReference;
		}

		public string Get(params object[] arugments)
		{
			return GetLocalizedString(this, arugments);
		}

		public string Get(IList<object> arugments)
		{
			return GetLocalizedString(this, arugments);
		}

		public string Get()
		{
			return GetLocalizedString(this);
		}

		public string GetWithCustomFallback(string fallback)
		{
			string localizedString = GetLocalizedString(this);
			if (string.IsNullOrEmpty(localizedString))
			{
				return fallback;
			}
			return localizedString;
		}

		private string DescribeTableEntry()
		{
			if (TableEntryReferenceLong == 0L)
			{
				if (string.IsNullOrEmpty(TableEntryReference))
				{
					return "NO ENTRY!";
				}
				return "Key: " + TableEntryReference + " Value: \"" + Get() + "\"";
			}
			return $"Key Id: {TableEntryReferenceLong} Value: \"{Get()}\"";
		}

		private string DescribeTableEntryWithoutValue()
		{
			if (TableEntryReferenceLong == 0L)
			{
				if (string.IsNullOrEmpty(TableEntryReference))
				{
					return "NO ENTRY!";
				}
				return "Key: " + TableEntryReference;
			}
			return $"Key Id: {TableEntryReferenceLong}";
		}

		public override string ToString()
		{
			return "TupleReference: Table: " + TableReference + " " + DescribeTableEntry();
		}

		public string ToStringWithoutValue()
		{
			return "TupleReference: Table: " + TableReference + " " + DescribeTableEntryWithoutValue();
		}

		public TupleReference Clone()
		{
			return (TupleReference)MemberwiseClone();
		}
	}

	public const string OldConversationEnding = "C";

	public const string ConversationEnding = "NC";

	public const string MenuEnding = "M";

	public const string TextInputEnding = "TI";

	public static bool TryParseEnding(string ending, out EndingType endingType, out int number)
	{
		endingType = EndingType.OldConversation;
		number = 0;
		if (string.IsNullOrEmpty(ending))
		{
			return false;
		}
		ending = ending.Trim();
		if (ending == "C")
		{
			endingType = EndingType.OldConversation;
			number = 0;
			return true;
		}
		if (ending == "M")
		{
			endingType = EndingType.Menu;
			number = 0;
			return true;
		}
		if (ending.Length < 4 || ending[2] != '+')
		{
			return false;
		}
		string text = ending.Substring(0, 2);
		if (!int.TryParse(ending.Substring(3), out var result) || result < 0)
		{
			return false;
		}
		if (!(text == "NC"))
		{
			if (text == "TI")
			{
				endingType = EndingType.TextInput;
				number = result;
				return true;
			}
			return false;
		}
		endingType = EndingType.Conversation;
		number = result;
		return true;
	}

	public static string GetKey(string BlockName, int index, string ending)
	{
		return $"{BlockName}_{index}_{ending}";
	}

	public static string GetOldConversationKey(string BlockName, int index)
	{
		return GetKey(BlockName, index, "C");
	}

	public static string GetConversationLineKey(string BlockName, int index, int lineIndex)
	{
		return GetKey(BlockName, index, "NC+" + lineIndex);
	}

	public static string GetContainerKey(string containerName, string fieldName)
	{
		return containerName + "+" + fieldName + "+C_0_";
	}

	public static string GetContainerConversationLineKey(string containerName, string fieldName, int lineIndex)
	{
		return string.Format("{0}+{1}+C_0_{2}+{3}", containerName, fieldName, "NC", lineIndex);
	}

	public static string GetMenuKey(string BlockName, int index)
	{
		return GetKey(BlockName, index, "M");
	}

	public static string GetBetterMenuKey(string BlockName, int index, int menuIndex)
	{
		return GetKey(BlockName, index, "M+" + menuIndex);
	}

	public static string GetTextInputKey(string BlockName, int index, string menuIndex)
	{
		return GetKey(BlockName, index, "TI+" + menuIndex);
	}

	public static bool IsNullOrEmpty(this TupleReference tr)
	{
		return tr?.IsEmpty ?? true;
	}

	public static bool IsNullOrEmptyOrEmptyEntry(this TupleReference tr)
	{
		if (tr == null)
		{
			return true;
		}
		if (tr.IsEmpty)
		{
			return true;
		}
		return string.IsNullOrWhiteSpace(tr.Get());
	}

	public static TupleReference EntryReference(this string table, TableEntryReference entryReference)
	{
		return new TupleReference(table, entryReference);
	}

	public static TupleReference EntryReference(this TableReference table, TableEntryReference entryReference)
	{
		return new TupleReference(table, entryReference);
	}

	public static string GetOther(this string entry, params object[] arugments)
	{
		return GetLocalizedString(LOC.Other.TABLE_REFERENCE, entry, arugments);
	}

	public static string GetOther(this string entry, IList<object> arugments = null)
	{
		return GetLocalizedString(LOC.Other.TABLE_REFERENCE, entry, arugments);
	}

	public static string GetUI(this long entry)
	{
		return GetLocalizedString(LOC.UI.TABLE_REFERENCE, entry);
	}

	public static string GetWithProjectLocale(this TupleReference tuple, IList<object> arugments = null)
	{
		return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(tuple.GetTableReference(), tuple.GetTableEntryReference(), arugments, LocalizationSettings.ProjectLocale).Result;
	}

	public static string GetLocalizedString(TupleReference id, IList<object> arugments = null, Locale locale = null)
	{
		return GetLocalizedString(id.GetTableReference(), id.GetTableEntryReference(), arugments, locale);
	}

	public static string GetLocalizedString(TableReference tableReference, TableEntryReference tableEntryReference, IList<object> arugments = null, Locale locale = null)
	{
		try
		{
			string result = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(tableReference, tableEntryReference, arugments).Result;
			if (!string.IsNullOrWhiteSpace(result))
			{
				return result;
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		return LocalizationSettings.StringDatabase.GetLocalizedStringAsync(tableReference, tableEntryReference, arugments, LocalizationSettings.ProjectLocale).Result;
	}

	public static void ReplaceCommand<Tnew, Told>(Told old, Block block, int o, Action<Told, Tnew> transferFunc) where Tnew : Command where Told : Command
	{
		Tnew val = old.gameObject.AddComponent<Tnew>();
		block.ReplaceCommandAtIndex(o, val);
		val.ParentBlock = old.ParentBlock;
		val.CommandIndex = o;
		transferFunc(old, val);
		UnityEngine.Object.DestroyImmediate(old);
	}

	public static void LocalizeCommand<Tnew, Told>(Told old, Block block, int o) where Tnew : Command, ILocalizedFrom<Told> where Told : Command
	{
		ReplaceCommand(old, block, o, (Told a, Tnew n) =>
		{
			n.CreateKeysFrom(a);
		});
	}

	public static (string output, bool changed) OldFormatToSmartString(string text)
	{
		Regex regex = new Regex("{.*?}");
		bool smart = false;
		return (output: regex.Replace(text, (Match match) =>
		{
			if (match.Value[1] == '$')
			{
				smart = true;
				return "{" + FlowchartSource.Prefix + match.Value.Substring(2, match.Value.Length - 3) + "}";
			}
			return "{" + match.Value + "}";
		}), changed: smart);
	}

	public static StringTableEntry GetEntryWithFallback(this StringTable table, TableEntryReference reference)
	{
		StringTableEntry entryFromReference = table.GetEntryFromReference(reference);
		if (entryFromReference != null)
		{
			return entryFromReference;
		}
		return LocalizationSettings.StringDatabase.GetTableEntryAsync(table.TableCollectionName, reference).Result.Entry;
	}

	public static T GetOrAddMetadata<T>(this MetadataCollection collection) where T : IMetadata, new()
	{
		T val = collection.GetMetadata<T>();
		if (val == null)
		{
			val = new T();
			collection.AddMetadata(val);
		}
		return val;
	}

	public static bool IsMachineTranslated(this UnityEngine.Localization.Metadata.Comment comment)
	{
		return comment.CommentText.StartsWith("Machine translated");
	}

	public static SharedTableData.SharedTableEntry CreateEntryIfNotExists(this SharedTableData table, string key)
	{
		if (!table.Contains(key))
		{
			return table.AddKey(key);
		}
		return table.GetEntry(key);
	}

	public static List<object> PreparseSerializedStringArgumentsForLocalization(List<string> specialValues)
	{
		if (specialValues == null)
		{
			return null;
		}
		List<object> list = MyCollectionPool<object>.GetList();
		foreach (string specialValue in specialValues)
		{
			if (int.TryParse(specialValue, out var result))
			{
				list.Add(result);
			}
			else
			{
				list.Add(specialValue);
			}
		}
		return list;
	}

	public static void SetTableDirty(LocalizationTable table)
	{
	}

	public static void SetTableDirty(StringTableEntry entry)
	{
	}

	public static void ClearAllNonProjectLocaleEntries(long keyId, TableReference tableReference)
	{
		Locale projectLocale = LocalizationSettings.ProjectLocale;
		ClearAllNonTargetLocaleEntries(keyId, tableReference, projectLocale);
	}

	public static void ClearAllNonTargetLocaleEntries(long keyId, TableReference tableReference, Locale targetLocale)
	{
		foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
		{
			if (locale != targetLocale)
			{
				StringTable tableSafe = GetTableSafe(tableReference, locale);
				tableSafe.RemoveEntry(keyId);
				SetTableDirty(tableSafe);
			}
		}
	}

	public static StringTableEntry GetTableEntrySafe(TableReference tableReference, TableEntryReference tableEntryReference, Locale locale = null, FallbackBehavior fallbackBehavior = FallbackBehavior.UseProjectSettings)
	{
		if (Application.isEditor)
		{
			return LocalizationSettings.StringDatabase.GetTableEntry(tableReference, tableEntryReference, locale, fallbackBehavior).Entry;
		}
		return LocalizationSettings.StringDatabase.GetTableEntryAsync(tableReference, tableEntryReference, locale, fallbackBehavior).Result.Entry;
	}

	public static StringTable GetTableSafe(TableReference tableReference, Locale locale = null)
	{
		if (Application.isEditor)
		{
			return LocalizationSettings.StringDatabase.GetTable(tableReference, locale);
		}
		return LocalizationSettings.StringDatabase.GetTableAsync(tableReference, locale).Result;
	}
}
