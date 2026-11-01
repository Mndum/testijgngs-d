using System;
using System.Collections.Generic;
using Fungus;

public class StoryEntryPointGenericFlagBase<TBranch> : StoryEntryPointBase<TBranch> where TBranch : StoryEntryPointBase.StoryBranchGenericFlagBase
{
	public List<TBranch> Branches { get; private set; }

	public Action<TBranch> OnActivation { get; set; }

	public string Id { get; protected set; }

	public StoryEntryPointGenericFlagBase(string id, List<TBranch> branches)
	{
		Id = id;
		Branches = branches;
	}

	protected Action BeforeFromCommand(Command command, bool waitTillBlockFinished)
	{
		Action result;
		if (waitTillBlockFinished)
		{
			result = command.Continue;
		}
		else
		{
			result = null;
			command.StopParentBlock();
		}
		return result;
	}

	public void ActivateBranch(Command callingCommand, TBranch branch)
	{
		if (!string.IsNullOrWhiteSpace(branch.RelatedFlag))
		{
			GameVariables.Current.AddFlag(branch.RelatedFlag);
		}
		OnActivation?.Invoke(branch);
		branch.OnActivateAction?.Invoke();
		Action onFinished = BeforeFromCommand(callingCommand, branch.WaitTillBlockFinished);
		branch.BranchTarget.Activate(onFinished);
	}

	public override IEnumerable<TBranch> GetBranches()
	{
		return Branches;
	}
}
