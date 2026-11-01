using System;
using System.Collections.Generic;
using System.Linq;
using GameItems;
using IC.AutomaticReference;
using StoryConditions;
using UnityEngine;

public abstract class CommonCocktractPartnerPrefabBase(SimpleLocalizedString name, SimpleLocalizedString description, IReference<Sprite> logoSprite, List<StoryCondition> unlockConditions, int order) : CocktractPartnerPrefab(name, description, logoSprite, unlockConditions, order)
{
	protected class GoldContractContainer
	{
		public Func<CocktractPartner, bool> AvailableFunc;

		public Func<CocktractPartner, CocktractContract> GenerateFunc;

		public GoldContractContainer(Func<CocktractPartner, bool> availableFunc, Func<CocktractPartner, CocktractContract> generateFunc)
		{
			AvailableFunc = availableFunc;
			GenerateFunc = generateFunc;
		}
	}

	protected class MilestoneContainer
	{
		public Func<CocktractPartner, bool> AvailableFunc;

		public Action<CocktractPartner> ActivateAction;

		public MilestoneContainer(Func<CocktractPartner, bool> availableFunc, Action<CocktractPartner> activateAction)
		{
			AvailableFunc = availableFunc;
			ActivateAction = activateAction;
		}
	}

	protected List<MilestoneContainer> MilestoneGoals = new List<MilestoneContainer>();

	protected List<Func<CocktractPartner, CocktractContract>> GenericContractGenerators;

	protected List<GoldContractContainer> GoldContractGenerators;

	protected EmailPrefabManager.CocktractPartnerEmailContainer EmailContainer;

	protected const int RespectPerLevel = 5;

	private const int MinTimeBetweenMilestones = 4320;

	private const int MinTimeBetweenGoldContracts = 4320;

	public abstract List<ItemEnum> MerchRewardList { get; protected set; }

	public GenericCocktractSpecialData GetSpecialData(CocktractPartner cocktractPartner)
	{
		return cocktractPartner.GetSpecialData<GenericCocktractSpecialData>();
	}

	protected static List<GoldContractContainer> CreateNormalGoldContractProgression(params Func<CocktractPartner, CocktractContract>[] generationFuncs)
	{
		List<GoldContractContainer> list = new List<GoldContractContainer>();
		list.Add(new GoldContractContainer((CocktractPartner x) => true, generationFuncs[0]));
		for (int num = 1; num < generationFuncs.Length; num++)
		{
			int i1 = num;
			list.Add(new GoldContractContainer((CocktractPartner p) => p.Respect > (float)(5 * i1), generationFuncs[num]));
		}
		return list;
	}

	public override CocktractContract GenerateGenericContract(CocktractPartner partner)
	{
		CocktractContract cocktractContract = GenericContractGenerators.GetRandom()(partner);
		cocktractContract.IsGold = false;
		if (cocktractContract.TimeLimitForAccepting == 0)
		{
			cocktractContract.TimeLimitForAccepting = UnityEngine.Random.Range(40, 80) * 60;
		}
		return cocktractContract;
	}

