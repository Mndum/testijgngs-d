using System;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabManagerDataOneToOneLazy<TKey, TPrefab, TSerializedPart> where TPrefab : IKey<TKey> where TSerializedPart : IKey<TKey>
{
	public class Prefabs
	{
		protected Dictionary<TKey, TPrefab> _prefabs;

		public IEnumerable<TPrefab> AllPrefabs => _prefabs.Values;

		public Prefabs(Dictionary<TKey, TPrefab> prefabs)
		{
			_prefabs = prefabs;
		}

		public Prefabs(IEnumerable<TPrefab> prefabs)
		{
			_prefabs = new Dictionary<TKey, TPrefab>();
			foreach (TPrefab prefab in prefabs)
			{
				_prefabs.Add(prefab.Key, prefab);
			}
		}

		public Prefabs()
		{
			_prefabs = new Dictionary<TKey, TPrefab>();
		}

		public void Add(TPrefab prefab)
		{
			_prefabs.Add(prefab.Key, prefab);
		}

		public bool TryGet(TKey key, out TPrefab prefab)
		{
			if (_prefabs.TryGetValue(key, out prefab))
			{
				return true;
			}
			prefab = default;
			return false;
		}
	}

	[Serializable]
	public class Serialized : ISerializationCallbackReceiver
	{
		[SerializeField]
		private List<TSerializedPart> _s = new List<TSerializedPart>();

		private Dictionary<TKey, TSerializedPart> _saveDictionary = new Dictionary<TKey, TSerializedPart>();

		private Func<TKey, TSerializedPart> _serializedPartGenerator;

		public IEnumerable<TSerializedPart> AllSaves => _saveDictionary.Values;

		public Serialized(Func<TKey, TSerializedPart> serializedPartGenerator)
		{
			_serializedPartGenerator = serializedPartGenerator;
		}

		public TSerializedPart Get(TKey key)
		{
			if (_saveDictionary.TryGetValue(key, out var value))
			{
				return value;
			}
			value = CreateSerializedPart(key);
			_saveDictionary.Add(key, value);
			return value;
		}

		public TSerializedPart Get(TPrefab prefab)
		{
			return Get(prefab.Key);
		}

		public void Remove(TKey dataKey)
		{
			_saveDictionary.Remove(dataKey);
		}

		public void Remove(TPrefab dataKey)
		{
			_saveDictionary.Remove(dataKey.Key);
		}

		public void Remove(TSerializedPart serializedPart)
		{
			_saveDictionary.Remove(serializedPart.Key);
		}

		public bool TryGet(TKey key, out TSerializedPart serializedPart)
		{
			return _saveDictionary.TryGetValue(key, out serializedPart);
		}

		public bool TryGet(TPrefab prefab, out TSerializedPart serializedPart)
		{
			return _saveDictionary.TryGetValue(prefab.Key, out serializedPart);
		}

		public void OnBeforeSerialize()
		{
			_s = _s.ClearOrCreate();
			_s.AddRange(_saveDictionary.Values);
		}

		public void OnAfterDeserialize()
		{
			_saveDictionary = _saveDictionary.ClearOrCreate();
			foreach (TSerializedPart item in _s)
			{
				if (item.Key == null)
				{
					Debug.LogError("Null key in " + typeof(TPrefab).Name + " <=" + typeof(TKey).Name + "=> " + typeof(TSerializedPart).Name);
				}
				else if (!_saveDictionary.TryAdd(item.Key, item))
				{
					Debug.LogError("Duplicate data in " + typeof(TPrefab).Name + " <=" + typeof(TKey).Name + "=> " + typeof(TSerializedPart).Name);
				}
			}
		}

		protected TSerializedPart CreateSerializedPart(TKey key)
		{
			return _serializedPartGenerator(key);
		}
	}
}
