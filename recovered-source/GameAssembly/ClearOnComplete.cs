using Fungus;
using UnityEngine;

[CommandInfo("Flow", "Clear on complete", "Clears on complete so that WaitUntilFinished calling blocks are not executed when this block stops!", 0)]
public class ClearOnComplete : Command
{
	public override void OnEnter()
	{
		Block parentBlock = ParentBlock;
		parentBlock.ClearOnComplete();
		foreach (Block executingBlock in GetFlowchart().GetExecutingBlocks())
		{
			if (executingBlock != parentBlock)
			{
				executingBlock.Stop();
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		return "Clears on complete and stops block waiting for this so that WaitUntilFinished calling blocks are not executed when this block stops!";
	}

	public override Color GetButtonColor()
	{
		return new Color32(216, 228, 170, 255);
	}
}
