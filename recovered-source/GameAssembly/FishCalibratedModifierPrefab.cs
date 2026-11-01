using System.Collections.Generic;
using GameItems;
using UnityEngine;

public class FishCalibratedModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[1] { MyColor32Presets.Blue };

	public const string FishId = "FishId";

	public override bool IsValidGameStatModifier => true;

	public override Color32[] GetModifierColors(ItemModifier itemModifier)
	{
		return ThisColorArray;
	}

	public override string GetModifierChar(ItemModifier itemModifier)
	{
		return "C";
	}

	public override string GetModifierName(ItemModifier itemModifier)
	{
		return LOC.UI.ItemModifier_FishCalibrated(GetRelatedItemPrefab(itemModifier).Name);
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.ItemModifier_FishCalibratedDescription(GetRelatedItemPrefab(itemModifier).Name);
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Legendary;
	}

	public override void ApplyAsGameStatModifier(ItemModifier itemModifier, GameVariables gameVariables, GameStats gameStats)
	{
		gameStats.FishingGameStats.FishDirectedCatchOddsBoost.AddBoost(GetRelatedGameId(itemModifier), itemModifier.GetLinearBoostFromLevel(1f));
	}

	public GameId GetRelatedGameId(ItemModifier itemModifier)
	{
		if (itemModifier.TryGetStringSpecialVariable("FishId", out var value) && GameId.FromShortString(value, out var gameId))
		{
			return gameId;
		}
		return ItemEnum.Carp.ToGameId();
	}

	public ItemPrefab GetRelatedItemPrefab(ItemModifier itemModifier)
	{
		return ItemPrefab.ItemPrefabManager.GetItemById(GetRelatedGameId(itemModifier));
	}

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
		itemModifier.Level = Random.Range(1f, 3f);
		HashSet<GameId> fishIds;
		using (FishingLocationDataManager.GetAllPossibleFishes(out fishIds))
		{
			itemModifier.SetStringSpecialVariable("FishId", fishIds.GetRandom().ToShortString());
		}
	}

	public override float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value)
	{
		if (target == ItemModifierDirectItemChangeFloat.Price)
		{
			return value * (1f + itemModifier.GetQuadraticBoostFromLevel(0.6f));
		}
		return value;
	}
}
