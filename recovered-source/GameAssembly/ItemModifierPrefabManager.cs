using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class ItemModifierPrefabManager
{
	private static Dictionary<GameId, ItemModifierPrefab> _modifierPrefabs = new Dictionary<GameId, ItemModifierPrefab>();

	public static ItemModifierPrefab GetItemModifierPrefab(GameId gameId)
	{
		if (_modifierPrefabs.TryGetValue(gameId, out var value))
		{
			return value;
		}
		return AddModifierPrefab(gameId, new EmptyItemModifierPrefab());
	}

	private static ItemModifierPrefab AddModifierPrefab(ItemModifierEnumGI itemModifier, ItemModifierPrefab itemModifierPrefab)
	{
		return AddModifierPrefab(GameId.CreateVanilla((int)itemModifier), itemModifierPrefab);
	}

	private static ItemModifierPrefab AddModifierPrefab(GameId gameId, ItemModifierPrefab itemModifierPrefab)
	{
		_modifierPrefabs[gameId] = itemModifierPrefab;
		itemModifierPrefab.SetGameId(gameId);
		return itemModifierPrefab;
	}

	public static GameId GetRandomModifier()
	{
		return _modifierPrefabs.Keys.ElementAt(Random.Range(0, _modifierPrefabs.Count));
	}

	[LoadableStaticMethod(null)]
	public static UniTask Load()
	{
		AddModifierPrefab(ItemModifierEnumGI.Expensive, new ExpensiveModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Popular, new PopularModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Streamers, new StreamersModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Cursed, new CursedModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Rare_Fast, new FastFishingModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Rare_Wide, new WideFishingModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Uncommon_Lightweight, new LightweightModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Uncommon_Precise, new PreciseModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Rare_ReinforcedLine, new ReinforcedLineModifierPrefab());
		AddModifierPrefab(ItemModifierEnumGI.Legendary_FishCalibrated, new FishCalibratedModifierPrefab());
		return UniTask.CompletedTask;
	}
}
