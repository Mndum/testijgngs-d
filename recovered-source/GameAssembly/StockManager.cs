using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class StockManager
{
	public const int TimeStep = 15;

	public const int MemorySize = 96;

	[field: SerializeField]
	public List<StockCompany> StockCompanies { get; private set; } = new List<StockCompany>();

	public event Action OnSync;

	public static StockManager CreateInstance()
	{
		StockManager stockManager = new StockManager();
		stockManager.FillPrefabData();
		return stockManager;
	}

	public void InitializeNewGame()
	{
		for (int i = 0; i < 96; i++)
		{
			foreach (StockCompany stockCompany in StockCompanies)
			{
				stockCompany.Sync();
			}
		}
	}

	public void MinutePassed(int time)
	{
		if (time % 15 != 0)
		{
			return;
		}
		foreach (StockCompany stockCompany in StockCompanies)
		{
			stockCompany.Sync();
		}
		OnSync?.Invoke();
	}

	public void FillPrefabData()
	{
		StockCompanyPrefabManager.EnsureCompaniesData(StockCompanies);
	}
}
