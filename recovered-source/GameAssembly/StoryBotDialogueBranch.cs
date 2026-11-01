using System;
using JetBrains.Annotations;
using StoryConditions;
using UnityEngine;

public sealed class StoryBotDialogueBranch : StoryEntryPointBase.StoryBranchGenericBase
{
	public const float MaxOdds = 1E+09f;

	public readonly GameId Id;

	public SimpleLocalizedString TitleSls;

	public StoryBotDialogueBranchContainer SourceContainer { get; private set; }

	public StoryBotDialogueTag SourceTag => SourceContainer.SourceTag;

	public StoryBotDialogueStage SourceStage => SourceTag.SourceStage;

	public int Priority { get; private set; }

	public float Odds { get; private set; } = 1f;

	public bool IsOneTime { get; private set; }

	public StoryBotDialogueBranchType BranchType { get; private set; }

	public SceneEnumFlag AllowedScenes { get; private set; } = SceneEnumFlag.All;

	public string Title => TitleSls?.GetValue();

	public void InitSource(StoryBotDialogueBranchContainer sourceContainer)
	{
		SourceContainer = sourceContainer;
	}

	public StoryBotDialogueBranch(GameId id, StoryBranchTarget branchTarget)
		: base(branchTarget)
	{
		Id = id;
		InitBranchType();
	}

	[UsedInLua]
	public StoryBotDialogueBranch(string text, Guid guid, int index, bool waitForInput = false)
		: base(new StoryLocalizedConversationBranchTarget(text, guid, index, waitForInput))
	{
		Id = GameId.CreateGeneric(guid, index);
		InitBranchType();
	}

	private void InitBranchType()
	{
		if (BranchTarget is IBotDialogueBranchTarget botDialogueBranchTarget)
		{
			BranchType = botDialogueBranchTarget.GetBranchType();
		}
		else
		{
			BranchType = StoryBotDialogueBranchType.Story;
		}
	}

	public StoryBotDialogueBranch SetOnActivateAction(Action action)
	{
		OnActivateAction = action;
		return this;
	}

	public StoryBotDialogueBranch SetOdds(float odds)
	{
		Odds = odds;
		return this;
	}

	public StoryBotDialogueBranch SetMaxOdds()
	{
		Odds = 1E+09f;
		return this;
	}

	public StoryBotDialogueBranch MarkOneTime()
	{
		IsOneTime = true;
		return this;
	}

	public StoryBotDialogueBranch SetPriority(int i)
	{
		Priority = i;
		return this;
	}

	public StoryBotDialogueBranch AddConditionsAtFront(params StoryCondition[] addedAdditionalConditions)
	{
		AdditionalConditions = AdditionalConditions.AddConditionsAtFront(addedAdditionalConditions);
		return this;
	}

	public StoryBotDialogueBranch AddConditions(params StoryCondition[] addedAdditionalConditions)
	{
		AdditionalConditions = AdditionalConditions.AddConditionsAtEnd(addedAdditionalConditions);
		return this;
	}

	public StoryBotDialogueBranch SetAllowedScenes(SceneEnumFlag allowedScenes)
	{
		AllowedScenes = allowedScenes;
		return this;
	}

	public bool CheckAdditionalConditionsOffset(int offset)
	{
		for (int i = offset; i < AdditionalConditions.Length; i++)
		{
			if (!AdditionalConditions[i].ConditionMet())
			{
				return false;
			}
		}
		return true;
	}

	[MustUseReturnValue]
	public StoryBranchTarget ActivateGetTarget()
	{
		StoryBotDialogueBranchContainer sourceContainer = SourceContainer;
		Debug.Log($"Started {Id}");
		OnActivateAction?.Invoke();
		if (IsOneTime)
		{
			sourceContainer.GetMemory().AddIgnored(Id);
			if (sourceContainer.StoryBotContainerType == StoryBotContainerTypeEnum.Prologue)
			{
				GameVariables.Current.UniqueConversationsLeft++;
			}
		}
		return BranchTarget;
	}
}
