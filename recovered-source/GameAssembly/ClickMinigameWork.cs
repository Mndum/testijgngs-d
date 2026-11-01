using System.Collections;
using UnityEngine;

public class ClickMinigameWork : ClickMinigameBase
{
	[SerializeField]
	private float secondReal = 45f;

	[SerializeField]
	private int minutesGame = 480;

	private bool _outOfTime;

	private IEnumerator Timer()
	{
		float seconds = secondReal / (float)minutesGame;
		WaitForSeconds x = new WaitForSeconds(seconds);
		GameScript gs = SingletonLoadableMonoBehaviour<GameScript>.Instance;
		for (int i = 0; i < minutesGame; i++)
		{
			gs.PassMinute(suppressEvents: true, GameVariables.StaminaSyncType.Normal);
			yield return x;
		}
		_outOfTime = true;
	}

	public static string GetScoreName(Score score)
	{
		return score switch
		{
			Score.Okay => LOC.UI.Minigame_Okay, 
			Score.Good => LOC.UI.Minigame_Good, 
			Score.Perfect => LOC.UI.Minigame_Perfect, 
			_ => LOC.UI.Minigame_Miss, 
		};
	}

	protected override IEnumerator Game()
	{
		Restart(0.2f);
		yield return MinigameCore.ShowTextTillClick(transform, LOC.UI.Minigame_Info);
		int time = minutesGame + SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time;
		yield return MinigameCore.ShowTextTillClick(transform, LOC.UI.Minigame_Shift(GameVariables.TimeToHourString(time)));
		StartCoroutine(Timer());
		int count = 1;
		int missCount = 0;
		int okayCount = 0;
		int goodCount = 0;
		int perfectCount = 0;
		bool cont = true;
		while (cont)
		{
			if (_outOfTime)
			{
				yield return MinigameCore.ShowText(transform, LOC.UI.Minigame_Overtime);
				cont = false;
			}
			yield return MinigameCore.ShowText(transform, LOC.UI.Minigame_Ready, 0.25f, 0.1f, 0.1f, canBeSkipped: false);
			yield return MinigameCore.ShowText(transform, LOC.UI.Minigame_Go, 0.25f, 0.1f, 0.1f, canBeSkipped: false);
			_currentScore = Score.None;
			yield return new WaitWhile(() => _currentScore == Score.None);
			switch (_currentScore)
			{
			default:
				missCount++;
				break;
			case Score.Okay:
				okayCount++;
				break;
			case Score.Good:
				goodCount++;
				break;
			case Score.Perfect:
				perfectCount++;
				break;
			}
			yield return MinigameCore.ShowText(transform, GetScoreName(_currentScore), 1f, 0.1f, 0.1f, canBeSkipped: false);
			Restart(0.3f / Mathf.Sqrt(count));
			count++;
		}
		float num = 0.2f;
		float num2 = 0.5f;
		float num3 = 1f;
		StringBuilderCache.AcquireManual(out var stringBuilder);
		stringBuilder.AppendLine($"{GetScoreName(Score.Miss)}: {missCount}");
		stringBuilder.AppendLine($"{GetScoreName(Score.Okay)}: {okayCount}*{num:0.0}={(float)okayCount * num:0.0}");
		stringBuilder.AppendLine($"{GetScoreName(Score.Good)}: {goodCount}*{num2:0.0}={(float)goodCount * num2:0.0}");
		stringBuilder.AppendLine($"{GetScoreName(Score.Perfect)}: {perfectCount}*{num3:0.0}={(float)perfectCount * num3:0.0}");
		yield return MinigameCore.ShowTextTillClick(transform, StringBuilderCache.ReleaseWithToString(stringBuilder));
		float totalScore = (float)okayCount * 0.2f + (float)goodCount * 0.5f + (float)perfectCount;
		if (missCount == 0)
		{
			float num4 = 1.5f;
			float finalTotalScore = totalScore * num4;
			yield return MinigameCore.ShowTextTillClick(transform, LOC.UI.Minigame_TotalScoreNoMiss(totalScore, num4, finalTotalScore));
			totalScore = finalTotalScore;
		}
		else
		{
			yield return MinigameCore.ShowTextTillClick(transform, LOC.UI.Minigame_TotalScore(totalScore));
		}
		_onComplete(totalScore);
	}
}
