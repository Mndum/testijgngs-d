using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[Serializable]
public abstract class LocalizedStringReferenceContainer : IHasStringReferences
{
	public abstract string ContainerName { get; }

	public int GetCount()
	{
		return GetAllLocalizedConversations().Count() + GetAllStringReferences().Count();
	}

	public IEnumerable<FieldInfo> GetAllLocalizedConversationFields()
	{
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (fieldInfo.FieldType == typeof(SerializedLocalizedConversation))
			{
				yield return fieldInfo;
			}
		}
	}

	public IEnumerable<(string fieldName, SerializedLocalizedConversation localizedConversation)> GetAllLocalizedConversations()
	{
		foreach (FieldInfo allLocalizedConversationField in GetAllLocalizedConversationFields())
		{
			yield return (fieldName: allLocalizedConversationField.Name, localizedConversation: (SerializedLocalizedConversation)allLocalizedConversationField.GetValue(this));
		}
	}

	public IEnumerable<LocalizedStringReference> GetAllStringReferences()
	{
		foreach (FieldInfo allStringReferencesField in GetAllStringReferencesFields())
		{
			yield return (LocalizedStringReference)allStringReferencesField.GetValue(this);
		}
	}

	public IEnumerable<(string fieldName, LocalizedStringReference reference)> GetAllStringReferencesWithFieldNames()
	{
		foreach (FieldInfo allStringReferencesField in GetAllStringReferencesFields())
		{
			yield return (fieldName: allStringReferencesField.Name, reference: (LocalizedStringReference)allStringReferencesField.GetValue(this));
		}
	}

	public IEnumerable<FieldInfo> GetAllStringReferencesFields()
	{
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (typeof(LocalizedStringReference).IsAssignableFrom(fieldInfo.FieldType))
			{
				yield return fieldInfo;
			}
		}
	}

	public IEnumerable<(string fieldName, LocalizedStringReference reference)> GetAllStringReferencesWithKeyNames()
	{
		return from x in GetAllStringReferencesWithFieldNames()
			select (LOS.GetContainerKey(ContainerName, x.fieldName), reference: x.reference);
	}

	private Func<int, string> GetLineKeyGenerator(string fieldName)
	{
		return (int lineIndex) => LOS.GetContainerConversationLineKey(ContainerName, fieldName, lineIndex);
	}

	public void ClearWithoutDeletingEntries()
	{
		GetAllStringReferences().ClearWithoutDeletingEntriesMultiple();
	}

	public void ClearAndDeleteEntries()
	{
		GetAllStringReferences().ClearAndDeleteEntriesMultiple();
	}

	public IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences()
	{
		foreach (LocalizedStringReference allStringReference in GetAllStringReferences())
		{
			yield return allStringReference;
		}
		foreach (var allLocalizedConversation in GetAllLocalizedConversations())
		{
			SerializedLocalizedConversation item = allLocalizedConversation.localizedConversation;
			foreach (LocalizedStringReferenceStory localizedLine in item.localizedLines)
			{
				yield return localizedLine;
			}
		}
	}

	public bool CreateEmptyKeys()
	{
		bool flag = GetAllStringReferencesWithKeyNames().CreateEmptyEntriesMultiple();
		foreach (var allLocalizedConversation in GetAllLocalizedConversations())
		{
			string item = allLocalizedConversation.fieldName;
			SerializedLocalizedConversation item2 = allLocalizedConversation.localizedConversation;
			flag |= item2.CreateEmptyKeys(GetLineKeyGenerator(item));
		}
		return flag;
	}

	public bool UpdateKeyNames()
	{
		bool flag = GetAllStringReferencesWithKeyNames().UpdateKeyNamesMultiple();
		foreach (var allLocalizedConversation in GetAllLocalizedConversations())
		{
			string item = allLocalizedConversation.fieldName;
			SerializedLocalizedConversation item2 = allLocalizedConversation.localizedConversation;
			flag |= item2.UpdateKeyNames(GetLineKeyGenerator(item));
		}
		return flag;
	}
}
