public class FungusBlockEmailEventTarget : EmailEventTarget
{
	private FungusBlocks.FungusBlockReference RelatedBlock;

	public FungusBlocks.FungusBlockReference Block => RelatedBlock;

	public FungusBlockEmailEventTarget(FungusBlocks.FungusBlockReference relatedBlock)
	{
		RelatedBlock = relatedBlock;
	}

	public override bool Start(NewEmail parent)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.FungusStartBlock(RelatedBlock.Name);
		return true;
	}

	public override string ToString()
	{
		return RelatedBlock.ToString();
	}
}
