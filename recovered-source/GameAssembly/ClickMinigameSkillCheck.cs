using System.Collections;
using UnityEngine;

public class ClickMinigameSkillCheck : ClickMinigameBase
{
	[SerializeField]
	private float timeLimit = 2f;

	protected override IEnumerator Game()
	{
		Restart(0.1f);
		yield return MinigameCore.ShowText(transform, LOC.UI.Minigame_Ready, 0.3f, 0.1f, 0.1f, canBeSkipped: false);
		float timeStart = Time.time;
		_currentScore = Score.None;
		float timeLeft;
		do
		{
			float num = Time.time - timeStart;
			timeLeft = timeLimit - num;
			yield return null;
		}
		while (timeLeft > 0f && _currentScore == Score.None);
		switch (_currentScore)
		{
		case Score.None:
		case Score.Miss:
			_onComplete(0f);
			break;
		case Score.Okay:
			_onComplete(0.2f);
			break;
		case Score.Good:
			_onComplete(0.5f);
			break;
		case Score.Perfect:
			_onComplete(1f);
			break;
		}
	}
}
