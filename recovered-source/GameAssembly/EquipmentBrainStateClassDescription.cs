public class EquipmentBrainStateClassDescription : NonJitClassDescription<EquipmentBrainState>
{
	public static readonly EquipmentBrainStateClassDescription Instance = new EquipmentBrainStateClassDescription();

	public EquipmentBrainStateClassDescription()
	{
		BooleanValues.Add("IsReacting", new NonJitClassDescriptionMember<bool>((EquipmentBrainState x) => x.IsReacting, null));
		BooleanValues.Add("IsReactingUnstoppable", new NonJitClassDescriptionMember<bool>((EquipmentBrainState x) => x.IsReactingUnstoppable, null));
		BooleanValues.Add("IsTalking", new NonJitClassDescriptionMember<bool>((EquipmentBrainState x) => x.IsTalking, null));
		BooleanValues.Add("CanBeChanged", new NonJitClassDescriptionMember<bool>((EquipmentBrainState x) => x.CanBeChanged, null));
		BooleanValues.Add("IsStoryState", new NonJitClassDescriptionMember<bool>((EquipmentBrainState x) => x.IsStoryState, null));
		BooleanValues.Add("IsInterested", new NonJitClassDescriptionMember<bool>((EquipmentBrainState x) => x.IsInterested, null));
	}
}
