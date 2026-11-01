using System;
using UnityEngine;

[Serializable]
public class ItemRepairOrder
{
	public Item RepairedItem;

	public int OrderStartedTime;

	public const float MinRepairAmount = 0.2f;

	public const float MaxRepairAmount = 0.4f;

	public const float Discount = 0.4f;

	public const int MinPrice = 500;

	public ItemRepairOrder()
	{
	}

	public ItemRepairOrder(Item repairedItem, int orderStartedTime)
	{
		RepairedItem = repairedItem;
		OrderStartedTime = orderStartedTime;
	}

	public bool IsDone()
	{
		int num = GameVariables.TimeToDay(OrderStartedTime);
		return GameVariables.Current.Day > num;
	}

	public void ApplyRepair()
	{
		RepairedItem.Repair(GetRandomRepairAmount());
	}

	public static float GetRandomRepairAmount()
	{
		return UnityEngine.Random.Range(0.2f, 0.4f);
	}

	public static int CalculateRepairPrice(Item item)
	{
		return Mathf.Max(Mathf.RoundToInt(item.PriceChangeByRepairAmount(0.2f) * 0.4f + 100f), 500);
	}
}
