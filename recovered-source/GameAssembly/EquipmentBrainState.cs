using System.Collections;

public class EquipmentBrainState : ModelBrainState<Live2DInteractionController>, IHasNonJitClassDescription
{
	private Live2DInteractableLimbUsage<Live2DInteractionController> _openMouth;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _closeMouth;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _pantiesInteractable;

	private Live2DInteractableLimbUsage<Live2DInteractionController> _skirtInteractable;

	public override void EnterState()
	{
		base.EnterState();
		Live2DController.PrepareCommonMouthInteractables(controller, out _openMouth, out _closeMouth);
		Live2DInteractionController.PreparePantiesAndSkirt(controller, out _pantiesInteractable, out _skirtInteractable);
	}

	public override void ExitState()
	{
		base.ExitState();
		controller.RemoveInteractable(_openMouth);
		controller.RemoveInteractable(_closeMouth);
		controller.RemoveInteractable(_pantiesInteractable);
		controller.RemoveInteractable(_skirtInteractable);
	}

	public void SayComment(string text)
	{
		Say(text);
	}

	protected override IEnumerator Brain()
	{
		yield return WaitForSecondCache.Seconds1;
	}
}
