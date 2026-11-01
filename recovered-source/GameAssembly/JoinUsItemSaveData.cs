using System;
using UnityEngine;

[Serializable]
public class JoinUsItemSaveData
{
	public enum InteractionTypeEnum
	{
		DownVote = -1,
		Nothing,
		Upvote
	}

	[SerializeField]
	private InteractionTypeEnum _interactionType;

	public InteractionTypeEnum InteractionType => _interactionType;

	public bool CanBeDeleted => _interactionType == InteractionTypeEnum.Nothing;

	public void SetInteraction(InteractionTypeEnum newInteraction)
	{
		_interactionType = newInteraction;
	}
}
