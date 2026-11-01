using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FedUpScript : Website
{
	[SerializeField]
	private GameObject deliveiresListGO;

	[SerializeField]
	private ItemList deliveriesList;

	[SerializeField]
	private GameObject noDeliveriesGO;

	private void DeliveryClicked(int i)
	{
		GameVariables gv = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		if (gv.deliveryManager.Deliveries.TryGetValue(i, out var delivery) && !delivery.PremiumDeliveryBought)
		{
			UiOverlay.Instance.BuyPopup(LOC.UI.Delivery_PremiumDelivery, delivery.PremiumDeliveryPrice, () =>
			{
				delivery.AcquirePremiumDelivery(gv.time);
				gv.deliveryManager.TryJoiningDelivery(delivery);
				SyncWebsite();
			}, LOC.UI.Deliveries_BuyPremiumPrompt(delivery.TrackingNumber, delivery.PremiumDeliveryPrice));
		}
	}

	public void SyncWebsite()
	{
		bool flag = deliveriesList.GetItemCount() > 0;
		noDeliveriesGO.SetActive(!flag);
		deliveriesList.gameObject.SetActive(flag);
		if (flag)
		{
			deliveriesList.UpdateAll();
		}
	}

	public override IEnumerator Open()
	{
		deliveriesList.ItemCountFunc = () => SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.deliveryManager.Deliveries.Count;
		deliveriesList.ButtonFindFunc = null;
		deliveriesList.ItemClickedAction = DeliveryClicked;
		deliveriesList.ItemUpdatedAction = (GameObject tmp, int i, bool focused) =>
		{
			DeliveryManager.Delivery delivery = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.deliveryManager.Deliveries[i];
			tmp.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = delivery.LongSummary();
			TextMeshProUGUI component = tmp.transform.Find("DeliveryType").GetComponent<TextMeshProUGUI>();
			component.text = delivery.DeliveryTypeSummary();
			component.GetComponent<Button>().interactable = !delivery.PremiumDeliveryBought;
		};
		SyncWebsite();
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
