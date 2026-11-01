using System;
using UnityEngine;

[Serializable]
public class ShopItem
{
	public enum ShopItemType
	{
		SingleBuy,
		Generic
	}

	[SerializeField]
	private Item _item;

	[SerializeField]
	private ShopItemType _itemType;

	public Item Item => _item;

	public ShopItemType ItemType => _itemType;

	private ShopItem()
	{
	}

	public ShopItem(Item item, ShopItemType itemType)
	{
		_item = item;
		_itemType = itemType;
	}

	public Item CloneTargetItem(int count = 1)
	{
		Item item = _item.Clone();
		item.SetCount(count);
		return item;
	}
}
