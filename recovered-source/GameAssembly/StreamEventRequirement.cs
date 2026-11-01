using System;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class StreamEventRequirement : Requirement, ISingleStreamRequirement, IStreamEventReceiver
{
	private static TwoWayDictionary<StreamEventRequirementEnum, InteractionMemoryEventEnum> _enumMap = new TwoWayDictionary<StreamEventRequirementEnum, InteractionMemoryEventEnum>((StreamEventRequirementEnum.CumInside, InteractionMemoryEventEnum.CumInside), (StreamEventRequirementEnum.CumOutside, InteractionMemoryEventEnum.CumOutside), (StreamEventRequirementEnum.GotFlacid, InteractionMemoryEventEnum.GotFlacid), (StreamEventRequirementEnum.Insert, InteractionMemoryEventEnum.Insert), (StreamEventRequirementEnum.Unzip, InteractionMemoryEventEnum.Unzip));

	[field: SerializeField]
	public float Difficulty { get; set; }

	[field: SerializeField]
	public int TimesRequired { get; private set; }

	[field: SerializeField]
	public int TimesLimit { get; private set; }

	[field: SerializeField]
	public StreamEventRequirementEnum[] RequirementEnums { get; private set; } = Array.Empty<StreamEventRequirementEnum>();

	[field: SerializeField]
	public int TimesDone { get; private set; }

	public StreamEventRequirement()
	{
	}

	public StreamEventRequirement(int timesRequired, params StreamEventRequirementEnum[] requirementEnums)
	{
		TimesRequired = timesRequired;
		RequirementEnums = requirementEnums;
	}

	public StreamEventRequirement(int timesRequired, int timesLimit, params StreamEventRequirementEnum[] requirementEnums)
	{
		TimesRequired = timesRequired;
		TimesLimit = timesLimit;
		RequirementEnums = requirementEnums;
	}

	public override float GetDifficulty()
	{
		return Difficulty;
	}

	public override bool CheckIfPossible()
	{
		if (TimesLimit > 0)
		{
			return TimesDone < TimesLimit;
		}
		return true;
	}

	public override bool CheckIfDone()
	{
		if (RequirementEnums.Length == 0)
		{
			return true;
		}
		if (TimesRequired > 0 && TimesLimit > 0)
		{
			if (TimesDone >= TimesRequired)
			{
				return TimesDone < TimesLimit;
			}
			return false;
		}
		if (TimesRequired > 0)
		{
			return TimesDone >= TimesRequired;
		}
		if (TimesLimit > 0)
		{
			return TimesDone < TimesLimit;
		}
		return true;
	}

	public void StreamEvent(StreamManager streamManager, InteractionMemoryEventEnum tag)
	{
		if (_enumMap.TryGet(tag, out var requirementEnum) && RequirementEnums.Any((StreamEventRequirementEnum t) => t == requirementEnum))
		{
			using (new StatusChangedAutoReporting(this))
			{
				TimesDone++;
			}
		}
	}

	public void ClearData()
	{
		TimesDone = 0;
	}

	public string GetProgressSummary()
	{
		if (BCocktractContract.Status != CocktractContract.ContractStatus.Available)
		{
			if (TimesDone <= 0)
			{
				return "";
			}
			if (TimesDone > 0)
			{
				return " (" + LOC.UI.Requirements_TimesDone(TimesDone) + ")";
			}
			return " (" + LOC.UI.Requirements_Done + ")";
		}
		return "";
	}

	protected override string GenerateDescriptionInternal()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			for (int i = 0; i < RequirementEnums.Length; i++)
			{
				if (_enumMap.TryGet(RequirementEnums[i], out var value))
				{
					stringBuilder.Append(value.GetLocalizedRequirement());
				}
				else
				{
					stringBuilder.Append(LOC.UI.UI_Error);
				}
				if (RequirementEnums.Length - 1 != i)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(LOC.UI.Requirement_Or);
					stringBuilder.Append(" ");
				}
			}
			stringBuilder.FirstCharToUpper();
			stringBuilder.Append(" ");
			if (TimesLimit > 0 && TimesRequired == TimesLimit - 1)
			{
				stringBuilder.Append(LOC.UI.Requirement_TimesExactly(TimesRequired));
			}
			else if (TimesRequired > 0 && TimesLimit > 0)
			{
				stringBuilder.Append(LOC.UI.Requirement_TimesAtLeastAndLess(TimesRequired - 1, TimesLimit));
			}
			else if (TimesRequired > 0)
			{
				stringBuilder.Append(LOC.UI.Requirement_TimesAtLeast(TimesRequired));
			}
			else if (TimesLimit > 0)
			{
				stringBuilder.Append(LOC.UI.Requirement_TimesLess(TimesLimit));
			}
			stringBuilder.Append(GetProgressSummary());
			return stringBuilder.ToString();
		}
	}
}
