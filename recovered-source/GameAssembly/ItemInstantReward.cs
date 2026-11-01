using System;

[Serializable]
public class ItemInstantReward : Reward
{
	public Item item;

	public ItemInstantReward()
	{
	}

	public ItemInstantReward(Item item)
	{
		this.item = item;
	}

	protected override void InnerClaim(IRewardSource rewardSource)
	{
		GameVariables.Current.itemManager.AddItem(item);
	}

	public override string GenerateDescription()
	{
		return LOC.UI.Rewards_Item(item.GetName());
	}

	public static explicit operator ItemInstantReward(Item obj)
	{
		return new ItemInstantReward(obj);
	}
}
