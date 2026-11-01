using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ShopManager : ISerializationCallbackReceiver
{
	[Serializable]
	public class ShopDictionary : SerializableDictionary<string, Shop>
	{
	}

	[Obsolete]
	[SerializeField]
	private ShopDictionary _dynamicShops = new ShopDictionary();

	[SerializeField]
	private List<Shop> _shops = new List<Shop>();

	private ShopManager()
	{
	}

	public static ShopManager CreateInstance()
	{
		ShopManager shopManager = new ShopManager();
		shopManager.FillPrefabData();
		return shopManager;
	}

	public bool TryGetShop(string name, out Shop shop)
	{
		int num = _shops.FindIndex((Shop x) => x.Key == name);
		if (num >= 0)
		{
			shop = _shops[num];
			return true;
		}
		shop = null;
		return false;
	}

	[UsedInLua]
	public Shop GetShop(string name)
	{
		TryGetShop(name, out var shop);
		return shop;
	}

	public List<ShopItem> GetItems(string name)
	{
		int num = _shops.FindIndex((Shop x) => x.Key == name);
		if (num >= 0)
		{
			return _shops[num].ShopItems.ToList();
		}
		return new List<ShopItem>();
	}

	public void ShuffleShops()
	{
		foreach (Shop shop in _shops)
		{
			shop.ShuffleShop();
		}
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		if (_dynamicShops.Count <= 0)
		{
			return;
		}
		foreach (KeyValuePair<string, Shop> dynamicShop in _dynamicShops)
		{
			dynamicShop.Value.Id = dynamicShop.Key;
			if (!TryGetShop(dynamicShop.Key, out var _))
			{
				_shops.Add(dynamicShop.Value);
			}
		}
		_dynamicShops.Clear();
	}

	public void FillPrefabData()
	{
		ShopPrefabManager.EnsureShops(_shops);
	}
}
