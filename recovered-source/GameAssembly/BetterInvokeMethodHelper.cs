using System;
using System.Collections.Generic;
using System.Globalization;
using Fungus;
using MarkerMetro.Unity.WinLegacy.Reflection;
using UnityEngine;

public static class BetterInvokeMethodHelper
{
	public abstract class TypeHandlerBase
	{
		public readonly Type HandledType;

		public abstract string DefaultValueString { get; }

		public TypeHandlerBase(Type handledType)
		{
			HandledType = handledType;
		}

		public abstract bool TryObjectFromString(string s, out object o);

		public abstract string ObjectToString(object value);
	}

	public class TypeHandler<T> : TypeHandlerBase
	{
		public delegate bool TryFromStringFunc(string s, out T value);

		private Func<T, string> ToStringFunc { get; }

		private TryFromStringFunc FromStringFunc { get; }

		public T DefaultValue { get; }

		public override string DefaultValueString => ToStringFunc(DefaultValue);

		public TypeHandler(Func<T, string> toString, TryFromStringFunc fromString, T defaultValue)
			: base(typeof(T))
		{
			ToStringFunc = toString;
			FromStringFunc = fromString;
			DefaultValue = defaultValue;
		}

		public override bool TryObjectFromString(string s, out object o)
		{
			if (FromStringFunc(s, out var value))
			{
				o = value;
				return true;
			}
			o = DefaultValue;
			return false;
		}

		public override string ObjectToString(object value)
		{
			if (value is T arg)
			{
				return ToStringFunc(arg);
			}
			Debug.LogError($"{value} is not {typeof(T).Name}");
			return null;
		}
	}

	private static HashSet<Type> _supportedTypes;

	private static Dictionary<string, TypeHandlerBase> _nameToHandler;

	private static Dictionary<Type, TypeHandlerBase> _typeToHandler;

	static BetterInvokeMethodHelper()
	{
		_supportedTypes = new HashSet<Type>();
		_nameToHandler = new Dictionary<string, TypeHandlerBase>();
		_typeToHandler = new Dictionary<Type, TypeHandlerBase>();
		RegisterType(new TypeHandler<int>((int x) => x.ToString(CultureInfo.InvariantCulture), delegate(string x, out int v)
		{
			return int.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out v);
		}, 0));
		RegisterType(new TypeHandler<bool>((bool x) => x.ToString(CultureInfo.InvariantCulture), bool.TryParse, defaultValue: false));
		RegisterType(new TypeHandler<float>((float x) => x.ToString(CultureInfo.InvariantCulture), delegate(string x, out float v)
		{
			return float.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out v);
		}, 0f));
		RegisterType(new TypeHandler<string>((string x) => x, delegate(string sin, out string sout)
		{
			sout = sin;
			return true;
		}, ""));
		RegisterType(new TypeHandler<Color>((Color x) => ColorUtility.ToHtmlStringRGBA(x), delegate(string x, out Color v)
		{
			return ColorUtility.TryParseHtmlString(x, out v);
		}, Color.white));
	}

	private static void RegisterType<T>(TypeHandler<T> typeToRegister)
	{
		Type handledType = typeToRegister.HandledType;
		if (_supportedTypes.Contains(handledType))
		{
			Debug.LogError($"Duplicate type handler for {handledType}!");
			return;
		}
		string assemblyQualifiedName = handledType.AssemblyQualifiedName;
		if (assemblyQualifiedName == null)
		{
			Debug.LogError($"AssemblyQualifiedName for {handledType} is null!");
			return;
		}
		_supportedTypes.Add(handledType);
		_nameToHandler.Add(assemblyQualifiedName, typeToRegister);
		_typeToHandler.Add(handledType, typeToRegister);
	}

	public static bool TryFindTypeByName(string typeName, out Type t)
	{
		return ReflectionHelper.TryFindTypeByName(typeName, out t);
	}

	public static bool IsUnityObject(string typeName)
	{
		if (TryFindTypeByName(typeName, out var t))
		{
			return IsUnityObject(t);
		}
		return false;
	}

	public static bool IsUnityObject(Type type)
	{
		return typeof(UnityEngine.Object).IsAssignableFrom(type);
	}

	public static bool CanBeParsedString(Type type)
	{
		if (_supportedTypes.Contains(type))
		{
			return true;
		}
		if (type.IsEnum)
		{
			return true;
		}
		return false;
	}

	public static string GetDefaultValueString(Type type)
	{
		if (_typeToHandler.TryGetValue(type, out var value))
		{
			return value.DefaultValueString;
		}
		if (type.IsEnum)
		{
			return Enum.GetName(type, Activator.CreateInstance(type));
		}
		return "";
	}

	public static bool TryFromString(string typeName, string value, out object o)
	{
		if (_nameToHandler.TryGetValue(typeName, out var value2))
		{
			return value2.TryObjectFromString(value, out o);
		}
		if (TryFindTypeByName(typeName, out var t) && ReflectionExtensions.IsEnum(t))
		{
			if (Enum.TryParse(t, value, ignoreCase: true, out var result))
			{
				o = result;
				return true;
			}
			o = 0;
			return false;
		}
		Debug.LogError("Unhandled type \"" + typeName + "\"!");
		o = null;
		return false;
	}

	public static string ToString(string typeName, object value)
	{
		if (_nameToHandler.TryGetValue(typeName, out var value2))
		{
			return value2.ObjectToString(value);
		}
		if (TryFindTypeByName(typeName, out var t))
		{
			if (ReflectionExtensions.IsEnum(t))
			{
				return Enum.GetName(t, value);
			}
			Debug.LogError($"Unhandled type \"{typeName}\" for value \"{value}\"!");
		}
		else
		{
			Debug.LogError("Type \"" + typeName + "\" not found.");
		}
		return null;
	}
}
