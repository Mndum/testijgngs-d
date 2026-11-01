using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class GameStatContainer : IGameStatContainer
{
	private List<string> _allGameStatsNames;

	private List<IGameStat> _allGameStats;

	public GameStatContainer()
	{
		PopulateGameStatsList();
	}

	private void PopulateGameStatsList()
	{
		_allGameStats = _allGameStats.ClearOrCreate();
		_allGameStatsNames = _allGameStatsNames.ClearOrCreate();
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (typeof(IGameStat).IsAssignableFrom(fieldInfo.FieldType))
			{
				if (fieldInfo.GetValue(this) is IGameStat item)
				{
					_allGameStats.Add(item);
					_allGameStatsNames.Add(fieldInfo.Name);
				}
				else
				{
					Debug.LogError(fieldInfo.Name + " is null!!!");
				}
			}
		}
	}

	public virtual void ResetAllStats()
	{
		foreach (IGameStat allGameStat in _allGameStats)
		{
			allGameStat.ResetStat();
		}
	}

	public IEnumerable<(string name, IGameStat stat)> IterateAllStatsWithNames()
	{
		return _allGameStats.Zip(_allGameStatsNames, (IGameStat stat, string name) => (name: name, stat: stat));
	}

	public IEnumerable<IGameStat> IterateAllStats()
	{
		return _allGameStats;
	}
}
