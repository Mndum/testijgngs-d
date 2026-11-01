using System;
using System.Collections.Generic;
using GameItems;
using StoryConditions;
using UnityEngine;

public class DiabeteCocktractsPrefab : CommonCocktractPartnerPrefabBase
{
	public static BrandColorsSet MainSet = new BrandColorsSet(new List<Color> { ColorPaletteManager.White }, new List<Color>
	{
		ColorPaletteManager.DiabetePurple,
		ColorPaletteManager.DiabeteGold
	});

	public static BrandColorsSet ColaSet = new BrandColorsSet(new List<Color> { ColorPaletteManager.White }, new List<Color>
	{
		ColorPaletteManager.DiabeteColaBlue,
		ColorPaletteManager.DiabeteGold,
		ColorPaletteManager.DiabeteColaYellow
	});

	public static BrandColorsSet DrSugarSet = new BrandColorsSet(new List<Color> { ColorPaletteManager.DiabeteDrSugarBlue }, new List<Color>
	{
		ColorPaletteManager.DiabeteDrSugarRed,
		ColorPaletteManager.DiabeteDrSugarBlue
	});

	public static BrandColorsSet SweetPotatoSet = new BrandColorsSet(new List<Color> { ColorPaletteManager.DiabeteSweetPotatoBrown }, new List<Color> { ColorPaletteManager.DiabeteSweetPotatoOrange });

	public override string Key => CocktractPartnerPrefabManager.diabeteId;

	public override List<BrandColorsSet> BrandColorsSets { get; protected set; } = new List<BrandColorsSet> { MainSet };

	public override List<ItemEnum> MerchRewardList { get; protected set; } = new List<ItemEnum>
	{
		ItemEnum.DiabeteMerchHoodie,
		ItemEnum.DiabeteMerchShirt,
		ItemEnum.DiabeteColaPowHoodie,
		ItemEnum.DiabeteColaPowShirt,
		ItemEnum.DiabeteDrSugarHoodie,
		ItemEnum.DiabeteDrSugarShirt,
		ItemEnum.DiabeteSweetPotatoHoodie,
		ItemEnum.DiabeteSweetPotatoShirt
	};

	public DiabeteCocktractsPrefab(int order)
		: base(LOC.Contracts.TupleReferences.Diabete_Name, LOC.Contracts.TupleReferences.Diabete_Description, R.PartnerLogos.Diabete, new List<StoryCondition> { StoryCondition.VariableCondition("followers", DataConditionComparators.SingleArgumentComparators.Above, 1000) }, order)
	{
		EmailContainer = EmailPrefabManager.DiabeteEmails;
		MilestoneGoals = new List<MilestoneContainer>
		{
			new MilestoneContainer((CocktractPartner partner) =>
			{
				GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
				return partner.Respect > 10f && specialData.CompletedGoldContracts > 2;
			}, (CocktractPartner partner) =>
			{
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.1f;
				GameVariables.Current.PlanEmail(EmailPrefabManager.GetEmail(EmailContainer.Milestone1Email), new EventManager.PlanningData
				{
					hourOffset = 15,
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
				partner.GetSpecialData<GenericCocktractSpecialData>().RewardsMultiplierBonus += 0.2f;
				GameVariables.Current.PlanEmail(EmailPrefabManager.GetEmail(EmailContainer.Milestone2Email), new EventManager.PlanningData
				{
					hourOffset = 15,
					plannedHour = 10,
					plannedMinute = 22,
					forceHourInDay = true
				});
			})
		};
		List<LOS.TupleReference> genericDescriptions = new List<LOS.TupleReference>
		{
			LOC.Contracts.TupleReferences.Diabete_Contract_Generic1,
			LOC.Contracts.TupleReferences.Diabete_Contract_Generic2,
			LOC.Contracts.TupleReferences.Diabete_Contract_Generic3
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
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Diabete_Contract_First);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Diabete_Contract_Gold1);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Diabete_Contract_Gold2);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Diabete_Contract_Gold3);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Diabete_Contract_Gold4);
			return cocktractContract;
		});
	}
}
