using System;
using System.Text;
using MoonSharp.Interpreter.Interop;

public abstract class ItemModifiableVariable : LuaUtilities.ILuaTransferable, ICloneable<ItemModifiableVariable>, ICloneable
{
	public string VarId { get; }

	public SimpleLocalizedString NameSls
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	protected ItemModifiableVariable(string varId, SimpleLocalizedString nameSls)
	{
		VarId = varId;
		NameSls = nameSls;
	}

	public string GetLocalizedName()
	{
		return NameSls.GetValue();
	}

	public abstract string ToLuaString();

	public abstract void GetLocalizedDescription(Item item, StringBuilder stringBuilder);

	public abstract ItemModifiableVariable Clone();
}
