using System.Collections.Generic;
using UnityEngine;

public abstract class CookingShow : MonoBehaviour
{
	public abstract void StartShow(CookingGameplayStaticGui staticGui);

	public abstract void UpdateShow(double beatCloseness, List<CookingNote> activeNotes);

	public abstract void Score(NoteClickedScoreEnum clickedScoreEnum, CookingNote relatedNote);

	public abstract void EndShow();
}
