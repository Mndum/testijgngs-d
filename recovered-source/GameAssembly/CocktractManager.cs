using System;
using System.Collections.Generic;
using System.Linq;
using StoryConditions;
using UnityEngine;

[Serializable]
public class CocktractManager : GameVariablesSubClass
{
	[SerializeField]
	public List<CocktractContract> AvailableContracts = new List<CocktractContract>();

	public const int TargetAvailableContractCount = 4;

	[SerializeField]
	public List<CocktractContract> CurrentContracts = new List<CocktractContract>();

	[SerializeField]
	public List<CocktractContract> PastContracts = new List<CocktractContract>();

	[SerializeField]
	private float globalRespect;

	public CocktractContract CurrentCocktractContract => CurrentContracts.FirstOrDefault();

	public bool HasActiveContract => CurrentContracts.Count > 0;

	public IEnumerable<CocktractContract> AllContracts => AvailableContracts.Concat(CurrentContracts).Concat(PastContracts.AsEnumerable().Reverse());

	public float GlobalRespect => globalRespect;

	[field: SerializeField]
	public List<CocktractPartner> CocktractPartners { get; private set; } = new List<CocktractPartner>();

	public bool PartnersInitialized => CocktractPartners.Count > 0;

	private bool GoldenContractAvailable => AvailableContracts.Any((CocktractContract t) => t.IsGold);

	public static bool CocktractsUnlocked => GameVariables.Current.CheckFlag("cocktractsunlocked");

	public static CocktractManager CreateInstance()
	{
		CocktractManager cocktractManager = new CocktractManager();
		cocktractManager.FillPrefabData();
		return cocktractManager;
	}

	public void AddPastContract(CocktractContract contract)
	{
		PastContracts.Insert(0, contract);
	}

	public (string source, int index) GetSource(CocktractContract cocktractContract)
	{
		switch (cocktractContract.Status)
		{
		default:
			return (source: "AvailableContracts", index: AvailableContracts.IndexOf(cocktractContract));
		case CocktractContract.ContractStatus.Accepted:
			return (source: "CurrentContracts", index: CurrentContracts.IndexOf(cocktractContract));
		case CocktractContract.ContractStatus.Completed:
		case CocktractContract.ContractStatus.Failed:
		case CocktractContract.ContractStatus.Abandoned:
			return (source: "PastContracts", index: PastContracts.IndexOf(cocktractContract));
		case CocktractContract.ContractStatus.Expired:
		case CocktractContract.ContractStatus.Rejected:
			return (source: "Error!", index: -1);
		}
	}

	public bool TryGetContract(string source, int index, out CocktractContract cocktractContract)
	{
		switch (source)
		{
		case "CurrentContracts":
			return CurrentContracts.TryGetValue(index, out cocktractContract);
		case "AvailableContracts":
			return AvailableContracts.TryGetValue(index, out cocktractContract);
		case "PastContracts":
			return PastContracts.TryGetValue(index, out cocktractContract);
		default:
			cocktractContract = null;
			return false;
		}
	}

	public List<T> GetAllRequirements<T>()
	{
		List<T> output = new List<T>();
		GetAllRequirements(ref output);
		return output;
	}

	public void GetAllRequirements<T>(ref List<T> output)
	{
		foreach (CocktractContract currentContract in CurrentContracts)
		{
			currentContract.GetAllRequirements(ref output);
		}
	}

	public void ShowCurrentContract()
	{
		if (CurrentCocktractContract != null)
		{
			UiOverlay.Instance.OkPopup(LOC.UI.Contract_CurrentContract, CurrentCocktractContract.GenerateDescription());
		}
	}

	public void AcceptContract(CocktractContract cocktractContract)
	{
		AvailableContracts.Remove(cocktractContract);
		CurrentContracts.Add(cocktractContract);
		cocktractContract.Accepted();
	}

	public void RejectContract(CocktractContract cocktractContract)
	{
		AvailableContracts.Remove(cocktractContract);
		cocktractContract.Rejected();
	}

	public void AbandonContract(CocktractContract cocktractContract)
	{
		CurrentContracts.Remove(cocktractContract);
		AddPastContract(cocktractContract);
		cocktractContract.Abandoned();
	}

	public void CompleteContract(CocktractContract cocktractContract)
	{
		CurrentContracts.Remove(cocktractContract);
		AddPastContract(cocktractContract);
		cocktractContract.Complete();
	}

