using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using StoryConditions;

public class StoryBotDialogueBranchContainer
{
	public readonly StoryBotContainerTypeEnum StoryBotContainerType;

	private readonly Dictionary<GameId, StoryBotDialogueBranch> _branchesDictionary = new Dictionary<GameId, StoryBotDialogueBranch>();

	private readonly List<StoryBotDialogueBranch> _unconditionalBranches = new List<StoryBotDialogueBranch>();

	private readonly Dictionary<StoryCondition, StoryBotDialogueBranch> _singleBranches = new Dictionary<StoryCondition, StoryBotDialogueBranch>();

	private readonly Dictionary<StoryCondition, List<StoryBotDialogueBranch>> _multipleBranches = new Dictionary<StoryCondition, List<StoryBotDialogueBranch>>();

	public StoryBotDialogueTag SourceTag { get; private set; }

	public StoryCondition[] Conditions { get; private set; }

	public int BranchCount => _branchesDictionary.Count;

	public IEnumerable<StoryBotDialogueBranch> AllBranches => _branchesDictionary.Values;

	public StoryBotDialogueBranchContainer(StoryBotDialogueTag sourceTag, StoryBotContainerTypeEnum storyBotContainerType = StoryBotContainerTypeEnum.Default)
	{
		StoryBotContainerType = storyBotContainerType;
		SourceTag = sourceTag;
	}

	public void SetConditions([NotNull] params StoryCondition[] newConditions)
	{
		Conditions = newConditions;
	}

	public bool ConditionsMet()
	{
		return Conditions.ConditionsMet();
	}

	private void InitBranch(StoryBotDialogueBranch branch)
	{
		branch.InitSource(this);
		SourceTag.SourceStage.InitBranch(branch);
		_branchesDictionary.Add(branch.Id, branch);
	}

	private void AddRangeToAllBranches(IEnumerable<StoryBotDialogueBranch> branches)
	{
		foreach (StoryBotDialogueBranch branch in branches)
		{
			InitBranch(branch);
		}
	}

	public void AddBranches(params StoryBotDialogueBranch[] branches)
	{
		AddBranches((IEnumerable<StoryBotDialogueBranch>)branches);
	}

	public void AddBranches(IEnumerable<StoryBotDialogueBranch> branches)
	{
		foreach (StoryBotDialogueBranch branch in branches)
		{
			AddBranch(branch);
		}
	}

	public void InitVanilla(string keyPrefix)
	{
		foreach (StoryBotDialogueBranch allBranch in AllBranches)
		{
			allBranch.TitleSls = SimpleLocalizedString.CreateInstance("Dialogue", keyPrefix + "_" + allBranch.Id.ToShortString());
		}
	}

	public StoryBotDialogueBranchContainer AddBranch(StoryBotDialogueBranch branch)
	{
		InitBranch(branch);
		if (branch.AdditionalConditions.TryGetValue(0, out var value))
		{
			StoryBotDialogueBranch value3;
			if (_multipleBranches.TryGetValue(value, out var value2))
			{
				value2.Add(branch);
			}
			else if (_singleBranches.Remove(value, out value3))
			{
				_multipleBranches.Add(value, new List<StoryBotDialogueBranch> { branch, value3 });
			}
			else
			{
				_singleBranches.Add(value, branch);
			}
		}
		else
		{
			_unconditionalBranches.Add(branch);
		}
		return this;
	}

	public void AddMultipleBranchesWithoutConditions(params StoryBotDialogueBranch[] branches)
	{
		AddMultipleBranchesWithoutConditions((IList<StoryBotDialogueBranch>)branches);
	}

	public void AddMultipleBranchesWithoutConditions(IList<StoryBotDialogueBranch> branches)
	{
		AddRangeToAllBranches(branches);
		_unconditionalBranches.AddRange(branches);
	}

