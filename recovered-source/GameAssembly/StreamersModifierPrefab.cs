using UnityEngine;

public class StreamersModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[2]
	{
		new Color32(234, 15, 201, 255),
		new Color32(11, 128, 251, 255)
	};

	public override bool IsValidGameStatModifier => true;

	public override Color32[] GetModifierColors(ItemModifier itemModifier)
	{
		return ThisColorArray;
	}

	public override string GetModifierChar(ItemModifier itemModifier)
	{
		return "S";
	}

	public override string GetModifierName(ItemModifier itemModifier)
	{
		return LOC.UI.StreamersItemModifier;
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.StreamersItemModifierDescription;
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Rare;
	}

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
		itemModifier.Level = Mathf.Lerp(1f, 6f, expectedRarity);
	}

	public override void ApplyAsGameStatModifier(ItemModifier itemModifier, GameVariables gameVariables, GameStats gameStats)
	{
		gameStats.StreamGameStats.streamViewerBoostPercent.Percent += itemModifier.GetLinearBoostFromLevel(0.01f);
		gameStats.StreamGameStats.streamDonationBoostPercent.Percent += itemModifier.GetLinearBoostFromLevel(0.01f);
	}

	public override float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value)
	{
		if (target == ItemModifierDirectItemChangeFloat.Price)
		{
			return ItemModifierUtilities.ApplyBoostPercent(ItemModifierDirectItemChangeFloat.Price, value, itemModifier.GetQuadraticBoostFromLevel(0.5f));
		}
		return value;
	}
}
