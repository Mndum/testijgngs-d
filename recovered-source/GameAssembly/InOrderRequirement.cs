using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InOrderRequirement : CompoundRequirement, ISingleStreamRequirement, IUpdatableRequirement
{
	[field: SerializeField]
	public int Step { get; private set; }

	public Requirement CurrentlyConsideredRequirement
	{
		get
		{
			if (!_operands.IsInRange(Step))
			{
				return null;
			}
			return _operands[Step].Value;
		}
	}

	public IEnumerable<Requirement> OperandsLeft
	{
		get
		{
			for (int i = Step; i < _operands.Count; i++)
			{
				yield return _operands[i].Value;
			}
		}
	}

	public InOrderRequirement()
	{
	}

	public InOrderRequirement(params RequirementContainer[] operands)
		: this(operands.ToList())
	{
	}

	public InOrderRequirement(List<RequirementContainer> operands)
	{
		_operands = operands;
	}

	public override float GetDifficulty()
	{
		return _operands.Sum((RequirementContainer t) => t.Value.GetDifficulty()) * 1.1f + 0.3f;
	}

	public void Update()
	{
		if (CurrentlyConsideredRequirement.CheckIfDone())
		{
			using (new StatusChangedAutoReporting(this))
			{
				Step++;
			}
		}
	}

	protected override void ChildStatusChanged(Requirement requirement)
	{
		if (CurrentlyConsideredRequirement == requirement)
		{
			Update();
		}
	}

	public override bool CheckIfPossible()
	{
		return OperandsLeft.All((Requirement t) => t.CheckIfPossible());
	}

	public override bool CheckIfDone()
	{
		return Step >= _operands.Count;
	}

	public override string GenerateDescription()
	{
		return LOC.UI.Requirements_InOrderGate(_operands.Count).TMPColorSurround(ColorString) + "\n" + CompoundRequirement.GenerateList(_operands, (int i, Requirement requirement) => (i == Step) ? $">{i + 1}." : $"{i + 1}.");
	}

	public void ClearData()
	{
		Step = 0;
		Update();
	}
}
