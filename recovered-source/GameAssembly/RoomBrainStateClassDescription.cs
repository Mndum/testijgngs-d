public class RoomBrainStateClassDescription : NonJitClassDescription<RoomBrainState>
{
	public static readonly RoomBrainStateClassDescription Instance = new RoomBrainStateClassDescription();

	public RoomBrainStateClassDescription()
	{
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((RoomBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((RoomBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((RoomBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((RoomBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((RoomBrainState x) => x.IsStoryState, null));
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((RoomBrainState x) => x.IsInterested, null));
	}
}
