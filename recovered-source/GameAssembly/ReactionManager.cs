using System.Collections.Generic;
using UnityEngine;

public class ReactionManager<T>
{
	public delegate void Reaction(T caller, ComplexEventMemory relatedMemory, int timesCalledInARow);

	private readonly Dictionary<InteractionMemoryEnum, Reaction> _reactions;

	public ReactionManager(Dictionary<InteractionMemoryEnum, Reaction> reactions)
	{
		_reactions = reactions;
	}

	public bool React(ref ReactionManagerData reactionManagerData, InteractionMemory interactionMemory, T state)
	{
		if (_reactions.Count == 0)
		{
			return false;
		}
		if (reactionManagerData.LastEventCount >= 0)
		{
			ComplexEventMemory complexMemory = interactionMemory.GetComplexMemory(reactionManagerData.LastEvent);
			if (complexMemory.Active && _reactions.TryGetValue(reactionManagerData.LastEvent, out var value))
			{
				reactionManagerData.LastEventCount++;
				reactionManagerData.LastSuccessfulReactionTime = Time.timeAsDouble;
				value(state, complexMemory, reactionManagerData.LastEventCount);
				return true;
			}
		}
		List<InteractionMemoryEnum> list;
		using (MyCollectionPool<InteractionMemoryEnum>.GetList(out list))
		{
			foreach (InteractionMemoryEnum key in _reactions.Keys)
			{
				if (interactionMemory.GetComplexMemory(key).Active)
				{
					list.Add(key);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			InteractionMemoryEnum interactionMemoryEnum = (reactionManagerData.LastEvent = list[Random.Range(0, list.Count)]);
			reactionManagerData.LastEventCount = 0;
			reactionManagerData.LastSuccessfulReactionTime = Time.timeAsDouble;
			_reactions[interactionMemoryEnum](state, interactionMemory.GetComplexMemory(interactionMemoryEnum), 0);
			return true;
		}
	}
}
