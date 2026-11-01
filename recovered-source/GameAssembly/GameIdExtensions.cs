using GameItems;

public static class GameIdExtensions
{
	public static GameId ToGameId(this ItemEnum itemEnum)
	{
		return GameId.CreateVanilla(itemEnum);
	}

	public static GameId ToGameId(this CookingSongEnumGI cookingSongEnum)
	{
		return GameId.CreateVanilla(cookingSongEnum);
	}

	public static GameId ToGameId(this FishingLocationEnumGI fishingSongEnum)
	{
		return GameId.CreateVanilla(fishingSongEnum);
	}

	public static GameId ToGameId(this ItemModifierEnumGI itemModifierEnum)
	{
		return GameId.CreateVanilla(itemModifierEnum);
	}
}
