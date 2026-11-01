public class ShowerBrainStateClassDescription : NonJitClassDescription<ShowerBrainState>
{
	public readonly FieldDictionary<ShowerBrainState.ShowerReactions> ShowerBrainState_ShowerReactionsValues = new FieldDictionary<ShowerBrainState.ShowerReactions>();

	public static readonly ShowerBrainStateClassDescription Instance = new ShowerBrainStateClassDescription();

	public ShowerBrainStateClassDescription()
	{
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.IsInterested, null));
		BooleanValues.Add("IsClean", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.IsClean, null));
		BooleanValues.Add("UsedSoap", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.UsedSoap, null));
		ShowerBrainState_ShowerReactionsValues.Add("CurrentReaction", new NonJitClassDescriptionMember<ShowerBrainState.ShowerReactions>((ShowerBrainState x) => x.CurrentReaction, null));
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((ShowerBrainState x) => x.IsStoryState, null));
		_valueDictionary.Add(typeof(ShowerBrainState.ShowerReactions), ShowerBrainState_ShowerReactionsValues);
	}
}
