using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WhileStreamViewershipRequirement : Requirement, IStreamMinutePassedChildReceiver
{
	private const float MinDif = 0.01f;

	private const float OneDifPerPercentageFromMax = 0.05f;

	private bool wasLastSuccessful;

	[field: SerializeField]
	public int ViewersGoal { get; private set; }

	[field: SerializeField]
	public int FollowersGoal { get; private set; }

	[field: SerializeField]
	public int SubsGoal { get; private set; }

	public WhileStreamViewershipRequirement()
	{
	}

	public WhileStreamViewershipRequirement(int viewersGoal, int followersGoal, int subsGoal)
	{
		ViewersGoal = viewersGoal;
		FollowersGoal = followersGoal;
		SubsGoal = subsGoal;
	}

	public static WhileStreamViewershipRequirement CreateFromTargetDifficulty(float target)
	{
		GameVariables current = GameVariables.Current;
		int num = Mathf.Max(current.followers, 10);
		int num2 = Mathf.Max(current.subs, 5);
		float num3 = (target - 0.01f) * 0.05f;
		float num4 = Mathf.Lerp(t: (float)Math.Tanh((float)num / 50000f), a: num3, b: num3 / 10f);
		int viewersGoal = Mathf.CeilToInt((float)num * (1f + num4)) + UnityEngine.Random.Range(1, 20);
		int followersGoal = ((!(UnityEngine.Random.value > 0.2f)) ? Mathf.CeilToInt((float)num * (1f + num4 * 0.5f)) : 0);
		int subsGoal = ((!(UnityEngine.Random.value > 0.1f)) ? Mathf.CeilToInt((float)num2 * (1f + num4 * 0.25f)) : 0);
		return new WhileStreamViewershipRequirement(viewersGoal, followersGoal, subsGoal);
	}

	public static float MaxDifficulty()
	{
		int num = Mathf.Max(GameVariables.Current.followers, 10);
		float num2 = Mathf.Min(1000f / (float)num, 0.5f);
		return 0.01f + num2 / 0.05f;
	}

	private float MathMagic(int v1, int v2)
	{
		return (float)(v1 - v2) / (float)v2;
	}

	public override float GetDifficulty()
	{
		GameVariables current = GameVariables.Current;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		if (current.followers > 0)
		{
			if (ViewersGoal > 0)
			{
				num = MathMagic(ViewersGoal, current.followers);
			}
			if (FollowersGoal > 0)
			{
				num2 = MathMagic(FollowersGoal, current.followers);
			}
		}
		if (current.subs > 0 && SubsGoal > 0)
		{
			num3 = MathMagic(SubsGoal, current.followers);
		}
		float num4 = Mathf.Max(num, num2, num3, 0f);
		return 0.01f + num4 / 0.05f;
	}

	public override bool CheckIfPossible()
	{
		return true;
	}

	public override bool CheckIfDone()
	{
		return wasLastSuccessful;
	}

	public void MinutePassed(StreamManager streamManager)
	{
		GameVariables gameVariables = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables;
		wasLastSuccessful = streamManager.Viewers >= ViewersGoal && gameVariables.followers >= FollowersGoal && gameVariables.subs >= SubsGoal;
	}

	protected override string GenerateDescriptionInternal()
	{
		if (new int[3] { ViewersGoal, FollowersGoal, SubsGoal }.Count((int t) => t > 0) <= 0)
		{
			return Requirement.AlwaysTrueString;
		}
		_ = ColorString;
		List<string> list = new List<string>();
		if (ViewersGoal > 0)
		{
			list.Add(LOC.UI.Requirements_ViewersRequired(ViewersGoal));
		}
		if (FollowersGoal > 0)
		{
			list.Add(LOC.UI.Requirements_FollowersRequired(FollowersGoal));
		}
		if (SubsGoal > 0)
		{
			list.Add(LOC.UI.Requirements_SubsRequired(SubsGoal));
		}
		return LOC.UI.Requirements_Viewership(string.Join(", ", list));
	}
}
