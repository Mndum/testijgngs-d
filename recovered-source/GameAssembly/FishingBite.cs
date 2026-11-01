using System;
using System.Collections.Generic;
using UnityEngine;

public class FishingBite
{
	public float FishStrengthApparentWeightKg;

	public FishingCatchBehaviourType BehaviourType;

	public FishingCatchEntry RelatedEntry;

	private IReadOnlyCollection<Item> _items;

	public FishingBite(float fishStrengthApparentWeightKg, FishingCatchBehaviourType behaviourType, FishingCatchEntry relatedEntry, IReadOnlyCollection<Item> items)
	{
		FishStrengthApparentWeightKg = fishStrengthApparentWeightKg;
		BehaviourType = behaviourType;
		RelatedEntry = relatedEntry;
		_items = items;
	}

	public override string ToString()
	{
		return $"Strength:{FishStrengthApparentWeightKg} Behaviour:{BehaviourType}";
	}

	public float GetFishDifficulty01(float fishTiredness, float gameplayStatsFishingRodMaxEffectiveWeightKg)
	{
		float num = Mathf.Lerp(1f, 0.5f, fishTiredness / 60f);
		float num2 = FishStrengthApparentWeightKg * num;
		float num3 = num2 - gameplayStatsFishingRodMaxEffectiveWeightKg;
		float a = num2 + gameplayStatsFishingRodMaxEffectiveWeightKg;
		float num4 = num3 / Mathf.Max(a, 0.1f);
		float num5 = Mathf.Min(num2 / 500f, 0.5f);
		float t = (MathF.Tanh(num4 + num5) + 1f) / 2f;
		return GlobalFunctions.Lerp3(0f, 0.4f, 1f, t);
	}

	public IReadOnlyCollection<Item> GetItems()
	{
		return _items ?? Array.Empty<Item>();
	}
}
