using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AllowedModifiers : LuaUtilities.ILuaTransferable
{
	public class Builder
	{
		private Dictionary<GameId, float> _itemModifierLottery = new Dictionary<GameId, float>();

		private float _noModifierOdds = 0.6f;

		private float _multipleModifierOdds = 0.01f;

		private int _maxModifierCount = 2;

		public Builder WithItemModifiers(Dictionary<GameId, float> lottery)
		{
			_itemModifierLottery = lottery;
			return this;
		}

		public Builder WithNoModifierOdds(float odds)
		{
			_noModifierOdds = odds;
			return this;
		}

		public Builder WithMultipleModifierOdds(float odds)
		{
			_multipleModifierOdds = odds;
			return this;
		}

		public Builder WithMaxModifierCount(int count)
		{
			_maxModifierCount = count;
			return this;
		}

		public AllowedModifiers Build()
		{
			return new AllowedModifiers(_itemModifierLottery, _noModifierOdds, _multipleModifierOdds, _maxModifierCount);
		}
	}

	public static readonly AllowedModifiers FishingRodAllowedModifiers = CreateBuilder().WithItemModifiers(new Dictionary<GameId, float>
	{
		[ItemModifierEnumGI.Rare_Fast.ToGameId()] = 0.25f,
		[ItemModifierEnumGI.Rare_Wide.ToGameId()] = 0.25f,
		[ItemModifierEnumGI.Uncommon_Lightweight.ToGameId()] = 1f,
		[ItemModifierEnumGI.Uncommon_Precise.ToGameId()] = 1f,
		[ItemModifierEnumGI.Rare_ReinforcedLine.ToGameId()] = 0.25f,
		[ItemModifierEnumGI.Legendary_FishCalibrated.ToGameId()] = 0.05f
	}).Build();

	private static readonly TwoWayDictionary<CommonAllowedModifiersEnum, AllowedModifiers> EnumToModifierMapping = new TwoWayDictionary<CommonAllowedModifiersEnum, AllowedModifiers>((CommonAllowedModifiersEnum.FishingRod, FishingRodAllowedModifiers));

	private readonly Dictionary<GameId, float> _itemModifierLotteryDict;

	private readonly Lottery<GameId> _itemModifierLottery;

	private readonly float _noModifierOdds;

	private readonly float _multipleModifiersOdds;

	private readonly int _maxModifierCount;

	public int MaxModifierCountClamped => Mathf.Clamp(_maxModifierCount, 0, 5);

	[CanBeNull]
	public static AllowedModifiers GetCommon(CommonAllowedModifiersEnum commonAllowedModifiersEnum)
	{
		if (!EnumToModifierMapping.TryGet(commonAllowedModifiersEnum, out var value))
		{
			return null;
		}
		return value;
	}

	private AllowedModifiers(Dictionary<GameId, float> itemModifierLotteryDict, float noModifierOdds, float multipleModifierOdds, int maxModifierCount)
	{
		_itemModifierLotteryDict = itemModifierLotteryDict;
		_itemModifierLottery = new Lottery<GameId>(itemModifierLotteryDict);
		_noModifierOdds = noModifierOdds;
		_multipleModifiersOdds = multipleModifierOdds;
		_maxModifierCount = maxModifierCount;
	}

	public static Builder CreateBuilder()
	{
		return new Builder();
	}

	public void SetRandomModifiers(Item item, bool tryNoEmpty = false)
	{
		int maxModifierCountClamped = MaxModifierCountClamped;
		bool flag = !tryNoEmpty && Random.value < _noModifierOdds;
		if (((maxModifierCountClamped <= 0) | flag) || _itemModifierLottery.ItemCount == 0)
		{
			item.ClearItemModifiers();
			return;
		}
		HashSet<GameId> hashSet;
		using (MyCollectionPool<GameId>.GetHashSet(out hashSet))
		{
			for (int i = 0; i < maxModifierCountClamped; i++)
			{
				hashSet.Add(_itemModifierLottery.GetRandom());
				if (Random.value < _multipleModifiersOdds)
				{
					break;
				}
			}
			item.SetItemModifiers(hashSet);
		}
	}

	public string ToLuaString()
	{
		if (EnumToModifierMapping.TryGet(this, out var value) && value != CommonAllowedModifiersEnum.None)
		{
			return "AllowedModifiers.GetCommon(" + value.ToLuaString() + ")";
		}
		return "AllowedModifiers.CreateBuilder().WithItemModifiers(" + _itemModifierLotteryDict.ToLuaString((GameId x) => x.ToLuaString(), (float x) => x.ToLuaString()) + ").WithNoModifierOdds(" + _noModifierOdds.ToLuaString() + ").WithMultipleModifierOdds(" + _multipleModifiersOdds.ToLuaString() + ").WithMaxModifierCount(" + _maxModifierCount.ToLuaString() + ").Build()";
	}
}
