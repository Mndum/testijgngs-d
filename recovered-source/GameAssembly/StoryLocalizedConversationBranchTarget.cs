using System;
using System.Collections.Generic;
using Fungus;

public class StoryLocalizedConversationBranchTarget : StoryBranchTarget, IBotDialogueBranchTarget
{
	public LocalizedConversation Conversation { get; private set; }

	public bool WaitForInput { get; private set; }

	public StoryLocalizedConversationBranchTarget(string text, Guid modGuid, int id, bool waitForInput = false)
	{
		Conversation = ModdableTable.CreateModdableConversation(text, modGuid, id);
		WaitForInput = waitForInput;
	}

	public StoryLocalizedConversationBranchTarget(LocalizedConversation conversation, bool waitForInput = false)
	{
		Conversation = conversation;
		WaitForInput = waitForInput;
	}

	public override void Activate(Action onFinished)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.StartCoroutine(Conversation.GetConversationEnumerator(BetterConversationManager.ConversationItemWriteSettings.Get(WaitForInput)).Then(onFinished));
	}

	public override Result CheckValidity(Dictionary<string, Block> blocks)
	{
		return Conversation.GetSummary().IsGood();
	}

	public StoryBotDialogueBranchType GetBranchType()
	{
		return StoryBotDialogueBranchType.Comment;
	}

	public override string ToString()
	{
		return Conversation.mainText.TruncateLongString(30);
	}

	public void Activate(ModelBrainState modelBrainState, bool noWrapper)
	{
		modelBrainState.DoTalking(Conversation.GetConversationEnumerator(BetterConversationManager.ConversationItemWriteSettings.Get(WaitForInput)), null, noWrapper);
	}
}
