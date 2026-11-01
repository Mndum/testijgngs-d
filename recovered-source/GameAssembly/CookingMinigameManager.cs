using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CookingMinigameManager
{
	[SerializeField]
	private PrefabManagerDataOneToOneLazy<GameId, CookingSong, CookingSongSave>.Serialized saves = new PrefabManagerDataOneToOneLazy<GameId, CookingSong, CookingSongSave>.Serialized((GameId x) => new CookingSongSave(x));

	private bool _isDirty = true;

	private int _passedSongs;

	private Dictionary<CookingGrade, int> _grades;

	public int PassedSongsCount
	{
		get
		{
			EnsureNotDirty();
			return _passedSongs;
		}
	}

	public int GetGradeCount(CookingGrade grade)
	{
		EnsureNotDirty();
		return _grades.GetValueOrDefault(grade, 0);
	}

	public int GetAtLeastGradeCount(CookingGrade minGrade)
	{
		EnsureNotDirty();
		int num = 0;
		foreach (var (cookingGrade2, num3) in _grades)
		{
			if (cookingGrade2 >= minGrade)
			{
				num += num3;
			}
		}
		return num;
	}

	public int GetPlayCount()
	{
		int num = 0;
		foreach (CookingSongSave allSafe in saves.AllSaves)
		{
			num += allSafe.TimesPlayed;
		}
		return num;
	}

	public void MarkDirty()
	{
		_isDirty = true;
	}

	private void EnsureNotDirty()
	{
		if (_isDirty)
		{
			Recalculate();
		}
	}

	private void Recalculate()
	{
		_grades = _grades.ClearOrCreate();
		_passedSongs = 0;
		foreach (CookingSongSave allSafe in saves.AllSaves)
		{
			allSafe.TrimAndSortScores();
			if (allSafe.TryGetBestGradeScore(out var _, out var _bestGrade))
			{
				_grades.AddToCountDictionary(_bestGrade);
				if (_bestGrade.IsGradePassing())
				{
					_passedSongs++;
				}
			}
		}
		_isDirty = false;
	}

	public CookingSongSave GetSave(CookingSong song)
	{
		return saves.Get(song);
	}
}
