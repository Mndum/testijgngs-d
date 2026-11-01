public class StoryBotDialogueTagDefinition
{
	public string Id;

	public static implicit operator StoryBotDialogueTagDefinition(string id)
	{
		return new StoryBotDialogueTagDefinition(id);
	}

	public StoryBotDialogueTagDefinition()
	{
	}

	public StoryBotDialogueTagDefinition(string id)
	{
		Id = id;
	}

	public void CreateContainer(StoryBotDialogueStage stage, out StoryBotDialogueBranchContainer o, StoryBotContainerTypeEnum type = StoryBotContainerTypeEnum.Default)
	{
		stage.GetTag(Id).CreateContainer(out o, type);
	}

	public StoryBotDialogueBranchContainer CreateContainerReturn(StoryBotDialogueStage stage, StoryBotContainerTypeEnum type = StoryBotContainerTypeEnum.Default)
	{
		stage.GetTag(Id).CreateContainer(out var container, type);
		return container;
	}

	public bool TryGetTag(out StoryBotDialogueTag o)
	{
		return SingletonMonoBehaviour<StorySingleton>.Instance.TryGetTag(Id, out o);
	}

	public bool StartDialogueFromModelBrainState(ModelBrainState modelBrainState, bool isRealDialogue = true)
	{
		if (SingletonMonoBehaviour<StorySingleton>.Instance.TryGetTag(Id, out var storyTag))
		{
			return storyTag.GetAndStartDialogueFromModelBrainState(modelBrainState, isRealDialogue);
		}
		return false;
	}

	public ManagedCoroutine StartDialogueFromModelBrainStateAndWait(ModelBrainState modelBrainState, bool isRealDialogue = true, bool noWrapper = false)
	{
		if (SingletonMonoBehaviour<StorySingleton>.Instance.TryGetTag(Id, out var storyTag))
		{
			storyTag.GetAndStartDialogueFromModelBrainState(modelBrainState, isRealDialogue, noWrapper);
			return modelBrainState.TalkingCoroutine;
		}
		return null;
	}

	public void FromTupleReference(StoryBotDialogueStage stage3, GameId id, LOS.TupleReference target, bool waitForInput = false)
	{
		CreateContainer(stage3, out var o);
		o.AddBranch(new StoryBotDialogueBranch(id, new StoryLocalizedStringBranchTarget(target, waitForInput)));
	}

	public void FromConversation(StoryBotDialogueStage stage3, GameId id, SerializedLocalizedConversation cowgirlConversationsAnonBegStop, bool waitForInput = false)
	{
		CreateContainer(stage3, out var o);
		o.AddBranch(new StoryBotDialogueBranch(id, new StoryLocalizedConversationBranchTarget(cowgirlConversationsAnonBegStop, waitForInput)));
	}
}
