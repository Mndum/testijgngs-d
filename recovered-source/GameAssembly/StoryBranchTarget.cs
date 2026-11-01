using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;

public abstract class StoryBranchTarget
{
	public abstract void Activate(Action onFinished);

	public abstract Result CheckValidity(Dictionary<string, Block> blocks);

	public static implicit operator StoryBranchTarget(FungusBlocks.FungusBlockReference b)
	{
		return new StoryBlockTarget(b);
	}

	public static implicit operator StoryBranchTarget(LOS.TupleReference b)
	{
		return new StoryLocalizedStringBranchTarget(b);
	}

	public static implicit operator StoryBranchTarget(SerializedLocalizedConversation b)
	{
		return new StoryLocalizedConversationBranchTarget(b);
	}

	public static implicit operator StoryBranchTarget(Func<IEnumerator> b)
	{
		return new StoryEnumeratorTarget(b);
	}
}
