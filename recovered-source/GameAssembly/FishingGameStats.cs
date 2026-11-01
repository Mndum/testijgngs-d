using UnityEngine;

public class FishingGameStats : GameStatContainer
{
	public struct BasicFishingGameplayStats
	{
		public float FishingRodSize;

		public float FishingRodAcceleration;

		public float FishingRodFishReelingSpeed;

		public float FishingRodMaxEffectiveWeight;

		public float ReelingSpeedNegative(float currentFishDifficulty)
		{
			return Mathf.Lerp(FishingRodFishReelingSpeed * 0.3f, FishingRodFishReelingSpeed * 0.6f, currentFishDifficulty);
		}
	}

	public readonly GameStatPercentBoost FishingRodSizeBoost = new GameStatPercentBoost();

	public readonly GameStatPercentBoost FishingRodAccelerationBoost = new GameStatPercentBoost();

	public readonly GameStatPercentBoost FishingRodFishReelingSpeedBoost = new GameStatPercentBoost();

	public readonly GameStatDirectedPercentBoost<GameId> FishDirectedCatchOddsBoost = new GameStatDirectedPercentBoost<GameId>();

	public const float MinReelingSpeed = 0.2f;

	public const float MaxReelingSpeed = 0.5f;

	public float FishingRodSize01 => FishingRodSizeBoost.CalculateBoosted(FishingRodItem?.FishingRodSize01 ?? 0f);

	public float FishingRodAcceleration01 => FishingRodAccelerationBoost.CalculateBoosted(FishingRodItem?.FishingRodAcceleration01 ?? 0f);

	public float FishingRodFishReelingSpeed01 => FishingRodFishReelingSpeedBoost.CalculateBoosted(FishingRodItem?.FishingRodReelingSpeed01 ?? 0f);

	public float FishingRodMaxEffectiveWeight => FishingRodItem?.FishingRodMaxEffectiveFishWeightKg ?? 1f;

	public Item FishingRodItem { get; private set; }

	public Item BaitItem { get; private set; }

	public FishingBaitItemPrefabData BaitItemPrefabData { get; private set; }

	public bool HasBait => BaitItem != null;

	public override void ResetAllStats()
	{
		base.ResetAllStats();
		FishingRodItem = null;
		BaitItem = null;
		BaitItemPrefabData = null;
	}

	public void Recalculate(GameVariables gameVariables)
	{
		Item.ItemManager itemManager = gameVariables.itemManager;
		Item firstItemAtSlot = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.Anon_FishingRod.Id);
		if (firstItemAtSlot != null)
		{
			_ = firstItemAtSlot.GetItemPrefab().FishingRodItemPrefabData;
			SetFishingRod(firstItemAtSlot);
		}
		Item firstItemAtSlot2 = itemManager.GetFirstItemAtSlot(ItemSlot.SlotManager.Anon_FishingBait.Id);
		if (firstItemAtSlot2 != null)
		{
			FishingBaitItemPrefabData fishingBaitItemPrefabData = firstItemAtSlot2.GetItemPrefab().FishingBaitItemPrefabData;
			SetBait(firstItemAtSlot2, fishingBaitItemPrefabData);
		}
		float fishJointStatBoost = gameVariables.FishJointStatBoost;
		FishingRodSizeBoost.Percent *= fishJointStatBoost;
		FishingRodAccelerationBoost.Percent *= fishJointStatBoost;
		FishingRodFishReelingSpeedBoost.Percent *= fishJointStatBoost;
	}

	public BasicFishingGameplayStats GetBasicGameplayStats()
	{
		return new BasicFishingGameplayStats
		{
			FishingRodSize = Mathf.Lerp(0.1f, 0.2f, FishingRodSize01),
			FishingRodAcceleration = Mathf.Lerp(3f, 10f, FishingRodAcceleration01),
			FishingRodFishReelingSpeed = Mathf.Lerp(0.2f, 0.5f, FishingRodFishReelingSpeed01),
			FishingRodMaxEffectiveWeight = FishingRodMaxEffectiveWeight
		};
	}

	public void SetFishingRod(Item fishingRod)
	{
		FishingRodItem = fishingRod;
	}

	public void SetBait(Item fishingBaitItem, FishingBaitItemPrefabData baitItemPrefabData)
	{
		BaitItem = fishingBaitItem;
		BaitItemPrefabData = baitItemPrefabData;
	}
}
