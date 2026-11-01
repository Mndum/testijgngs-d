using System;
using UnityEngine;

[Serializable]
public class WhileWearingItemRequirement : EquippedItemRequirement
{
	private const float DifBase = 0.1f;

	[field: SerializeField]
	public Item Item { get; private set; }

	[field: SerializeField]
	public bool EnforceId { get; private set; }

	[field: SerializeField]
	public bool CheckColors { get; private set; }

	public WhileWearingItemRequirement()
	{
	}

	public WhileWearingItemRequirement(Item item, bool enforceId = true, bool checkColors = false)
	{
		Item = item;
		EnforceId = enforceId;
		CheckColors = checkColors;
	}

	public override float GetDifficulty()
	{
		float num = 0.1f;
		if (!IncludedInOnAcceptBonus())
		{
			num += Mathf.Log10(Item.GetItemPrefab().Price);
			if (CheckColors)
			{
				num *= 2f;
			}
		}
		return num;
	}

	public override bool CheckIfPossible()
	{
		return true;
	}

	public override bool CheckIfDone()
	{
		return SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.itemManager.IsEquippedComplex(Item, EnforceId, CheckColors);
	}

	public override bool Fulfills(Item item)
	{
		return item.Equals_Complex(Item, EnforceId, CheckColors);
	}

	protected override string GenerateDescriptionInternal()
	{
		bool flag = false;
		foreach (RewardContainer item in BCocktractContract.AcceptBonusReward)
		{
			if (item.Value is ItemDeliveryReward itemDeliveryReward && itemDeliveryReward.item.Equals_Complex(Item, EnforceId, CheckColors))
			{
				flag = true;
			}
		}
		StringBuilderCache.AcquireManual(out var stringBuilder);
		ItemNameIncludeEnum itemNameIncludeEnum = ItemNameIncludeEnum.Default;
		if (!CheckColors)
		{
			itemNameIncludeEnum &= ~ItemNameIncludeEnum.ColorInfo;
		}
		stringBuilder.Append(LOC.UI.Requirement_WhileWearing(Item.GetName(itemNameIncludeEnum)));
		if (flag)
		{
			stringBuilder.Append("\n");
			stringBuilder.Append(Requirement.GetDepthPrefix(b_depth, " "));
			stringBuilder.Append(EnforceId ? LOC.UI.Requirement_OnlyProvidedItemCounts : LOC.UI.Requirement_AnyItemCounts);
			stringBuilder.Append("\n");
			stringBuilder.Append(Requirement.GetDepthPrefix(b_depth, " "));
			stringBuilder.Append(LOC.UI.Requirement_ItemIncludedInOnAcceptBonus);
		}
		else if (EnforceId)
		{
			stringBuilder.Append("\n");
			stringBuilder.Append(Requirement.GetDepthPrefix(b_depth, " "));
			stringBuilder.Append(LOC.UI.Requirement_OnlyCertainItemWorks);
		}
		if (CheckColors)
		{
			stringBuilder.Append("\n");
			stringBuilder.Append(Requirement.GetDepthPrefix(b_depth, " "));
			stringBuilder.Append(LOC.UI.Requirement_ColorsMustMatch);
		}
		return StringBuilderCache.ReleaseWithToString(stringBuilder);
	}
}
