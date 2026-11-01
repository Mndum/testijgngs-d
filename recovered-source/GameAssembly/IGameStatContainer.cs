using System.Collections.Generic;

public interface IGameStatContainer
{
	void ResetAllStats();

	IEnumerable<(string name, IGameStat stat)> IterateAllStatsWithNames();
}
