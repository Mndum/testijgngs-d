using System.Linq;
using UnityEngine;

public static class CookingGameplayUtilities
{
	public static Color LeftClickMainColor = new Color32(39, 131, 242, 255);

	public static Color RightClickMainColor = new Color32(0, 145, 68, 255);

	public static Color LeftClickSecondColor = new Color32(0, 204, 255, 255);

	public static Color RightClickSecondColor = new Color32(60, 179, 113, 255);

	public static readonly CookingGameplayModifiers[] AllModifiers = new CookingGameplayModifiers[5]
	{
		CookingGameplayModifiers.SlowMode,
		CookingGameplayModifiers.FastMode,
		CookingGameplayModifiers.UltraFastTimeMode,
		CookingGameplayModifiers.InsaneFastTimeMode,
		CookingGameplayModifiers.Practice
	};

	public const double MinPixelsPerSecond = 320.0;

	public const double AveragePixelsPerSecond = 640.0;

	public const double MaxPixelsPerSecond = 1920.0;

	public static bool HasFlagFast(this CookingInputFlagEnum self, CookingInputFlagEnum flag)
	{
		return (self & flag) == flag;
	}

	public static bool HasFlagFast(this CookingInputConsumeResult self, CookingInputConsumeResult flag)
	{
		return (self & flag) == flag;
	}

	public static bool HasFlagFast(this CookingGameplayModifiers self, CookingGameplayModifiers flag)
	{
		return (self & flag) == flag;
	}

	public static bool AnyModifiers(this CookingGameplayModifiers self)
	{
		return self != CookingGameplayModifiers.NoMods;
	}

	public static CookingGameplayModifiers[] SplitFlagsToSingle(CookingGameplayModifiers flags)
	{
		return AllModifiers.Where((CookingGameplayModifiers x) => (x & flags) != 0).ToArray();
	}

	public static string ToLocalizedString(this CookingGameplayModifiers flags)
	{
		return string.Join(", ", from x in SplitFlagsToSingle(flags)
			select x.ToLocalizedStringSingle());
	}

	public static string ToLocalizedStringSingle(this CookingGameplayModifiers modifier)
	{
		return modifier switch
		{
			CookingGameplayModifiers.SlowMode => LOC.UI.CookingSong_ModifierSlowMode, 
			CookingGameplayModifiers.FastMode => LOC.UI.CookingSong_ModifierFastMode, 
			CookingGameplayModifiers.UltraFastTimeMode => LOC.UI.CookingSong_ModifierUltraFastMode, 
			CookingGameplayModifiers.InsaneFastTimeMode => LOC.UI.CookingSong_ModifierInsaneMode, 
			CookingGameplayModifiers.Practice => LOC.UI.CookingSong_ModifierPractice, 
			_ => LOC.UI.CookingSong_NoModifiers, 
		};
	}

	public static float DetermineTargetPitch(this CookingGameplayModifiers modifiers)
	{
		return (modifiers & CookingGameplayModifiers.AllSpeedMods) switch
		{
			CookingGameplayModifiers.SlowMode => 0.75f, 
			CookingGameplayModifiers.FastMode => 1.25f, 
			CookingGameplayModifiers.UltraFastTimeMode => 1.5f, 
			CookingGameplayModifiers.InsaneFastTimeMode => 1.75f, 
			_ => 1f, 
		};
	}

	public static double DetermineScoreMultiplier(this CookingGameplayModifiers modifiers)
	{
		float num = (((modifiers & CookingGameplayModifiers.AutoPlay) != 0) ? 0.5f : 1f);
		if ((modifiers & CookingGameplayModifiers.Practice) != 0)
		{
			return 0.0;
		}
		return (modifiers & CookingGameplayModifiers.AllSpeedMods) switch
		{
			CookingGameplayModifiers.SlowMode => 0.3 * (double)num, 
			CookingGameplayModifiers.FastMode => 1.5 * (double)num, 
			CookingGameplayModifiers.UltraFastTimeMode => 2.0 * (double)num, 
			CookingGameplayModifiers.InsaneFastTimeMode => 2.1 * (double)num, 
			_ => 1.0 * (double)num, 
		};
	}

	public static double GetPixelsPerSecondsFromDifficulty(double difficulty)
	{
		if (difficulty < 5.0)
		{
			return Mathf.Lerp(320f, 640f, (float)difficulty / 5f);
		}
		return Mathf.Lerp(640f, 1920f, (float)(difficulty - 5.0) / 5f);
	}

	public static double GetSpeedDifficultyFromPixelsPerSecond(double pixelsPerSecond)
	{
		if (pixelsPerSecond < 320.0)
		{
			return 0.0;
		}
		if (pixelsPerSecond < 640.0)
		{
			return Mathf.InverseLerp(320f, 640f, (float)pixelsPerSecond) * 5f;
		}
		if (pixelsPerSecond < 1920.0)
		{
			return Mathf.InverseLerp(640f, 1920f, (float)pixelsPerSecond) * 5f + 5f;
		}
		return 10.0;
	}
}
