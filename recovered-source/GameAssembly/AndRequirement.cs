using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class AndRequirement : CompoundRequirement
{
	public AndRequirement()
	{
	}

	public AndRequirement(params RequirementContainer[] operands)
		: this(operands.ToList())
	{
	}

	public AndRequirement(List<RequirementContainer> operands)
	{
		_operands = operands;
	}

	public override float GetDifficulty()
	{
		return _operands.Sum((RequirementContainer t) => t.Value.GetDifficulty());
	}

	public override bool CheckIfPossible()
	{
		return _operands.All((RequirementContainer t) => t.Value.CheckIfPossible());
	}

	public override bool CheckIfDone()
	{
		return _operands.All((RequirementContainer t) => t.Value.CheckIfDone());
	}

	public override string GenerateDescription()
	{
		return LOC.UI.Requirements_AndGate.TMPColorSurround(ColorString) + "\n" + CompoundRequirement.GenerateList(_operands);
	}
}
