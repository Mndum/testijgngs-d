using UnityEngine;

public class PopularModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[1]
	{
		new Color32(11, 128, 251, 255)
	};

	public override bool IsValidGameStatModifier => true;

	public override Color32[] GetModifierColors(ItemModifier itemModifier)
	{
		return ThisColorArray;
	}

	public override string GetModifierChar(ItemModifier itemModifier)
	{
		return "P";
	}

	public override string GetModifierName(ItemModifier itemModifier)
	{
		return LOC.UI.PopularItemModifier;
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.PopularItemModifierDescription;
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Uncommon;
	}

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
		itemModifier.Level = Mathf.Lerp(1f, 6f, expectedRarity);
	}

	public override void ApplyAsGameStatModifier(ItemModifier itemModifier, GameVariables gameVariables, GameStats gameStats)
	{
		gameStats.StreamGameStats.streamViewerBoostPercent.Percent += itemModifier.GetLinearBoostFromLevel(0.01f);
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
