using Live2D.Cubism.Core;
using UnityEngine;

public interface ILive2DInteractable
{
	bool Active { get; }

	void StartHold(CubismDrawable clickedDrawable);

	void ContinueHold(Vector2 mouseMove);

	void ContinueHoldFixed();

	void EndHold();

	bool ReportLimbUsageFinished(ILimbUsage limbUsage);
}
