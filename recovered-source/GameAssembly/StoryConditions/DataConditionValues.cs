using System;
using System.Collections.Generic;

namespace StoryConditions;

public static class DataConditionValues
{
	public abstract class DataConditionValue<T>
	{
		private static Dictionary<string, DataConditionValue<T>> PermanentCache;

		private static Dictionary<string, WeakReference<DataConditionValue<T>>> Cache;

		public readonly string ValueName;

		static DataConditionValue()
		{
			PermanentCache = new Dictionary<string, DataConditionValue<T>>();
			Cache = new Dictionary<string, WeakReference<DataConditionValue<T>>>();
			if (typeof(T) == typeof(int))
			{
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("Day", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Day), "day");
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("Hour", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.Hour), "hour");
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("time", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time), "Time");
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("currentstreamlength", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GetStreamLength()));
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("currentstreamviewers", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GetStreamViewers()));
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("combinedstreamlength", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.streamedFor));
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("streamcount", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.streamCount));
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("longeststreamlength", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.longestStream));
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("clothieritemsbought", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.ShopManager.GetShop("clothier").ItemsBought));
				DataConditionValue<int>.AddToPermanentCache((DataConditionValue<int>)new FuncDataConditionValue<int>("clothiermoneyspent", () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.ShopManager.GetShop("clothier").MoneySpent));
			}
			else if (typeof(T) == typeof(bool))
			{
				DataConditionValue<bool>.AddToPermanentCache((DataConditionValue<bool>)new FuncDataConditionValue<bool>("DebugEnabled", () => OptionsStatic.DebugEnabledPref), "Debug");
			}
		}

		protected DataConditionValue(string valueName)
		{
			ValueName = valueName;
		}

		public static bool TryGetCachedValue(string name, out DataConditionValue<T> value)
		{
			if (PermanentCache.TryGetValue(name, out value))
			{
				return true;
			}
			if (Cache.TryGetValue(name, out var value2) && value2.TryGetTarget(out value))
			{
				return true;
			}
			return false;
		}

		public static void AddToPermanentCache(DataConditionValue<T> value, params string[] additionalAliases)
		{
			PermanentCache[value.ValueName] = value;
			foreach (string key in additionalAliases)
			{
				PermanentCache[key] = value;
			}
		}

		public static void AddToCache(DataConditionValue<T> value)
		{
			Cache[value.ValueName] = new WeakReference<DataConditionValue<T>>(value);
		}

		public abstract T GetValue();
	}

	public class FuncDataConditionValue<T> : DataConditionValue<T>
	{
		public readonly Func<T> GetValueFunc;

		public FuncDataConditionValue(string valueName, Func<T> getValueFunc)
			: base(valueName)
		{
			GetValueFunc = getValueFunc;
		}

		public override T GetValue()
		{
			return GetValueFunc();
		}
	}

	public class GameVariablesConditionValue<T> : DataConditionValue<T>
	{
		public readonly Func<GameVariables, T> Getter;

		public GameVariablesConditionValue(string valueName, Func<GameVariables, T> getter)
			: base(valueName)
		{
			Getter = getter;
		}

		public static bool TryCreate(string name, out GameVariablesConditionValue<T> value)
		{
			if (GameVariables.ClassDescription.GetMembers<T>().TryGetGetter(name, out var getter))
			{
				value = new GameVariablesConditionValue<T>(name, getter);
				return true;
			}
			value = null;
			return false;
		}

		public override T GetValue()
		{
			return Getter(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables);
		}
	}

	public class ModelBrainStateConditionValue<TValue, TState> : DataConditionValue<TValue> where TState : ModelBrainState
	{
		private const char SplitChar = '_';

		public readonly Func<TState, TValue> Getter;

		public ModelBrainStateConditionValue(string valueName, Func<TState, TValue> getter)
			: base(valueName)
		{
			Getter = getter;
		}

		public static string GetName(string valueName)
		{
			return $"{typeof(TState).Name}{'_'}{valueName}";
		}

		public static ModelBrainStateConditionValue<TValue, TState> Create(string value)
		{
			NonJitClassDescription<TState>.FieldDictionary<TValue> members = NonJitClassDescriptionSingleton.GetClassDescription<TState>().GetMembers<TValue>();
			return new ModelBrainStateConditionValue<TValue, TState>(GetName(value), members.GetGetter(value));
		}

		public static bool TryCreate(string fullName, out ModelBrainStateConditionValue<TValue, TState> value)
		{
			int num = fullName.IndexOf('_');
			if (num >= 0)
			{
				string name = fullName.Substring(0, num);
				string name2 = fullName.Substring(num + 1);
				NonJitClassDescription<TState>.FieldDictionary<TValue> members = ((NonJitClassDescription<TState>)NonJitClassDescriptionSingleton.GetClassDescriptionByName(name)).GetMembers<TValue>();
				value = new ModelBrainStateConditionValue<TValue, TState>(fullName, members.GetGetter(name2));
				return true;
			}
			value = null;
			return false;
		}

		public override TValue GetValue()
		{
			if (ModelBrain.TryGet("bot", out var brain) && brain.CurrentState is TState arg)
			{
				return Getter(arg);
			}
			return default;
		}
	}

	public static bool TryCreate<T>(string name, out DataConditionValue<T> value)
	{
		if (DataConditionValue<T>.TryGetCachedValue(name, out value))
		{
			return true;
		}
		if (GameVariablesConditionValue<T>.TryCreate(name, out var value2))
		{
			value = value2;
			DataConditionValue<T>.AddToCache(value);
			return true;
		}
		value = null;
		return false;
	}

	public static DataConditionValue<T> Create<T>(string name)
	{
		if (TryCreate(name, out DataConditionValue<T> value))
		{
			return value;
		}
		throw new Exception("Data condition source " + name + " not found!");
	}
}
