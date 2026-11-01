using UnityEngine;

public class FastFishingModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[1] { MyColor32Presets.LightBlue };

	public override Color32[] GetModifierColors(ItemModifier itemModifier)
	{
		return ThisColorArray;
	}

	public override string GetModifierChar(ItemModifier itemModifier)
	{
		return "F";
	}

	public override string GetModifierName(ItemModifier itemModifier)
	{
		return LOC.UI.ItemModifierFastFishing;
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.ItemModifierFastFishingDescription;
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Rare;
	}

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
		itemModifier.Level = Random.Range(1f, 3f);
	}

	public override float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value)
	{
		return target switch
		{
			ItemModifierDirectItemChangeFloat.FishingRodAcceleration01 => value * (1f + itemModifier.GetLinearBoostFromLevel(0.1f)), 
			ItemModifierDirectItemChangeFloat.Price => value * (1f + itemModifier.GetQuadraticBoostFromLevel(0.2f)), 
			_ => value, 
		};
	}
}
