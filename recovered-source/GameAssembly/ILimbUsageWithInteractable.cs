using UnityEngine;

public interface ILimbUsageWithInteractable : ILimbUsage
{
	void ContinueHold(Vector2 mouseMove);

	void ContinueHoldFixed();
}
