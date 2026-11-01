using System.Collections.Generic;
using UnityEngine;

public class FishFishingCatchEntry : FishingCatchEntry
{
	public readonly GameId FishItemGameId;

	private ItemPrefab _relatedItemPrefab;

	private float _rawCatchOdds;

	public override bool ShownInTips => true;

	public FishFishingCatchEntry(float catchCatchOdds, GameId fishItemGameId)
	{
		_rawCatchOdds = catchCatchOdds;
		FishItemGameId = fishItemGameId;
		ItemPrefab fishItemPrefab = GetFishItemPrefab();
		if (fishItemPrefab.FishData == null)
		{
			Debug.LogError($"{GetType().Name} was passed a non fish {fishItemPrefab.Name} {fishItemGameId}!");
		}
	}

	public ItemPrefab GetFishItemPrefab()
	{
		return _relatedItemPrefab ?? (_relatedItemPrefab = ItemPrefab.ItemPrefabManager.GetItemById(FishItemGameId));
	}

	public override FishingBite ConvertToBite(float throwValue, FishingGameStats fishingGameStats, FishingLocationData fishingLocationData)
	{
		FishItemPrefabData fishData = ItemPrefab.ItemPrefabManager.GetItemById(FishItemGameId).FishData;
		if (fishData != null)
		{
			Item item = Item.CreateRandomItem(FishItemGameId);
			fishData.FillFishData(throwValue, item, fishingGameStats, fishingLocationData);
			return new FishingBite(fishData.StrengthMultiplier * item.FishWeight, fishData.BehaviourType, this, new Item[1] { item });
		}
		Debug.LogError(string.Format("Not fish ({0}) in {1}!!!", FishItemGameId, "FishFishingCatchEntry"));
		return new FishingBite(0f, FishingCatchBehaviourType.Rock, this, null);
	}

	public override bool CalculateOdds(FishingGameStats fishingGameStats, int currentHour, out float odds)
	{
		odds = 0f;
		if (!fishingGameStats.HasBait)
		{
			return false;
		}
		FishPreferences fishPreferences = GetFishItemPrefab().FishData?.FishPreferences;
		if (fishPreferences == null)
		{
			return false;
		}
		float num = 0f;
		foreach (var (key, num3) in fishingGameStats.BaitItemPrefabData.FishingBaitComposition)
		{
			if (fishPreferences.FishingBaitTypesPreference.TryGetValue(key, out var value))
			{
				num += value * num3;
			}
		}
		if (fishPreferences.SpecificFishingBaitItemPreference.TryGetValue(FishItemGameId, out var value2))
		{
			num += value2;
		}
		num *= fishPreferences.DetermineActiveHourModifier(currentHour);
		num = fishingGameStats.FishDirectedCatchOddsBoost.CalculateBoosted(FishItemGameId, num);
		odds = num * _rawCatchOdds;
		return true;
	}

	public override string Describe()
	{
		return GetFishItemPrefab().Name;
	}
}
