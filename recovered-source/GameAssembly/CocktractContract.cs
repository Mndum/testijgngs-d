using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameItems;
using UnityEngine;
using UnityEngine.Localization.Tables;

[Serializable]
public class CocktractContract : ISerializationCallbackReceiver, IRewardSource
{
	public class RewardsAggregateData
	{
		public int Money;

		public float Respect;

		public List<Item> ItemRewards = new List<Item>();

		public bool OtherRewards;

		public void Add(Reward val)
		{
			if (val is MoneyReward moneyReward)
			{
				Money += moneyReward.money;
			}
			else if (val is CocktractsRespectReward cocktractsRespectReward)
			{
				Respect += cocktractsRespectReward.respect;
			}
			else if (val is ItemDeliveryReward itemDeliveryReward)
			{
				ItemRewards.Add(itemDeliveryReward.item);
			}
			else
			{
				OtherRewards = true;
			}
		}
	}

	public enum ContractStatus
	{
		Available = 0,
		Accepted = 100,
		Expired = 202,
		Rejected = 203,
		Completed = 300,
		Failed = 301,
		Abandoned = 302
	}

	public class ContractsAggregateData
	{
		public int AvailableCount;

		public int AcceptedCount;

		public int AbandonedCount;

		public int FailedCount;

		public int CompletedCount;

		public int FailedAndAbandonedCount => FailedCount + AbandonedCount;
	}

	[SerializeField]
	private SimpleLocalizedStringWithBakedArguments _contractNarration;

	[SerializeField]
	private string _partnerId = "";

	private CocktractPartner _partner;

	[SerializeField]
	private RequirementContainer _requirement = new RequirementContainer(null);

	private List<IUpdatableRequirement> _updateableRequirements = new List<IUpdatableRequirement>();

	public bool HasNarration
	{
		get
		{
			if (_contractNarration != null)
			{
				return !_contractNarration.IsEmpty;
			}
			return false;
		}
	}

	[field: SerializeField]
	public bool Seen { get; set; }

	[field: SerializeField]
	public bool IsGold { get; set; }

	public string PartnerId => _partnerId;

	public CocktractPartner Partner
	{
		get
		{
			InitializePartners();
			return _partner;
		}
		set
		{
			_partnerId = value.Key;
			_partner = value;
		}
	}

	public bool Completed
	{
		get
		{
			if (!_requirement.IsNull)
			{
				return _requirement.Value.CheckIfDone();
			}
			return true;
		}
	}

	public bool Possible
	{
		get
		{
			if (!_requirement.IsNull)
			{
				return _requirement.Value.CheckIfPossible();
			}
			return true;
		}
	}

	[field: SerializeField]
	public List<RewardContainer> AcceptBonusReward { get; private set; } = new List<RewardContainer>();

	[field: SerializeField]
	public List<RewardContainer> SuccessReward { get; private set; } = new List<RewardContainer>();

	[field: SerializeField]
	public List<RewardContainer> FailureReward { get; private set; } = new List<RewardContainer>();

	[field: SerializeField]
	public ContractStatus Status { get; private set; }

	public bool StatusCompleted => Status == ContractStatus.Completed;

	public bool StatusFailed
	{
		get
		{
			if (Status != ContractStatus.Failed && Status != ContractStatus.Abandoned)
			{
				return Status == ContractStatus.Expired;
			}
			return true;
		}
	}

	[field: SerializeField]
	public int CreatedTime { get; private set; }

	[field: SerializeField]
	public int TimeLimitForAccepting { get; set; }

	public bool HasAcceptingTimeLimit => TimeLimitForAccepting > 0;

	public int TimeLeftToAccept => CreatedTime + TimeLimitForAccepting - GameVariables.Current.time;

	[field: SerializeField]
	public int AcceptedTime { get; private set; }

	[field: SerializeField]
	public int FinishedTime { get; private set; }

