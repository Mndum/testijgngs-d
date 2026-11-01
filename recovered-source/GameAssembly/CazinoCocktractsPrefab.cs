using System;
using System.Collections.Generic;
using GameItems;
using StoryConditions;
using UnityEngine;

public class CazinoCocktractsPrefab : CommonCocktractPartnerPrefabBase
{
	public override string Key => CocktractPartnerPrefabManager.cazinoId;

	public override List<BrandColorsSet> BrandColorsSets { get; protected set; } = new List<BrandColorsSet>
	{
		new BrandColorsSet(new List<Color> { ColorPaletteManager.CazinoGreen }, new List<Color>
		{
			ColorPaletteManager.CazinoBrown,
			ColorPaletteManager.CazinoGreen
		})
	};

	public override List<ItemEnum> MerchRewardList { get; protected set; } = new List<ItemEnum>
	{
		ItemEnum.CazinoMerchHoodie,
		ItemEnum.CazinoMerchShirt
	};

	public CazinoCocktractsPrefab(int order)
		: base(LOC.Contracts.TupleReferences.Cazino_Name, LOC.Contracts.TupleReferences.Cazino_Description, R.PartnerLogos.Cazino, new List<StoryCondition>
		{
			StoryCondition.FlagSet("casino"),
			StoryCondition.VariableCondition("followers", DataConditionComparators.SingleArgumentComparators.Above, 100),
			StoryCondition.VariableCondition("casinoTokens", DataConditionComparators.SingleArgumentComparators.Above, 10000)
		}, order)
	{
		EmailContainer = EmailPrefabManager.CazinoEmails;
		MilestoneGoals = new List<MilestoneContainer>
		{
			new MilestoneContainer((CocktractPartner partner) =>
			{
				GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
				return partner.Respect > 10f && specialData.CompletedGoldContracts > 2;
			}, (CocktractPartner partner) =>
			{
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.3f;
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
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.1f;
				GenerateMilestoneReward();
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
			LOC.Contracts.TupleReferences.Cazino_Contract_Generic1,
			LOC.Contracts.TupleReferences.Cazino_Contract_Generic2,
			LOC.Contracts.TupleReferences.Cazino_Contract_Generic3
		};
		GenericContractGenerators = new List<Func<CocktractPartner, CocktractContract>>
		{
			(CocktractPartner partner) =>
			{
				CocktractContract cocktractContract = CocktractContract.CreateRandom(partner, 10f);
				cocktractContract.SetNarration(genericDescriptions.GetRandom());
				return cocktractContract;
			}
		};
		GoldContractGenerators = CommonCocktractPartnerPrefabBase.CreateNormalGoldContractProgression((CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Cazino_Contract_First);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Cazino_Contract_Gold1);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Cazino_Contract_Gold2);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Cazino_Contract_Gold3);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Cazino_Contract_Gold4);
			return cocktractContract;
		});
	}

	private Item GenerateMilestoneReward()
	{
		return new Item(ItemEnum.BingChillingMagazineBundle);
	}

	public override void GenerateContractRewards(CocktractPartner partner, CocktractContract c, float difficultyTarget, float rewardsMultiplierBonus)
	{
		GenerateContractMerchRewards(partner, c, difficultyTarget, rewardsMultiplierBonus);
		GameVariables current = GameVariables.Current;
		float num = RewardsMulti(difficultyTarget, rewardsMultiplierBonus);
		c.AddDefaultRespectRewards(num);
		float num2 = current.cocktractManager.GenerateRandomMoneyReward(num);
		c.SuccessReward.Add(new CasinoTokensReward(Mathf.CeilToInt(num2 / 0.8f)));
	}
}
