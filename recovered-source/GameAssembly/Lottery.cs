using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Lottery<T>
{
	public bool NotEmpty => LotteryItems.Any();

	public bool Empty => !LotteryItems.Any();

	public int ItemCount => LotteryItems.Count;

	public float CumulativeOdds
	{
		get
		{
			if (LotteryCumulativeOdds.Count != 0)
			{
				return LotteryCumulativeOdds[LotteryCumulativeOdds.Count - 1];
			}
			return 0f;
		}
	}

	private List<T> LotteryItems { get; set; } = new List<T>();

	private List<float> LotteryCumulativeOdds { get; set; } = new List<float>();

	public Lottery()
	{
	}

	public Lottery(Dictionary<T, float> itemModifierLotteryDict)
	{
		foreach (var (item, odds) in itemModifierLotteryDict)
		{
			AddItem(item, odds);
		}
	}

	public void AddItem(T item, float odds)
	{
		LotteryItems.Add(item);
		float item2 = ((LotteryCumulativeOdds.Count <= 0) ? odds : (LotteryCumulativeOdds[LotteryCumulativeOdds.Count - 1] + odds));
		LotteryCumulativeOdds.Add(item2);
	}

	public void AddItem(T item, Func<T, float> oddsGetter)
	{
		AddItem(item, oddsGetter(item));
	}

	public void AddItems(IEnumerable<T> range, Func<T, float> oddsGetter)
	{
		int count = LotteryItems.Count;
		LotteryItems.AddRange(range);
		float num = CumulativeOdds;
		for (int i = count; i < LotteryItems.Count; i++)
		{
			float num2 = oddsGetter(LotteryItems[i]);
			num += num2;
			LotteryCumulativeOdds.Add(num);
		}
	}

	public void Clear()
	{
		LotteryItems.Clear();
		LotteryCumulativeOdds.Clear();
	}

	public void RecalculateOdds(Func<T, float> oddsGetter)
	{
		float num = 0f;
		for (int i = 0; i < LotteryItems.Count; i++)
		{
			num += oddsGetter(LotteryItems[i]);
			LotteryCumulativeOdds[i] = num;
		}
	}

	public T GetRandomOrDefault(T d = default(T), System.Random random = null)
	{
		if (TryGetRandom(out var result, random))
		{
			return result;
		}
		return d;
	}

	public bool TryGetRandom(out T result, System.Random random = null)
	{
		if (LotteryCumulativeOdds.Count > 0)
		{
			result = GetRandom(random);
			return true;
		}
		result = default;
		return false;
	}

	public int GetCumOddsIndex(float number)
	{
		int num = LotteryCumulativeOdds.BinarySearch(number);
		if (num < 0)
		{
			num = ~num;
		}
		return num;
	}

	public T GetAtCumOdds(float number)
	{
		return LotteryItems[GetCumOddsIndex(number)];
	}

	public T GetRandom(int seed)
	{
		if (LotteryCumulativeOdds.Count == 0)
		{
			throw new InvalidOperationException("No items in the lottery.");
		}
		Unity.Mathematics.Random random = new Unity.Mathematics.Random(Convert.ToUInt32(seed));
		float max = LotteryCumulativeOdds[LotteryCumulativeOdds.Count - 1];
		float number = random.NextFloat(max);
		return GetAtCumOdds(number);
	}

	public T GetRandom(System.Random random)
	{
		if (random == null)
		{
			return GetRandom();
		}
		if (LotteryCumulativeOdds.Count == 0)
		{
			throw new InvalidOperationException("No items in the lottery.");
		}
		float num = LotteryCumulativeOdds[LotteryCumulativeOdds.Count - 1];
		float number = (float)random.NextDouble(0.0, num);
		return GetAtCumOdds(number);
	}

	public void GetRandomUniqueMultipleRolls(List<T> output, int tries)
	{
		HashSet<T> hashSet;
		using (MyCollectionPool<T>.GetHashSet(out hashSet))
		{
			for (int i = 0; i < tries; i++)
			{
				if (TryGetRandom(out var result))
				{
					hashSet.Add(result);
				}
			}
			output.AddRange(hashSet);
		}
	}

	public T GetRandom()
	{
		if (LotteryCumulativeOdds.Count == 0)
		{
			throw new InvalidOperationException("No items in the lottery.");
		}
		float maxInclusive = LotteryCumulativeOdds[LotteryCumulativeOdds.Count - 1];
		float number = UnityEngine.Random.Range(0f, maxInclusive);
		return GetAtCumOdds(number);
	}

	public (T item, int index) GetRandomWithIndex()
	{
		if (LotteryCumulativeOdds.Count == 0)
		{
			throw new InvalidOperationException("No items in the lottery.");
		}
		float maxInclusive = LotteryCumulativeOdds[LotteryCumulativeOdds.Count - 1];
		float number = UnityEngine.Random.Range(0f, maxInclusive);
		int cumOddsIndex = GetCumOddsIndex(number);
		return (item: LotteryItems[cumOddsIndex], index: cumOddsIndex);
	}

	public void RemoveItemAtIndex(int index)
	{
		if (index < 0 || index >= LotteryItems.Count)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		LotteryItems.RemoveAt(index);
		if (LotteryCumulativeOdds.Count != 0)
		{
			float num = ((index == 0) ? LotteryCumulativeOdds[0] : (LotteryCumulativeOdds[index] - LotteryCumulativeOdds[index - 1]));
			LotteryCumulativeOdds.RemoveAt(index);
			for (int i = index; i < LotteryCumulativeOdds.Count; i++)
			{
				LotteryCumulativeOdds[i] -= num;
			}
		}
	}
}
