using System;
using UnityEngine;

[Serializable]
public class WhileStreamTimeOfDayRequirement : Requirement, IStreamMinutePassedChildReceiver
{
	public const int MaxMinutes = 480;

	public int HowManyMinutesInDay
	{
		get
		{
			if (!IsInverted)
			{
				return Time2 - Time1;
			}
			return Time2 + 1440 - Time1;
		}
	}

	[field: SerializeField]
	public bool WasLastSuccessful { get; private set; }

	[field: SerializeField]
	public int Time1 { get; private set; }

	[field: SerializeField]
	public int Time2 { get; private set; }

	private bool IsInverted => Time1 > Time2;

	public WhileStreamTimeOfDayRequirement()
	{
	}

	public WhileStreamTimeOfDayRequirement(int time1, int time2)
	{
		Time1 = GlobalFunctions.Mod(time1, 1440);
		Time2 = GlobalFunctions.Mod(time2, 1440);
	}

	public static bool TryCreateFromTargetDifficulty(float difficulty, int minMinutes, out WhileStreamTimeOfDayRequirement requirementContainer)
	{
		requirementContainer = null;
		if (minMinutes > 480)
		{
			return false;
		}
		int num = (int)Mathf.Lerp(minMinutes, 480f, difficulty);
		int num2 = UnityEngine.Random.Range(0, 1440);
		int time = num2 + num;
		requirementContainer = new WhileStreamTimeOfDayRequirement(num2, time);
		return true;
	}

	public bool MakeSureIsPossible(int streamRequirementMinutesRequired, int contractDaysTimeLimit)
	{
		int num = contractDaysTimeLimit * HowManyMinutesInDay;
		if (streamRequirementMinutesRequired > num)
		{
			if (streamRequirementMinutesRequired > 1000 * contractDaysTimeLimit)
			{
				return false;
			}
			int num2 = Mathf.CeilToInt(1.2f * (float)streamRequirementMinutesRequired / (float)contractDaysTimeLimit);
			int num3 = UnityEngine.Random.Range(0, 1440);
			int x = num3 + num2;
			Time1 = GlobalFunctions.Mod(num3, 1440);
			Time2 = GlobalFunctions.Mod(x, 1440);
		}
		return true;
	}

	public override float GetDifficulty()
	{
		return (float)HowManyMinutesInDay / 480f;
	}

	public override bool CheckIfPossible()
	{
		return true;
	}

	public override bool CheckIfDone()
	{
		return WasLastSuccessful;
	}

	public void MinutePassed(StreamManager streamManager)
	{
		int timeDay = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.TimeDay;
		if (IsInverted)
		{
			WasLastSuccessful = timeDay <= Time2 || Time1 <= timeDay;
		}
		else
		{
			WasLastSuccessful = Time1 <= timeDay && timeDay <= Time2;
		}
	}

	protected override string GenerateDescriptionInternal()
	{
		if (IsInverted)
		{
			return LOC.UI.Requirement_TimeBeforeAfter(GameVariables.TimeToHourString(Time2), GameVariables.TimeToHourString(Time1));
		}
		return LOC.UI.Requirement_TimeBetween(GameVariables.TimeToHourString(Time1), GameVariables.TimeToHourString(Time2));
	}
}
