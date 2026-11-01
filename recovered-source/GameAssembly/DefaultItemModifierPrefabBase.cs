using System.Collections.Generic;
using System.Text;

public abstract class DefaultItemModifierPrefabBase : ItemModifierPrefab
{
	public override bool AllowedMultiple => false;

	public override int DefaultGameStatModifierOrder => 1000;

	public override bool IsValidGameStatModifier => false;

	public override void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity)
	{
	}

	public override float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value)
	{
		return value;
	}

	public override void DirectModifierStringBuilder(ItemModifier itemModifier, ItemModifierDirectItemChangeStringBuilder target, StringBuilder stringBuilder)
	{
	}

	public override void ApplyAsGameStatModifier(ItemModifier itemModifier, GameVariables gameVariables, GameStats gameStats)
	{
	}

	public override void FillAdditionalGameStatModifiers(ItemModifier itemModifier, List<IGameStatModifier> gameStatModifiers)
	{
	}
}
