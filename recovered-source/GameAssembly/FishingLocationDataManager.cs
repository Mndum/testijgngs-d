using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameItems;
using JetBrains.Annotations;

public static class FishingLocationDataManager
{
	private static readonly Dictionary<GameId, FishingLocationData> fishingLocations = new Dictionary<GameId, FishingLocationData>();

	public static IEnumerable<FishingLocationData> GetAllUnlockedLocations()
	{
		return fishingLocations.Values.Where((FishingLocationData x) => x.IsUnlocked);
	}

	public static IEnumerable<FishingLocationData> GetAllLocations()
	{
		return fishingLocations.Values;
	}

	[LoadableStaticMethod(new Type[] { typeof(ItemPrefab.ItemPrefabManager) })]
	private static UniTask Load()
	{
		List<FishingLocationData> list = new List<FishingLocationData>();
		Func<bool> unlockedFunc = () => GameVariables.Current.CheckFlag("fishing3");
		FishingLocationData fishingLocationData = new FishingLocationData(FishingLocationEnumGI.MicroplasticsCoast.ToGameId(), LOC.Other.TupleReferences.Fishing_MicroplasticsCoastName, LOC.Other.TupleReferences.Fishing_MicroplasticsCoastDescription, 3f, 1f, 9f, R.FishingBackgrounds.microplastic_coast);
		fishingLocationData.AddFishCatch(ItemEnum.Salmon.ToGameId(), 1f);
		fishingLocationData.AddFishCatch(ItemEnum.RedSnapper.ToGameId(), 0.7f);
		fishingLocationData.AddFishCatch(ItemEnum.Mackerel.ToGameId(), 1f);
		fishingLocationData.AddFishCatch(ItemEnum.Flounder.ToGameId(), 0.7f);
		fishingLocationData.AddFishCatch(ItemEnum.BlackSeaBass.ToGameId(), 0.7f);
		fishingLocationData.AddFishCatch(ItemEnum.Halibut.ToGameId(), 0.7f);
		fishingLocationData.AddFishCatch(ItemEnum.GoldenSalmon.ToGameId(), 0.001f);
		fishingLocationData.AddTrashCatches(0.1f, ItemEnum.OldCan, ItemEnum.OldShoe, ItemEnum.RustedBucket, ItemEnum.OilDrumLid, ItemEnum.OldDrSugarCan, ItemEnum.PlasticGroceryBag, ItemEnum.OldFishingNet, ItemEnum.OldCarBattery);
		list.Add(fishingLocationData);
		FishingLocationData fishingLocationData2 = new FishingLocationData(FishingLocationEnumGI.PollutedLake.ToGameId(), LOC.Other.TupleReferences.Fishing_PollutedLakeName, LOC.Other.TupleReferences.Fishing_PollutedLakeDescription, 2f, 5f, 7f, R.FishingBackgrounds.polluted_lake)
		{
			UnlockedFunc = unlockedFunc
		};
		fishingLocationData2.AddFishCatch(ItemEnum.Bluegill.ToGameId(), 1f);
		fishingLocationData2.AddFishCatch(ItemEnum.LargemouthBass.ToGameId(), 0.9f);
		fishingLocationData2.AddFishCatch(ItemEnum.NorthernPike.ToGameId(), 0.8f);
		fishingLocationData2.AddFishCatch(ItemEnum.YellowPerch.ToGameId(), 1f);
		fishingLocationData2.AddFishCatch(ItemEnum.Catfish.ToGameId(), 0.7f);
		fishingLocationData2.AddFishCatch(ItemEnum.Walleye.ToGameId(), 0.7f);
		fishingLocationData2.AddFishCatch(ItemEnum.Carp.ToGameId(), 2f);
		fishingLocationData2.AddFishCatch(ItemEnum.Tilapia.ToGameId(), 2f);
		fishingLocationData2.AddTrashCatches(0.1f, ItemEnum.OldCan, ItemEnum.OldShoe, ItemEnum.OldSyringe, ItemEnum.OldGrenade, ItemEnum.RustedBucket, ItemEnum.OilDrumLid, ItemEnum.BoneFragments, ItemEnum.ShatteredSmartphone, ItemEnum.OldDrSugarCan, ItemEnum.PlasticGroceryBag);
		list.Add(fishingLocationData2);
		FishingLocationData fishingLocationData3 = new FishingLocationData(FishingLocationEnumGI.TrashedRiver.ToGameId(), LOC.Other.TupleReferences.Fishing_TrashedRiverName, LOC.Other.TupleReferences.Fishing_TrashedRiverDescription, 4f, 2f, 9f, R.FishingBackgrounds.trashed_river)
		{
			UnlockedFunc = unlockedFunc
		};
		fishingLocationData3.AddFishCatch(ItemEnum.Salmon.ToGameId(), 0.01f);
		fishingLocationData3.AddFishCatch(ItemEnum.RainbowTrout.ToGameId(), 1.2f);
		fishingLocationData3.AddFishCatch(ItemEnum.SmallmouthBass.ToGameId(), 1f);
		fishingLocationData3.AddFishCatch(ItemEnum.Catfish.ToGameId(), 0.2f);
		fishingLocationData3.AddFishCatch(ItemEnum.BrownTrout.ToGameId(), 1f);
		fishingLocationData3.AddFishCatch(ItemEnum.Carp.ToGameId(), 2f);
		fishingLocationData3.AddFishCatch(ItemEnum.Tilapia.ToGameId(), 0.1f);
		fishingLocationData3.AddTrashCatches(0.1f, ItemEnum.OldCan, ItemEnum.OldShoe, ItemEnum.OldSyringe, ItemEnum.OldGrenade, ItemEnum.RustedBucket, ItemEnum.OilDrumLid, ItemEnum.ShatteredSmartphone, ItemEnum.OldDrSugarCan, ItemEnum.PlasticGroceryBag);
		list.Add(fishingLocationData3);
		FishingLocationData fishingLocationData4 = new FishingLocationData(FishingLocationEnumGI.PremiumFishFarm.ToGameId(), LOC.Other.TupleReferences.Fishing_PremiumFishFarmName, LOC.Other.TupleReferences.Fishing_PremiumFishFarmDescription, 8f, 10f, 2f, R.FishingBackgrounds.premium_farm)
		{
			UnlockedFunc = unlockedFunc,
			PriceToEnter = 5000,
			FishWeightLimitHardKg = 20f
		};
		fishingLocationData4.AddFishCatch(ItemEnum.Salmon.ToGameId(), 2f);
		fishingLocationData4.AddFishCatch(ItemEnum.Tilapia.ToGameId(), 1f);
		fishingLocationData4.AddFishCatch(ItemEnum.GoldenSalmon.ToGameId(), 0.0001f);
		fishingLocationData4.AddTrashCatches(0.010000001f, ItemEnum.ShatteredSmartphone, ItemEnum.OldShoe);
		list.Add(fishingLocationData4);
		FishingLocationData fishingLocationData5 = new FishingLocationData(FishingLocationEnumGI.ImaginarySea.ToGameId(), LOC.Other.TupleReferences.Fishing_ImaginarySeaName, LOC.Other.TupleReferences.Fishing_ImaginarySeaDescription, 10f, 7f, 5f, R.FishingBackgrounds.FishingJoint_SunnyBeach)
		{
			UnlockedFunc = () => false,
			FishWeightLimitHardKg = 100f,
			IsImaginary = true
		};
		fishingLocationData5.AddFishCatch(ItemEnum.Salmon.ToGameId(), 1f);
		fishingLocationData5.AddFishCatch(ItemEnum.RedSnapper.ToGameId(), 0.7f);
		fishingLocationData5.AddFishCatch(ItemEnum.Flounder.ToGameId(), 0.7f);
		fishingLocationData5.AddFishCatch(ItemEnum.Halibut.ToGameId(), 0.25f);
		fishingLocationData5.AddFishCatch(ItemEnum.GoldenSalmon.ToGameId(), 0.1f);
		fishingLocationData5.AddTrashCatches(0.020000001f, ItemEnum.OldShoe, ItemEnum.RustedBucket, ItemEnum.BoneFragments);
		list.Add(fishingLocationData5);
		foreach (FishingLocationData item in list)
		{
			fishingLocations.Add(item.Key, item);
		}
		return UniTask.CompletedTask;
	}

	[MustDisposeResource]
	public static PooledHashSet<GameId> GetAllPossibleFishes(out HashSet<GameId> fishIds)
	{
		return GetAllLocations().SelectMany((FishingLocationData x) => x.GetAllPossibleFishes()).ToHashSetPooled(out fishIds);
	}

	public static bool TryGetLocationData(GameId key, out FishingLocationData fishingLocationData)
	{
		return fishingLocations.TryGetValue(key, out fishingLocationData);
	}

	public static FishingLocationData GetLocationData(GameId fishingLocationEnum)
	{
		return fishingLocations[fishingLocationEnum];
	}
}
