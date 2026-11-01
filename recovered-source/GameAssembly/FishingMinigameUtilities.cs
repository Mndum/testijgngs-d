using System;
using System.Text;

public static class FishingMinigameUtilities
{
	private class PercentileInfo
	{
		public float Percentile { get; }

		public Func<string> NameFunc { get; }

		public string GetName()
		{
			return NameFunc();
		}

		public PercentileInfo(float percentile, Func<string> nameFunc)
		{
			Percentile = percentile;
			NameFunc = nameFunc;
		}
	}

	public const float BigPercentile = 0.8f;

	private static PercentileInfo[] percentiles = new PercentileInfo[9]
	{
		new PercentileInfo(0.1f, () => LOC.UI.FishSize_10Percentile),
		new PercentileInfo(0.2f, () => LOC.UI.FishSize_20Percentile),
		new PercentileInfo(0.3f, () => LOC.UI.FishSize_30Percentile),
		new PercentileInfo(0.4f, () => LOC.UI.FishSize_40Percentile),
		new PercentileInfo(0.6f, () => LOC.UI.FishSize_60Percentile),
		new PercentileInfo(0.7f, () => LOC.UI.FishSize_70Percentile),
		new PercentileInfo(0.8f, () => LOC.UI.FishSize_80Percentile),
		new PercentileInfo(0.9f, () => LOC.UI.FishSize_90Percentile),
		new PercentileInfo(1f, () => LOC.UI.FishSize_100Percentile)
	};

	public static bool HasFlagFast(this FishRegisteringFlagsEnum e, FishRegisteringFlagsEnum flag)
	{
		return (e & flag) != 0;
	}

	public static void AppendLocalizedString(this FishRegisteringFlagsEnum flag, StringBuilder stringBuilder)
	{
		if (flag == FishRegisteringFlagsEnum.None)
		{
			return;
		}
		using (stringBuilder.StartColorTagUsing(MyColor32Presets.Green))
		{
			if (flag.HasFlagFast(FishRegisteringFlagsEnum.FirstTime))
			{
				stringBuilder.EnsureLastCharWhiteSpaceOrBeginning();
				stringBuilder.Append(LOC.UI.FishFirstTimeCaught);
			}
			if (flag.HasFlagFast(FishRegisteringFlagsEnum.NewMinSizeRecord))
			{
				stringBuilder.EnsureLastCharWhiteSpaceOrBeginning();
				stringBuilder.Append(LOC.UI.FishNewMinSizeRecord);
			}
			if (flag.HasFlagFast(FishRegisteringFlagsEnum.NewMaxSizeRecord))
			{
				stringBuilder.EnsureLastCharWhiteSpaceOrBeginning();
				stringBuilder.Append(LOC.UI.FishNewMaxSizeRecord);
			}
		}
	}

	public static string GetSizeDescriptionForFishPercentile(float value)
	{
		for (int i = 0; i < percentiles.Length; i++)
		{
			if (value <= percentiles[i].Percentile)
			{
				return percentiles[i].GetName();
			}
		}
		return percentiles[percentiles.Length - 1].GetName();
	}
}
