using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryBotDialogueStage : StoryEntryPointBase<StoryBotDialogueBranch>
{
	public readonly string OldOriginalSource;

	public readonly string OldOriginalSourceLinear;

	public readonly string OldOriginalKeyPrefix;

	private Dictionary<Guid, int> _lastIdUsed = new Dictionary<Guid, int>();

	private Dictionary<string, StoryBotDialogueTag> _tagged = new Dictionary<string, StoryBotDialogueTag>();

	public const string MainDialogueKey = "Interact_MainDialogue";

	public StoryBotDialogueTag MainDialogue { get; }

	public IEnumerable<StoryBotDialogueBranchContainer> AllContainers => _tagged.SelectMany((KeyValuePair<string, StoryBotDialogueTag> t) => t.Value.AllContainers);

	public IEnumerable<KeyValuePair<string, StoryBotDialogueTag>> AllTags => _tagged;

	public StoryBotDialogueStage(string oldOriginalSource)
	{
		MainDialogue = GetTag("Interact_MainDialogue");
		OldOriginalSource = oldOriginalSource;
		OldOriginalSourceLinear = oldOriginalSource + "Linear";
		OldOriginalKeyPrefix = oldOriginalSource.Split('/').Last();
	}

	public DialogueChainMemory GetMemory(GameVariables gameVariables)
	{
		return gameVariables.GetDialogueChainMemory(OldOriginalSource);
	}

	public DialogueChainMemory GetMemory()
	{
		return GetMemory(GameVariables.Current);
	}

	public bool TryGetBranchById(GameId id, out StoryBotDialogueBranch branch)
	{
		foreach (StoryBotDialogueBranchContainer allContainer in AllContainers)
		{
			if (allContainer.TryGetBranchById(id, out branch))
			{
				return true;
			}
		}
		branch = null;
		return false;
	}

	private void RegisterUsedId(GameId source)
	{
		if (_lastIdUsed.TryGetValue(source.Guid, out var value))
		{
			if (source.Id > value)
			{
				_lastIdUsed[source.Guid] = source.Id;
			}
		}
		else
		{
			_lastIdUsed[source.Guid] = source.Id;
		}
	}

	public int GetNextFreeId(Guid source)
	{
		if (_lastIdUsed.TryGetValue(source, out var value))
		{
			return value + 1;
		}
		return 0;
	}

	public int GetNextFreeVanillaId()
	{
		return GetNextFreeId(Guid.Empty);
	}

	public bool TryGetLastUsedVanillaIdEnum(Stage3VanillaDialogueIds dialogue, out int lastUsed)
	{
		int lastPossibleIndex = dialogue.GetLastPossibleIndex();
		lastUsed = (int)(dialogue - 1);
		foreach (StoryBotDialogueBranchContainer allContainer in AllContainers)
		{
			foreach (StoryBotDialogueBranch allBranch in allContainer.AllBranches)
			{
				int id = allBranch.Id.Id;
				if (id >= (int)dialogue && id < lastPossibleIndex && allBranch.Id.IsVanilla && id > lastUsed)
				{
					lastUsed = id;
				}
			}
		}
		return lastUsed >= (int)dialogue;
	}

	public int GetNextSuggestedVanillaContainerStart()
	{
		int nextFreeVanillaId = GetNextFreeVanillaId();
		return nextFreeVanillaId + (1000 - nextFreeVanillaId % 1000);
	}

	public bool TryGetTag(string tag, out StoryBotDialogueTag container)
	{
		return _tagged.TryGetValue(tag, out container);
	}

	public StoryBotDialogueTag GetTag(string tagName)
	{
		if (TryGetTag(tagName, out var container))
		{
			return container;
		}
		StoryBotDialogueTag storyBotDialogueTag = new StoryBotDialogueTag();
		storyBotDialogueTag.InitSource(this);
		_tagged[tagName] = storyBotDialogueTag;
		return storyBotDialogueTag;
	}

	public override IEnumerable<StoryBotDialogueBranch> GetBranches()
	{
		return AllContainers.SelectMany((StoryBotDialogueBranchContainer t) => t.AllBranches);
	}

	public bool IsPrologueFinished()
	{
		if (MainDialogue.TryGetByContainerType(StoryBotContainerTypeEnum.Prologue, out var values))
		{
			return values.All((StoryBotDialogueBranchContainer t) => t.IsLastFinished());
		}
		return false;
	}

	public int GetBranchCount()
	{
		return AllContainers.Sum((StoryBotDialogueBranchContainer t) => t.BranchCount);
	}

	public void AddVanilla(out StoryBotDialogueBranchContainer main, out StoryBotDialogueBranchContainer prologue)
	{
		MainDialogue.CreateContainer(out main);
		MainDialogue.CreateContainer(out prologue, StoryBotContainerTypeEnum.Prologue);
	}

	public void InitVanilla()
	{
		foreach (StoryBotDialogueBranchContainer allContainer in MainDialogue.AllContainers)
		{
			allContainer.InitVanilla(OldOriginalKeyPrefix);
		}
	}

	public void InitBranch(StoryBotDialogueBranch branch)
	{
		if (TryGetBranchById(branch.Id, out var _))
		{
			Debug.LogError($"Branch with id {branch.Id} already exists! Shit's fucked! You need to make sure branch id's numbers are unique!");
		}
		else
		{
			RegisterUsedId(branch.Id);
		}
	}
}
