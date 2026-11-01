using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class FishingMinigameManager : ISerializationCallbackReceiver
{
	[SerializeField]
	private List<FishSave> _serializedSaves = new List<FishSave>();

	[SerializeField]
	private int _fishingTipSeed;

	private Dictionary<GameId, FishSave> _saveDictionary = new Dictionary<GameId, FishSave>();

	public int FishingTipSeed => _fishingTipSeed;

	public int FishCaughtCount => _saveDictionary.Sum((KeyValuePair<GameId, FishSave> save) => save.Value.TimesCaught);

	public int DifferentFishesCaughtCount => Enumerable.Count(_saveDictionary, (KeyValuePair<GameId, FishSave> save) => save.Value.WasEverCaught);

	public int BigFishesCaughtCount => Enumerable.Count(_saveDictionary, (KeyValuePair<GameId, FishSave> save) => save.Value.MaxWeightCaughtPercentile > 0.8f);

	public double MaxCaughtFishSize => _saveDictionary.Max((KeyValuePair<GameId, FishSave> save) => save.Value.MaxWeightCaught);

	public void UpdateDayPassed()
	{
		_fishingTipSeed = UnityEngine.Random.Range(-2147483648, 2147483647);
	}

	public void OnBeforeSerialize()
	{
		_serializedSaves.Clear();
		_serializedSaves.AddRange(_saveDictionary.Values);
	}

	public void OnAfterDeserialize()
	{
		_saveDictionary.Clear();
		foreach (FishSave serializedSafe in _serializedSaves)
		{
			if (!_saveDictionary.TryAdd(serializedSafe.Key, serializedSafe))
			{
				Debug.LogError($"Duplicate saved song data in {typeof(CookingMinigameManager)}");
			}
		}
	}

	public FishRegisteringFlagsEnum RegisterCaughtFish(FishingLocationData fishingLocationData, Item item)
	{
		if (!item.IsFish)
		{
			return FishRegisteringFlagsEnum.None;
		}
		return GetSave(item.GameId).RegisterCaughtFish(fishingLocationData, item);
	}

	public FishSave GetSave(GameId key)
	{
		if (_saveDictionary.TryGetValue(key, out var value))
		{
			return value;
		}
		value = new FishSave(key);
		_saveDictionary.Add(key, value);
		return value;
	}
}
