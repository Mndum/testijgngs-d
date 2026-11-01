public abstract class RuntimeEventTarget
{
	public abstract bool Start(RuntimeEvent parent);

	public static implicit operator RuntimeEventTarget(FungusBlocks.FungusBlockReference eventTarget)
	{
		return new FungusBlockEventTarget(eventTarget);
	}
}
