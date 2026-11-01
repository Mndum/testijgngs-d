using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CockTwitchLeaderboardWebsite : Website
{
	public GameObject LeaderboardEntryPrefab;

	private Transform Leaderboard;

	private void GenerateViewerInfo()
	{
		List<Streamer> topStreamers = SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.cockTwitchManager.GetTopStreamers(10);
		for (int i = 0; i < topStreamers.Count; i++)
		{
			Object.Instantiate(LeaderboardEntryPrefab, Leaderboard).GetComponent<TextMeshProUGUI>().text = LOC.UI.cocktwitchdotic_LeaderboardEntry(i + 1, topStreamers[i].Name, CockTwitchManager.FormatViewers(topStreamers[i].Followers), topStreamers[i].Followers);
		}
	}

	public override IEnumerator Open()
	{
		Leaderboard = transform.Find("Leaderboard");
		GenerateViewerInfo();
		DisableAllChildren();
		yield return new WaitForSeconds(0.1f);
		yield return SlowlyEnableAllChildren();
		BrowserPopup.FinishLoading();
	}
}
