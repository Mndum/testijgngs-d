using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;

namespace StoryConditions;

public abstract class DataConditionComparators
{
	public abstract class DataConditionComparator<T>
	{
		public abstract bool Check(T value);

		public abstract string Explain(string x, bool negate);
	}

	public enum SingleArgumentComparators
	{
		Equal,
		NotEqual,
		AboveEqual,
		BelowEqual,
		Above,
		Below
	}

	public abstract class DataConditionComparatorOneParameter<T> : DataConditionComparator<T>
	{
		private sealed class CompareValueEqualityComparer : IEqualityComparer<DataConditionComparatorOneParameter<T>>
		{
			public bool Equals(DataConditionComparatorOneParameter<T> x, DataConditionComparatorOneParameter<T> y)
			{
				if (x == y)
				{
					return true;
				}
				if (x == null)
				{
					return false;
				}
				if (y == null)
				{
					return false;
				}
				if (x.GetType() != y.GetType())
				{
					return false;
				}
				return EqualityComparer<T>.Default.Equals(x.CompareValue, y.CompareValue);
			}

			public int GetHashCode(DataConditionComparatorOneParameter<T> obj)
			{
				return EqualityComparer<T>.Default.GetHashCode(obj.CompareValue);
			}
		}

		public readonly T CompareValue;

		public abstract SingleArgumentComparators Comparator { get; }

		public static IEqualityComparer<DataConditionComparatorOneParameter<T>> CompareValueComparer { get; } = new CompareValueEqualityComparer();

		protected DataConditionComparatorOneParameter(T compareValue)
		{
			CompareValue = compareValue;
		}

		public SingleArgumentComparators GetComparator(bool negate)
		{
			if (negate)
			{
				return NegateComparator(Comparator);
			}
			return Comparator;
		}
	}

	public abstract class DataConditionComparatorTwoParameter<T> : DataConditionComparator<T>
	{
		private sealed class CompareValue1CompareValue2EqualityComparer : IEqualityComparer<DataConditionComparatorTwoParameter<T>>
		{
			public bool Equals(DataConditionComparatorTwoParameter<T> x, DataConditionComparatorTwoParameter<T> y)
			{
				if (x == y)
				{
					return true;
				}
				if (x == null)
				{
					return false;
				}
				if (y == null)
				{
					return false;
				}
				if (x.GetType() != y.GetType())
				{
					return false;
				}
				if (EqualityComparer<T>.Default.Equals(x.CompareValue1, y.CompareValue1))
				{
					return EqualityComparer<T>.Default.Equals(x.CompareValue2, y.CompareValue2);
				}
				return false;
			}

			public int GetHashCode(DataConditionComparatorTwoParameter<T> obj)
			{
				return HashCode.Combine(obj.CompareValue1, obj.CompareValue2);
			}
		}

		public readonly T CompareValue1;

		public readonly T CompareValue2;

		public abstract TwoArgumentComparators Comparator { get; }

		public static IEqualityComparer<DataConditionComparatorTwoParameter<T>> CompareValuesComparer { get; } = new CompareValue1CompareValue2EqualityComparer();

		protected DataConditionComparatorTwoParameter(T compareValue1, T compareValue2)
		{
			CompareValue1 = compareValue1;
			CompareValue2 = compareValue2;
		}

		public TwoArgumentComparators GetComparator(bool negate)
		{
			if (negate)
			{
				return NegateComparator(Comparator);
			}
			return Comparator;
		}
	}

	public sealed class EqualityConditionComparator<T> : DataConditionComparatorOneParameter<T> where T : IComparable<T>
	{
		public override SingleArgumentComparators Comparator => SingleArgumentComparators.Equal;

		public EqualityConditionComparator(T compareValue)
			: base(compareValue)
		{
		}

		public override string Explain(string x, bool negate)
		{
			if (!negate)
			{
				return $"{x} == {CompareValue}";
			}
			return $"{x} != {CompareValue}";
		}

		public override bool Check(T value)
		{
			return value.CompareTo(CompareValue) == 0;
		}
	}

	public sealed class AboveConditionComparator<T> : DataConditionComparatorOneParameter<T> where T : IComparable<T>
	{
		public override SingleArgumentComparators Comparator => SingleArgumentComparators.Above;

		public AboveConditionComparator(T compareValue)
			: base(compareValue)
		{
		}

		public override string Explain(string x, bool negate)
		{
			if (!negate)
			{
				return $"{x} > {CompareValue}";
			}
			return $"{x} <= {CompareValue}";
		}

		public override bool Check(T value)
		{
			return value.CompareTo(CompareValue) > 0;
		}
	}

	public sealed class AboveOrEqualConditionComparator<T> : DataConditionComparatorOneParameter<T> where T : IComparable<T>
	{
		public override SingleArgumentComparators Comparator => SingleArgumentComparators.AboveEqual;

		public AboveOrEqualConditionComparator(T compareValue)
			: base(compareValue)
		{
		}

