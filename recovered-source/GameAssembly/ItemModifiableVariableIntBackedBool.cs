using System.Text;

public class ItemModifiableVariableIntBackedBool : ItemModifiableVariable
{
	private bool _defaultValue;

	public bool DefaultValue
	{
		get
		{
			return _defaultValue;
		}
		set
		{
			_defaultValue = value;
		}
	}

	public ItemModifiableVariableIntBackedBool(string varId, SimpleLocalizedString nameSls, bool defaultValue = false)
		: base(varId, nameSls)
	{
		DefaultValue = defaultValue;
	}

	public override string ToLuaString()
	{
		return "ItemModifiableVariableFactory.CreateInstanceBoolBackedVariable(" + VarId.ToLuaString() + ", " + NameSls.ToLuaString() + ", " + DefaultValue.ToLuaString() + ")";
	}

	public bool GetValue(Item item)
	{
		return item.GetIntSpecialVariableOrDefaultCastBool(VarId, DefaultValue);
	}

	public void SetValueRaw(Item item, bool value)
	{
		item.SetIntSpecialVariableCastBool(VarId, value);
	}

	public override void GetLocalizedDescription(Item item, StringBuilder stringBuilder)
	{
		bool value = GetValue(item);
		stringBuilder.AppendStringDisplay(GetLocalizedName(), value ? LOC.UI.SpecialItemVariableEnabled : LOC.UI.SpecialItemVariableDisabled);
	}

	public override ItemModifiableVariable Clone()
	{
		return (ItemModifiableVariableIntBackedBool)MemberwiseClone();
	}
}
