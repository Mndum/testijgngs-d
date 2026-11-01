using System;

[Flags]
public enum StoryBotDialogueBranchType
{
	Story = 1,
	Reaction = 2,
	Comment = 4,
	All = Story | Reaction | Comment
}
