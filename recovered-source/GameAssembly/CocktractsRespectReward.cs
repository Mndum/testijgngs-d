using System;
using UnityEngine;

[Serializable]
public class CocktractsRespectReward : Reward
{
	public float respect;

	public CocktractsRespectReward()
	{
	}

	public CocktractsRespectReward(float respect)
	{
		this.respect = respect;
	}

	protected override void InnerClaim(IRewardSource rewardSource)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager.AddRespect(respect);
		if (rewardSource is CocktractContract cocktractContract)
		{
			cocktractContract.Partner.AddRespect(respect);
		}
	}

	private static string GenerateAmountStr(float nr)
	{
		nr = Mathf.Abs(nr);
		if (nr <= 0.3f)
		{
			return LOC.UI.Rewards_RespectBasicallyNone;
		}
		if (nr <= 0.6f)
		{
			return LOC.UI.Rewards_RespectTinyAmount;
		}
		if (nr <= 1.1f)
		{
			return LOC.UI.Rewards_RespectNormalAmount;
		}
		if (nr <= 2.1f)
		{
			return LOC.UI.Rewards_RespectDecentAmount;
		}
		if (nr <= 3.1f)
		{
			return LOC.UI.Rewards_RespectBigAmount;
		}
		if (nr <= 4.1f)
		{
			return LOC.UI.Rewards_RespectHugeAmount;
		}
		return LOC.UI.Rewards_RespectGiganticAmount;
	}

	public static string GenerateStringForRespect(float respect)
	{
		if (respect >= 0f)
		{
			return LOC.UI.Rewards_RespectGain(GenerateAmountStr(respect));
		}
		return LOC.UI.Rewards_RespectLoss(GenerateAmountStr(respect));
	}

	public override string GenerateDescription()
	{
		return GenerateStringForRespect(respect);
	}
}
