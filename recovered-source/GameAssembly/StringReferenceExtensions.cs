using System.Collections.Generic;

public static class StringReferenceExtensions
{
	public static void ClearWithoutDeletingEntriesMultiple(this IEnumerable<LocalizedStringReference> references)
	{
		foreach (LocalizedStringReference reference in references)
		{
			reference.ClearWithoutDeletingEntry();
		}
	}

	public static void ClearAndDeleteEntriesMultiple(this IEnumerable<LocalizedStringReference> references)
	{
		foreach (LocalizedStringReference reference in references)
		{
			reference.Delete();
		}
	}

	public static bool CreateEmptyEntriesMultiple(this IEnumerable<(string name, LocalizedStringReference reference)> references)
	{
		bool flag = false;
		foreach (var reference in references)
		{
			flag |= reference.reference.CreateIfNotExists(reference.name);
		}
		return flag;
	}

	public static bool UpdateKeyNamesMultiple(this IEnumerable<(string name, LocalizedStringReference reference)> references)
	{
		bool flag = false;
		foreach (var reference in references)
		{
			flag |= reference.reference.UpdateKeyName(reference.name);
		}
		return flag;
	}
}
