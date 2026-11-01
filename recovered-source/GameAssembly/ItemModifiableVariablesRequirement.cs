using System;

public abstract class ItemModifiableVariablesRequirement : LuaUtilities.ILuaTransferable, ICloneable<ItemModifiableVariablesRequirement>, ICloneable
{
	public abstract void Enforce(Item item, ItemPrefab itemPrefab, int lastModified);

	public abstract bool IsBoolChangeAllowed(Item item, ItemPrefab itemPrefab, int modifiableVariableIndex, bool newValue);

	public abstract string ToLuaString();

	public abstract ItemModifiableVariablesRequirement Clone();
}
