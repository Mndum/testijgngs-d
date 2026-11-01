using System;
using UnityEngine;

public class ShopPopup : PrefabPopup<ShopPopup>
{
	[SerializeField]
	private GenericShop genericShop;

	public GenericShop GenericShop => genericShop;

	public void Open(string shopId, Action onDisableAction)
	{
		base.Open();
		genericShop.Open(shopId);
		genericShop.OnDisableAction = onDisableAction;
	}
}
