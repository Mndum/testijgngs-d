using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

public class StoryMenuEntryPoint : StoryEntryPointGenericFlagBase<StoryMenuBranch>
{
	public StoryMenuEntryPoint(string id, [ItemNotNull] params StoryMenuBranch[] branches)
		: base(id, branches.ToList())
	{
	}

	public IEnumerable<(StoryMenuBranch branches, bool interactable)> GetCurrentMenus()
	{
		foreach (StoryMenuBranch branch in Branches)
		{
			if (branch.CanHappen)
			{
				yield return (branches: branch, interactable: true);
			}
			else if (branch.ShowerGrayedOutWhenUnavailable)
			{
				yield return (branches: branch, interactable: false);
			}
		}
	}

	public bool AnyPossible()
	{
		return Branches.Any((StoryMenuBranch x) => x.CanHappen);
	}

	public bool AnyWithConditionsOrFlagPossible()
	{
		return Branches.Any((StoryMenuBranch x) => (x.AdditionalConditions.Length != 0 || !string.IsNullOrEmpty(x.RelatedFlag)) && x.CanHappen);
	}
}
