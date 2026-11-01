using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public sealed class CocktractPartner : SerializedPart<string, CocktractPartnerPrefab>
{
	private sealed class RespectRelationalComparer : IComparer<CocktractPartner>
	{
		public int Compare(CocktractPartner x, CocktractPartner y)
		{
			if (x == y)
			{
				return 0;
			}
			if (y == null)
			{
				return 1;
			}
			if (x == null)
			{
				return -1;
			}
			return y.Respect.CompareTo(x.Respect);
		}
	}

	[SerializeField]
	private CocktractPartnerSpecialDataContainer specialData;

	[field: SerializeField]
	public float Respect { get; set; }

	[field: SerializeField]
	public string Id { get; private set; }

	[field: SerializeField]
	public bool Unlocked { get; private set; }

	public string Name => Prefab.Name.GetValue();

	public SimpleLocalizedString NameSLS => Prefab.Name;

	public override string Key => Id;

	public Sprite LogoSprite => Prefab.LogoSprite.GetResource();

	public string Description => Prefab.Description.Value;

	public float ContractDifficulty => Prefab.CalculateContractDifficulty(this);

	public float GoldContractDifficulty
	{
		get
		{
			float num = Prefab.CalculateContractDifficulty(this) * 1.5f;
			if (!(num > 1f))
			{
				return num;
			}
			return Mathf.Pow(num, 1.3f);
		}
	}

	public float ContractRewardsBonusMultiplier => Prefab.CalculateRewardsBonusMultiplier(this);

	public static IComparer<CocktractPartner> RespectComparer { get; } = new RespectRelationalComparer();

	public CocktractPartner(string id, CocktractPartnerPrefab cocktractPartnerPrefab)
	{
		InitPrefab(cocktractPartnerPrefab);
		Id = id;
	}

	public sealed override void InitPrefab(CocktractPartnerPrefab prefab)
	{
		base.InitPrefab(prefab);
	}

	public T GetSpecialData<T>() where T : CocktractPartnerSpecialData, new()
	{
		if (specialData?.Value is T result)
		{
			return result;
		}
		T val = new T();
		specialData = val;
		return val;
	}

	public bool TryGetSpecialData<T>(out T value) where T : CocktractPartnerSpecialData
	{
		if (specialData.Value is T val)
		{
			value = val;
			return true;
		}
		value = null;
		return false;
	}

	public IEnumerable<CocktractContract> FindRelatedContracts()
	{
		return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager.AllContracts.Where((CocktractContract t) => t.Partner == this);
	}

	public IEnumerable<CocktractContract> FindRelatedFinishedContracts()
	{
		return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager.PastContracts.Where((CocktractContract t) => t.Partner == this);
	}

	public CocktractContract.ContractsAggregateData GetFinishedContractsData()
	{
		return CocktractContract.AggregateData(FindRelatedFinishedContracts());
	}

	public string GetFinishedContractsDescription()
	{
		if (Unlocked)
		{
			CocktractContract.ContractsAggregateData finishedContractsData = GetFinishedContractsData();
			return LOC.UI.cocktracts_Partner_ContractData(finishedContractsData.CompletedCount, finishedContractsData.FailedAndAbandonedCount);
		}
		return LOC.UI.cocktracts_Partner_NotUnlocked;
	}

	public void AddRespect(float respectChange)
	{
		Respect += respectChange;
	}

	public void Unlock()
	{
		Unlocked = true;
	}

	public bool HasGenericContract()
	{
		return Prefab.HasGenericContract(this);
	}

	public bool HasGoldContract()
	{
		return Prefab.HasGoldContract(this);
	}

	public CocktractContract GenerateGenericContract()
	{
		return Prefab.GenerateGenericContract(this);
	}

	public CocktractContract GenerateGoldContract()
	{
		return Prefab.GenerateGoldContract(this);
	}

	public void AddAllPossibleContracts(List<CocktractContract> allContracts)
	{
		Prefab.AddAllPossibleContracts(this, allContracts);
	}

	public void GenerateContractRewards(CocktractContract cocktractContract, float difficultyTarget, float rewardsMultiplier)
	{
		Prefab.GenerateContractRewards(this, cocktractContract, difficultyTarget, rewardsMultiplier);
	}

	public void EndOfDay()
	{
		Prefab.EndOfDay(this);
	}
}
