using System;
using System.Collections.Generic;
using Live2D.Cubism.Framework.Raycasting;

public class Live2DInteractable : Live2DInteractableDefaultBase, ILive2DInteractableHoverable, ILive2DInteractable
{
	public SimpleLocalizedString InfoSls;

	public CursorSingleton.CursorType HoverCursor;

	public Action OnHoverStart;

	public Action OnHoverEnd;

	public string HoverText => InfoSls?.Value;

	CursorSingleton.CursorType ILive2DInteractableHoverable.HoverCursor => HoverCursor;

	public List<CubismRaycastable> ConnectedRaycastables { get; private set; }

	public void StartHover()
	{
		OnHoverStart?.Invoke();
	}

	public void EndHover()
	{
		OnHoverEnd?.Invoke();
	}

	public void SetConnectedRaycastables(List<CubismRaycastable> cubismRaycastables)
	{
		ConnectedRaycastables = cubismRaycastables;
	}

	public bool ReportLimbUsageFinished(ILimbUsage limbUsage)
	{
		return false;
	}
}
