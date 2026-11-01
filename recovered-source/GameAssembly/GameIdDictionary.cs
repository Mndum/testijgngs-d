using System;
using System.Collections.Generic;

public class GameIdDictionary<T> : Dictionary<Guid, Dictionary<int, T>>
{
	public Dictionary<int, T> Vanilla
	{
		get
		{
			if (TryGetValue(Guid.Empty, out var value))
			{
				return value;
			}
			return base[Guid.Empty] = new Dictionary<int, T>();
		}
	}

	public IEnumerable<(GameId id, T value)> AllValuesWithIds()
	{
		using Enumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<Guid, Dictionary<int, T>> guidToDictionary = enumerator.Current;
			foreach (KeyValuePair<int, T> item in guidToDictionary.Value)
			{
				yield return (id: GameId.CreateGeneric(guidToDictionary.Key, item.Key), value: item.Value);
			}
		}
	}

	public IEnumerable<T> AllValues()
	{
		using Enumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			foreach (KeyValuePair<int, T> item in enumerator.Current.Value)
			{
				yield return item.Value;
			}
		}
	}

	public bool TryGetValue(GameId gameId, out T t)
	{
		if (TryGetValue(gameId.Guid, out var value) && value.TryGetValue(gameId.Id, out t))
		{
			return true;
		}
		t = default;
		return false;
	}

	public void AddOrReplace(GameId gameId, T value)
	{
		if (TryGetValue(gameId.Guid, out var value2))
		{
			value2[gameId.Id] = value;
			return;
		}
		Add(gameId.Guid, new Dictionary<int, T> { { gameId.Id, value } });
	}
}
