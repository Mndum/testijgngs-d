using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class DeliveryManager
{
	[Serializable]
	public class SerializableDeliveryData
	{
		public int timeTillDelivery;

		public bool hasSender;

		public SimpleLocalizedString sender;

		public Item[] items;

		public SimpleLocalizedString Sender
		{
			get
			{
				if (!hasSender)
				{
					return null;
				}
				return sender;
			}
		}

		public SerializableDeliveryData(int timeTillDelivery, SimpleLocalizedString sender, params Item[] items)
		{
			this.timeTillDelivery = timeTillDelivery;
			this.sender = sender;
			hasSender = sender != null;
			this.items = items;
		}
	}

	[Serializable]
	public class Delivery
	{
		public enum DeliveryStatus
		{
			ToBeCollected,
			ToBeSorted,
			ToBeDelivered,
			Delivered
		}

		public static string TitleColor = "#C0C0C0";

		public static string Size = "140%";

		[field: SerializeField]
		public string TrackingNumber { get; private set; }

		[field: SerializeField]
		public List<Item> DeliveryItems { get; private set; }

		[field: SerializeField]
		public int SentTime { get; private set; }

		[field: SerializeField]
		public int DeliveryDuration { get; private set; }

		[field: SerializeField]
		public bool PremiumDeliveryBought { get; private set; }

		public int DeliveryValue => DeliveryItems.Sum((Item t) => t.GetPrice());

		public int PremiumDeliveryPrice => Mathf.RoundToInt((float)DeliveryValue * 0.1f) + 420;

		public int ArrivalTime => SentTime + DeliveryDuration;

		[field: SerializeField]
		public SimpleLocalizedString Sender { get; private set; }

		public Delivery(string trackingNumber, Item[] deliveryItems, int sentTime, int deliveryDuration, SimpleLocalizedString sender)
		{
			TrackingNumber = trackingNumber;
			DeliveryItems = deliveryItems.ToList();
			SentTime = sentTime;
			DeliveryDuration = deliveryDuration;
			Sender = sender;
		}

		public bool CanBeJoined(int currentTime, Delivery secondDelivery)
		{
			if (PremiumDeliveryBought != secondDelivery.PremiumDeliveryBought)
			{
				return false;
			}
			DeliveryStatus deliveryStatus = Status(currentTime);
			if (deliveryStatus != secondDelivery.Status(currentTime))
			{
				return false;
			}
			if (deliveryStatus == DeliveryStatus.ToBeCollected)
			{
				if (Sender == secondDelivery.Sender)
				{
					return true;
				}
				if (Sender == MultipleSenders || secondDelivery.Sender == MultipleSenders)
				{
					return true;
				}
				return false;
			}
			return true;
		}

		public void JoinPackages(string newTrackingNumber, Delivery secondDelivery)
		{
			string trackingNumber = TrackingNumber;
			TrackingNumber = newTrackingNumber;
			DeliveryItems.AddRange(secondDelivery.DeliveryItems);
			SentTime = Mathf.Min(SentTime, secondDelivery.SentTime);
			DeliveryDuration = Mathf.RoundToInt((float)(DeliveryDuration + secondDelivery.DeliveryDuration) / 2f);
			if (Sender != secondDelivery.Sender)
			{
				Sender = MultipleSenders;
			}
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.deliveryManager.SendConsolidationEmail(trackingNumber, secondDelivery, this);
		}

		public void AcquirePremiumDelivery(int currentTime)
		{
			PremiumDeliveryBought = true;
			DeliveryDuration = Mathf.Min(currentTime + 15 - SentTime, DeliveryDuration);
		}

		public DeliveryStatus Status(int currentTime)
		{
			if (currentTime >= SentTime + DeliveryDuration)
			{
				return DeliveryStatus.Delivered;
			}
			if ((float)currentTime >= (float)SentTime + (float)DeliveryDuration * 0.3f)
			{
				return DeliveryStatus.ToBeDelivered;
			}
			if ((float)currentTime >= (float)SentTime + (float)DeliveryDuration * 0.1f)
			{
				return DeliveryStatus.ToBeSorted;
			}
			return DeliveryStatus.ToBeCollected;
		}

		public int PredictedArrivalTime(float uncertainityPercentage)
		{
			UnityEngine.Random.State state = UnityEngine.Random.state;
			try
			{
				UnityEngine.Random.InitState(SentTime);
				float num = DeliveryDuration;
				num *= UnityEngine.Random.Range(1f - uncertainityPercentage, 1f + uncertainityPercentage);
				if (num <= 0f)
				{
					num = 0f;
				}
				return SentTime + Mathf.RoundToInt(num);
			}
			finally
			{
				UnityEngine.Random.state = state;
			}
		}

		public bool HasArrived(int time)
		{
			return ArrivalTime <= time;
		}

		public string ContentSummary()
		{
			return string.Join(", ", DeliveryItems.Select((Item t) => t.GetName())) + "\n" + LOC.UI.Delivery_ReportedPrice(DeliveryValue);
		}

		public string StatusSummary()
		{
			int time = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
			return Status(time) switch
			{
				DeliveryStatus.ToBeCollected => LOC.Other.Delivery_StatusToBeCollected, 
				DeliveryStatus.ToBeSorted => LOC.Other.Delivery_StatusToBeSorted, 
				DeliveryStatus.ToBeDelivered => LOC.Other.Delivery_DeliveryDispatched, 
				_ => LOC.Other.Delivery_DeliverySoon, 
			};
		}

		public string TimeSummary()
		{
			int time = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
			int num;
			switch (Status(time))
			{
			case DeliveryStatus.ToBeCollected:
				num = PredictedArrivalTime(0.4f);
				break;
			case DeliveryStatus.ToBeSorted:
				num = PredictedArrivalTime(0.2f);
				break;
			case DeliveryStatus.ToBeDelivered:
				num = ArrivalTime;
				break;
			default:
				return LOC.Other.Delivery_PredictedTimeSoon;
			}
			return LOC.Other.Delivery_PredictedTime(GlobalFunctions.TimePeriod(num - time), GameVariables.TimeToDateString(num));
		}

		public string LongSummary()
		{
			return "<color=" + TitleColor + ">" + LOC.UI.Delivery_NumberTitle + "</color>\n<size=" + Size + ">#" + TrackingNumber + "</size>\n<color=" + TitleColor + ">" + LOC.UI.Delivery_StatusTitle + "</color>\n<size=" + Size + ">" + StatusSummary() + "</size>\n<color=" + TitleColor + ">" + LOC.UI.Delivery_ArrivalTimeTitle + "</color>\n<size=" + Size + ">" + TimeSummary() + "</size>\n<color=" + TitleColor + ">" + LOC.UI.Delivery_SenderTitle + "</color>\n<size=" + Size + ">" + Sender.Value + "</size>\n<color=" + TitleColor + ">" + LOC.UI.Delivery_ContentsTitle + "</color>\n" + ContentSummary();
		}

		public string DeliveryTypeSummary()
		{
			string text = "<color=" + TitleColor + ">" + LOC.UI.Delivery_DeliveryTypeTitle + "</color><size=" + Size + ">\n";
			if (PremiumDeliveryBought)
			{
				return text + "<color=#D2AF26>" + LOC.UI.Delivery_PremiumDelivery + "</color>";
			}
			return text + "<size=" + Size + ">" + LOC.UI.Delivery_StandardDelivery + "\n<b><color=#D2AF26>" + LOC.UI.Delivery_BuyPremium;
		}
	}

	public static readonly SimpleLocalizedString FishingWebsiteSender = LOC.Other.TupleReferences.Sender_FishingWebsite;

	public static readonly SimpleLocalizedString UnknownSender = LOC.Other.TupleReferences.Sender_Unknown;

	public static readonly SimpleLocalizedString MultipleSenders = LOC.Other.TupleReferences.Sender_Multiple;

	public static readonly SimpleLocalizedString BDHSender = LOC.Other.TupleReferences.Sender_BigDaddyHurt;

	[field: SerializeField]
	public List<Delivery> Deliveries { get; private set; } = new List<Delivery>();

	private void SendNotificationEmail(Delivery delivery)
	{
		NewEmail email = EmailPrefabManager.GetEmail(EmailPrefabManager.FedUpNewDeliveryEmail, new List<string>
		{
			delivery.TrackingNumber,
			delivery.Sender.ToBakedArgument()
		});
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddEmail(email);
	}

	private void SendConsolidationEmail(string dt1, Delivery d2, Delivery newDelivery)
	{
		NewEmail email = EmailPrefabManager.GetEmail(EmailPrefabManager.FedUpConsolidationEmail, new List<string> { dt1, d2.TrackingNumber, newDelivery.TrackingNumber });
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.AddEmail(email);
	}

	public Delivery CreateDelivery(SerializableDeliveryData serializableDeliveryData)
	{
		return CreateDelivery(serializableDeliveryData.timeTillDelivery, serializableDeliveryData.Sender, serializableDeliveryData.items);
	}

	public Delivery CreateDelivery(int timeTillDelivery, SimpleLocalizedString sender = null, params Item[] contents)
	{
		if (contents.Length == 0)
		{
			return null;
		}
		if (sender == null)
		{
			sender = UnknownSender;
		}
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		gameVariables.AddFlag("firstdelivery");
		int time = gameVariables.time;
		Delivery delivery = new Delivery(GenerateTrackingNumber(), contents, time, timeTillDelivery, sender);
		SendNotificationEmail(delivery);
		foreach (Delivery delivery2 in Deliveries)
		{
			if (delivery2.Status(time) == Delivery.DeliveryStatus.ToBeCollected && delivery2.Sender == sender)
			{
				delivery2.JoinPackages(GenerateTrackingNumber(), delivery);
				return delivery2;
			}
		}
		Deliveries.Add(delivery);
		return delivery;
	}

	public void TryJoiningDelivery(Delivery mainDelivery)
	{
		int time = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		for (int i = 0; i < Deliveries.Count; i++)
		{
			Delivery delivery = Deliveries[i];
			if (delivery != mainDelivery && delivery.CanBeJoined(time, mainDelivery))
			{
				delivery.JoinPackages(GenerateTrackingNumber(), mainDelivery);
				Deliveries.Remove(mainDelivery);
				break;
			}
		}
	}

	public bool UpdateDeliveries()
	{
		GameScript instance = SingletonLoadableMonoBehaviour<GameScript>.Instance;
		int time = instance.GameVariables.time;
		foreach (Delivery delivery in Deliveries)
		{
			if (!delivery.HasArrived(time))
			{
				continue;
			}
			List<Item> list = new List<Item>();
			for (int num = Deliveries.Count - 1; num >= 0; num--)
			{
				if (Deliveries[num].HasArrived(time))
				{
					list.AddRange(Deliveries[num].DeliveryItems);
					Deliveries.RemoveAt(num);
				}
			}
			foreach (Item item in list)
			{
				instance.GameVariables.itemManager.AddItem(item);
			}
			instance.FungusSetCustomText(string.Join(", ", list.Select((Item t) => t.GetName())));
			instance.ChangeToStoryStateBlockReference(FungusBlocks.DroneDelivery);
			return true;
		}
		return true;
	}

	private string GenerateTrackingNumberInternal()
	{
		int num = 16;
		int num2 = 3;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < num2; i++)
			{
				stringBuilder.Append(StringUtils.RandomLetter());
			}
			for (int j = 0; j < num - num2; j++)
			{
				stringBuilder.Append(StringUtils.RandomDigit());
			}
			return stringBuilder.ToString();
		}
	}

	public string GenerateTrackingNumber()
	{
		string newTrackingNumber = GenerateTrackingNumberInternal();
		for (int i = 0; i < Deliveries.Count; i++)
		{
			newTrackingNumber = GenerateTrackingNumberInternal();
			if (Deliveries.All((Delivery t) => t.TrackingNumber != newTrackingNumber))
			{
				return newTrackingNumber;
			}
		}
		return newTrackingNumber;
	}
}
