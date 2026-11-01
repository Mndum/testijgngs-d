using System.Collections.Generic;

public interface IHasStringReferences
{
	IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences();

	bool CreateEmptyKeys();

	bool UpdateKeyNames();

	void ClearWithoutDeletingEntries();

	void ClearAndDeleteEntries();
}
