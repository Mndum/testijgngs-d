using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameItems;
using TMPro;
using UnityEngine;

public class RealMenFishingScript : Website
{
	[SerializeField]
	private TextMeshProUGUI _tipsText;

	[SerializeField]
	private GenericShop _genericShop;

	private void SyncTips()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.TMPAppendHeaderSizeTag();
			stringBuilder.AppendLine(LOC.UI.FishingWebsite_TipsFromThePros);
			stringBuilder.TMPAppendCloseSizeTag();
			GameVariables current = GameVariables.Current;
			System.Random random = new System.Random(current.fishingMinigameManager.FishingTipSeed);
			FishingGameStats fishingGameStats = new FishingGameStats();
			Item fishingRod = Item.CreateRandomItem(ItemEnum.BasicFishingRod.ToGameId(), allowModifiers: false);
			fishingGameStats.SetFishingRod(fishingRod);
			List<FishingLocationData> list;
			using (FishingLocationDataManager.GetAllUnlockedLocations().ToListPooled(out list))
			{
				if (list.Count == 0)
				{
					stringBuilder.AppendLine(LOC.UI.FishingWebsite_NoTips);
				}
				else
				{
					FishingLocationData fishingLocationData = list.ElementAt(random.Next(0, list.Count));
					IReadOnlyCollection<FishingCatchEntry> possibleCatches = fishingLocationData.PossibleCatches;
					FishingCatchEntry fishingCatchEntry = possibleCatches.ElementAt(random.Next(0, possibleCatches.Count));
					Lottery<(GameId, ItemPrefab)> lottery = new Lottery<(GameId, ItemPrefab)>();
					int hour = current.Hour;
					foreach (var allNonDefaultItem in ItemPrefab.ItemPrefabManager.AllNonDefaultItems)
					{
						GameId item = allNonDefaultItem.id;
						ItemPrefab item2 = allNonDefaultItem.prefab;
						FishingBaitItemPrefabData fishingBaitItemPrefabData = item2.FishingBaitItemPrefabData;
						if (fishingBaitItemPrefabData != null)
						{
							Item fishingBaitItem = Item.CreateRandomItem(item);
							fishingGameStats.SetBait(fishingBaitItem, fishingBaitItemPrefabData);
							if (fishingCatchEntry.CalculateOdds(fishingGameStats, hour, out var odds) && odds > 0.3f)
							{
								lottery.AddItem((item, item2), odds * odds * odds);
							}
						}
					}
					if (lottery.TryGetRandom(out var result, random))
					{
						stringBuilder.AppendLine(LOC.UI.FishingWebsite_Tip1(fishingCatchEntry.Describe(), fishingLocationData.Name, result.Item2.Name));
					}
					else
					{
						stringBuilder.AppendLine(LOC.UI.FishingWebsite_NoTips);
					}
				}
				_tipsText.SetText(stringBuilder);
			}
		}
	}

	public void SyncWebsite()
	{
		SyncTips();
		List<Folder<Item>> list = MyCollectionPool<Folder<Item>>.GetList();
		list.Add(new Folder<Item>(() => LOC.UI.FishingFishingRods, (Item item) => item.CanGoInto(ItemSlot.SlotManager.Anon_FishingRod)));
		list.Add(new Folder<Item>(() => LOC.UI.FishingBait, (Item item) => item.CanGoInto(ItemSlot.SlotManager.Anon_FishingBait)));
		list.Add(new Folder<Item>(() => LOC.UI.Equipment_Other, (Item item) => !item.CanGoInto(ItemSlot.SlotManager.Anon_FishingRod) && !item.CanGoInto(ItemSlot.SlotManager.Anon_FishingBait), hideIfEmpty: true));
		_genericShop.Open("realmenfishing", list);
		_genericShop.BuyConfirmation = (ShopItem shopItem, int count) =>
		{
			Shop shop = GameVariables.Current.ShopManager.GetShop(_genericShop.ShopId);
			shop.ShowBuyPopup(shopItem, count, (int pricePaid) =>
			{
				Item item = shop.BuyItem(shopItem, count, pricePaid);
				DeliveryManager.Delivery delivery = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.deliveryManager.CreateDelivery(1440, DeliveryManager.FishingWebsiteSender, item);
				_genericShop.UpdateAll();
				UiOverlay.Instance.OkPopup(LOC.UI.UI_Success, LOC.UI.Deliveries_DeliverySentPrompt(delivery.TrackingNumber, delivery.TimeSummary()));
			});
		};
	}

	public override IEnumerator Open()
	{
		SyncWebsite();
		DisableAllChildren();
		yield return WaitForSecondCache.Seconds0_1;
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
