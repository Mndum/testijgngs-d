using System.Collections.Generic;
using UnityEngine;

public class GameStatDirectedPercentBoost<T> : IGameStat
{
	public readonly float MinAllowedBoostPercent;

	public readonly float MaxAllowedBoostPercent;

	private readonly Dictionary<T, float> _percentBoost = new Dictionary<T, float>();

	public GameStatDirectedPercentBoost(float minAllowedBoostPercent = -0.5f, float maxAllowedBoostPercent = 1f)
	{
		MinAllowedBoostPercent = minAllowedBoostPercent;
		MaxAllowedBoostPercent = maxAllowedBoostPercent;
		ResetStat();
	}

	public void ResetStat()
	{
		_percentBoost.Clear();
	}

	public void AddBoost(T area, float boost)
	{
		float valueOrDefault = _percentBoost.GetValueOrDefault(area, 0f);
		_percentBoost[area] = valueOrDefault + boost;
	}

	public float GetBoostMultiplyValue(T area)
	{
		return Mathf.Clamp(_percentBoost.GetValueOrDefault(area, 0f), MinAllowedBoostPercent, MaxAllowedBoostPercent) + 1f;
	}

	public float CalculateBoosted(T area, float value)
	{
		return GetBoostMultiplyValue(area) * value;
	}

	public override string ToString()
	{
		if (_percentBoost.Count == 0)
		{
			return "(Default)";
		}
		return _percentBoost.ToStringBetter();
	}
}
