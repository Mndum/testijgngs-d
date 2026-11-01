using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public static class LuaUtilities
{
	public interface ILuaTransferable
	{
		string ToLuaString();
	}

	public const string New = "__new";

	public const string EmptyTable = "{}";

	public static string LuaConstructor(string type, params string[] arguments)
	{
		if (arguments == null)
		{
			arguments = Array.Empty<string>();
		}
		return type + ".__new(" + string.Join(", ", arguments) + ")";
	}

	public static string ToLuaString(this string text)
	{
		if (text == null)
		{
			return "\"\"";
		}
		return "'" + text.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("'", "\\'") + "'";
	}

	public static string ToLuaString(this float value)
	{
		return value.ToString(CultureInfo.InvariantCulture);
	}

	public static string ToLuaString(this int value)
	{
		return value.ToString(CultureInfo.InvariantCulture);
	}

	public static string ToLuaString(this Enum value)
	{
		return $"{value.GetType().Name}.{value}";
	}

	public static string ToLuaString(this Guid value)
	{
		return "Guid.Parse(" + value.ToString().ToLuaString() + ")";
	}

	public static string ToLuaString(this bool value)
	{
		if (!value)
		{
			return "false";
		}
		return "true";
	}

	public static string ToLuaString(this IEnumerable<string> text)
	{
		if (text != null)
		{
			return "{" + string.Join(", ", text.Select((string t) => t.ToLuaString())) + "}";
		}
		return "{}";
	}

	public static string ToLuaString(this IEnumerable<int> text)
	{
		if (text != null)
		{
			return "{" + string.Join(", ", text.Select((int t) => t.ToLuaString())) + "}";
		}
		return "{}";
	}

	public static string ToLuaStringTransferable<T>(this IEnumerable<T> objects) where T : ILuaTransferable
	{
		if (objects != null)
		{
			return "{" + string.Join(", ", objects.Select((T t) => t.ToLuaString())) + "}";
		}
		return "{}";
	}

	public static string ToLuaString<T>(this IDictionary<string, string> dictionary)
	{
		if (dictionary != null)
		{
			return "{" + string.Join(", ", dictionary.Select((KeyValuePair<string, string> t) => "[" + t.Key.ToLuaString() + "] = " + t.Value.ToLuaString())) + "}";
		}
		return "{}";
	}

	public static string ToLuaString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, string> keyToLuaString, Func<TValue, string> valueToLuaString)
	{
		if (dictionary != null)
		{
			return "{" + string.Join(", ", dictionary.Select((KeyValuePair<TKey, TValue> t) => "[" + keyToLuaString(t.Key) + "] = " + valueToLuaString(t.Value))) + "}";
		}
		return "{}";
	}

	public static string ToLuaStringRaw<T>(this IEnumerable<KeyValuePair<string, T>> dictionary) where T : ILuaTransferable
	{
		if (dictionary != null)
		{
			return "{" + string.Join(", ", dictionary.Select((KeyValuePair<string, T> t) => "[" + t.Key.ToLuaString() + "] = " + t.Value.ToLuaString())) + "}";
		}
		return "{}";
	}
}
