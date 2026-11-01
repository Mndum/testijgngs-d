using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class CompoundRequirement : Requirement
{
	[SerializeField]
	protected List<RequirementContainer> _operands = new List<RequirementContainer>();

	public ReadOnlyCollection<RequirementContainer> OperandsGet => _operands.AsReadOnly();

	public override void Initialize(CocktractContract cocktractContract, Requirement parent, int depth = 0)
	{
		base.Initialize(cocktractContract, parent, depth);
		foreach (RequirementContainer operand in _operands)
		{
			operand.Value.Initialize(cocktractContract, this, depth + 1);
		}
	}

	public override void GetAllToList<T>(List<T> aggregate)
	{
		base.GetAllToList(aggregate);
		foreach (RequirementContainer operand in _operands)
		{
			operand.Value.GetAllToList(aggregate);
		}
	}

	public static string GenerateList(IEnumerable<RequirementContainer> operands, Func<int, Requirement, string> prefixGenerator = null)
	{
		if (prefixGenerator == null)
		{
			return string.Join("\n", operands.Select((RequirementContainer r) => r.Value.GenerateDescriptionWithDepth()));
		}
		return string.Join("\n", operands.Select((RequirementContainer r, int i) => r.Value.GenerateDescriptionWithDepth(prefixGenerator(i, r.Value))));
	}

	protected sealed override string GenerateDescriptionInternal()
	{
		return "";
	}

	public void AddOperand(RequirementContainer requirementContainer)
	{
		_operands.Add(requirementContainer);
	}

	public void SetOperands(List<RequirementContainer> requirementContainers)
	{
		_operands = requirementContainers;
	}
}
