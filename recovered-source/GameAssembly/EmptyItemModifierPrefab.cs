using UnityEngine;

public class EmptyItemModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[1] { MyColor32Presets.Black };

	public override string GetModifierChar(ItemModifier itemModifier)
	{
		return "E";
	}

	public override string GetModifierName(ItemModifier itemModifier)
	{
		return LOC.UI.UI_Error;
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.UI_Error;
	}

	public override Color32[] GetModifierColors(ItemModifier itemModifier)
	{
		return ThisColorArray;
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Common;
	}
}
