using System;
using System.Collections.Generic;
using GameItems;
using StoryConditions;
using UnityEngine;

public class VolksautoCocktractsPrefab : CommonCocktractPartnerPrefabBase
{
	public override string Key => CocktractPartnerPrefabManager.volksAutoId;

	public override List<BrandColorsSet> BrandColorsSets { get; protected set; } = new List<BrandColorsSet>
	{
		new BrandColorsSet(new List<Color> { ColorPaletteManager.White }, new List<Color> { ColorPaletteManager.VolksAutoGrey })
	};

	public override List<ItemEnum> MerchRewardList { get; protected set; } = new List<ItemEnum>
	{
		ItemEnum.VolksAutoMerchShirt,
		ItemEnum.VolksAutoMerchHoodie
	};

	public VolksautoCocktractsPrefab(int order)
		: base(LOC.Contracts.TupleReferences.VolksAuto_Name, LOC.Contracts.TupleReferences.VolksAuto_Description, R.PartnerLogos.RehabTech, new List<StoryCondition> { StoryCondition.VariableCondition("followers", DataConditionComparators.SingleArgumentComparators.Above, 3000) }, order)
	{
		EmailContainer = EmailPrefabManager.VolksAutoEmails;
		MilestoneGoals = new List<MilestoneContainer>
		{
			new MilestoneContainer((CocktractPartner partner) =>
			{
				GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
				return partner.Respect > 10f && specialData.CompletedGoldContracts > 2;
			}, (CocktractPartner partner) =>
			{
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.15f;
				GameVariables.Current.PlanEmail(EmailPrefabManager.GetEmail(EmailContainer.Milestone1Email), new EventManager.PlanningData
				{
					hourOffset = 5,
					plannedHour = 10,
					plannedMinute = 38,
					forceHourInDay = true
				});
			}),
			new MilestoneContainer((CocktractPartner partner) =>
			{
				GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
				return partner.Respect > 20f && specialData.CompletedGoldContracts > 3;
			}, (CocktractPartner partner) =>
			{
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.15f;
				Item item = GenerateMilestoneReward();
				GameVariables.Current.deliveryManager.CreateDelivery(60, Name, item);
				GameVariables.Current.PlanEmail(EmailPrefabManager.GetEmail(EmailContainer.Milestone2Email), new EventManager.PlanningData
				{
					hourOffset = 5,
					plannedHour = 10,
					plannedMinute = 22,
					forceHourInDay = true
				});
			})
		};
		List<LOS.TupleReference> genericDescriptions = new List<LOS.TupleReference>
		{
			LOC.Contracts.TupleReferences.VolksAuto_Contract_Generic1,
			LOC.Contracts.TupleReferences.VolksAuto_Contract_Generic2,
			LOC.Contracts.TupleReferences.VolksAuto_Contract_Generic3
		};
		GenericContractGenerators = new List<Func<CocktractPartner, CocktractContract>>
		{
			(CocktractPartner partner) =>
			{
				CocktractContract cocktractContract = CocktractContract.CreateRandom(partner);
				cocktractContract.SetNarration(genericDescriptions.GetRandom());
				return cocktractContract;
			}
		};
		GoldContractGenerators = CommonCocktractPartnerPrefabBase.CreateNormalGoldContractProgression((CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.VolksAuto_Contract_First);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.VolksAuto_Contract_Gold1);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			Item item = GenerateRehabTechArmItem();
			CocktractContract cocktractContract = CocktractContract.CreateBasicItem(partner, 12, item);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.VolksAuto_Contract_Gold2);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.VolksAuto_Contract_Gold3);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			Item[] items = GenerateRehabTechLegItem();
			CocktractContract cocktractContract = CocktractContract.CreateBasicItem(partner, 4, items);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.VolksAuto_Contract_Gold4);
			return cocktractContract;
		});
	}

	private Item GenerateMilestoneReward()
	{
		Item item = Item.CreateRandomItem(ItemEnum.VolksAutoBasicArmL.ToGameId());
		item.SetQuality(1.2f);
		return item;
	}

	public Item GenerateRehabTechArmItem()
	{
		Item item = Item.CreateRandomItem(ItemEnum.VolksAutoBasicArmR.ToGameId());
		item.SetQuality(1.2f);
		return item;
	}

	public Item[] GenerateRehabTechLegItem()
	{
		Item[] array = new Item[2]
		{
			Item.CreateRandomItem(ItemEnum.VolksAutoBasicLegR.ToGameId()),
			null
		};
		array[0].SetQuality(1.2f);
		array[1] = Item.CreateRandomItem(ItemEnum.VolksAutoBasicLegL.ToGameId());
		array[1].SetQuality(1.2f);
		return array;
	}
}
