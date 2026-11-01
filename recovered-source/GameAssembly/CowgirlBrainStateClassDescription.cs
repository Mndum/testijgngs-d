public class CowgirlBrainStateClassDescription : NonJitClassDescription<CowgirlBrainState>
{
	public readonly FieldDictionary<CowgirlBrainState.CowgirlReactions> CowgirlBrainState_CowgirlReactionsValues = new FieldDictionary<CowgirlBrainState.CowgirlReactions>();

	public static readonly CowgirlBrainStateClassDescription Instance = new CowgirlBrainStateClassDescription();

	public CowgirlBrainStateClassDescription()
	{
		BooleanValues.Add("FlacidityActivated", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.FlacidityActivated, (CowgirlBrainState x, bool v) =>
		{
			x.FlacidityActivated = v;
		}));
		CowgirlBrainState_CowgirlReactionsValues.Add("CurrentReaction", new NonJitClassDescriptionMember<CowgirlBrainState.CowgirlReactions>((CowgirlBrainState x) => x.CurrentReaction, (CowgirlBrainState x, CowgirlBrainState.CowgirlReactions v) =>
		{
			x.CurrentReaction = v;
		}));
		BooleanValues.Add("IsFree", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.IsFree, null));
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.IsStoryState, null));
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((CowgirlBrainState x) => x.IsInterested, null));
		_valueDictionary.Add(typeof(CowgirlBrainState.CowgirlReactions), CowgirlBrainState_CowgirlReactionsValues);
	}
}