	public int AcceptedTimeConsideringStatus
	{
		get
		{
			if (Status != ContractStatus.Available)
			{
				return AcceptedTime;
			}
			return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		}
	}

	public int CurrentTimeConsideringStatus
	{
		get
		{
			if (Status != ContractStatus.Failed && Status != ContractStatus.Completed && Status != ContractStatus.Abandoned)
			{
				return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
			}
			return FinishedTime;
		}
	}

	public static TableReference ContractTableName => LOC.Contracts.TABLE_REFERENCE;

	private CocktractContract()
	{
	}

	public CocktractContract(int createdTime, CocktractPartner partner)
	{
		CreatedTime = createdTime;
		Partner = partner;
	}

	public string GetNarration()
	{
		if (!HasNarration)
		{
			return LOC.UI.Contract_GenericNarration;
		}
		return _contractNarration.Value;
	}

	public void SetNarration(SimpleLocalizedStringWithBakedArguments description)
	{
		_contractNarration = description;
	}

	public void SetNarration(LOS.TupleReference description)
	{
		_contractNarration = SimpleLocalizedStringWithBakedArguments.CreateInstance(description);
	}

	private void InitializePartners()
	{
		if (_partner == null && GameScript.HasGameStarted && SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager.PartnersInitialized)
		{
			CocktractManager cocktractManager = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager;
			if (cocktractManager.TryGetPartnerById(_partnerId, out var partner))
			{
				_partner = partner;
				return;
			}
			Debug.LogError("Error initializing contract partners! Partner \"" + _partnerId + "\" wasn't found in the CTM!!! Using just the first partner and hoping it works!");
			_partner = cocktractManager.CocktractPartners[0];
		}
	}

	public List<T> GetAllRequirements<T>()
	{
		List<T> list = new List<T>();
		_requirement.Value?.GetAllToList(list);
		return list;
	}

	public void GetAllRequirements<T>(ref List<T> list)
	{
		_requirement.Value?.GetAllToList(list);
	}

	public void SetRequirement(RequirementContainer value)
	{
		_requirement = value;
		InitializeRequirements();
	}

	private void InitializeRequirements()
	{
		_requirement.Value?.Initialize(this, null);
		GetAllRequirements(ref _updateableRequirements);
	}

	private void UpdateRequirements()
	{
		foreach (IUpdatableRequirement updateableRequirement in _updateableRequirements)
		{
			updateableRequirement.Update();
		}
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		InitializeRequirements();
	}

	public void AddDefaultRespectRewards(float amount)
	{
		SuccessReward.Add(new CocktractsRespectReward(amount));
		FailureReward.Add(new CocktractsRespectReward((0f - amount) * 2f));
	}

	public void AddDefaultCashReward(int amount)
	{
		SuccessReward.Add(new MoneyReward(amount));
	}

	public RewardsAggregateData CalculateRewardsAggregateData()
	{
		RewardsAggregateData rewardsAggregateData = new RewardsAggregateData();
		foreach (RewardContainer item in AcceptBonusReward)
		{
			rewardsAggregateData.Add(item.Value);
		}
		foreach (RewardContainer item2 in SuccessReward)
		{
			rewardsAggregateData.Add(item2.Value);
		}
		return rewardsAggregateData;
	}

	public int GetMoneyRewardAmount()
	{
		return CalculateRewardsAggregateData().Money;
	}

