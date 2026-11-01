public class Donation : DialogueChainStreamReplyable
{
	public Donation(DialogueChain dialogueChain)
	{
		InnerSetDialogueChain(dialogueChain);
	}
}
