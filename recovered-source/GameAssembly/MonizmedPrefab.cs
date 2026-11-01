using System;
using System.Collections.Generic;
using System.Linq;
using GameItems;
using StoryConditions;
using UnityEngine;

public class MonizmedPrefab : CommonCocktractPartnerPrefabBase
{
	public override string Key => CocktractPartnerPrefabManager.monizmedId;

	public override List<BrandColorsSet> BrandColorsSets { get; protected set; } = new List<BrandColorsSet>
	{
		new BrandColorsSet(new List<Color>
		{
			ColorPaletteManager.White,
			ColorPaletteManager.EBYellow
		}, new List<Color>
		{
			ColorPaletteManager.MonizmedPurple,
			ColorPaletteManager.MonizmedBurgundy
		})
	};

	public override List<ItemEnum> MerchRewardList { get; protected set; } = new List<ItemEnum>
	{
		ItemEnum.MonizmedMerchShirt,
		ItemEnum.MonizmedMerchHoodie
	};

	public MonizmedPrefab(int order)
		: base(LOC.Contracts.TupleReferences.Monizmed_Name, LOC.Contracts.TupleReferences.Monizmed_Description, R.PartnerLogos.Monizmed, new List<StoryCondition> { StoryCondition.VariableCondition("followers", DataConditionComparators.SingleArgumentComparators.Above, 1000) }, order)
	{
		EmailContainer = EmailPrefabManager.MonizmedEmails;
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
			LOC.Contracts.TupleReferences.Monizmed_Contract_Generic1,
			LOC.Contracts.TupleReferences.Monizmed_Contract_Generic2,
			LOC.Contracts.TupleReferences.Monizmed_Contract_Generic3
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
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Monizmed_Contract_First);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Monizmed_Contract_Gold1);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Monizmed_Contract_Gold2);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Monizmed_Contract_Gold3);
			return cocktractContract;
		}, (CocktractPartner partner) =>
		{
			CocktractContract cocktractContract = CocktractContract.CreateRandomGold(partner);
			cocktractContract.SetNarration(LOC.Contracts.TupleReferences.Monizmed_Contract_Gold4);
			return cocktractContract;
		});
	}

	public override void GenerateContractRewards(CocktractPartner partner, CocktractContract c, float difficultyTarget, float rewardsMultiplierBonus)
	{
		GenerateContractMerchRewards(partner, c, difficultyTarget, rewardsMultiplierBonus);
		GameVariables current = GameVariables.Current;
		float num = RewardsMulti(difficultyTarget, rewardsMultiplierBonus);
		c.AddDefaultRespectRewards(num);
		float num2 = current.cocktractManager.GenerateRandomMoneyReward(num);
		List<(GameId, ItemPrefab)> list = new ItemEnum[4]
		{
			ItemEnum.Vinegara,
			ItemEnum.DeathGrip,
			ItemEnum.EndurancePlus,
			ItemEnum.SempillX
		}.Select((ItemEnum t) =>
		{
			GameId gameId = t.ToGameId();
			return (gameId: gameId, ItemPrefab.ItemPrefabManager.GetItemById(gameId));
		}).ToList();
		if (list.Count > 0 && UnityEngine.Random.value > 0.5f)
		{
			(GameId, ItemPrefab) random = list.GetRandom();
			int count = Mathf.Clamp(Mathf.FloorToInt(num2 / (float)random.Item2.Price), 1, 5);
			Item item = Item.CreateRandomItem(random.Item1);
			item.SetCount(count);
			c.SuccessReward.Add(new ItemDeliveryReward(item));
			c.AddDefaultCashReward(Mathf.CeilToInt(num2 / 2f));
		}
		else
		{
			c.AddDefaultCashReward(Mathf.CeilToInt(num2));
		}
	}

	public override void OnContractRejected(CocktractPartner partner, CocktractContract contract)
	{
		GenericCocktractSpecialData specialData = partner.GetSpecialData<GenericCocktractSpecialData>();
		if (contract.IsGold)
		{
			if (specialData.RejectedGoldContracts > 0)
			{
				EmailPrefabManager.GetEmail(EmailContainer.GoldRefusalEmail);
			}
			specialData.RejectedGoldContracts++;
		}
	}
}
