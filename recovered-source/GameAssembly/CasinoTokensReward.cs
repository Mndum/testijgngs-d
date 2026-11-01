using System;
using UnityEngine;

[Serializable]
public class CasinoTokensReward : Reward
{
	public int tokens;

	public CasinoTokensReward()
	{
	}

	public CasinoTokensReward(int tokens)
	{
		this.tokens = tokens;
	}

	protected override void InnerClaim(IRewardSource rewardSource)
	{
		GameVariables.Current.casinoTokens += tokens;
	}

	public override string GenerateDescription()
	{
		return LOC.UI.Rewards_Tokens(tokens, Mathf.CeilToInt((float)tokens * 0.8f));
	}
}
