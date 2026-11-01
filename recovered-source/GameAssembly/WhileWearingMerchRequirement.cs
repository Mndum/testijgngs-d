using System;
using System.Text;
using UnityEngine;

[Serializable]
public class WhileWearingMerchRequirement : EquippedItemRequirement
{
	private const float DifficultyBase = 0.1f;

	public override bool Fulfills(Item item)
	{
		return item.GetItemPrefab().IsMadeByPartner(BCocktractContract.PartnerId);
	}

	public override float GetDifficulty()
	{
		float num = 0.1f;
		if (!IncludedInOnAcceptBonus())
		{
			num *= 2f;
		}
		return num;
	}

	public override bool CheckIfPossible()
	{
		return true;
	}

	public override bool CheckIfDone()
	{
		return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.IsEquippedMerch(BCocktractContract.PartnerId);
	}

	protected override string GenerateDescriptionInternal()
	{
		bool flag = IncludedInOnAcceptBonus();
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			stringBuilder.Append(LOC.UI.Requirement_WhileWearingMerch(BCocktractContract.Partner.Name));
			if (flag)
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(Requirement.GetDepthPrefix(b_depth, " "));
				stringBuilder.Append(LOC.UI.Requirement_ItemIncludedInOnAcceptBonus);
			}
			else if (!GameVariables.Current.itemManager.HasAnyMerch(BCocktractContract.PartnerId))
			{
				stringBuilder.Append("\n");
				stringBuilder.Append(Requirement.GetDepthPrefix(b_depth, " "));
				stringBuilder.TMPColorSurroundStringBuilderAppend(Color.red, LOC.UI.Requirement_YouDontHaveAnyMerch);
			}
			return stringBuilder.ToString();
		}
	}
}
