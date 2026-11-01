using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameStats
{
	public PersonalityGameStats PersonalityGameStats = new PersonalityGameStats();

	public OtherFrickGameStats OtherFrickGameStats = new OtherFrickGameStats();

	public FishingGameStats FishingGameStats = new FishingGameStats();

	public StreamGameStats StreamGameStats = new StreamGameStats();

	public CommonGameStats CommonGameStats = new CommonGameStats();

	public EquipmentGameStats EquipmentGameStats = new EquipmentGameStats();

	private List<string> _statContainerNames;

	private List<IGameStatContainer> _statContainers;

	public IReadOnlyList<string> StatContainerNames => _statContainerNames;

	public IReadOnlyList<IGameStatContainer> StatContainers => _statContainers;

	public GameStats()
	{
		PopulateEverything();
	}

	private void PopulateEverything()
	{
		_statContainers = _statContainers.ClearOrCreate();
		_statContainerNames = _statContainerNames.ClearOrCreate();
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (typeof(IGameStatContainer).IsAssignableFrom(fieldInfo.FieldType))
			{
				if (fieldInfo.GetValue(this) is IGameStatContainer item)
				{
					_statContainers.Add(item);
					_statContainerNames.Add(fieldInfo.Name);
				}
				else
				{
					Debug.LogError(fieldInfo.Name + " is null!!!");
				}
			}
		}
	}

	public void ResetStats()
	{
		foreach (IGameStatContainer statContainer in _statContainers)
		{
			statContainer.ResetAllStats();
		}
	}
}
