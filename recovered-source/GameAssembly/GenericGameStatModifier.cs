using System;

public class GenericGameStatModifier : IGameStatModifier
{
	public const int EarlyOrder = 1000;

	public const int NormalOrder = 1000;

	public const int LateOrder = 2000;

	public const int VeryLateOrder = 3000;

	public Action<GameVariables, GameStats> ModifyAction { get; }

	public int Order { get; }

	public bool IsValid => ModifyAction != null;

	public GenericGameStatModifier(int order, Action<GameVariables, GameStats> modifyAction)
	{
		Order = order;
		ModifyAction = modifyAction;
	}

	public void ApplyToGameStats(GameVariables gameVariables, GameStats gameStats)
	{
		ModifyAction(gameVariables, gameStats);
	}
}
