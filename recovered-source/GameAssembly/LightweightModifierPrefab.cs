using UnityEngine;

public class LightweightModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[1] { MyColor32Presets.White };

	public override Color32[] GetModifierColors(ItemModifier itemModifier)
	{
		return ThisColorArray;
	}

	public override string GetModifierChar(ItemModifier itemModifier)
	{
		return "L";
	}

	public override string GetModifierName(ItemModifier itemModifier)
	{
		return LOC.UI.ItemModifier_Lightweight;
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.ItemModifier_LightweightDescription;
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Uncommon;
	}

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
		itemModifier.Level = Random.Range(1f, 3f);
	}

	public override float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value)
	{
		return target switch
		{
			ItemModifierDirectItemChangeFloat.MaxEffectiveFishWeightKg => value * (1f - itemModifier.GetLinearBoostFromLevel(0.1f)), 
			ItemModifierDirectItemChangeFloat.FishingRodAcceleration01 => value * (1f + itemModifier.GetLinearBoostFromLevel(0.05f)), 
			ItemModifierDirectItemChangeFloat.FishingRodSize01 => value * (1f - itemModifier.GetLinearBoostFromLevel(0.05f)), 
			ItemModifierDirectItemChangeFloat.FishingRodReelingSpeed01 => value * (1f + itemModifier.GetLinearBoostFromLevel(0.1f)), 
			ItemModifierDirectItemChangeFloat.Price => value * (1f + itemModifier.GetQuadraticBoostFromLevel(0.3f)), 
			_ => value, 
		};
	}
}
