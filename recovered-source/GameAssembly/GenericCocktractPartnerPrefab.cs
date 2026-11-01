using System.Collections.Generic;
using GameItems;
using StoryConditions;
using UnityEngine;

public class GenericCocktractPartnerPrefab : CocktractPartnerPrefab
{
	public override string Key { get; }

	public override List<BrandColorsSet> BrandColorsSets { get; protected set; } = new List<BrandColorsSet>
	{
		new BrandColorsSet(new List<Color> { Color.magenta }, new List<Color> { Color.magenta })
	};

	public GenericCocktractPartnerPrefab(string key, int order)
		: base(SimpleLocalizedString.CreateInstanceReplacement(key), SimpleLocalizedString.CreateInstanceReplacement(LOC.UI.UI_Error), R.PartnerLogos.Stilou, new List<StoryCondition> { StoryCondition.AlwaysFalseCondition }, order)
	{
		Key = key;
	}

	public override Item GenerateMerchReward()
	{
		return new Item(ItemEnum.Sausage);
	}

	public override CocktractContract GenerateGenericContract(CocktractPartner partner)
	{
		return CocktractContract.CreateBasic(partner, 1);
	}

	public override CocktractContract GenerateGoldContract(CocktractPartner partner)
	{
		return CocktractContract.CreateBasic(partner, 1);
	}

	public override bool HasGoldContract(CocktractPartner partner)
	{
		return false;
	}
}
