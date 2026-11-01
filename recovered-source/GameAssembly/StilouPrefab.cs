using System;
using System.Collections.Generic;
using GameItems;
using StoryConditions;
using UnityEngine;

public class StilouPrefab : CommonCocktractPartnerPrefabBase
{
	public override string Key => CocktractPartnerPrefabManager.stilouId;

	public override List<BrandColorsSet> BrandColorsSets { get; protected set; } = new List<BrandColorsSet>
	{
		new BrandColorsSet(new List<Color> { ColorPaletteManager.StilouBlue }, new List<Color>
		{
			ColorPaletteManager.StilouRed,
			ColorPaletteManager.StilouYellow
		})
	};

	public override List<ItemEnum> MerchRewardList { get; protected set; } = new List<ItemEnum>
	{
		ItemEnum.StilouMerchHoodie,
		ItemEnum.StilouMerchShirt
	};

	public StilouPrefab(int order)
		: base(LOC.Contracts.TupleReferences.Stilou_Name, LOC.Contracts.TupleReferences.Stilou_Description, R.PartnerLogos.Stilou, new List<StoryCondition> { StoryCondition.VariableCondition("followers", DataConditionComparators.SingleArgumentComparators.Above, 100) }, order)
	{
		EmailContainer = EmailPrefabManager.StilouEmails;
		MilestoneGoals = new List<MilestoneContainer>
		{
			new MilestoneContainer((CocktractPartner partner) =>
			{
				GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
				return partner.Respect > 10f && specialData.CompletedGoldContracts > 2;
			}, (CocktractPartner partner) =>
			{
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.2f;
				GameVariables.Current.PlanEmail(EmailPrefabManager.GetEmail(EmailContainer.Milestone1Email), new EventManager.PlanningData
				{
					hourOffset = 4,
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
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.3f;
				Item item = GenerateMilestoneReward();
				GameVariables.Current.deliveryManager.CreateDelivery(60, Name, item);
				GameVariables.Current.PlanEmail(EmailPrefabManager.GetEmail(EmailContainer.Milestone2Email), new EventManager.PlanningData
				{
					hourOffset = 4,
					plannedHour = 10,
					plannedMinute = 22,
					forceHourInDay = true
				});
			})
		};
		List<LOS.TupleReference> genericDescriptions = new List<LOS.TupleReference>
		{
			LOC.Contracts.TupleReferences.Stilou_Contract_Generic1,
			LOC.Contracts.TupleReferences.Stilou_Contract_Generic2,
			LOC.Contracts.TupleReferences.Stilou_Contract_Generic3
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
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Stilou_Contract_First);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Stilou_Contract_Gold1);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Stilou_Contract_Gold2);
			GameVariables.Current.newsDataManager.Force(NewsDataManager.NewsId.MainNews, GameId.CreateVanilla(1002));
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Stilou_Contract_Gold3);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Stilou_Contract_Gold4);
			return cocktractContract;
		});
	}

	private Item GenerateMilestoneReward()
	{
		return new Item(ItemEnum.PlainShirt);
	}
}
