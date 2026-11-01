using System.Collections;

public class RoomBrainState : ModelBrainState, IHasNonJitClassDescription
{
	protected override IEnumerator Brain()
	{
		yield return WaitForSecondCache.Seconds0_1;
	}
}
