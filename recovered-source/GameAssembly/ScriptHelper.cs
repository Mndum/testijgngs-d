using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fungus;
using MoonSharp.Interpreter;
using UnityEngine;

public static class ScriptHelper
{
	public static void RegisterSimpleFunc<T>()
	{
		Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T>), (DynValue v) =>
		{
			Closure function = v.Function;
			return (Func<T>)(() => function.Call().ToObject<T>());
		});
	}

	public static void RegisterSimpleFunc<T1, T2, TResult>()
	{
		Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, T2, TResult>), (DynValue v) =>
		{
			Closure function = v.Function;
			return (Func<T1, T2, TResult>)((T1 p1, T2 p2) => function.Call(p1, p2).ToObject<TResult>());
		});
	}

	public static void RegisterSimpleFunc<T1, TResult>()
	{
		Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Func<T1, TResult>), (DynValue v) =>
		{
			Closure function = v.Function;
			return (Func<T1, TResult>)((T1 p1) => function.Call(p1).ToObject<TResult>());
		});
	}

	public static void RegisterSimpleAction<T1, T2>()
	{
		Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T1, T2>), (DynValue v) =>
		{
			Closure function = v.Function;
			return (Action<T1, T2>)((T1 p1, T2 p2) =>
			{
				function.Call(p1, p2);
			});
		});
	}

	public static void RegisterSimpleAction<T>()
	{
		Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action<T>), (DynValue v) =>
		{
			Closure function = v.Function;
			return (Action<T>)((T p) =>
			{
				function.Call(p);
			});
		});
	}

	public static void RegisterSimpleAction()
	{
		Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Function, typeof(Action), (DynValue v) =>
		{
			Closure function = v.Function;
			return (Action)(() =>
			{
				function.Call();
			});
		});
	}

	public static bool RegisterTypeWithSafety(Type type)
	{
		if (UserData.RegisterType(type) == null)
		{
			Debug.LogError($"Failed to register \"{type}\"!");
			return false;
		}
		return true;
	}

	public static void RegisterWithFieldsAndPropertiesAndMethods(Type type)
	{
		if (!RegisterTypeWithSafety(type))
		{
			return;
		}
		HashSet<Type> hashSet = new HashSet<Type>();
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
		for (int i = 0; i < fields.Length; i++)
		{
			Type type2 = fields[i].GetType();
			hashSet.Add(type2);
		}
		PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
		for (int i = 0; i < properties.Length; i++)
		{
			Type type3 = properties[i].GetType();
			hashSet.Add(type3);
		}
		MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
		foreach (MethodInfo obj in methods)
		{
			Type returnType = obj.ReturnType;
			hashSet.Add(returnType);
			ParameterInfo[] parameters = obj.GetParameters();
			for (int j = 0; j < parameters.Length; j++)
			{
				Type parameterType = parameters[j].ParameterType;
				hashSet.Add(parameterType);
			}
		}
		hashSet.Remove(typeof(object));
		foreach (Type item in hashSet)
		{
			if (!UserData.IsTypeRegistered(item))
			{
				RegisterWithFieldsAndPropertiesAndMethods(item);
			}
		}
	}

	public static void RegisterTypeAndAddTypeToGlobalTable<T>(this LuaEnvironment luaEnvironment, string name = null)
	{
		if (name == null)
		{
			name = typeof(T).Name;
		}
		UserData.RegisterType<T>();
		luaEnvironment.Interpreter.Globals[name] = UserData.CreateStatic<T>();
	}

	public static void RegisterTypeAndAddValueToGlobalTable<T>(this LuaEnvironment luaEnvironment, T value, string name = null)
	{
		if (name == null)
		{
			name = typeof(T).Name;
		}
		UserData.RegisterType<T>();
		luaEnvironment.Interpreter.Globals[name] = value;
	}

	public static void GenerateCode(List<Type> types)
	{
		ILookup<int, MethodInfo> lookup = (from t in typeof(ScriptHelper).GetMethods()
			where t.IsGenericMethod
			select t).ToArray().ToLookup((MethodInfo t) => t.GetGenericArguments().Length);
		string text = "RegisterSimpleAction();\n";
		foreach (MethodInfo item in lookup[1])
		{
			foreach (Type type in types)
			{
				text = text + item.Name + "<" + type.Name + ">();\n";
			}
		}
		foreach (MethodInfo item2 in lookup[2])
		{
			foreach (Type type2 in types)
			{
				foreach (Type type3 in types)
				{
					text = text + item2.Name + "<" + type2.Name + "," + type3.Name + ">();\n";
				}
			}
		}
		Debug.Log(text);
	}

	public static void RegisterAll()
	{
		RegisterSimpleAction();
		RegisterSimpleFunc<bool>();
		RegisterSimpleFunc<float>();
		RegisterSimpleFunc<int>();
		RegisterSimpleFunc<double>();
		RegisterSimpleFunc<string>();
		RegisterSimpleAction<bool>();
		RegisterSimpleAction<float>();
		RegisterSimpleAction<int>();
		RegisterSimpleAction<double>();
		RegisterSimpleAction<string>();
		RegisterSimpleFunc<bool, bool>();
		RegisterSimpleFunc<bool, float>();
		RegisterSimpleFunc<bool, int>();
		RegisterSimpleFunc<bool, double>();
		RegisterSimpleFunc<bool, string>();
		RegisterSimpleFunc<float, bool>();
		RegisterSimpleFunc<float, float>();
		RegisterSimpleFunc<float, int>();
		RegisterSimpleFunc<float, double>();
		RegisterSimpleFunc<float, string>();
		RegisterSimpleFunc<int, bool>();
		RegisterSimpleFunc<int, float>();
		RegisterSimpleFunc<int, int>();
		RegisterSimpleFunc<int, double>();
		RegisterSimpleFunc<int, string>();
		RegisterSimpleFunc<double, bool>();
		RegisterSimpleFunc<double, float>();
		RegisterSimpleFunc<double, int>();
		RegisterSimpleFunc<double, double>();
		RegisterSimpleFunc<double, string>();
		RegisterSimpleFunc<string, bool>();
		RegisterSimpleFunc<string, float>();
		RegisterSimpleFunc<string, int>();
		RegisterSimpleFunc<string, double>();
		RegisterSimpleFunc<string, string>();
	}
}
