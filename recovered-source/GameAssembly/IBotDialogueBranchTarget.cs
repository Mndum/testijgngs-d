public interface IBotDialogueBranchTarget
{
	StoryBotDialogueBranchType GetBranchType();

	void Activate(ModelBrainState modelBrainState, bool noWrapper = false);
}
