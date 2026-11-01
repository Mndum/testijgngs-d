using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StockCompany : SerializedPart<string, StockCompanyPrefab>, ISerializationCallbackReceiver
{
	[SerializeField]
	public int OwnedStocksCount;

	[SerializeField]
	public int OwnedStocksCost;

	[SerializeField]
	private List<float> priceMemorySerialize;

	public const int MaxOwnedStockCountSingle = 25000;

	[field: SerializeField]
	public string Name { get; private set; }

	[field: SerializeField]
	public float TargetPrice { get; private set; }

	public LinkedList<float> PriceMemory { get; private set; }

	public override string Key => Name;

	public StockCompany()
	{
	}

	public StockCompany(string name, StockCompanyPrefab prefab)
	{
		Name = name;
		PriceMemory = new LinkedList<float>();
		OwnedStocksCount = 0;
		OwnedStocksCost = 0;
		InitPrefab(prefab);
		TargetPrice = prefab.StartingPrice;
		AddMemory(prefab.StartingPrice);
	}

	public sealed override void InitPrefab(StockCompanyPrefab prefab)
	{
		base.InitPrefab(prefab);
		if (TargetPrice < prefab.MinPrice)
		{
			TargetPrice = prefab.MinPrice;
		}
	}

	public void OnBeforeSerialize()
	{
		if (priceMemorySerialize == null)
		{
			priceMemorySerialize = new List<float>();
		}
		else
		{
			priceMemorySerialize.Clear();
		}
		priceMemorySerialize = PriceMemory.ToList();
	}

	public void OnAfterDeserialize()
	{
		PriceMemory = new LinkedList<float>(priceMemorySerialize);
		if (PriceMemory.Count == 0)
		{
			PriceMemory.AddLast(1f);
		}
	}

	public OwnedStockInfo GetOwnedStockInfo()
	{
		return new OwnedStockInfo
		{
			count = OwnedStocksCount,
			sum = OwnedStocksCost
		};
	}

	public bool SellStock(int count)
	{
		if (count <= OwnedStocksCount)
		{
			GameVariables.Current.AddMoney(count * CurrentPriceRounded());
			float num = (float)OwnedStocksCost / (float)OwnedStocksCount;
			OwnedStocksCost -= Mathf.RoundToInt(num * (float)count);
			if (OwnedStocksCost < 0)
			{
				OwnedStocksCost = 0;
			}
			OwnedStocksCount -= count;
			return true;
		}
		return false;
	}

	public void SellAllStock()
	{
		SellStock(OwnedStocksCount);
	}

	public bool BuyStock(int count)
	{
		int num = count * CurrentPriceRounded();
		if (SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.TryTakeMoney(num))
		{
			OwnedStocksCount += count;
			OwnedStocksCost += num;
			return true;
		}
		return false;
	}

	public int CurrentPriceRounded()
	{
		return Mathf.Max(Mathf.RoundToInt(Enumerable.Last(PriceMemory)), 1);
	}

	public void CalculatePossibleToBuyCount(int money, out int possibleToBuyMoney, out int possibleToBuyGeneral)
	{
		int num = CurrentPriceRounded();
		possibleToBuyMoney = money / num;
		int b = 25000 - OwnedStocksCount;
		possibleToBuyGeneral = Mathf.Min(possibleToBuyMoney, b);
	}

	private void AddMemory(float price)
	{
		if (PriceMemory.Count >= 96)
		{
			LinkedListNode<float> first = PriceMemory.First;
			PriceMemory.RemoveFirst();
			first.Value = price;
			PriceMemory.AddLast(first);
		}
		else
		{
			PriceMemory.AddLast(price);
		}
	}

	public void Sync()
	{
		float value = PriceMemory.Last.Value;
		float instability = Prefab.Instability;
		float minPrice = Prefab.MinPrice;
		value += UnityEngine.Random.Range((0f - instability) * 0.25f, instability * 0.25f);
		value = ((!(value < TargetPrice)) ? (value + UnityEngine.Random.Range((0f - instability) * 0.8f, instability * 0.2f)) : (value + UnityEngine.Random.Range((0f - instability) * 0.2f, instability * 0.8f)));
		if (value < 1f)
		{
			value = 1f;
		}
		else if (value < minPrice)
		{
			value = (minPrice * 2f + value) / 3f;
		}
		if (value > 9999f)
		{
			value = 9999f;
		}
		AddMemory(value);
		float num = 1f - Mathf.Abs(TargetPrice - value) / (TargetPrice + value);
		if (UnityEngine.Random.value <= num * 0.1f)
		{
			TargetPrice += UnityEngine.Random.Range((0f - instability) * 5f, instability * 5f);
			if (TargetPrice < 1f)
			{
				TargetPrice = UnityEngine.Random.Range(1f, 1f + instability * 2f);
			}
			else if (TargetPrice < minPrice)
			{
				TargetPrice = (minPrice * 2f + TargetPrice) / 3f;
			}
			if (TargetPrice > 9999f)
			{
				TargetPrice = UnityEngine.Random.Range(Prefab.StartingPrice, 9999f);
			}
		}
	}
}
