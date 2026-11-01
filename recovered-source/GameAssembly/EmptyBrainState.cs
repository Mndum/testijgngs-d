using System.Collections;

public class EmptyBrainState : ModelBrainState, IHasNonJitClassDescription
{
	public override void EnterState()
	{
		InitializeCoroutines();
	}

	protected override IEnumerator Brain()
	{
		yield break;
	}
}