	public string GenerateDescription()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			if (AcceptBonusReward.Any())
			{
				stringBuilder.AppendLine("\n" + LOC.UI.Contract_OnAcceptBonus);
				stringBuilder.AppendJoin("\n", AcceptBonusReward.Select((RewardContainer t) => t.Value.GenerateDescription().TMPColorSurround("green")));
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(LOC.UI.UI_Requirements);
			if (!_requirement.IsNull)
			{
				stringBuilder.AppendLine(_requirement.Value.GenerateDescription());
			}
			else
			{
				stringBuilder.AppendLine("");
			}
			if (SuccessReward.Any())
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(LOC.UI.Reward_OnSuccess);
				stringBuilder.AppendJoin("\n", SuccessReward.Select((RewardContainer t) => t.Value.GenerateDescription().TMPColorSurround("green")));
			}
			if (FailureReward.Any())
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(LOC.UI.Reward_OnFailure);
				stringBuilder.AppendJoin("\n", FailureReward.Select((RewardContainer t) => t.Value.GenerateDescription().TMPColorSurround("red")));
			}
			if (SuccessReward.Count == 0 && FailureReward.Count == 0)
			{
				stringBuilder.AppendLine(LOC.UI.Reward_NoReward);
			}
			return stringBuilder.ToString();
		}
	}

	public string GenerateAvailableDescription()
	{
		if (HasAcceptingTimeLimit)
		{
			return GenerateTimeLeftSummary() + "\n" + GenerateRewardsSummary();
		}
		return GenerateRewardsSummary();
	}

	public string GenerateHistoryDescription()
	{
		StringBuilder stringBuilder = new StringBuilder();
		switch (Status)
		{
		case ContractStatus.Available:
			stringBuilder.Append(LOC.UI.Contract_AvailableContract);
			break;
		case ContractStatus.Accepted:
			stringBuilder.Append(LOC.UI.Contract_AcceptedContract);
			break;
		case ContractStatus.Expired:
			stringBuilder.Append(LOC.UI.Contract_ExpiredContract);
			break;
		case ContractStatus.Rejected:
			stringBuilder.Append(LOC.UI.Contract_RejectedContract);
			break;
		case ContractStatus.Completed:
			stringBuilder.Append(LOC.UI.Contract_CompletedContract);
			break;
		case ContractStatus.Failed:
			stringBuilder.Append(LOC.UI.Contract_FailedContract);
			break;
		case ContractStatus.Abandoned:
			stringBuilder.Append(LOC.UI.Contract_AbandonedContract);
			break;
		}
		stringBuilder.Append(" ");
		stringBuilder.Append(Partner.Name);
		if (AcceptedTime > 0)
		{
			stringBuilder.Append(" ");
			stringBuilder.Append(LOC.UI.Contract_AcceptedAt(GameVariables.TimeToDateString(AcceptedTime)));
		}
		if (FinishedTime > 0)
		{
			stringBuilder.Append(" ");
			switch (Status)
			{
			case ContractStatus.Completed:
				stringBuilder.Append(LOC.UI.Contract_CompletedAt(GameVariables.TimeToDateString(FinishedTime)));
				break;
			case ContractStatus.Failed:
				stringBuilder.Append(LOC.UI.Contract_FailedAt(GameVariables.TimeToDateString(FinishedTime)));
				break;
			case ContractStatus.Abandoned:
				stringBuilder.Append(LOC.UI.Contract_AbandonedAt(GameVariables.TimeToDateString(FinishedTime)));
				break;
			}
		}
		return stringBuilder.ToString();
	}

	public string GenerateTimeLeftSummary()
	{
		if (Status != ContractStatus.Available)
		{
			return "";
		}
		if (TimeLimitForAccepting <= 0)
		{
			return "";
		}
		return LOC.UI.Contracts_TimeLeftToAccept(GlobalFunctions.TimePeriodHours(TimeLeftToAccept));
	}

	public string GenerateRewardsSummary()
	{
		RewardsAggregateData rewardsAggregateData = CalculateRewardsAggregateData();
		StringBuilder stringBuilder = new StringBuilder();
		if (rewardsAggregateData.Money > 0)
		{
			stringBuilder.Append(rewardsAggregateData.Money);
			stringBuilder.Append("$");
			stringBuilder.AppendLine();
		}
		if (rewardsAggregateData.Respect > 0f)
		{
			stringBuilder.AppendLine(CocktractsRespectReward.GenerateStringForRespect(rewardsAggregateData.Respect));
		}
		if (rewardsAggregateData.ItemRewards.Count > 0)
		{
			stringBuilder.AppendJoin(" ", rewardsAggregateData.ItemRewards.Select((Item t) => t.GetName()));
		}
		if (rewardsAggregateData.OtherRewards)
		{
			stringBuilder.AppendLine(LOC.UI.Contact_HasOtherRewardShort);
		}
		if (stringBuilder.Length == 0)
		{
			stringBuilder.AppendLine(LOC.UI.Contract_NoRewards);
		}
		else
		{
			stringBuilder.TMPColorSurroundStringBuilderWrap(Color.green);
			stringBuilder.Insert(0, "\n");
			stringBuilder.Insert(0, LOC.UI.Contract_RewardShort);
		}
		return stringBuilder.ToString();
	}

	public bool TooLateToAccept(int currentTime)
	{
		if (TimeLimitForAccepting > 0)
		{
			return currentTime > CreatedTime + TimeLimitForAccepting;
		}
		return false;
	}

	public void Expired()
	{
		Status = ContractStatus.Expired;
		FinishedTime = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		Partner.Prefab.OnContractChangedStatus(Partner, this);
	}

	public void Rejected()
	{
		Status = ContractStatus.Rejected;
		FinishedTime = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		Partner.Prefab.OnContractChangedStatus(Partner, this);
	}

	public void Accepted()
	{
		Status = ContractStatus.Accepted;
		FinishedTime = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		AcceptedTime = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		foreach (RewardContainer item in AcceptBonusReward)
		{
			item.Value.Claim(this);
		}
		UpdateRequirements();
		Partner.Prefab.OnContractChangedStatus(Partner, this);
	}

	public void Abandoned()
	{
		Status = (Possible ? ContractStatus.Abandoned : ContractStatus.Failed);
		FinishedTime = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		foreach (RewardContainer item in FailureReward)
		{
			item.Value.Claim(this);
		}
		Partner.Prefab.OnContractChangedStatus(Partner, this);
	}

	public void Complete()
	{
		Status = ContractStatus.Completed;
		FinishedTime = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		foreach (RewardContainer item in SuccessReward)
		{
			item.Value.Claim(this);
		}
		Partner.Prefab.OnContractChangedStatus(Partner, this);
	}

	public static CocktractContract CreateRandom(CocktractPartner partner)
	{
		return CreateRandom(partner, partner.ContractDifficulty, partner.ContractRewardsBonusMultiplier);
	}

	public static CocktractContract CreateRandomGold(CocktractPartner partner)
	{
		CocktractContract cocktractContract = CreateRandom(partner, partner.GoldContractDifficulty, partner.ContractRewardsBonusMultiplier);
		cocktractContract.IsGold = true;
		return cocktractContract;
	}

	public static CocktractContract CreateRandom(CocktractPartner partner, float difficultyTarget, float rewardsMultiplierBonus = 0f)
	{
		CocktractContract cocktractContract = new CocktractContract(GameVariables.Current.time, partner);
		_ = GameVariables.Current;
		if (difficultyTarget < 1f)
		{
			difficultyTarget = 1f;
		}
		float num = UnityEngine.Random.Range(0.7f, 1.3f) * difficultyTarget;
		List<RequirementContainer> list = new List<RequirementContainer>();
		TimeConstraintRequirement timeConstraintRequirement = TimeConstraintRequirement.CreateFromTargetDifficulty(UnityEngine.Random.Range(0f, num));
		list.Add(timeConstraintRequirement);
		if (UnityEngine.Random.value < 0.2f)
		{
			AchieveInOneStream achieveInOneStream = new AchieveInOneStream();
			float num2 = UnityEngine.Random.Range(0f, num / 2f);
			float num3 = difficultyTarget / 2f - num2;
			StreamForXMinutesRequirement streamForXMinutesRequirement = StreamForXMinutesRequirement.CreateFromTargetDifficulty(num2, 1);
			achieveInOneStream.AddOperand(streamForXMinutesRequirement);
			if (num3 > 3f)
			{
				float value = UnityEngine.Random.value;
				if (value < 0.5f)
				{
					achieveInOneStream.AddOperand(new StreamEventRequirement(0, 1, StreamEventRequirementEnum.GotFlacid)
					{
						Difficulty = 3f
					});
				}
				else if (value < 0.8f)
				{
					achieveInOneStream.AddOperand(new StreamEventRequirement(UnityEngine.Random.Range(3, 5), 0, default(StreamEventRequirementEnum))
					{
						Difficulty = 3f
					});
				}
				else
				{
					achieveInOneStream.AddOperand(new StreamEventRequirement(UnityEngine.Random.Range(3, 5), 0, StreamEventRequirementEnum.CumOutside)
					{
						Difficulty = 3f
					});
				}
				num3 -= 3f;
			}
			list.Insert(0, achieveInOneStream);
		}
		else
		{
			RequirementContainer item = StreamForXMinutesRequirement.CreateFromTargetDifficulty(num, Mathf.CeilToInt((float)timeConstraintRequirement.Minutes / 1440f));
			list.Insert(0, item);
		}
		cocktractContract.SetRequirement(new AndRequirement(list));
		partner.GenerateContractRewards(cocktractContract, difficultyTarget, rewardsMultiplierBonus);
		return cocktractContract;
	}

	public static CocktractContract CreateBasic(CocktractPartner partner, int streamHours)
	{
		CocktractContract cocktractContract = new CocktractContract(GameVariables.Current.time, partner);
		cocktractContract.SetRequirement(new AndRequirement(new StreamForXMinutesRequirement(streamHours * 60), new TimeConstraintRequirement(10080)));
		cocktractContract.AddDefaultRespectRewards(1f);
		return cocktractContract;
	}

	public static CocktractContract CreateBasicItem(CocktractPartner partner, int streamHours, params Item[] items)
	{
		CocktractContract cocktractContract = new CocktractContract(GameVariables.Current.time, partner)
		{
			AcceptBonusReward = items.Select((Item t) => new ItemDeliveryReward(t).ToRewardContainer()).ToList()
		};
		cocktractContract.SetRequirement(new AndRequirement(new StreamForXMinutesRequirement(streamHours * 60, items.Select((Item t) => new WhileWearingItemRequirement(t).ToRequirementContainer()).ToArray()), new TimeConstraintRequirement(10080)));
		cocktractContract.AddDefaultRespectRewards(1f);
		return cocktractContract;
	}

	public static CocktractContract CreateBasicOneLongStream(CocktractPartner partner, int streamHours)
	{
		CocktractContract cocktractContract = new CocktractContract(GameVariables.Current.time, partner);
		cocktractContract.SetRequirement(new AndRequirement(new AchieveInOneStream(new StreamForXMinutesRequirement(streamHours * 60)), new TimeConstraintRequirement(10080)));
		cocktractContract.AddDefaultRespectRewards(1f);
		return cocktractContract;
	}

	public static void Test()
	{
		Item item = new Item(ItemEnum.HyperCamoCatEars);
		CocktractContract cocktractContract = new CocktractContract();
		cocktractContract.AcceptBonusReward.Add((ItemDeliveryReward)item);
		cocktractContract.SuccessReward.Add((MoneyReward)1000);
		cocktractContract.SuccessReward.Add(new CocktractsRespectReward(50f));
		cocktractContract.FailureReward.Add(new CocktractsRespectReward(-50f));
		cocktractContract.SetRequirement(new AndRequirement(new AchieveInOneStream(new StreamForXMinutesRequirement(60, new WhileStreamViewershipRequirement(500, 0, 0), new WhileStreamPositionRequirement(ControllerEnum.Blowjob), new WhileStreamTimeOfDayRequirement(GameVariables.TimeInDay(21, 37), GameVariables.TimeInDay(23, 0)), new WhileWearingItemRequirement(item)), new StreamEventRequirement(0, 1, StreamEventRequirementEnum.GotFlacid), new StreamEventRequirement(5, StreamEventRequirementEnum.CumOutside, StreamEventRequirementEnum.CumInside), new StreamEventRequirement(5, 6, StreamEventRequirementEnum.Insert)), new TimeConstraintRequirement(10080)));
		CocktractContract cocktractContract2 = new CocktractContract();
		cocktractContract2.SuccessReward.Add((MoneyReward)100);
		cocktractContract2.SuccessReward.Add((MoneyReward)50);
		cocktractContract2.SuccessReward.Add(new CocktractsRespectReward(50f));
		cocktractContract2.FailureReward.Add(new CocktractsRespectReward(-50f));
		cocktractContract2.SetRequirement(new AndRequirement(new AchieveInOneStream(new StreamForXMinutesRequirement(50, new WhileStreamViewershipRequirement(500, 0, 0), new WhileStreamPositionRequirement(ControllerEnum.Blowjob), new WhileStreamTimeOfDayRequirement(GameVariables.TimeInDay(21, 37), GameVariables.TimeInDay(23, 0))), new InOrderRequirement(new StreamEventRequirement(1, 0, StreamEventRequirementEnum.Unzip), new StreamEventRequirement(2, StreamEventRequirementEnum.CumOutside, StreamEventRequirementEnum.CumInside), new StreamEventRequirement(1, 0, StreamEventRequirementEnum.GotFlacid))), new TimeConstraintRequirement(10080)));
		CocktractContract cocktractContract3 = new CocktractContract();
		cocktractContract3.AcceptBonusReward.Add(new ItemDeliveryReward(new Item(ItemEnum.Sausage)));
		cocktractContract3.SuccessReward.Add((ItemDeliveryReward)new Item(ItemEnum.PickledOnions));
		cocktractContract3.SuccessReward.Add((MoneyReward)50);
		cocktractContract3.SuccessReward.Add(new CocktractsRespectReward(50f));
		cocktractContract3.FailureReward.Add(new CocktractsRespectReward(-50f));
		cocktractContract3.SetRequirement(new AndRequirement(new AchieveInOneStream(new StreamForXMinutesRequirement(50, new WhileStreamViewershipRequirement(150, 50, 69)), new TimeConstraintRequirement(120)), new AchieveInOneStream(), new AchieveInOneStream(new StreamForXMinutesRequirement())));
		CocktractContract cocktractContract4 = new CocktractContract();
		cocktractContract4.SuccessReward.Add((MoneyReward)100);
		cocktractContract4.SuccessReward.Add((MoneyReward)50);
		cocktractContract4.SuccessReward.Add(new CocktractsRespectReward(50f));
		cocktractContract4.FailureReward.Add(new CocktractsRespectReward(-50f));
		cocktractContract4.SetRequirement(new AndRequirement(new OrRequirement(new StreamForXMinutesRequirement(), new StreamForXMinutesRequirement(), new AndRequirement(new StreamForXMinutesRequirement(), new TimeConstraintRequirement(50))), new TimeConstraintRequirement(10080)));
	}

	public static ContractsAggregateData AggregateData(IEnumerable<CocktractContract> source, ContractsAggregateData cad = null)
	{
		if (cad == null)
		{
			cad = new ContractsAggregateData();
		}
		foreach (CocktractContract item in source)
		{
			switch (item.Status)
			{
			case ContractStatus.Available:
				cad.AvailableCount++;
				break;
			case ContractStatus.Accepted:
				cad.AcceptedCount++;
				break;
			case ContractStatus.Abandoned:
				cad.AbandonedCount++;
				break;
			case ContractStatus.Failed:
				cad.FailedCount++;
				break;
			case ContractStatus.Completed:
				cad.CompletedCount++;
				break;
			}
		}
		return cad;
	}
}
