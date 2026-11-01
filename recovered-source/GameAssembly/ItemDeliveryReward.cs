using System;

[Serializable]
public class ItemDeliveryReward : Reward
{
	public Item item;

	public ItemDeliveryReward()
	{
	}

	public ItemDeliveryReward(Item item)
	{
		this.item = item;
	}

	protected override void InnerClaim(IRewardSource rewardSource)
	{
		SimpleLocalizedString sender = ((!(rewardSource is CocktractContract cocktractContract)) ? null : cocktractContract.Partner.NameSLS);
		GameVariables.Current.deliveryManager.CreateDelivery(60, sender, item);
	}

	public override string GenerateDescription()
	{
		return LOC.UI.Rewards_Item(item.GetName());
	}

	public static explicit operator ItemDeliveryReward(Item obj)
	{
		return new ItemDeliveryReward(obj);
	}
}
