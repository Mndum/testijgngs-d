using StoryConditions;

public sealed class StoryAutoBranch : StoryEntryPointBase.StoryBranchGenericFlagBase
{
	public float RandomOdds { get; set; } = -1f;

	public bool IsLinear => RandomOdds <= 0f;

	public StoryAutoBranch(StoryBranchTarget branchTarget, bool waitTillFinished, string relatedFlag, float randomOdds, params StoryCondition[] additionalConditions)
		: base(branchTarget, relatedFlag, additionalConditions)
	{
		RandomOdds = randomOdds;
		WaitTillBlockFinished = waitTillFinished;
	}

	public StoryAutoBranch(StoryBranchTarget branchTarget, bool waitTillFinished, string relatedFlag, params StoryCondition[] additionalConditions)
		: base(branchTarget, relatedFlag, additionalConditions)
	{
		WaitTillBlockFinished = waitTillFinished;
	}

	public StoryAutoBranch(StoryBranchTarget branchTarget, string relatedFlag, params StoryCondition[] additionalConditions)
		: base(branchTarget, relatedFlag, additionalConditions)
	{
	}
}
