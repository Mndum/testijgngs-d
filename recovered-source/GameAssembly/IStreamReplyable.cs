public interface IStreamReplyable
{
	bool CanBeRepliedTo { get; }

	DialogueChain ConsumeResponse();
}
