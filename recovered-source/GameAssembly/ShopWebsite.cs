using System.Collections;
using System.Collections.Generic;
using GameItems;
using UnityEngine;

public class ShopWebsite : Website
{
	[SerializeField]
	private GenericShop genericShop;

	private string MusicId => GetType().Name;

	private void OnEnable()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.RegisterMusicOptions(new SoundSingleton.MusicOption(MusicId, SoundSingleton.MusicOption.MusicOptionLayer.Base, R.Music.PC_Exotic_Hardware.Get()));
	}

	private void OnDisable()
	{
		if (gameObject.scene.isLoaded)
		{
			SingletonMonoBehaviour<SoundSingleton>.Instance.UnregisterMusicOptionsById(MusicId, stopIfPlaying: false);
		}
	}

	public override IEnumerator Open()
	{
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
		List<Folder<Item>> list = MyCollectionPool<Folder<Item>>.GetList();
		list.Add(Folder<Item>.AllFolder);
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_HighQuality, (Item item) => item.GetItemPrefab().HasQuality && item.Quality >= 1f, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_PersonalityModules, (Item item) => item.GetItemPrefab().IsPersonalityModule, hideIfEmpty: true));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Under20k, (Item item) => item.GetPrice() < 20000, hideIfEmpty: true));
		genericShop.Open("ladyparts.ic", list);
		genericShop.BuyConfirmation = (ShopItem shopItem, int count) =>
		{
			Shop shop = GameVariables.Current.ShopManager.GetShop(genericShop.ShopId);
			shop.ShowBuyPopup(shopItem, count, (int pricePaid) =>
			{
				Item item = shop.BuyItem(shopItem, count, pricePaid);
				GameVariables current = GameVariables.Current;
				DeliveryManager.Delivery delivery;
				if (current.CheckFlag("bigDaddysAdvice"))
				{
					delivery = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.deliveryManager.CreateDelivery(1440, DeliveryManager.BDHSender, item, Item.CreateRandomItem(ItemEnum.LingerieCatalogue.ToGameId()));
					current.RemoveFlag("bigDaddysAdvice");
					current.AddFlag("bigDaddysAdviceBeforePart2");
				}
				else
				{
					delivery = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.deliveryManager.CreateDelivery(1440, DeliveryManager.BDHSender, item);
				}
				genericShop.UpdateAll();
				UiOverlay.Instance.OkPopup(LOC.UI.ladypartsdotic_PurchaseComplete, LOC.UI.Deliveries_DeliverySentPrompt(delivery.TrackingNumber, delivery.TimeSummary()), () =>
				{
				});
			});
		};
		DisableAllChildren();
		yield return WaitForSecondCache.Seconds0_1;
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
