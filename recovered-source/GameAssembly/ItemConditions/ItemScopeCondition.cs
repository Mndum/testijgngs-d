using System;

namespace ItemConditions;

public abstract class ItemScopeCondition : LuaUtilities.ILuaTransferable, ICloneable<ItemScopeCondition>, ICloneable
{
	public abstract bool IsActive(Item item);

	public abstract string ToLuaString();

	public abstract ItemScopeCondition Clone();
}
