public class OldMaidBrainStateClassDescription : NonJitClassDescription<OldMaidBrainState>
{
	public static readonly OldMaidBrainStateClassDescription Instance = new OldMaidBrainStateClassDescription();

	public OldMaidBrainStateClassDescription()
	{
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((OldMaidBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((OldMaidBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((OldMaidBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((OldMaidBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((OldMaidBrainState x) => x.IsStoryState, null));
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((OldMaidBrainState x) => x.IsInterested, null));
	}
}
