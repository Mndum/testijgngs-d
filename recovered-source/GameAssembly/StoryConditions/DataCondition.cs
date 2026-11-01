using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StoryConditions;

public sealed class DataCondition<T> : StoryCondition, IEquatable<DataCondition<T>>, IDataCondition
{
	public readonly DataConditionValues.DataConditionValue<T> Value;

	public readonly DataConditionComparators.DataConditionComparator<T> Comparator;

	public DataCondition(DataConditionValues.DataConditionValue<T> value, DataConditionComparators.DataConditionComparator<T> comparator, bool negate = false)
		: base(negate)
	{
		Value = value;
		Comparator = comparator;
	}

	protected override bool InnerConditionMet()
	{
		return Comparator.Check(Value.GetValue());
	}

	public override string Explain()
	{
		return Comparator.Explain(Value.ValueName, Negate);
	}

	private string FormatValueToCode(T value)
	{
		if (!(value is float num))
		{
			if (value is bool flag)
			{
				return flag.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
			}
			return value.ToString();
		}
		return num.ToString(CultureInfo.InvariantCulture) + "f";
	}

	private string FormatValueToCodeName(T value)
	{
		if (!(value is float num))
		{
			if (value is int num2)
			{
				if ((float)num2 < 0f)
				{
					return "Minus" + (-num2).ToString(CultureInfo.InvariantCulture);
				}
				return num2.ToString(CultureInfo.InvariantCulture);
			}
			return value.ToString();
		}
		if (num < 0f)
		{
			return "Minus" + (0f - num).ToString(CultureInfo.InvariantCulture).Replace('.', '_');
		}
		return num.ToString(CultureInfo.InvariantCulture).Replace('.', '_');
	}

	public string GenerateVariableName()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(Value.ValueName);
			if (Comparator is DataConditionComparators.DataConditionComparatorOneParameter<T> dataConditionComparatorOneParameter)
			{
				stringBuilder.Append(dataConditionComparatorOneParameter.GetComparator(Negate));
				stringBuilder.Append(FormatValueToCodeName(dataConditionComparatorOneParameter.CompareValue));
			}
			else if (Comparator is DataConditionComparators.DataConditionComparatorTwoParameter<T> dataConditionComparatorTwoParameter)
			{
				stringBuilder.Append(dataConditionComparatorTwoParameter.GetComparator(Negate));
				stringBuilder.Append(FormatValueToCodeName(dataConditionComparatorTwoParameter.CompareValue1));
				stringBuilder.Append("And");
				stringBuilder.Append(FormatValueToCodeName(dataConditionComparatorTwoParameter.CompareValue2));
			}
			else
			{
				stringBuilder.Append("UnknownComparator");
			}
			return stringBuilder.ToString();
		}
	}

	public override string GenerateCode()
	{
		Type typeFromHandle = typeof(GameVariables);
		FieldInfo fieldInfo = typeFromHandle.GetFields().FirstOrDefault((FieldInfo t) => string.Equals(t.Name, Value.ValueName, StringComparison.OrdinalIgnoreCase));
		PropertyInfo propertyInfo = typeFromHandle.GetProperties().FirstOrDefault((PropertyInfo t) => string.Equals(t.Name, Value.ValueName, StringComparison.OrdinalIgnoreCase));
		string text;
		if (fieldInfo != null)
		{
			text = "nameof(GameVariables." + fieldInfo.Name + ")";
		}
		else
		{
			text = ((!(propertyInfo != null)) ? ("/*TODO: FIX*/\"" + Value.ValueName + "\"") : ("nameof(GameVariables." + propertyInfo.Name + ")"));
		}
		if (Comparator is DataConditionComparators.DataConditionComparatorOneParameter<T> dataConditionComparatorOneParameter)
		{
			string text2 = string.Format("{0}.{1}", "SingleArgumentComparators", dataConditionComparatorOneParameter.GetComparator(Negate));
			return "StoryCondition.VariableCondition<" + typeof(T).Name + ">(" + text + ", " + text2 + ", " + FormatValueToCode(dataConditionComparatorOneParameter.CompareValue) + ")";
		}
		if (Comparator is DataConditionComparators.DataConditionComparatorTwoParameter<T> dataConditionComparatorTwoParameter)
		{
			string text3 = string.Format("{0}.{1}", "TwoArgumentComparators", dataConditionComparatorTwoParameter.GetComparator(Negate));
			return "StoryCondition.VariableCondition<" + typeof(T).Name + ">(" + text + ", " + text3 + ", " + FormatValueToCode(dataConditionComparatorTwoParameter.CompareValue1) + ", " + FormatValueToCode(dataConditionComparatorTwoParameter.CompareValue2) + ")";
		}
		return "(UNKNOWN COMPARATOR)";
	}

	public bool Equals(DataCondition<T> other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		if (Comparator is DataConditionComparators.DataConditionComparatorOneParameter<T> dataConditionComparatorOneParameter && other.Comparator is DataConditionComparators.DataConditionComparatorOneParameter<T> dataConditionComparatorOneParameter2)
		{
			if (dataConditionComparatorOneParameter.GetComparator(Negate) != dataConditionComparatorOneParameter2.GetComparator(other.Negate))
			{
				return false;
			}
			if (!DataConditionComparators.DataConditionComparatorOneParameter<T>.CompareValueComparer.Equals(dataConditionComparatorOneParameter, dataConditionComparatorOneParameter2))
			{
				return false;
			}
		}
		else
		{
			if (!(Comparator is DataConditionComparators.DataConditionComparatorTwoParameter<T> dataConditionComparatorTwoParameter) || !(other.Comparator is DataConditionComparators.DataConditionComparatorTwoParameter<T> dataConditionComparatorTwoParameter2))
			{
				return false;
			}
			if (dataConditionComparatorTwoParameter.GetComparator(Negate) != dataConditionComparatorTwoParameter2.GetComparator(other.Negate))
			{
				return false;
			}
			if (!DataConditionComparators.DataConditionComparatorTwoParameter<T>.CompareValuesComparer.Equals(dataConditionComparatorTwoParameter, dataConditionComparatorTwoParameter2))
			{
				return false;
			}
		}
		if (Value == other.Value)
		{
			return true;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (this != obj)
		{
			if (obj is DataCondition<T> other)
			{
				return Equals(other);
			}
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Negate, Value.ValueName);
	}
}
