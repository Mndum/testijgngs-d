using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class StoryBotDialogueTag : IBranchContainer<StoryBotDialogueBranch>
{
	private class BranchLottery
	{
		private Lottery<StoryBotDialogueBranch> _branchLotteryInner = new Lottery<StoryBotDialogueBranch>();

		public int CurrentPriority { get; private set; } = -2147483648;

		public bool NotEmpty => _branchLotteryInner.NotEmpty;

		public void Clear()
		{
			_branchLotteryInner.Clear();
			CurrentPriority = -2147483648;
		}

		public void AddItem(StoryBotDialogueBranch item)
		{
			int priority = item.Priority;
			if (_branchLotteryInner.ItemCount == 0)
			{
				_branchLotteryInner.AddItem(item, item.Odds);
				CurrentPriority = priority;
			}
			else if (priority > CurrentPriority)
			{
				_branchLotteryInner.Clear();
				CurrentPriority = priority;
				_branchLotteryInner.AddItem(item, item.Odds);
			}
			else if (priority == CurrentPriority)
			{
				_branchLotteryInner.AddItem(item, item.Odds);
			}
		}

		public void AddItems(IEnumerable<StoryBotDialogueBranch> items)
		{
			foreach (StoryBotDialogueBranch item in items)
			{
				AddItem(item);
			}
		}

		public StoryBotDialogueBranch GetRandom()
		{
			return _branchLotteryInner.GetRandom();
		}
	}

	private class BranchLotteryPoll : BetterObjectPool<BranchLottery>
	{
		public BranchLotteryPoll(int maxCachedInstances = 3)
			: base(maxCachedInstances)
		{
		}

		protected override BranchLottery CreateNew()
		{
			return new BranchLottery();
		}

		protected override void OnAfterAddedToPool(BranchLottery obj)
		{
			obj.Clear();
		}

		protected override bool CanBePooled(BranchLottery obj)
		{
			return true;
		}
	}

	protected Dictionary<StoryBotContainerTypeEnum, List<StoryBotDialogueBranchContainer>> _containersByType = new Dictionary<StoryBotContainerTypeEnum, List<StoryBotDialogueBranchContainer>>();

	protected List<StoryBotDialogueBranchContainer> _containers = new List<StoryBotDialogueBranchContainer>();

	private static BranchLotteryPoll _branchLotteryPoll = new BranchLotteryPoll();

	public StoryBotDialogueStage SourceStage { get; private set; }

	public ReadOnlyCollection<StoryBotDialogueBranchContainer> AllContainers => _containers.AsReadOnly();

	public void InitSource(StoryBotDialogueStage storyBotDialogueStage)
	{
		SourceStage = storyBotDialogueStage;
	}

	public void CreateContainer(out StoryBotDialogueBranchContainer container, StoryBotContainerTypeEnum type = StoryBotContainerTypeEnum.Default)
	{
		container = new StoryBotDialogueBranchContainer(this, type);
		AddInner(container);
	}

	public IEnumerable<StoryBotDialogueBranch> GetBranches()
	{
		return AllContainers.SelectMany((StoryBotDialogueBranchContainer t) => t.AllBranches);
	}

	public void GetDialogues(HashSet<StoryBotDialogueBranch> uniqueDialogues, HashSet<StoryBotDialogueBranch> genericDialogues, int uniqueDialoguesCount, SceneEnumFlag sceneEnumFlag)
	{
		if (TryGetByContainerType(StoryBotContainerTypeEnum.Prologue, out var values))
		{
			foreach (StoryBotDialogueBranchContainer item in values)
			{
				if (!item.IsLastFinished())
				{
					if (item.TryGetFirst(out var branch, sceneEnumFlag))
					{
						uniqueDialogues.Add(branch);
					}
					return;
				}
			}
		}
		List<StoryBotDialogueBranchContainer> values3;
		if (TryGetByContainerTypeConditionsMet(StoryBotContainerTypeEnum.Override, out var values2))
		{
			FillFromContainers(values2);
		}
		else if (TryGetByContainerTypeConditionsMet(StoryBotContainerTypeEnum.Default, out values3))
		{
			FillFromContainers(values3);
		}
		void FillFromContainers(IEnumerable<StoryBotDialogueBranchContainer> storyBotDialogueBranchContainers)
		{
			BranchLottery pooled;
			using (_branchLotteryPoll.Acquire(out pooled))
			{
				BranchLottery pooled2;
				using (_branchLotteryPoll.Acquire(out pooled2))
				{
					foreach (StoryBotDialogueBranchContainer storyBotDialogueBranchContainer in storyBotDialogueBranchContainers)
					{
						foreach (StoryBotDialogueBranch item2 in storyBotDialogueBranchContainer.GetAllBranchesCheckConditionsAndIgnored(sceneEnumFlag))
						{
							if (item2.IsOneTime)
							{
								pooled.AddItem(item2);
							}
							else
							{
								pooled2.AddItem(item2);
							}
						}
					}
					if (pooled.NotEmpty)
					{
						for (int i = 0; i < uniqueDialoguesCount; i++)
						{
							uniqueDialogues.Add(pooled.GetRandom());
						}
					}
					if (pooled2.NotEmpty)
					{
						genericDialogues.Add(pooled2.GetRandom());
					}
				}
			}
		}
	}

	public bool GetDialogueForModelBrainState(ModelBrainState modelBrainState, out StoryBotDialogueBranch branch)
	{
		if (modelBrainState == null)
		{
			branch = null;
			return false;
		}
		StoryBotDialogueBranchType storyBotDialogueBranchType = StoryBotDialogueBranchType.Story;
		if (!modelBrainState.IsReactingUnstoppable)
		{
			storyBotDialogueBranchType |= StoryBotDialogueBranchType.Reaction;
		}
		if (!modelBrainState.IsTalking)
		{
			storyBotDialogueBranchType |= StoryBotDialogueBranchType.Comment;
		}
		if (TryGetRandom(out branch, modelBrainState.Owner.ConnectedController.SceneEnum, storyBotDialogueBranchType))
		{
			return true;
		}
		return false;
	}

	public bool StartDialogueFromModelBrainState(StoryBotDialogueBranch branch, ModelBrainState modelBrainState, bool isRealDialogue = true, bool noWrapper = false)
	{
		if (branch.ActivateGetTarget() is IBotDialogueBranchTarget botDialogueBranchTarget)
		{
			botDialogueBranchTarget.Activate(modelBrainState, noWrapper);
			if (isRealDialogue)
			{
				modelBrainState.Owner.InteractionMemory.FireEvent(InteractionMemoryEventEnum.BotStartedTalking);
			}
			return true;
		}
		Debug.LogError("Trying to start a non IBotDialogueBranchTarget branch from GetAndStartDialogueFromModelBrainState that is illegal!");
		return false;
	}

	public bool GetAndStartDialogueFromModelBrainState(ModelBrainState modelBrainState, bool isRealDialogue = true, bool noWrapper = false)
	{
		if (GetDialogueForModelBrainState(modelBrainState, out var branch))
		{
			return StartDialogueFromModelBrainState(branch, modelBrainState, isRealDialogue, noWrapper);
		}
		return false;
	}

	public bool TryGetRandom(out StoryBotDialogueBranch branch, SceneEnumFlag scene, StoryBotDialogueBranchType allowedTypes = StoryBotDialogueBranchType.All)
	{
		if (TryGetByContainerType(StoryBotContainerTypeEnum.Prologue, out var values))
		{
			foreach (StoryBotDialogueBranchContainer item in values)
			{
				if (!item.IsLastFinished())
				{
					if (item.TryGetFirst(out var branch2, scene))
					{
						branch = branch2;
						return true;
					}
					branch = null;
					return false;
				}
			}
		}
		if (TryGetByContainerTypeConditionsMet(StoryBotContainerTypeEnum.Override, out var values2))
		{
			return TryGetRandomFromContainers(values2, out branch);
		}
		if (TryGetByContainerTypeConditionsMet(StoryBotContainerTypeEnum.Default, out var values3))
		{
			return TryGetRandomFromContainers(values3, out branch);
		}
		branch = null;
		return false;
		bool TryGetRandomFromContainers(IEnumerable<StoryBotDialogueBranchContainer> storyBotDialogueBranchContainers, out StoryBotDialogueBranch reference)
		{
			BranchLottery pooled;
			using (_branchLotteryPoll.Acquire(out pooled))
			{
				foreach (StoryBotDialogueBranchContainer storyBotDialogueBranchContainer in storyBotDialogueBranchContainers)
				{
					pooled.AddItems(storyBotDialogueBranchContainer.GetAllBranchesCheckConditionsAndIgnored(scene, allowedTypes));
				}
				if (pooled.NotEmpty)
				{
					reference = pooled.GetRandom();
					return true;
				}
				reference = null;
				return false;
			}
		}
	}

	public int GetBranchCount()
	{
		return _containers.Sum((StoryBotDialogueBranchContainer t) => t.BranchCount);
	}

	protected void AddInner(StoryBotDialogueBranchContainer container)
	{
		if (!_containersByType.TryGetValue(container.StoryBotContainerType, out var value))
		{
			value = (_containersByType[container.StoryBotContainerType] = new List<StoryBotDialogueBranchContainer>());
		}
		value.Add(container);
		_containers.Add(container);
	}

	public bool TryGetByContainerType(StoryBotContainerTypeEnum storyBotContainerTypeEnum, out IReadOnlyCollection<StoryBotDialogueBranchContainer> values)
	{
		if (_containersByType.TryGetValue(storyBotContainerTypeEnum, out var value))
		{
			values = value;
			return true;
		}
		values = null;
		return false;
	}

	public bool TryGetByContainerTypeConditionsMet(StoryBotContainerTypeEnum storyBotContainerTypeEnum, out List<StoryBotDialogueBranchContainer> values)
	{
		if (_containersByType.TryGetValue(storyBotContainerTypeEnum, out var value))
		{
			values = value.Where((StoryBotDialogueBranchContainer x) => x.ConditionsMet()).ToList();
			return values.Count > 0;
		}
		values = null;
		return false;
	}

	public bool TryGetBranchById(GameId id, out StoryBotDialogueBranch branch)
	{
		foreach (StoryBotDialogueBranchContainer container in _containers)
		{
			if (container.TryGetBranchById(id, out branch))
			{
				return true;
			}
		}
		branch = null;
		return false;
	}

	public bool HasAny(StoryBotContainerTypeEnum storyBotContainerTypeEnum)
	{
		List<StoryBotDialogueBranchContainer> value;
		return _containersByType.TryGetValue(storyBotContainerTypeEnum, out value);
	}
}
