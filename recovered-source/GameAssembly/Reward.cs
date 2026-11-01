using System;
using UnityEngine;

[Serializable]
public abstract class Reward
{
	[SerializeField]
	private bool _claimed;

	public bool Claimed => _claimed;

	public abstract string GenerateDescription();

	public void Claim(IRewardSource rewardSource)
	{
		_claimed = true;
		InnerClaim(rewardSource);
	}

	protected abstract void InnerClaim(IRewardSource rewardSource);

	public RewardContainer ToRewardContainer()
	{
		return new RewardContainer(this);
	}
}
