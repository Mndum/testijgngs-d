using UnityEngine;

public static class ItemModifierUtilities
{
	public static bool IsBoostPositiveForItem(ItemModifierDirectItemChangeFloat value)
	{
		return true;
	}

	public static float ApplyBoostPercent(ItemModifierDirectItemChangeFloat target, float value, float boostPercent)
	{
		float num = (IsBoostPositiveForItem(target) ? boostPercent : (0f - boostPercent));
		float num2 = Mathf.Clamp(1f + num, 0.01f, 10f);
		return value * num2;
	}
}
