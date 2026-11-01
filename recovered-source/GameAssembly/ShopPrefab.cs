using System;
using System.Collections.Generic;
using System.Linq;
using GameItems;
using UnityEngine;

public class ShopPrefab : PrefabPart<string>
{
	public class ShopItemContainer
	{
		public class ShopItemContainerEntryGameId
		{
			public GameId Id;

			public float OccurenceRate;

			public int TmpCompensationData;

			public ShopItemContainerEntryGameId(GameId id, float occurenceRate)
			{
				Id = id;
				OccurenceRate = occurenceRate;
			}
		}

		public class ShopItemContainerEntry
		{
			public List<ShopItemContainerEntryGameId> GameIdWithData;

			public Func<bool> Condition;

			public bool TmpPassedCondition;
		}

		private List<ShopItemContainerEntry> _items = new List<ShopItemContainerEntry>();

		public void AddItems(IEnumerable<ItemEnum> gameIds)
		{
			AddItems(null, 1f, gameIds.Select((ItemEnum t) => t.ToGameId()));
		}

		public void AddItems(IEnumerable<GameId> gameIds)
		{
			AddItems(null, 1f, gameIds);
		}

		public void AddItems(params ItemEnum[] gameIds)
		{
			AddItems(null, 1f, gameIds);
		}

		public void AddItems(Func<bool> condition = null, float occurrenceRate = 1f, params ItemEnum[] gameIds)
		{
			AddItems(condition, occurrenceRate, gameIds.Select((ItemEnum t) => t.ToGameId()).ToArray());
		}

		public void AddItems(Func<bool> condition, float occurrenceRate, IEnumerable<GameId> gameIds)
		{
			if (occurrenceRate <= 0f)
			{
				Debug.LogError("Game ids \"" + gameIds.ToStringBetterEnumerable() + "\" couldn't be added! occurenceRate must be above 0!");
				return;
			}
			List<ShopItemContainerEntryGameId> list = gameIds.Select((GameId t) => new ShopItemContainerEntryGameId(t, occurrenceRate)).ToList();
			if (list.Count == 0)
			{
				Debug.LogError("Game ids couldn't be added! It needs items!");
				return;
			}
			int num = _items.FindIndex((ShopItemContainerEntry t) => t.Condition == condition);
			if (num < 0)
			{
				ShopItemContainerEntry item = new ShopItemContainerEntry
				{
					GameIdWithData = list,
					Condition = condition
				};
				_items.Add(item);
			}
			else
			{
				_items[num].GameIdWithData.AddRange(list);
			}
		}

		public void AddItems(Func<bool> condition = null, float occurrenceRate = 1f, params GameId[] gameIds)
		{
			AddItems(condition, occurrenceRate, gameIds.AsEnumerable());
		}

		public void FillItemLottery(Shop shop, Lottery<GameId> proxyLottery)
		{
			proxyLottery.Clear();
			int num = 2147483647;
			foreach (ShopItemContainerEntry item in _items)
			{
				item.TmpPassedCondition = item.Condition == null || item.Condition();
				if (!item.TmpPassedCondition)
				{
					continue;
				}
				foreach (ShopItemContainerEntryGameId gameIdWithDatum in item.GameIdWithData)
				{
					int num2 = (gameIdWithDatum.TmpCompensationData = shop.GetCompensationData(gameIdWithDatum.Id));
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			if (num == 2147483647)
			{
				return;
			}
			num--;
			foreach (ShopItemContainerEntry item2 in _items)
			{
				if (!item2.TmpPassedCondition)
				{
					continue;
				}
				foreach (ShopItemContainerEntryGameId gameIdWithDatum2 in item2.GameIdWithData)
				{
					proxyLottery.AddItem(gameIdWithDatum2.Id, gameIdWithDatum2.OccurenceRate / (float)(gameIdWithDatum2.TmpCompensationData - num));
				}
			}
		}

		public List<GameId> GetGameIdsWithCheck()
		{
			return _items.Where((ShopItemContainerEntry entry) => entry.Condition?.Invoke() ?? true).SelectMany((ShopItemContainerEntry t) => t.GameIdWithData.Select((ShopItemContainerEntryGameId shopItemContainerEntryGameId) => shopItemContainerEntryGameId.Id)).ToList();
		}

		public List<ShopItem> GetItemsWithCheck()
		{
			return (from t in GetGameIdsWithCheck()
				select new ShopItem(new Item(t), ShopItem.ShopItemType.Generic)).ToList();
		}

		public bool Has(GameId id)
		{
			return _items.Any((ShopItemContainerEntry t) => t.GameIdWithData.Any((ShopItemContainerEntryGameId x) => x.Id == id));
		}
	}

	private static Lottery<GameId> _reusableLottery = new Lottery<GameId>();

	private SimpleLocalizedString _name;

	private int targetSingleBuyItemAmount;

	public string Name => _name.Value;

	public ShopItemContainer GenericContainer { get; private set; }

	public ShopItemContainer SingleContainer { get; private set; }

	public Func<ShopItem, int, int> PriceCalculateFunc { get; set; } = DefaultPriceCalculateFunc;

	public ShopPrefab(string key, string name)
		: base(key)
	{
		_name = SimpleLocalizedString.CreateInstanceReplacement(name);
		GenericContainer = new ShopItemContainer();
		SingleContainer = new ShopItemContainer();
		targetSingleBuyItemAmount = 0;
	}

	public ShopPrefab(string key, SimpleLocalizedString name, int targetSingleBuyItemAmount = 10)
		: base(key)
	{
		_name = name;
		GenericContainer = new ShopItemContainer();
		SingleContainer = new ShopItemContainer();
		this.targetSingleBuyItemAmount = targetSingleBuyItemAmount;
	}

	public List<ShopItem> GetGenericShopItems()
	{
		return GenericContainer.GetItemsWithCheck();
	}

	public void ShuffleSingleBuyItems(Shop shop, List<ShopItem> itemsInShop)
	{
		int num = UnityEngine.Random.Range(0, itemsInShop.Count / 2);
		for (int i = -2; i < num; i++)
		{
			if (itemsInShop.Count == 0)
			{
				break;
			}
			itemsInShop.RemoveAtSwapBack(UnityEngine.Random.Range(0, itemsInShop.Count));
		}
		int num2 = targetSingleBuyItemAmount - itemsInShop.Count;
		if (num2 > 0)
		{
			SingleContainer.FillItemLottery(shop, _reusableLottery);
			if (_reusableLottery.NotEmpty)
			{
				int num3 = targetSingleBuyItemAmount / 4;
				int num4 = Mathf.Max((num2 + UnityEngine.Random.Range(-num3, num3 + 1)) / 2, 1);
				for (int j = 0; j < num4; j++)
				{
					if (_reusableLottery.TryGetRandom(out var result))
					{
						shop.AddItem(new ShopItem(Item.CreateRandomItem(result), ShopItem.ShopItemType.SingleBuy));
					}
				}
			}
		}
		itemsInShop.RemoveAll((ShopItem t) => t == null);
	}

	public bool Has(GameId id)
	{
		if (!GenericContainer.Has(id))
		{
			return SingleContainer.Has(id);
		}
		return true;
	}

	public static int DefaultPriceCalculateFunc(ShopItem item, int count)
	{
		return item.Item.GetPrice() * count;
	}
}
