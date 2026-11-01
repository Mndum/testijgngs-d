public class NonJitClassDescriptionSingleton : NonJitClassDescriptionSingletonBase<NonJitClassDescriptionSingleton>
{
	private static NonJitClassDescriptionSingleton _instance;

	public static NonJitClassDescriptionSingleton Instance => _instance ?? (_instance = new NonJitClassDescriptionSingleton());

	private NonJitClassDescriptionSingleton()
	{
		_handledClasses.Add(typeof(BlowjobBrainState), BlowjobBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(CowgirlBrainState), CowgirlBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(CuddleBrainState), CuddleBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(GenericFuckBrainState), GenericFuckBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(NewInteractBrainState), NewInteractBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(EquipmentBrainState), EquipmentBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(RoomBrainState), RoomBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(OldMaidBrainState), OldMaidBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(StoryBrainState), StoryBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(EmptyBrainState), EmptyBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(ShowerBrainState), ShowerBrainStateClassDescription.Instance);
		_handledClasses.Add(typeof(GameVariables), GameVariablesClassDescription.Instance);
		AfterInit();
	}

	public static INonJitClassDescription GetClassDescriptionByName(string name)
	{
		return Instance.InstanceGetClassDescriptionByName(name);
	}

	public static NonJitClassDescription<T> GetClassDescription<T>()
	{
		return Instance.InstanceGetClassDescription<T>();
	}
}
