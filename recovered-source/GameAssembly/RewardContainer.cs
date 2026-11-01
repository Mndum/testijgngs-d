using System;

[Serializable]
public class RewardContainer : PolymorphicSerializableContainer<RewardContainer, Reward>
{
	public static readonly (Type type, string prefix)[] _polymorphicTypes = new (Type, string)[4]
	{
		(typeof(MoneyReward), "M"),
		(typeof(ItemDeliveryReward), "I"),
		(typeof(CocktractsRespectReward), "R"),
		(typeof(CasinoTokensReward), "T")
	};

	public override (Type type, string prefix)[] PolymorphicTypes => _polymorphicTypes;

	public RewardContainer(Reward deserializedObject)
		: base(deserializedObject)
	{
	}

	public static implicit operator RewardContainer(Reward obj)
	{
		return new RewardContainer(obj);
	}
}
