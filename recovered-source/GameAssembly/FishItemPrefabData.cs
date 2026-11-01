using System;
using System.Text;
using MoonSharp.Interpreter.Interop;
using UnityEngine;

public class FishItemPrefabData : LuaUtilities.ILuaTransferable, ICloneable<FishItemPrefabData>, ICloneable
{
	public const float FishMinWeight = 0.01f;

	public const float FishMaxWeight = 1000f;

	private float _sizeDistribution = 0.5f;

	private float _maxWeightKg = 2f;

	private float _strengthMultiplier = 1f;

	public float AverageWeightKg => _maxWeightKg * _sizeDistribution;

	public float SizeDistribution01
	{
		get
		{
			return _sizeDistribution;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_sizeDistribution = Mathf.Clamp01(value);
		}
	}

	public float MaxWeightKg
	{
		get
		{
			return _maxWeightKg;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_maxWeightKg = Mathf.Clamp(value, 0.01f, 1000f);
		}
	}

	public float StrengthMultiplier
	{
		get
		{
			return _strengthMultiplier;
		}
		[MoonSharpVisible(true)]
		private set
		{
			_strengthMultiplier = Mathf.Clamp(value, 0.1f, 4f);
		}
	}

	public FishingCatchBehaviourType BehaviourType
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	}

	public FishPreferences FishPreferences
	{
		get;
		[MoonSharpVisible(true)]
		private set;
	} = FishPreferences.OmnivorousFishPreferences;

	public static FishItemPrefabData CreateInstance(float sizeDistribution01, float maxWeight, FishPreferences fishPreferences, FishingCatchBehaviourType behaviourType, float strengthMultiplier)
	{
		return new FishItemPrefabData
		{
			SizeDistribution01 = sizeDistribution01,
			MaxWeightKg = maxWeight,
			FishPreferences = fishPreferences,
			BehaviourType = behaviourType,
			StrengthMultiplier = strengthMultiplier
		};
	}

	public static FishItemPrefabData CreateInstance()
	{
		return new FishItemPrefabData();
	}

	public string ToLuaString()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.AppendLine("(function()");
			stringBuilder.AppendLine("local fd = FishItemPrefabData.CreateInstance()");
			stringBuilder.AppendLine("fd.SizeDistribution01 = " + SizeDistribution01.ToLuaString());
			stringBuilder.AppendLine("fd.MaxWeightKg = " + MaxWeightKg.ToLuaString());
			stringBuilder.AppendLine("fd.StrengthMultiplier = " + StrengthMultiplier.ToLuaString());
			stringBuilder.AppendLine("fd.BehaviourType = " + BehaviourType.ToLuaString());
			stringBuilder.AppendLine("fd.FishPreferences = " + FishPreferences.ToLuaString());
			stringBuilder.Append("\treturn fd end)()");
			return stringBuilder.ToString();
		}
	}

	public void FillFishData(float throwValue, Item fishItem, FishingGameStats fishingGameStats, FishingLocationData fishingLocationData)
	{
		throwValue = Mathf.Clamp01(throwValue);
		fishItem.FishWeight = DetermineFishWeight(throwValue, fishingGameStats, fishingLocationData);
		fishItem.FishPollution = DetermineFishPollution(throwValue, fishingGameStats, fishingLocationData);
	}

	private float DetermineFishPollution(float throwValue01, FishingGameStats fishingGameStats, FishingLocationData fishingLocationData)
	{
		float value = UnityEngine.Random.value;
		if (value < 0.02f || value < throwValue01 / 10f)
		{
			return 0f;
		}
		float num = Mathf.Lerp(1f, 0.1f, throwValue01);
		float dirtiness = fishingLocationData.Dirtiness01;
		return GlobalFunctions.Lerp3(0f, dirtiness * 0.75f * num, dirtiness, value);
	}

	private float DetermineFishWeight(float throwValue01, FishingGameStats fishingGameStats, FishingLocationData fishingLocationData)
	{
		float fishingRodMaxEffectiveWeight = fishingGameStats.FishingRodMaxEffectiveWeight;
		float fishSizePotential = fishingLocationData.FishSizePotential01;
		float min = 0.01f;
		float num2;
		float num3;
		if ((double)UnityEngine.Random.value < (double)fishSizePotential * 0.03)
		{
			float num = UnityEngine.Random.Range(1f, 1.1f);
			num2 = AverageWeightKg * num;
			num3 = MaxWeightKg * num;
		}
		else
		{
			num2 = Mathf.Lerp(0.01f, AverageWeightKg, 0.5f + 0.5f * fishSizePotential);
			num3 = Mathf.Lerp(AverageWeightKg, MaxWeightKg, fishSizePotential * 0.5f + 0.5f);
		}
		float sizeMultiplierLimiter = fishingLocationData.GetSizeMultiplierLimiter(num3);
		num2 *= sizeMultiplierLimiter;
		num3 *= sizeMultiplierLimiter;
		float p = Mathf.Lerp(3f, 0.4f, throwValue01);
		float num4 = GlobalFunctions.Lerp3(min, num2, num3, Mathf.Pow(UnityEngine.Random.value, p));
		if (num4 < fishingRodMaxEffectiveWeight)
		{
			return num4;
		}
		if (UnityEngine.Random.value < 0.01f)
		{
			return num4;
		}
		float max = fishingRodMaxEffectiveWeight * 1.3f;
		num4 = num4 * 0.25f + fishingRodMaxEffectiveWeight * 0.75f;
		return Mathf.Clamp(num4, min, max);
	}

	public float DetermineFishPercentile(float weight)
	{
		if (weight <= 0.01f)
		{
			return 0f;
		}
		if (weight <= AverageWeightKg)
		{
			return 0.5f * Mathf.InverseLerp(0.01f, AverageWeightKg, weight);
		}
		return 0.5f + 0.5f * Mathf.InverseLerp(AverageWeightKg, MaxWeightKg, weight);
	}

	public float DetermineWeightFromPercentile(float percentile)
	{
		if (percentile <= 0f)
		{
			return 0.01f;
		}
		if (percentile <= 0.5f)
		{
			return Mathf.Lerp(0.01f, AverageWeightKg, 2f * percentile);
		}
		return Mathf.Lerp(AverageWeightKg, MaxWeightKg, 2f * percentile - 1f);
	}

	public FishItemPrefabData Clone()
	{
		FishItemPrefabData fishItemPrefabData = (FishItemPrefabData)MemberwiseClone();
		fishItemPrefabData.FishPreferences = fishItemPrefabData.FishPreferences.Clone();
		return fishItemPrefabData;
	}
}
