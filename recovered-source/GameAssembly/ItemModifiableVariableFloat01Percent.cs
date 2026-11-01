using System.Text;
using UnityEngine;

public class ItemModifiableVariableFloat01Percent : ItemModifiableVariable
{
	public enum FloatVariableTypeEnum
	{
		Percentage01
	}

	private float _defaultValue;

	public float DefaultValue
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

	public ItemModifiableVariableFloat01Percent(string varId, SimpleLocalizedString nameSls, float defaultValue = 0f)
		: base(varId, nameSls)
	{
		_defaultValue = defaultValue;
	}

	public override string ToLuaString()
	{
		return "ItemModifiableVariableFactory.CreateInstanceFloatVariable(" + VarId.ToLuaString() + ", " + NameSls.ToLuaString() + ", " + DefaultValue.ToLuaString() + ")";
	}

	public float GetValue(Item item)
	{
		return Mathf.Clamp01(item.GetFloatSpecialVariableOrDefault(VarId, DefaultValue));
	}

	public void SetValue(Item item, float value)
	{
		item.SetFloatSpecialVariable(VarId, Mathf.Clamp01(value));
	}

	public override void GetLocalizedDescription(Item item, StringBuilder stringBuilder)
	{
		float value = GetValue(item);
		stringBuilder.AppendPercentDisplay(GetLocalizedName(), value);
	}

	public override ItemModifiableVariable Clone()
	{
		return (ItemModifiableVariableFloat01Percent)MemberwiseClone();
	}
}
