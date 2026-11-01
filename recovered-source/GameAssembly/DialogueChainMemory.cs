using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DialogueChainMemory : ISerializationCallbackReceiver
{
	[SerializeField]
	private List<GameId> ignoredGameIds;

	private HashSet<GameId> ignoredGameIdsSet;

	[field: SerializeField]
	public List<int> ignored { get; private set; }

	public IReadOnlyCollection<GameId> IgnoredGameIds => ignoredGameIds.AsReadOnly();

	public DialogueChainMemory()
	{
		ignored = new List<int>();
		ignoredGameIds = new List<GameId>();
		ignoredGameIdsSet = new HashSet<GameId>();
	}

	public void AddIgnored(GameId id)
	{
		if (ignoredGameIdsSet.Add(id))
		{
			ignoredGameIds.Add(id);
		}
	}

	public void RemoveIgnored(GameId id)
	{
		if (ignoredGameIdsSet.Remove(id))
		{
			ignoredGameIds.Remove(id);
		}
	}

	public bool IsIgnored(GameId id)
	{
		return ignoredGameIdsSet.Contains(id);
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		ignoredGameIdsSet = ignoredGameIdsSet.ClearOrCreate();
		ignoredGameIdsSet.AddRange(ignoredGameIds);
		if (!ignored.Any())
		{
			return;
		}
		foreach (int item in ignored)
		{
			AddIgnored(GameId.CreateVanilla(item));
		}
		ignored.Clear();
	}
}
