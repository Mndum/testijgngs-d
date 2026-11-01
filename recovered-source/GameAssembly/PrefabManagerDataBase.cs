using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class PrefabManagerDataBase<TKey, TPrefab, TSerializedPart> where TPrefab : IKey<TKey> where TSerializedPart : IKey<TKey>, IWithPrefab<TPrefab>
{
	protected Dictionary<TKey, TPrefab> _prefabs = new Dictionary<TKey, TPrefab>();

	public IEnumerable<KeyValuePair<TKey, TPrefab>> AllPrefabs => _prefabs.AsEnumerable();

	public void Init(IEnumerable<TPrefab> prefabs)
	{
		_prefabs.Clear();
		foreach (TPrefab prefab in prefabs)
		{
			if (!_prefabs.TryAdd(prefab.Key, prefab))
			{
				Debug.LogError($"Prefab with duplicate key \"{prefab.Key}\" in {GetType().Name}");
			}
		}
	}

	public void Init(params TPrefab[] prefabs)
	{
		Init((IEnumerable<TPrefab>)prefabs);
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

	public TPrefab Get(TKey key)
	{
		if (_prefabs.TryGetValue(key, out var value))
		{
			return value;
		}
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine($"{GetType()}: Key {key} not found! Created a new one!");
			stringBuilder.AppendLine("Available keys:");
			stringBuilder.AppendJoin(", ", _prefabs.Keys);
			Debug.LogWarning(stringBuilder);
			return _prefabs[key] = GenerateEmpty(key);
		}
	}

	protected abstract TPrefab GenerateEmpty(TKey key);
}