	public void AddMultipleBranchesWithSameCondition(StoryCondition condition, params StoryBotDialogueBranch[] branches)
	{
		AddMultipleBranchesWithSameCondition(condition, (IList<StoryBotDialogueBranch>)branches);
	}

	public void AddMultipleBranchesWithSameCondition(StoryCondition condition, IList<StoryBotDialogueBranch> branches)
	{
		if (branches.Count == 0)
		{
			return;
		}
		StoryCondition[] addedAdditionalConditions = new StoryCondition[1] { condition };
		foreach (StoryBotDialogueBranch branch in branches)
		{
			branch.AddConditionsAtFront(addedAdditionalConditions);
		}
		StoryBotDialogueBranch value2;
		if (_multipleBranches.TryGetValue(condition, out var value))
		{
			AddRangeToAllBranches(branches);
			value.AddRange(branches);
		}
		else if (_singleBranches.TryGetValue(condition, out value2))
		{
			AddRangeToAllBranches(branches);
			_singleBranches.Remove(condition);
			List<StoryBotDialogueBranch> list = new List<StoryBotDialogueBranch> { value2 };
			list.AddRange(branches);
			_multipleBranches.Add(condition, list);
		}
		else if (branches.Count >= 2)
		{
			AddRangeToAllBranches(branches);
			_multipleBranches[condition] = new List<StoryBotDialogueBranch>(branches);
		}
		else
		{
			AddBranch(branches.First());
		}
	}

	public bool IsLastFinished()
	{
		StoryBotDialogueBranch storyBotDialogueBranch = AllBranches.LastOrDefault();
		if (storyBotDialogueBranch == null)
		{
			return true;
		}
		return GetMemory().IsIgnored(storyBotDialogueBranch.Id);
	}

	public bool TryGetBranchById(GameId id, out StoryBotDialogueBranch branch)
	{
		return _branchesDictionary.TryGetValue(id, out branch);
	}

	public DialogueChainMemory GetMemory()
	{
		return SourceTag.SourceStage.GetMemory();
	}

	public bool TryGetFirst(out StoryBotDialogueBranch branch, SceneEnumFlag sceneEnumFlag, StoryBotDialogueBranchType allowedTypes = StoryBotDialogueBranchType.All)
	{
		branch = (from x in GetAllBranchesCheckConditionsAndIgnored(sceneEnumFlag, allowedTypes)
			orderby x.Id
			select x).FirstOrDefault();
		return branch != null;
	}

	private IEnumerable<StoryBotDialogueBranch> GetAllBranchesCheckConditions()
	{
		foreach (StoryBotDialogueBranch unconditionalBranch in _unconditionalBranches)
		{
			yield return unconditionalBranch;
		}
		foreach (KeyValuePair<StoryCondition, StoryBotDialogueBranch> singleBranch in _singleBranches)
		{
			if (singleBranch.Key.ConditionMet() && singleBranch.Value.CheckAdditionalConditionsOffset(1))
			{
				yield return singleBranch.Value;
			}
		}
		foreach (KeyValuePair<StoryCondition, List<StoryBotDialogueBranch>> multipleBranch in _multipleBranches)
		{
			if (!multipleBranch.Key.ConditionMet())
			{
				continue;
			}
			foreach (StoryBotDialogueBranch item in multipleBranch.Value)
			{
				if (item.CheckAdditionalConditionsOffset(1))
				{
					yield return item;
				}
			}
		}
	}

	public IEnumerable<StoryBotDialogueBranch> GetAllBranchesCheckConditionsAndIgnored(SceneEnumFlag sceneEnumFlag, StoryBotDialogueBranchType allowedTypes = StoryBotDialogueBranchType.All)
	{
		DialogueChainMemory memory = GetMemory();
		return from t in GetAllBranchesCheckConditions()
			where allowedTypes.HasFlag(t.BranchType) && t.AllowedScenes.HasFlag(sceneEnumFlag) && !memory.IsIgnored(t.Id)
			select t;
	}
}
