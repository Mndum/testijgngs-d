public class EmptyBrainStateClassDescription : NonJitClassDescription<EmptyBrainState>
{
	public static readonly EmptyBrainStateClassDescription Instance = new EmptyBrainStateClassDescription();

	public EmptyBrainStateClassDescription()
	{
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((EmptyBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((EmptyBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((EmptyBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((EmptyBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((EmptyBrainState x) => x.IsStoryState, null));
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((EmptyBrainState x) => x.IsInterested, null));
	}
}
