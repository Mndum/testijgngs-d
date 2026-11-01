using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class OrRequirement : CompoundRequirement
{
	public OrRequirement()
	{
	}

	public OrRequirement(params RequirementContainer[] operands)
		: this(operands.ToList())
	{
	}

	public OrRequirement(List<RequirementContainer> operands)
	{
		_operands = operands;
	}

	public override float GetDifficulty()
	{
		return _operands.Min((RequirementContainer t) => t.Value.GetDifficulty());
	}

	public override bool CheckIfPossible()
	{
		return _operands.Any((RequirementContainer t) => t.Value.CheckIfPossible());
	}

	public override bool CheckIfDone()
	{
		return _operands.Any((RequirementContainer t) => t.Value.CheckIfDone());
	}

	public override string GenerateDescription()
	{
		return LOC.UI.Requirements_OrGate.TMPColorSurround(ColorString) + "\n" + CompoundRequirement.GenerateList(_operands);
	}
}
