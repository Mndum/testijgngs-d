using System;

[Serializable]
public class GenericCocktractSpecialData : CocktractPartnerSpecialData
{
	public int GoldContractsGiven;

	public int NextMilestoneLevel;

	public int FailedContracts;

	public int RejectedContracts;

	public int ExpiredContracts;

	public int CompletedContracts;

	public int FailedGoldContracts;

	public int RejectedGoldContracts;

	public int ExpiredGoldContracts;

	public int CompletedGoldContracts;

	public float RewardsMultiplierBonus;

	public int LastMilestoneDoneTime;

	public int LastGoldContractGivenTime;

	public int GoldContractsEnded => FailedGoldContracts + RejectedGoldContracts + ExpiredGoldContracts + CompletedGoldContracts;
}
