public abstract class EmailEventTarget
{
	public abstract bool Start(NewEmail parent);

	public static implicit operator EmailEventTarget(FungusBlocks.FungusBlockReference eventTarget)
	{
		return new FungusBlockEmailEventTarget(eventTarget);
	}
}
