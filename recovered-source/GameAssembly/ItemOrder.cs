using System;

[Serializable]
public class ItemOrder
{
	public enum OrderStatus
	{
		InCreation,
		Created,
		Done
	}

	public Item Item;

	public int OrderFinishedTime;

	public OrderStatus Status;

	public ItemOrder()
	{
	}

	public ItemOrder(Item item)
	{
		Item = item;
		Status = OrderStatus.InCreation;
	}

	public void CreateOrder(int time)
	{
		if (Status == OrderStatus.InCreation)
		{
			Status = OrderStatus.Created;
			OrderFinishedTime = time + SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		}
	}

	public OrderStatus GetStatusConsideringTime()
	{
		switch (Status)
		{
		case OrderStatus.InCreation:
			return OrderStatus.InCreation;
		case OrderStatus.Created:
			if (OrderFinishedTime <= SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time)
			{
				return OrderStatus.Done;
			}
			return OrderStatus.Created;
		default:
			return OrderStatus.Done;
		}
	}
}
