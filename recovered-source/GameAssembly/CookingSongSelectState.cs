using UnityEngine;

public class CookingSongSelectState : GameState
{
	private CookingSongSelectStaticGui _gui;

	public override void EnterState()
	{
		CookingStateUtilities.RegisterSilenceMusic();
		GetAndEnableStaticGui<CookingSongSelectStaticGui>(out _gui);
		_gui.StartGui(OnSongSelected);
	}

	private void OnSongSelected(CookingGameplayStartInfo startInfo)
	{
		if (startInfo == null)
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.GoHome();
		}
		else if (startInfo.ActiveModifiers.HasFlagFast(CookingGameplayModifiers.AutoPlay))
		{
			CookingSong song = startInfo.Song;
			if (startInfo.CheatInfo == CookingGameplayCheatStartInfo.NoCheats)
			{
				if (GameVariables.Current.cookingMinigameManager.GetSave(song).TryGetBestDishScore(out var _bestDishScore, out var _))
				{
					CookingGameplayScoreData cookingGameplayScoreData = _bestDishScore.Clone();
					cookingGameplayScoreData.ActiveModifiers = _bestDishScore.ActiveModifiers | CookingGameplayModifiers.AutoPlay;
					SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new CookingResultsState(new CookingResults(startInfo, cookingGameplayScoreData)));
				}
				else
				{
					Debug.LogError("NO SCORE FOUND!");
				}
			}
			else if (startInfo.CheatInfo == CookingGameplayCheatStartInfo.CheatNoSave || startInfo.CheatInfo == CookingGameplayCheatStartInfo.CheatSave)
			{
				CookingGameplayScoreData cookingGameplayScoreData2 = song.RuntimeData.GetPerfectScoreReadonly().Clone();
				cookingGameplayScoreData2.GreatCount--;
				cookingGameplayScoreData2.MissCount++;
				cookingGameplayScoreData2.MaxCombo--;
				cookingGameplayScoreData2.RawScore--;
				cookingGameplayScoreData2.AccuracyScore--;
				if (startInfo.CheatInfo == CookingGameplayCheatStartInfo.CheatNoSave)
				{
					cookingGameplayScoreData2.ActiveModifiers |= CookingGameplayModifiers.AutoPlay;
					startInfo.ActiveModifiers |= CookingGameplayModifiers.AutoPlay;
				}
				else
				{
					cookingGameplayScoreData2.ActiveModifiers &= ~CookingGameplayModifiers.AutoPlay;
					startInfo.ActiveModifiers &= ~CookingGameplayModifiers.AutoPlay;
				}
				SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new CookingResultsState(new CookingResults(startInfo, cookingGameplayScoreData2)));
			}
		}
		else
		{
			SingletonLoadableMonoBehaviour<GameScript>.Instance.ChangeState(new CookingGameplayState(startInfo));
		}
	}

	public override void ExitState()
	{
		CookingStateUtilities.UnregisterSilenceMusic();
		_gui.EndGui();
		DisableAllClaimedStaticGui();
	}

	public override void UpdateState()
	{
	}

	public override void FixedUpdateState()
	{
	}
}
