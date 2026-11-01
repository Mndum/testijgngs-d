using Live2D.Cubism.Rendering;

public class Live2DGenericInteractable : Live2DInteractableDefaultBase, ILive2DInteractable
{
	public CubismRenderer[] Renderers;

	public bool ReportLimbUsageFinished(ILimbUsage limbUsage)
	{
		return false;
	}
}
