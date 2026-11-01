using System;
using System.Collections.Generic;
using GameItems;
using IC.AutomaticReference;
using UnityEngine;

public class FishingLocationData : IKey<GameId>
{
	private readonly GameId _key;

	public SimpleLocalizedString NameSls;

	public SimpleLocalizedString DescriptionSls;

	public IReference<Sprite> BackgroundSprite;

	public Func<bool> UnlockedFunc;

	public int PriceToEnter;

	public float Cleanliness;

	public float FishQuantity;

	public float FishSizePotential;

	public float FishWeightLimitHardKg;

	public bool IsImaginary;

	private static Lottery<FishingCatchEntry> _reusableLottery = new Lottery<FishingCatchEntry>();

	private List<FishingCatchEntry> _possibleCatches = new List<FishingCatchEntry>();

	public GameId Key => _key;

	public string Name => NameSls.Value;

	public string Description => DescriptionSls.Value;

	public bool IsUnlocked
	{
		get
		{
			if (UnlockedFunc != null)
			{
				return UnlockedFunc();
			}
			return true;
		}
	}

	public float Dirtiness01 => Mathf.Clamp01(1f - Cleanliness / 10f);

	public float FishQuantity01 => Mathf.Clamp01(FishQuantity / 10f);

	public bool HasHardWeightLimit => FishWeightLimitHardKg > 1f;

	public float FishSizePotential01 => Mathf.Clamp01(FishSizePotential / 10f);

	public IReadOnlyCollection<FishingCatchEntry> PossibleCatches => _possibleCatches;

	public FishingLocationData(GameId key, SimpleLocalizedString nameSls, SimpleLocalizedString descriptionSls, float cleanliness, float fishQuantity, float fishSizePotential, IReference<Sprite> backgroundSprite)
	{
		_key = key;
		NameSls = nameSls;
		DescriptionSls = descriptionSls;
		Cleanliness = cleanliness;
		FishQuantity = fishQuantity;
		FishSizePotential = fishSizePotential;
		BackgroundSprite = backgroundSprite;
	}

	public float GetSizeMultiplierLimiter(float maxWeight)
	{
		if (HasHardWeightLimit && maxWeight > FishWeightLimitHardKg)
		{
			return FishWeightLimitHardKg / maxWeight;
		}
		return 1f;
	}

	public bool TryGetBite(float waitedFor, float throwValue, FishingGameStats fishingGameStats, out FishingBite result)
	{
		result = null;
		_reusableLottery.Clear();
		int hour = GameVariables.Current.Hour;
		float num = -1f;
		foreach (FishingCatchEntry possibleCatch in _possibleCatches)
		{
			if (possibleCatch.CalculateOdds(fishingGameStats, hour, out var odds))
			{
				_reusableLottery.AddItem(possibleCatch, odds);
				if (odds > num)
				{
					num = odds;
				}
			}
		}
		if (num <= 0f)
		{
			return false;
		}
		float num2 = (0.1f + MathF.Tanh(waitedFor / 30f) * 0.9f) * FishQuantity01 * num;
		if (UnityEngine.Random.value > num2)
		{
			return false;
		}
		if (_reusableLottery.TryGetRandom(out var result2))
		{
			result = result2.ConvertToBite(throwValue, fishingGameStats, this);
			return true;
		}
		result = null;
		return false;
	}

	public void AddCatch(FishingCatchEntry fishingCatchEntry)
	{
		_possibleCatches.Add(fishingCatchEntry);
	}

	public void AddFishCatch(GameId fish, float odds)
	{
		_possibleCatches.Add(new FishFishingCatchEntry(odds, fish));
	}

	public void AddTrashCatches(float trashOdds, params ItemEnum[] trashItems)
	{
		foreach (ItemEnum itemEnum in trashItems)
		{
			_possibleCatches.Add(new TrashCatchEntry(trashOdds / (float)trashItems.Length, itemEnum.ToGameId()));
		}
	}

	public IEnumerable<GameId> GetAllPossibleFishes()
	{
		foreach (FishingCatchEntry possibleCatch in _possibleCatches)
		{
			if (possibleCatch is FishFishingCatchEntry fishFishingCatchEntry)
			{
				yield return fishFishingCatchEntry.FishItemGameId;
			}
		}
	}
}
