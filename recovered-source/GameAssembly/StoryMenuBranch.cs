using System.Collections.Generic;
using System.Text;
using Fungus;
using JetBrains.Annotations;
using StoryConditions;

public sealed class StoryMenuBranch : StoryEntryPointBase.StoryBranchGenericFlagBase
{
	public LocalizedStringReference MenuText { get; set; }

	public bool ShowerGrayedOutWhenUnavailable { get; set; }

	public StoryMenuBranch(LocalizedStringReference menuText, StoryBranchTarget branchTarget, string relatedFlag, [ItemNotNull] params StoryCondition[] additionalConditions)
		: base(branchTarget, relatedFlag, additionalConditions)
	{
		MenuText = menuText;
	}

	public override StringBuilder EditorSummary(StringBuilder sb, Dictionary<string, Block> blocks, bool richText = true, bool runTime = false)
	{
		sb.AppendLine("Text: \"" + MenuText.GetString() + "\"");
		return base.EditorSummary(sb, blocks, richText, runTime);
	}
}
