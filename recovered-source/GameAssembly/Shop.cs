using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Shop : SerializedPart<string, ShopPrefab>
{
	[SerializeField]
	private List<ShopItem> _shopItems = new List<ShopItem>();

	[SerializeField]
	private SerializableDictionary<GameId, int> _rngCompensationData = new SerializableDictionary<GameId, int>();

	public IEnumerable<ShopItem> ShopItems => _shopItems.Concat(Prefab.GetGenericShopItems());

	public string Name => Prefab.Name;

	[field: SerializeField]
	public int MoneySpent { get; set; }

	[field: SerializeField]
	public int ItemsBought { get; set; }

	public override string Key => Id;

	[field: SerializeField]
	public string Id { get; set; }

	public Shop()
	{
	}

	public Shop(string id, ShopPrefab prefab)
	{
		InitPrefab(prefab);
		Id = id;
	}

	public void Clear()
	{
		_shopItems.Clear();
	}

	public int GetPrice(ShopItem item, int count = 1)
	{
		return Prefab.PriceCalculateFunc(item, count);
	}

	public void AddItem(ShopItem item)
	{
		_shopItems.Add(item);
		AddCompensationData(item.Item.GameId);
	}

	public bool TryGetItemByGuid(Guid originalId, out ShopItem shopItemOut)
	{
		foreach (ShopItem shopItem in _shopItems)
		{
			if (shopItem.Item.GetUniqueGuid() == originalId)
			{
				shopItemOut = shopItem;
				return true;
			}
		}
		shopItemOut = null;
		return false;
	}

	private void AddCompensationData(GameId gameId)
	{
		if (_rngCompensationData.TryGetValue(gameId, out var value))
		{
			_rngCompensationData[gameId] = value + 1;
		}
		else
		{
			_rngCompensationData[gameId] = 2;
		}
	}

	public int GetCompensationData(GameId gameId)
	{
		if (_rngCompensationData.TryGetValue(gameId, out var value))
		{
			if (value >= 1)
			{
				return value;
			}
			_rngCompensationData[gameId] = 1;
			return 1;
		}
		return 1;
	}

	public void ShuffleShop()
	{
		Prefab.ShuffleSingleBuyItems(this, _shopItems);
	}

	public void ShowBuyPopup(ShopItem shopItem, int count, Action<int> OnPaid, Action OnNotBuy = null, Action OnNoMoney = null)
	{
		int price = GetPrice(shopItem, count);
		UiOverlay.Instance.BuyPopup(LOC.UI.UI_AreYouSure, price, () =>
		{
			OnPaid?.Invoke(price);
		}, null, OnNotBuy, OnNoMoney);
	}

	public Item BuyItem(ShopItem shopItem, int count, int pricePaid)
	{
		R.Sound.Money_All.GetRandom().Get().PlaySlightlyRandomVolume();
		ItemsBought += count;
		MoneySpent += pricePaid;
		if (shopItem.ItemType == ShopItem.ShopItemType.SingleBuy)
		{
			int num = _shopItems.IndexOf(shopItem);
			if (num >= 0)
			{
				if (!shopItem.Item.IsStackable || shopItem.Item.SetCount(shopItem.Item.GetCount() - count))
				{
					_shopItems.RemoveAt(num);
				}
			}
			else
			{
				Debug.LogError("Buying item that doesn't exist in _shopItems!");
			}
		}
		return shopItem.CloneTargetItem(count);
	}
}
