public class BlowjobBrainStateClassDescription : NonJitClassDescription<BlowjobBrainState>
{
	public readonly FieldDictionary<BlowjobBrainState.BlowjobReaction> BlowjobBrainState_BlowjobReactionValues = new FieldDictionary<BlowjobBrainState.BlowjobReaction>();

	public static readonly BlowjobBrainStateClassDescription Instance = new BlowjobBrainStateClassDescription();

	public BlowjobBrainStateClassDescription()
	{
		BooleanValues.Add("IsFree", new NonJitClassDescriptionMember<bool>((BlowjobBrainState x) => x.IsFree, null));
		BlowjobBrainState_BlowjobReactionValues.Add("CurrentReaction", new NonJitClassDescriptionMember<BlowjobBrainState.BlowjobReaction>((BlowjobBrainState x) => x.CurrentReaction, null));
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((BlowjobBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((BlowjobBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((BlowjobBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((BlowjobBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((BlowjobBrainState x) => x.IsStoryState, null));
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((BlowjobBrainState x) => x.IsInterested, null));
		_valueDictionary.Add(typeof(BlowjobBrainState.BlowjobReaction), BlowjobBrainState_BlowjobReactionValues);
	}
}
