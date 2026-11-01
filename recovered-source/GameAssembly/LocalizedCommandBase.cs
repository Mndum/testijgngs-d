using System.Collections.Generic;
using Fungus;

public abstract class LocalizedCommandBase : Command, IHasStringReferences
{
	public abstract IEnumerable<LocalizedStringReference> IterateAllLocalizedStringReferences();

	public abstract bool CreateEmptyKeys();

	public abstract bool UpdateKeyNames();

	public abstract void ClearWithoutDeletingEntries();

	public abstract void ClearAndDeleteEntries();

	private void SetDirty()
	{
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		if (CreateEmptyKeys())
		{
			SetDirty();
		}
	}

	public override void CommandIndexChanged()
	{
		if (UpdateKeyNames())
		{
			SetDirty();
		}
	}

	public override void OnCommandPastedAfterCopy(Command source)
	{
		ClearWithoutDeletingEntries();
		CreateEmptyKeys();
	}

	public override void OnCommandRemoved(Block parentBlock)
	{
		ClearAndDeleteEntries();
	}
}
