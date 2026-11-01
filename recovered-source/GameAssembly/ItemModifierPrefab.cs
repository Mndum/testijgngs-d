using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class ItemModifierPrefab
{
	public GameId Id { get; private set; }

	public abstract bool AllowedMultiple { get; }

	public abstract bool IsValidGameStatModifier { get; }

	public abstract int DefaultGameStatModifierOrder { get; }

	public void SetGameId(GameId gameId)
	{
		Id = gameId;
	}

	public abstract string GetModifierChar(ItemModifier itemModifier);

	public abstract string GetModifierName(ItemModifier itemModifier);

	public abstract string GetModifierDescription(ItemModifier itemModifier);

	public abstract Color32[] GetModifierColors(ItemModifier itemModifier);

	public abstract ItemModifierRarityEnum GetModifierRarity(ItemModifier itemModifier);

	public abstract void FillCreatedItemModifier(ItemModifier itemModifier, float expectedRarity);

	public abstract float DirectModifierFloat(ItemModifier itemModifier, ItemModifierDirectItemChangeFloat target, float value);

	public abstract void DirectModifierStringBuilder(ItemModifier itemModifier, ItemModifierDirectItemChangeStringBuilder target, StringBuilder stringBuilder);

	public abstract void ApplyAsGameStatModifier(ItemModifier itemModifier, GameVariables gameVariables, GameStats gameStats);

	public abstract void FillAdditionalGameStatModifiers(ItemModifier itemModifier, List<IGameStatModifier> gameStatModifiers);
}
