using UnityEngine;

public static class CookingGameplayScoreDataExtensions
{
	public static string ToFriendlyString(this CookingGrade grade)
	{
		return grade switch
		{
			CookingGrade.F => "F", 
			CookingGrade.D => "D", 
			CookingGrade.C => "C", 
			CookingGrade.B => "B", 
			CookingGrade.A => "A", 
			CookingGrade.S => "S", 
			CookingGrade.SS => "SS", 
			CookingGrade.SS_plus => "SS+", 
			CookingGrade.SS_plus2 => "SS++", 
			_ => "F", 
		};
	}

	public static bool IsGradePassing(this CookingGrade grade)
	{
		return grade >= CookingGrade.D;
	}

	public static Color32 GradeToColor(this CookingGrade grade)
	{
		return grade switch
		{
			CookingGrade.F => MyColor32Presets.Red, 
			CookingGrade.D => MyColor32Presets.DarkOrange, 
			CookingGrade.C => MyColor32Presets.LightOrange, 
			CookingGrade.B => MyColor32Presets.Yellow, 
			CookingGrade.A => MyColor32Presets.YellowGreen, 
			CookingGrade.S => MyColor32Presets.Green, 
			CookingGrade.SS => MyColor32Presets.Cyan, 
			CookingGrade.SS_plus => MyColor32Presets.SeaGreen, 
			CookingGrade.SS_plus2 => MyColor32Presets.VividPurple, 
			_ => MyColor32Presets.White, 
		};
	}

	public static bool IsScoreValid(this CookingGameplayScoreData scoreData)
	{
		if (scoreData == null || !scoreData.IsComplete)
		{
			return false;
		}
		return true;
	}

	public static bool IsScoreValid(this CookingGameplayScoreData scoreData, int version)
	{
		if (scoreData == null || !scoreData.IsComplete)
		{
			return false;
		}
		if (scoreData.SongVersion < version)
		{
			return false;
		}
		return true;
	}
}
