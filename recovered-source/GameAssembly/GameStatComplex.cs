using System;

public class GameStatComplex<T> : IGameStat
{
	public readonly Func<T> DefaultValueGenerator;

	public T Value;

	public GameStatComplex(Func<T> defaultValueGenerator)
	{
		DefaultValueGenerator = defaultValueGenerator;
		ResetStat();
	}

	public void ResetStat()
	{
		Value = DefaultValueGenerator();
	}

	public override string ToString()
	{
		return Value.ToString();
	}
}
