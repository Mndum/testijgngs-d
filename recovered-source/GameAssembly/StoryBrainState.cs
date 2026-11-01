using System.Collections;

public class StoryBrainState : ModelBrainState, IHasNonJitClassDescription
{
	public override bool IsStoryState => true;

	public override void EnterState()
	{
		InitializeCoroutines();
		CanBeChanged = false;
		Owner.RestoreDefaultFaceState();
	}

	public override void ExitState()
	{
		base.ExitState();
		if (Owner.ConnectedController != null)
		{
			Owner.ConnectedController.Expression.ClearExpression();
		}
	}

	protected override IEnumerator Brain()
	{
		yield break;
	}
}