	public override CocktractContract GenerateGoldContract(CocktractPartner partner)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		if (!GoldContractGenerators.TryGetValue(specialData.GoldContractsGiven, out var value))
		{
			value = GoldContractGenerators[0];
		}
		specialData.LastGoldContractGivenTime = GameVariables.Current.time;
		specialData.GoldContractsGiven++;
		CocktractContract cocktractContract = value.GenerateFunc(partner);
		cocktractContract.IsGold = true;
		if (cocktractContract.TimeLimitForAccepting == 0)
		{
			cocktractContract.TimeLimitForAccepting = 24 * UnityEngine.Random.Range(7, 12) * 60;
		}
		return cocktractContract;
	}

	public override Item GenerateMerchReward()
	{
		return Item.CreateRandomItem(MerchRewardList.GetRandom().ToGameId());
	}

	public override bool HasGenericContract(CocktractPartner cocktractPartner)
	{
		return GetSpecialData(cocktractPartner).GoldContractsGiven > 0;
	}

	public override bool HasGoldContract(CocktractPartner partner)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		GameVariables current = GameVariables.Current;
		if (specialData.GoldContractsGiven > specialData.GoldContractsEnded)
		{
			return false;
		}
		if (current.time - specialData.LastGoldContractGivenTime < 4320)
		{
			return false;
		}
		if (!GoldContractGenerators.TryGetValue(specialData.GoldContractsGiven, out var value))
		{
			return false;
		}
		return value.AvailableFunc(partner);
	}

	public override void EndOfDay(CocktractPartner partner)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		GameVariables current = GameVariables.Current;
		if (current.time - specialData.LastMilestoneDoneTime >= 4320 && MilestoneGoals.TryGetValue(specialData.NextMilestoneLevel, out var value) && value.AvailableFunc(partner))
		{
			specialData.LastMilestoneDoneTime = current.time;
			specialData.NextMilestoneLevel++;
			value.ActivateAction(partner);
		}
	}

	public virtual void OnContractRejected(CocktractPartner partner, CocktractContract contract)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		if (contract.IsGold)
		{
			GameVariables.Current.AddEmail(EmailPrefabManager.GetEmail(EmailContainer.GoldRefusalEmail));
			specialData.RejectedGoldContracts++;
			partner.AddRespect(-0.4f);
		}
		else if (specialData.RejectedContracts - specialData.RejectedGoldContracts == 0)
		{
			GameVariables.Current.AddEmail(EmailPrefabManager.GetEmail(EmailContainer.RefusalFirstEmail));
		}
		specialData.RejectedContracts++;
		partner.AddRespect(-0.1f);
	}

	public virtual void OnContractFailed(CocktractPartner partner, CocktractContract contract)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		GameVariables.Current.AddEmail((specialData.FailedContracts <= 0) ? EmailPrefabManager.GetEmail(EmailContainer.FailFirstEmail) : EmailPrefabManager.GetEmail(EmailContainer.FailGenericEmail));
		specialData.FailedContracts++;
		if (contract.IsGold)
		{
			specialData.FailedGoldContracts++;
		}
	}

	public virtual void OnContractExpired(CocktractPartner partner, CocktractContract contract)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		if (contract.IsGold)
		{
			GameVariables.Current.AddEmail(EmailPrefabManager.GetEmail(EmailContainer.GoldExpiryGenericEmail));
			specialData.ExpiredGoldContracts++;
			partner.AddRespect(-0.2f);
		}
		specialData.ExpiredContracts++;
		partner.AddRespect(-0.1f);
	}

	public virtual void OnContractCompleted(CocktractPartner partner, CocktractContract contract)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		if (contract.IsGold)
		{
			specialData.CompletedGoldContracts++;
		}
		specialData.CompletedContracts++;
	}

	public override void OnContractChangedStatus(CocktractPartner partner, CocktractContract contract)
	{
		switch (contract.Status)
		{
		case CocktractContract.ContractStatus.Rejected:
			OnContractRejected(partner, contract);
			break;
		case CocktractContract.ContractStatus.Failed:
		case CocktractContract.ContractStatus.Abandoned:
			OnContractFailed(partner, contract);
			break;
		case CocktractContract.ContractStatus.Expired:
			OnContractExpired(partner, contract);
			break;
		case CocktractContract.ContractStatus.Completed:
			OnContractCompleted(partner, contract);
			break;
		}
	}

	public override float CalculateContractDifficulty(CocktractPartner cocktractPartner)
	{
		return 1f + Mathf.Max(cocktractPartner.Respect / 2f, 0f);
	}

	public override float CalculateRewardsBonusMultiplier(CocktractPartner cocktractPartner)
	{
		return cocktractPartner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus;
	}

	public override void AddAllPossibleContracts(CocktractPartner cocktractPartner, List<CocktractContract> allContracts)
	{
		for (int i = 0; i < GenericContractGenerators.Count; i++)
		{
			Func<CocktractPartner, CocktractContract> func = GenericContractGenerators[i];
			cocktractPartner.Respect = 15 * i;
			allContracts.Add(func(cocktractPartner));
		}
		for (int j = 0; j < GoldContractGenerators.Count; j++)
		{
			GoldContractContainer goldContractContainer = GoldContractGenerators[j];
			cocktractPartner.Respect = 5 * j;
			allContracts.Add(goldContractContainer.GenerateFunc(cocktractPartner));
		}
	}

	public virtual void GenerateContractMerchRewards(CocktractPartner partner, CocktractContract c, float rewardsMultiplier, float rewardsMultiplierBonus)
	{
		int num = GameVariables.Current.itemManager.MerchCount(partner.Id);
		float num2 = ((num > 0) ? (0.2f + 0.4f / (float)num) : 0.7f);
		if (UnityEngine.Random.value < num2 || (num == 0 && c.GetAllRequirements<WhileWearingMerchRequirement>().Any()))
		{
			Item item = GenerateMerchReward();
			c.AcceptBonusReward.Add(new ItemDeliveryReward(item));
		}
	}

	protected float RewardsMulti(float difficultyTarget, float rewardsMultiplierBonus)
	{
		return (1f + (float)(10.0 * Math.Tanh(difficultyTarget / 100f))) * (1f + rewardsMultiplierBonus);
	}

	public override void GenerateContractRewards(CocktractPartner partner, CocktractContract c, float difficultyTarget, float rewardsMultiplierBonus)
	{
		GenerateContractMerchRewards(partner, c, difficultyTarget, rewardsMultiplierBonus);
		GameVariables current = GameVariables.Current;
		float num = RewardsMulti(difficultyTarget, rewardsMultiplierBonus);
		c.AddDefaultRespectRewards(num);
		c.AddDefaultCashReward(current.cocktractManager.GenerateRandomMoneyReward(num));
	}
}
