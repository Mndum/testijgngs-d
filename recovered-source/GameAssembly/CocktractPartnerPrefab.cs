using System.Collections.Generic;
using IC.AutomaticReference;
using StoryConditions;
using UnityEngine;

public abstract class CocktractPartnerPrefab : IKey<string>
{
	public class BrandColorsSet
	{
		public List<Color> BrandColorsMain { get; private set; }

		public List<Color> BrandColorsAccent { get; private set; }

		public BrandColorsSet(List<Color> brandColorsMain, List<Color> brandColorsAccent)
		{
			BrandColorsMain = brandColorsMain;
			BrandColorsAccent = brandColorsAccent;
		}

		public virtual IEnumerable<Color> GetBrandColors(int count)
		{
			if (count > 0)
			{
				yield return BrandColorsMain.GetRandom();
				int offset = Random.Range(0, BrandColorsAccent.Count);
				for (int i = 1; i < count; i++)
				{
					yield return BrandColorsAccent[(i + offset) % BrandColorsAccent.Count];
				}
			}
		}
	}

	public abstract string Key { get; }

	public SimpleLocalizedString Name { get; private set; }

	public SimpleLocalizedString Description { get; private set; }

	public IReference<Sprite> LogoSprite { get; private set; }

	public abstract List<BrandColorsSet> BrandColorsSets { get; protected set; }

	public List<StoryCondition> UnlockConditions { get; private set; }

	public int Order { get; private set; }

	public CocktractPartnerPrefab(SimpleLocalizedString name, SimpleLocalizedString description, IReference<Sprite> logoSprite, List<StoryCondition> unlockConditions, int order)
	{
		Name = name;
		Description = description;
		LogoSprite = logoSprite;
		UnlockConditions = unlockConditions;
		Order = order;
	}

	protected IEnumerator<Color> BrandColorsFromList(int count, List<Color> colors)
	{
		for (int i = 0; i < count; i++)
		{
			yield return colors[i % colors.Count];
		}
	}

	public abstract Item GenerateMerchReward();

	public BrandColorsSet GetRandomBrandColorsSet()
	{
		return BrandColorsSets.GetRandom();
	}

	public abstract CocktractContract GenerateGenericContract(CocktractPartner partner);

	public abstract CocktractContract GenerateGoldContract(CocktractPartner partner);

	public abstract bool HasGoldContract(CocktractPartner partner);

	public virtual void EndOfDay(CocktractPartner partner)
	{
	}

	public virtual void OnContractChangedStatus(CocktractPartner partner, CocktractContract contract)
	{
	}

	public virtual void AddAllPossibleContracts(CocktractPartner cocktractPartner, List<CocktractContract> allContracts)
	{
	}

	public virtual float CalculateContractDifficulty(CocktractPartner cocktractPartner)
	{
		return 1f;
	}

	public virtual float CalculateRewardsBonusMultiplier(CocktractPartner cocktractPartner)
	{
		return 0f;
	}

	public virtual void GenerateContractRewards(CocktractPartner partner, CocktractContract cocktractContract, float rewardsMultiplier, float rewardsMultiplierBonus)
	{
	}

	public virtual bool HasGenericContract(CocktractPartner cocktractPartner)
	{
		return false;
	}
}
