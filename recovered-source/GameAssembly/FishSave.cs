using System;
using UnityEngine;

[Serializable]
public class FishSave
{
	[SerializeField]
	private GameId _key;

	private ItemPrefab _itemPrefab;

	[SerializeField]
	private int _timesCaught;

	[SerializeField]
	private float _minWeightCaught;

	[SerializeField]
	private float _maxWeightCaught;

	[SerializeField]
	private GameId _lastCaughtLocationDataKey;

	private ItemPrefab ItemPrefab => _itemPrefab ?? (_itemPrefab = ItemPrefab.ItemPrefabManager.GetItemById(_key));

	public GameId Key => _key;

	public int TimesCaught => _timesCaught;

	public float MinWeightCaught => _minWeightCaught;

	public float MaxWeightCaught => _maxWeightCaught;

	public GameId LastCaughtLocationDataKey => _lastCaughtLocationDataKey;

	public bool WasEverCaught => _timesCaught > 0;

	public float MaxWeightCaughtPercentile => ItemPrefab.FishData?.DetermineFishPercentile(MaxWeightCaught) ?? 0f;

	public FishSave(GameId key)
	{
		_key = key;
	}

	public FishRegisteringFlagsEnum RegisterCaughtFish(FishingLocationData fishingLocationData, Item item)
	{
		if (fishingLocationData.IsImaginary)
		{
			return FishRegisteringFlagsEnum.None;
		}
		if (item.GameId != _key)
		{
			Debug.LogError($"Trying to register a fish with a wrong id! {item.GameId} to {_key}!");
			return FishRegisteringFlagsEnum.None;
		}
		float fishWeight = item.FishWeight;
		if (fishWeight < 0f)
		{
			Debug.LogError("Trying to register a fish (" + item.GetName() + ") with negative weight! Ignoring the weight!");
			return FishRegisteringFlagsEnum.None;
		}
		_lastCaughtLocationDataKey = fishingLocationData.Key;
		_timesCaught++;
		if (_timesCaught == 1)
		{
			_minWeightCaught = (_maxWeightCaught = fishWeight);
			return FishRegisteringFlagsEnum.FirstTime;
		}
		FishRegisteringFlagsEnum fishRegisteringFlagsEnum = FishRegisteringFlagsEnum.None;
		if (_maxWeightCaught < fishWeight)
		{
			_maxWeightCaught = fishWeight;
			fishRegisteringFlagsEnum |= FishRegisteringFlagsEnum.NewMaxSizeRecord;
		}
		if (_minWeightCaught > fishWeight)
		{
			_minWeightCaught = fishWeight;
			fishRegisteringFlagsEnum |= FishRegisteringFlagsEnum.NewMinSizeRecord;
		}
		return fishRegisteringFlagsEnum;
	}
}
