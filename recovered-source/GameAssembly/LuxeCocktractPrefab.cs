using System;
using System.Collections.Generic;
using System.Globalization;
using GameItems;
using StoryConditions;
using UnityEngine;

public class LuxeCocktractPrefab : CommonCocktractPartnerPrefabBase
{
	public override string Key => CocktractPartnerPrefabManager.luxeId;

	public override List<BrandColorsSet> BrandColorsSets { get; protected set; } = new List<BrandColorsSet>
	{
		new BrandColorsSet(new List<Color> { ColorPaletteManager.SatinBlack }, new List<Color>
		{
			ColorPaletteManager.White,
			ColorPaletteManager.EBRed
		})
	};

	public override List<ItemEnum> MerchRewardList { get; protected set; } = new List<ItemEnum>
	{
		ItemEnum.LuxeMerchShirt,
		ItemEnum.LuxeMerchHoodie
	};

	public LuxeCocktractPrefab(int order)
		: base(LOC.Contracts.TupleReferences.Luxe_Name, LOC.Contracts.TupleReferences.Luxe_Description, R.PartnerLogos.Luxe, new List<StoryCondition> { StoryCondition.VariableCondition("followers", DataConditionComparators.SingleArgumentComparators.Above, 100) }, order)
	{
		EmailContainer = EmailPrefabManager.LuxeEmails;
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
					plannedHour = 13,
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
			LOC.Contracts.TupleReferences.Luxe_Contract_Generic1,
			LOC.Contracts.TupleReferences.Luxe_Contract_Generic2,
			LOC.Contracts.TupleReferences.Luxe_Contract_Generic3
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
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Luxe_Contract_First);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			Item item = GenerateLuxeItem1();
			CocktractContract cocktractContract = CocktractContract.CreateBasicItem(partner, 4, item);
			cocktractContract.SetNarration(new SimpleLocalizedStringWithBakedArguments(LOC.Contracts.TupleReferences.Luxe_Contract_Gold1, item.GetRawNameSls().ToBakedArgument()));
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandom(partner, partner.GoldContractDifficulty, 1f);
			cocktractContract.SetNarration(new SimpleLocalizedStringWithBakedArguments(LOC.Contracts.TupleReferences.Luxe_Contract_Gold2, cocktractContract.GetMoneyRewardAmount().ToString(CultureInfo.InvariantCulture)));
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Luxe_Contract_Gold3);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Luxe_Contract_Gold4);
			return cocktractContract;
		});
	}

	public Item GenerateLuxeItem1()
	{
		return Item.CreateRandomItem(ItemEnum.LuxeRosePanties.ToGameId());
	}

	public Item GenerateMilestoneReward()
	{
		return Item.CreateRandomItem(ItemEnum.LuxeLingeriePanties.ToGameId());
	}
}
