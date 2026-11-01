using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StreamForXMinutesRequirement : CompoundRequirement, IStreamMinutePassedReceiver, ISingleStreamRequirement
{
	private abstract class RequirementGeneratorModifier
	{
		public abstract float MinDifficulty { get; }

		public abstract bool Apply(StreamForXMinutesRequirement streamRequirement, float operandDifficultySum, float operandDifficultyMulti, float difficultyLeft);

		public virtual void AfterFinished(StreamForXMinutesRequirement streamRequirement)
		{
		}
	}

	private class ExtendStreamLengthModifier : RequirementGeneratorModifier
	{
		private int maxStreamLength;

		public override float MinDifficulty => 0.1f;

		public ExtendStreamLengthModifier(int maxStreamLength)
		{
			this.maxStreamLength = maxStreamLength;
		}

		public override bool Apply(StreamForXMinutesRequirement streamRequirement, float operandDifficultySum, float operandDifficultyMulti, float difficultyLeft)
		{
			int minutesRequired = streamRequirement.MinutesRequired;
			if (minutesRequired < maxStreamLength)
			{
				int num = Mathf.Min(Mathf.FloorToInt(difficultyLeft / operandDifficultySum), maxStreamLength - minutesRequired);
				if (num > 0)
				{
					int num2 = UnityEngine.Random.Range(1, num + 1);
					streamRequirement.MinutesRequired += num2;
					return false;
				}
			}
			return true;
		}
	}

	private class WhileWearingMerchModifier : RequirementGeneratorModifier
	{
		public override float MinDifficulty => 0.1f;

		public override bool Apply(StreamForXMinutesRequirement streamRequirement, float operandDifficultySum, float operandDifficultyMulti, float difficultyLeft)
		{
			if (GameVariables.Current.HasBothArms)
			{
				streamRequirement.AddOperand(new WhileWearingMerchRequirement());
				return true;
			}
			return false;
		}
	}

	private class WhileStreamPositionModifier : RequirementGeneratorModifier
	{
		public override float MinDifficulty => 0.25f;

		public override bool Apply(StreamForXMinutesRequirement streamRequirement, float operandDifficultySum, float operandDifficultyMulti, float difficultyLeft)
		{
			streamRequirement.AddOperand(WhileStreamPositionRequirement.CreateRandom());
			return true;
		}
	}

	private class WhileStreamViewerShipModifier : RequirementGeneratorModifier
	{
		public override float MinDifficulty => 0.4f;

		public override bool Apply(StreamForXMinutesRequirement streamRequirement, float operandDifficultySum, float operandDifficultyMulti, float difficultyLeft)
		{
			float target = UnityEngine.Random.Range(0.3f, Mathf.Min(difficultyLeft, WhileStreamViewershipRequirement.MaxDifficulty()));
			streamRequirement.AddOperand(WhileStreamViewershipRequirement.CreateFromTargetDifficulty(target));
			return true;
		}
	}

	private class WhileStreamTimeOfDayModifier : RequirementGeneratorModifier
	{
		private int contractDaysTimeLimit;

		public override float MinDifficulty => 0.4f;

		public WhileStreamTimeOfDayModifier(int contractDaysTimeLimit)
		{
			this.contractDaysTimeLimit = contractDaysTimeLimit;
		}

		public override bool Apply(StreamForXMinutesRequirement streamRequirement, float operandDifficultySum, float operandDifficultyMulti, float difficultyLeft)
		{
			int minMinutes = Mathf.FloorToInt(((contractDaysTimeLimit > 1) ? 1.5f : 2.4f) * (float)streamRequirement.MinutesRequired / (float)contractDaysTimeLimit);
			if (WhileStreamTimeOfDayRequirement.TryCreateFromTargetDifficulty(UnityEngine.Random.Range(0.2f, 1f), minMinutes, out var requirementContainer))
			{
				streamRequirement.AddOperand(requirementContainer);
				return true;
			}
			return false;
		}

		public override void AfterFinished(StreamForXMinutesRequirement streamRequirement)
		{
			foreach (RequirementContainer operand in streamRequirement._operands)
			{
				if (operand.Value is WhileStreamTimeOfDayRequirement whileStreamTimeOfDayRequirement)
				{
					whileStreamTimeOfDayRequirement.MakeSureIsPossible(streamRequirement.MinutesRequired, contractDaysTimeLimit);
					break;
				}
			}
		}
	}

	private const int maxCreationIterations = 25;

	public List<IStreamMinutePassedChildReceiver> MinutePassedChildReceivers { get; private set; } = new List<IStreamMinutePassedChildReceiver>();

	[field: SerializeField]
	public int MinutesRequired { get; private set; } = 60;

	[field: SerializeField]
	public int MinutesSucceeded { get; private set; }

	public StreamForXMinutesRequirement()
	{
	}

	public StreamForXMinutesRequirement(int minutesRequired, params RequirementContainer[] operands)
		: this(minutesRequired, operands.ToList())
	{
	}

	public StreamForXMinutesRequirement(int minutesRequired, List<RequirementContainer> operands)
	{
		MinutesRequired = minutesRequired;
		_operands = operands;
	}

	public static StreamForXMinutesRequirement CreateFromTargetDifficulty(float targetDifficulty, int contractDaysTimeLimit)
	{
		int minutesRequired = contractDaysTimeLimit * 20;
		int maxStreamLength = contractDaysTimeLimit * 4 * 60;
		StreamForXMinutesRequirement streamForXMinutesRequirement = new StreamForXMinutesRequirement(minutesRequired, new List<RequirementContainer>());
		List<RequirementGeneratorModifier> list = new List<RequirementGeneratorModifier>
		{
			new ExtendStreamLengthModifier(maxStreamLength),
			new WhileStreamPositionModifier(),
			new WhileWearingMerchModifier(),
			new WhileStreamViewerShipModifier(),
			new WhileStreamTimeOfDayModifier(contractDaysTimeLimit)
		};
		List<RequirementGeneratorModifier> list2 = new List<RequirementGeneratorModifier>();
		for (int i = 0; i < 25; i++)
		{
			(float left, float right) tuple = streamForXMinutesRequirement.CalculateDifficultyBoth();
			float item = tuple.left;
			float item2 = tuple.right;
			float num = targetDifficulty - item * item2;
			if (list.Count == 0)
			{
				break;
			}
			int index = UnityEngine.Random.Range(0, list.Count);
			RequirementGeneratorModifier requirementGeneratorModifier = list[index];
			if (num < requirementGeneratorModifier.MinDifficulty)
			{
				list.RemoveAt(index);
			}
			else if (requirementGeneratorModifier.Apply(streamForXMinutesRequirement, item, item2, num))
			{
				list2.Add(requirementGeneratorModifier);
				list.RemoveAt(index);
			}
		}
		foreach (RequirementGeneratorModifier item3 in list2)
		{
			item3.AfterFinished(streamForXMinutesRequirement);
		}
		return streamForXMinutesRequirement;
	}

	public override void Initialize(CocktractContract cocktractContract, Requirement parent, int depth = 0)
	{
		base.Initialize(cocktractContract, parent, depth);
		MinutePassedChildReceivers = MinutePassedChildReceivers.ClearOrCreate();
		GetAllToList(MinutePassedChildReceivers);
	}

	public (float left, float right) CalculateDifficultyBoth()
	{
		return (left: 1f + _operands.Sum((RequirementContainer t) => t.Value.GetDifficulty()), right: (float)MinutesRequired / 60f);
	}

	public override float GetDifficulty()
	{
		(float, float) tuple = CalculateDifficultyBoth();
		return tuple.Item1 * tuple.Item2;
	}

	public override bool CheckIfPossible()
	{
		return _operands.All((RequirementContainer t) => t.Value.CheckIfPossible());
	}

	public override bool CheckIfDone()
	{
		return MinutesSucceeded >= MinutesRequired;
	}

	public void MinutePassed(StreamManager streamManager)
	{
		foreach (IStreamMinutePassedChildReceiver minutePassedChildReceiver in MinutePassedChildReceivers)
		{
			minutePassedChildReceiver.MinutePassed(streamManager);
		}
		foreach (RequirementContainer operand in _operands)
		{
			if (!operand.Value.CheckIfDone())
			{
				return;
			}
		}
		using (new StatusChangedAutoReporting(this))
		{
			MinutesSucceeded++;
		}
	}

	public void ClearData()
	{
		MinutesSucceeded = 0;
	}

	public string GetProgressSummary()
	{
		if (BCocktractContract.Status != CocktractContract.ContractStatus.Available)
		{
			int num = MinutesRequired - MinutesSucceeded;
			if (MinutesSucceeded == 0)
			{
				return "";
			}
			if (num > 0)
			{
				return " (" + LOC.UI.Requirements_TimeLeft(GlobalFunctions.TimePeriod(num)) + ")";
			}
			return " (" + LOC.UI.Requirements_Done + ")";
		}
		return "";
	}

	public override string GenerateDescription()
	{
		if (_operands.Count == 0)
		{
			return (LOC.UI.Requirements_StreamForXMinutesSimple(GlobalFunctions.TimePeriod(MinutesRequired)) + GetProgressSummary()).TMPColorSurround(ColorString);
		}
		return (LOC.UI.Requirements_StreamForXMinutesCompound(GlobalFunctions.TimePeriod(MinutesRequired)) + GetProgressSummary()).TMPColorSurround(ColorString) + "\n" + CompoundRequirement.GenerateList(_operands);
	}
}
