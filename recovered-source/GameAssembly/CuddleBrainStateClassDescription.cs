public class CuddleBrainStateClassDescription : NonJitClassDescription<CuddleBrainState>
{
	public readonly FieldDictionary<CuddleBrainState.CuddleState> CuddleBrainState_CuddleStateValues = new FieldDictionary<CuddleBrainState.CuddleState>();

	public readonly FieldDictionary<CuddleBrainState.CuddleReactions> CuddleBrainState_CuddleReactionsValues = new FieldDictionary<CuddleBrainState.CuddleReactions>();

	public static readonly CuddleBrainStateClassDescription Instance = new CuddleBrainStateClassDescription();

	public CuddleBrainStateClassDescription()
	{
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.IsInterested, null));
		BooleanValues.Add("IsFree", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.IsFree, null));
		BooleanValues.Add("IsReactionAnonHeadPats", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.IsReactionAnonHeadPats, null));
		CuddleBrainState_CuddleStateValues.Add("CurrentState", new NonJitClassDescriptionMember<CuddleBrainState.CuddleState>((CuddleBrainState x) => x.CurrentState, null));
		CuddleBrainState_CuddleReactionsValues.Add("CurrentReaction", new NonJitClassDescriptionMember<CuddleBrainState.CuddleReactions>((CuddleBrainState x) => x.CurrentReaction, null));
		BooleanValues.Add("HasGreeted", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.HasGreeted, null));
		BooleanValues.Add("HasSlept", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.HasSlept, null));
		IntValues.Add("SleptForMinutes", new NonJitClassDescriptionMember<int>((CuddleBrainState x) => x.SleptForMinutes, null));
		IntValues.Add("CuddleStartedAtTime", new NonJitClassDescriptionMember<int>((CuddleBrainState x) => x.CuddleStartedAtTime, null));
		IntValues.Add("CuddleLength", new NonJitClassDescriptionMember<int>((CuddleBrainState x) => x.CuddleLength, null));
		IntValues.Add("CuddleLengthWithoutSleeping", new NonJitClassDescriptionMember<int>((CuddleBrainState x) => x.CuddleLengthWithoutSleeping, null));
		BooleanValues.Add("ShouldTimePass", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.ShouldTimePass, null));
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((CuddleBrainState x) => x.IsStoryState, null));
		_valueDictionary.Add(typeof(CuddleBrainState.CuddleState), CuddleBrainState_CuddleStateValues);
		_valueDictionary.Add(typeof(CuddleBrainState.CuddleReactions), CuddleBrainState_CuddleReactionsValues);
	}
}