		public override string Explain(string x, bool negate)
		{
			if (!negate)
			{
				return $"{x} >= {CompareValue}";
			}
			return $"{x} < {CompareValue}";
		}

		public override bool Check(T value)
		{
			return value.CompareTo(CompareValue) >= 0;
		}
	}

	public sealed class BetweenConditionComparator<T> : DataConditionComparatorTwoParameter<T> where T : IComparable<T>
	{
		public override TwoArgumentComparators Comparator => TwoArgumentComparators.Between;

		public BetweenConditionComparator(T compareValue1, T compareValue2)
			: base(compareValue1, compareValue2)
		{
		}

		public override string Explain(string x, bool negate)
		{
			if (!negate)
			{
				return $"{CompareValue1} < {x} < {CompareValue2}";
			}
			return $"{x} <= {CompareValue1} or {CompareValue2} <= {x}";
		}

		public override bool Check(T value)
		{
			T compareValue = CompareValue1;
			if (value.CompareTo(compareValue) > 0)
			{
				T compareValue2 = CompareValue2;
				return value.CompareTo(compareValue2) < 0;
			}
			return false;
		}
	}

	public sealed class BetweenOrEqualConditionComparator<T> : DataConditionComparatorTwoParameter<T> where T : IComparable<T>
	{
		public override TwoArgumentComparators Comparator => TwoArgumentComparators.BetweenEqual;

		public BetweenOrEqualConditionComparator(T compareValue1, T compareValue2)
			: base(compareValue1, compareValue2)
		{
		}

		public override string Explain(string x, bool negate)
		{
			if (!negate)
			{
				return $"{CompareValue1} <= {x} <= {CompareValue2}";
			}
			return $"{x} < {CompareValue1} or {CompareValue2} < {x}";
		}

		public override bool Check(T value)
		{
			T compareValue = CompareValue1;
			if (value.CompareTo(compareValue) >= 0)
			{
				T compareValue2 = CompareValue2;
				return value.CompareTo(compareValue2) <= 0;
			}
			return false;
		}
	}

	public enum TwoArgumentComparators
	{
		BetweenEqual,
		Between,
		AroundEqual,
		Around
	}

	[Pure]
	public static SingleArgumentComparators NegateComparator(SingleArgumentComparators comparator)
	{
		return comparator switch
		{
			SingleArgumentComparators.Equal => SingleArgumentComparators.NotEqual, 
			SingleArgumentComparators.NotEqual => SingleArgumentComparators.Equal, 
			SingleArgumentComparators.AboveEqual => SingleArgumentComparators.Below, 
			SingleArgumentComparators.BelowEqual => SingleArgumentComparators.Above, 
			SingleArgumentComparators.Above => SingleArgumentComparators.BelowEqual, 
			SingleArgumentComparators.Below => SingleArgumentComparators.AboveEqual, 
			_ => throw new ArgumentOutOfRangeException("comparator", comparator, null), 
		};
	}

	public static (DataConditionComparator<T> comparator, bool negate) GetSingleArgumentComparator<T>(SingleArgumentComparators singleArgumentComparators, T value) where T : IComparable<T>
	{
		bool item = false;
		if (typeof(T) == typeof(bool) && singleArgumentComparators != SingleArgumentComparators.Equal && singleArgumentComparators != SingleArgumentComparators.NotEqual)
		{
			throw new ArgumentException("Invalid condition specified for bool type.");
		}
		DataConditionComparator<T> item2;
		switch (singleArgumentComparators)
		{
		case SingleArgumentComparators.Equal:
			item2 = new EqualityConditionComparator<T>(value);
			break;
		case SingleArgumentComparators.NotEqual:
			item2 = new EqualityConditionComparator<T>(value);
			item = true;
			break;
		case SingleArgumentComparators.AboveEqual:
			item2 = new AboveOrEqualConditionComparator<T>(value);
			break;
		case SingleArgumentComparators.BelowEqual:
			item2 = new AboveConditionComparator<T>(value);
			item = true;
			break;
		case SingleArgumentComparators.Above:
			item2 = new AboveConditionComparator<T>(value);
			break;
		case SingleArgumentComparators.Below:
			item2 = new AboveOrEqualConditionComparator<T>(value);
			item = true;
			break;
		default:
			throw new ArgumentException("Invalid singleArgumentComparators value");
		}
		return (comparator: item2, negate: item);
	}

	[Pure]
	public static TwoArgumentComparators NegateComparator(TwoArgumentComparators comparator)
	{
		return comparator switch
		{
			TwoArgumentComparators.BetweenEqual => TwoArgumentComparators.Around, 
			TwoArgumentComparators.Between => TwoArgumentComparators.AroundEqual, 
			TwoArgumentComparators.AroundEqual => TwoArgumentComparators.Between, 
			TwoArgumentComparators.Around => TwoArgumentComparators.BetweenEqual, 
			_ => throw new ArgumentOutOfRangeException("comparator", comparator, null), 
		};
	}

