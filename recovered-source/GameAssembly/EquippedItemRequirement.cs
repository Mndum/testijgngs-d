using System.Collections.Generic;
using System.Linq;

public abstract class EquippedItemRequirement : Requirement
{
	public abstract bool Fulfills(Item item);

	public bool IncludedInOnAcceptBonus()
	{
		if (BCocktractContract == null)
		{
			return false;
		}
		foreach (RewardContainer item in BCocktractContract.AcceptBonusReward)
		{
			if (item.Value is ItemDeliveryReward itemDeliveryReward && Fulfills(itemDeliveryReward.item))
			{
				return true;
			}
		}
		return false;
	}

	public static void AddRequiredFolderIfContractNeedsAnyItem(List<Folder<Item>> folders)
	{
		List<EquippedItemRequirement> list = MyCollectionPool<EquippedItemRequirement>.GetList();
		SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cocktractManager.GetAllRequirements(ref list);
		if (list.Count > 0)
		{
			folders.Add(new Folder<Item>(() => LOC.UI.Equipment_Contract, (Item x) => list.Any((EquippedItemRequirement req) => req.Fulfills(x))));
		}
		else
		{
			MyCollectionPool<EquippedItemRequirement>.Release(list);
		}
	}
}
