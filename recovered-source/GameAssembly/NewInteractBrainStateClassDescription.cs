public class NewInteractBrainStateClassDescription : NonJitClassDescription<NewInteractBrainState>
{
	public static readonly NewInteractBrainStateClassDescription Instance = new NewInteractBrainStateClassDescription();

	public NewInteractBrainStateClassDescription()
	{
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((NewInteractBrainState x) => x.IsInterested, null));
		BooleanValues.Add("AlreadyDidGreeting", new NonJitClassDescriptionMember<bool>((NewInteractBrainState x) => x.AlreadyDidGreeting, (NewInteractBrainState x, bool v) =>
		{
			x.AlreadyDidGreeting = v;
		}));
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((NewInteractBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((NewInteractBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((NewInteractBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((NewInteractBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((NewInteractBrainState x) => x.IsStoryState, null));
	}
}