	public static (DataConditionComparator<T> comparator, bool negate) GetTwoArgumentComparator<T>(TwoArgumentComparators twoArgumentComparators, T value1, T value2) where T : IComparable<T>
	{
		bool item = false;
		if (typeof(T) == typeof(bool))
		{
			throw new ArgumentException("Invalid condition specified for bool type.");
		}
		DataConditionComparator<T> item2;
		switch (twoArgumentComparators)
		{
		case TwoArgumentComparators.BetweenEqual:
			item2 = new BetweenOrEqualConditionComparator<T>(value1, value2);
			break;
		case TwoArgumentComparators.Between:
			item2 = new BetweenConditionComparator<T>(value1, value2);
			break;
		case TwoArgumentComparators.Around:
			item2 = new BetweenOrEqualConditionComparator<T>(value1, value2);
			item = true;
			break;
		case TwoArgumentComparators.AroundEqual:
			item2 = new BetweenConditionComparator<T>(value1, value2);
			item = true;
			break;
		default:
			throw new ArgumentException("Invalid twoArgumentComparators value");
		}
		return (comparator: item2, negate: item);
	}

	public static (DataConditionComparator<T> comparator, bool negate) GetDataConditionComparator<T>(string[] arg) where T : IComparable<T>
	{
		if (arg.Length == 0)
		{
			throw new ArgumentException("No comparator name given! Expected for example \"equals\"!");
		}
		string condition = arg[0].TrimStart(' ').ToLower();
		bool item = false;
		if (typeof(T) == typeof(bool) && condition != "equals" && condition != "notequals")
		{
			throw new ArgumentException("Invalid condition specified for bool type.");
		}
		DataConditionComparator<T> item2;
		switch (condition)
		{
		case "equals":
			item2 = new EqualityConditionComparator<T>(ParseOneArgument());
			break;
		case "notequals":
			item2 = new EqualityConditionComparator<T>(ParseOneArgument());
			item = true;
			break;
		case "aboveeq":
			item2 = new AboveOrEqualConditionComparator<T>(ParseOneArgument());
			break;
		case "beloweq":
			item2 = new AboveConditionComparator<T>(ParseOneArgument());
			item = true;
			break;
		case "above":
			item2 = new AboveConditionComparator<T>(ParseOneArgument());
			break;
		case "below":
			item2 = new AboveOrEqualConditionComparator<T>(ParseOneArgument());
			item = true;
			break;
		case "betweeneq":
		{
			(T, T) tuple2 = ParseTwoArguments();
			item2 = new BetweenOrEqualConditionComparator<T>(tuple2.Item1, tuple2.Item2);
			break;
		}
		case "between":
		{
			(T, T) tuple = ParseTwoArguments();
			item2 = new BetweenConditionComparator<T>(tuple.Item1, tuple.Item2);
			break;
		}
		default:
			throw new ArgumentException("Invalid condition specified.");
		}
		return (comparator: item2, negate: item);
		T ParseOneArgument()
		{
			if (arg.Length < 2)
			{
				throw new ArgumentException("Not enough arguments for " + condition + "!");
			}
			if (arg.Length > 2)
			{
				throw new ArgumentException($"Too many arguments given for {condition}! (Expected 1 value. Got {arg.Length - 1})");
			}
			if (!TryParseValue<T>(arg[1], out var result))
			{
				throw new ArgumentException($"Invalid value for value1. \"{arg[1]}\" can't be parsed as {typeof(T)}!");
			}
			return result;
		}
		(T value1, T value2) ParseTwoArguments()
		{
			if (arg.Length < 3)
			{
				throw new ArgumentException($"Not enough arguments for {condition} (Expected 2 values. Got {arg.Length - 1})!");
			}
			if (arg.Length > 3)
			{
				throw new ArgumentException($"Too many arguments given for {condition}! (Expected 2 value. Got {arg.Length - 1})");
			}
			if (!TryParseValue<T>(arg[1], out var result))
			{
				throw new ArgumentException($"Invalid value for value1. \"{arg[1]}\" can't be parsed as {typeof(T)}!");
			}
			if (!TryParseValue<T>(arg[2], out var result2))
			{
				throw new ArgumentException($"Invalid value for value1. \"{arg[2]}\" can't be parsed as {typeof(T)}!");
			}
			return (value1: result, value2: result2);
		}
	}

	private static bool TryParseValue<T>(string value, out T result)
	{
		result = default;
		if (typeof(T) == typeof(int) && int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result2))
		{
			result = (T)(object)result2;
			return true;
		}
		if (typeof(T) == typeof(float) && float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result3))
		{
			result = (T)(object)result3;
			return true;
		}
		if (typeof(T) == typeof(long) && long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result4))
		{
			result = (T)(object)result4;
			return true;
		}
		if (typeof(T) == typeof(bool) && bool.TryParse(value, out var result5))
		{
			result = (T)(object)result5;
			return true;
		}
		return false;
	}
}
