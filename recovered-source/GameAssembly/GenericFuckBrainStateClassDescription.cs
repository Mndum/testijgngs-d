public class GenericFuckBrainStateClassDescription : NonJitClassDescription<GenericFuckBrainState>
{
	public static readonly GenericFuckBrainStateClassDescription Instance = new GenericFuckBrainStateClassDescription();

	public GenericFuckBrainStateClassDescription()
	{
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((GenericFuckBrainState x) => x.IsInterested, null));
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((GenericFuckBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((GenericFuckBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((GenericFuckBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((GenericFuckBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((GenericFuckBrainState x) => x.IsStoryState, null));
	}
}
