public abstract class DialogueChainStreamReplyable : IStreamReplyable
{
	protected DialogueChain _dialogueChain;

	public DialogueChain DialogueChain => _dialogueChain;

	public bool CanBeRepliedTo { get; private set; }

	protected void InnerSetDialogueChain(DialogueChain dialogueChain)
	{
		_dialogueChain = dialogueChain;
		CanBeRepliedTo = dialogueChain.Next != null;
	}

	public DialogueChain ConsumeResponse()
	{
		CanBeRepliedTo = false;
		if (_dialogueChain.ConditionsMet())
		{
			return _dialogueChain.Next;
		}
		return null;
	}
}
