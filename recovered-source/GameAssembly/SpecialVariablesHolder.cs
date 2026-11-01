using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpecialVariablesHolder
{
	[Serializable]
	public struct SerializableKeyValue<TVal>(string k, TVal v)
	{
		private sealed class KEqualityComparer : IEqualityComparer<SerializableKeyValue<TVal>>
		{
			public bool Equals(SerializableKeyValue<TVal> x, SerializableKeyValue<TVal> y)
			{
				return string.Equals(x.K, y.K, StringComparison.Ordinal);
			}

			public int GetHashCode(SerializableKeyValue<TVal> obj)
			{
				if (obj.K == null)
				{
					return 0;
				}
				return obj.K.GetHashCode(StringComparison.Ordinal);
			}
		}

		private sealed class KRelationalComparer : IComparer<SerializableKeyValue<TVal>>
		{
			public int Compare(SerializableKeyValue<TVal> x, SerializableKeyValue<TVal> y)
			{
				return string.Compare(x.K, y.K, StringComparison.Ordinal);
			}
		}

		public string K = k;

		public TVal V = v;

		public static IComparer<SerializableKeyValue<TVal>> KRelational { get; } = new KRelationalComparer();

		public static IEqualityComparer<SerializableKeyValue<TVal>> KEquality { get; } = new KEqualityComparer();
	}

	private const StringComparison KeyStringComparison = StringComparison.Ordinal;

	[SerializeField]
	protected SerializableKeyValue<int>[] I = Array.Empty<SerializableKeyValue<int>>();

	[SerializeField]
	protected SerializableKeyValue<float>[] F = Array.Empty<SerializableKeyValue<float>>();

	[SerializeField]
	protected SerializableKeyValue<string>[] S = Array.Empty<SerializableKeyValue<string>>();

	protected static int GetSpecialVariableIndex<TVal>(string key, SerializableKeyValue<TVal>[] array)
	{
		int num = 0;
		int num2 = array.Length - 1;
		while (num <= num2)
		{
			int num3 = num + (num2 - num) / 2;
			int num4 = string.Compare(array[num3].K, key, StringComparison.Ordinal);
			if (num4 == 0)
			{
				return num3;
			}
			if (num4 < 0)
			{
				num = num3 + 1;
			}
			else
			{
				num2 = num3 - 1;
			}
		}
		return -1;
	}

	protected static bool TryGetSpecialVariableValue<TVal>(string key, SerializableKeyValue<TVal>[] array, out TVal value)
	{
		int specialVariableIndex = GetSpecialVariableIndex(key, array);
		if (specialVariableIndex >= 0)
		{
			value = array[specialVariableIndex].V;
			return true;
		}
		value = default;
		return false;
	}

	protected static void SetSpecialVariable<TVal>(string key, ref SerializableKeyValue<TVal>[] array, TVal value)
	{
		int specialVariableIndex = GetSpecialVariableIndex(key, array);
		if (specialVariableIndex >= 0)
		{
			array[specialVariableIndex].V = value;
		}
		else
		{
			array = array.AddToSortedArrayCopy(new SerializableKeyValue<TVal>(key, value), SerializableKeyValue<TVal>.KRelational);
		}
	}

	protected static void EnsureArrayExistsAndIsSorted<TVal>(ref SerializableKeyValue<TVal>[] array)
	{
		if (array == null)
		{
			array = Array.Empty<SerializableKeyValue<TVal>>();
		}
		else if (array.Length != 0)
		{
			Array.Sort(array, SerializableKeyValue<TVal>.KRelational);
		}
	}

	protected static void CopyArrayContentToTarget<TVal>(SerializableKeyValue<TVal>[] source, ref SerializableKeyValue<TVal>[] target)
	{
		if (source.Length == 0)
		{
			target = source;
			return;
		}
		if (target == null || source == target || source.Length != target.Length)
		{
			target = new SerializableKeyValue<TVal>[source.Length];
		}
		Array.Copy(source, target, source.Length);
	}

	public void SetIntSpecialVariable(string key, int value)
	{
		SetSpecialVariable(key, ref I, value);
	}

	public bool TryGetIntSpecialVariable(string key, out int value)
	{
		return TryGetSpecialVariableValue(key, I, out value);
	}

	public int GetIntSpecialVariableOrDefault(string key, int defaultValue = 0)
	{
		if (!TryGetSpecialVariableValue(key, I, out var value))
		{
			return defaultValue;
		}
		return value;
	}

	public void SetIntSpecialVariableCastBool(string key, bool value)
	{
		SetSpecialVariable(key, ref I, value ? 1 : 0);
	}

	public bool TryGetIntSpecialVariableCastBool(string key, out bool value)
	{
		if (TryGetSpecialVariableValue(key, I, out var value2))
		{
			value = value2 != 0;
			return true;
		}
		value = false;
		return false;
	}

	public bool GetIntSpecialVariableOrDefaultCastBool(string key, bool defaultValue = false)
	{
		if (!TryGetIntSpecialVariableCastBool(key, out var value))
		{
			return defaultValue;
		}
		return value;
	}

	public void SetFloatSpecialVariable(string key, float value)
	{
		SetSpecialVariable(key, ref F, value);
	}

	public bool TryGetFloatSpecialVariable(string key, out float value)
	{
		return TryGetSpecialVariableValue(key, F, out value);
	}

	public float GetFloatSpecialVariableOrDefault(string key, float defaultValue = 0f)
	{
		if (!TryGetSpecialVariableValue(key, F, out var value))
		{
			return defaultValue;
		}
		return value;
	}

	public void SetStringSpecialVariable(string key, string value)
	{
		SetSpecialVariable(key, ref S, value);
	}

	public bool TryGetStringSpecialVariable(string key, out string value)
	{
		return TryGetSpecialVariableValue(key, S, out value);
	}

	public string GetStringSpecialVariableOrDefault(string key, string defaultValue = "")
	{
		if (!TryGetSpecialVariableValue(key, S, out var value))
		{
			return defaultValue;
		}
		return value;
	}

	public bool TryGetAnySpecialVariable(string key, out object value)
	{
		if (TryGetSpecialVariableValue(key, I, out var value2))
		{
			value = value2;
			return true;
		}
		if (TryGetSpecialVariableValue(key, F, out var value3))
		{
			value = value3;
			return true;
		}
		if (TryGetSpecialVariableValue(key, S, out var value4))
		{
			value = value4;
			return true;
		}
		value = null;
		return false;
	}

	protected void CloneSpecialVariables(SpecialVariablesHolder clone)
	{
		CopyArrayContentToTarget(I, ref clone.I);
		CopyArrayContentToTarget(F, ref clone.F);
		CopyArrayContentToTarget(S, ref clone.S);
	}

	public void OnAfterDeserializeSpecialVariableHolder()
	{
		EnsureArrayExistsAndIsSorted(ref I);
		EnsureArrayExistsAndIsSorted(ref F);
		EnsureArrayExistsAndIsSorted(ref S);
	}
}
