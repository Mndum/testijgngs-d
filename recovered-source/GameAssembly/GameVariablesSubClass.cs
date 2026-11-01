using System;

public class GameVariablesSubClass
{
	[field: NonSerialized]
	public GameVariables RelatedGameVariables { get; private set; }

	public void InitGameVariables(GameVariables gameVariables)
	{
		RelatedGameVariables = gameVariables;
	}
}
