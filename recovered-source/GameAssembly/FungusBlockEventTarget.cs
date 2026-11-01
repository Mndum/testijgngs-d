public class FungusBlockEventTarget : RuntimeEventTarget
{
	private FungusBlocks.FungusBlockReference RelatedBlock;

	public FungusBlocks.FungusBlockReference Block => RelatedBlock;

	public FungusBlockEventTarget(FungusBlocks.FungusBlockReference relatedBlock)
	{
		RelatedBlock = relatedBlock;
	}

	public override bool Start(RuntimeEvent parent)
	{
		SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeToStoryStateBlockReference(RelatedBlock);
		return true;
	}

	public override string ToString()
	{
		return RelatedBlock.ToString();
	}
}
