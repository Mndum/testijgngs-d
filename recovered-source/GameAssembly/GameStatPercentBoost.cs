using UnityEngine;

public class GameStatPercentBoost : IGameStat
{
	public readonly float MinAllowedPercent;

	public readonly float MaxAllowedPercent;

	public float Percent;

	public GameStatPercentBoost(float minAllowedPercent = -0.5f, float maxAllowedPercent = 1f)
	{
		MinAllowedPercent = minAllowedPercent;
		MaxAllowedPercent = maxAllowedPercent;
		ResetStat();
	}

	public void ResetStat()
	{
		Percent = 0f;
	}

	public float CalculateBoosted(float value)
	{
		return (Mathf.Clamp(Percent, MinAllowedPercent, MaxAllowedPercent) + 1f) * value;
	}

	public int CalculateBoostedInt(int value)
	{
		return Mathf.RoundToInt(CalculateBoosted(value));
	}

	public override string ToString()
	{
		if (Mathf.Approximately(Percent, 0f))
		{
			return $"{Percent:P} (Default)";
		}
		return $"{Percent:P}";
	}
}
