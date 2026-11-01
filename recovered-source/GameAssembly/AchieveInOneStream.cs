using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AchieveInOneStream : CompoundRequirement, IStreamStartAndEndReceiver
{
	public List<ISingleStreamRequirement> StreamRequirements { get; private set; } = new List<ISingleStreamRequirement>();

	[field: SerializeField]
	public bool Succeeded { get; private set; }

	[field: SerializeField]
	public int Failures { get; private set; }

	public AchieveInOneStream()
	{
	}

	public AchieveInOneStream(params RequirementContainer[] operands)
		: this(operands.ToList())
	{
	}

	public AchieveInOneStream(List<RequirementContainer> operands)
	{
		_operands = operands;
	}

	public override void Initialize(CocktractContract cocktractContract, Requirement parent, int depth = 0)
	{
		base.Initialize(cocktractContract, parent, depth);
		StreamRequirements = StreamRequirements.ClearOrCreate();
		GetAllToList(StreamRequirements);
	}

	public override float GetDifficulty()
	{
		return _operands.Sum((RequirementContainer t) => t.Value.GetDifficulty()) * 2f + 0.5f;
	}

	public override bool CheckIfPossible()
	{
		if (StreamRequirements.Count > 0)
		{
			foreach (RequirementContainer operand in _operands)
			{
				Requirement val = operand.Value;
				if (!val.CheckIfPossible() && StreamRequirements.All((ISingleStreamRequirement x) => x != val))
				{
					return false;
				}
			}
		}
		return true;
	}

	public override bool CheckIfDone()
	{
		if (StreamRequirements.Count > 0)
		{
			if (Succeeded)
			{
				return true;
			}
			if (!StreamRequirements.Any())
			{
				return _operands.All((RequirementContainer t) => t.Value.CheckIfDone());
			}
			return false;
		}
		return true;
	}

	public void StreamStarted(StreamManager streamManager)
	{
		foreach (ISingleStreamRequirement streamRequirement in StreamRequirements)
		{
			streamRequirement.ClearData();
		}
	}

	public void StreamFinished(StreamManager streamManager)
	{
	}

	public void StreamLateFinished(StreamManager streamManager)
	{
		if (!Succeeded)
		{
			if (_operands.All((RequirementContainer t) => t.Value.CheckIfDone()))
			{
				Succeeded = true;
				ReportStatusChanged();
			}
			else
			{
				Succeeded = false;
				Failures++;
			}
		}
	}

	public override string GenerateDescription()
	{
		if (_operands.Count == 0)
		{
			return Requirement.AlwaysTrueString.TMPColorSurround(ColorString);
		}
		return LOC.UI.Requirements_InOneStreamCompound.TMPColorSurround(ColorString) + "\n" + CompoundRequirement.GenerateList(_operands);
	}
}
