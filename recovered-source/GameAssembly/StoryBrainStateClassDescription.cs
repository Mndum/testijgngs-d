public class StoryBrainStateClassDescription : NonJitClassDescription<StoryBrainState>
{
	public static readonly StoryBrainStateClassDescription Instance = new StoryBrainStateClassDescription();

	public StoryBrainStateClassDescription()
	{
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((StoryBrainState x) => x.IsStoryState, null));
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((StoryBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((StoryBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((StoryBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((StoryBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((StoryBrainState x) => x.IsInterested, null));
	}
}
