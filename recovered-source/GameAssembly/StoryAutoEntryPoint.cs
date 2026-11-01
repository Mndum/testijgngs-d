using System.Linq;

public sealed class StoryAutoEntryPoint(string id, params StoryAutoBranch[] branches) : StoryEntryPointGenericFlagBase<StoryAutoBranch>(id, branches.ToList())
{
	public float NothingChosenOdds = 0.5f;

	public StoryAutoBranch GetBranch()
	{
		foreach (StoryAutoBranch branch in Branches)
		{
			if (branch.IsLinear && branch.CanHappen)
			{
				return branch;
			}
		}
		Lottery<StoryAutoBranch> lottery = new Lottery<StoryAutoBranch>();
		lottery.AddItems(Branches.Where((StoryAutoBranch t) => !t.IsLinear && t.CanHappen), (StoryAutoBranch x) => x.RandomOdds);
		if (NothingChosenOdds > 0f)
		{
			lottery.AddItem(null, NothingChosenOdds);
		}
		return lottery.GetRandomOrDefault();
	}

	public bool HasLinearBranchThatCanHappen()
	{
		foreach (StoryAutoBranch branch in Branches)
		{
			if (branch.IsLinear && branch.CanHappen)
			{
				return true;
			}
		}
		return false;
	}

	public bool AnyPossible()
	{
		return Branches.Any((StoryAutoBranch x) => x.CanHappen);
	}
}
