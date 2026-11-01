using UnityEngine;

public class GameStatFixedValue : IGameStat
{
	public readonly float DefaultValue;

	public float Value;

	public GameStatFixedValue(float defaultValue)
	{
		DefaultValue = defaultValue;
		ResetStat();
	}

	public void SetValue(float value)
	{
		Value = value;
	}

	public void ResetStat()
	{
		Value = DefaultValue;
	}

	public float Calculate(float add = 0f)
	{
		return Value + add;
	}

	public override string ToString()
	{
		if (Mathf.Approximately(Value, DefaultValue))
		{
			return $"{Value} (Default)";
		}
		return $"{Value}";
	}
}
