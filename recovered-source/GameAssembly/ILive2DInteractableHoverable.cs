using System.Collections.Generic;
using Live2D.Cubism.Framework.Raycasting;

public interface ILive2DInteractableHoverable : ILive2DInteractable
{
	string HoverText { get; }

	CursorSingleton.CursorType HoverCursor { get; }

	List<CubismRaycastable> ConnectedRaycastables { get; }

	void StartHover();

	void EndHover();

	void SetConnectedRaycastables(List<CubismRaycastable> cubismRaycastables);
}
