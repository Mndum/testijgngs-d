using System;

[Serializable]
public class MoneyReward : Reward
{
	public int money;

	public MoneyReward()
	{
	}

	public MoneyReward(int money)
	{
		this.money = money;
	}

	protected override void InnerClaim(IRewardSource rewardSource)
	{
		GameVariables.Current.AddMoney(money);
	}

	public override string GenerateDescription()
	{
		return LOC.UI.Rewards_Money(money);
	}

	public static implicit operator MoneyReward(int obj)
	{
		return new MoneyReward(obj);
	}
}
