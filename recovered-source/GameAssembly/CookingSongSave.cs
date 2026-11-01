using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CookingSongSave : IKey<GameId>
{
	[SerializeField]
	private GameId _key;

	public const int MaxRememberedScoresPerSong = 3;

	[SerializeField]
	private List<CookingGameplayScoreData> _allScores = new List<CookingGameplayScoreData>();

	public int TimesPlayed;

	public int TimesFinished;

	public GameId Key => _key;

	public CookingSongSave(GameId key)
	{
		_key = key;
	}

	public void SaveResults(CookingResults results)
	{
		CookingGameplayModifiers activeModifiers = results.StartInfo.ActiveModifiers;
		if (!activeModifiers.HasFlagFast(CookingGameplayModifiers.Practice) && !activeModifiers.HasFlagFast(CookingGameplayModifiers.AutoPlay))
		{
			TimesPlayed++;
			CookingGameplayScoreData score = results.Score;
			if (score.IsComplete)
			{
				TimesFinished++;
				_allScores.Add(score);
				TrimAndSortScores();
			}
			GameVariables.Current.cookingMinigameManager.MarkDirty();
		}
	}

	private bool TryGetRelatedSong(out CookingSong song)
	{
		return CookingSongPrefabManager.TryGetSongById(_key, out song);
	}

	public void TrimAndSortScores()
	{
		if (!TryGetRelatedSong(out var song))
		{
			Debug.LogError($"Missing song with {_key}!");
			return;
		}
		int version = song.RuntimeData.SongVersion;
		_allScores.RemoveAll((CookingGameplayScoreData x) => !x.IsScoreValid(version));
		TryGetBestScore(out var _bestScore);
		TryGetBestAccuracyScore(out var _bestAccuracyScore);
		TryGetBestDishScore(out var _bestDishScore, out var _);
		_allScores.Sort(CookingGameplayScoreData.RawScoreDescendingComparer);
		int num = _allScores.Count - 3;
		if (num <= 0)
		{
			return;
		}
		for (int num2 = _allScores.Count - 1; num2 >= 0; num2--)
		{
			CookingGameplayScoreData cookingGameplayScoreData = _allScores[num2];
			if (cookingGameplayScoreData != _bestScore && cookingGameplayScoreData != _bestAccuracyScore && cookingGameplayScoreData != _bestDishScore)
			{
				_allScores.RemoveAt(num2);
				num--;
				if (num <= 0)
				{
					break;
				}
			}
		}
	}

	public bool TryGetBestAccuracyScore(out CookingGameplayScoreData _bestAccuracyScore)
	{
		if (_allScores.Count <= 0)
		{
			_bestAccuracyScore = null;
			return false;
		}
		_bestAccuracyScore = _allScores[0];
		for (int i = 1; i < _allScores.Count; i++)
		{
			CookingGameplayScoreData cookingGameplayScoreData = _allScores[i];
			if (cookingGameplayScoreData.AccuracyScore > _bestAccuracyScore.AccuracyScore)
			{
				_bestAccuracyScore = cookingGameplayScoreData;
			}
		}
		return true;
	}

	public bool TryGetBestDishScore(out CookingGameplayScoreData _bestDishScore, out float bestDishQuality)
	{
		if (_allScores.Count <= 0 || !TryGetRelatedSong(out var song))
		{
			_bestDishScore = null;
			bestDishQuality = 0f;
			return false;
		}
		_bestDishScore = _allScores[0];
		CookingGameplayScoreData perfectScoreReadonly = song.RuntimeData.GetPerfectScoreReadonly();
		bestDishQuality = _bestDishScore.GetDishQuality(perfectScoreReadonly);
		for (int i = 1; i < _allScores.Count; i++)
		{
			CookingGameplayScoreData cookingGameplayScoreData = _allScores[i];
			float dishQuality = cookingGameplayScoreData.GetDishQuality(perfectScoreReadonly);
			if (dishQuality > bestDishQuality)
			{
				_bestDishScore = cookingGameplayScoreData;
				bestDishQuality = dishQuality;
			}
		}
		return true;
	}

	public bool TryGetBestGradeScore(out CookingGameplayScoreData _bestGradeScore, out CookingGrade _bestGrade)
	{
		_bestGradeScore = null;
		_bestGrade = CookingGrade.F;
		if (_allScores.Count <= 0)
		{
			return false;
		}
		if (!TryGetRelatedSong(out var song))
		{
			Debug.LogError($"Missing song with {_key}!");
			return false;
		}
		CookingGameplayScoreData perfectScoreReadonly = song.RuntimeData.GetPerfectScoreReadonly();
		_bestGradeScore = _allScores[0];
		_bestGrade = _bestGradeScore.GetScoreGrade(perfectScoreReadonly);
		for (int i = 1; i < _allScores.Count; i++)
		{
			CookingGrade scoreGrade = _allScores[i].GetScoreGrade(perfectScoreReadonly);
			if (scoreGrade > _bestGrade)
			{
				_bestGrade = scoreGrade;
			}
		}
		return true;
	}

	public bool TryGetBestScore(out CookingGameplayScoreData _bestScore)
	{
		if (_allScores.Count <= 0)
		{
			_bestScore = null;
			return false;
		}
		_bestScore = _allScores[0];
		return true;
	}

	public bool IsPassed()
	{
		if (TryGetBestGradeScore(out var _, out var _bestGrade))
		{
			return _bestGrade.IsGradePassing();
		}
		return false;
	}
}