	public void AddRespect(float respect)
	{
		globalRespect += respect;
	}

	public void FillPrefabData()
	{
		CocktractPartnerPrefabManager.EnsureCocktractPartners(CocktractPartners);
	}

	public bool TryGetPartnerById(string key, out CocktractPartner partner)
	{
		foreach (CocktractPartner cocktractPartner in CocktractPartners)
		{
			if (cocktractPartner.Key == key)
			{
				partner = cocktractPartner;
				return true;
			}
		}
		partner = null;
		return false;
	}

	private void RemoveExpiredContracts()
	{
		int time = GameVariables.Current.time;
		for (int num = AvailableContracts.Count - 1; num >= 0; num--)
		{
			CocktractContract cocktractContract = AvailableContracts[num];
			if (cocktractContract.TooLateToAccept(time))
			{
				cocktractContract.Expired();
				AvailableContracts.RemoveAt(num);
			}
		}
	}

	private void AddNewContracts()
	{
		if (AvailableContracts.Count >= 4)
		{
			return;
		}
		List<CocktractPartner> list;
		using (MyCollectionPool<CocktractPartner>.GetList(out list))
		{
			Lottery<CocktractPartner> lottery = new Lottery<CocktractPartner>();
			foreach (CocktractPartner partner in CocktractPartners)
			{
				if (partner.Unlocked && AvailableContracts.All((CocktractContract x) => x.PartnerId != partner.Id))
				{
					lottery.AddItem(partner, Mathf.Max(partner.Respect + 20f, 1f));
				}
			}
			if (!lottery.NotEmpty)
			{
				return;
			}
			lottery.GetRandomUniqueMultipleRolls(list, 3);
			list.Sort(CocktractPartner.RespectComparer);
			int num = -1;
			if (!GoldenContractAvailable && !HasActiveContract)
			{
				int num2 = -1;
				for (int num3 = 0; num3 < list.Count; num3++)
				{
					if (list[num3].HasGoldContract())
					{
						num2 = num3;
						if (UnityEngine.Random.value > 0.4f)
						{
							num = num3;
							break;
						}
					}
				}
				if (num == -1)
				{
					num = num2;
				}
			}
			int num4 = 4 - AvailableContracts.Count;
			if (num >= 0)
			{
				CocktractPartner cocktractPartner = list[num];
				list.RemoveAt(num);
				AvailableContracts.Add(cocktractPartner.GenerateGoldContract());
				num4--;
			}
			for (int num5 = 0; num5 < num4; num5++)
			{
				if (list.Count == 0)
				{
					break;
				}
				int index = UnityEngine.Random.Range(0, list.Count);
				CocktractPartner cocktractPartner2 = list[index];
				if (cocktractPartner2.HasGenericContract())
				{
					AvailableContracts.Add(cocktractPartner2.GenerateGenericContract());
				}
				list.RemoveAt(index);
			}
		}
	}

	public void UpdatePartners()
	{
		CocktractPartner cocktractPartner = null;
		foreach (CocktractPartner cocktractPartner2 in CocktractPartners)
		{
			if (!cocktractPartner2.Unlocked)
			{
				if (cocktractPartner2.Prefab.UnlockConditions.ConditionsMet())
				{
					cocktractPartner2.Unlock();
					cocktractPartner = cocktractPartner2;
				}
			}
			else
			{
				cocktractPartner2.EndOfDay();
			}
		}
		if (cocktractPartner != null && !CocktractsUnlocked)
		{
			Debug.Log("Cocktracts event planned!(" + cocktractPartner.Name + " unlocked)");
			RelatedGameVariables.AddFlag("cocktractsunlockedplanned");
		}
	}

	public void GenerateAllContracts()
	{
		List<CocktractContract> list = new List<CocktractContract>();
		foreach (CocktractPartner cocktractPartner in CocktractPartners)
		{
			cocktractPartner.AddAllPossibleContracts(list);
		}
		AvailableContracts = list;
	}

	public int GenerateRandomMoneyReward(float multi)
	{
		return 10 + Mathf.CeilToInt((float)Mathf.Max(GameVariables.Current.followers, 50) * multi);
	}

	public void HourlyUpdate()
	{
		RemoveExpiredContracts();
	}

	public void EndOfDay()
	{
		UpdatePartners();
		AddNewContracts();
	}
}
