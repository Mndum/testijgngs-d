using System;
using System.Collections.Generic;
using System.Text;
using Fungus;
using StoryConditions;
using UnityEngine;

public class StoryEntryPointBase
{
	public abstract class StoryBranchGenericBase
	{
		public StoryBranchTarget BranchTarget { get; private set; }

		public StoryCondition[] AdditionalConditions { get; protected set; }

		public Action OnActivateAction { get; set; }

		public virtual bool CanHappen => AdditionalConditionCheck;

		protected bool AdditionalConditionCheck => AdditionalConditions.ConditionsMet();

		protected StoryBranchGenericBase(StoryBranchTarget branchTarget)
		{
			BranchTarget = branchTarget;
			AdditionalConditions = Array.Empty<StoryCondition>();
		}

		public StoryBranchGenericBase(StoryBranchTarget branchTarget, StoryCondition[] additionalConditions)
		{
			BranchTarget = branchTarget;
			AdditionalConditions = additionalConditions;
		}

		public virtual StringBuilder EditorSummary(StringBuilder sb, Dictionary<string, Block> blocks, bool richText = true, bool runTime = false)
		{
			sb.Append(BranchTarget);
			Block block;
			if (blocks != null)
			{
				if (BranchTarget is StoryBlockTarget storyBlockTarget && !blocks.TryGetValue(storyBlockTarget.TargetBlockName, out var _))
				{
					sb.TMPColorSurroundStringBuilderAppend(Color.red, "BLOCK NOT FOUND");
				}
			}
			else if (BranchTarget is StoryBlockTarget storyBlockTarget2 && !SingletonLoadableMonoBehaviour<GameScript>.Instance.Flowchart.TryGetBlock(storyBlockTarget2.TargetBlockName, out block))
			{
				sb.TMPColorSurroundStringBuilderAppend(Color.red, "BLOCK NOT FOUND");
			}
			sb.AppendLine();
			sb.AppendLine(AdditionalConditions.Explain(richText, runTime));
			return sb;
		}

		public string Summary(bool richText = true, bool runTime = false)
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				EditorSummary(stringBuilder, null, richText, runTime);
				return stringBuilder.ToString();
			}
		}

		public void AddConnectedBlocks(ref List<Block> connectedBlocks, Flowchart flowchart)
		{
			if (BranchTarget is StoryBlockTarget storyBlockTarget)
			{
				connectedBlocks.Add(storyBlockTarget.GetTargetBlock(flowchart));
			}
		}

		public bool MayCallBlock(Block block)
		{
			if (BranchTarget is StoryBlockTarget storyBlockTarget && block.BlockName == storyBlockTarget.TargetBlockName)
			{
				return true;
			}
			return false;
		}
	}

	public abstract class StoryBranchGenericFlagBase : StoryBranchGenericBase
	{
		public override bool CanHappen
		{
			get
			{
				if (string.IsNullOrWhiteSpace(RelatedFlag) || !IsFlagBlocking)
				{
					return AdditionalConditionCheck;
				}
				if (!GameVariables.Current.CheckFlag(RelatedFlag))
				{
					return AdditionalConditionCheck;
				}
				return false;
			}
		}

		public string RelatedFlag { get; private set; }

		public bool IsFlagBlocking { get; set; } = true;

		public bool IsOneTime
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(RelatedFlag))
				{
					return IsFlagBlocking;
				}
				return false;
			}
		}

		public bool IsFlagSet
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(RelatedFlag))
				{
					return GameVariables.Current.CheckFlag(RelatedFlag);
				}
				return false;
			}
		}

		public bool WaitTillBlockFinished { get; set; }

		public StoryBranchGenericFlagBase(StoryBranchTarget branchTarget, string relatedFlag, StoryCondition[] additionalConditions)
			: base(branchTarget, additionalConditions)
		{
			RelatedFlag = relatedFlag;
		}

		public override StringBuilder EditorSummary(StringBuilder sb, Dictionary<string, Block> blocks, bool richText = true, bool runTime = false)
		{
			base.EditorSummary(sb, blocks, richText, runTime);
			sb.AppendLine(runTime ? FlagSummaryRuntime() : FlagSummary());
			return sb;
		}

		public string FlagSummary()
		{
			if (string.IsNullOrWhiteSpace(RelatedFlag))
			{
				return "No related flag!";
			}
			return RelatedFlag + (IsFlagBlocking ? "" : "(not blocking)");
		}

		public string FlagSummaryRuntime()
		{
			if (string.IsNullOrWhiteSpace(RelatedFlag))
			{
				return "No related flag!";
			}
			return RelatedFlag + (IsFlagSet ? "(Flag already set)" : "(Flag not set)") + (IsFlagBlocking ? "" : "(not blocking)");
		}
	}

	public string EntryPointExplanation { get; set; }

	public bool HasExplanation => !string.IsNullOrWhiteSpace(EntryPointExplanation);

	public void AppendExplanation(StringBuilder stringBuilder)
	{
		if (HasExplanation)
		{
			stringBuilder.Append("Explanation: ");
			stringBuilder.AppendLine(EntryPointExplanation);
		}
	}
}
public abstract class StoryEntryPointBase<TBranch> : StoryEntryPointBase, IBranchContainer<TBranch> where TBranch : StoryEntryPointBase.StoryBranchGenericBase
{
	public abstract IEnumerable<TBranch> GetBranches();

	public void AddConnectedBlocks(ref List<Block> connectedBlocks, Flowchart flowchart)
	{
		foreach (TBranch branch in GetBranches())
		{
			branch.AddConnectedBlocks(ref connectedBlocks, flowchart);
		}
	}

	public bool MayCallBlock(Block block)
	{
		foreach (TBranch branch in GetBranches())
		{
			if (branch.MayCallBlock(block))
			{
				return true;
			}
		}
		return false;
	}
}
