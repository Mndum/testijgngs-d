public class GameStat<T> : IGameStat
{
	public readonly T DefaultValue;

	public T Value;

	public GameStat(T defaultValue)
	{
		DefaultValue = defaultValue;
		ResetStat();
	}

	public void SetValue(T value)
	{
		Value = value;
	}

	public void ResetStat()
	{
		Value = DefaultValue;
	}

	public override string ToString()
	{
		return Value.ToString();
	}
}
