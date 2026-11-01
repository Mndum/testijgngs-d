using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class CookingGameplayScoreData : ICloneable<CookingGameplayScoreData>, ICloneable
{
	private sealed class RawScoreRelationalComparer : IComparer<CookingGameplayScoreData>
	{
		public int Compare(CookingGameplayScoreData x, CookingGameplayScoreData y)
		{
			return y.GetScoreWithMultipliers().CompareTo(x.GetScoreWithMultipliers());
		}
	}

	public int GreatCount;

	public int GoodCount;

	public int BadCount;

	public int MissCount;

	public int MaxCombo;

	public double RawScore;

	public CookingGameplayModifiers ActiveModifiers;

	public double AccuracyScore;

	public int SongVersion;

	public bool IsComplete;

	public const double AccuracyScoreGreat = 1.0;

	public const double AccuracyScoreGood = 0.75;

	public const double AccuracyScoreBad = 0.5;

	public const double AccuracyScoreWrongNoteMiss = 0.0;

	public static IComparer<CookingGameplayScoreData> RawScoreDescendingComparer { get; } = new RawScoreRelationalComparer();

	public static CookingGameplayScoreData CreateEmpty(CookingGameplayModifiers activeModifiers)
	{
		return new CookingGameplayScoreData
		{
			ActiveModifiers = activeModifiers
		};
	}

	public static CookingGameplayScoreData CreatePerfect(CookingSong song)
	{
		CookingGameplayScoreData cookingGameplayScoreData = new CookingGameplayScoreData();
		int _currentCombo = 0;
		foreach (CookingSongNoteEvent note in song.RuntimeData.Notes)
		{
			if (note.IsDuration)
			{
				cookingGameplayScoreData.RegisterNote(ref _currentCombo, NoteClickedScoreEnum.Great);
				cookingGameplayScoreData.RegisterNote(ref _currentCombo, NoteClickedScoreEnum.Great);
			}
			else
			{
				cookingGameplayScoreData.RegisterNote(ref _currentCombo, NoteClickedScoreEnum.Great);
			}
		}
		cookingGameplayScoreData.IsComplete = true;
		return cookingGameplayScoreData;
	}

	public double GetScoreWithMultipliers()
	{
		return ActiveModifiers.DetermineScoreMultiplier() * RawScore;
	}

	public double GetAccuracyScorePercent(CookingGameplayScoreData perfectScore)
	{
		return AccuracyScore / perfectScore.AccuracyScore;
	}

	public CookingGrade GetScoreGrade(CookingGameplayScoreData perfectScore)
	{
		double accuracyScorePercent = GetAccuracyScorePercent(perfectScore);
		if (accuracyScorePercent >= 0.99 && MissCount <= 0 && BadCount <= 0 && GoodCount <= 0)
		{
			if (ActiveModifiers.HasFlagFast(CookingGameplayModifiers.FastMode))
			{
				return CookingGrade.SS_plus;
			}
			if (ActiveModifiers.HasFlagFast(CookingGameplayModifiers.InsaneFastTimeMode) || ActiveModifiers.HasFlagFast(CookingGameplayModifiers.UltraFastTimeMode))
			{
				return CookingGrade.SS_plus2;
			}
			return CookingGrade.SS;
		}
		if (accuracyScorePercent >= 0.9 && MissCount <= 0)
		{
			return CookingGrade.S;
		}
		if (accuracyScorePercent >= 0.85)
		{
			return CookingGrade.A;
		}
		if (accuracyScorePercent >= 0.8)
		{
			return CookingGrade.B;
		}
		if (accuracyScorePercent >= 0.75)
		{
			return CookingGrade.C;
		}
		if (accuracyScorePercent >= 0.7)
		{
			return CookingGrade.D;
		}
		return CookingGrade.F;
	}

	public double RegisterNote(ref int _currentCombo, NoteClickedScoreEnum clickedScoreEnum)
	{
		if (clickedScoreEnum == NoteClickedScoreEnum.None)
		{
			return 0.0;
		}
		int num = 0;
		double num2 = 0.0;
		switch (clickedScoreEnum)
		{
		case NoteClickedScoreEnum.Great:
			_currentCombo++;
			num = 300;
			num2 = 1.0;
			GreatCount++;
			break;
		case NoteClickedScoreEnum.Good:
			_currentCombo++;
			num = 150;
			num2 = 0.75;
			GoodCount++;
			break;
		case NoteClickedScoreEnum.Bad:
			_currentCombo++;
			num = 100;
			num2 = 0.5;
			BadCount++;
			break;
		case NoteClickedScoreEnum.WrongNote:
		case NoteClickedScoreEnum.Miss:
			_currentCombo = 0;
			num2 = 0.0;
			MissCount++;
			break;
		}
		AccuracyScore += num2;
		if (_currentCombo > MaxCombo)
		{
			MaxCombo = _currentCombo;
		}
		RawScore += _currentCombo * num;
		return num2;
	}

	public static bool IsSuccess(NoteClickedScoreEnum startScore)
	{
		if (startScore != NoteClickedScoreEnum.None && (uint)(startScore - 4) > 1u)
		{
			return true;
		}
		return false;
	}

	public CookingGameplayScoreData Clone()
	{
		return (CookingGameplayScoreData)MemberwiseClone();
	}

	public float GetDishQuality(CookingGameplayScoreData perfectScore)
	{
		double num = GetAccuracyScorePercent(perfectScore) * 0.9;
		if (perfectScore.GreatCount == GreatCount)
		{
			num += 0.1;
		}
		num *= ActiveModifiers.DetermineScoreMultiplier();
		return (float)num;
	}

	public void AppendString(StringBuilder stringBuilder, CookingGameplayScoreData perfectScore)
	{
		stringBuilder.AppendLine(LOC.UI.CookingMinigame_Results);
		if (ActiveModifiers != CookingGameplayModifiers.NoMods)
		{
			stringBuilder.AppendLineStringDisplay(LOC.UI.CookingSong_ActiveModifiers, ActiveModifiers.ToLocalizedString());
		}
		stringBuilder.AppendLineStringDisplay(LOC.UI.CookingMinigame_Grade, GetScoreGrade(perfectScore).ToFriendlyString());
		stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Great, GreatCount);
		stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Good, GoodCount);
		stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Bad, BadCount);
		stringBuilder.AppendLineIntDisplay(LOC.UI.CookingMinigame_Miss, MissCount);
		stringBuilder.AppendIntDisplay(LOC.UI.CookingMinigame_MaxCombo, MaxCombo);
		if (MaxCombo == perfectScore.MaxCombo)
		{
			stringBuilder.AppendColorTagStart(Color.green);
			stringBuilder.AppendSurroundParentheses(LOC.UI.CookingMinigame_FullCombo);
			stringBuilder.AppendColorTagEnd();
		}
		stringBuilder.AppendLine();
		stringBuilder.AppendIntDisplay(LOC.UI.CookingMinigame_Score, GetScoreWithMultipliers());
		stringBuilder.AppendLine();
		stringBuilder.AppendPercentDisplay(LOC.UI.CookingMinigame_Accuracy, GetAccuracyScorePercent(perfectScore));
		stringBuilder.AppendLine();
	}
}
