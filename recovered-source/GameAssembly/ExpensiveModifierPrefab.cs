using UnityEngine;

public class ExpensiveModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[1] { MyColor32Presets.Gold };

	public override Color32[] GetModifierColors(ItemModifier itemModifier)
	{
		return ThisColorArray;
	}

	public override string GetModifierChar(ItemModifier itemModifier)
	{
		return "$";
	}

	public override string GetModifierName(ItemModifier itemModifier)
	{
		return LOC.UI.ExpensiveItemModifier;
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.ExpensiveItemModifierDescription;
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Common;
	}

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
		itemModifier.Level = Mathf.Lerp(1f, 6f, expectedRarity);
	}

	public override float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value)
	{
		if (target == ItemModifierDirectItemChangeFloat.Price)
		{
			float quadraticBoostFromLevel = itemModifier.GetQuadraticBoostFromLevel(0.2f);
			return (500f + value) * (1f + quadraticBoostFromLevel);
		}
		return value;
	}
}
