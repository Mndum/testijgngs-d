using System;
using UnityEngine;

[Serializable]
public class TimeConstraintRequirement : Requirement
{
	private const int MaxContractDurationDays = 14;

	private const int MaxContractDuration = 20160;

	public int LastAllowedMinute => BCocktractContract.AcceptedTimeConsideringStatus + Minutes;

	public int TimeLeft => LastAllowedMinute - BCocktractContract.CurrentTimeConsideringStatus;

	[field: SerializeField]
	public int Minutes { get; private set; }

	public TimeConstraintRequirement()
	{
	}

	public TimeConstraintRequirement(int minutes)
	{
		Minutes = minutes;
	}

	public static TimeConstraintRequirement CreateFromTargetDifficulty(float difficulty)
	{
		return new TimeConstraintRequirement(Mathf.Max(Mathf.CeilToInt(14f / (1f + difficulty)), 4) * 24 * 60);
	}

	public override float GetDifficulty()
	{
		if (Minutes > 0)
		{
			return 20160f / (float)Minutes - 1f;
		}
		return 100f;
	}

	public override bool CheckIfPossible()
	{
		return CheckIfDone();
	}

	public override bool CheckIfDone()
	{
		return BCocktractContract.CurrentTimeConsideringStatus <= LastAllowedMinute;
	}

	protected override string GenerateDescriptionInternal()
	{
		string text = LOC.UI.Requirements_Time(GlobalFunctions.TimePeriod(Minutes));
		if (BCocktractContract.Status != CocktractContract.ContractStatus.Available)
		{
			int timeLeft = TimeLeft;
			if (timeLeft >= Minutes)
			{
				return text;
			}
			if (timeLeft > 0)
			{
				return text + " (" + LOC.UI.Requirements_TimeLeft(GlobalFunctions.TimePeriod(TimeLeft)) + ")";
			}
			return text + " (" + LOC.UI.Requirements_OutOfTime + ")";
		}
		return text;
	}
}
