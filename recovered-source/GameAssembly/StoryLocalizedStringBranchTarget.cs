using System;
using System.Collections.Generic;
using Fungus;

public class StoryLocalizedStringBranchTarget : StoryBranchTarget, IBotDialogueBranchTarget
{
	public SimpleLocalizedString TargetText { get; private set; }

	public bool WaitForInput { get; private set; }

	public StoryLocalizedStringBranchTarget(LOS.TupleReference targetText, bool waitForInput = false)
	{
		TargetText = targetText;
		WaitForInput = waitForInput;
	}

	public StoryLocalizedStringBranchTarget(string tableReference, string tableEntryReference, bool waitForInput = false)
	{
		TargetText = SimpleLocalizedString.CreateInstance(tableReference, tableEntryReference);
		WaitForInput = waitForInput;
	}

	public override void Activate(Action onFinished)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.StartCoroutine(SingletonMonoBehaviour<FungusEnumerators>.Instance.DoConversation(TargetText.GetValue(), WaitForInput).Then(onFinished));
	}

	public override Result CheckValidity(Dictionary<string, Block> blocks)
	{
		if (TargetText.Reference.IsNullOrEmpty())
		{
			return Result.FailWithDescription("Target empty!");
		}
		if (string.IsNullOrWhiteSpace(TargetText.GetValue()))
		{
			return Result.FailWithDescription($"Entry: {TargetText.Reference} empty!");
		}
		return Result.Success;
	}

	public void Activate(ModelBrainState modelBrainState, bool noWrapper)
	{
		modelBrainState.Say(StringUtils.SplitStringAndPickRandom(TargetText.GetValue()), WaitForInput, null, noWrapper);
	}

	public override string ToString()
	{
		return TargetText.ToString();
	}

	public StoryBotDialogueBranchType GetBranchType()
	{
		return StoryBotDialogueBranchType.Comment;
	}
}
