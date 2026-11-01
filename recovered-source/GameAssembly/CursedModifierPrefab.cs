using UnityEngine;

public class CursedModifierPrefab : DefaultItemModifierPrefabBase
{
	private static Color32[] ThisColorArray = new Color32[3]
	{
		MyColor32Presets.Black,
		new Color32(63, 253, 136, 255),
		new Color32(105, 253, 163, 255)
	};

	public const string CursedByVariable = "cursedBy";

	public override bool IsValidGameStatModifier => base.IsValidGameStatModifier;

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
		return LOC.UI.ItemModifierCursed;
	}

	public override string GetModifierDescription(ItemModifier itemModifier)
	{
		return LOC.UI.ItemModifierCursedDescription(itemModifier.GetStringSpecialVariableOrDefault("cursedBy", "Sheep"));
	}

	public override ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier)
	{
		return ItemModifierRarityEnum.Common;
	}

	public override void ApplyAsGameStatModifier(ItemModifier itemModifier, GameVariables gameVariables, GameStats gameStats)
	{
		gameStats.CommonGameStats.LuckBoost.Percent -= 0.4f;
	}

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
		itemModifier.Level = 1f;
		if (Random.value > 0.9f)
		{
			itemModifier.SetStringSpecialVariable("cursedBy", "Sheep");
		}
		else
		{
			itemModifier.SetStringSpecialVariable("cursedBy", HellWebsite.GenerateLovecraftianName());
		}
	}

	public override float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value)
	{
		if (target == ItemModifierDirectItemChangeFloat.Price)
		{
			return value * 0.5f;
		}
		return value;
	}
}
